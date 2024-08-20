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
    public class mKategoriController : ApiController
    {
        private eFinanceContext db = new eFinanceContext();
        public HttpResponseMessage GetSiswa(int id)
        {
            try
            {
                var id_kategori = new SqlParameter("@noKategori", id);
                var kategoriList = db.Database.SqlQuery<mKategori>("exec APIKategori @noKategori=" + id_kategori.Value + "").ToList<mKategori>();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JArray.FromObject(kategoriList).ToString(), Encoding.UTF8, "application/json")
                };

            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                { Content = new StringContent(e.Message, Encoding.UTF8, "application/json") };
                //return null;
            }
        }
    }
}
