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
    public class TransPiutangSiswaController : ApiController
    {
        private eFinanceContext db = new eFinanceContext();

        // GET: api/TransPiutangSiswa
        public HttpResponseMessage GetTransPiutangSiswa(int id)
        {
            try
            {
                var id_siswa = new SqlParameter("@nosiswa", id);
                var piutangList = db.Database.SqlQuery<SPTransPiutangSiswa_Result>("exec SPTransPiutangSiswa @nosiswa=" + id_siswa.Value + "").ToList<SPTransPiutangSiswa_Result>();
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

        // GET: api/TransPiutangSiswa/tahunajaran/nosiswa
        //public HttpResponseMessage Get(string tahunajaran, int? nosiswa)
        //{
        //    try
        //    {
        //        var tahun_ajaran = new SqlParameter("@tahunajaran", tahunajaran);
        //        var id_siswa = new SqlParameter("@nosiswa", nosiswa);
        //        var piutangList = db.Database.SqlQuery<SPTransPiutangSiswa_Result>("exec SPTransPiutangSiswa @tahunajaran='" + tahun_ajaran.Value + "', @nosiswa=" + id_siswa.Value + "").ToList<SPTransPiutangSiswa_Result>();
        //        return new HttpResponseMessage(HttpStatusCode.OK)
        //        {
        //            Content = new StringContent(JArray.FromObject(piutangList).ToString(), Encoding.UTF8, "application/json")
        //        };
        //    }
        //    catch (Exception)
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    }
        //}
    }

    
}
