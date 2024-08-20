using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class AbsenOnlineKaryawan : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgkaryawan.ImageUrl = "~/assets/images/user/" + ObjSys.GetUserImage;
                lblNameUser.Text = ObjSys.GetFullName;
                lblCabang.Text = ObjSys.GetCabangName;
                GetUserIPAddress();

                DataSet mySet = ObjDb.GetRows("select * from parameterCabang where nocabang = '" + ObjSys.GetCabangId + "' ");
                DataRow myRow = mySet.Tables[0].Rows[0];
                string getjammasuk = myRow["jammasuk"].ToString();
                string getjamkeluar = myRow["jamkeluar"].ToString();
                jammasuk.InnerText = getjammasuk;
                jamkeluar.InnerText = getjamkeluar;

                var tgl1 = DateTime.Now.ToString("yyyy-MM-dd");
                DataSet mySeta = ObjDb.GetRows("select count(*) as adaAbsenM from tabsensi where nocabang = '" + ObjSys.GetCabangId + "' and tgl = '" + tgl1 + "' and nokaryawan = '" + ObjSys.GetUserId + "' and keterangan = 'M' ");
                DataRow myRowa = mySeta.Tables[0].Rows[0];
                string getadaAbsenMasuk = myRowa["adaAbsenM"].ToString();

                DataSet mySetb = ObjDb.GetRows("select count(*) as adaAbsenK from tabsensi where nocabang = '" + ObjSys.GetCabangId + "' and tgl = '" + tgl1 + "' and nokaryawan = '" + ObjSys.GetUserId + "' and keterangan = 'K' ");
                DataRow myRowb = mySetb.Tables[0].Rows[0];
                string getadaAbsenKeluar = myRowb["adaAbsenK"].ToString();

                if (getadaAbsenMasuk == "0" && getadaAbsenKeluar == "0")
                {
                    btnMasuk.Enabled = true;
                    btnKeluar.Enabled = false;
                    formalertAbsenK.Visible = false;
                    formalertAbsenM.Visible = true;
                    formalertAbsenS.Visible = false;

                    alertAbsenS.InnerText = "Anda Sudah Absen Hari Ini";
                    alertAbsenS.Visible = false;

                    alertAbsenK.InnerText = "Anda Belum Absen Keluar";
                    alertAbsenK.Visible = false;
                    alertAbsenM.InnerText = "Anda Belum Absen Masuk";
                    alertAbsenM.Visible = true;

                }
                else if (getadaAbsenMasuk != "0" && getadaAbsenKeluar == "0")
                {
                    btnMasuk.Enabled = false;
                    btnKeluar.Enabled = true;
                    formalertAbsenK.Visible = true;
                    formalertAbsenM.Visible = false;
                    formalertAbsenS.Visible = false;

                    alertAbsenS.InnerText = "Anda Sudah Absen Hari Ini";
                    alertAbsenS.Visible = false;

                    alertAbsenK.InnerText = "Anda Belum Absen Keluar";
                    alertAbsenK.Visible = true;
                    alertAbsenM.InnerText = "Anda Belum Absen Masuk";
                    alertAbsenM.Visible = false;
                }
                else if (getadaAbsenMasuk == "0" && getadaAbsenKeluar != "0")
                {
                    btnMasuk.Enabled = false;
                    btnKeluar.Enabled = false;
                    formalertAbsenK.Visible = false;
                    formalertAbsenM.Visible = false;
                    formalertAbsenS.Visible = true;

                    alertAbsenS.InnerText = "Anda Sudah Absen Hari Ini";
                    alertAbsenS.Visible = true;

                    alertAbsenK.InnerText = "Anda Belum Absen Keluar";
                    alertAbsenK.Visible = false;
                    alertAbsenM.InnerText = "Anda Belum Absen Masuk";
                    alertAbsenM.Visible = false;
                }



                    //string[] officeIPs = { "192.168.1.0", "192.168.1.1" }; // Masukkan IP Sekolah atau Kantor di sini
                    string[] officeIPs = new string[0]; // Initialize with an empty array
                DataSet mySetc = ObjDb.GetRows("select ipcabang from parametercabang a inner join muser b on a.nocabang=b.nocabang  where a.nocabang = '" + ObjSys.GetCabangId + "' and b.nouser = '" + ObjSys.GetUserId + "'");
                if (mySetc != null && mySetc.Tables.Count > 0)
                {
                    // Extract IP addresses using LINQ
                    officeIPs = mySetc.Tables[0].AsEnumerable().Where(row => row.Field<string>("ipcabang") != null).Select(row => row.Field<string>("ipcabang")).ToArray();
                }

                string userIPAddress = Request.UserHostAddress;
                lblIPAddress.Text = "Your IP Address: " + userIPAddress;

                if (IsInOfficeIPRange(userIPAddress, officeIPs))
                {
                    // User mengakses dari sekolah / kantor
                }
                else
                {
                    // User mengakses dari luar sekolah / kantor
                    //Response.StatusCode = 403; // Forbidden
                    Response.Redirect("Expired.html");
                    Response.End();
                }

            }
        }

        private string GetUserIPAddress()
        {
            // Mendapatkan alamat IP pengguna
            string UserHostAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(UserHostAddress))
            {
                UserHostAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            return UserHostAddress;
        }



        private bool IsInOfficeIPRange(string userIPAddress, string[] officeIPs)
           {
                foreach (string officeIP in officeIPs)
                {
                    if (userIPAddress.StartsWith(officeIP))
                    {
                        return true;
                    }
                }
                return false;
            }
     


        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            try
            {
                var jam = DateTime.Now.ToString("HH:mm:ss");
                var tgl = DateTime.Now.ToString("yyyy-MM-dd");
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("jam", jam);
                ObjGlobal.Param.Add("tgl", tgl);
                ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                ObjGlobal.Param.Add("nouser", ObjSys.GetUserId);
                ObjGlobal.Param.Add("keterangan", "masuk");

                ObjGlobal.ExecuteProcedure("Spinserttabsen", ObjGlobal.Param);

                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil disimpan.");
                formalertAbsenK.Visible = true;
                formalertAbsenM.Visible = false;
                formalertAbsenS.Visible = false;
                alertAbsenK.Visible = true;
                btnKeluar.Enabled = true;
                btnMasuk.Enabled = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void btnKeluar_Click(object sender, EventArgs e)
        {
            string message = "";
            try
            {
                var jam = DateTime.Now.ToString("HH:mm:ss");
                var tgl = DateTime.Now.ToString("yyyy-MM-dd");
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("jam", jam);
                ObjGlobal.Param.Add("tgl", tgl);
                ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                ObjGlobal.Param.Add("nouser", ObjSys.GetUserId);
                ObjGlobal.ExecuteProcedure("Stabsenkeluar", ObjGlobal.Param);
                formalertAbsenK.Visible = false;
                formalertAbsenM.Visible = false;
                formalertAbsenS.Visible = true;
                alertAbsenS.Visible = true;
                btnKeluar.Enabled = false;
                btnMasuk.Enabled = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

       

        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }
        protected void CloseMessage()
        {
            ((Label)Master.FindControl("lblMessage")).Text = "";
            ((Label)Master.FindControl("lblMessage")).Visible = false;
        }


    }
}