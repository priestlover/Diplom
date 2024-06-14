using Diplom.Services.Interfaces;
using Diplom.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public BasketController(IBasketService basketService,IUserService userService,IEmailService emailService) 
        { 
            _basketService = basketService;
            _emailService = emailService;
            _userService = userService;
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

        public async Task<IActionResult> Order(List<OrderViewModel> orders)
        {
            var userName = User.Identity.Name;
            var userMail = await _userService.GetUserEmail(userName);
            var message = await _basketService.GetStringOrder(userName);


            await _emailService.SendEmailAsync(userMail.Data,"Заказ Diplom", message.Data);
            await _basketService.DeleteAllOrders(userName);

            return RedirectToAction("Basket");
        }
    }
}
