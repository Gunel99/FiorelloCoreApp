using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontToBackCoreApp.DAL;
using FrontToBackCoreApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBackCoreApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Products.Select(p=> new ProductVM
            { 
                Id = p.Id,
                 Price = p.Price,
                  Title = p.Title,
                   Image = p.Image,
                    Category = p.Category
            }).Take(8));
        }

        public IActionResult Load()
        {
            var model = _db.Products.Select(p => new ProductVM
            {
                Id = p.Id,
                Price = p.Price,
                Title = p.Title,
                Image = p.Image,
                Category = p.Category
            }).Skip(8).Take(8);

            return PartialView("_ProductPartial", model);
        }
    }
}