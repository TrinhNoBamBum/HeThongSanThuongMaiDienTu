using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTC_SHOPVIP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AnhSPLienKet")]
    public partial class AnhSPLienKet
    {
        [Key]
        public int Masp { get; set; }
        public string TenSP { get; set; }
        public string Linkanh { get; set; }
        public int Gia { get; set; }

    }
}