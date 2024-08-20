using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.View
{
    public partial class MSupplierView : System.Web.UI.Page
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
            grdBarang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataSuplier", ObjGlobal.Param);
            grdBarang.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdBarang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdBarang.PageIndex = e.NewPageIndex;
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
        }

        protected void LoadDataCombo()
        {
            cboKota.DataSource = ObjDb.GetRows("select 0 as noKota, '---Pilih Kota---' as Kota union all select noKota, Kota from mKota");
            cboKota.DataValueField = "noKota";
            cboKota.DataTextField = "Kota";
            cboKota.DataBind();
        }

       

        protected void grdBarang_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectEdit")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nobarang = grdBarang.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nobarang;

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noSupplier", hdnId.Value);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDatasSupplierDetil", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtKode.Text = myRow["kodeSupplier"].ToString();
                    txtnamaSupplier.Text = myRow["namaSupplier"].ToString();
                    txtnpwp.Text = myRow["NPWP"].ToString();
                    txtAlamat.Text = myRow["Alamat"].ToString();
                    cboKota.SelectedValue = myRow["kota"].ToString();
                    txttelpKantor.Text = myRow["telpKantor"].ToString();
                    txtFax.Text = myRow["Fax"].ToString();
                    txtemail.Text = myRow["email"].ToString();
                    txtnamaPIC.Text = myRow["namaPIC"].ToString();
                    txttelpPIC.Text = myRow["telpPIC"].ToString();
                    txtnoaccount.Text = myRow["noaccount"].ToString();
                    txtBank.Text = myRow["Bank"].ToString();
                    keterangan.Text = myRow["keterangan"].ToString();
                    keterangan1.Text = myRow["keterangan1"].ToString();
                    keterangan2.Text = myRow["keterangan2"].ToString();

                    LoadDataCombo();
                    this.ShowHideGridAndForm(false, true);
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