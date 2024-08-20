namespace eFinance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Http;

    [Table("mKategori")]
    public partial class mKategori
    {
        [Key]
        public int noKategori { get; set; }
        public string jns { get; set; }
        public string kategori { get; set; }
        public int? norek { get; set; }
        public string sts { get; set; }
    }
}
