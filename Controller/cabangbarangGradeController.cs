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
    public class cabangbarangGradeController : ApiController
    {
        private eFinanceContext db = new eFinanceContext();

        //GET: api/cabangbarangGrade/nocabang/grade
        public HttpResponseMessage Get(int id, string id2)
        {
            try
            {
                var id_cabang = new SqlParameter("@nocabang", id);
                var id_grade = new SqlParameter("@grade", id2);
                var cabangBarangGradeList = db.Database.SqlQuery<mBarang>("exec APICabangBarangGrade @nocabang=" + id_cabang.Value + ", @grade='" + id_grade.Value + "'").ToList<mBarang>();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JArray.FromObject(cabangBarangGradeList).ToString(), Encoding.UTF8, "application/json")
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
