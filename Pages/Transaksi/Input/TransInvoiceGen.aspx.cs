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
    public partial class TransInvoiceGen : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                loadkodeold();
                loadkodetax();
                loadDataTax();
                cbolistkodeold.Visible = false;
                cbolistkodetaxold.Visible = false;
                cboCurrency.Text = "0";
                txtKurs.Visible = false;
                Kurstrs.Visible = false;
                txtKurs.Text = "1.00";
                cbokode.Text = "1";
                cboTax.Text = "1";
                cboTaxNo.Text = "1";
                SetInitialRow();
                for (int i = 1; i < 4; i++)
                {
                    AddNewRow();
                }
                string sql = "select * from mcabang where nocabang = "+ ObjSys.GetCabangId;
                DataSet mySet = ObjDb.GetRows(sql);
                if (mySet.Tables[0].Rows[0]["kategoriusaha"].ToString() == "Sekolah")
                {
                    cboSales.Visible = true;
                }
                else
                {
                    cboSales.Visible = false;
                }
                loadrekening();
            }
        }
        protected void loadrekening()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("GetstsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("GetstsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("kategoriusaha", ObjSys.GetKategori_Usaha);
            GridView1.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekeningKasBank3", ObjGlobal.Param);
            GridView1.DataBind();
        }

        protected void grdBank_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            GridView1.PageIndex = e.NewPageIndex;
                loadrekening();

            dlgBank.Show();
        }

        protected void grdBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;

                int rowIndexHdn = Convert.ToInt32(hdnParameterProd.Value);
                int rowIndex = GridView1.SelectedRow.RowIndex;

                string kdRek = (GridView1.SelectedRow.FindControl("lblKdRek") as Label).Text;
                string Ket = (GridView1.SelectedRow.FindControl("lblKet") as Label).Text;
                string noRek = (GridView1.SelectedRow.FindControl("hdnNoRek") as HiddenField).Value;

                HiddenField hdnAccount = (HiddenField)grdInvoice.Rows[rowIndexHdn].FindControl("hdnAccount");
                TextBox txtAccount = (TextBox)grdInvoice.Rows[rowIndexHdn].FindControl("txtAccount");
                Label lblDescription = (Label)grdInvoice.Rows[rowIndexHdn].FindControl("lblDescription");
               

                if (valid == true)
                {
                    hdnAccount.Value = noRek;
                    txtAccount.Text = kdRek;
                    lblDescription.Text = Ket;
                loadrekening();

                    txtSearch.Text = "";
                     
                    dlgBank.Hide();

                    lblMessageError.Visible = false;
                }
                else
                {
                    dlgBank.Show();
                    lblMessageError.Text = ObjSys.GetMessage("error", message);
                    lblMessageError.Visible = true;
                    loadrekening();

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
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


        protected void btnBrowsePelanggan_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataPelanggan();
            dlgAddData.PopupControlID = "panelAddDataPelanggan";
            dlgAddData.Show();
        }

        protected void btnBrowseBank_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataBank();
            dlgBank.PopupControlID = "panelAddDataBank";
            dlgBank.Show();
        }

        protected void LoadDataPelanggan()
        {
            DataTable myData = ObjDb.GetRowsDataTable("select * from mCustomer");
            grdDataPelanggan.DataSource = myData;
            grdDataPelanggan.DataBind();
        }
        protected void LoadDataBank()
        {
            DataTable myData = ObjDb.GetRowsDataTable("select * from mBank");
            grdBank.DataSource = myData;
            grdBank.DataBind();
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
      
        private void loadkodeold()
        {
            cbolistkodeold.DataSource = ObjDb.GetRows("select distinct kodeInvoice from tTampKodeInvoice");
            cbolistkodeold.DataValueField = "kodeInvoice";
            cbolistkodeold.DataTextField = "kodeInvoice";
            cbolistkodeold.DataBind();
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
            dt.Columns.Add(new DataColumn("lblDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dr["Column4"] = string.Empty;
            dr["Column5"] = string.Empty;
            dr["txtAccount"] = string.Empty;
            dr["hdnAccount"] = string.Empty;
            dr["lblDescription"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdInvoice.DataSource = dt;
            grdInvoice.DataBind();
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
                        TextBox txtProductName = (TextBox)grdInvoice.Rows[i].FindControl("txtProductName");
                        TextBox txtQty = (TextBox)grdInvoice.Rows[i].FindControl("txtQty");
                        TextBox txtSatuan = (TextBox)grdInvoice.Rows[i].FindControl("txtSatuan");
                        TextBox txtHarga = (TextBox)grdInvoice.Rows[i].FindControl("txtHarga");
                        TextBox txtJumlah = (TextBox)grdInvoice.Rows[i].FindControl("txtJumlah");
                        HiddenField hdnAccount = (HiddenField)grdInvoice.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdInvoice.Rows[i].FindControl("txtAccount");
                        Label lblDescription = (Label)grdInvoice.Rows[i].FindControl("lblDescription");

                        txtProductName.Text = dt.Rows[i]["Column1"].ToString();
                        txtQty.Text = dt.Rows[i]["Column2"].ToString();
                        txtSatuan.Text = dt.Rows[i]["Column3"].ToString();
                        txtHarga.Text = dt.Rows[i]["Column4"].ToString();
                        txtJumlah.Text = dt.Rows[i]["Column5"].ToString();

                        txtAccount.Text = dt.Rows[i]["txtAccount"].ToString();
                        hdnAccount.Value = dt.Rows[i]["hdnAccount"].ToString();
                        lblDescription.Text = dt.Rows[i]["lblDescription"].ToString();
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
                        TextBox txtProductName = (TextBox)grdInvoice.Rows[i].FindControl("txtProductName");
                        TextBox txtQty = (TextBox)grdInvoice.Rows[i].FindControl("txtQty");
                        TextBox txtSatuan = (TextBox)grdInvoice.Rows[i].FindControl("txtSatuan");
                        TextBox txtHarga = (TextBox)grdInvoice.Rows[i].FindControl("txtHarga");
                        TextBox txtJumlah = (TextBox)grdInvoice.Rows[i].FindControl("txtJumlah");
                        HiddenField hdnAccount = (HiddenField)grdInvoice.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdInvoice.Rows[i].FindControl("txtAccount");
                        Label lblDescription = (Label)grdInvoice.Rows[i].FindControl("lblDescription");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtProductName.Text;
                        dtCurrentTable.Rows[i]["Column2"] = txtQty.Text;
                        dtCurrentTable.Rows[i]["Column3"] = txtSatuan.Text;
                        dtCurrentTable.Rows[i]["Column4"] = txtHarga.Text;
                        dtCurrentTable.Rows[i]["Column5"] = txtJumlah.Text;
                        dtCurrentTable.Rows[i]["txtAccount"] = txtAccount.Text;
                        dtCurrentTable.Rows[i]["hdnAccount"] = hdnAccount.Value;
                        dtCurrentTable.Rows[i]["lblDescription"] = lblDescription.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdInvoice.DataSource = dtCurrentTable;
                    grdInvoice.DataBind();
                }
            }
            SetPreviousData();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadrekening();
            dlgBank.Show();
        }
        protected void grdKasBank_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                hdnParameterProd.Value = rowIndex.ToString();
                if (e.CommandName == "Select")
                {
                
                        CloseMessage();
                loadrekening();

                    dlgBank.Show();
                  

                }
                else if (e.CommandName == "Clear")
                {
                    TextBox txtAccount = (TextBox)grdInvoice.Rows[rowIndex].FindControl("txtAccount");
                    HiddenField hdnAccount = (HiddenField)grdInvoice.Rows[rowIndex].FindControl("hdnAccount");
                    Label lblDescription = (Label)grdInvoice.Rows[rowIndex].FindControl("lblDescription");
                    TextBox txtQty = (TextBox)grdInvoice.Rows[rowIndex].FindControl("txtQty");
                    TextBox txtSatuan = (TextBox)grdInvoice.Rows[rowIndex].FindControl("txtSatuan");
                    TextBox txtHarga = (TextBox)grdInvoice.Rows[rowIndex].FindControl("txtHarga");
                    TextBox txtJumlah = (TextBox)grdInvoice.Rows[rowIndex].FindControl("txtJumlah");

                    txtAccount.Text = "";
                    hdnAccount.Value = "";
                    lblDescription.Text = "";
                    txtQty.Text = "";
                    txtSatuan.Text = "";
                    txtHarga.Text = "";
                    txtJumlah.Text = "";

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();CalculateDiscount();", true);

                }

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
            string Kode = "";
            bool valid = true;

            //if (txtIDCust.Text == "")
            //{
            //    message += ObjSys.CreateMessage("Customer tidak boleh kosong.");
            //    valid = false;
            //}
            if (txtTop.Text == "")
            {
                message += ObjSys.CreateMessage("TOP tidak boleh kosong.");
                valid = false;
            }
            if (cboCurrency.Text == "")
            {
                message += ObjSys.CreateMessage("Mata Uang harus dipilih.");
                valid = false;
            }
            int cek = 0;
            for (int i = 0; i < grdInvoice.Rows.Count; i++)
            {
                TextBox txtProductName = (TextBox)grdInvoice.Rows[i].FindControl("txtProductName");
                TextBox txtQty = (TextBox)grdInvoice.Rows[i].FindControl("txtQty");
                TextBox txtHarga = (TextBox)grdInvoice.Rows[i].FindControl("txtHarga");

                if (txtProductName.Text != "" && txtQty.Text != "" && txtHarga.Text != "")
                {
                    cek++;
                }
            }

            if (cek == 0)
            {
                message += ObjSys.CreateMessage("Detil tidak boleh kosong.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {

                    ObjDb.Data.Clear();
                    if (cbokode.Text == "1")
                        Kode = ObjSys.GetCodeAutoNumber("30", Convert.ToDateTime(dtInv.Text).ToString("yyyy-MM-dd"));
                    else
                        Kode = cbolistkodeold.Text;

                    ObjDb.Data.Add("kdInvoice", Kode);
                    ObjDb.Data.Add("tglInvoice", Convert.ToDateTime(dtInv.Text).ToString("yyyy-MM-dd"));
                    ObjDb.Data.Add("nocust", hdnNoCustomer.Value);
                    ObjDb.Data.Add("termInvoice", txtTop.Text);
                    ObjDb.Data.Add("keterangan", txtRemarks.Text);
                    ObjDb.Data.Add("stspajak", cboTax.Text);
                    ObjDb.Data.Add("prosenPajak", Convert.ToDecimal(hdnPersenPPn.Value).ToString());
                    ObjDb.Data.Add("nosales", cboSales.Text);
                    ObjDb.Data.Add("noBank", hdnNoBank.Value);
                    ObjDb.Data.Add("noPO", txtPOno.Text);
                    ObjDb.Data.Add("noRef", txtNoref.Text);
                    ObjDb.Data.Add("noMataUang", cboCurrency.Text);
                    ObjDb.Data.Add("kursInvoice", Convert.ToDecimal(txtKurs.Text).ToString());
                    if (cboTax.Text == "1" && cboTaxNo.Text == "1")
                    {
                        ObjDb.Data.Add("taxid", cbolistkodetaxnew.Text);
                    }
                    else if (cboTax.Text == "1" && cboTaxNo.Text == "2")
                    {
                        ObjDb.Data.Add("taxid", cbolistkodetaxold.Text);
                    }
                    else
                    {
                        ObjDb.Data.Add("taxid", "");
                    }

                    ObjDb.Data.Add("nilaiNet", Convert.ToDecimal(txtTotalAmount.Text).ToString());
                    ObjDb.Data.Add("nilaiNetRp", (Convert.ToDecimal(txtTotalAmount.Text) * Convert.ToDecimal(txtKurs.Text)).ToString());
                    ObjDb.Data.Add("nilaiPajak", Convert.ToDecimal(txtPPN.Text).ToString());
                    ObjDb.Data.Add("nilaiPajakRp", (Convert.ToDecimal(txtPPN.Text) * Convert.ToDecimal(txtKurs.Text)).ToString());
                    ObjDb.Data.Add("nilaipph", Convert.ToDecimal(txtpph.Text).ToString());
                    ObjDb.Data.Add("nilaipphrp", (Convert.ToDecimal(txtpph.Text) * Convert.ToDecimal(txtKurs.Text)).ToString());
                    ObjDb.Data.Add("Gross", Convert.ToDecimal(txtAmount.Text).ToString());
                    ObjDb.Data.Add("GrossRp", (Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtKurs.Text)).ToString());
                    ObjDb.Data.Add("stsInvoice", "0");
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("nocabang", ObjSys.GetCabangId);
                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                    ObjDb.Insert("tInvoiceGeneral_H", ObjDb.Data);

                    if (cboTax.Text == "1" && cboTaxNo.Text == "1")
                    {
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noPajak", cbolistkodetaxnew.Text);
                        ObjGlobal.ExecuteProcedure("SPUpdatePajak", ObjGlobal.Param);
                    }
                    else if (cboTax.Text == "1" && cboTaxNo.Text == "2")
                    {
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noPajak", cbolistkodetaxold.Text);
                        ObjGlobal.ExecuteProcedure("SPUpdatePajak", ObjGlobal.Param);
                    }

                    if (cbokode.Text == "1")
                        ObjSys.UpdateAutoNumberCode("30", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                    else
                        ObjDb.Where.Clear();
                    ObjDb.Data.Clear();
                    ObjDb.Where.Add("kodeInvoice", cbolistkodeold.Text);
                    ObjDb.Delete("tTampKodeInvoice", ObjDb.Where);

                    DataSet mySetH = ObjDb.GetRows("select * from tInvoiceGeneral_H where kdInvoice = '" + Kode + "'");
                    DataRow myRowH = mySetH.Tables[0].Rows[0];
                    string noInvoice = myRowH["noInvoiceGen"].ToString();

                    for (int i = 0; i < grdInvoice.Rows.Count; i++)
                    {
                        {
                            TextBox txtProductName = (TextBox)grdInvoice.Rows[i].FindControl("txtProductName");
                            TextBox txtQty = (TextBox)grdInvoice.Rows[i].FindControl("txtQty");
                            TextBox txtSatuan = (TextBox)grdInvoice.Rows[i].FindControl("txtSatuan");
                            TextBox txtHarga = (TextBox)grdInvoice.Rows[i].FindControl("txtHarga");
                            TextBox txtJumlah = (TextBox)grdInvoice.Rows[i].FindControl("txtJumlah");

                            if (txtProductName.Text != "" && txtQty.Text != "" && txtHarga.Text != "")
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noInvoiceGen", noInvoice);
                                ObjDb.Data.Add("brg", txtProductName.Text);
                                ObjDb.Data.Add("qty", Convert.ToDecimal(txtQty.Text).ToString());
                                ObjDb.Data.Add("satuan", txtSatuan.Text);
                                ObjDb.Data.Add("hargaSatuan", Convert.ToDecimal(txtHarga.Text).ToString());
                                ObjDb.Data.Add("grossDet", Convert.ToDecimal(txtJumlah.Text).ToString());
                                ObjDb.Insert("tInvoiceGeneral_D", ObjDb.Data);
                            }
                        }
                    }

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                    clearData();
                    loadkodetax();
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
            clearData();
            CloseMessage();
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
            dtInv.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            //txtIDCust.Text = "";
            //lblAddress.Text = "";
            //lblNamaCust.Text = "";
            //lblPhoneFax.Text = "";
            txtTop.Text = "";
            txtPOno.Text = "";
            txtNoref.Text = "";
            txtAmount.Text = "";
            txtTotalAmount.Text = "";
            txtPPN.Text = "";
            txtKurs.Text = "1.00";
            cboTax.Text = "1";
            txtRemarks.Text = "";

            SetInitialRow();
            for (int i = 1; i < 4; i++)
            {
                AddNewRow();
            }
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

        protected void grdDataPelanggan_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRow = grdDataPelanggan.SelectedIndex;
            hdnNoCustomer.Value = grdDataPelanggan.Rows[selectedRow].Cells[0].Text;
            txtNamaPelanggan.Text = grdDataPelanggan.Rows[selectedRow].Cells[1].Text;
            txtAlamatPelanggan.Text = grdDataPelanggan.Rows[selectedRow].Cells[2].Text;
        }
        protected void grdDataBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRow = grdBank.SelectedIndex;
            hdnNoBank.Value = grdBank.Rows[selectedRow].Cells[0].Text;
            txtBankName.Text = grdBank.Rows[selectedRow].Cells[1].Text;
            txtNoRek.Text = grdBank.Rows[selectedRow].Cells[2].Text;
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
    }
}