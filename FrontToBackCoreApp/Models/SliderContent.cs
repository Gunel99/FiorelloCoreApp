﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackCoreApp.Models
{
    public class SliderContent
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, MinLength(150), MaxLength(500)]
        public string Description { get; set; }

        [Required, StringLength(255)]
        public string Image { get; set; }
    }
}
