﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlayListApp.Models
{
    public class Singer
    {
        public Guid Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }
        public byte[] Image { get; set; }

        [Display(Name = "Songs")]
        public ICollection<Songs> Songs { get; set; } = new HashSet<Songs>();
        [NotMapped]
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}