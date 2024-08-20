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
    public class cabangBarangController : ApiController
    {
        private eFinanceContext db = new eFinanceContext();

        //GET: api/cabangbarang/nocabang/nobarang
        public HttpResponseMessage Get(int id, int id2)
        {
            try
            {
                var id_cabang = new SqlParameter("@nocabang", id);
                var id_barang = new SqlParameter("@nobarang", id2);
                var cabangBarangList = db.Database.SqlQuery<mBarang>("exec APICabangBarang @nocabang=" + id_cabang.Value + ", @nobarang=" + id_barang.Value + "").ToList<mBarang>();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JArray.FromObject(cabangBarangList).ToString(), Encoding.UTF8, "application/json")
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
