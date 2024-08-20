using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class InsertBookingNomorPajak : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                dtSurat.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }

        protected void CloseMessage()
        {
            ((Label)Master.FindControl("lblMessage")).Text = "";
            ((Label)Master.FindControl("lblMessage")).Visible = false;
        }
        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "";

            DataSet mySet_cek = ObjDb.GetRows("Select * from TransMintaNomorPajak Where nomorSurat = '" + txtNoSurat.Text + "'");
            if (mySet_cek.Tables[0].Rows.Count > 0)
            {
                message += ObjSys.CreateMessage("Booking Number already exists !");
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("nomorSurat", txtNoSurat.Text);
                    ObjGlobal.Param.Add("tglInput", dtDate.Text);
                    ObjGlobal.Param.Add("tglSurat", dtSurat.Text);
                    ObjGlobal.Param.Add("noKolom1", txtKolom1.Text);
                    ObjGlobal.Param.Add("noKolom2", txtKolom2.Text);
                    ObjGlobal.Param.Add("noDepan", txtKolom1.Text + "." + txtKolom2.Text);
                    ObjGlobal.Param.Add("dariNomor", txtNoPajak.Text);
                    ObjGlobal.Param.Add("sampaiNomor", txtNoPajak2.Text);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.ExecuteProcedure("SPProsesCreateNomorPajak", ObjGlobal.Param);

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data Successfully Saved.");
                    ClearData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Invalid transaction data.");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }

        protected void ClearData()
        {

            txtKolom1.Text = "";
            txtKolom2.Text = "";
            txtNoPajak.Text = "0";
            txtNoPajak2.Text = "0";
            txtNoSurat.Text = "";
            dtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            dtSurat.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
            CloseMessage();
        }
    }
}