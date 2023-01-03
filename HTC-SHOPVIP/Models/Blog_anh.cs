namespace HTC_SHOPVIP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Blog_anh
    {
        [Key]
        public int MaBlog { get; set; }

        [StringLength(250)]
        public string Linkanh { get; set; }

        [StringLength(250)]
        public string Anh1 { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
