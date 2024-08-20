using eFinance.Models;
using eFinance.Models.Report.ReportAsset;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace eFinance.Controllers
{
    public class ReportAssetController : ApiController
    {
        private eFinanceContext _context = new eFinanceContext();

        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        // GET api/ReportAsset
        public IEnumerable<dynamic> Get([FromUri] string url_mhs = null)
        {
            return _context.Database.SqlQuery<Asset>("exec APISiswa").ToList<Asset>();
        }

        // GET api/ReportAsset/ReportClassInventory
        [HttpGet]
        [Route("api/ReportAsset/ReportClassInventory")]
        public List<ReportClassInventory> ReportClassInventory([FromUri] string url_mhs = null)
        {
            string query = @"
                select kodeAsset as kode_barang,a.namaBarang,b.Lokasi,MONTH(tglAsset) as bulan,
                    YEAR(tglAsset) as tahun,a.nocabang,SubLokasi,namaCabang,
                    COUNT(noBrg) as jumlah
                from Tasset a
                    inner join mLokasi b on a.nolokasi=b.nolokasi
                    inner join mSubLokasi c on a.noSubLokasi=c.noSubLokasi and b.nolokasi=c.nolokasi
                    inner join mCabang d on a.nocabang=d.noCabang and b.nocabang=d.noCabang
                where d.[linkUrl]='" + url_mhs + 
                "'group by kodeAsset,a.namaBarang,b.Lokasi,MONTH(tglAsset),YEAR(tglAsset),a.nocabang,SubLokasi,namaCabang";
            return _context.Database
                .SqlQuery<ReportClassInventory>(query)
                .ToList();
        }
    }
}