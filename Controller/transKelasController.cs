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
    public class transKelasController : ApiController
    {
        private eFinanceContext db = new eFinanceContext();

        //GET: api/transkelas/nosiswa/tahunajaran
        public HttpResponseMessage Get(int id, string id2)
        {
            try
            {
                var id_siswa = new SqlParameter("@nosiswa", id);
                var tahunajaran = new SqlParameter("@tahunajaran", id2);
                var transKelasList = db.Database.SqlQuery<transKelas>("exec APITransKelas @nosiswa=" + id_siswa.Value + ", @tahunajaran='" + tahunajaran.Value + "'").ToList<transKelas>();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JArray.FromObject(transKelasList).ToString(), Encoding.UTF8, "application/json")
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/transKelase
        [ResponseType(typeof(transKelas))]
        public async Task<IHttpActionResult> PostTransKelas(transKelas transkls)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.transKelase.Add(transkls);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = transkls.notKelas }, transkls);
        }

        // DELETE: api/transKelas/5
        [ResponseType(typeof(transKelas))]
        public async Task<IHttpActionResult> DeleteTransKelas(int id)
        {
            transKelas transkls = await db.transKelase.FindAsync(id);
            if (transkls == null)
            {
                return NotFound();
            }

            db.transKelase.Remove(transkls);
            await db.SaveChangesAsync();

            return Ok(transkls);
        }
        
    }
}
