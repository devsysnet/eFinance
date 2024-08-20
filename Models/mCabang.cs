namespace eFinance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Http;

    [Table("mCabang")]
    public partial class mCabang
    {
        [Key]
        public int noCabang { get; set; }
        public int parent { get; set; }
        public string kdCabang { get; set; }
        public string namaCabang { get; set; }
        public string alamatCabang { get; set; }
        public string noTelpCabang { get; set; }
        public string noFaxCabang { get; set; }
        public string kodePosCabang { get; set; }
        public string emailCabang { get; set; }
        public string kotaCabang { get; set; }
        public string namaOfficerFin { get; set; }
        public string jnsSekolah { get; set; }
        public string stsPusat { get; set; }
        public string stsCabang { get; set; }
        public string linkUrl { get; set; }
        public string kategoriUsaha { get; set; }

        public decimal latitude { get; set; }
        public decimal longitude { get; set; }

        public List<mstKaryawan> karyawans { get; set; }
    }
}
