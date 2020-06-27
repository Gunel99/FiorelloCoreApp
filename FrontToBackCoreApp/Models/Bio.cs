using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackCoreApp.Models
{
    public class Bio
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string LogoImage { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
    }
}
