using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackCoreApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Zehmet olmasa, xanani doldurun!"), MaxLength(50, ErrorMessage = "Simvol sayi 100-den cox ola bilmez!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Zehmet olmasa, xanani doldurun!"), StringLength(500, ErrorMessage = "Simvol sayi 500-den cox ola bilmez!")]
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
