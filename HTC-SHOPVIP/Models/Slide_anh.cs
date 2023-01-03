namespace HTC_SHOPVIP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Slide_anh
    {
        [Key]
        public int MaSlide { get; set; }

        [StringLength(250)]
        public string Linkanh { get; set; }
    }
}
