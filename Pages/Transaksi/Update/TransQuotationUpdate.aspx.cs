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
    public partial class TransQuotationUpdate : System.Web.UI.Page
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
                loadkodetax();
                loadDataCombo();
                //dtInv.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
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
            grdInvoiceGen.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataViewQuotation", ObjGlobal.Param);
            grdInvoiceGen.DataBind();

        }
        protected void LoadDataPelanggan()
        {
            //DataTable myData = ObjDb.GetRowsDataTable("select * from mCustomer");
            //grdDataPelanggan.DataSource = myData;
            //grdDataPelanggan.DataBind();
        }
        protected void btnBrowseBank_Click(object sender, ImageClickEventArgs e)
        {
            //LoadDataBank();
            //dlgBank.PopupControlID = "panelAddDataBank";
            //dlgBank.Show();
        }
        protected void LoadDataBank()
        {
            //DataTable myData = ObjDb.GetRowsDataTable("select * from mBank");
            //grdBank.DataSource = myData;
            //grdBank.DataBind();
        }
        protected void btnBrowsePelanggan_Click(object sender, ImageClickEventArgs e)
        {
            //LoadDataPelanggan();
            //dlgAddData.PopupControlID = "panelAddDataPelanggan";
            //dlgAddData.Show();
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

            cboCurrencyTrans.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noMataUang id, namaMataUang name FROM mMataUang) a");
            cboCurrencyTrans.DataValueField = "id";
            cboCurrencyTrans.DataTextField = "name";
            cboCurrencyTrans.DataBind();
        }

       
        private void loadkodetax()
        {
            //cbolistkodetaxnew.DataSource = ObjDb.GetRows("select top 1 convert(varchar(30),noDepan)+'.'+convert(varchar(30),noPajak) as notax from TransNomorPajak where stsNomor='1' order by noUrutPajak");
            //cbolistkodetaxnew.DataValueField = "notax";
            //cbolistkodetaxnew.DataTextField = "notax";
            //cbolistkodetaxnew.DataBind();
        }


        #endregion
        protected void grdDataPelanggan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int selectedRow = grdDataPelanggan.SelectedIndex;
            //hdnNoCustomer.Value = grdDataPelanggan.Rows[selectedRow].Cells[0].Text;
            //txtNamaPelanggan.Text = grdDataPelanggan.Rows[selectedRow].Cells[1].Text;
            //txtAlamatPelanggan.Text = grdDataPelanggan.Rows[selectedRow].Cells[2].Text;
        }
        protected void grdDataBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int selectedRow = grdBank.SelectedIndex;
            //hdnNoBank.Value = grdBank.Rows[selectedRow].Cells[0].Text;
            //txtBankName.Text = grdBank.Rows[selectedRow].Cells[1].Text;
            //txtNoRek.Text = grdBank.Rows[selectedRow].Cells[2].Text;
        }
        
     
        #region setInitial & AddRow
        private void SetInitialRow(string Id = "0")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnNoQuotationD", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnProduct", typeof(string)));
            dt.Columns.Add(new DataColumn("txtProduct", typeof(string)));
            dt.Columns.Add(new DataColumn("txtProductName", typeof(string))); 
            dt.Columns.Add(new DataColumn("ddCIF", typeof(string)));
            dt.Columns.Add(new DataColumn("txtCIFValue", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQty", typeof(string)));
            dt.Columns.Add(new DataColumn("lblUnit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtUnitPrice", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTotal1", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataQuotationD", ObjGlobal.Param);
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnNoQuotationD"] = myRow["noDetQuotation"].ToString();
                dr["hdnProduct"] = myRow["noproduct"].ToString();
                dr["txtProduct"] = myRow["kodeBarang"].ToString();
                dr["txtProductName"] = myRow["namaItem"].ToString();
                dr["lblUnit"] = myRow["punit"].ToString();
                dr["ddCIF"] = myRow["pfactor"].ToString();
                dr["txtCIFValue"] = myRow["delivery"].ToString();
                dr["txtQty"] = ObjSys.IsFormatNumber(myRow["qty"].ToString());
                dr["txtUnitPrice"] = ObjSys.IsFormatNumber(myRow["hargaSatuan"].ToString());
                dr["txtTotal1"] = ObjSys.IsFormatNumber(myRow["nilaiNet"].ToString());
                
                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnNoQuotationD"] = string.Empty;
                dr["hdnProduct"] = string.Empty;
                dr["txtProduct"] = string.Empty;
                dr["txtProductName"] = string.Empty;
                dr["lblUnit"] = string.Empty;
                dr["ddCIF"] = string.Empty;
                dr["txtCIFValue"] = string.Empty;
                dr["txtQty"] = string.Empty;
                dr["txtUnitPrice"] = string.Empty;
                dr["txtTotal1"] = string.Empty;
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

                        TextBox txtProduct = (TextBox)grdSOD.Rows[i].FindControl("txtProduct");
                        TextBox txtProductName = (TextBox)grdSOD.Rows[i].FindControl("txtProductName");
                        DropDownList ddCIF = (DropDownList)grdSOD.Rows[i].FindControl("ddCIF");
                        TextBox txtCIFValue = (TextBox)grdSOD.Rows[i].FindControl("txtCIFValue");
                        TextBox txtQty = (TextBox)grdSOD.Rows[i].FindControl("txtQty");
                        TextBox lblUnit = (TextBox)grdSOD.Rows[i].FindControl("lblUnit");
                        TextBox txtUnitPrice = (TextBox)grdSOD.Rows[i].FindControl("txtUnitPrice");
                        TextBox txtTotal1 = (TextBox)grdSOD.Rows[i].FindControl("txtTotal1");
                        HiddenField hdnNoQuotationD = (HiddenField)grdSOD.Rows[i].FindControl("hdnNoQuotationD");
                        HiddenField hdnProduct = (HiddenField)grdSOD.Rows[i].FindControl("hdnProduct");

                        txtProduct.Text = dt.Rows[i]["txtProduct"].ToString();
                        txtProductName.Text = dt.Rows[i]["txtProductName"].ToString();
                        ddCIF.Text = dt.Rows[i]["ddCIF"].ToString();
                        txtCIFValue.Text = dt.Rows[i]["txtCIFValue"].ToString();
                        txtQty.Text = dt.Rows[i]["txtQty"].ToString();
                        lblUnit.Text = dt.Rows[i]["lblUnit"].ToString();
                        txtUnitPrice.Text = dt.Rows[i]["txtUnitPrice"].ToString();
                        txtTotal1.Text = dt.Rows[i]["txtTotal1"].ToString();
                        hdnProduct.Value = dt.Rows[i]["hdnProduct"].ToString();
                        hdnNoQuotationD.Value = dt.Rows[i]["hdnNoQuotationD"].ToString();

                    }
                }
            }
        }
        protected void grdPO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgButtonNewProduct = (ImageButton)e.Row.FindControl("imgButtonNewProduct");
                ImageButton imgButtonProduct = (ImageButton)e.Row.FindControl("imgButtonProduct");

                //if (cboType.Text != "5")
                //{
                imgButtonProduct.Visible = true;
                //imgButtonNewProduct.Visible = false;
                //}
                //else
                //{
                //  imgButtonProduct.Visible = false;
                //    imgButtonNewProduct.Visible = true;
                //}

            }
        }

        protected void grdPO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            txtHdnPopup.Value = rowIndex.ToString();
            if (e.CommandName == "Select")
            {
                TextBox txtProduct = (TextBox)grdSOD.Rows[rowIndex].FindControl("txtProduct");
                lblMessageError.Visible = false;
                txtSearch.Text = "";
                LoadDataProduct();
                mpe.Show();

            }
            else if (e.CommandName == "Clear")
            {
                TextBox txtProduct = (TextBox)grdSOD.Rows[rowIndex].FindControl("txtProduct");
                TextBox txtProductName = (TextBox)grdSOD.Rows[rowIndex].FindControl("txtProductName");
                DropDownList ddCIF = (DropDownList)grdSOD.Rows[rowIndex].FindControl("ddCIF");
                TextBox txtCIFValue = (TextBox)grdSOD.Rows[rowIndex].FindControl("txtCIFValue");
                TextBox txtQty = (TextBox)grdSOD.Rows[rowIndex].FindControl("txtQty");
                TextBox lblUnit = (TextBox)grdSOD.Rows[rowIndex].FindControl("lblUnit");
                TextBox txtUnitPrice = (TextBox)grdSOD.Rows[rowIndex].FindControl("txtUnitPrice");
                TextBox txtTotal1 = (TextBox)grdSOD.Rows[rowIndex].FindControl("txtTotal1");
                HiddenField hdnNoQuotationD = (HiddenField)grdSOD.Rows[rowIndex].FindControl("hdnNoQuotationD");
                HiddenField hdnProduct = (HiddenField)grdSOD.Rows[rowIndex].FindControl("hdnProduct");

                txtProduct.Text = "";
                txtProductName.Text = "";
                ddCIF.Text = "0";
                txtCIFValue.Text = "";
                txtQty.Text = "";
                lblUnit.Text = "";
                txtUnitPrice.Text = "0.00";
                txtTotal1.Text = "0.00";
                hdnNoQuotationD.Value = "";
                hdnProduct.Value = "";
                txtTotal.Text = "0.00";

                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);

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

       

        #region Select
        protected void grdInvoiceGen_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdInvoiceGen.PageIndex = e.NewPageIndex;
            LoadData();
        }
        protected void TglPO_SelectionChanged(object sender, EventArgs e)
        {
            dtPO.Text = TglPO.SelectedDate.ToString("dd-MMM-yyyy");
            TglPO.Visible = false;
            DateTime tanggalAkhir = DateTime.ParseExact(dtPO.Text, "dd-MMM-yyyy", null);
            dtQuoValidity.Text = tanggalAkhir.AddDays(7).ToString("dd-MMM-yyyy");
        }
        protected void cboCurrencyTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nilai = "0.00";
            DataSet mySetH = ObjDb.GetRows("select TOP 1 nilaiKursPajak from tKursPajak where noMatauang = '" + cboCurrencyTrans.SelectedValue + "'");
            if (mySetH.Tables[0].Rows.Count > 0)
            {
                DataRow myRowH = mySetH.Tables[0].Rows[0];
                nilai = myRowH["nilaiKursPajak"].ToString();
            }
            txtkursrate.Visible = true;
            Kurstrs2.Visible = true;
            txtkursrate.Text = ObjSys.IsFormatNumber(nilai);
        }
        protected void btnBrowseAlamatKirim_Click(object sender, ImageClickEventArgs e)
        {
            DataTable myData = ObjDb.GetRowsDataTable("select distinct alamat from mAlamatkirim where nocust='" + hdnNoCust.Value + "'");
            grdDataAlamatKirim.DataSource = myData;
            grdDataAlamatKirim.DataBind();
            mp1.PopupControlID = "panelAlamatKirim";
            mp1.Show();
        }
        protected void cboCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nilai = "0.00";
            DataSet mySetH = ObjDb.GetRows("select TOP 1 nilaiKursPajak from tKursPajak where noMatauang = '" + cboCurrency.SelectedValue + "'");
            if (mySetH.Tables[0].Rows.Count > 0)
            {
                DataRow myRowH = mySetH.Tables[0].Rows[0];
                nilai = myRowH["nilaiKursPajak"].ToString();
            }

            txtKurs.Visible = true;
            Kurstrs.Visible = true;
            txtKurs.Text = ObjSys.IsFormatNumber(nilai);

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
        protected void cboPICName_TextChanged(object sender, EventArgs e)
        {
            LoadDataPICEmail();
        }
        protected void lnkPickDate_Click(object sender, EventArgs e)
        {
            TglPO.Visible = true;
        }
        protected void loadDataCust()
        {
            grdCustomer.DataSource = ObjDb.GetRows("select * from mcustomer where namaCust LIKE '%" + TextBox2.Text + "%' or kdCust LIKE '%" + TextBox2.Text + "%'");
            grdCustomer.DataBind();
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            loadDataCust();
            mpe1.Show();
        }
        protected void grdKeterangan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnStsPilih = (HiddenField)e.Row.FindControl("hdnStsPilih");
                CheckBox chkCheck = (CheckBox)e.Row.FindControl("chkSelectSub");

                if (hdnStsPilih.Value == "1")
                    chkCheck.Checked = true;
                else
                    chkCheck.Checked = false;
            }
        }
        protected void UpdateChecked()
        {
            DataTable myData = (DataTable)ViewState["DataKet"];
            for (int i = 0; i < grdKeterangan.Rows.Count; i++)
            {
                CheckBox chkCheck = (CheckBox)grdKeterangan.Rows[i].FindControl("chkSelectSub");
                string itemId = grdKeterangan.DataKeys[i].Value.ToString();

                DataRow[] rowCek = myData.Select("noKeterangan='" + itemId + "'");
                if (chkCheck.Checked == true)
                    rowCek[0]["stsPilih"] = "1";
                else
                    rowCek[0]["stsPilih"] = "0";
                myData.AcceptChanges();
                myData.Rows[i].SetModified();

            }

            ViewState["DataKet"] = myData;
        }
        protected void grdProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            int rowIndexHdn = Convert.ToInt32(txtHdnPopup.Value);
            int rowIndex = grdProduct.SelectedRow.RowIndex;

            string prodno = "", prodnm = "", noprod = "", unit = "";
            prodno = (grdProduct.SelectedRow.FindControl("lblKodeReagent") as Label).Text;
            prodnm = (grdProduct.SelectedRow.FindControl("lblNamaReagent") as Label).Text;
            noprod = (grdProduct.SelectedRow.FindControl("hidNoReagent") as HiddenField).Value;
            unit = (grdProduct.SelectedRow.FindControl("lblPacking") as Label).Text;
            TextBox txtProduct = (TextBox)grdSOD.Rows[rowIndexHdn].FindControl("txtProduct");
            TextBox txtProductName = (TextBox)grdSOD.Rows[rowIndexHdn].FindControl("txtProductName");
            HiddenField hdnProduct = (HiddenField)grdSOD.Rows[rowIndexHdn].FindControl("hdnProduct");
            TextBox lblUnit = (TextBox)grdSOD.Rows[rowIndexHdn].FindControl("lblUnit");


            //DropDownList ddCIF = (DropDownList)grdPO.Rows[rowIndexHdn].FindControl("ddCIF");

            //tutup 12/03/19 boleh dobel product
            //for (int i = 0; i < grdPO.Rows.Count; i++)
            //{
            //    TextBox txtNoProduct = (TextBox)grdPO.Rows[i].Cells[1].FindControl("txtProduct");
            //    if (txtNoProduct.Text == prodno)
            //    {
            //        message += ObjSys.CreateMessage("Product : " + prodnm + " sudah terpilih.");
            //        valid = false;
            //    }
            //}

            if (valid == true)
            {
                //if (cboType.Text == "5")
                //{
                //    txtProduct.Text = prodno;
                //    txtProductName.Text = prodnm;
                //    txtOrigin.Text = origin;
                //    hdnProduct.Value = noprod;

                //    //lblUnit.Text = qty;
                //    setUnitData(lblUnit, ddCIF, prodno);
                //    lblMessageError.Visible = false;
                //    mpe.Hide();
                //}
                //else
                //{
                txtProduct.Text = prodno;
                txtProductName.Text = prodnm;
                hdnProduct.Value = noprod;
                lblUnit.Text = unit;
          
                //setUnitData(ddCIF, prodno);

                lblMessageError.Visible = false;
                mpe.Hide();
                //}

            }
            else
            {
                mpe.Show();
                lblMessageError.Text = ObjSys.GetMessage("error", message);
                lblMessageError.Visible = true;
            }


        }
        protected void grdDataAlamatKirim_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = grdDataAlamatKirim.SelectedRow.RowIndex;
            txtDeliveryAddress.Text = grdDataAlamatKirim.Rows[rowIndex].Cells[0].Text;

        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            loadDataCust();
            mpe1.Show();
        }
        protected void LoadDataPICName()
        {
            cboPICName.DataSource = ObjDb.GetRows("select a.* from (select '' id,'---' name union all SELECT distinct namaCP id, namaCP name FROM mcustomerCP where noCust = '" + hdnNoCust.Value + "' ) a");
            cboPICName.DataValueField = "id";
            cboPICName.DataTextField = "name";
            cboPICName.DataBind();

            txtEmailCP.Text = "";
        }
        protected void grdCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string namaCust = "", noCust = "", kodeCust = "", noFax = "", addres = "", topcust = "0", kredit = "0", sisakredit = "0";
            //kodeCust = (grdCust.SelectedRow.FindControl("lblKodeCust") as Label).Text;
            kodeCust = (grdCustomer.SelectedRow.FindControl("lblKodeReagent") as Label).Text;
            namaCust = (grdCustomer.SelectedRow.FindControl("lblNamaReagent") as Label).Text;
            noCust = (grdCustomer.SelectedRow.FindControl("hidNoReagent") as HiddenField).Value;
            noFax = (grdCustomer.SelectedRow.FindControl("hdnNoFax") as HiddenField).Value;
            addres = (grdCustomer.SelectedRow.FindControl("lblaAddress") as Label).Text;
            topcust = (grdCustomer.SelectedRow.FindControl("hdnTermCust") as HiddenField).Value;
            kredit = (grdCustomer.SelectedRow.FindControl("hdnKreditLimit") as HiddenField).Value;
            sisakredit = (grdCustomer.SelectedRow.FindControl("hdnSisaKreditLimit") as HiddenField).Value;

            //string sisaPiut = "0";
            //string sql = "select isnull(sum(nSaldopiutang),0) as sisapiutang from tpiutang where noCus= '" + noCust + "' ";
            //DataSet mySet = ObjDb.GetRows(sql);
            //if (mySet.Tables[0].Rows.Count > 0)
            //{
            //    sisaPiut = mySet.Tables[0].Rows[0]["sisapiutang"].ToString();
            //}

            string sisakreditlimit = "0";
            //sisakreditlimit = (Convert.ToDecimal(sisakredit) - Convert.ToDecimal(sisaPiut)).ToString();
            //txtKodeCust.Text = kodeCust;
            sisakreditlimit = Convert.ToDecimal(sisakredit).ToString();
            lblNamaCust.Text = namaCust;
            hdnNoCust.Value = noCust;
            txtIDCust.Text = kodeCust;
            lblPhoneFax.Text = noFax;
            lblAddress.Text = addres;
            txtTop.Text = topcust;
            lblkreditlimit.Text = ObjSys.IsFormatNumber(kredit).ToString();
            lblsisakreditlimit.Text = ObjSys.IsFormatNumber(sisakreditlimit).ToString();
            LoadDataPICName();

            mpe1.Hide();
        }
        private void LoadDataPanel()
        {
            grdSupp.DataSource = ObjDb.GetRows("select * from mSupplier a left join mSupplierCP b on a.noSup = b.noSup");
            grdSupp.DataBind();
        }
        protected void btnCari1_Click(object sender, EventArgs e)
        {
            LoadDataPanel();
        }
        protected void grdSupp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSupp.PageIndex = e.NewPageIndex;
            //LoadDataPanel();
            dlgSupplier.Show();
        }
        protected void grdSupp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string namaSup = "", nosup = "", kodeSup = "", noFax = "", addres = "", sp = "";
            kodeSup = (grdSupp.SelectedRow.FindControl("lblKodeSup") as Label).Text;
            namaSup = (grdSupp.SelectedRow.FindControl("lblNamaSup") as Label).Text;
            nosup = (grdSupp.SelectedRow.FindControl("hdnNoSupp") as HiddenField).Value;
            sp = (grdSupp.SelectedRow.FindControl("hdnNoSP") as HiddenField).Value;
            noFax = (grdSupp.SelectedRow.FindControl("hdnNoFax") as HiddenField).Value;
            //addres = (grdSupp.SelectedRow.FindControl("lblaAddress") as Label).Text;

            lblPhoneFax.Text = noFax;
            //lblAddress.Text = addres;
            dlgSupplier.Hide();
        }
        protected void grdCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCustomer.PageIndex = e.NewPageIndex;
            loadDataCust();
            mpe1.Show();
        }
        private void LoadDataProduct()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearch.Text);
            grdProduct.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadDataProductQuo", ObjGlobal.Param);
            grdProduct.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDataProduct();
            mpe.Show();
        }
        protected void btnSearch3_Click(object sender, EventArgs e)
        {
            LoadData();
            
        }
        protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProduct.PageIndex = e.NewPageIndex;
            LoadDataProduct();
            mpe.Show();
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            UpdateChecked();
            dlgKeterangan.Hide();

        }
        protected void grdKeterangan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UpdateChecked();
            grdKeterangan.PageIndex = e.NewPageIndex;
            dlgKeterangan.Show();
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
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataQuotationH", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];
                
                dtPO.Text = Convert.ToDateTime(myRow["tglQuotation"]).ToString("dd-MMM-yyyy");
                hdnNoCust.Value = myRow["noCust"].ToString();
                hdnCIF.Value = myRow["PIC"].ToString();
                lblNamaCust.Text = myRow["namaCust"].ToString();
                txtIDCust.Text = myRow["kdCust"].ToString();
                lblAddress.Text = myRow["alamatCust"].ToString();
                lblPhoneFax.Text = myRow["noTelpCust"].ToString();
                cboType.Text = myRow["tipe"].ToString();
                cboSales.Text = myRow["nosales"].ToString();
                txtDeliveryAddress.Text = myRow["AlamatKirim"].ToString();
                txtTop.Text = myRow["termSO"].ToString();
                cboPayment.Text = myRow["noTypePayment"].ToString();
                cboCurrency.Text = myRow["nomatauang"].ToString();
                txtKurs.Text = ObjSys.IsFormatNumber(myRow["kurs"].ToString());
                cboCurrencyTrans.Text = myRow["noMataUangRate"].ToString();
                txtkursrate.Text = ObjSys.IsFormatNumber(myRow["kursrate"].ToString());
                cboPICName.Text = myRow["PIC"].ToString();
                txtEmailCP.Text = myRow["PICEmail"].ToString();
                txtRemarks.Text = myRow["remark"].ToString();
                txtTotal.Text = ObjSys.IsFormatNumber(myRow["gross"].ToString());
                txtSubTotal.Text = ObjSys.IsFormatNumber(myRow["nilaiNet"].ToString());
                txtPPN.Text = ObjSys.IsFormatNumber(myRow["nilaiPajak"].ToString());
                //txtPPN.Text = ObjSys.IsFormatNumber(myRow["nilaiPajakInv"].ToString());

                dtQuoValidity.Text = Convert.ToDateTime(myRow["QuoValidityDate"]).ToString("dd-MMM-yyyy");
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
                            ObjDb.Where.Add("noQuotation", itemRow);
                            ObjDb.Delete("TransQuotation_D", ObjDb.Where);
                            ObjDb.Delete("TransQuotation_H", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdInvoiceGen.Rows)
                    {
                        string itemId = grdInvoiceGen.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdInvoiceGen.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {

                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noQuotation", itemId);
                            ObjDb.Delete("TransQuotation_D", ObjDb.Where);
                            ObjDb.Delete("TransQuotation_H", ObjDb.Where);
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
                    ObjDb.Where.Add("noQuotation", hdnId.Value);

                    ObjDb.Data.Add("tglQuotation", dtPO.Text);
                    ObjDb.Data.Add("nocust", hdnNoCust.Value);
                     ObjDb.Data.Add("tipe", cboType.Text);
                    ObjDb.Data.Add("nosales", cboSales.Text);
                    ObjDb.Data.Add("AlamatKirim", txtDeliveryAddress.Text);
                    ObjDb.Data.Add("termSO", txtTop.Text);
                    ObjDb.Data.Add("noTypePayment", cboPayment.Text);
                    ObjDb.Data.Add("remark", txtRemarks.Text);
                     ObjDb.Data.Add("QuoValidityDate", dtQuoValidity.Text);
                    ObjDb.Data.Add("nomatauang", cboCurrency.Text);
                    ObjDb.Data.Add("kurs", Convert.ToDecimal(txtKurs.Text).ToString());
                    ObjDb.Data.Add("noMataUangRate", cboCurrencyTrans.Text);
                    ObjDb.Data.Add("kursrate", Convert.ToDecimal(txtkursrate.Text).ToString());
                    ObjDb.Data.Add("Gross", Convert.ToDecimal(txtTotal.Text).ToString());
                    ObjDb.Data.Add("nilaiPajak", Convert.ToDecimal(txtPPN.Text).ToString());
                    ObjDb.Data.Add("nilaiNet", Convert.ToDecimal(txtSubTotal.Text).ToString());
                    ObjDb.Data.Add("modifiedBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modifiedDate", ObjSys.GetNow);
                    ObjDb.Update("TransQuotation_H", ObjDb.Data, ObjDb.Where);



                    for (int i = 0; i < grdSOD.Rows.Count; i++)
                    {

                        TextBox txtProduct = (TextBox)grdSOD.Rows[i].FindControl("txtProduct");
                        TextBox txtProductName = (TextBox)grdSOD.Rows[i].FindControl("txtProductName");
                        DropDownList ddCIF = (DropDownList)grdSOD.Rows[i].FindControl("ddCIF");
                        TextBox txtCIFValue = (TextBox)grdSOD.Rows[i].FindControl("txtCIFValue");
                        TextBox txtQty = (TextBox)grdSOD.Rows[i].FindControl("txtQty");
                        TextBox lblUnit = (TextBox)grdSOD.Rows[i].FindControl("lblUnit");
                        TextBox txtUnitPrice = (TextBox)grdSOD.Rows[i].FindControl("txtUnitPrice");
                        TextBox nilaiNet = (TextBox)grdSOD.Rows[i].FindControl("txtTotal1");
                        HiddenField hdnNoQuotationD = (HiddenField)grdSOD.Rows[i].FindControl("hdnNoQuotationD");
                        HiddenField hdnProduct = (HiddenField)grdSOD.Rows[i].FindControl("hdnProduct");
                        if (txtProductName.Text != "" && txtQty.Text != "" && txtUnitPrice.Text != "")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noDetQuotation", hdnNoQuotationD.Value);
                            ObjDb.Where.Add("noproduct", hdnProduct.Value);
                            ObjDb.Data.Add("namaItem", txtProductName.Text);
                            ObjDb.Data.Add("qty", Convert.ToDecimal(txtQty.Text).ToString());
                            ObjDb.Data.Add("pUnit", lblUnit.Text);
                            ObjDb.Data.Add("hargaSatuan", Convert.ToDecimal(txtUnitPrice.Text).ToString());
                            ObjDb.Data.Add("nilaiNet", Convert.ToDecimal(nilaiNet.Text).ToString());
                            ObjDb.Update("TransQuotation_D", ObjDb.Data, ObjDb.Where);
                        }

                    }
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