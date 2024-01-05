using Diplom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;


        public BasketController(IBasketService basketService) 
        { 
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> Basket()
        {
            var response = await _basketService.GetOrders(User.Identity.Name);

            if(response.StatusCode == Models.Authorization.StatusCode.OK)
            {
                return View(response.Data.ToList());
            }
            return View("Index","Home");
        }

        public async Task<IActionResult> DeleteOrder(int id)
        {
            var response = await _basketService.DeleteOrder(id);

            if (response.StatusCode == Models.Authorization.StatusCode.OK)
            {
                return RedirectToAction("Basket");
            }
            return RedirectToAction("Index","Home");
        }

    }
}
