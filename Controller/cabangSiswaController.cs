using eFinance.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace eFinance.Controller
{
    public class cabangSiswaController : ApiController
    {
        private eFinanceContext db = new eFinanceContext();

        //GET: api/cabangsiswa/nocabang/nosiswa
        public HttpResponseMessage Get(int id, int id2)
        {
            try
            {
                var id_cabang = new SqlParameter("@nocabang", id);
                var id_siswa = new SqlParameter("@nosiswa", id2);
                var cabangSiswaList = db.Database.SqlQuery<mSiswa>("exec APICabangSiswa @nocabang=" + id_cabang.Value + ", @nosiswa=" + id_siswa.Value + "").ToList<mSiswa>();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JArray.FromObject(cabangSiswaList).ToString(), Encoding.UTF8, "application/json")
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
