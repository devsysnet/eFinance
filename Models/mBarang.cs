namespace eFinance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Http;

    [Table("mBarang")]
    public partial class mBarang
    {
        [Key]
        public int noBarang { get; set; }
        public string kodeBarang { get; set; }
        public string namaBarang { get; set; }
        public string jenisBrg { get; set; }
        public int kategoriBarang { get; set; }
        public int? norek { get; set; }
        public string uraian { get; set; }
        public string kodeAset { get; set; }
        public int? nocabang { get; set; }
        public string kelas { get; set; }
        public string Kategori { get; set; }
        public int harga { get; set; }
    }
}
