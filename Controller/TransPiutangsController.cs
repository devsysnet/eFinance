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
    public class TransPiutangsController : ApiController
    {
        private eFinanceContext db = new eFinanceContext();

        // GET: api/TransPiutangs
        public JsonResult<List<TransPiutang>> GetTransPiutangs()
        {
            var piutangs = db.TransPiutangs.ToList();
            return Json(piutangs);
        }

        // GET: api/TransPiutangs/5
        [ResponseType(typeof(TransPiutang))]
        public async Task<IHttpActionResult> GetTransPiutang(int id)
        {
            //data lebih dari satu menggunakan list
            List<TransPiutang> transPiutang = await db.TransPiutangs
                .Where(piutang => piutang.nocabang == id) 
                .ToListAsync(); //select * from TransPiutangs where nocabang = id

            //ini kalau misalnya ingin satu data saja
            //TransPiutang transPiutang = await db.TransPiutangs
            //    .Where(piutang => piutang.nocabang == id)
            //    .FirstAsync(); //select top 1 * from TransPiutangs where nocabang = id

            if (transPiutang == null)
            {
                return NotFound();
            }

            return Json(transPiutang);
        }

        // PUT: api/TransPiutangs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTransPiutang(int id, TransPiutang transPiutang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transPiutang.noPiutang)
            {
                return BadRequest();
            }

            db.Entry(transPiutang).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransPiutangExists(id))
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

        // POST: api/TransPiutangs
        [ResponseType(typeof(TransPiutang))]
        public async Task<IHttpActionResult> PostTransPiutang(TransPiutang transPiutang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TransPiutangs.Add(transPiutang);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = transPiutang.noPiutang }, transPiutang);
        }

        // DELETE: api/TransPiutangs/5
        [ResponseType(typeof(TransPiutang))]
        public async Task<IHttpActionResult> DeleteTransPiutang(int id)
        {
            TransPiutang transPiutang = await db.TransPiutangs.FindAsync(id);
            if (transPiutang == null)
            {
                return NotFound();
            }

            db.TransPiutangs.Remove(transPiutang);
            await db.SaveChangesAsync();

            return Ok(transPiutang);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransPiutangExists(int id)
        {
            return db.TransPiutangs.Count(e => e.noPiutang == id) > 0;
        }
    }
}