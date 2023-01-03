namespace HTC_SHOPVIP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SP_Danhgia
    {
        [Key]
        public int Madanhgia_sp { get; set; }

        public int MaSP { get; set; }

        public string noidung_danhgia { get; set; }

        public int MaKH { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDanhgia { get; set; }

        public int? Sao { get; set; }

        public virtual Khachhang Khachhang { get; set; }

        public virtual Sanpham Sanpham { get; set; }
    }
}
