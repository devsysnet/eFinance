using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.View
{
    public partial class TransInvoiceNewView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                //loadDataCombo();
                dtPO.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                //dtETD.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            }
        }

        #region LoadData()
        protected void LoadData()
        {
            string start = "", end = "";
            try
            {
                start = Convert.ToDateTime(dtSearchStart.Text).ToString("yyyy-MM-dd");
            }
            catch (Exception)
            {
                start = Convert.ToDateTime(ObjSys.GetDate).ToString("yyyy-MM-dd");
            }

            try
            {
                end = Convert.ToDateTime(dtSearchEnd.Text).ToString("yyyy-MM-dd");
            }
            catch (Exception)
            {
                end = Convert.ToDateTime(ObjSys.GetDate).ToString("yyyy-MM-dd");
            }

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("Start", start);
            ObjGlobal.Param.Add("End", end);
            grdPOImport.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataViewInvoice", ObjGlobal.Param);
            grdPOImport.DataBind();

        }

        #endregion

        #region setInitial & AddRow
        private void SetInitialRow(string Id = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtProduct", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnProduct", typeof(string)));
            dt.Columns.Add(new DataColumn("txtProductName", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQty", typeof(string)));
            dt.Columns.Add(new DataColumn("lblUnit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtUnitPrice", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTotal", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataInvoiceDet", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtProduct"] = myRow["prodno"].ToString();
                dr["hdnProduct"] = myRow["noproduct"].ToString();
                dr["txtProductName"] = myRow["prodnm"].ToString();
                dr["txtQty"] = myRow["qty"].ToString();
                dr["lblUnit"] = myRow["punit"].ToString();
                dr["txtUnitPrice"] = ObjSys.IsFormatNumber(myRow["hargaSatuan"].ToString());
                dr["txtTotal"] = ObjSys.IsFormatNumber(myRow["subTotal"].ToString());

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtProduct"] = string.Empty;
                dr["hdnProduct"] = string.Empty;
                dr["txtProductName"] = string.Empty;
                dr["txtQty"] = string.Empty;
                dr["lblUnit"] = string.Empty;
                dr["txtUnitPrice"] = string.Empty;
                dr["txtTotal"] = string.Empty;

                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grdPO.DataSource = dt;
            grdPO.DataBind();

            SetPreviousData();
        }
        private void SetPreviousData()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HiddenField hdnProduct = (HiddenField)grdPO.Rows[i].FindControl("hdnProduct");
                        TextBox txtProduct = (TextBox)grdPO.Rows[i].FindControl("txtProduct");
                        TextBox txtProductName = (TextBox)grdPO.Rows[i].FindControl("txtProductName");
                        TextBox txtUnitPrice = (TextBox)grdPO.Rows[i].FindControl("txtUnitPrice");
                        Label lblUnit = (Label)grdPO.Rows[i].FindControl("lblUnit");
                        TextBox txtQty = (TextBox)grdPO.Rows[i].FindControl("txtQty");
                        TextBox txtTotal = (TextBox)grdPO.Rows[i].FindControl("txtTotal");

                        txtProduct.Text = dt.Rows[i]["txtProduct"].ToString();
                        hdnProduct.Value = dt.Rows[i]["hdnProduct"].ToString();
                        txtProductName.Text = dt.Rows[i]["txtProductName"].ToString();
                        txtQty.Text = dt.Rows[i]["txtQty"].ToString();
                        lblUnit.Text = dt.Rows[i]["lblUnit"].ToString();
                        txtUnitPrice.Text = dt.Rows[i]["txtUnitPrice"].ToString();
                        txtTotal.Text = dt.Rows[i]["txtTotal"].ToString();
                    }
                }
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        #region Select
        protected void grdPOImport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPOImport.PageIndex = e.NewPageIndex;
            LoadData();
        }

        #endregion

        #region Other
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

            txtSupplier.Text = "";
            dtPO.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtAmount.Text = "0.00";
            txtTotalAmount.Text = "0.00";

            SetInitialRow("0");
            LoadData();
            showHideForm(true, false);
        }

        protected void showHideForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            showHideForm(true, false);
        }
        #endregion

        protected void grdPOImport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detail")
            {
                try
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    HiddenField hdnIdPrint = (HiddenField)grdPOImport.Rows[rowIndex].FindControl("hdnIdPrint");

                    HttpContext.Current.Session["ParamReport"] = null;
                    Session["REPORTNAME"] = null;
                    Session["REPORTTITLE"] = null;
                    Param.Clear();
                    Param.Add("Id", hdnIdPrint.Value);
                    HttpContext.Current.Session.Add("ParamReport", Param);
                    Session["REPORTNAME"] = "RptInvoiceSO.rpt";
                    Session["REPORTTILE"] = "Report Delivery Order";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);
                }
                catch (Exception ex)
                {
                    Response.Write("Error:" + ex.ToString());
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
                }
            }
            else if (e.CommandName == "Select")
            {
                try
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    HiddenField hdnIdPrint = (HiddenField)grdPOImport.Rows[rowIndex].FindControl("hdnIdPrint");

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("Id", hdnIdPrint.Value);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataInvoice_D", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    dtPO.Text = Convert.ToDateTime(myRow["tglInv"]).ToString("dd-MMM-yyyy");
                    txtSupplier.Text = myRow["namaCust"].ToString();
                    txtAddress.Text = myRow["alamatKirim"].ToString();
                    txtKdInv.Text = myRow["kdInv"].ToString();
                    txtPPN.Text = ObjSys.IsFormatNumber(myRow["PPN"].ToString()).ToString();
                    txtAmount.Text = ObjSys.IsFormatNumber(myRow["Total"].ToString()).ToString();
                    txtTotalAmount.Text = ObjSys.IsFormatNumber(myRow["nilaiNet"].ToString());
                    SetInitialRow(hdnIdPrint.Value);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
                    showHideForm(false, true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.Message);
                }
            }
        }
    }
}