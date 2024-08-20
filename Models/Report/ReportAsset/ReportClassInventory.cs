using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eFinance.Models.Report.ReportAsset
{
    public partial class ReportClassInventory
    {
        public string kode_barang { get; set; }
        public string namaBarang { get; set; }
        public string lokasi { get; set; }
        public int bulan { get; set; }
        public int tahun { get; set; }
        public int nocabang { get; set; }
        public string SubLokasi { get; set; }
        public string namaCabang { get; set; }
        public int jumlah { get; set; }
    }
}