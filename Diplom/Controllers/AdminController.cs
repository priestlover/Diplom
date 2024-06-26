﻿using Diplom.Services.Interfaces;
using Diplom.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Diplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IGameService _gameService;

        public AdminController(IUserService userService, IGameService gameService)
        {
            _userService = userService;
            _gameService = gameService;
        }

     
        public IActionResult Admin() => View();

        public async Task<IActionResult> GetUsers()
        {

            var response = await _userService.GetUsers();

            if (response.StatusCode == Models.Authorization.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
          
            if (response.StatusCode == Models.Authorization.StatusCode.OK)
            {
                return RedirectToAction("GetUsers");
            }
            return RedirectToAction("Index");
        }

        public  async Task<IActionResult> Games()
        {
            var response = await _gameService.GetAll();

            if(response.StatusCode == Models.Authorization.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index");
        }

        public async Task <IActionResult> DeleteGame(int id)
        {
            var response = await _gameService.DeleteGame(id);

            if(response.StatusCode == Models.Authorization.StatusCode.OK)
            {
                return RedirectToAction("Games");
            }

            return RedirectToAction("Index");
        }

        public IActionResult AddGame() => View();

        public async Task<IActionResult> AddGameToDb(GameViewModel game)
        {
            var response = await _gameService.AddGame(game);

            if(response.StatusCode == Models.Authorization.StatusCode.OK) 
            {
                return RedirectToAction("Admin");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateGame(int id)
        {
            var response = await _gameService.GetGame(id);

            if(response.StatusCode == Models.Authorization.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateGameToDb(int gameId,GameViewModel game)
        {
            game.Id = gameId;
            var response = await _gameService.UpdateGame(game);

            if (response.StatusCode == Models.Authorization.StatusCode.OK)
            {
                return RedirectToAction("Admin");
            }
            return RedirectToAction("Index");
        }

    }
}
