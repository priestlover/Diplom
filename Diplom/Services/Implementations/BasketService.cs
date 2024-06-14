using Diplom.Models.Authorization;
using Diplom.Models.Entity;
using Diplom.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Diplom.ViewModels;
using Diplom.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace Diplom.Services.Implementations
{
    public class BasketService:IBasketService
    {
        private readonly IBaseRepository<Game> _gameRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<Basket> _basketRepository;
        private readonly ILogger<GameService> _logger;

        public BasketService(IBaseRepository<Game> gameRepository, IBaseRepository<User> userRepository, IBaseRepository<Basket> basketRepository,
            IBaseRepository<Order> orderRepository, ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<bool>> DeleteOrder(int id)
        {
            try
            {
                var order = await _orderRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (order == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.OrderNotFound,
                        Data = false
                    };
                }

                await _orderRepository.Delete(order);
                _logger.LogInformation($"[OrderService.DeleteOrder] заказ удален");

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[OrderService.DeleteOrder] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetOrders(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include( x => x.Basket)
                    .ThenInclude(x => x.Orders)
                    .FirstOrDefaultAsync(x => x.Name == userName);

                var response = from o in user.Basket?.Orders
                               join g in _gameRepository.GetAll() on o.GameId equals g.Id
                               select new OrderViewModel()
                               {
                                   Id = o.Id,
                                   Name = g.Name,
                                   ImgSource = g.ImgSource,
                                   CreatedDate = o.CreatedDate,
                                   Price = g.Price
                               };
                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<OrderViewModel>> ()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteAllOrders(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                                 .Include(x => x.Basket)
                                 .ThenInclude(x => x.Orders)
                                 .FirstOrDefaultAsync(x => x.Name == userName);
                var orders = user.Basket.Orders;

                for(int i = 0; orders.Count != 0; )
                {
                    await _orderRepository.Delete(orders[i]);
                }

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true,
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }

        public async Task<IBaseResponse<string>> GetStringOrder(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                        .Include(x => x.Basket)
                        .ThenInclude(x => x.Orders)
                        .FirstOrDefaultAsync(x => x.Name == userName);

                var orders = from o in user.Basket?.Orders
                               join g in _gameRepository.GetAll() on o.GameId equals g.Id
                               select new OrderViewModel()
                               {
                                   Id = o.Id,
                                   Name = g.Name,
                                   ImgSource = g.ImgSource,
                                   CreatedDate = o.CreatedDate,
                                   Price = g.Price
                               };
                string response = string.Empty;
                foreach (var order in orders)
                {
                    response += order.Name + " - " + KeyGenerator.GenerateKey() + "\n";
                }

                return new BaseResponse<string>()
                {
                    StatusCode = StatusCode.OK,
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }

        }
    }
}
