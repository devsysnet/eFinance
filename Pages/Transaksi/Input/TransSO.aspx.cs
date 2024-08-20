using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;


namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransSO : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
               
                cboCurrencyTrans.Text = "0";
                txtkursrate.Visible = false;
                Kurstrs2.Visible = false;


                SetInitialRow();
                for (int i = 1; i < 4; i++)
                {
                    AddNewRow();
                }

                LoadDataComboa();
                loadDataTax();




                //SetInitialRowx();
                //for (int i = 1; i < 1; i++)
                //{
                //    AddNewRowx();
                //}

                showhidegrdpo.Visible = false;
                showhidegrdpototal.Visible = false;
                showhidegrdpobutton.Visible = false;
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

        protected void grdCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCustomer.PageIndex = e.NewPageIndex;
            loadDataCust();
            mpe1.Show();
        }
        protected void loadDataCust()
        {
            grdCustomer.DataSource = ObjDb.GetRows("select * from mcustomer where namaCust LIKE '%" + TextBox2.Text + "%' or kdCust LIKE '%" + TextBox2.Text + "%'");
            grdCustomer.DataBind();
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            loadDataCust();
            mpe1.Show();
        }

        protected void imgButtonSup1_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataPanel();
            dlgSupplier.Show();
        }


        private void LoadDataPanel()
        {
            grdSupp.DataSource = ObjDb.GetRows("select * from mSupplier a left join mSupplierCP b on a.noSup = b.noSup");
            grdSupp.DataBind();
        }

        protected void grdSupp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSupp.PageIndex = e.NewPageIndex;
            LoadDataPanel();
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
        protected void btnCari1_Click(object sender, EventArgs e)
        {
            LoadDataPanel();
        }
        protected void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.Text == "1")
            {
                formRek.Visible = true;

            }

            else
            {
                formRek.Visible = false;
            }

           
        }
        private void loadDataCombo()
        {
            string sql = "";
            //    sql = "select a.* from (select '0' id,' ---Pilih Akun--- ' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where jenis='2' and nocabang = '" + ObjSys.GetCabangId + "') a order by a.id";
            sql = "select a.* from (select '0' id,' ---Pilih Akun--- ' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where jenis='2') a order by a.id";


            cboCurrencyTrans.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noMataUang id, namaMataUang name FROM mMataUang) a");
            cboCurrencyTrans.DataValueField = "id";
            cboCurrencyTrans.DataTextField = "name";
            cboCurrencyTrans.DataBind();

            cboRek.DataSource = ObjDb.GetRows(sql);
            cboRek.DataValueField = "id";
            cboRek.DataTextField = "name";
            cboRek.DataBind();

        }

        #region rows
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string)));
            dt.Columns.Add(new DataColumn("Column5", typeof(string)));
            dt.Columns.Add(new DataColumn("Column6", typeof(string)));
            dt.Columns.Add(new DataColumn("Column7", typeof(string)));
            dt.Columns.Add(new DataColumn("Column8", typeof(string)));
            dt.Columns.Add(new DataColumn("Column9", typeof(string)));
            dt.Columns.Add(new DataColumn("Column10", typeof(string)));
            dt.Columns.Add(new DataColumn("Column11", typeof(string)));
            dt.Columns.Add(new DataColumn("Column12", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dr["Column4"] = string.Empty;
            dr["Column5"] = string.Empty;
            dr["Column6"] = string.Empty;
            dr["Column7"] = string.Empty;
            dr["Column8"] = string.Empty;
            dr["Column9"] = string.Empty;
            dr["Column10"] = string.Empty;
            dr["Column11"] = string.Empty;
            dr["Column12"] = string.Empty;


            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdPO.DataSource = dt;
            grdPO.DataBind();
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
                        TextBox txtProduct = (TextBox)grdPO.Rows[i].FindControl("txtProduct");
                        HiddenField hdnProduct = (HiddenField)grdPO.Rows[i].FindControl("hdnProduct");
                        TextBox lblUnit = (TextBox)grdPO.Rows[i].FindControl("lblUnit");
                        //DropDownList ddCIF = (DropDownList)grdPO.Rows[i].FindControl("ddCIF");
                        TextBox txtProductName = (TextBox)grdPO.Rows[i].FindControl("txtProductName");
                        TextBox txtUnitPrice = (TextBox)grdPO.Rows[i].FindControl("txtUnitPrice");
                        TextBox lblQty = (TextBox)grdPO.Rows[i].FindControl("txtQty");
                        TextBox lblTotal = (TextBox)grdPO.Rows[i].FindControl("txtTotal");
                        HiddenField hdnHasil = (HiddenField)grdPO.Rows[i].FindControl("hdnHasil");
                        TextBox txtCIFValue = (TextBox)grdPO.Rows[i].FindControl("txtCIFValue");
                        TextBox txtQtySatuanBesar = (TextBox)grdPO.Rows[i].FindControl("txtQtySatuanBesar");
                        TextBox lblUnitSatuanBesar = (TextBox)grdPO.Rows[i].FindControl("lblUnitSatuanBesar");
                        TextBox txtQtySatuanBesar1 = (TextBox)grdPO.Rows[i].FindControl("txtQtySatuanBesar1");
                        TextBox lblUnitSatuanBesar1 = (TextBox)grdPO.Rows[i].FindControl("lblUnitSatuanBesar1");
                        txtProduct.Text = dt.Rows[i]["Column1"].ToString();
                        hdnProduct.Value = dt.Rows[i]["Column2"].ToString();
                        txtProductName.Text = dt.Rows[i]["Column3"].ToString();
                        hdnHasil.Value = dt.Rows[i]["Column4"].ToString();
                        lblQty.Text = dt.Rows[i]["Column5"].ToString();
                        //setUnitData(ddCIF, txtProduct.Text);
                        lblUnit.Text = dt.Rows[i]["Column6"].ToString();
                        txtQtySatuanBesar.Text = dt.Rows[i]["Column7"].ToString();
                        //setUnitData(ddCIF, txtProduct.Text);
                        lblUnitSatuanBesar.Text = dt.Rows[i]["Column8"].ToString();
                        txtUnitPrice.Text = dt.Rows[i]["Column9"].ToString();
                        lblTotal.Text = dt.Rows[i]["Column10"].ToString();
                        txtQtySatuanBesar1.Text = dt.Rows[i]["Column11"].ToString();
                        lblUnitSatuanBesar1.Text = dt.Rows[i]["Column12"].ToString();
                    }
                }
            }
        }


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
                        TextBox txtProduct = (TextBox)grdPO.Rows[i].FindControl("txtProduct");
                        HiddenField hdnProduct = (HiddenField)grdPO.Rows[i].FindControl("hdnProduct");
                        TextBox lblUnit = (TextBox)grdPO.Rows[i].FindControl("lblUnit");
                        TextBox txtProductName = (TextBox)grdPO.Rows[i].FindControl("txtProductName");
                        TextBox txtUnitPrice = (TextBox)grdPO.Rows[i].FindControl("txtUnitPrice");
                        TextBox lblQty = (TextBox)grdPO.Rows[i].FindControl("txtQty");
                        TextBox txtTotal = (TextBox)grdPO.Rows[i].FindControl("txtTotal");
                        HiddenField hdnHasil = (HiddenField)grdPO.Rows[i].FindControl("hdnHasil");
                        DropDownList ddCIF = (DropDownList)grdPO.Rows[i].FindControl("ddCIF");
                        TextBox txtCIFValue = (TextBox)grdPO.Rows[i].FindControl("txtCIFValue");
                        TextBox txtQtySatuanBesar = (TextBox)grdPO.Rows[i].FindControl("txtQtySatuanBesar");
                        TextBox lblUnitSatuanBesar = (TextBox)grdPO.Rows[i].FindControl("lblUnitSatuanBesar");
                        TextBox txtQtySatuanBesar1 = (TextBox)grdPO.Rows[i].FindControl("txtQtySatuanBesar1");
                        TextBox lblUnitSatuanBesar1 = (TextBox)grdPO.Rows[i].FindControl("lblUnitSatuanBesar1");
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtProduct.Text;
                        dtCurrentTable.Rows[i]["Column2"] = hdnProduct.Value;
                        dtCurrentTable.Rows[i]["Column3"] = txtProductName.Text;
                        dtCurrentTable.Rows[i]["Column4"] = hdnHasil.Value;
                        dtCurrentTable.Rows[i]["Column5"] = lblQty.Text;
                        dtCurrentTable.Rows[i]["Column6"] = lblUnit.Text;
                        dtCurrentTable.Rows[i]["Column7"] = txtQtySatuanBesar.Text;
                        dtCurrentTable.Rows[i]["Column8"] = lblUnitSatuanBesar.Text;
                        dtCurrentTable.Rows[i]["Column9"] = txtUnitPrice.Text;
                        dtCurrentTable.Rows[i]["Column10"] = txtTotal.Text;
                        dtCurrentTable.Rows[i]["Column11"] = txtQtySatuanBesar1.Text;
                        dtCurrentTable.Rows[i]["Column12"] = lblUnitSatuanBesar1.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdPO.DataSource = dtCurrentTable;
                    grdPO.DataBind();
                }
            }
            SetPreviousData();
        }


        #endregion

        protected void loadDataNewProduct(DropDownList cboPackingUnit, DropDownList cboManufacture, DropDownList cboUnit)
        {
            DataTable data = ObjDb.GetRowsDataTable("select TOP 1 measurement from Parameter");
            string[] dataString = data.Rows[0]["measurement"].ToString().Split('^');
            for (int j = 0; j < dataString.Length; j++)
            {
                cboUnit.Items.Add(dataString[j].ToString());
                cboPackingUnit.Items.Add(dataString[j].ToString());
            }

            cboManufacture.DataSource = ObjDb.GetRows("select a.* from (SELECT '---' name UNION ALL SELECT distinct ManufactureName name FROM mManufacture) a");
            cboManufacture.DataValueField = "name";
            cboManufacture.DataTextField = "name";
            cboManufacture.DataBind();
        }

        #region PopUpProduct
        private void LoadDataProduct()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearch.Text);
            grdProduct.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadDataProductSO", ObjGlobal.Param);
            grdProduct.DataBind();
        }


        protected void grdProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            int rowIndexHdn = Convert.ToInt32(txtHdnPopup.Value);
            int rowIndex = grdProduct.SelectedRow.RowIndex;

            string prodno = "", prodnm = "", noprod = "", unit = "", unitBesar = "",unitBesar1 = "",konfersiBesar = "",konfersiBesar1 = "";
            prodno = (grdProduct.SelectedRow.FindControl("lblKodeReagent") as Label).Text;
            prodnm = (grdProduct.SelectedRow.FindControl("lblNamaReagent") as Label).Text;
            noprod = (grdProduct.SelectedRow.FindControl("hidNoReagent") as HiddenField).Value;
            unit = (grdProduct.SelectedRow.FindControl("hdnSatuan") as HiddenField).Value;
            unitBesar = (grdProduct.SelectedRow.FindControl("hdnSatuanBesar") as HiddenField).Value;
            unitBesar1 = (grdProduct.SelectedRow.FindControl("hdnSatuanBesar1") as HiddenField).Value;
            konfersiBesar = (grdProduct.SelectedRow.FindControl("hdnkonfersiBesar") as HiddenField).Value;
            konfersiBesar1 = (grdProduct.SelectedRow.FindControl("hdnkonfersiBesar1") as HiddenField).Value;

            TextBox txtProduct = (TextBox)grdPO.Rows[rowIndexHdn].FindControl("txtProduct");
            TextBox txtProductName = (TextBox)grdPO.Rows[rowIndexHdn].FindControl("txtProductName");
            HiddenField hdnProduct = (HiddenField)grdPO.Rows[rowIndexHdn].FindControl("hdnProduct");
            TextBox lblUnit = (TextBox)grdPO.Rows[rowIndexHdn].FindControl("lblUnit");
            TextBox lblUnitSatuanBesar = (TextBox)grdPO.Rows[rowIndexHdn].FindControl("lblUnitSatuanBesar");
            TextBox lblUnitSatuanBesar1 = (TextBox)grdPO.Rows[rowIndexHdn].FindControl("lblUnitSatuanBesar1");
            HiddenField tkonfersiBesar1 = (HiddenField)grdPO.Rows[rowIndexHdn].FindControl("konfersiBesar1");
            HiddenField tkonfersiBesar = (HiddenField)grdPO.Rows[rowIndexHdn].FindControl("konfersiBesar");
            TextBox txtQtySatuanBesar1 = (TextBox)grdPO.Rows[rowIndexHdn].FindControl("txtQtySatuanBesar1");
            TextBox txtQtySatuanBesar = (TextBox)grdPO.Rows[rowIndexHdn].FindControl("txtQtySatuanBesar");
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
                if(konfersiBesar1 == "0")
                {
                    txtQtySatuanBesar1.Enabled = false;
                }
                else
                {
                    txtQtySatuanBesar1.Enabled = true;
                }
                if (konfersiBesar == "0")
                {
                    txtQtySatuanBesar.Enabled = false;
                }
                else
                {
                    txtQtySatuanBesar.Enabled = true;
                }
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
                lblUnitSatuanBesar.Text = unitBesar;
                lblUnitSatuanBesar1.Text = unitBesar1;
                //setUnitData(ddCIF, prodno);
                tkonfersiBesar1.Value = konfersiBesar1;
                tkonfersiBesar.Value = konfersiBesar;
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

        protected void setUnitData(DropDownList ddCIF, string prodno)
        {
            //if (cboType.Text == "5")
            //{
            //    lblUnit.DataSource = ObjDb.GetRows("select a.* from (select '-1' pfactor,' ---------' name union all select pfactor, punit name from PickunitTamp where prodno = '" + prodno + "') as a");
            //    lblUnit.DataValueField = "pfactor";
            //    lblUnit.DataTextField = "name";
            //    lblUnit.DataBind();
            //}
            //else
            //{

            //}


            ddCIF.DataSource = ObjDb.GetRows("select a.* from (select '-1' noMataUang,' ---------' namaMataUang union all select noMataUang, namaMataUang from mMataUang) as a");
            ddCIF.DataValueField = "noMataUang";
            ddCIF.DataTextField = "namaMataUang";
            ddCIF.DataBind();
        }

        protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProduct.PageIndex = e.NewPageIndex;
            LoadDataProduct();
            mpe.Show();
        }
        #endregion
       

        protected void AddDataHPP(string hpp, string nomatauangpo, string kurspo, string nilaipo, string nomatauangcif, string nilaicif)
        {
            string valueHPP = "0", valuenomatauangpo = "0", valuekurspo = "0", valuenilaipo = "0", valuenomatauangcif = "0", valuenilaicif = "0";
            if (hpp != "")
                valueHPP = Convert.ToDecimal(hpp).ToString();
            if (nomatauangpo != "")
                valuenomatauangpo = nomatauangpo;
            if (kurspo != "")
                valuekurspo = Convert.ToDecimal(kurspo).ToString();
            if (nilaipo != "")
                valuenilaipo = Convert.ToDecimal(nilaipo).ToString();
            if (nomatauangcif != "")
                valuenomatauangcif = nomatauangcif;
            if (nilaicif != "")
                valuenilaicif = Convert.ToDecimal(nilaicif).ToString();

            ObjDb.Data.Add("hpp", valueHPP);
            ObjDb.Data.Add("nomatauangPO", valuenomatauangpo);
            ObjDb.Data.Add("kursPO", valuekurspo);
            ObjDb.Data.Add("nilaiPO", valuenilaipo);
            ObjDb.Data.Add("nomatauangCIF", valuenomatauangcif);
            ObjDb.Data.Add("nilaiCIF", valuenilaicif);
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (txtIDCust.Text == "")
            {
                message += ObjSys.CreateMessage("Customer tidak boleh kosong.");
                valid = false;
            }
            else if (cboSales.Text == "0")
            {
                message += ObjSys.CreateMessage("Sales Name harus dipilih.");
                valid = false;
            }
            //else if (cboPICName.Text == "")
            //{
            //    message += ObjSys.CreateMessage("PIC tidak boleh kosong.");
            //    valid = false;
            //}



            string PPN = "0", TotalAmount = "0", Amount = "0", Kurs = "0", kursrate = "0";
            if (txtPPN.Text != "")
                PPN = Convert.ToDecimal(txtPPN.Text).ToString();

            if (txtTotalAmount.Text != "")
                TotalAmount = Convert.ToDecimal(txtTotalAmount.Text).ToString();

            if (txtAmount.Text != "")
                Amount = Convert.ToDecimal(txtAmount.Text).ToString();


            if (txtkursrate.Text != "")
                kursrate = Convert.ToDecimal(txtkursrate.Text).ToString();

            try
            {
                if (valid == true)
                {
                    ObjDb.Data.Clear();

                    string Kode = ObjSys.GetCodeAutoNumber("31", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));

                    ObjDb.Data.Add("kdSO", Kode);
                    ObjDb.Data.Add("tglSO", dtPO.Text);
                    ObjDb.Data.Add("noCust", hdnNoCust.Value);
                    ObjDb.Data.Add("typeSO", cboType.SelectedValue);
                    ObjDb.Data.Add("termSO", txtTop.Text);
                    ObjDb.Data.Add("AlamatKirim", txtDeliveryAddress.Text);
                    ObjDb.Data.Add("noRek", cboRek.SelectedValue);
                    ObjDb.Data.Add("nilaiPajak", PPN);
                    ObjDb.Data.Add("Gross", TotalAmount);
                    ObjDb.Data.Add("nilaiNet", Amount);
                    ObjDb.Data.Add("kurs", Kurs);
                    ObjDb.Data.Add("nomatauang", cboCurrencyTrans.SelectedValue);
                    ObjDb.Data.Add("nosales", cboSales.Text);
                    ObjDb.Data.Add("nocabang", ObjSys.GetCabangId);
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                    ObjDb.Data.Add("PIC", cboPICName.Text);
                    ObjDb.Data.Add("stsSO", "0");
                    ObjDb.Data.Add("keterangan", txtRemarks.Text);
                    //ObjDb.Data.Add("PICEmail", txtEmailCP.Text);
                    ObjDb.Insert("hso", ObjDb.Data);
                    //--------------
                    DataSet mySetH = ObjDb.GetRows("select * from hso where kdSO = '" + Kode + "'");
                    DataRow myRowH = mySetH.Tables[0].Rows[0];
                    string noSO = myRowH["noSO"].ToString();
                    ObjSys.UpdateAutoNumberCode("31", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));

                    for (int i = 0; i < grdPO.Rows.Count; i++)
                    {
                        HiddenField hdnProduct = (HiddenField)grdPO.Rows[i].FindControl("hdnProduct");
                        HiddenField hdnHasil = (HiddenField)grdPO.Rows[i].FindControl("hdnHasil");
                        TextBox txtProduct = (TextBox)grdPO.Rows[i].FindControl("txtProduct");
                        TextBox txtProductName = (TextBox)grdPO.Rows[i].FindControl("txtProductName");
                        TextBox txtQty = (TextBox)grdPO.Rows[i].FindControl("txtQty");
                        TextBox lblUnit = (TextBox)grdPO.Rows[i].FindControl("lblUnit");
                        TextBox txtQtySatuanBesar = (TextBox)grdPO.Rows[i].FindControl("txtQtySatuanBesar");
                        TextBox lblUnitSatuanBesar = (TextBox)grdPO.Rows[i].FindControl("lblUnitSatuanBesar");
                        TextBox txtQtySatuanBesar1 = (TextBox)grdPO.Rows[i].FindControl("txtQtySatuanBesar1");
                        TextBox lblUnitSatuanBesar1 = (TextBox)grdPO.Rows[i].FindControl("lblUnitSatuanBesar1");
                        TextBox txtUnitPrice = (TextBox)grdPO.Rows[i].FindControl("txtUnitPrice");
                        TextBox txtTotal = (TextBox)grdPO.Rows[i].FindControl("txtTotal");

                        //    HiddenField hdnLastPricePONo = (HiddenField)grdPO.Rows[i].FindControl("hdnLastPricePONo");
                        //    HiddenField hdnLastPricePOKurs = (HiddenField)grdPO.Rows[i].FindControl("hdnLastPricePOKurs");
                        //    HiddenField hdnLastPricePONilai = (HiddenField)grdPO.Rows[i].FindControl("hdnLastPricePONilai");
                        //    DropDownList ddCIF = (DropDownList)grdPO.Rows[i].FindControl("ddCIF");
                        //    TextBox txtCIFValue = (TextBox)grdPO.Rows[i].FindControl("txtCIFValue");

                        //    string CIFValue = "0";
                        //    if (txtCIFValue.Text != "")
                        //        CIFValue = Convert.ToDecimal(txtCIFValue.Text).ToString();
                       
                        if (txtProduct.Text != "" && txtProductName.Text != "" && txtQty.Text != "" && txtUnitPrice.Text != "")
                        {

                            decimal Qty = 0;
                            if (txtQty.Text != "")
                            {
                                Qty = Convert.ToDecimal(txtQty.Text);
                            }

                            decimal packing = 0;
                            string sql = "";
                            //if (cboType.Text == "5")
                            //sql = "select x.* from (select prodno, packing from ProductTamp) x where x.prodno = '" + txtProduct.Text + "'";
                            //else
                            sql = "select sum(sisaSA) as qty from tSaldoAging where noproduct = '" + hdnProduct.Value + "'";

                            DataSet mySetHx = ObjDb.GetRows(sql);
                            if (mySetHx.Tables[0].Rows.Count > 0)
                            {
                                DataRow myRowHx = mySetHx.Tables[0].Rows[0];
                                packing = Convert.ToDecimal(myRowHx["qty"]);
                            }

                            if (Qty > packing)
                            {
                                message += ObjSys.CreateMessage("Qty untuk produk " + txtProduct.Text + " harus kurang dari / sama dengan stok.");
                                valid = false;
                            }
                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("noSO", noSO);
                            ObjGlobal.Param.Add("noproduct", hdnProduct.Value);
                            ObjGlobal.Param.Add("namaItem", txtProductName.Text);
                            ObjGlobal.Param.Add("qty", Convert.ToDecimal(txtQty.Text).ToString());
                            ObjGlobal.Param.Add("qtySatuanBesar", Convert.ToDecimal(txtQtySatuanBesar.Text).ToString());
                            ObjGlobal.Param.Add("qtySatuanBesar1", Convert.ToDecimal(txtQtySatuanBesar1.Text).ToString());
                            ObjGlobal.Param.Add("punit", lblUnit.Text);
                            ObjGlobal.Param.Add("pUnitSatuanBesar", lblUnitSatuanBesar.Text);
                            ObjGlobal.Param.Add("pUnitSatuanBesar1", lblUnitSatuanBesar1.Text);


                            ObjGlobal.Param.Add("hargaSatuan", Convert.ToDecimal(txtUnitPrice.Text).ToString());
                            ObjGlobal.Param.Add("hargaSatuanRp", Convert.ToDecimal(txtTotalAmount.Text).ToString());

                            ObjGlobal.Param.Add("sisaQty", "0");
                            ObjGlobal.Param.Add("qtyKurang", Convert.ToDecimal(txtQty.Text).ToString());
                            ObjGlobal.Param.Add("nilaiDisc", "0");
                            ObjGlobal.Param.Add("subTotal", Convert.ToDecimal(txtAmount.Text).ToString());
                            ObjGlobal.Param.Add("nilaiNet", Convert.ToDecimal(txtTotalAmount.Text).ToString());
                            ObjGlobal.Param.Add("stsbrg", "0");


                            ObjGlobal.ExecuteProcedure("SPInsertSO", ObjGlobal.Param);

                        }

                    }

                    if (cboType.SelectedValue == "1")
                    {
                        HttpContext.Current.Session["ParamReport"] = null;
                        Session["REPORTNAME"] = null;
                        Session["REPORTTITLE"] = null;
                        Param.Clear();
                        Param.Add("noSO", noSO);
                        HttpContext.Current.Session.Add("ParamReport", Param);
                        Session["REPORTNAME"] = "RVoucherCashSO.rpt";
                        Session["REPORTTILE"] = "Report Voucher SO";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);
                        clearData();
                    }
                    else
                    {
ShowMessage("success", "Data berhasil disimpan.");
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    clearData();
                    }

                    
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }

        }

        protected void CloseMessage()
        {
            ((Label)Master.FindControl("lblMessage")).Text = "";
            ((Label)Master.FindControl("lblMessage")).Visible = false;
        }
        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }
        private void clearData()
        {
            dtPO.Text = "";
            txtIDCust.Text = "";
            lblAddress.Text = "";
            lblNamaCust.Text = "";
            lblPhoneFax.Text = "";
            txtTop.Text = "";
            cboType.Text = "1";
            txtDeliveryAddress.Text = "";
            txtAmount.Text = "";
            txtPPN.Text = "";
            cboSales.SelectedIndex = 0;
            cboPICName.Text = "";
            cboCurrencyTrans.Text = "0";
            txtEmailCP.Text = "";
            txtkursrate.Text = "0.00";
            txtTotalAmount.Text = "";
          
            lblkreditlimit.Text = "0.00";
            lblsisakreditlimit.Text = "0.00";
            TextBox2.Text = "";
            txtSearch.Text = "";
            SetInitialRow();
            for (int i = 1; i < 5; i++)
            {
                AddNewRow();
            }

            //SetInitialRowx();
            //for (int i = 1; i < 1; i++)
            //{
            //    AddNewRowx();
            //}

            clearGrid();
            //txtAmount2.Text = "0.00";
            showhidegrdpo.Visible = false;
            showhidegrdpototal.Visible = false;
            showhidegrdpobutton.Visible = false;

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearData();
            CloseMessage();
        }

        protected void LoadDataComboa()
        {
            cboSales.DataSource = ObjDb.GetRows("select distinct a.* from (select '0' id,'---' name union all select noUser id, namauser name FROM mUser where stsUser = '1' and noUser = '" + ObjSys.GetUserId + "' union all SELECT distinct noUser id, namauser name FROM mUser u where stsUser = '1' and EXISTS(SELECT 1 FROM tAkses t WHERE noAkses IN('3', '9', '12') AND u.noUser = t.noUser)) a");
            cboSales.DataValueField = "id";
            cboSales.DataTextField = "name";
            cboSales.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDataProduct();
            mpe.Show();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void grdDataAlamatKirim_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = grdDataAlamatKirim.SelectedRow.RowIndex;
            txtDeliveryAddress.Text = grdDataAlamatKirim.Rows[rowIndex].Cells[0].Text;
        }

        protected void btnBrowseAlamatKirim_Click(object sender, ImageClickEventArgs e)
        {
            DataTable myData = ObjDb.GetRowsDataTable("select distinct alamat from mAlamatkirim where nocust='" + hdnNoCust.Value + "'");
            grdDataAlamatKirim.DataSource = myData;
            grdDataAlamatKirim.DataBind();
            mp1.PopupControlID = "panelAlamatKirim";
            mp1.Show();
        }


        protected string remarkText;

        protected void Button4_Click(object sender, EventArgs e)
        {
            UpdateChecked();
            dlgKeterangan.Hide();

        }

        protected void clearGrid()
        {
            ViewState["DataKet"] = null;
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

        protected void btnRemark_Click(object sender, ImageClickEventArgs e)
        {
            dlgKeterangan.Show();
        }
        protected void imgButtonSup_Click(object sender, ImageClickEventArgs e)
        {
            loadDataCust();
            mpe1.Show();
        }

        protected void Button6_Click(object sender, EventArgs e)
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

        protected void TglPO_SelectionChanged(object sender, EventArgs e)
        {
            dtPO.Text = TglPO.SelectedDate.ToString("dd-MMM-yyyy");
            TglPO.Visible = false;
            DateTime tanggalAkhir = DateTime.ParseExact(dtPO.Text, "dd-MMM-yyyy", null);
          
        }

        protected void lnkPickDate_Click(object sender, EventArgs e)
        {
            TglPO.Visible = true;
        }

        //protected void cboType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboType.Text == "2")
        //    {
        //        //tampil kolom shipment
        //        for (int i = 0; i < grdPO.Rows.Count; i++)
        //        {
        //            grdPO.Columns[11].Visible = false;
        //            grdPO.Columns[12].Visible = true;
        //        }

        //    }
        //    else
        //    {
        //        for (int i = 0; i < grdPO.Rows.Count; i++)
        //        {
        //            grdPO.Columns[11].Visible = true;
        //            grdPO.Columns[12].Visible = false;
        //        }

        //    }

        //    //if (cboType.Text == "5")
        //    //{
        //    //    showhidegrdpo.Visible = true;
        //    //    showhidegrdpototal.Visible = true;
        //    //    showhidegrdpobutton.Visible = true;

        //    //    for (int k = 0; k < grdPONewProduct.Rows.Count; k++)
        //    //    {
        //    //        DropDownList cboPackingUnit = (DropDownList)grdPONewProduct.Rows[k].FindControl("cboPackingUnit");
        //    //        DropDownList cboManufacture = (DropDownList)grdPONewProduct.Rows[k].FindControl("cboManufacture");
        //    //        DropDownList cboUnit = (DropDownList)grdPONewProduct.Rows[k].FindControl("cboUnit");
        //    //        loadDataNewProduct(cboPackingUnit, cboManufacture, cboUnit);
        //    //    }
        //    //    txtAmount2.Text = "0.00";
        //    //    txtAmount.Text = "0.00";
        //    //    txtPPN.Text = "0.00";
        //    //    txtTotalAmount.Text = "0.00";
        //    //}
        //    //else
        //    //{
        //    showhidegrdpo.Visible = false;
        //    showhidegrdpototal.Visible = false;
        //    showhidegrdpobutton.Visible = false;

        //    //txtAmount2.Text = "0.00";
        //    txtAmount.Text = "0.000";
        //    txtPPN.Text = "0.000";
        //    txtTotalAmount.Text = "0.000";
        //    //}

        //    SetInitialRow();
        //    for (int i = 1; i < 5; i++)
        //    {
        //        AddNewRow();

        //    }
        //}

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

        protected void grdKeterangan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UpdateChecked();
            grdKeterangan.PageIndex = e.NewPageIndex;
            dlgKeterangan.Show();
        }

        protected void cboCurrencyTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nilai = "0.00";
            DataSet mySetH = ObjDb.GetRows("select TOP 1 nilaiKursPajak from tKursPajak where noMatauang = '" + cboCurrencyTrans.SelectedValue + "'");
            DataSet mySetHx = ObjDb.GetRows("select stsDefault from mmatauang where noMatauang = '" + cboCurrencyTrans.SelectedValue + "'");
            DataRow myRowHx = mySetHx.Tables[0].Rows[0];


            //if (myRowHx["stsDefault"].ToString() == "1")
            //{
            //    nilai = myRowHx["stsDefault"].ToString();
            //}
            //else
            //{
            //    DataRow myRowH = mySetH.Tables[0].Rows[0];
            //    if (mySetH.Tables[0].Rows.Count > 0)
            //    {
            //        nilai = myRowH["nilaiKursPajak"].ToString();
            //    }
            //    else
            //    {
            //        nilai = "0.00";
            //    }


            //}

            if (myRowHx["stsDefault"].ToString() == "1")
            {
                nilai = myRowHx["stsDefault"].ToString();
            }
            else
            {
                nilai = "0.00";
            }
            
            txtkursrate.Visible = true;
            Kurstrs2.Visible = true;
            txtkursrate.Text = ObjSys.IsFormatNumber(nilai);
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

        protected void cboTax_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
        }


        protected void grdPO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            if (e.CommandName == "Select")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                txtHdnPopup.Value = rowIndex.ToString();
                TextBox txtProduct = (TextBox)grdPO.Rows[rowIndex].FindControl("txtProduct");
                lblMessageError.Visible = false;
                txtSearch.Text = "";
                LoadDataProduct();
                mpe.Show();

            }
            else if (e.CommandName == "Clear")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                TextBox txtProduct = (TextBox)grdPO.Rows[rowIndex].FindControl("txtProduct");
                TextBox txtProductName = (TextBox)grdPO.Rows[rowIndex].FindControl("txtProductName");
                HiddenField hdnProduct = (HiddenField)grdPO.Rows[rowIndex].FindControl("hdnProduct");
                TextBox txtQty = (TextBox)grdPO.Rows[rowIndex].FindControl("txtQty");
                TextBox lblUnit = (TextBox)grdPO.Rows[rowIndex].FindControl("lblUnit");
                TextBox txtQtySatuanBesar = (TextBox)grdPO.Rows[rowIndex].FindControl("txtQtySatuanBesar");
                TextBox lblUnitSatuanBesar = (TextBox)grdPO.Rows[rowIndex].FindControl("lblUnitSatuanBesar");
                TextBox txtQtySatuanBesar1 = (TextBox)grdPO.Rows[rowIndex].FindControl("txtQtySatuanBesar1");
                TextBox lblUnitSatuanBesar1 = (TextBox)grdPO.Rows[rowIndex].FindControl("lblUnitSatuanBesar1");
                TextBox txtUnitPrice = (TextBox)grdPO.Rows[rowIndex].FindControl("txtUnitPrice");
                TextBox txtTotal = (TextBox)grdPO.Rows[rowIndex].FindControl("txtTotal");

                lblUnit.Text = "";
                lblUnitSatuanBesar.Text = "";
                lblUnitSatuanBesar1.Text = "";
                txtProduct.Text = "";
                txtProductName.Text = "";            
                hdnProduct.Value = "";
                txtAmount.Text = "0";
                txtPPN.Text = "0";
                txtTotalAmount.Text = "0";
                txtQty.Text = "0";
                txtQtySatuanBesar.Text = "0";
                txtQtySatuanBesar1.Text = "0";
                txtUnitPrice.Text = "0";
                txtTotal.Text = "0";

                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);

            }
        }


    }
}