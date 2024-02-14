﻿using Diplom.Helpers;
using Diplom.Models.Authorization;
using Diplom.Models.Entity;
using Diplom.Services.Interfaces;
using Diplom.ViewModels.Admin;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Diplom.Services.Implementations.Admin
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Basket> _basketRepository;

        public UserService(ILogger<UserService> logger, IBaseRepository<User> userReposytory,
            IBaseRepository<Basket> basketReposytory)
        {
            _logger = logger;
            _userRepository = userReposytory;
            _basketRepository = basketReposytory;
        }

        public async Task<IBaseResponse<User>> Create(UserViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user != null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "Пользователь с таким логином уже есть",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }
                user = new User()
                {

                    Name = model.Name,
                    Role = Enum.Parse<Role>(model.Role),
                    Password = HashPasswordHelper.HashPassword(model.Password),
                    Email = model.Email
                };

                await _userRepository.Create(user);

                var basket = new Basket()
                {
                    UserId = user.Id,
                };

                await _basketRepository.Create(basket);

                return new BaseResponse<User>()
                {
                    Data = user,
                    Description = "Пользователь добавлен",
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserService.Create] error: {ex.Message}");
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUser(int id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                await _userRepository.Delete(user);
                _logger.LogInformation($"[UserService.DeleteUser] пользователь удален");

                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };

            }
        }

        public IBaseResponse<Dictionary<int, string>> GetRoles()
        {
            try
            {
                var roles = ((Role[])Enum.GetValues(typeof(Role)))
                    .ToDictionary(y => (int)y, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = roles,
                    StatusCode = StatusCode.OK,
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };

            }
        }
        public async Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAll()
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Role = x.Role.ToString(),
                        Email = x.Email
                    })
                    .ToListAsync();
                _logger.LogInformation($"[UserService.GetUsers] получено элементов {users.Count}");

                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.GetUsers] error: {ex.Message}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}