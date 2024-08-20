using eFinance.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace eFinance.Controller
{
    public class TransCutiController : ApiController
    {
        private eFinanceContext db = new eFinanceContext();










        // GET: api/TransPiutangSiswa
        public HttpResponseMessage GetTransCuti(int id)
        {
            try
            {
                var id_karyawan = new SqlParameter("@nokaryawan", id);
                var piutangList = db.Database.SqlQuery<SPTransCuti_Result>("exec SPTranscutiKaryawan @nokaryawan=" + id_karyawan.Value + "").ToList<SPTransCuti_Result>();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JArray.FromObject(piutangList).ToString(), Encoding.UTF8, "application/json")
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
