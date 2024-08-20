using eFinance.Models;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace eFinance.Controller
{
    public class LoginRequest
    {
        [Required]
        public string email { get; set; } = string.Empty;
        [Required]
        public string password { get; set; } = string.Empty;
    }

    public class mstKaryawanLoginController : ApiController
    {
        private eFinanceContext db = new eFinanceContext();
        private Systems ObjSys = new Systems();
        
        [HttpPost]
        public IHttpActionResult Login([FromBody] LoginRequest request)
        {
            var karyawan = db.mstKaryawans
                .Include(k => k.Cabang)
                .Include(k => k.Jabatan)
                .Where(k => k.email == request.email)
                .Select(k => new
                {
                    k.noKaryawan,
                    k.nama,
                    k.Cabang.namaCabang,
                    k.Cabang.noTelpCabang,
                    k.Cabang.latitude,
                    k.Cabang.longitude,
                    k.Jabatan.Jabatan,
                    k.email,
                    k.password
                })
                .FirstOrDefault();

            if (karyawan == null || ObjSys.Sha1(request.password) != karyawan.password)
            {
                return BadRequest("Wrong email or password");
            }

            return Ok(new
            {
                Status = true,
                Data = new
                {
                    karyawan.noKaryawan,
                    karyawan.email,
                    karyawan.nama,
                    karyawan.namaCabang,
                    karyawan.noTelpCabang,
                    karyawan.latitude,
                    karyawan.longitude,
                    karyawan.Jabatan,
                }
            });
        }
    }
}
