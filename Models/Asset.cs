namespace eFinance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Http;

  
    public partial class Asset
    {
        [Key]
        public int noSiswa { get; set; }
        public string namaSiswa { get; set; }
        public string nik { get; set; }
        public string nis { get; set; }
        public string nisn { get; set; }
        public string gender { get; set; }
        public int? agama { get; set; }
        public string alamat { get; set; }
        public int? kota { get; set; }

        [Column(TypeName = "date")]
        public DateTime? tgllahir { get; set; }
        public string namaOrangTua { get; set; }
        public string telp { get; set; }
        public string sts { get; set; }
        public int nocabang { get; set; }
        public string kelas { get; set; }
        public string tahunAjaran { get; set; }
        
    }

}
