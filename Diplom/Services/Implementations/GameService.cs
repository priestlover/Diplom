using Diplom.Models.Entity;
using Diplom.Services.Implementations.Admin;
using Diplom.Services.Interfaces;
using Diplom.Models.Authorization;
using Microsoft.EntityFrameworkCore;
using Diplom.ViewModels;

namespace Diplom.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IBaseRepository<Game> _gameRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<GameDescription> _gameDescRepository;
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<Basket> _basketRepository;
        private readonly IBaseRepository<Tag> _tagRepository;
        private readonly ILogger<GameService> _logger;

        public GameService(IBaseRepository<Game> gameRepository,IBaseRepository<User> userRepository,IBaseRepository<Basket> basketRepository,
            IBaseRepository<GameDescription> gameDesc,IBaseRepository<Tag> tagRepository,IBaseRepository<Order> orderRepository,ILogger<GameService> logger) 
        {
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            _gameDescRepository = gameDesc;
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _tagRepository = tagRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<IEnumerable<Game>>> GetAll()
        {
            try
            {
                var response = await _gameRepository.GetAll().ToListAsync();

                return new BaseResponse<IEnumerable<Game>>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Game>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };

            }
        }



        public async Task<IBaseResponse<bool>> AddToBasket(string userName, int gameId)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userName);
                var basket = await _basketRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id);

                var order = new Order
                {
                    CreatedDate = DateTime.Now,
                    GameId = gameId,
                    BasketId = basket.Id
                };

                await _orderRepository.Create(order);

                return new BaseResponse<bool>()
                {
                    Description = "Заказ создан",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteGame(int id)
        {
            try
            {
                var game = await _gameRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (game == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.GameNotFound,
                        Data = false
                    };
                }

                await _gameRepository.Delete(game);
                _logger.LogInformation($"[GameService.DeleteGame] игра удалена");

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GameService.DeleteGame] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<GameViewModel>> GetGame(int id)
        {
            try
            {
                var game =  await _gameRepository.GetAll().Include(x=>x.Tags).FirstOrDefaultAsync(x => x.Id == id);
                var desc = await _gameDescRepository.GetAll().FirstOrDefaultAsync(y => y.GameId == game.Id);

                var response = new GameViewModel
                {
                    Id = game.Id, 
                    Name = game.Name,
                    Price = game.Price,
                    ImgSource = game.ImgSource,
                    Date = game.Date,
                    Description = desc.Text,
                    Tags = string.Join(",",game.Tags.Select(x => x.TagId)),

                };
                
                return new BaseResponse<GameViewModel>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex){

                return new BaseResponse<GameViewModel>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> AddGame(GameViewModel gameToAdd)
        {
            try
            {
                var notTags = gameToAdd.Tags.Split(' ').ToList();

                ICollection<Tag> tags = new List<Tag>();

                foreach(var tag in notTags)
                {
                    tags.Add(new Tag { TagId = tag });
                }

                var game = new Game
                {
                    Name = gameToAdd.Name,
                    Price = gameToAdd.Price,
                    ImgSource = gameToAdd.ImgSource,
                    Date = gameToAdd.Date,
                };

                await _gameRepository.Create(game);

                foreach(var tagToAdd in tags)
                {
                    var tag = await _tagRepository.GetAll().FirstOrDefaultAsync(x => x.TagId == tagToAdd.TagId);
                    tag.Games.Add(game);
                    await _tagRepository.Update(tag);
                }

                var desc = new GameDescription
                {
                    Text = gameToAdd.Description,
                    GameId = game.Id,
                };

                await _gameDescRepository.Create(desc);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> UpdateGame(GameViewModel gameToUpdate)
        {
            try
            {
                var game = await _gameRepository.GetAll()
                    .Include(x => x.GameDescription)
                    .Include(x => x.Tags)
                    .FirstOrDefaultAsync(x => x.Id == gameToUpdate.Id);
                if (game != null)
                {
                    game.Name = gameToUpdate.Name;
                    game.ImgSource = gameToUpdate.ImgSource;
                    game.Price = gameToUpdate.Price;
                    game.Date = gameToUpdate.Date;
                    game.GameDescription.Text = gameToUpdate.Description;
                }

                _gameRepository.Update(game);

                return new BaseResponse<bool>
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }


        }

    }
}
