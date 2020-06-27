using FrontToBackCoreApp.DAL;
using FrontToBackCoreApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackCoreApp.ViewComponents
{
    public class ProductsViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public ProductsViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _db.Products.Select(p => new ProductVM
            {
                Id = p.Id,
                Price = p.Price,
                Title = p.Title,
                Image = p.Image,
                Category = p.Category
            }).Take(8);

            return View(await Task.FromResult(model));
        }
    }
}
