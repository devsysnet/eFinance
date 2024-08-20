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
    public partial class DeleteVirtualAccount : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        private int rowIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            //execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
            }
        }

        protected void LoadDataPelunasanAR()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("KdBayar", txtKdBayar.Text);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdARSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataVAdelete", ObjGlobal.Param);
            grdARSiswa.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtKdBayar.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Kode Bayar harus diisi.");
            }
            else
            {
                CloseMessage();
                LoadDataPelunasanAR();
            }
        }

        //protected void grdARSiswa_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "SelectDelete")
        //    {
        //        int rowIndex = int.Parse(e.CommandArgument.ToString());
        //        string noSiswa = grdARSiswa.DataKeys[rowIndex].Value.ToString();
        //        HiddenField hdnNoPiut = (HiddenField)grdARSiswa.Rows[rowIndex].FindControl("hdnNoPiut");
        //        HiddenField hdnkdBayar = (HiddenField)grdARSiswa.Rows[rowIndex].FindControl("hdnkdBayar");

        //        ObjGlobal.Param.Clear();
        //        ObjGlobal.Param.Add("noSiswa", noSiswa);
        //        ObjGlobal.Param.Add("noPiutang", hdnNoPiut.Value);
        //        ObjGlobal.Param.Add("kdBayar", hdnkdBayar.Value);
        //        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
        //        ObjGlobal.ExecuteProcedure("SPUpdatePelunasanAR", ObjGlobal.Param);

        //        LoadDataPelunasanAR();
        //        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //        ShowMessage("success", "Data berhasil dihapus");
        //        this.ShowHideGridAndForm(true, false);
        //    }
        //}

        //protected void cmdMode_Click(object sender, EventArgs e)
        //{


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                HiddenField hdnkdBayar = (HiddenField)grdARSiswa.Rows[rowIndex].FindControl("hdnkdBayar");
                //if (hdnMode.Value.ToLower() == "delete")
                //{
                ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kdBayar", hdnkdBayar.Value);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.ExecuteProcedure("SPdeleteVA", ObjGlobal.Param);

                    LoadDataPelunasanAR();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus.");
                    this.ShowHideGridAndForm(true, false);
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Invalid transaction data.");
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

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
    }
}