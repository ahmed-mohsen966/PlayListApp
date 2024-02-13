using PlayListApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlayListApp.Models.ViewModels
{
    public class SingerSongsViewModel
    {
        public string SongName { get; set; }
        public string SongType { get; set; }
        public Guid SingerId { get; set; }

        [Required, MaxLength(150)]
        public string SingerName { get; set; }
        public byte[] Image { get; set; }

        [NotMapped]
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}