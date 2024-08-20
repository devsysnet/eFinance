using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using eFinance.Models;
using System.Web.Http.Results;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Text;

namespace eFinance.Controller
{
    public class mSiswaController : ApiController
    {
        private eFinanceContext db = new eFinanceContext();

        // GET: api/mSiswas

        //public JsonResult<List<mSiswa>> GetSiswas()
        //{
        //    var siswas = db.mSiswas.ToList();
        //    return Json(siswas);
        //}

        //// GET: api/mSiswas/1
        //[ResponseType(typeof(mSiswa))]
        //public async Task<IHttpActionResult> GetSiswa(int id)
        //{
        //    //data lebih dari satu menggunakan list
        //    List<mSiswa> msiswa = await db.mSiswas
        //        .Where(siswa => siswa.noSiswa == id)
        //        .ToListAsync(); //select * from msiswa where nosiswa = id

        //    //ini kalau misalnya ingin satu data saja
        //    //mSiswa msiswa = await db.mSiswas
        //    //    .Where(siswa => siswa.noSiswa == id)
        //    //    .FirstAsync(); //select top 1 * from msiswa where nosiswa = id

        //    if (msiswa == null)
        //    {
        //        return NotFound();
        //    }

        //    return Json(msiswa);
        //}

        public HttpResponseMessage GetSiswa(int id)
        {
            try
            {
                var id_siswa = new SqlParameter("@nosiswa", id);
                var siswaList = db.Database.SqlQuery<mSiswa>("exec APISiswa @nosiswa=" + id_siswa.Value + "").ToList<mSiswa>();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JArray.FromObject(siswaList).ToString(), Encoding.UTF8, "application/json")
                };

            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                { Content = new StringContent(e.Message, Encoding.UTF8, "application/json") };
                //return null;
            }
        }
        // PUT: api/mSiswas/1
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMSiswa(int id, mSiswa msiswa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != msiswa.noSiswa)
            {
                return BadRequest();
            }

            db.Entry(msiswa).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mSiswaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/mSiswas
        [ResponseType(typeof(mSiswa))]
        public async Task<IHttpActionResult> PostmSiswa(mSiswa msiswa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mSiswas.Add(msiswa);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = msiswa.noSiswa }, msiswa);
        }

        // DELETE: api/mSiswas/5
        [ResponseType(typeof(mSiswa))]
        public async Task<IHttpActionResult> DeletemSiswa(int id)
        {
            mSiswa msiswa = await db.mSiswas.FindAsync(id);
            if (msiswa == null)
            {
                return NotFound();
            }

            db.mSiswas.Remove(msiswa);
            await db.SaveChangesAsync();

            return Ok(msiswa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mSiswaExists(int id)
        {
            return db.mSiswas.Count(e => e.noSiswa == id) > 0;
        }
    }
}
