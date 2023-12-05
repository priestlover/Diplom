using Diplom.Models.Authorization;
using Diplom.Models.Entity;
using Diplom.Services.Interfaces;
using Diplom.ViewModels.Account;
using System.Security.Claims;

namespace Diplom.Services.Implementations
{
    public class AccountService: IAccountService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Basket> _basketRepository;


        public Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<ClaimsIdentity>> Register(SignUpViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
