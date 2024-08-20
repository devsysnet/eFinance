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

namespace eFinance.Pages.Transaksi.Posting
{
    public partial class Postpaymentgateway : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        public string _urlCreateVA = string.Empty;
        public string _urlSync = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string sakWebApiUrl = ConfigurationManager.AppSettings.Get("SakWebApiUrl");

                //_urlCreateVA = sakWebApiUrl + "/api/PaymentTransaction/CreateVA/All";
                //_urlSync = sakWebApiUrl + "/api/PaymentTransaction/Sync";

                //lblApiUrl.Text = sakWebApiUrl;
                //btnCreateVA.PostBackUrl = _urlCreateVA;
                //btnSync.PostBackUrl = _urlSync;
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

        protected void btnPosting_Click(object sender, EventArgs e)
        {
            try
            {
               

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                ObjGlobal.ExecuteProcedure("SPProsesPaymentgateway", ObjGlobal.Param);

                //showHideForm(true, false);
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data Berhasil Di Posting");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
            //LoadData();
        }

     
    }
}