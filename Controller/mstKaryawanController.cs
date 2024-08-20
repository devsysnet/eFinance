using eFinance.Models;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;

namespace eFinance.Controller
{
    public class mstKaryawanController : ApiController
    {
        private eFinanceContext db = new eFinanceContext();

        //GET: api/mstKaryawan/{nokaryawan}
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var karyawans = db.mstKaryawans
                .Include(k => k.Cabang)
                .Include(k => k.Jabatan)
                .Select(k => new
                {
                    k.noKaryawan,
                    k.nama,
                    k.Cabang.namaCabang,
                    k.Cabang.noTelpCabang,
                    k.Cabang.latitude,
                    k.Cabang.longitude,
                    k.Jabatan.Jabatan
                })
                .ToList();
            return Ok(karyawans);
        }
        
        [HttpGet]
        public IHttpActionResult GetByNoKaryawan(int id)
        {
            var karyawan = db.mstKaryawans
                .Include(k => k.Cabang)
                .Include(k => k.Jabatan)
                .Where(k => k.noKaryawan == id)
                .Select(k => new
                {
                    k.noKaryawan,
                    k.nama,
                    k.Cabang.namaCabang,
                    k.Cabang.noTelpCabang,
                    k.Cabang.latitude,
                    k.Cabang.longitude,
                    k.Jabatan.Jabatan
                })
                .FirstOrDefault();
            return Ok(karyawan);
        }
    }
}
