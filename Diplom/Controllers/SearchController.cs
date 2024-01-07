using Diplom.Services.Implementations;
using Diplom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService) 
        { 
            _searchService = searchService;
        }

        public async Task<IActionResult> Search(string searchStr)
        {
            var response = await _searchService.Search(searchStr);

            if (response.StatusCode == Diplom.Models.Authorization.StatusCode.OK)
            {
                return View(response.Data.ToList());
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Catalog()
        {
            var response = await _searchService.Search("");

            if (response.StatusCode == Diplom.Models.Authorization.StatusCode.OK)
            {
                return View("Search",response.Data.ToList());
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
