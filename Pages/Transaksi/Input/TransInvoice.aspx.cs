using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransInvoice : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtSO.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                LoadData();
                //loadDataTax();
            }
        }

        protected void loadDataTax()
        {
            string persenPPn = "0.00";
            string sql = "select * from Parameter";
            DataSet mySet = ObjDb.GetRows(sql);
            if (mySet.Tables[0].Rows.Count > 0)
            {
                persenPPn = mySet.Tables[0].Rows[0]["persenPajak"].ToString();
            }
            lblPersenPPn.Text = persenPPn;
            hdnPersenPPn.Value = persenPPn;
        }

        #region loadData
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdSO.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataInvoice", ObjGlobal.Param);
            grdSO.DataBind();
        }

        protected void loadDataProduct()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdProduct.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataProduct", ObjGlobal.Param);
            grdProduct.DataBind();
        }

        protected void loadDataCustomer()
        {
            grdCustomer.DataSource = ObjDb.GetRows("select * from mCustomer where stsCust = '1'");
            grdCustomer.DataBind();
        }

        protected void loadDataCustomerSearch()
        {
            grdCustomer.DataSource = ObjDb.GetRows("select x.* from (select a.noCust, a.kdCust, a.namaCust, case when a.alamatCust is null then '-' else a.alamatCust end alamatCust from mCustomer a where a.stsCust = '1')x where(x.kdCust like '%" + txtSearchCust.Text + "%' or x.namaCust like '%" + txtSearchCust.Text + "%' or x.alamatCust like '%" + txtSearchCust.Text + "%') ");
            grdCustomer.DataBind();
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
            dt.Columns.Add(new DataColumn("txtQtyBesar", typeof(string)));
            dt.Columns.Add(new DataColumn("lblUnitBesar", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQtyBesar1", typeof(string)));
            dt.Columns.Add(new DataColumn("lblUnitBesar1", typeof(string)));
            //dt.Columns.Add(new DataColumn("hdnnoconfD", typeof(string)));


            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noQuotation", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDatainv_D", ObjGlobal.Param);
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtProduct"] = myRow["prodno"].ToString();
                dr["hdnProduct"] = myRow["noproduct"].ToString();
                dr["hdnUnit"] = myRow["punit"].ToString();
                dr["txtProductName"] = myRow["namaItem"].ToString();
                dr["txtQtyBesar"] = ObjSys.IsFormatNumber(myRow["qtySatuanBesarConf"].ToString());
                dr["txtQty"] = ObjSys.IsFormatNumber(myRow["qtyconf"].ToString());
                dr["lblUnit"] = myRow["punit"].ToString();
                dr["lblUnitBesar"] = myRow["satuanBesar"].ToString();
                dr["txtQtyBesar1"] = ObjSys.IsFormatNumber(myRow["qtySatuanBesar1Conf"].ToString());
                dr["lblUnitBesar1"] = myRow["satuanBesar1"].ToString();
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
                dr["txtQtyBesar"] = string.Empty;
                dr["txtQty"] = string.Empty;
                dr["lblUnit"] = string.Empty;
                dr["lblUnitBesar"] = string.Empty;
                dr["txtQtyBesar1"] = string.Empty;
                dr["lblUnitBesar1"] = string.Empty;
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
                        Label lblUnitBesar = (Label)grdSOD.Rows[i].FindControl("lblUnitBesar");
                        Label lblUnitBesar1 = (Label)grdSOD.Rows[i].FindControl("lblUnitBesar1");
                        TextBox txtQtyBesar = (TextBox)grdSOD.Rows[i].FindControl("txtQtyBesar");
                        TextBox txtQtyBesar1 = (TextBox)grdSOD.Rows[i].FindControl("txtQtyBesar1");
                        TextBox txtQty = (TextBox)grdSOD.Rows[i].FindControl("txtQty");
                        Label lblUnit = (Label)grdSOD.Rows[i].FindControl("lblUnit");
                        CheckBox chkDefault = grdSOD.Rows[i].FindControl("chkDefault") as CheckBox;
                        //HiddenField hdnnoconfD = (HiddenField)grdSOD.Rows[i].FindControl("hdnnoconfD");

                        txtProduct.Text = dt.Rows[i]["txtProduct"].ToString();
                        hdnProduct.Value = dt.Rows[i]["hdnProduct"].ToString();
                        hdnUnit.Value = dt.Rows[i]["hdnUnit"].ToString();
                        txtProductName.Text = dt.Rows[i]["txtProductName"].ToString();
                        lblUnitBesar.Text = dt.Rows[i]["lblUnitBesar"].ToString();
                        txtQty.Text = dt.Rows[i]["txtQty"].ToString();
                        lblUnit.Text = dt.Rows[i]["lblUnit"].ToString();
                        txtQtyBesar.Text = dt.Rows[i]["txtQtyBesar"].ToString();
                        txtQtyBesar1.Text = dt.Rows[i]["txtQtyBesar1"].ToString();
                        lblUnitBesar1.Text = dt.Rows[i]["lblUnitBesar1"].ToString();

                    }
                }
            }
        }

        #endregion

        #region Select & Pagging
        protected void grdSOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataProduct();
            string value = (grdSOD.SelectedRow.FindControl("hdnParameter") as HiddenField).Value;
            hdnParameterProd.Value = value;
            dlgProduct.Show();
        }

        protected void grdSO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSO.PageIndex = e.NewPageIndex;
            LoadData();
        }


        protected void grdCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCustomer.PageIndex = e.NewPageIndex;
            loadDataCustomer();
            dlgCustomer.Show();
        }

        protected void grdCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = grdCustomer.SelectedRow.RowIndex;
                string Id = grdCustomer.DataKeys[rowIndex].Values[0].ToString();
                string kdCust = grdCustomer.SelectedRow.Cells[1].Text;
                string namaCust = grdCustomer.SelectedRow.Cells[2].Text;
                string alamatCust = grdCustomer.SelectedRow.Cells[3].Text;
                HiddenField noTelp = (HiddenField)grdCustomer.Rows[rowIndex].FindControl("hdnTelp");
                HiddenField hdnKreditLimit = (HiddenField)grdCustomer.Rows[rowIndex].FindControl("hdnKreditLimit");

                hdnCustomer.Value = Id;
                txtAddress.Text = alamatCust;
                txtPhone.Text = noTelp.Value;


                DataSet mySet = ObjDb.GetRows("select * from mCustomer where noCust = '" + hdnCustomer.Value + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];

                hdnDelivery.Value = myRow["noCust"].ToString();
                txtDeliveryAddress.Text = myRow["alamatCust"].ToString();


                loadDataCustomer();
                dlgCustomer.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProduct.PageIndex = e.NewPageIndex;
            loadDataProduct();
            dlgProduct.Show();
        }


        protected void grdProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(hdnParameterProd.Value);

                string Id = (grdProduct.SelectedRow.FindControl("hdnNoProd") as HiddenField).Value;
                string kdProd = (grdProduct.SelectedRow.FindControl("lblKdProd") as Label).Text;
                string namaProd = (grdProduct.SelectedRow.FindControl("lblNamaProd") as Label).Text;
                string PickUnit = (grdProduct.SelectedRow.FindControl("lblPickUnit") as Label).Text;

                HiddenField hdnProduct = (HiddenField)grdSOD.Rows[rowIndex - 1].FindControl("hdnProduct");
                HiddenField hdnUnit = (HiddenField)grdSOD.Rows[rowIndex - 1].FindControl("hdnUnit");
                TextBox txtKdProd = (TextBox)grdSOD.Rows[rowIndex - 1].FindControl("txtProduct");
                TextBox txtProductName = (TextBox)grdSOD.Rows[rowIndex - 1].FindControl("txtProductName");
                DropDownList cboExpiredDate = (DropDownList)grdSOD.Rows[rowIndex - 1].FindControl("cboExpiredDate");


                int cek = 0;
                for (int i = 0; i < grdSOD.Rows.Count; i++)
                {
                    TextBox kdProdPO = (TextBox)grdSOD.Rows[i].Cells[1].FindControl("txtProduct");
                    if (kdProd == kdProdPO.Text)
                        cek += 1;
                }
                if (cek > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Kode Product tidak boleh sama !");
                }
                else
                {
                    hdnProduct.Value = Id;
                    txtKdProd.Text = kdProd;
                    txtProductName.Text = namaProd;
                    hdnUnit.Value = PickUnit;

                    txtSearch.Text = "";
                    loadDataProduct();
                    dlgProduct.Hide();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
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

        protected void ClearData()
        {
            CloseMessage();

            txtAddress.Text = "";
            txtSubTotal.Text = "";
            txtCustomerPOref.Text = "";
            txtDeliveryAddress.Text = "";
            txtPPN.Text = "";
            txtPhone.Text = "";
            txtRemarks.Text = "";
            txtSearch.Text = "";
            txtSearchCust.Text = "";
            txtSearchProduct.Text = "";
            txtTotal.Text = "";
            dtSO.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            hdnCustomer.Value = "";
            hdnDelivery.Value = "";
            hdnId.Value = "";
            hdnParameterProd.Value = "";
            txtSales.Text = "";
            cbogabung.Text = "0";

        }

        protected void showHideForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        #endregion

        #region Button
        protected void btnSearchProduct_Click(object sender, EventArgs e)
        {
            loadDataProduct();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnSearchCust_Click(object sender, EventArgs e)
        {
            if (txtSearchCust.Text != "")
                loadDataCustomerSearch();
            else
                loadDataCustomer();
            dlgCustomer.Show();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (dtSO.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal Invoice tidak boleh kosong.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                    string Kode = "";
                    ObjDb.Data.Clear();
                    if (cbokode.Text == "1")
                        Kode = ObjSys.GetCodeAutoNumberNew("34", Convert.ToDateTime(dtSO.Text).ToString("yyyy-MM-dd"));
                    else
                        Kode = cbolistkodeold.Text;

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kdInv", Kode);
                    ObjGlobal.Param.Add("tglInv", Convert.ToDateTime(dtSO.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("noCust", hdnCustomer.Value);
                    ObjGlobal.Param.Add("Term", txtTOP.Text);
                    ObjGlobal.Param.Add("gabung", cbogabung.Text);
                    ObjGlobal.Param.Add("keterangan", txtRemarks.Text);
                    if (cboTax.Text == "1" && cboTaxNo.Text == "1")
                    {
                        ObjGlobal.Param.Add("taxid", cbolistkodetaxnew.Text);
                    }
                    else if (cboTax.Text == "1" && cboTaxNo.Text == "2")
                    {
                        ObjGlobal.Param.Add("taxid", cbolistkodetaxold.Text);
                    }
                    else
                    {
                        ObjDb.Data.Add("taxid", "");
                    }
                  ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("noQuotation", hdnId.Value);
                    ObjGlobal.GetDataProcedure("SPInsertInv", ObjGlobal.Param);

                    if (cbokode.Text == "1")
                    {
                        ObjSys.UpdateAutoNumberCode("34", Convert.ToDateTime(dtSO.Text).ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        ObjDb.Where.Clear();
                        ObjDb.Data.Clear();
                        ObjDb.Where.Add("jnsInv", "New");
                        ObjDb.Where.Add("kodeInvoice", cbolistkodeold.Text);
                        ObjDb.Delete("tTampKodeInvoice", ObjDb.Where);
                    }

                    DataSet mySet = ObjDb.GetRows("select noInv from tInvoiceSO_H where kdInv = '" + Kode + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    string noInv = myRow["noInv"].ToString();

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noInv", noInv);
                    ObjGlobal.Param.Add("noQuotation", hdnId.Value);
                    ObjGlobal.GetDataProcedure("SPInsertInvD", ObjGlobal.Param);

                    ClearData();
                    showHideForm(true, false);
                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                    loadkodetax();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.Message);
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
            showHideForm(true, false);
        }

        protected void btnImgCustomer_Click(object sender, ImageClickEventArgs e)
        {
            loadDataCustomer();
            dlgCustomer.Show();
        }
        #endregion

        protected void grdSO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detail")
            {
                try
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string itemId = grdSO.DataKeys[rowIndex].Value.ToString();

                    ObjDb.Where.Clear();
                    ObjDb.Data.Clear();
                    ObjDb.Where.Add("noQoutation", itemId);
                    ObjDb.Data.Add("stsQoutation", "5");
                    ObjDb.Update("hQoutation", ObjDb.Data, ObjDb.Where);

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
                    string Id = grdSO.DataKeys[rowIndex].Values[0].ToString();
                    HiddenField hdnCust = (HiddenField)grdSO.Rows[rowIndex].FindControl("hdnCust");
                    HiddenField hdnKdCust = (HiddenField)grdSO.Rows[rowIndex].FindControl("hdnKdCust");
                    HiddenField hdnalamatCust = (HiddenField)grdSO.Rows[rowIndex].FindControl("hdnalamatCust");
                    HiddenField hdnTelpCust = (HiddenField)grdSO.Rows[rowIndex].FindControl("hdnTelpCust");
                    HiddenField hdnKreditLimit = (HiddenField)grdSO.Rows[rowIndex].FindControl("hdnKreditLimit");
                    hdnId.Value = Id;

                    //sales
                    string qId = ((HiddenField)grdSO.Rows[rowIndex].FindControl("hdnQoutationId")).Value;
                    txtSales.Text = ObjDb.GetRows("select distinct kdConf as kdQoutation, nosales, namauser from tConfDO_H h inner join tDO_H a on h.nodo=a.nodo inner join hso h1 on h.noso=h1.noSO inner JOIN mUser u ON h1.nosales = u.noUser  where a.kddo = '" + qId + "'").Tables[0].Rows[0]["namauser"].ToString();

                    txtAddress.Text = hdnalamatCust.Value;
                    txtPhone.Text = hdnTelpCust.Value;

                    DataSet mySet = ObjDb.GetRows("select distinct h.kdConf as kdQoutation, a5.nosales, u.namacust, " +
                        "u.noCust,u.alamatCust,a5.nopo,a5.termSO,h1.nomatauang,v.namaMataUang,(select persenPajak from Parameter) as persenppn from tConfDO_H h " +
                        "inner join tDO_H a on h.nodo=a.nodo inner join hso h1 on h.noso=h1.noSO " +
                        "inner join tDO_D a1 on a1.noDO=a.noDO inner join tPicking_D a2 on a1.noPickingD=a2.noPickingD " +
                        "inner join dSO a3 on a2.noDetSO=a3.noDetSO inner join hSO a5 on a3.noSO=a5.noSO " +
                        "inner JOIN mcustomer u ON h1.nocust = u.nocust inner join mMataUang v on a5.nomatauang=v.nomatauang " +
                        "where u.noCust = '" + hdnCust.Value + "' and a.kddo='" + qId + "' ");
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    hdnCustomer.Value = myRow["noCust"].ToString();
                    txtnamacust.Text = myRow["namaCust"].ToString();
                    txtDeliveryAddress.Text = myRow["alamatCust"].ToString();
                    txtCustomerPOref.Text = myRow["nopo"].ToString();
                    txtTOP.Text = myRow["termSO"].ToString();
                    persenppn.Text = myRow["persenppn"].ToString();

                    loadkodeold();
                    loadkodetax();
                    loadDataTax();
                    cbolistkodeold.Visible = false;
                    cbolistkodetaxold.Visible = false;
                    cbokode.Text = "1";
                    cboTax.Text = "1";
                    cboTaxNo.Text = "1";

                    SetInitialRow(hdnId.Value);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
                    showHideForm(false, true);

                }
                catch (Exception ex)
                {
                    Response.Write("Error:" + ex.ToString());
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
                }
            }
        }

        protected void cbokode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbokode.Text == "2")
            {
                cbolistkodeold.Visible = true;
                loadkodeold();
            }
            else
            {
                cbolistkodeold.Visible = false;
            }
        }

        private void loadkodeold()
        {
            cbolistkodeold.DataSource = ObjDb.GetRows("select distinct kodeInvoice from tTampKodeInvoice where nocus='" + hdnCustomer.Value + "' and sts='0' ");
            cbolistkodeold.DataValueField = "kodeInvoice";
            cbolistkodeold.DataTextField = "kodeInvoice";
            cbolistkodeold.DataBind();
        }

        protected void cboTax_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTax.Text == "1")
            {
                if (cboTaxNo.Text == "1")
                {
                    cbolistkodetaxnew.Visible = true;
                    cbolistkodetaxold.Visible = false;
                    loadkodetax();
                }
                else
                {
                    cbolistkodetaxnew.Visible = false;
                    cbolistkodetaxold.Visible = true;
                    loadkodetaxold();
                }
            }
            else
            {
                cbolistkodetaxnew.Visible = false;
                cbolistkodetaxold.Visible = false;
            }

            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
        }

        private void loadkodetax()
        {
            cbolistkodetaxnew.DataSource = ObjDb.GetRows("select top 1 convert(varchar(30),noDepan)+'.'+convert(varchar(30),noPajak) as notax from TransNomorPajak where stsNomor='1' order by noUrutPajak");
            cbolistkodetaxnew.DataValueField = "notax";
            cbolistkodetaxnew.DataTextField = "notax";
            cbolistkodetaxnew.DataBind();
        }

        private void loadkodetaxold()
        {
            cbolistkodetaxold.DataSource = ObjDb.GetRows("select convert(varchar(30),noDepan)+'.'+convert(varchar(30),noPajak) as notax from TransNomorPajak where stsNomor='2' order by noUrutPajak");
            cbolistkodetaxold.DataValueField = "notax";
            cbolistkodetaxold.DataTextField = "notax";
            cbolistkodetaxold.DataBind();
        }

        protected void cboTaxNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTaxNo.Text == "1")
            {
                cbolistkodetaxold.Visible = false;
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
                if (cboTax.Text == "2")
                {
                    cbolistkodetaxold.Visible = false;
                }
                else
                {
                    cbolistkodetaxold.Visible = true;
                    loadkodetaxold();
                }

            }
        }
    }
}