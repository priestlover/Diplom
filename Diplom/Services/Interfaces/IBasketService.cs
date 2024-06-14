using Diplom.Models.Entity;
using Diplom.ViewModels;

namespace Diplom.Services.Interfaces
{
    public interface IBasketService
    {
        Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetOrders(string userName);
        Task<IBaseResponse<bool>> DeleteOrder(int id);
        Task<IBaseResponse<bool>> DeleteAllOrders(string userName);
        Task<IBaseResponse<string>> GetStringOrder(string userName);
    }
}
