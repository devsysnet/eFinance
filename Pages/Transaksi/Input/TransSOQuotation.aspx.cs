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

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransSOQuotation : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtSO.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                dtPO.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                LoadData();
                loadDataProduct();

                SetInitialRow();
                for (int g = 0; g < grdSOD.Rows.Count; g++)
                {
                    DropDownList cboExpiredDate = grdSOD.Rows[g].FindControl("cboExpiredDate") as DropDownList;
                    CheckBox chkDefault = grdSOD.Rows[g].FindControl("chkDefault") as CheckBox;

                    if (cboExpiredDate.Text != "")
                        chkDefault.Checked = true;
                    else
                        chkDefault.Checked = false;

                    if (chkDefault.Checked == true)
                        cboExpiredDate.Enabled = false;
                    else
                        cboExpiredDate.Enabled = true;
                }
                for (int i = 0; i < 4; i++)
                {
                    //AddNewRow();
                }
            }
        }

        #region loadData
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdSO.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataSO", ObjGlobal.Param);
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
            dt.Columns.Add(new DataColumn("hdnleadtime", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnUnit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtProductName", typeof(string)));
            dt.Columns.Add(new DataColumn("lblPacking", typeof(string)));
            dt.Columns.Add(new DataColumn("dtEstimasi", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQty", typeof(string)));
            dt.Columns.Add(new DataColumn("lblUnit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtUnitPrice", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTotal", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnQouationD", typeof(string)));

            //decimal hasil = 0;
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noQuotation", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataSO_D", ObjGlobal.Param);
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtProduct"] = myRow["prodno"].ToString();
                dr["hdnProduct"] = myRow["noproduct"].ToString();
                dr["hdnleadtime"] = myRow["leadtime"].ToString();
                dr["hdnUnit"] = myRow["punit"].ToString();
                dr["txtProductName"] = myRow["namaItem"].ToString();
                dr["lblPacking"] = myRow["packing"].ToString();
                dr["dtEstimasi"] = Convert.ToDateTime(myRow["tglQoutation"]).ToString("dd-MMM-yyyy");
                dr["txtQty"] = ObjSys.IsFormatNumber(myRow["qty"].ToString());
                dr["lblUnit"] = myRow["punit"].ToString();
                dr["txtUnitPrice"] = ObjSys.IsFormatNumber(myRow["hargaSatuan"].ToString());
                dr["txtTotal"] = ObjSys.IsFormatNumber(myRow["subTotal"].ToString());
                dr["hdnQouationD"] = myRow["noQuotationD"].ToString();

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtProduct"] = string.Empty;
                dr["hdnProduct"] = string.Empty;
                dr["hdnleadtime"] = string.Empty;
                dr["hdnUnit"] = string.Empty;
                dr["txtProductName"] = string.Empty;
                dr["lblPacking"] = string.Empty;
                dr["dtEstimasi"] = string.Empty;
                dr["txtQty"] = string.Empty;
                dr["lblUnit"] = string.Empty;
                dr["txtUnitPrice"] = string.Empty;
                dr["txtTotal"] = string.Empty;
                dr["hdnQouationD"] = string.Empty;

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdSOD.DataSource = dt;
            grdSOD.DataBind();

            for (int i = 0; i < grdSOD.Rows.Count; i++)
            {
                DropDownList cboExpiredDate = grdSOD.Rows[i].FindControl("cboExpiredDate") as DropDownList;
                string kdProd = dt.Rows[i]["txtProduct"].ToString();
                setExpiredDate(cboExpiredDate, kdProd);
            }

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
                        HiddenField hdnleadtime = (HiddenField)grdSOD.Rows[i].FindControl("hdnleadtime");
                        HiddenField hdnUnit = (HiddenField)grdSOD.Rows[i].FindControl("hdnUnit");
                        TextBox txtProduct = (TextBox)grdSOD.Rows[i].FindControl("txtProduct");
                        TextBox txtProductName = (TextBox)grdSOD.Rows[i].FindControl("txtProductName");
                        Label lblPacking = (Label)grdSOD.Rows[i].FindControl("lblPacking");
                        TextBox dtEstimasi = (TextBox)grdSOD.Rows[i].FindControl("dtEstimasi");
                        TextBox txtUnitPrice = (TextBox)grdSOD.Rows[i].FindControl("txtUnitPrice");
                        //TextBox txtDiscount = (TextBox)grdSOD.Rows[i].FindControl("txtDiscount");
                        TextBox txtQty = (TextBox)grdSOD.Rows[i].FindControl("txtQty");
                        Label lblUnit = (Label)grdSOD.Rows[i].FindControl("lblUnit");
                        TextBox txtTotal = (TextBox)grdSOD.Rows[i].FindControl("txtTotal");
                        CheckBox chkDefault = grdSOD.Rows[i].FindControl("chkDefault") as CheckBox;
                        DropDownList cboExpiredDate = grdSOD.Rows[i].FindControl("cboExpiredDate") as DropDownList;
                        HiddenField hdnQouationD = (HiddenField)grdSOD.Rows[i].FindControl("hdnQouationD");

                        txtProduct.Text = dt.Rows[i]["txtProduct"].ToString();
                        hdnProduct.Value = dt.Rows[i]["hdnProduct"].ToString();
                        hdnleadtime.Value = dt.Rows[i]["hdnleadtime"].ToString();
                        hdnUnit.Value = dt.Rows[i]["hdnUnit"].ToString();
                        txtProductName.Text = dt.Rows[i]["txtProductName"].ToString();
                        lblPacking.Text = dt.Rows[i]["lblPacking"].ToString();
                        dtEstimasi.Text = dt.Rows[i]["dtEstimasi"].ToString();
                        txtQty.Text = dt.Rows[i]["txtQty"].ToString();
                        lblUnit.Text = dt.Rows[i]["lblUnit"].ToString();
                        txtUnitPrice.Text = dt.Rows[i]["txtUnitPrice"].ToString();
                        //txtDiscount.Text = dt.Rows[i]["txtDiscount"].ToString();
                        txtTotal.Text = dt.Rows[i]["txtTotal"].ToString();

                        if (cboExpiredDate.Text != "")
                            chkDefault.Checked = true;
                        else
                            chkDefault.Checked = false;

                        if (chkDefault.Checked == true)
                            cboExpiredDate.Enabled = false;
                        else
                            cboExpiredDate.Enabled = true;
                        hdnQouationD.Value = dt.Rows[i]["hdnQouationD"].ToString();
                    }
                }
            }
        }
        #endregion

        #region AddNewRow
        /*
        private void AddNewRow()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        HiddenField hdnProduct = (HiddenField)grdSOD.Rows[i].FindControl("hdnProduct");
                        HiddenField hdnUnit = (HiddenField)grdSOD.Rows[i].FindControl("hdnUnit");
                        TextBox txtProduct = (TextBox)grdSOD.Rows[i].FindControl("txtProduct");
                        TextBox txtProductName = (TextBox)grdSOD.Rows[i].FindControl("txtProductName");
                        Label lblPacking = (Label)grdSOD.Rows[i].FindControl("lblPacking");
                        TextBox dtEstimasi = (TextBox)grdSOD.Rows[i].FindControl("dtEstimasi");
                        TextBox txtUnitPrice = (TextBox)grdSOD.Rows[i].FindControl("txtUnitPrice");
                        TextBox txtQty = (TextBox)grdSOD.Rows[i].FindControl("txtQty");
                        Label lblUnit = (Label)grdSOD.Rows[i].FindControl("lblUnit");
                        TextBox txtTotal = (TextBox)grdSOD.Rows[i].FindControl("txtTotal");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["txtProduct"] = txtProduct.Text;
                        dtCurrentTable.Rows[i]["hdnProduct"] = hdnProduct.Value;
                        dtCurrentTable.Rows[i]["hdnUnit"] = hdnUnit.Value;
                        dtCurrentTable.Rows[i]["txtProductName"] = txtProductName.Text;
                        dtCurrentTable.Rows[i]["lblPacking"] = lblPacking.Text;
                        dtCurrentTable.Rows[i]["dtEstimasi"] = dtEstimasi.Text;
                        dtCurrentTable.Rows[i]["txtQty"] = txtQty.Text;
                        dtCurrentTable.Rows[i]["lblUnit"] = lblUnit.Text;
                        dtCurrentTable.Rows[i]["txtUnitPrice"] = txtUnitPrice.Text;
                        dtCurrentTable.Rows[i]["txtTotal"] = txtTotal.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdSOD.DataSource = dtCurrentTable;
                    grdSOD.DataBind();
                }
            }
            SetPreviousData();
        }
        */
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

        protected void grdSO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = grdSO.SelectedRow.RowIndex;
                string Id = grdSO.DataKeys[rowIndex].Values[0].ToString();
                string namaCust = grdSO.SelectedRow.Cells[2].Text;
                string alamatCust = grdSO.SelectedRow.Cells[3].Text;
                HiddenField hdnCust = (HiddenField)grdSO.Rows[rowIndex].FindControl("hdnCust");
                HiddenField hdnKdCust = (HiddenField)grdSO.Rows[rowIndex].FindControl("hdnKdCust");
                HiddenField hdnTelpCust = (HiddenField)grdSO.Rows[rowIndex].FindControl("hdnTelpCust");
                HiddenField hdnKreditLimit = (HiddenField)grdSO.Rows[rowIndex].FindControl("hdnKreditLimit");
                hdnId.Value = Id;

                //sales
                string qId = ((HiddenField)grdSO.Rows[rowIndex].FindControl("hdnQoutationId")).Value;


                hdnCustomer.Value = hdnCust.Value;
                txtCustomer.Text = namaCust;
                txtAddress.Text = alamatCust;
                txtPhone.Text = hdnTelpCust.Value;
                txtCreditLimit.Text = ObjSys.IsFormatNumber(hdnKreditLimit.Value);
                DataSet mySety = ObjDb.GetRows("select * from TransQuotation_H where noQuotation = '" + hdnId.Value + "'");
                DataRow myRowy = mySety.Tables[0].Rows[0];
                txtTOP.Text = myRowy["termSO"].ToString();
                //txtSubTotal.Text = ObjSys.IsFormatNumber(myRowy["nilaiNet"].ToString());
                //txtPPN.Text = ObjSys.IsFormatNumber(myRowy["nilaiPajak"].ToString());
                //txtTotalx.Text = ObjSys.IsFormatNumber(myRowy["gross"].ToString());
                txtDeliveryAddress.Text = myRowy["keterangan"].ToString();
                LoadDataPICName();
                LoadDataPICEmail();
                cboPICName.Text = myRowy["PIC"].ToString();
                txtEmailCP.Text = myRowy["PICEmail"].ToString();
                string noSales = myRowy["noSales"].ToString();

                DataSet mySetx = ObjDb.GetRows("select noUser id, namauser name FROM mUser where stsUser = '1' and noUser = '" + noSales + "' union all SELECT distinct noUser id, namauser name FROM mUser u where stsUser = '1' and EXISTS(SELECT 1 FROM tAkses t WHERE noAkses IN('3', '9', '12') AND u.noUser = t.noUser)");
                DataRow myRowx = mySetx.Tables[0].Rows[0];
                txtSales.Text = myRowx["name"].ToString();

                DataSet mySet = ObjDb.GetRows("select * from mCustomer where noCust = '" + hdnCust.Value + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];

                hdnDelivery.Value = myRow["noCust"].ToString();
                txtDelivery.Text = myRow["namaCust"].ToString();

                //DataSet mySet_piutang = ObjDb.GetRows("select coalesce(sum(nSaldoPiutang),0) as SaldoPiutang from tPiutang where noCus = '" + hdnCustomer.Value + "'");
                //DataRow myRow_piutang = mySet_piutang.Tables[0].Rows[0];
                txtBalance.Text = ObjSys.IsFormatNumber("0");

                decimal a = 0, b = 0, hasil = 0;
                a = Convert.ToDecimal(txtCreditLimit.Text) * 1;
                b = Convert.ToDecimal(txtBalance.Text) * 1;
                hasil = a - b;

                txtRemaining.Text = ObjSys.IsFormatNumber(hasil.ToString());
                SetInitialRow(hdnId.Value);
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
                showHideForm(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
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
                txtCustomer.Text = kdCust;
                txtAddress.Text = alamatCust;
                txtPhone.Text = noTelp.Value;
                txtCreditLimit.Text = Convert.ToDecimal(hdnKreditLimit.Value).ToString();

                DataSet mySet = ObjDb.GetRows("select * from mCustomer where noCust = '" + hdnCustomer.Value + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];

                hdnDelivery.Value = myRow["noCust"].ToString();
                txtDelivery.Text = myRow["namaCust"].ToString();
                txtDeliveryAddress.Text = myRow["alamatCust"].ToString();

                DataSet mySet_piutang = ObjDb.GetRows("select coalesce(sum(nSaldoPiutang),0) as SaldoPiutang from tPiutang where noCus = '" + hdnCustomer.Value + "'");
                DataRow myRow_piutang = mySet_piutang.Tables[0].Rows[0];
                txtBalance.Text = ObjSys.IsFormatNumber(myRow_piutang["SaldoPiutang"].ToString());

                decimal a = 0, b = 0, hasil = 0;
                a = Convert.ToDecimal(txtCreditLimit.Text) * 1;
                b = Convert.ToDecimal(txtBalance.Text) * 1;
                hasil = a - b;

                txtRemaining.Text = ObjSys.IsFormatNumber(hasil.ToString());

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

        protected void setExpiredDate(DropDownList cboExpiredDate, string kdProd)
        {
            cboExpiredDate.DataSource = ObjDb.GetRows("select distinct CONVERT(VARCHAR, expired, 106 ) as expired from tSaldoAging t join Product p on t.noproduct = p.noproduct where p.prodno = '" + kdProd + "' order by expired asc");
            cboExpiredDate.DataValueField = "expired";
            cboExpiredDate.DataTextField = "expired";
            cboExpiredDate.DataBind();
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
                setExpiredDate(cboExpiredDate, kdProd);

                //int cek = 0;
                //for (int i = 0; i < grdSOD.Rows.Count; i++)
                //{
                //    TextBox kdProdPO = (TextBox)grdSOD.Rows[i].Cells[1].FindControl("txtProduct");
                //    if (kdProd == kdProdPO.Text)
                //        cek += 1;
                //}
                //if (cek > 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //    ShowMessage("error", "Kode Product tidak boleh sama !");
                //}
                //else
                //{
                hdnProduct.Value = Id;
                txtKdProd.Text = kdProd;
                txtProductName.Text = namaProd;
                hdnUnit.Value = PickUnit;

                txtSearch.Text = "";
                loadDataProduct();
                dlgProduct.Hide();
                //}
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
            txtBalance.Text = "";
            txtCreditLimit.Text = "";
            txtCustomer.Text = "";
            txtCustomerPOref.Text = "";
            txtDelivery.Text = "";
            txtDeliveryAddress.Text = "";
            txtPhone.Text = "";
            txtRemaining.Text = "";
            txtRemarks.Text = "";
            txtSearch.Text = "";
            txtSearchCust.Text = "";
            txtSearchProduct.Text = "";
            txtTOP.Text = "";
            cboPICName.Text = "";
            txtEmailCP.Text = "";
            dtSO.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            dtPO.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            hdnCustomer.Value = "";
            hdnDelivery.Value = "";
            hdnId.Value = "";
            hdnParameterProd.Value = "";
            //cboCurrency.Text = "0";
            //cboPayment.Text = "0";
            txtSales.Text = "";

            SetInitialRow();
            for (int i = 1; i < 5; i++)
            {
                //AddNewRow();
            }
            LoadData();
            showHideForm(true, false);
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

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            //AddNewRow();
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
            ObjDb.Data.Clear();

            if (dtSO.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal SO tidak boleh kosong.");
                valid = false;
            }
            if (txtCustomer.Text == "")
            {
                message += ObjSys.CreateMessage("Customer tidak boleh kosong.");
                valid = false;
            }
            if (txtCustomerPOref.Text == "")
            {
                message += ObjSys.CreateMessage("Customer PO ref no tidak boleh kosong.");
                valid = false;
            }
            if (dtPO.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal PO tidak boleh kosong.");
                valid = false;
            }
            int count = 0;
            for (int g = 0; g < grdSOD.Rows.Count; g++)
            {
                CheckBox check = (CheckBox)grdSOD.Rows[g].FindControl("chkSOD");

                if (check.Checked != false)
                {
                    count += 1;
                }
            }
            if (count == 0)
            {
                message += ObjSys.CreateMessage("Data detil belum di Pilih");
                valid = false;
            }
            try
            {
                if (valid == true)
                {
                    decimal subTotal = 0;
                    for (int i = 0; i < grdSOD.Rows.Count; i++)
                    {
                        CheckBox chkSOD = (CheckBox)grdSOD.Rows[i].FindControl("chkSOD");
                        TextBox txtTotal = (TextBox)grdSOD.Rows[i].FindControl("txtTotal");
                        if (chkSOD.Checked == true)
                        {
                            subTotal += Convert.ToDecimal(txtTotal.Text);
                        }
                    }

                    decimal SubTotal1x = 0, PPN1x = 0, Total1x = 0;
                    SubTotal1x = Convert.ToDecimal(subTotal);
                    PPN1x = Convert.ToDecimal((subTotal * 10) / 100);
                    Total1x = SubTotal1x + PPN1x;
                    //HiddenField hdnProducts = (HiddenField)grdSOD.Rows[0].FindControl("hdnProduct");
                    //DataSet mySetz = ObjDb.GetRows("select * from product where noproduct = '" + hdnProducts.Value + "'");
                    //DataRow myRowz = mySetz.Tables[0].Rows[0];
                    //string typeProduct = myRowz["type"].ToString();
                    string Kode = "";
                    string fileName = "";
                   
                        Kode = ObjSys.GetCodeAutoNumberNew("36", Convert.ToDateTime(dtSO.Text).ToString("yyyy-MM-dd"));
                    ObjDb.Where.Clear();

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kdSO", Kode);
                    ObjGlobal.Param.Add("tglSO", Convert.ToDateTime(dtSO.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("noCust", hdnCustomer.Value);
                    ObjGlobal.Param.Add("Term", txtTOP.Text);
                    ObjGlobal.Param.Add("keterangan", txtRemarks.Text);
                    //ObjGlobal.Param.Add("Pembayaran", cboPayment.Text);
                    ObjGlobal.Param.Add("alamatPengirim", txtDeliveryAddress.Text);
                    //ObjGlobal.Param.Add("noMataUang", cboCurrency.Text);
                    ObjGlobal.Param.Add("salesman", "");// cboSales.SelectedItem.Text);
                    ObjGlobal.Param.Add("noPO", txtCustomerPOref.Text);
                    ObjGlobal.Param.Add("Total", Convert.ToDecimal(SubTotal1x).ToString());
                    ObjGlobal.Param.Add("Gross", Convert.ToDecimal(SubTotal1x).ToString());
                    ObjGlobal.Param.Add("NilaiPajak", Convert.ToDecimal(PPN1x).ToString());
                    ObjGlobal.Param.Add("nilaiNet", Convert.ToDecimal(Total1x).ToString());
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("noQuotation", hdnId.Value);
                    ObjGlobal.Param.Add("tglPORef", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("PIC", cboPICName.Text);
                    ObjGlobal.Param.Add("PICEmail", txtEmailCP.Text);
                    ObjGlobal.GetDataProcedure("SPInsertSO1", ObjGlobal.Param);

              
                        ObjSys.UpdateAutoNumberCodeNew("36", Convert.ToDateTime(dtSO.Text).ToString("yyyy-MM-dd"));


                    DataSet mySet = ObjDb.GetRows("select * from hSO where kdSO = '" + Kode + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    string noSO = myRow["noSO"].ToString();

                    for (int i = 0; i < grdSOD.Rows.Count; i++)
                    {
                        HiddenField hdnProduct = (HiddenField)grdSOD.Rows[i].FindControl("hdnProduct");
                        HiddenField hdnQouationD = (HiddenField)grdSOD.Rows[i].FindControl("hdnQouationD");
                        HiddenField hdnUnit = (HiddenField)grdSOD.Rows[i].FindControl("hdnUnit");
                        TextBox txtProduct = (TextBox)grdSOD.Rows[i].FindControl("txtProduct");
                        TextBox txtProductName = (TextBox)grdSOD.Rows[i].FindControl("txtProductName");
                        TextBox txtQty = (TextBox)grdSOD.Rows[i].FindControl("txtQty");
                        TextBox txtUnitPrice = (TextBox)grdSOD.Rows[i].FindControl("txtUnitPrice");
                        TextBox txtDiscount = (TextBox)grdSOD.Rows[i].FindControl("txtDiscount");
                        TextBox dtEstimasi = (TextBox)grdSOD.Rows[i].FindControl("dtEstimasi");
                        DropDownList cboExpiredDate = grdSOD.Rows[i].FindControl("cboExpiredDate") as DropDownList;
                        CheckBox chkDefault = grdSOD.Rows[i].FindControl("chkDefault") as CheckBox;
                        CheckBox chkSOD = grdSOD.Rows[i].FindControl("chkSOD") as CheckBox;

                        if (chkSOD.Checked == true)
                        {
                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("noSO", noSO);
                            ObjGlobal.Param.Add("noQouationD", hdnQouationD.Value);
                            ObjGlobal.Param.Add("tglEstimasi", Convert.ToDateTime(dtEstimasi.Text).ToString("yyyy-MM-dd"));
                            if (chkDefault.Checked)
                            {
                                if (cboExpiredDate.Text == "")
                                    ObjGlobal.Param.Add("EXPIRED", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                                else
                                    ObjGlobal.Param.Add("EXPIRED", Convert.ToDateTime(cboExpiredDate.SelectedItem.Text).ToString("yyyy-MM-dd"));

                                ObjGlobal.Param.Add("isExpiredDefault", "Y");
                            }
                            else
                            {
                                ObjGlobal.Param.Add("EXPIRED", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                                ObjGlobal.Param.Add("isExpiredDefault", "N");
                            }

                            ObjGlobal.Param.Add("noQuotation", hdnId.Value);
                            ObjGlobal.GetDataProcedure("SPInsertSOD", ObjGlobal.Param);
                        }

                    }

                    if (flUpload.HasFile == true)
                    {
                        ObjDb.Where.Add("noSO", noSO);
                        ObjDb.Data.Add("uploadDocPO", flUpload.FileName);
                        fileName = flUpload.FileName;
                        flUpload.SaveAs(Server.MapPath("~/Assets/DocPOinSO/" + flUpload.FileName));
                        ObjDb.Update("hSO", ObjDb.Data, ObjDb.Where);

                    }
                    LoadData();
                    showHideForm(true, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate()", "Calculate();", true);
                    ShowMessage("error", message);
                    LoadCalculate();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
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
        }

        protected void chkDefault_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkDefault = sender as CheckBox;
            GridViewRow row = chkDefault.NamingContainer as GridViewRow;
            if (row != null)
            {
                int rowIndex = row.RowIndex;
                DropDownList cboExpiredDate = grdSOD.Rows[rowIndex].FindControl("cboExpiredDate") as DropDownList;
                cboExpiredDate.Enabled = !chkDefault.Checked;
                if (!cboExpiredDate.Enabled)
                {
                    try
                    {
                        cboExpiredDate.SelectedIndex = 0;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        protected void TglSO_SelectionChanged(object sender, EventArgs e)
        {
            dtSO.Text = TglSO.SelectedDate.ToString("dd-MMM-yyyy");
            TglSO.Visible = false;
            DateTime tanggalAkhir = DateTime.ParseExact(dtSO.Text, "dd-MMM-yyyy", null);
            for (int i = 0; i < grdSOD.Rows.Count; i++)
            {
                HiddenField hdnleadtime = (HiddenField)grdSOD.Rows[i].FindControl("hdnleadtime");
                TextBox dtEstimasi = (TextBox)grdSOD.Rows[i].FindControl("dtEstimasi");

                dtEstimasi.Text = tanggalAkhir.AddDays(Convert.ToDouble(hdnleadtime.Value)).ToString("dd-MMM-yyyy");
            }
        }

        protected void lnkPickDate_Click(object sender, EventArgs e)
        {
            TglSO.Visible = true;
        }

        protected void LoadCalculate()
        {
            decimal subTotal = 0;
            for (int i = 0; i < grdSOD.Rows.Count; i++)
            {
                CheckBox chkSOD = (CheckBox)grdSOD.Rows[i].FindControl("chkSOD");
                TextBox txtTotal = (TextBox)grdSOD.Rows[i].FindControl("txtTotal");
                if (chkSOD.Checked == true)
                {
                    subTotal += Convert.ToDecimal(txtTotal.Text);
                }
            }

            txtSubTotal.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(subTotal).ToString());
            txtPPN.Text = ObjSys.IsFormatNumber((Convert.ToDecimal(subTotal) * 10 / 100).ToString());
            txtTotalx.Text = ObjSys.IsFormatNumber((Convert.ToDecimal(subTotal) + Convert.ToDecimal(txtPPN.Text)).ToString());

        }

        protected void btnBrowseAlamatKirim_Click(object sender, ImageClickEventArgs e)
        {
            DataTable myData = ObjDb.GetRowsDataTable("select distinct alamat from mAlamatkirim where nocust='" + hdnCustomer.Value + "'");
            grdDataAlamatKirim.DataSource = myData;
            grdDataAlamatKirim.DataBind();
            dlgDelivery.Show();
        }

        protected void grdDataAlamatKirim_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = grdDataAlamatKirim.SelectedRow.RowIndex;
            txtDeliveryAddress.Text = grdDataAlamatKirim.Rows[rowIndex].Cells[0].Text;
        }

        protected void cboPICName_TextChanged(object sender, EventArgs e)
        {
            LoadDataPICEmail();
        }

        protected void LoadDataPICEmail()
        {
            string email = "";
            DataSet mySetH = ObjDb.GetRows("select mailCP from mCustomerCP where namaCP = '" + cboPICName.Text + "'");
            if (mySetH.Tables[0].Rows.Count > 0)
            {
                DataRow myRowH = mySetH.Tables[0].Rows[0];
                email = myRowH["mailCP"].ToString();
            }
            else
            {
                email = "";
            }

            txtEmailCP.Text = email;
        }

        protected void LoadDataPICName()
        {
            cboPICName.DataSource = ObjDb.GetRows("select a.* from (select '' id,'---' name union all SELECT distinct namaCP id, namaCP name FROM mcustomerCP where noCust = '" + hdnCustomer.Value + "' ) a");
            cboPICName.DataValueField = "id";
            cboPICName.DataTextField = "name";
            cboPICName.DataBind();
        }
    }
}