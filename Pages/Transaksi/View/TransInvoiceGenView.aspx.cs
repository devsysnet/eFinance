using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;

namespace eFinance.Pages.Transaksi.View
{
    public partial class TransInvoiceGenView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();
        protected string execBind = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                LoadData();
                loadkodetax();
                loadDataCombo();
                dtInv.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
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
            grdInvoiceGen.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataViewInvoiceGen", ObjGlobal.Param);
            grdInvoiceGen.DataBind();

        }
        protected void LoadDataPelanggan()
        {
            DataTable myData = ObjDb.GetRowsDataTable("select * from mCustomer");
            grdDataPelanggan.DataSource = myData;
            grdDataPelanggan.DataBind();
        }
        protected void btnBrowsePelanggan_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataPelanggan();
            dlgAddData.PopupControlID = "panelAddDataPelanggan";
            dlgAddData.Show();
        }
        private void loadDataCombo()
        {
            cboCurrency.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noMataUang id, namaMataUang name FROM mMataUang) a");
            cboCurrency.DataValueField = "id";
            cboCurrency.DataTextField = "name";
            cboCurrency.DataBind();

            cboSales.DataSource = ObjDb.GetRows("select a.* from (select noUser id, namauser name FROM mUser where stsUser = '1' and noUser = '" + ObjSys.GetUserId + "' union all SELECT distinct noUser id, namauser name FROM mUser u where stsUser = '1' and EXISTS(SELECT 1 FROM tAkses t WHERE noAkses IN('9', '12') AND u.noUser = t.noUser)) a");
            cboSales.DataValueField = "id";
            cboSales.DataTextField = "name";
            cboSales.DataBind();
        }

        protected void cboCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kurs = "";
            DataSet mySetH = ObjDb.GetRows("select TOP 1 nilaiKursPajak from tKursPajak where noMatauang = '" + cboCurrency.SelectedValue + "'");
            if (mySetH.Tables[0].Rows.Count > 0)
            {
                DataRow myRowH = mySetH.Tables[0].Rows[0];
                kurs = ObjSys.IsFormatNumber(myRowH["nilaiKursPajak"].ToString());
            }

            if (cboCurrency.Text == "0")
            {
                txtKurs.Visible = false;
                Kurstrs.Visible = false;
            }
            else
            {
                txtKurs.Visible = true;
                Kurstrs.Visible = true;
            }
            txtKurs.Text = kurs;

            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
        }
        private void loadkodetax()
        {
            cbolistkodetaxnew.DataSource = ObjDb.GetRows("select top 1 convert(varchar(30),noDepan)+'.'+convert(varchar(30),noPajak) as notax from TransNomorPajak where stsNomor='1' order by noUrutPajak");
            cbolistkodetaxnew.DataValueField = "notax";
            cbolistkodetaxnew.DataTextField = "notax";
            cbolistkodetaxnew.DataBind();
        }


        #endregion
        protected void grdDataPelanggan_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRow = grdDataPelanggan.SelectedIndex;
            hdnNoCustomer.Value = grdDataPelanggan.Rows[selectedRow].Cells[0].Text;
            txtNamaPelanggan.Text = grdDataPelanggan.Rows[selectedRow].Cells[1].Text;
            txtAlamatPelanggan.Text = grdDataPelanggan.Rows[selectedRow].Cells[2].Text;
        }

        protected void cboTax_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTax.Text == "1")
            {
                if (cboTaxNo.Text == "1")
                {
                    cbolistkodetaxnew.Visible = true;
                    loadkodetax();
                }
                else
                {
                    cbolistkodetaxnew.Visible = false;
                }
            }
            else
            {
                cbolistkodetaxnew.Visible = false;
            }

            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
        }
        protected void cboTaxNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTaxNo.Text == "1")
            {
                if (cboTax.Text == "1")
                {
                    cbolistkodetaxnew.Visible = true;
                    loadkodetax();
                }
                else
                {
                    cbolistkodetaxnew.Visible = false;

                }
            }
            else
            {
                cbolistkodetaxnew.Visible = false;


            }
        }
        #region setInitial & AddRow
        private void SetInitialRow(string Id = "0")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTotal", typeof(string)));
            dt.Columns.Add(new DataColumn("txtSatuan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtProductName", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQty", typeof(string)));
            dt.Columns.Add(new DataColumn("txtUnitPrice", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnNoInvoicegenD", typeof(string)));


            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataInvoiceDetgen", ObjGlobal.Param);
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtSatuan"] = myRow["satuan"].ToString();
                dr["txtProductName"] = myRow["brg"].ToString();
                dr["txtQty"] = ObjSys.IsFormatNumber(myRow["qty"].ToString());
                dr["txtUnitPrice"] = ObjSys.IsFormatNumber(myRow["hargaSatuan"].ToString());
                dr["txtTotal"] = ObjSys.IsFormatNumber(myRow["grossDet"].ToString());
                dr["hdnNoInvoicegenD"] = myRow["noInvoicegenD"].ToString();
                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtProductName"] = string.Empty;
                dr["txtTotal"] = string.Empty;
                dr["txtSatuan"] = string.Empty;
                dr["txtQty"] = string.Empty;
                dr["txtUnitPrice"] = string.Empty;
                dr["hdnNoInvoicegenD"] = string.Empty;

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdSOD.DataSource = dt;
            grdSOD.DataBind();


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

                        TextBox txtProductName = (TextBox)grdSOD.Rows[i].FindControl("txtProductName");
                        TextBox txtUnitPrice = (TextBox)grdSOD.Rows[i].FindControl("txtUnitPrice");
                        TextBox txtSatuan = (TextBox)grdSOD.Rows[i].FindControl("txtSatuan");
                        TextBox txtQty = (TextBox)grdSOD.Rows[i].FindControl("txtQty");
                        TextBox txtTotal = (TextBox)grdSOD.Rows[i].FindControl("txtTotal");
                        HiddenField hdnNoInvoicegenD = (HiddenField)grdSOD.Rows[i].FindControl("hdnNoInvoicegenD");

                        txtProductName.Text = dt.Rows[i]["txtProductName"].ToString();
                        txtQty.Text = dt.Rows[i]["txtQty"].ToString();
                        txtSatuan.Text = dt.Rows[i]["txtSatuan"].ToString();
                        txtUnitPrice.Text = dt.Rows[i]["txtUnitPrice"].ToString();
                        txtTotal.Text = dt.Rows[i]["txtTotal"].ToString();
                        hdnNoInvoicegenD.Value = dt.Rows[i]["hdnNoInvoicegenD"].ToString();

                    }
                }
            }
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

        protected void showHideForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        #region Select
        protected void grdInvoiceGen_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdInvoiceGen.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void grdInvoiceGen_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = grdInvoiceGen.SelectedRow.RowIndex;
                string Id = grdInvoiceGen.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = Id;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("Id", hdnId.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataInvoice_Dgen", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                dtInv.Text = Convert.ToDateTime(myRow["tglInvoice"]).ToString("dd-MMM-yyyy");
                txtNamaPelanggan.Text = myRow["namaCust"].ToString();
                cboSales.Text = myRow["nosales"].ToString();
                txtAlamatPelanggan.Text = myRow["alamatCust"].ToString();
                hdnNoCustomer.Value = myRow["noCust"].ToString();
                cboCurrency.Text = myRow["noMataUang"].ToString();
                txtKurs.Text = ObjSys.IsFormatNumber(myRow["kursInvoice"].ToString());
                txtPOno.Text = myRow["nopo"].ToString();
                txtTop.Text = myRow["termInvoice"].ToString();
                txtBankName.Text = myRow["bankname"].ToString();
                txtNoRek.Text = myRow["bankcode"].ToString();
                txtSubTotal.Text = ObjSys.IsFormatNumber(myRow["nilaiNet"].ToString());
                txtTotal.Text = ObjSys.IsFormatNumber(myRow["Total"].ToString());
                txtNoref.Text = myRow["noRef"].ToString();
                TxtKode.Text = myRow["kdInvoice"].ToString();
                txtPPN.Text = ObjSys.IsFormatNumber(myRow["nilaiPajak"].ToString());
                //txtPPN.Text = ObjSys.IsFormatNumber(myRow["nilaiPajakInv"].ToString());
                txtRemarks.Text = myRow["Keterangan"].ToString();


                SetInitialRow(hdnId.Value);
                //ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
                showHideForm(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        #endregion
        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdInvoiceGen.Rows)
                {
                    string index = grdInvoiceGen.DataKeys[gvrow.RowIndex].Value.ToString();
                    if (userdetails.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)gvrow.FindControl("chkCheck");
                        myCheckBox.Checked = true;
                    }
                }
            }
        }
        private void SaveCheckedValues()
        {
            ArrayList userdetails = new ArrayList();
            foreach (GridViewRow gvrow in grdInvoiceGen.Rows)
            {
                string index = grdInvoiceGen.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdInvoiceGen.Rows[gvrow.RowIndex].FindControl("chkCheck");
                bool result = chkCheck.Checked;
                // Check in the Session
                if (ViewState["CHECKED_ITEMS"] != null)
                    userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
                if (result)
                {
                    if (!userdetails.Contains(index))
                        userdetails.Add(index);
                }
                else
                    userdetails.Remove(index);
            }
            if (userdetails != null && userdetails.Count > 0)
                ViewState["CHECKED_ITEMS"] = userdetails;
        }
        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnMode.Value.ToLower() == "deleteall")
                {
                    /*DELETE ALL SELECTED*/

                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {


                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noInvoicegen", itemRow);
                            ObjDb.Delete("tInvoiceGeneral_D", ObjDb.Where);
                            ObjDb.Delete("tInvoiceGeneral_H", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdInvoiceGen.Rows)
                    {
                        string itemId = grdInvoiceGen.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdInvoiceGen.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {

                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noInvoicegen", itemId);
                            ObjDb.Delete("tInvoiceGeneral_D", ObjDb.Where);
                            ObjDb.Delete("tInvoiceGeneral_H", ObjDb.Where);
                        }
                    }
                }
                /*END DELETE ALL SELECTED*/
                LoadData();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil dihapus.");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

       

        protected void btnReset_Click(object sender, EventArgs e)
        {
            showHideForm(true, false);
            LoadData();
        }
        protected void grdInvoiceGen_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detail")
            {
                try
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    HiddenField hdnIdPrint = (HiddenField)grdInvoiceGen.Rows[rowIndex].FindControl("hdnIdPrint");

                    HttpContext.Current.Session["ParamReport"] = null;
                    Session["REPORTNAME"] = null;
                    Session["REPORTTITLE"] = null;
                    Param.Clear();
                    Param.Add("Id", hdnIdPrint.Value);
                    HttpContext.Current.Session.Add("ParamReport", Param);
                    Session["REPORTNAME"] = "TransInvoiceGen.rpt";
                    Session["REPORTTILE"] = "Report Delivery Order";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);
                }
                catch (Exception ex)
                {
                    Response.Write("Error:" + ex.ToString());
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
                }
            
            
            }
        }
    }
} 