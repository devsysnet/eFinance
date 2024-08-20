using eFinance.Models;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System;
using System.Threading.Tasks;

namespace eFinance.Controller
{
    public class PostAbsenRequest
    {
        [Required]
        public int? noKaryawan { get; set; }
        [Required]
        public string tipeAbsensi { get; set; } = string.Empty; // "masuk" atau "keluar"
    }

    public class GetAbsenRequest
    {
        [Required]
        public int? noKaryawan { get; set; }
        [Required]
        public int bulan { get; set; }
    }

    public class tAbsensiController : ApiController
    {
        private eFinanceContext db = new eFinanceContext();
        
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] PostAbsenRequest request)
        {
            DateTime now = DateTime.Now;
            
            var absensi = await db.tAbsensis
                .Where(a => a.noKaryawan == request.noKaryawan && a.tgl.Value.Date.Equals(now.Date))
                .FirstOrDefaultAsync();

            if (absensi == null)
            {
                db.tAbsensis.Add(new tAbsensi
                {
                    noKaryawan = request.noKaryawan.Value,
                    tgl = now,
                    jamMasuk = now.ToShortTimeString(),
                    jamKeluar = null,
                    sts = "1",
                    noCabang = null,
                    createdDate = now
                });
            }
            else
            {
                if (request.tipeAbsensi == "masuk")
                {
                    absensi.jamMasuk = now.ToShortTimeString();
                }
                else
                {
                    absensi.jamKeluar = now.ToShortTimeString();
                }
            }


            await db.SaveChangesAsync();

            return Ok();
        }
        

        [HttpGet]
        public IHttpActionResult Get([FromUri] GetAbsenRequest request)
        {
            return Ok(
                db.tAbsensis
                    .Where(a =>
                        a.noKaryawan == request.noKaryawan.Value &&
                        a.tgl.Value.Month.Equals(request.bulan)
                    )
                    .ToList()
            );
        }
    }
}
