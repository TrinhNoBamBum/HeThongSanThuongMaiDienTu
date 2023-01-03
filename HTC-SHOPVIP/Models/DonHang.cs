namespace HTC_SHOPVIP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            CTDonHangs = new HashSet<CTDonHang>();
        }

        [Key]
        public int MaDon { get; set; }

        public DateTime? Ngaydat { get; set; }

        public int MaKH { get; set; }

        public int MaKM { get; set; }

        public int? Tongtien { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        [StringLength(250)]
        public string DiachiGui { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDonHang> CTDonHangs { get; set; }

        public virtual Khachhang Khachhang { get; set; }
    }
}
