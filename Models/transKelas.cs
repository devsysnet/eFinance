namespace eFinance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Http;

    [Table("transkelas")]
    public partial class transKelas
    {
        [Key]
        public int notKelas { get; set; }
        public int nosiswa { get; set; }
        public string namaSiswa { get; set; }
        public string kelas { get; set; }
        public string tahunajaran { get; set; }
        public decimal? nilaibayar { get; set; }
        public int? nocabang { get; set; }
        public string classGrade { get; set; }
    }
}
