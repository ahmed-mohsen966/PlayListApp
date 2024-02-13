using PlayListApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlayListApp.Models
{
    public class Songs
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SongTypeEnum Type { get; set; }
        public Guid SingerId { get; set; }

        [ForeignKey(nameof(SingerId))]
        public Singer Singer { get; set; }
    }
}