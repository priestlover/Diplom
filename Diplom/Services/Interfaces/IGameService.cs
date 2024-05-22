using Diplom.Models.Entity;
using Diplom.ViewModels;

namespace Diplom.Services.Interfaces
{
    public interface IGameService
    {
        Task<IBaseResponse<GameViewModel>> GetGame(int id);
        Task<IBaseResponse<bool>> DeleteGame(int id);
        Task<IBaseResponse<bool>> AddToBasket(string userName, int gameId);
        Task<IBaseResponse<IEnumerable<Game>>> GetAll();
    }
}
