using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontToBackCoreApp.DAL;
using FrontToBackCoreApp.Models;
using FrontToBackCoreApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontToBackCoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = _db.Sliders,
                SliderContents = _db.SliderContents.FirstOrDefault(),
                Categories = _db.Categories
            };
            return View(homeVM);
        }

        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null) return NotFound();
            Product product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            List<Product> products;
            string exitBasket = Request.Cookies["basket"];

            if (exitBasket != null)
            {
                products = new List<Product>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<Product>>(exitBasket);
            }

            products = new List<Product>();
            products.Add(product);
            string basket = JsonConvert.SerializeObject(products);
            Response.Cookies.Append("basket", basket, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Basket()
        {
            return Content(Request.Cookies["basket"]);
        }
    }
}