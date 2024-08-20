using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.IO;

namespace eFinance
{
    public partial class Default : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ISLOGIN"] == "Yes")
            {
                Response.Redirect(Session["HOMEFILE"].ToString());
            }
            if (!IsPostBack)
            {
                // DEBUG
                //this.debugAutoLogin();
            }
        }

        //protected void debugAutoLogin()
        //{
        //    txtUserID.Text = "andreas";
        //    txtPassword.Text = "sak4sysnet";
        //    btnSignIn_Click(new object(), new EventArgs());
        //}

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (txtUserID.Text == "")
            {
                message += ObjSys.CreateMessage("User Id tidak boleh kosong.");
                valid = false;
            }
            if (txtPassword.Text == "")
            {
                message += ObjSys.CreateMessage("Password tidak boleh kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("userID", txtUserID.Text);
                    ObjGlobal.Param.Add("passwordUser", ObjSys.Sha1(txtPassword.Text));
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPCekLoginData", ObjGlobal.Param);
                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                       // string myIP = GetIPAddress();
                        DataRow myRow = mySet.Tables[0].Rows[0];
                        Session["USERID"] = myRow["noUser"].ToString();
                        Session["USERNAME"] = myRow["userID"].ToString();
                        Session["NAMAUSER"] = myRow["namaUser"].ToString();
                        Session["IMAGES"] = myRow["fotoUser"].ToString();
                        Session["CABANGID"] = myRow["noCabang"].ToString();
                        Session["JABATAN"] = myRow["namaJabatan"].ToString();
                        Session["STSCABANG"] = myRow["stsCabang"].ToString();
                        Session["CABANG"] = myRow["cabang"].ToString();
                        Session["PARENT"] = myRow["parent"].ToString();
                        Session["Kategori_Usaha"] = myRow["kategoriusaha"].ToString();

                        Session["ISLOGIN"] = "Yes";
                        DataSet mySetFile = ObjDb.GetRows("select a.noUser, a.noAkses, c.namaFileMenu, d.hakAkses "+
                            "from tAkses a left join tHakMenu b on a.noAkses = b.noAkses left join Menu c on "+
                            "b.noMenu = c.noMenu left join mAkses d on d.noAkses = a.noAkses "+
                            "where a.noUser = '" + myRow["noUser"].ToString() + "' and c.noMenu is not null order by c.noMenu");
                        string menu = mySetFile.Tables[0].Rows[0]["namaFileMenu"].ToString();
                        
                        Session["HOMEFILE"] = menu;

                        //DataSet myDashboard = ObjDb.GetRows("select distinct a.noCabang, a.namaCabang from mcabang a inner join mUser b on a.noCabang=b.noCabang " +
                        //    "where((a.stsPusat = 1 and a.stsCabang = 0) or(a.stsPusat = 0 and a.stsCabang = 1)) " +
                        //    "and b.noUser = '" + myRow["noUser"].ToString() + "' and a.noCabang = '" + myRow["noCabang"].ToString() + "'");
                        //if (myDashboard.Tables[0].Rows.Count > 0)
                        //{
                            Response.Redirect(menu);
                        //}
                        
                    }
                    else
                    {
                        lblMessage.Text = ObjSys.GetMessageStandard("danger", "Silahkan cek username atau password anda.");
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ObjSys.GetMessageStandard("danger", "Invalid transaction data, please contact developer.");
                }
            }
            else
            {
                lblMessage.Text = ObjSys.GetMessageStandard("danger", message);
            }
        }
    }
}