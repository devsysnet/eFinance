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

namespace eFinance.Pages.Master.View
{
    public partial class LokasiGudangView : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                LoadDataCombo();
                LoadDataView();
            }
        }
        protected void LoadData()
        {
            //DataSet MySet = ObjDb.GetRows("select * from product");
            //if (MySet.Tables[0].Rows.Count > 0)
            //    lblJumlahData.Text = MySet.Tables[0].Rows.Count.ToString();
        }
        protected void LoadDataView()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdLokGudang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUpdateLokGudang", ObjGlobal.Param);
            grdLokGudang.DataBind();
        }
        protected void LoadDataCombo()
        {
            cboGudang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-------' name union all SELECT noGudang id, kdGudang +'-'+ namaGudang name FROM Gudang where stsGudang not in ('Inspeksi') and noCabang='" + ObjSys.GetCabangId + "') a");
            cboGudang.DataValueField = "id";
            cboGudang.DataTextField = "name";
            cboGudang.DataBind();

            cboBrand.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-------' name union all SELECT brand id, brand name FROM mbrand) a");
            cboBrand.DataValueField = "id";
            cboBrand.DataTextField = "name";
            cboBrand.DataBind();
        }
        protected void LoadDataProduk()
        {
            if (ViewState["ProdukState"] == null)
            {
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noLokasiGudang", hdnId.Value);
                ViewState["ProdukState"] = ObjGlobal.GetDataProcedureDataTable("SPLoadDataViewProductLokGudang", ObjGlobal.Param);
            }
            DataTable myData = (DataTable)ViewState["ProdukState"];
            myData.DefaultView.RowFilter = "stsPilih=1";
            grdAddProduk.DataSource = myData;
            grdAddProduk.DataBind();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDataView();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ShowHideGridAndForm(true, false);
        }
        protected void btnPilihProduk_Click(object sender, EventArgs e)
        {
            LoadDataProduk();
            dlgAddProduk.Show();
        }

        protected void grdAddProduk_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnStsPilih = (HiddenField)e.Row.FindControl("hdnStsPilih");
                CheckBox chkCheck = (CheckBox)e.Row.FindControl("chkCheck");

                if (hdnStsPilih.Value == "1")
                    chkCheck.Checked = true;
                else
                    chkCheck.Checked = false;
            }
        }
        protected void grdLokGudang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLokGudang.PageIndex = e.NewPageIndex;
            LoadDataView();
        }
        protected void grdLokGudang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdLokGudang.SelectedRow.RowIndex;
                string noLokGudang = grdLokGudang.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noLokGudang;
                DataSet MySet = ObjDb.GetRows("select * from LokasiGudang where noLokasiGudang='" + noLokGudang + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0];

                    txtnama.Text = MyRow["namaLokGud"].ToString();
                    txtKet.Text = MyRow["ketLokGud"].ToString();
                    cboGudang.Text = MyRow["noGudang"].ToString();
                    txtSuhuDari.Text = MyRow["suhuDari"].ToString();
                    txtSuhuKe.Text = MyRow["suhuKe"].ToString();
                    //LoadDataProduk();
                    ShowHideGridAndForm(false, true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Tidak ada.");
                }

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
    }
}