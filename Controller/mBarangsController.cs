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
    public class mBarangsController : ApiController
    {
        private eFinanceContext db = new eFinanceContext();

        // GET: api/mBarangs
        //public JsonResult<List<mBarang>> GetBarangs()
        //{
        //    var barangs = db.mBarangs.Where(kategori => kategori.kategoriBarang == 5).ToList();
        //    return Json(barangs);
        //}

        //// GET: api/mBarangs/1
        //[ResponseType(typeof(mBarang))]
        //public async Task<IHttpActionResult> GetBarang(int id)
        //{
        //    //data lebih dari satu menggunakan list
        //    List<mBarang> mbarang = await db.mBarangs
        //        .Where(barang => barang.noBarang == id)
        //        .Where(barang => barang.kategoriBarang == 5)
        //        .ToListAsync(); //select * from mbarang where noBarang = id

        //    //ini kalau misalnya ingin satu data saja
        //    //mBarang mbarang = await db.mBarangs
        //    //    .Where(barang => barang.noBarang == id)
        //    //    .FirstAsync(); //select top 1 * from mbarang where noBarang = id

        //    if (mbarang == null)
        //    {
        //        return NotFound();
        //    }

        //    return Json(mbarang);
        //}

        public HttpResponseMessage GetBarang(int id)
        {
            try
            {
                var id_barang = new SqlParameter("@noBarang", id);
                var barangList = db.Database.SqlQuery<mBarang>("exec APIBarang @nobarang=" + id_barang.Value + "").ToList<mBarang>();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JArray.FromObject(barangList).ToString(), Encoding.UTF8, "application/json")
                };

            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                { Content = new StringContent(e.Message, Encoding.UTF8, "application/json") };
                //return null;
            }
        }

        // PUT: api/mCabangs/1
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMBarang(int id, mBarang mbarang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mbarang.noBarang)
            {
                return BadRequest();
            }

            db.Entry(mbarang).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mBarangExists(id))
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

        // POST: api/mBarangs
        [ResponseType(typeof(mBarang))]
        public async Task<IHttpActionResult> PostmBarang(mBarang mbarang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mBarangs.Add(mbarang);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = mbarang.noBarang }, mbarang);
        }

        // DELETE: api/mBarangs/5
        [ResponseType(typeof(mCabang))]
        public async Task<IHttpActionResult> DeletemBarang(int id)
        {
            mBarang mbarang = await db.mBarangs.FindAsync(id);
            if (mbarang == null)
            {
                return NotFound();
            }

            db.mBarangs.Remove(mbarang);
            await db.SaveChangesAsync();

            return Ok(mbarang);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mBarangExists(int id)
        {
            return db.mBarangs.Count(e => e.noBarang == id) > 0;
        }
    }
}
