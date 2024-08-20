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

namespace eFinance.Controller
{
    public class mCabangsController : ApiController
    {
        private eFinanceContext db = new eFinanceContext();

        // GET: api/mCabangs
        public JsonResult<List<mCabang>> GetCabangs()
        {
            var cabangs = db.mCabangs.ToList();
            return Json(cabangs);
        }

        // GET: api/mCabangs/1
        [ResponseType(typeof(mCabang))]
        public async Task<IHttpActionResult> GetCabang(int id)
        {
            //data lebih dari satu menggunakan list
            List<mCabang> mCabang = await db.mCabangs
                .Where(cabang => cabang.noCabang == id)
                .ToListAsync(); //select * from mCabang where nocabang = id

            //ini kalau misalnya ingin satu data saja
            //mCabang mCabang = await db.mCabangs
            //    .Where(cabang => cabang.nocabang == id)
            //    .FirstAsync(); //select top 1 * from mcabang where nocabang = id

            if (mCabang == null)
            {
                return NotFound();
            }

            return Json(mCabang);
        }

        // PUT: api/mCabangs/1
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMCabang(int id, mCabang mcabang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mcabang.noCabang)
            {
                return BadRequest();
            }

            db.Entry(mcabang).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mCabangExists(id))
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

        // POST: api/mCabangs
        [ResponseType(typeof(mCabang))]
        public async Task<IHttpActionResult> PostmCabang(mCabang mcabang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mCabangs.Add(mcabang);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = mcabang.noCabang }, mcabang);
        }

        // DELETE: api/mCabangs/5
        [ResponseType(typeof(mCabang))]
        public async Task<IHttpActionResult> DeletemCabang(int id)
        {
            mCabang mcabang = await db.mCabangs.FindAsync(id);
            if (mcabang == null)
            {
                return NotFound();
            }

            db.mCabangs.Remove(mcabang);
            await db.SaveChangesAsync();

            return Ok(mcabang);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mCabangExists(int id)
        {
            return db.mCabangs.Count(e => e.noCabang == id) > 0;
        }
    }
}
