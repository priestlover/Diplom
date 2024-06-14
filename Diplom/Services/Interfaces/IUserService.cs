using Diplom.Models.Authorization;
using Diplom.ViewModels.Admin;

namespace Diplom.Services.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> Create(UserViewModel model);
        Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsers();
        Task<IBaseResponse<bool>> DeleteUser(int id);
        IBaseResponse<Dictionary<int, string>> GetRoles();
        Task<IBaseResponse<string>> GetUserEmail(string userName);
    }
}
