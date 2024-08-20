using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Text.RegularExpressions;

namespace eFinance
{
    public class Systems
    {
        private Database ObjDb = new Database();

        public string CreateMessage(string message)
        {
            return "<div>" + message + "</div>";
        }

        public string GetMessage(string _class = "", string message = "")
        {
            string html = "";
            switch (_class)
            {
                case "success":
                    //html += "<div class='alert alert-success'><button data-dismiss='alert' class='close'>&times;</button> " + message + " </div>";
                    html += "<div class='alert alert-page pa_page_alerts_default alert-success'><button type='button' data-dismiss='alert' class='close'>&times;</button> " + message + " </div>";
                    break;
                case "info":
                    //html += "<div class='alert alert-info'><button data-dismiss='alert' class='close'>&times;</button> " + message + " </div>";
                    html += "<div class='alert alert-page pa_page_alerts_default alert-info'><button type='button' data-dismiss='alert' class='close'>&times;</button> " + message + " </div>";
                    break;
                case "warning":
                    //html += "<div class='alert'><button data-dismiss='alert' class='close'>&times;</button> " + message + " </div>";
                    html += "<div class='alert alert-page pa_page_alerts_default'><button type='button' data-dismiss='alert' class='close'>&times;</button> " + message + " </div>";
                    break;
                case "error":
                    //html += "<div class='alert alert-danger'><button data-dismiss='alert' class='close'>&times;</button> " + message + " </div>";
                    html += "<div class='alert alert-page pa_page_alerts_default alert-danger'><button type='button' data-dismiss='alert' class='close'>&times;</button><div><a href='javascript:void()' class='message-top'>Mohon untuk cek list error dibawah ini !</a></div> " + message + " </div>";
                    break;
                case "default":
                    //html += "<div> " + message + " </div>";
                    html += "<div class='alert alert-page pa_page_alerts_default'><button type='button' data-dismiss='alert' class='close'>&times;</button> " + message + " </div>";
                    break;
            }
            return html;
        }

        public string GetMessageStandard(string _class = "", string message = "")
        {

            string html = "";
            switch (_class)
            {
                case "success":
                    html += "<div class='alert alert-success'><button data-dismiss='alert' class='close'>&times;</button> " + message + " </div>";
                    break;
                case "info":
                    html += "<div class='alert alert-info'><button data-dismiss='alert' class='close'>&times;</button> " + message + " </div>";
                    break;
                case "warning":
                    html += "<div class='alert'><button data-dismiss='alert' class='close'>&times;</button> " + message + " </div>";
                    break;
                case "danger":
                    html += "<div class='alert alert-danger'><button data-dismiss='alert' class='close'>&times;</button> " + message + " </div>";
                    break;
                case "default":
                    html += "<div> " + message + " </div>";
                    break;
            }
            return html;
        }

        public string GetNow
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            }
        }

        public string GetDate
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        public string GetDateDMY
        {
            get
            {
                return DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }

        public string ToDate(string date)
        {
            string dateReturn = null;
            if (date != "")
            {
                dateReturn = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
            }

            return dateReturn;
        }

        public string ToSqlDate(string date)
        {
            string dateReturn = null;
            if (date != "")
            {
                dateReturn = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
            }

            return dateReturn;
        }

        public string RandomNumber
        {
            get
            {
                return DateTime.Now.ToString("yyyyMMddhhmmss");
            }
        }

        public bool IsValidEmail(string strIn)
        {
            string pattern = null;
            pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(strIn, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetNewDate(string date, int param)
        {
            DateTime newDate = Convert.ToDateTime(date).AddDays(param);
            date = newDate.ToString("yyyy-MM-dd");
            return date;
        }

        public bool IsNumeric(string strIn)
        {
            string pattern = null;
            pattern = "^[0-9]*$";
            if (Regex.IsMatch(strIn, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string IsFormatNumber(string value = "")
        {
            //return value = String.Format("{0:#,##0}", Convert.ToDecimal(value));
            return value = string.Format("{0:#,0.00}", Convert.ToDecimal(value));
        }

        public string IsFormatQuantityNumber(string value = "")
        {
            return value = string.Format("{0:0,0.00}", Convert.ToDecimal(value));
        }

        #region security & menu
        public string Sha1(string text)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(text);

            SHA1Managed hashString = new SHA1Managed();
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        public string IsInjection(string sData)
        {
            string ret = "";

            if (sData != "" && sData != null)
            {
                ret = sData.ToString().Replace("'", "''");
            }

            return ret.Trim();
        }

        public void SessionCheck(string namaFile)
        {
            var page = (Page)HttpContext.Current.Handler;
            DataSet query = ObjDb.GetRows("SELECT * FROM Menu WHERE namaFile like '%" + namaFile + "%'");
             if (query.Tables[0].Rows.Count==0)
                HttpContext.Current.Response.Redirect(Func.BaseUrl + "Default.aspx");
            DataRow myRow = query.Tables[0].Rows[0];

            string siteMap = myRow["siteMapMenu"].ToString();
            string title = myRow["judulMenu"].ToString();
            string isPublic = myRow["tingkatMenu"].ToString();
            string noMenu = myRow["noMenu"].ToString();

            /*
                isPublic 0 : bisa diakses tanpa login
                isPublic 1 : bisa diakses dengan ketentuan user sudah login
                isPublic 2 : bisa diakses dengan ketentuan user sudah login dan mempunyai akses terhadap menu tersebut (dari security menu)
            */
            if (isPublic == "1" || isPublic == "2" || isPublic == "3" || isPublic == "4")
            {
                if (this.GetUserId == "")
                {
                    //HttpContext.Current.Response.Redirect(Func.BaseUrl + "Warning.aspx?act=login");
                    HttpContext.Current.Response.Redirect(Func.BaseUrl + "Default.aspx");
                }
                else
                {

                    if (ObjDb.GetRowsDataTable("select * from thakmenu where noAkses = '" + this.GetGroup + "' and noMenu = '" + noMenu + "'").Rows.Count == 0)
                    {
                        //HttpContext.Current.Response.Redirect(Func.BaseUrl + "warning.aspx?act=access");
                        HttpContext.Current.Response.Redirect(Func.BaseUrl + "Default.aspx");
                    }
                }
            }
        }
        #endregion

        #region GetUserData
        public string GetUserName
        {
            get
            {
                string username = "";
                if (HttpContext.Current.Session["USERNAME"] != null)
                {
                    username = HttpContext.Current.Session["USERNAME"].ToString();
                }
                return username;
            }
        }
        public string GetFullName
        {
            get
            {
                string nameUser = "";
                if (HttpContext.Current.Session["NAMAUSER"] != null)
                {
                    nameUser = HttpContext.Current.Session["NAMAUSER"].ToString();
                }
                return nameUser;
            }
        }
        public string GetUserImage
        {
            get
            {
                string images = "";
                if (HttpContext.Current.Session["IMAGES"] != null && HttpContext.Current.Session["IMAGES"] != "")
                {
                    images = HttpContext.Current.Session["IMAGES"].ToString();
                }
                else
                {
                    images = "empty.jpg";
                }
                return images;
            }
        }
        public string GetUserId
        {
            get
            {
                string userId = "";
                if (HttpContext.Current.Session["USERID"] != null)
                {
                    userId = HttpContext.Current.Session["USERID"].ToString();
                }
                return userId;
            }
        }

        public string GetCabangId
        {
            get
            {
                string cabangId = "";
                if (HttpContext.Current.Session["CABANGID"] != null)
                {
                    cabangId = HttpContext.Current.Session["CABANGID"].ToString();
                }
                return cabangId;
            }
        }

        public string GetstsCabang
        {
            get
            {
                string stscabang = "";
                if (HttpContext.Current.Session["STSCABANG"] != null)
                {
                    stscabang = HttpContext.Current.Session["STSCABANG"].ToString();
                }
                return stscabang;
            }
        }

        public string GetstsPusat
        {
            get
            {
                string stspusat = "";
                if (HttpContext.Current.Session["CABANG"] != null)
                {
                    stspusat = HttpContext.Current.Session["CABANG"].ToString();
                }
                return stspusat;
            }
        }

        public string GetParentCabang
        {
            get
            {
                string noparent = "";
                if (HttpContext.Current.Session["PARENT"] != null)
                {
                    noparent = HttpContext.Current.Session["PARENT"].ToString();
                }
                return noparent;
            }
        }

        public string GetCabangCode
        {
            get
            {
                string cabangId = "", cabangCode = "";
                if (HttpContext.Current.Session["CABANGID"] != null)
                {
                    cabangId = HttpContext.Current.Session["CABANGID"].ToString();
                    DataSet mySet = ObjDb.GetRows("SELECT * FROM mCabang WHERE noCabang = '" + cabangId + "'");
                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        DataRow myRow = mySet.Tables[0].Rows[0];
                        cabangCode = myRow["kdCabang"].ToString();
                    }
                }
                return cabangCode;
            }
        }

      

        public string GetCabangName
        {
            get
            {
                string cabangId = "", cabangName = "";
                if (HttpContext.Current.Session["CABANGID"] != null)
                {
                    cabangId = HttpContext.Current.Session["CABANGID"].ToString();
                    DataSet mySet = ObjDb.GetRows("SELECT * FROM mCabang WHERE noCabang = '" + cabangId + "'");
                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        DataRow myRow = mySet.Tables[0].Rows[0];
                        cabangName = myRow["namaCabang"].ToString();
                    }
                }
                return cabangName;
            }
        }
        public string GetJabatan
        {
            get
            {
                string jabatan = "";
                if (HttpContext.Current.Session["JABATAN"] != null)
                {
                    jabatan = HttpContext.Current.Session["JABATAN"].ToString();
                }
                return jabatan;
            }
        }

        public string GetKategori_Usaha
        {
            get
            {
                string kat_Usaha = "";
                if (HttpContext.Current.Session["Kategori_Usaha"] != null)
                {
                    kat_Usaha = HttpContext.Current.Session["Kategori_Usaha"].ToString();
                }
                return kat_Usaha;
            }
        }

        public string GetGroup
        {
            get
            {
                string sGroupId = "";
                DataTable Result = ObjDb.GetRowsDataTable("select * from tAkses where noUser = '" + this.GetUserId + "'");
                if (Result.Rows.Count > 0)
                {
                    sGroupId = Result.Rows[0]["noAkses"].ToString();
                }

                return sGroupId;
            }
        }

        public string Getjnsbank
        {
            get
            {
                string sGroupjnsbank = "";
                DataTable Result = ObjDb.GetRowsDataTable("select * from parameter");
                if (Result.Rows.Count > 0)
                {
                    sGroupjnsbank = Result.Rows[0]["jnsbank"].ToString();
                }

                return sGroupjnsbank;
            }
        }

        public string Getjnstkas
        {
            get
            {
                string sGroupIdtfkas = "";
                DataTable Result = ObjDb.GetRowsDataTable("select * from parameter");
                if (Result.Rows.Count > 0)
                {
                    sGroupIdtfkas = Result.Rows[0]["jnstfkas"].ToString();
                }

                return sGroupIdtfkas;
            }
        }

        #endregion

        #region get generate code
        public string GetCodeAutoNumber(string kodeSN, string date)
        {
            DateTime dateNumber = Convert.ToDateTime(date);
            string autoNumber = "", noSN = "", typeNumber = "", sql = "", kodeCabang = this.GetCabangCode, stringCode = "";
            int startNumber = 0;
            DataSet dataSN = ObjDb.GetRows("SELECT * FROM setNumber WHERE noSN = '" + kodeSN + "'");
            if (dataSN.Tables[0].Rows.Count > 0)
            {
                DataRow myRowSn = dataSN.Tables[0].Rows[0];
                kodeSN = myRowSn["kodeSN"].ToString();
                typeNumber = myRowSn["tipeNomor"].ToString();
                noSN = myRowSn["noSN"].ToString();
            }
            if (typeNumber == "T")
            {
                sql = "SELECT a.* FROM (SELECT Nomor.*, YEAR(Tahun) DateNumber FROM Nomor where noCabang = '" + GetCabangId + "') a WHERE a.noSN = '" + noSN + "' AND a.DateNumber = '" + dateNumber.ToString("yyyy") + "'";
                stringCode = kodeSN + "-" + dateNumber.ToString("yyyy") + "-";
            }
            else if (typeNumber == "B")
            {
                sql = "SELECT a.* FROM (SELECT Nomor.*, CONVERT(char(4), YEAR(Tahun)) + '-' + CONVERT(char(2), Tahun, 101) DateNumber FROM Nomor where noCabang = '" + GetCabangId + "') a WHERE a.noSN = '" + noSN + "' AND a.DateNumber = '" + dateNumber.ToString("yyyy-MM") + "'";
                stringCode = kodeSN + "-";
            }
            DataSet dataNumber = ObjDb.GetRows(sql);
            if (dataNumber.Tables[0].Rows.Count > 0)
                startNumber = Convert.ToInt32(dataNumber.Tables[0].Rows[0]["Nomor"].ToString());
            else
                startNumber = 1;

            if (startNumber < 10)
                autoNumber = stringCode + "0000" + startNumber;
            else if (startNumber < 100)
                autoNumber = stringCode + "000" + startNumber;
            else if (startNumber < 1000)
                autoNumber = stringCode + "00" + startNumber;
            else if (startNumber < 10000)
                autoNumber = stringCode + "0" + startNumber;
            else
                autoNumber = stringCode + Convert.ToString(startNumber);
            return autoNumber;
        }
        public string GetCodeAutoNumberGroup(string kodeSN, string date)
        {
            DateTime dateNumber = Convert.ToDateTime(date);
            string autoNumber = "", noSN = "", typeNumber = "", sql = "", kodeCabang = this.GetCabangCode, stringCode = "";
            int startNumber = 0;
            DataSet dataSN = ObjDb.GetRows("SELECT * FROM setNumber WHERE noSN = '" + kodeSN + "'");
            if (dataSN.Tables[0].Rows.Count > 0)
            {
                DataRow myRowSn = dataSN.Tables[0].Rows[0];
                kodeSN = myRowSn["kodeSN"].ToString();
                typeNumber = myRowSn["tipeNomor"].ToString();
                noSN = myRowSn["noSN"].ToString();
            }
            if (typeNumber == "T")
            {
                sql = "SELECT a.* FROM (SELECT Nomor.*, YEAR(Tahun) DateNumber FROM Nomor where noCabang = '" + GetCabangId + "') a WHERE a.noSN = '" + noSN + "' AND a.DateNumber = '" + dateNumber.ToString("yyyy") + "'";
                stringCode = kodeSN + "" + kodeCabang + "" + dateNumber.ToString("yyyy") + "";
            }
            else if (typeNumber == "B")
            {
                sql = "SELECT a.* FROM (SELECT Nomor.*, CONVERT(char(4), YEAR(Tahun)) + '-' + CONVERT(char(2), Tahun, 101) DateNumber FROM Nomor where noCabang = '" + GetCabangId + "') a WHERE a.noSN = '" + noSN + "' AND a.DateNumber = '" + dateNumber.ToString("yyyy-MM") + "'";
                stringCode = kodeSN + "" + kodeCabang + "" + dateNumber.ToString("yyyy") + "" + dateNumber.ToString("MM") + "";
            }
            DataSet dataNumber = ObjDb.GetRows(sql);
            if (dataNumber.Tables[0].Rows.Count > 0)
                startNumber = Convert.ToInt32(dataNumber.Tables[0].Rows[0]["Nomor"].ToString());
            else
                startNumber = 1;

            if (startNumber < 10)
                autoNumber = stringCode + "0000" + startNumber;
            else if (startNumber < 100)
                autoNumber = stringCode + "000" + startNumber;
            else if (startNumber < 1000)
                autoNumber = stringCode + "00" + startNumber;
            else if (startNumber < 10000)
                autoNumber = stringCode + "0" + startNumber;
            else
                autoNumber = stringCode + Convert.ToString(startNumber);
            return autoNumber;
        }

        public void UpdateAutoNumberCode(string kodeSN, string date)
        {
            string noSN = "", typeNumber = "";
            int newNumber = 0;
            DateTime dateNumber = Convert.ToDateTime(date);
            DataSet dataSN = ObjDb.GetRows("SELECT * FROM setNumber WHERE noSN = '" + kodeSN + "'");
            if (dataSN.Tables[0].Rows.Count > 0)
            {
                noSN = dataSN.Tables[0].Rows[0]["noSN"].ToString();
                typeNumber = dataSN.Tables[0].Rows[0]["tipeNomor"].ToString();
            }
            if (typeNumber == "T")
            {
                DataSet dataNomor = ObjDb.GetRows("SELECT * FROM (SELECT Nomor.*, YEAR(Tahun) DateNumber FROM Nomor) a WHERE a.noSN = '" + noSN + "' and a.noCabang = '" + this.GetCabangId + "' and a.DateNumber = '" + dateNumber.ToString("yyyy") + "'");
                if (dataNomor.Tables[0].Rows.Count > 0)
                {
                    newNumber = Convert.ToInt32(dataNomor.Tables[0].Rows[0]["Nomor"].ToString()) + 1;
                    ObjDb.ExecQuery("UPDATE Nomor SET Nomor = '" + newNumber + "' WHERE noSN = '" + noSN + "' and noCabang = '" + this.GetCabangId + "' and YEAR(Tahun) = '" + dateNumber.ToString("yyyy") + "'");
                }
                else
                {

                    ObjDb.ExecQuery("INSERT INTO Nomor (Nomor, noSN, Tahun, noCabang) VALUES('2', '" + noSN + "', '" + dateNumber.ToString("yyyy-MM-dd") + "', '" + this.GetCabangId + "')");
                }
            }
            else if (typeNumber == "B")
            {
                DataSet dataNomor = ObjDb.GetRows("SELECT * FROM (SELECT Nomor.*, CONVERT(char(4), YEAR(Tahun)) + '-' + CONVERT(char(2), Tahun, 101) DateNumber FROM Nomor) a WHERE a.noSN = '" + noSN + "' and a.noCabang = '" + this.GetCabangId + "' and a.DateNumber = '" + dateNumber.ToString("yyyy-MM") + "'");
                if (dataNomor.Tables[0].Rows.Count > 0)
                {
                    newNumber = Convert.ToInt32(dataNomor.Tables[0].Rows[0]["Nomor"].ToString()) + 1;
                    ObjDb.ExecQuery("UPDATE Nomor SET Nomor = '" + newNumber + "' WHERE noSN = '" + noSN + "' and noCabang = '" + this.GetCabangId + "' and CONVERT(char(4), YEAR(Tahun)) + '-' + CONVERT(char(2), Tahun, 101) = '" + dateNumber.ToString("yyyy-MM") + "'");
                }
                else
                {

                    ObjDb.ExecQuery("INSERT INTO Nomor (Nomor, noSN, Tahun, noCabang) VALUES('2', '" + noSN + "', '" + dateNumber.ToString("yyyy-MM-dd") + "', '" + this.GetCabangId + "')");
                }
            }

        }

        //AUTO NUMBER MASTER
        public string GetCodeAutoNumberMaster(string kodeSN, string date)
        {
            DateTime dateNumber = Convert.ToDateTime(date);
            string autoNumber = "", noSN = "", typeNumber = "", sql = "", kodeCabang = this.GetCabangCode, stringCode = "";
            int startNumber = 0;
            DataSet dataSN = ObjDb.GetRows("SELECT * FROM setNumber WHERE noSN = '" + kodeSN + "'");
            if (dataSN.Tables[0].Rows.Count > 0)
            {
                DataRow myRowSn = dataSN.Tables[0].Rows[0];
                kodeSN = myRowSn["kodeSN"].ToString();
                typeNumber = myRowSn["tipeNomor"].ToString();
                noSN = myRowSn["noSN"].ToString();
            }
            if (typeNumber == "T")
            {
                sql = "SELECT a.* FROM (SELECT Nomor.*, YEAR(Tahun) DateNumber FROM Nomor) a WHERE a.noSN = '" + noSN + "' AND a.DateNumber = '" + dateNumber.ToString("yyyy") + "'";
                stringCode = kodeSN + dateNumber.ToString("yyyy");
            }
            else if (typeNumber == "B")
            {
                sql = "SELECT a.* FROM (SELECT Nomor.*, CONVERT(char(4), YEAR(Tahun)) + '-' + CONVERT(char(2), Tahun, 101) DateNumber FROM Nomor) a WHERE a.noSN = '" + noSN + "' AND a.DateNumber = '" + dateNumber.ToString("yyyy-MM") + "'";
                stringCode = kodeSN;
            }
            DataSet dataNumber = ObjDb.GetRows(sql);
            if (dataNumber.Tables[0].Rows.Count > 0)
                startNumber = Convert.ToInt32(dataNumber.Tables[0].Rows[0]["Nomor"].ToString());
            else
                startNumber = 1;

            if (startNumber < 10)
                autoNumber = stringCode + "0000000" + startNumber;
            else if (startNumber < 100)
                autoNumber = stringCode + "000000" + startNumber;
            else if (startNumber < 1000)
                autoNumber = stringCode + "00000" + startNumber;
            else if (startNumber < 10000)
                autoNumber = stringCode + "0000" + startNumber;
            else if (startNumber < 100000)
                autoNumber = stringCode + "000" + startNumber;
            else if (startNumber < 1000000)
                autoNumber = stringCode + "00" + startNumber;
            else if (startNumber < 10000000)
                autoNumber = stringCode + "0" + startNumber;
            else
                autoNumber = stringCode + Convert.ToString(startNumber);
            return autoNumber;
        }

        public string GetCodeAutoNumberMasterGudang(string kodeSN, string date)
        {
            DateTime dateNumber = Convert.ToDateTime(date);
            string autoNumber = "", noSN = "", typeNumber = "", sql = "", kodeCabang = this.GetCabangCode, stringCode = "";
            int startNumber = 0;
            DataSet dataSN = ObjDb.GetRows("SELECT * FROM setNumber WHERE noSN = '" + kodeSN + "'");
            if (dataSN.Tables[0].Rows.Count > 0)
            {
                DataRow myRowSn = dataSN.Tables[0].Rows[0];
                kodeSN = myRowSn["kodeSN"].ToString();
                typeNumber = myRowSn["tipeNomor"].ToString();
                noSN = myRowSn["noSN"].ToString();
            }
            if (typeNumber == "T")
            {
                sql = "SELECT a.* FROM (SELECT Nomor.*, YEAR(Tahun) DateNumber FROM Nomor) a WHERE a.noSN = '" + noSN + "' AND a.DateNumber = '" + dateNumber.ToString("yyyy") + "'";
                stringCode = kodeSN + dateNumber.ToString("yyyy");
            }
            else if (typeNumber == "B")
            {
                sql = "SELECT a.* FROM (SELECT Nomor.*, CONVERT(char(4), YEAR(Tahun)) + '-' + CONVERT(char(2), Tahun, 101) DateNumber FROM Nomor) a WHERE a.noSN = '" + noSN + "' AND a.DateNumber = '" + dateNumber.ToString("yyyy-MM") + "'";
                stringCode = kodeSN + "-";
            }
            DataSet dataNumber = ObjDb.GetRows(sql);
            if (dataNumber.Tables[0].Rows.Count > 0)
                startNumber = Convert.ToInt32(dataNumber.Tables[0].Rows[0]["Nomor"].ToString());
            else
                startNumber = 1;

            //if (startNumber < 10)
            //    autoNumber = stringCode + "0000000" + startNumber;
            //else if (startNumber < 100)
            //    autoNumber = stringCode + "000000" + startNumber;
            //else if (startNumber < 1000)
            //    autoNumber = stringCode + "00000" + startNumber;
            //else if (startNumber < 10000)
            //    autoNumber = stringCode + "0000" + startNumber;
            //else if (startNumber < 100000)
            //    autoNumber = stringCode + "000" + startNumber;
            //else if (startNumber < 1000000)
            //    autoNumber = stringCode + "00" + startNumber;
            //else if (startNumber < 10000000)
            //    autoNumber = stringCode + "0" + startNumber;
            //else
            autoNumber = stringCode + Convert.ToString(startNumber);
            return autoNumber;
        }

        public string GetCodeAutoNumberMasterLokasiGudang(string kodeSN, string KodeGudang,string noGudang)
        {
            string autoNumber = "", noSN = "", typeNumber = "", sql = "", kodeCabang = this.GetCabangCode, stringCode = "";
            int startNumber = 0;
            DataSet dataSN = ObjDb.GetRows("SELECT * FROM setNumber WHERE noSN = '" + kodeSN + "'");
            if (dataSN.Tables[0].Rows.Count > 0)
            {
                DataRow myRowSn = dataSN.Tables[0].Rows[0];
                kodeSN = myRowSn["kodeSN"].ToString();
                typeNumber = myRowSn["tipeNomor"].ToString();
                noSN = myRowSn["noSN"].ToString();
            }
            if (typeNumber == "B")
            {
                sql = "SELECT * from LokasiGudang WHERE noGudang = '" + noGudang + "' order by createdDate desc";
                stringCode = KodeGudang + "-";
            }
            DataSet dataNumber = ObjDb.GetRows(sql);
            if (dataNumber.Tables[0].Rows.Count > 0 && dataNumber.Tables[0].Rows[0]["kdLokGud"].ToString() == stringCode + Convert.ToString(dataNumber.Tables[0].Rows.Count))
                startNumber = dataNumber.Tables[0].Rows.Count + 1;
            else
                startNumber = 1;

            autoNumber = stringCode + Convert.ToString(startNumber);
            return autoNumber;
        }

        public string GetCodeAutoNumberNewCustom(string noSN, string date, int Akun)
        {
            DateTime dateNumber = Convert.ToDateTime(date);
            string autoNumber = "", kodeSN = "",  typeNumber = "", sql = "", kodeCabang = this.GetCabangCode, stringCode = "", kodeCb = "", kodeAkun = "", jnsAkun = "";
            int startNumber = 0;
            DataSet dataSN = ObjDb.GetRows("SELECT * FROM setNumber WHERE noSN = '" + noSN + "'");
            if (dataSN.Tables[0].Rows.Count > 0)
            {
                DataRow myRowSn = dataSN.Tables[0].Rows[0];
                kodeSN = myRowSn["kodeSN"].ToString(); // 1=KM - Masuk Kas/Bank, 2=KK - Keluar Kas/Bank
                typeNumber = myRowSn["tipeNomor"].ToString();
                //noSN = myRowSn["noSN"].ToString();
            }

            DataSet dataCb = ObjDb.GetRows("SELECT * FROM mCabang WHERE noCabang = '" + GetCabangId + "'");
            if (dataCb.Tables[0].Rows.Count > 0)
            {
                DataRow myRowCb = dataCb.Tables[0].Rows[0];
                kodeCb = myRowCb["noCabang"].ToString();
            }

            DataSet dataAkun = ObjDb.GetRows("SELECT * FROM mRekening WHERE noRek = " + Akun + "");
            if (dataAkun.Tables[0].Rows.Count > 0)
            {
                DataRow myRowAkun = dataAkun.Tables[0].Rows[0];
                kodeAkun = myRowAkun["kdRek"].ToString();
                jnsAkun = myRowAkun["jenis"].ToString(); //1=Kas,2=Bank
            }

            if (typeNumber == "B")
            {
                //Penerimaan Kas  KM / Kodeakun / kodecabang / bln / thn / nomorurut
                //Pengeluaran Kas KK / Kodeakun / kodecabang / bln / thn / nomorurut
                sql = "SELECT a.* FROM (SELECT Nomor.*, YEAR(Tahun) YearDateNumber, Month(Tahun) MonthDateNumber FROM Nomor where noCabang = '" + GetCabangId + "') a WHERE a.noSN = '" + noSN + "' AND a.YearDateNumber = '" + dateNumber.ToString("yyyy") + "' AND a.MonthDateNumber = '" + dateNumber.ToString("MM") + "' and a.noRek = "+ Akun +"";
                //Jika Penerimaan Bank 
                if (jnsAkun == "2" && noSN == "1")
                    stringCode = "BM/" + kodeAkun + "/" + kodeCb + "/" + dateNumber.ToString("MM") + "/" + dateNumber.ToString("yy") + "/";
                //Jika Pengeluaran Bank 
                if (jnsAkun == "2" && noSN == "2")
                    stringCode = "BK/" + kodeAkun + "/" + kodeCb + "/" + dateNumber.ToString("MM") + "/" + dateNumber.ToString("yy") + "/";
                //Jika Penerimaan Kas 
                if (jnsAkun == "1" && noSN == "1")
                    stringCode = "KM/" + kodeAkun + "/" + kodeCb + "/" + dateNumber.ToString("MM") + "/" + dateNumber.ToString("yy") + "/";
                //Jika Pengeluaran Kas 
                if (jnsAkun == "1" && noSN == "2")
                    stringCode = "KK/" + kodeAkun + "/" + kodeCb + "/" + dateNumber.ToString("MM") + "/" + dateNumber.ToString("yy") + "/";
            }
            else
            {
                //Penerimaan Kas   KM'+ +'kodecabang'+ +'thn'+ +'bln'+ +'nomorurut
                //Pengeluaran Kas  KK'+ +'kodecabang'+ +'thn'+ +'bln'+ +'nomorurut
                sql = "SELECT a.* FROM (SELECT Nomor.*, YEAR(Tahun) DateNumber FROM Nomor where noCabang = '" + GetCabangId + "') a WHERE a.noSN = '" + noSN + "' AND a.DateNumber = '" + dateNumber.ToString("yyyy") + "' and a.noRek is null";
                stringCode = kodeSN + "" + kodeCb + "" + dateNumber.ToString("yyMM");
            }


            DataSet dataNumber = ObjDb.GetRows(sql);
            if (dataNumber.Tables[0].Rows.Count > 0)
                startNumber = Convert.ToInt32(dataNumber.Tables[0].Rows[0]["Nomor"].ToString());
            else
                startNumber = 1;

            if (startNumber < 10)
                autoNumber = stringCode + "0000" + startNumber;
            else if (startNumber < 100)
                autoNumber = stringCode + "000" + startNumber;
            else if (startNumber < 1000)
                autoNumber = stringCode + "00" + startNumber;
            else if (startNumber < 10000)
                autoNumber = stringCode + "0" + startNumber;
            else
                autoNumber = stringCode + Convert.ToString(startNumber);
            return autoNumber;
        }

        public void UpdateAutoNumberCodeNewCustom(string noSN, string date, int Akun)
        {
            string typeNumber = "";
            int newNumber = 0;
            DateTime dateNumber = Convert.ToDateTime(date);
            DataSet dataSN = ObjDb.GetRows("SELECT * FROM setNumber WHERE noSN = '" + noSN + "'");
            if (dataSN.Tables[0].Rows.Count > 0)
            {
                //noSN = dataSN.Tables[0].Rows[0]["noSN"].ToString();
                typeNumber = dataSN.Tables[0].Rows[0]["tipeNomor"].ToString();
            }

            if (typeNumber == "B")
            {
                DataSet dataNomor = ObjDb.GetRows("SELECT * FROM (SELECT Nomor.*, YEAR(Tahun) YearDateNumber, Month(Tahun) MonthDateNumber FROM Nomor) a WHERE a.noSN = '" + noSN + "' and a.noCabang = '" + this.GetCabangId + "' and a.YearDateNumber = '" + dateNumber.ToString("yyyy") + "' and a.MonthDateNumber = '" + dateNumber.ToString("MM") + "' and a.noRek = "+ Akun + "");
                if (dataNomor.Tables[0].Rows.Count > 0)
                {
                    newNumber = Convert.ToInt32(dataNomor.Tables[0].Rows[0]["Nomor"].ToString()) + 1;
                    ObjDb.ExecQuery("UPDATE Nomor SET Nomor = '" + newNumber + "' WHERE noSN = '" + noSN + "' and noCabang = '" + this.GetCabangId + "' and YEAR(Tahun) = '" + dateNumber.ToString("yyyy") + "' and Month(Tahun) = '" + dateNumber.ToString("MM") + "' and noRek = "+ Akun +"");
                }
                else
                {
                    ObjDb.ExecQuery("INSERT INTO Nomor (Nomor, noSN, Tahun, noCabang, noRek) VALUES('2', '" + noSN + "', '" + dateNumber.ToString("yyyy-MM-dd") + "', '" + this.GetCabangId + "', "+ Akun +")");
                }
            }
            else
            {
                DataSet dataNomor = ObjDb.GetRows("SELECT * FROM (SELECT Nomor.*, YEAR(Tahun) DateNumber FROM Nomor) a WHERE a.noSN = '" + noSN + "' and a.noCabang = '" + this.GetCabangId + "' and a.DateNumber = '" + dateNumber.ToString("yyyy") + "' and a.noRek is null");
                if (dataNomor.Tables[0].Rows.Count > 0)
                {
                    newNumber = Convert.ToInt32(dataNomor.Tables[0].Rows[0]["Nomor"].ToString()) + 1;
                    ObjDb.ExecQuery("UPDATE Nomor SET Nomor = '" + newNumber + "' WHERE noSN = '" + noSN + "' and noCabang = '" + this.GetCabangId + "' and YEAR(Tahun) = '" + dateNumber.ToString("yyyy") + "'  and noRek is null");
                }
                else
                {
                    ObjDb.ExecQuery("INSERT INTO Nomor (Nomor, noSN, Tahun, noCabang) VALUES('2', '" + noSN + "', '" + dateNumber.ToString("yyyy-MM-dd") + "', '" + this.GetCabangId + "')");
                }
            }

        }

        public string GetCodeAutoNumberNew(string kodeSN, string date)
        {
            DateTime dateNumber = Convert.ToDateTime(date);
            string autoNumber = "", noSN = "", typeNumber = "", sql = "", kodeCabang = this.GetCabangCode, stringCode = "", kodeCb = "";
            int startNumber = 0;
            DataSet dataSN = ObjDb.GetRows("SELECT * FROM setNumber WHERE noSN = '" + kodeSN + "'");
            if (dataSN.Tables[0].Rows.Count > 0)
            {
                DataRow myRowSn = dataSN.Tables[0].Rows[0];
                kodeSN = myRowSn["kodeSN"].ToString();
                typeNumber = myRowSn["tipeNomor"].ToString();
                noSN = myRowSn["noSN"].ToString();
            }

            DataSet dataCb = ObjDb.GetRows("SELECT * FROM mCabang WHERE noCabang = '" + GetCabangId + "'");
            if (dataCb.Tables[0].Rows.Count > 0)
            {
                DataRow myRowCb = dataCb.Tables[0].Rows[0];
                kodeCb = myRowCb["noCabang"].ToString();
            }

            sql = "SELECT a.* FROM (SELECT Nomor.*, YEAR(Tahun) DateNumber FROM Nomor where noCabang = '" + GetCabangId + "') a WHERE a.noSN = '" + noSN + "' AND a.DateNumber = '" + dateNumber.ToString("yyyy") + "'";
            stringCode = kodeSN + "" + kodeCb + "" + dateNumber.ToString("yyMM");


            DataSet dataNumber = ObjDb.GetRows(sql);
            if (dataNumber.Tables[0].Rows.Count > 0)
                startNumber = Convert.ToInt32(dataNumber.Tables[0].Rows[0]["Nomor"].ToString());
            else
                startNumber = 1;

            if (startNumber < 10)
                autoNumber = stringCode + "0000" + startNumber;
            else if (startNumber < 100)
                autoNumber = stringCode + "000" + startNumber;
            else if (startNumber < 1000)
                autoNumber = stringCode + "00" + startNumber;
            else if (startNumber < 10000)
                autoNumber = stringCode + "0" + startNumber;
            else
                autoNumber = stringCode + Convert.ToString(startNumber);
            return autoNumber;
        }

        public void UpdateAutoNumberCodeNew(string kodeSN, string date)
        {
            string noSN = "", typeNumber = "";
            int newNumber = 0;
            DateTime dateNumber = Convert.ToDateTime(date);
            DataSet dataSN = ObjDb.GetRows("SELECT * FROM setNumber WHERE noSN = '" + kodeSN + "'");
            if (dataSN.Tables[0].Rows.Count > 0)
            {
                noSN = dataSN.Tables[0].Rows[0]["noSN"].ToString();
                typeNumber = dataSN.Tables[0].Rows[0]["tipeNomor"].ToString();
            }

            DataSet dataNomor = ObjDb.GetRows("SELECT * FROM (SELECT Nomor.*, YEAR(Tahun) DateNumber FROM Nomor) a WHERE a.noSN = '" + noSN + "' and a.noCabang = '" + this.GetCabangId + "' and a.DateNumber = '" + dateNumber.ToString("yyyy") + "'");
            if (dataNomor.Tables[0].Rows.Count > 0)
            {
                newNumber = Convert.ToInt32(dataNomor.Tables[0].Rows[0]["Nomor"].ToString()) + 1;
                ObjDb.ExecQuery("UPDATE Nomor SET Nomor = '" + newNumber + "' WHERE noSN = '" + noSN + "' and noCabang = '" + this.GetCabangId + "' and YEAR(Tahun) = '" + dateNumber.ToString("yyyy") + "'");
            }
            else
            {
                ObjDb.ExecQuery("INSERT INTO Nomor (Nomor, noSN, Tahun, noCabang) VALUES('2', '" + noSN + "', '" + dateNumber.ToString("yyyy-MM-dd") + "', '" + this.GetCabangId + "')");
            }
            

        }

        public void UpdateAutoNumberMaster(string kodeSN, string date)
        {
            string noSN = "", typeNumber = "";
            int newNumber = 0;
            DateTime dateNumber = Convert.ToDateTime(date);
            DataSet dataSN = ObjDb.GetRows("SELECT * FROM setNumber WHERE noSN = '" + kodeSN + "'");
            if (dataSN.Tables[0].Rows.Count > 0)
            {
                noSN = dataSN.Tables[0].Rows[0]["noSN"].ToString();
                typeNumber = dataSN.Tables[0].Rows[0]["tipeNomor"].ToString();
            }

            DataSet dataNomor = ObjDb.GetRows("SELECT * FROM (SELECT Nomor.*, YEAR(Tahun) DateNumber FROM Nomor) a WHERE a.noSN = '" + noSN + "' and a.DateNumber = '" + dateNumber.ToString("yyyy") + "'");
            if (dataNomor.Tables[0].Rows.Count > 0)
            {
                newNumber = Convert.ToInt32(dataNomor.Tables[0].Rows[0]["Nomor"].ToString()) + 1;
                ObjDb.ExecQuery("UPDATE Nomor SET Nomor = '" + newNumber + "' WHERE noSN = '" + noSN + "' and YEAR(Tahun) = '" + dateNumber.ToString("yyyy") + "'");
            }
            else
            {
                ObjDb.ExecQuery("INSERT INTO Nomor (Nomor, noSN, Tahun) VALUES('2', '" + noSN + "', '" + dateNumber.ToString("yyyy-MM-dd") + "')");
            }

        }

        //GET IP
        public string GetIP
        {
            get
            {
                var context = System.Web.HttpContext.Current;
                string ip = String.Empty;

                if (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                    ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                else if (!String.IsNullOrWhiteSpace(context.Request.UserHostAddress))
                    ip = context.Request.UserHostAddress;

                if (ip == "::1")
                    ip = "127.0.0.1";

                return ip;
            }
        }

        
        #endregion

    }
}