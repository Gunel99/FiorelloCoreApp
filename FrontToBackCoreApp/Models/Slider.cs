using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackCoreApp.Models
{
    public class Slider
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Image { get; set; }

        [NotMapped]  // yeni bu column tekce front ucun nezerde tutulub bu columnu db-e vurma
        [Required(ErrorMessage = "Zehmet olmasa, sekil secin!")]
        public IFormFile Photo { get; set; }  
    }
}
