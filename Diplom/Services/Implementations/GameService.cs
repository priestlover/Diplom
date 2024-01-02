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
        private readonly ILogger<GameService> _logger;

        public GameService(IBaseRepository<Game> gameRepository,IBaseRepository<User> userRepository,
            IBaseRepository<GameDescription> gameDesc,IBaseRepository<Order> orderRepository,ILogger<GameService> logger) 
        {
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            _gameDescRepository = gameDesc;
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<bool>> AddToBasket(string userName, int gameId)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userName);

                var order = new Order
                {
                    CreatedDate = DateTime.Now,
                    GameId = gameId,
                    BasketId = user.Basket.Id
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
                var game = _gameRepository?.GetAll().FirstOrDefault(x => x.Id == id);
                var desc = _gameDescRepository?.GetAll().FirstOrDefault(y => y.GameId == game.Id);

                var response = new GameViewModel
                {
                    Id = game.Id, 
                    Name = game.Name,
                    Price = game.Price,
                    ImgSource = game.ImgSource,
                    Date = game.Date,
                    Description = desc.Text
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
    }
}
