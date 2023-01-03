namespace HTC_SHOPVIP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Blog_Danhgia
    {
        [Key]
        public int MaDanhGia { get; set; }

        public string NoidungDanhgia { get; set; }

        public int MaBlog { get; set; }

        public int? MaKH { get; set; }

        public virtual Blog Blog { get; set; }

        public virtual Khachhang Khachhang { get; set; }
    }
}
