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
    public partial class TransSuratMenyuratInput : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void LoadDataPeminta()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
        
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
            dtDate.Text = "";
            cboJenis.Text = "";
            txtNoSurat.Text = "";
            txtNoFile.Text = "";
            txtPerihal.Text = "";
            
        }

       

      

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";

            if (dtDate.Text == "")
            {
                message = ObjSys.CreateMessage("Tanggal harus di isi.");
                alert = "error";
                valid = false;
            }
            if (cboJenis.Text == "")
            {
                message = ObjSys.CreateMessage("Jenis harus di pilih.");
                alert = "error";
                valid = false;
            }
            if (txtNoSurat.Text == "")
            {
                message = ObjSys.CreateMessage("No Surat harus di isi.");
                alert = "error";
                valid = false;
            }
            if (txtNoFile.Text == "")
            {
                message = ObjSys.CreateMessage("No File harus di isi.");
                alert = "error";
                valid = false;
            }
            if (txtPerihal.Text == "")
            {
                message = ObjSys.CreateMessage("Perihal harus di isi.");
                alert = "error";
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                    string Kode = ObjSys.GetCodeAutoNumberNew("35", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("tglsurat", dtDate.Text);
                    ObjGlobal.Param.Add("jenis", cboJenis.Text);
                    ObjGlobal.Param.Add("nomorsurat", txtNoSurat.Text);
                    ObjGlobal.Param.Add("nofile", txtNoFile.Text);
                    ObjGlobal.Param.Add("perihal", txtPerihal.Text);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("createddate", ObjSys.GetNow);

                    ObjGlobal.ExecuteProcedure("SPInsertSuratMenyurat", ObjGlobal.Param);

                    ObjSys.UpdateAutoNumberCodeNew("35", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan");
                    ClearData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.Message);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage(alert, message);
            }

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void btnMinta_Click(object sender, EventArgs e)
        {
            LoadDataPeminta();
            
        }

    }
}