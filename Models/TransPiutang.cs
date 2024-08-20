namespace eFinance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransPiutang")]
    public partial class TransPiutang
    {
        [Key]
        public int noPiutang { get; set; }

        [StringLength(50)]
        public string tahunajaran { get; set; }

        public int? nosiswa { get; set; }

        public int? noTransaksi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? tgl { get; set; }

        [Column(TypeName = "date")]
        public DateTime? tgljttempo { get; set; }

        public decimal? nilai { get; set; }

        public decimal? nilaibayar { get; set; }

        public decimal? saldo { get; set; }

        public int? nocabang { get; set; }

        [StringLength(1)]
        public string stscetak { get; set; }

        public string catatan { get; set; }

        public int? nobank { get; set; }

        [StringLength(50)]
        public string nomorKode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? tglbayar { get; set; }

        [StringLength(100)]
        public string noref { get; set; }
    }
}
