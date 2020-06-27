using FrontToBackCoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackCoreApp.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SliderContent SliderContents { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
