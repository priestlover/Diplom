using Diplom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Diplom.ViewModels;

namespace Diplom.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService) 
        {    
            _gameService = gameService;
        }


        public async Task<IActionResult> Game(int id)
        {
            var response = await _gameService.GetGame(id);

            if (response.StatusCode == Diplom.Models.Authorization.StatusCode.OK)
            {
                return View(response.Data);            
            }

            return RedirectToAction("Index","Home");
        }






    }
}
