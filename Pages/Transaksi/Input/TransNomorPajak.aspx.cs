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
using System.IO;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransNomorPajak : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("TransNomorPajak.aspx");
                dtInput.Text = DateTime.Now.ToString("dd MMM yyyy"); ;
                dtSurat.Text = DateTime.Now.ToString("dd MMM yyyy"); ;
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
        protected void ClearData()
        {
            dtInput.Text = DateTime.Now.ToString("dd MMM yyyy"); ;
            dtSurat.Text = DateTime.Now.ToString("dd MMM yyyy"); ;
            txtBookPajakFrom1.Text = "";
            txtBookPajakFrom2.Text = "";
            txtBookPajakFrom3.Text = "";
            txtBookPajakTo1.Text = "";
            txtNoSurat.Text = "";
            CloseMessage();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (txtNoSurat.Text == "")
            {
                message += ObjSys.CreateMessage("No Surat tidak boleh kosong.");
                valid = false;
            }
            if (txtBookPajakTo1.Text == "")
            {
                message += ObjSys.CreateMessage("Book Pajak 1 tidak boleh kosong.");
                valid = false;
            }
            if (txtBookPajakFrom3.Text == "")
            {
                message += ObjSys.CreateMessage("Book Pajak 3 tidak boleh kosong.");
                valid = false;
            }
            if (txtBookPajakFrom2.Text == "")
            {
                message += ObjSys.CreateMessage("Book Pajak 2 tidak boleh kosong.");
                valid = false;
            }
            if (txtBookPajakFrom1.Text == "")
            {
                message += ObjSys.CreateMessage("Book Pajak 4 tidak boleh kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {

                    ObjDb.Data.Clear();
                    ObjDb.Data.Add("noSurat", txtNoSurat.Text);
                    ObjDb.Data.Add("tglSuratPajak", dtSurat.Text);
                    ObjDb.Data.Add("tglInput", dtInput.Text);
                    ObjDb.Data.Add("tingkat4To", txtBookPajakTo1.Text);
                    ObjDb.Data.Add("tingkat1", txtBookPajakFrom1.Text);
                    ObjDb.Data.Add("tingkat2", txtBookPajakFrom2.Text);
                    ObjDb.Data.Add("tingkat3", txtBookPajakFrom3.Text);
                    ObjDb.Data.Add("sts", "1");
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("createdDate", ObjSys.GetDate);
                    ObjDb.Insert("tOrderPajak", ObjDb.Data);

                    ClearData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                }
                catch (Exception ex)
                {
                    if (valid == false)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", message);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", ex.ToString());
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}