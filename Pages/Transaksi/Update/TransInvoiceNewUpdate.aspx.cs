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

namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransInvoiceNewUpdate : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected string execBind = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                LoadData();
                //loadDataCombo();
                dtSO.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
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
        private void SetInitialRow(string Id = "0")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtProduct", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnProduct", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnUnit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtProductName", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQty", typeof(string)));
            dt.Columns.Add(new DataColumn("lblUnit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtUnitPrice", typeof(string)));
            dt.Columns.Add(new DataColumn("txtDiscount", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTotal", typeof(string)));
            //dt.Columns.Add(new DataColumn("hdnnoconfD", typeof(string)));


            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataInvoiceDet", ObjGlobal.Param);
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtProduct"] = myRow["prodno"].ToString();
                dr["hdnProduct"] = myRow["noproduct"].ToString();
                dr["hdnUnit"] = myRow["punit"].ToString();
                dr["txtProductName"] = myRow["prodnm"].ToString();
                dr["txtQty"] = ObjSys.IsFormatNumber(myRow["qty"].ToString());
                dr["lblUnit"] = myRow["punit"].ToString();
                dr["txtUnitPrice"] = ObjSys.IsFormatNumber(myRow["hargaSatuan"].ToString());
                dr["txtDiscount"] = ObjSys.IsFormatNumber(myRow["nilDisc"].ToString());
                dr["txtTotal"] = ObjSys.IsFormatNumber(myRow["subTotal"].ToString());
                //dr["hdnnoconfD"] = myRow["noconfD"].ToString();

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtProduct"] = string.Empty;
                dr["hdnProduct"] = string.Empty;
                dr["hdnUnit"] = string.Empty;
                dr["txtProductName"] = string.Empty;
                dr["txtQty"] = string.Empty;
                dr["lblUnit"] = string.Empty;
                dr["txtUnitPrice"] = string.Empty;
                dr["txtDiscount"] = string.Empty;
                dr["txtTotal"] = string.Empty;
                //dr["hdnnoconfD"] = string.Empty;

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
                        HiddenField hdnProduct = (HiddenField)grdSOD.Rows[i].FindControl("hdnProduct");
                        HiddenField hdnUnit = (HiddenField)grdSOD.Rows[i].FindControl("hdnUnit");
                        TextBox txtProduct = (TextBox)grdSOD.Rows[i].FindControl("txtProduct");
                        TextBox txtProductName = (TextBox)grdSOD.Rows[i].FindControl("txtProductName");
                        TextBox txtUnitPrice = (TextBox)grdSOD.Rows[i].FindControl("txtUnitPrice");
                        TextBox txtDiscount = (TextBox)grdSOD.Rows[i].FindControl("txtDiscount");
                        TextBox txtQty = (TextBox)grdSOD.Rows[i].FindControl("txtQty");
                        Label lblUnit = (Label)grdSOD.Rows[i].FindControl("lblUnit");
                        TextBox txtTotal = (TextBox)grdSOD.Rows[i].FindControl("txtTotal");
                        CheckBox chkDefault = grdSOD.Rows[i].FindControl("chkDefault") as CheckBox;
                        //HiddenField hdnnoconfD = (HiddenField)grdSOD.Rows[i].FindControl("hdnnoconfD");

                        txtProduct.Text = dt.Rows[i]["txtProduct"].ToString();
                        hdnProduct.Value = dt.Rows[i]["hdnProduct"].ToString();
                        hdnUnit.Value = dt.Rows[i]["hdnUnit"].ToString();
                        txtProductName.Text = dt.Rows[i]["txtProductName"].ToString();
                        txtQty.Text = dt.Rows[i]["txtQty"].ToString();
                        lblUnit.Text = dt.Rows[i]["lblUnit"].ToString();
                        txtUnitPrice.Text = dt.Rows[i]["txtUnitPrice"].ToString();
                        txtDiscount.Text = dt.Rows[i]["txtDiscount"].ToString();
                        txtTotal.Text = dt.Rows[i]["txtTotal"].ToString();

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
        protected void grdPOImport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPOImport.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void grdPOImport_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = grdPOImport.SelectedRow.RowIndex;
                string Id = grdPOImport.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = Id;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("Id", hdnId.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataInvoice_D", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                dtSO.Text = Convert.ToDateTime(myRow["tglInv"]).ToString("dd-MMM-yyyy");
                txtnamacust.Text = myRow["namaCust"].ToString();
                txtAddress.Text = myRow["alamatCust"].ToString();
                hdnCustomer.Value = myRow["noCust"].ToString();
                txtDeliveryAddress.Text = myRow["alamatKirim"].ToString();
                txtPhone.Text = myRow["noTelpCust"].ToString();
                txtCustomerPOref.Text = myRow["referensiInv"].ToString();
                txtTOP.Text = myRow["term"].ToString();
                TextBox1.Text = myRow["kdInv"].ToString();
                txtDiscountH.Text = ObjSys.IsFormatNumber(myRow["DiskonH"].ToString());
                txtPPN.Text = ObjSys.IsFormatNumber(myRow["nilaiPajakInv"].ToString());
                txtRemarks.Text = myRow["KeteranganInv"].ToString();
                txtSubTotal.Text = ObjSys.IsFormatNumber(myRow["grossInv"].ToString());
                txtTotal.Text = ObjSys.IsFormatNumber(myRow["netInv"].ToString());

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
                foreach (GridViewRow gvrow in grdPOImport.Rows)
                {
                    string index = grdPOImport.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdPOImport.Rows)
            {
                string index = grdPOImport.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdPOImport.Rows[gvrow.RowIndex].FindControl("chkCheck");
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
                            DataSet MySet = ObjDb.GetRows("select * from tInvoiceSO_H where noInv = '" + itemRow + "'");
                            if (MySet.Tables[0].Rows.Count > 0)
                            {
                                DataRow MyRow = MySet.Tables[0].Rows[0];
                                string noConf = MyRow["noSJ"].ToString();

                                ObjDb.ExecQuery("UPDATE tConfDO_H SET sts = 0 WHERE noConf = '" + noConf + "'");
                            }

                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noInv", itemRow);
                            ObjDb.Delete("tInvoiceSO_D", ObjDb.Where);
                            ObjDb.Delete("tInvoiceSO_H", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdPOImport.Rows)
                    {
                        string itemId = grdPOImport.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdPOImport.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            DataSet MySet = ObjDb.GetRows("select * from tInvoiceSO_H where noInv = '" + itemId + "'");
                            if (MySet.Tables[0].Rows.Count > 0)
                            {
                                DataRow MyRow = MySet.Tables[0].Rows[0];
                                string noConf = MyRow["noSJ"].ToString();

                                ObjDb.ExecQuery("UPDATE tConfDO_H SET sts = 0 WHERE noConf = '" + noConf + "'");
                            }

                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noInv", itemId);
                            ObjDb.Delete("tInvoiceSO_D", ObjDb.Where);
                            ObjDb.Delete("tInvoiceSO_H", ObjDb.Where);
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

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (valid == true)
            {
                try
                {
                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noInv", hdnId.Value);
                    ObjDb.Data.Add("tglInv", dtSO.Text);
                    ObjDb.Data.Add("term", txtTOP.Text);
                    ObjDb.Data.Add("KeteranganInv", txtRemarks.Text);
                    ObjDb.Data.Add("nilaiPajakInv", Convert.ToDecimal(txtPPN.Text).ToString());
                    ObjDb.Data.Add("DiskonH", Convert.ToDecimal(txtDiscountH.Text).ToString());
                    ObjDb.Data.Add("grossInv", Convert.ToDecimal(txtSubTotal.Text).ToString());
                    ObjDb.Data.Add("DPP", Convert.ToDecimal(txtSubTotal.Text).ToString());
                    ObjDb.Data.Add("grossInvRp", Convert.ToDecimal(txtSubTotal.Text).ToString());
                    ObjDb.Data.Add("DPPRp", Convert.ToDecimal(txtSubTotal.Text).ToString());
                    ObjDb.Data.Add("netInv", Convert.ToDecimal(txtTotal.Text).ToString());
                    ObjDb.Data.Add("netInvRp", Convert.ToDecimal(txtTotal.Text).ToString());
                    ObjDb.Update("tInvoiceSO_H", ObjDb.Data, ObjDb.Where);

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diupdate.");
                    showHideForm(true, false);
                    LoadData();
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            showHideForm(true, false);
            LoadData();
        }
    }
}