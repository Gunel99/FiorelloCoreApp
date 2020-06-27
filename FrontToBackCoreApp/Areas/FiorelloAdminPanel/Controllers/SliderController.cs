using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FrontToBackCoreApp.DAL;
using FrontToBackCoreApp.Extentions;
using FrontToBackCoreApp.Helpers;
using FrontToBackCoreApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBackCoreApp.Areas.FiorelloAdminPanel.Controllers
{
    [Area("FiorelloAdminPanel")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;

        public SliderController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {
            ViewBag.SliderCount = _db.Sliders.Count();
            return View(_db.Sliders);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();

            Slider slider = await _db.Sliders.FindAsync(id);

            if (slider == null) return NotFound();

            return View(slider);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if(ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa, sekil formati secin!");
                return View();
            }

            if(slider.Photo.MaxLengthFile(300))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu maxsimum 200kb olmalidir!");
            }

            int count = _db.Sliders.Count();

            if(count >= 5)
            {
                ModelState.AddModelError("", "Sliderde maximum 5 sekil ola biler!");
                return View();
            }

            slider.Image = await slider.Photo.SaveFile(_env.WebRootPath, "img");
            await _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Slider slider = await _db.Sliders.FindAsync(id);

            if (slider == null) return NotFound();

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();

            Slider slider = await _db.Sliders.FindAsync(id);

            if (slider == null) return NotFound();

            Helper.DeleteImg(_env.WebRootPath, "img", slider.Image);

            _db.Sliders.Remove(slider);
            await _db.SaveChangesAsync();

            return  RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            Slider slider = await _db.Sliders.FindAsync(id);

            if (slider == null) return NotFound();

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Slider slider)
        {
            if (id == null) return NotFound();
            if(slider.Photo != null)
            {
                if (!slider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa, sekil formati secin!");
                    return View();
                }

                if (slider.Photo.MaxLengthFile(300))
                {
                    ModelState.AddModelError("Photo", "Sekilin olcusu maxsimum 300kb olmalidir!");
                }

                Slider dbSlider = await _db.Sliders.FindAsync(id);
                Helper.DeleteImg(_env.WebRootPath, "img", dbSlider.Image);
                dbSlider.Image = await slider.Photo.SaveFile(_env.WebRootPath, "img");

                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}