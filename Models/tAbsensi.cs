using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eFinance.Models
{
    [Table("tAbsensi")]
    public class tAbsensi
    {
        [Key]
        public Int64 noAbsensi { get; set; }
        public int noKaryawan { get; set; }

        public DateTime? tgl { get; set; }
        public string jamMasuk { get; set; } = null;
        public string jamKeluar { get; set; } = null;

        public string sts { get; set; }
        public int? noCabang { get; set; } = null;
        public DateTime createdDate { get; set; }
    }
}