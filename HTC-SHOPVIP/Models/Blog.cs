namespace HTC_SHOPVIP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Blog")]
    public partial class Blog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Blog()
        {
            Blog_Danhgia = new HashSet<Blog_Danhgia>();
        }

        [Key]
        public int MaBLOG { get; set; }

        [StringLength(200)]
        public string TenBlog { get; set; }

        public string Noidung { get; set; }

        public int MaTheloai { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngayviet { get; set; }

        [StringLength(50)]
        public string tacgia { get; set; }

        public string Tomtat { get; set; }

        public string Modau { get; set; }

        public string Tags { get; set; }

        public virtual Blog_anh Blog_anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Blog_Danhgia> Blog_Danhgia { get; set; }

        public virtual Blog_Theloai Blog_Theloai { get; set; }
    }
}
