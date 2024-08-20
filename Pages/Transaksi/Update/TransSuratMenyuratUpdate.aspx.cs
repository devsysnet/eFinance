using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransSuratMenyuratUpdate : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdSuratMenyurat.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataSuratMenyurat", ObjGlobal.Param);
            grdSuratMenyurat.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdSuratMenyurat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdSuratMenyurat.PageIndex = e.NewPageIndex;
            LoadData();
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false);
            LoadData();
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (dtDate.Text == "")
            {
                message = ObjSys.CreateMessage("Tanggal Surat harus di isi.");
                valid = false;
            }
            if (cboJenis.Text == "")
            {
                message += ObjSys.CreateMessage("Jenis harus di pilih.");
                valid = false;
            }
            if (txtNoSurat.Text == "")
            {
                message = ObjSys.CreateMessage("No Surat Surat harus di isi.");
                valid = false;
            }
            if (txtPerihal.Text == "")
            {
                message = ObjSys.CreateMessage("Perihal Surat harus di isi.");
                valid = false;
            }
            if (txtNoFile.Text == "")
            {
                message = ObjSys.CreateMessage("No File Surat harus di isi.");
                valid = false;
            }


            if (valid == true)

            {
                try
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("nosurat", hdnId.Value);
                    ObjGlobal.Param.Add("tglsurat", dtDate.Text);
                    ObjGlobal.Param.Add("jenis", cboJenis.Text);
                    ObjGlobal.Param.Add("nomorsurat", txtNoSurat.Text);
                    ObjGlobal.Param.Add("nofile", txtNoFile.Text);
                    ObjGlobal.Param.Add("perihal", txtPerihal.Text);
                    ObjGlobal.Param.Add("modiBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("modiDate", ObjSys.GetNow);
                    ObjGlobal.GetDataProcedure("SPUpdateSuratMenyurat", ObjGlobal.Param);

                    LoadData();
                    this.ShowHideGridAndForm(true, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diubah.");
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }

        protected void grdSuratMenyurat_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectEdit")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nosurat = grdSuratMenyurat.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nosurat;

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("nosurat", hdnId.Value);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadSuratMenyuratDetil", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    cboJenis.Text = myRow["jenis"].ToString();
                    txtNoSurat.Text = myRow["nomorsurat"].ToString();
                    dtDate.Text = myRow["tglsurat"].ToString();
                    txtPerihal.Text = myRow["perihal"].ToString();
                    txtNoFile.Text = myRow["nofile"].ToString();

                    this.ShowHideGridAndForm(false, true);
                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nosurat = grdSuratMenyurat.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nosurat;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("nosurat", hdnId.Value);
                    ObjDb.Delete("TSuratmenyurat", ObjDb.Where);

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus");
                    this.ShowHideGridAndForm(true, false);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
    }
}