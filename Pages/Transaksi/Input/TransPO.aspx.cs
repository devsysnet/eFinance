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
    public partial class TransPO : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
                cboTransaction.Text = "0";
                cboDana.Text = "Tidak";
                loadPajakPPh();

            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("noUser", ObjSys.GetUserId);
            ObjGlobal.Param.Add("Dana", cboDana.Text);
            ObjGlobal.Param.Add("Jenis", cboTransaction.Text);
            grdPR.DataSource = ObjGlobal.GetDataProcedure("SPLoadinputPO", ObjGlobal.Param);
            grdPR.DataBind();
            if (grdPR.Rows.Count > 0)
                button.Visible = true;
            else
                button.Visible = false;

            cekKategori_Usaha();
        }

        protected void cekKategori_Usaha()
        {
            if (ObjSys.GetKategori_Usaha == "Sekolah")
            {
                divDanaBOSH.Visible = true;

                for (int i = 0; i < grdPR.Rows.Count; i++)
                {
                    grdPR.Columns[1].Visible = true;
                    grdPR.Columns[2].Visible = true;
                    grdPR.Columns[3].Visible = true;
                    grdPR.Columns[4].Visible = true;
                    grdPR.Columns[5].Visible = true;
                    grdPR.Columns[6].Visible = true;
                    grdPR.Columns[7].Visible = true;
                    grdPR.Columns[8].Visible = true;
                    grdPR.Columns[9].Visible = true;
                    grdPR.Columns[10].Visible = true;
                }
            }
            else
            {
                divDanaBOSH.Visible = false;

                for (int i = 0; i < grdPR.Rows.Count; i++)
                {
                    grdPR.Columns[1].Visible = true;
                    grdPR.Columns[2].Visible = true;
                    grdPR.Columns[3].Visible = true;
                    grdPR.Columns[4].Visible = true;
                    grdPR.Columns[5].Visible = true;
                    grdPR.Columns[6].Visible = true;
                    grdPR.Columns[7].Visible = true;
                    grdPR.Columns[8].Visible = true;
                    grdPR.Columns[9].Visible = true;
                    grdPR.Columns[10].Visible = false;
                }
                if (grdPR.Rows.Count == 0)
                {
                    grdPR.Columns[1].Visible = true;
                    grdPR.Columns[2].Visible = true;
                    grdPR.Columns[3].Visible = true;
                    grdPR.Columns[4].Visible = true;
                    grdPR.Columns[5].Visible = true;
                    grdPR.Columns[6].Visible = true;
                    grdPR.Columns[7].Visible = true;
                    grdPR.Columns[8].Visible = true;
                    grdPR.Columns[9].Visible = true;
                    grdPR.Columns[10].Visible = false;
                }

            }
        }
        protected void loadPajakPPh()
        {
            string persenPPn = "0", persenPPh = "0";
            string sql = "select isnull(persenPajak,0) as persenPajak, isnull(pph,0) as persenPPh from Parameter";
            DataSet mySet = ObjDb.GetRows(sql);
            if (mySet.Tables[0].Rows.Count > 0)
            {
                persenPPn = mySet.Tables[0].Rows[0]["persenPajak"].ToString();
                persenPPh = mySet.Tables[0].Rows[0]["persenPPh"].ToString();
            }

            lblPersenPPn.Text = persenPPn;
            hdnPersenPajak.Value = persenPPn;
            hdnPersenPPh.Value = persenPPh;

        }
        protected void imgButtonSup_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataPanel();
            dlgSupplier.Show();
        }

        private void LoadDataPanel()
        {
            grdSupp.DataSource = ObjDb.GetRows("select * from mSupplier where (kodeSupplier like '%" + txtSearch.Text + "%' or namaSupplier like '%" + txtSearch.Text + "%')");
            grdSupp.DataBind();
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataPanel();
            dlgSupplier.Show();
        }

        protected void grdSupp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSupp.PageIndex = e.NewPageIndex;
            LoadDataPanel();
            dlgSupplier.Show();
        }
        protected void grdSupp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string namaCust = "", noCust = "", kodeCust = "", addres = "", notelp = "";
            kodeCust = (grdSupp.SelectedRow.FindControl("lblKodeSup") as Label).Text;
            namaCust = (grdSupp.SelectedRow.FindControl("lblNamaSup") as Label).Text;
            noCust = (grdSupp.SelectedRow.FindControl("hidNoSup") as HiddenField).Value;
            addres = (grdSupp.SelectedRow.FindControl("lblaAddress") as Label).Text;
            notelp = (grdSupp.SelectedRow.FindControl("lblNoTel") as Label).Text;

            lblNama.Text = namaCust;
            hdnNoSupp.Value = noCust;
            txtIDSup.Text = kodeCust;
            lblAddress.Text = addres;
            lblPhoneFax.Text = notelp;
            dlgSupplier.Hide();
        }

        #region setInitial & AddRow
        private void SetInitialRow(string Id = "", string PRD = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnnoPRd", typeof(string)));
            dt.Columns.Add(new DataColumn("txtkodebrg", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("lblnamaBarang", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQtyBesar", typeof(string)));
            dt.Columns.Add(new DataColumn("txtsatuanBesar", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQty", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQtykecil", typeof(string)));
            dt.Columns.Add(new DataColumn("txtsatuan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtBudgetPR", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKeterangan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtHargaSatuan", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("Id", Id);
            ObjGlobal.Param.Add("PRD", PRD);
            ObjGlobal.Param.Add("DanaBOS", cboDana.Text);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPViewPOD", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnnoPRd"] = myRow["noPRd"].ToString();
                dr["txtkodebrg"] = myRow["kodeBarang"].ToString();
                dr["lblnamaBarang"] = myRow["namaBarang"].ToString();
                dr["txtsatuanBesar"] = myRow["satbesar"].ToString();
                dr["txtQtyBesar"] = ObjSys.IsFormatNumber(myRow["qtybesar"].ToString());
                dr["txtsatuan"] = myRow["satuan"].ToString();
                dr["txtQty"] = ObjSys.IsFormatNumber(myRow["qty"].ToString());
                dr["txtQtykecil"] = ObjSys.IsFormatNumber(myRow["qtykecil"].ToString());
                dr["txtBudgetPR"] = ObjSys.IsFormatNumber(myRow["budget"].ToString());
                dr["txtHargaSatuan"] = ObjSys.IsFormatNumber(myRow["hargaSatuanPR"].ToString());

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnnoPRd"] = string.Empty;
                dr["txtkodebrg"] = string.Empty;
                dr["lblnamaBarang"] = string.Empty;
                dr["txtQtyBesar"] = string.Empty;
                dr["txtsatuanBesar"] = string.Empty;
                dr["txtQty"] = string.Empty;
                dr["txtQtykecil"] = string.Empty;
                dr["txtsatuan"] = string.Empty;
                dr["txtBudgetPR"] = string.Empty;
                dr["txtHargaSatuan"] = string.Empty;

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdPRDetil.DataSource = dt;
            grdPRDetil.DataBind();

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
                        HiddenField hdnnoPRd = (HiddenField)grdPRDetil.Rows[i].FindControl("hdnnoPRd");
                        TextBox txtkodebrg = (TextBox)grdPRDetil.Rows[i].FindControl("txtkodebrg");
                        TextBox txtQtyBesar = (TextBox)grdPRDetil.Rows[i].FindControl("txtQtyBesar");
                        TextBox txtsatuanBesar = (TextBox)grdPRDetil.Rows[i].FindControl("txtsatuanBesar");
                        TextBox txtQty = (TextBox)grdPRDetil.Rows[i].FindControl("txtQty");
                        TextBox txtQtykecil = (TextBox)grdPRDetil.Rows[i].FindControl("txtQtykecil");
                        Label lblnamaBarang = (Label)grdPRDetil.Rows[i].FindControl("lblnamaBarang");
                        TextBox txtsatuan = (TextBox)grdPRDetil.Rows[i].FindControl("txtsatuan");
                        TextBox txtBudgetPR = (TextBox)grdPRDetil.Rows[i].FindControl("txtBudgetPR");
                        TextBox txtHargaSatuan = (TextBox)grdPRDetil.Rows[i].FindControl("txtHargaSatuan");

                        txtkodebrg.Text = dt.Rows[i]["txtkodebrg"].ToString();
                        lblnamaBarang.Text = dt.Rows[i]["lblnamaBarang"].ToString();
                        txtQtyBesar.Text = dt.Rows[i]["txtQtyBesar"].ToString();
                        txtsatuanBesar.Text = dt.Rows[i]["txtsatuanBesar"].ToString();
                        txtQty.Text = dt.Rows[i]["txtQty"].ToString();
                        txtQtykecil.Text = dt.Rows[i]["txtQtykecil"].ToString();
                        txtsatuan.Text = dt.Rows[i]["txtsatuan"].ToString();
                        txtBudgetPR.Text = dt.Rows[i]["txtBudgetPR"].ToString();
                        txtHargaSatuan.Text = dt.Rows[i]["txtHargaSatuan"].ToString();
                    }
                }
            }
        }
        #endregion

        protected void btndetail_Click(object sender, EventArgs e)
        {

        }

        protected void grdPR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdPR.PageIndex = e.NewPageIndex;
            loadData();
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

        protected void showHideFormKas(bool DivGrid, bool DivForm, bool Div1)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabDD.Visible = Div1;
        }

        protected void showHidePPnPPh(bool DivPPn, bool DivPPh)
        {
            showPajak.Visible = DivPPn;
            showPPh.Visible = DivPPh;

        }

        protected void showDetilTotal(bool DivDetil1, bool DivDetil2)
        {
            showDetil.Visible = DivDetil1;
            showDetil2.Visible = DivDetil2;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            showHideFormKas(true, false, false);
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {

            bool valid = true;
            string message = "", alert = "";

            if (dtPO.Text == "")
            {
                message = ObjSys.CreateMessage("Tgl Beli harus di isi.");
                alert = "error";
                valid = false;
            }
            else if (hdnNoSupp.Value == "")
            {
                message = ObjSys.CreateMessage("Supplier harus di pilih.");
                alert = "error";
                valid = false;
            }
            else
            {
                for (int i = 0; i < grdPRDetil.Rows.Count; i++)
                {
                    Label lblnamaBarang = (Label)grdPRDetil.Rows[i].FindControl("lblnamaBarang");
                    TextBox txtHargaSatuan = (TextBox)grdPRDetil.Rows[i].FindControl("txtHargaSatuan");
                    TextBox txtBudgetPR = (TextBox)grdPRDetil.Rows[i].FindControl("txtBudgetPR");

                    string nilai = "0";
                    if (txtHargaSatuan.Text != "0.00")
                        nilai = Convert.ToDecimal(txtHargaSatuan.Text).ToString();

                    if (nilai == "0")
                    {
                        message = ObjSys.CreateMessage("Harga PO " + lblnamaBarang.Text + " harus > 0.");
                        alert = "error";
                        valid = false;
                    }
                    if (Convert.ToDecimal(txtHargaSatuan.Text) > Convert.ToDecimal(txtBudgetPR.Text))
                    {
                        message = ObjSys.CreateMessage("Harga PO harus lebih kecil atau sama dengan Alokasi Budget");
                        alert = "error";
                        valid = false;
                    }
                }
            }

            try
            {
                if (valid == true)
                {
                    string Kode = "";

                    Kode = ObjSys.GetCodeAutoNumberNew("9", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));
                    ObjDb.Data.Clear();
                    ObjDb.Data.Add("KodePO", Kode);
                    ObjDb.Data.Add("tglPO", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));
                    ObjDb.Data.Add("nosup", hdnNoSupp.Value);
                    ObjDb.Data.Add("Keterangan", txtKeterangan.Text);
                    ObjDb.Data.Add("stsApv", "0");
                    ObjDb.Data.Add("tipePO", cboTransaction.Text);
                    if (cboTransaction.Text == "3")
                    {
                        ObjDb.Data.Add("stsPPh", cboPPh.Text);
                        //default stsppn kosong jika pilih pph
                        ObjDb.Data.Add("stsPPn", "");
                        if (cboPPh.Text == "Ya")
                            ObjDb.Data.Add("prosenPPh", hdnPersenPPh.Value);
                        else
                            ObjDb.Data.Add("prosenPPh", "0");
                        ObjDb.Data.Add("subTotal", Convert.ToDecimal(txtSubTotal.Text).ToString());
                    }
                    else
                    {
                        ObjDb.Data.Add("stsPPn", cboPajak.Text);
                        //default stspph kosong jika pilih ppn
                        ObjDb.Data.Add("stsPPh", "");
                        if (cboPajak.Text == "Ya")
                            ObjDb.Data.Add("prosenPPn", hdnPersenPajak.Value);
                        else
                            ObjDb.Data.Add("prosenPPn", "0");
                        ObjDb.Data.Add("subTotal", Convert.ToDecimal(txtSubTotal.Text).ToString());
                        ObjDb.Data.Add("nilaiPPn", Convert.ToDecimal(txtPPn.Text).ToString());
                        ObjDb.Data.Add("Total", Convert.ToDecimal(txtTotal.Text).ToString());
                    }
                    ObjDb.Data.Add("nocabang", ObjSys.GetCabangId);
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("createddate", ObjSys.GetDate);
                    ObjDb.Insert("TransPO_H", ObjDb.Data);

                    ObjSys.UpdateAutoNumberCodeNew("9", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));


                    DataSet mySetH = ObjDb.GetRows("Select * from transPO_H Where KodePO = '" + Kode + "'");
                    if (mySetH.Tables[0].Rows.Count > 0)
                    {
                        DataRow myRowH = mySetH.Tables[0].Rows[0];
                        string noPO = myRowH["noPO"].ToString();

                        for (int i = 0; i < grdPRDetil.Rows.Count; i++)
                        {
                            TextBox txtkodebrg = (TextBox)grdPRDetil.Rows[i].FindControl("txtkodebrg"); 
                            TextBox txtQtyBesar = (TextBox)grdPRDetil.Rows[i].FindControl("txtQtyBesar");
                            TextBox txtSatuanBesar = (TextBox)grdPRDetil.Rows[i].FindControl("txtSatuanBesar");
                            TextBox txtQty = (TextBox)grdPRDetil.Rows[i].FindControl("txtQty");
                            TextBox txtQtykecil = (TextBox)grdPRDetil.Rows[i].FindControl("txtQtykecil");
                            TextBox txtSatuan = (TextBox)grdPRDetil.Rows[i].FindControl("txtsatuan");
                            TextBox txtHargaSatuan = (TextBox)grdPRDetil.Rows[i].FindControl("txtHargaSatuan");
                            TextBox txtTotalharga = (TextBox)grdPRDetil.Rows[i].FindControl("txtTotalharga");


                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noPO", noPO);
                            DataSet mySetH2 = ObjDb.GetRows("Select nobarang from mbarang Where kodebarang = '" + txtkodebrg.Text + "'");
                            if (mySetH2.Tables[0].Rows.Count > 0)
                            {
                                DataRow myRowH2 = mySetH2.Tables[0].Rows[0];
                                string nobarang = myRowH2["nobarang"].ToString();
                                ObjDb.Data.Add("nobarang", nobarang);
                            }
                            if (cboTransaction.Text == "3")
                            {
                                ObjDb.Data.Add("qtyBesar", "0");
                                ObjDb.Data.Add("satbesar", "");
                                ObjDb.Data.Add("qty", "1");
                            }
                            else
                            {
                                ObjDb.Data.Add("qtyBesar", Convert.ToDecimal(txtQtyBesar.Text).ToString());
                                ObjDb.Data.Add("satbesar", txtSatuanBesar.Text);
                                ObjDb.Data.Add("qty", Convert.ToDecimal(txtQty.Text).ToString());
                                ObjDb.Data.Add("qtykecil", Convert.ToDecimal(txtQtykecil.Text).ToString());
                            }

                            ObjDb.Data.Add("satuan", txtSatuan.Text);
                            ObjDb.Data.Add("hargasatuan", Convert.ToDecimal(txtHargaSatuan.Text).ToString());
                            ObjDb.Data.Add("totalHarga", Convert.ToDecimal(txtTotalharga.Text).ToString());
                            ObjDb.Data.Add("ststeriima", "0");
                            ObjDb.Insert("TransPO_D", ObjDb.Data);

                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("noPRD", hdnPRD.Value);
                            ObjGlobal.Param.Add("noPO", noPO);
                            DataSet mySetH3 = ObjDb.GetRows("Select a.noPOd from TransPO_D a inner join mBarang b on a.nobarang=b.nobarang Where b.kodebarang = '" + txtkodebrg.Text + "' and a.nopo='" + noPO + "' ");
                            if (mySetH3.Tables[0].Rows.Count > 0)
                            {
                                DataRow myRowH3 = mySetH3.Tables[0].Rows[0];
                                string noPOd = myRowH3["noPOd"].ToString();
                                ObjGlobal.Param.Add("noPOd", noPOd);
                            }
                            ObjGlobal.GetDataProcedure("SPUpdatePRDetilPO", ObjGlobal.Param);

                        }

                    }

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data Berhasil Disimpan");
                    showHideFormKas(true, false, false);
                    loadData();
                    ClearData();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage(alert, message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void ClearData()
        {
            dtPO.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            txtKeterangan.Text = "";
            txtSearch.Text = "";
            txtIDSup.Text = "";
            lblNama.Text = "";
            lblAddress.Text = "";
            lblPhoneFax.Text = "";
            hdnPRD.Value = "";
            cboPajak.Text = "Ya";
            cboPPh.Text = "Ya";
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CloseMessage();
            try
            {
                string cek = "", noPRD = "";
                int cekx = 0;
                for (int i = 0; i < grdPR.Rows.Count; i++)
                {
                    HiddenField hdnnoBarang = (HiddenField)grdPR.Rows[i].FindControl("hdnnoBarang");
                    HiddenField hdnnoPRD = (HiddenField)grdPR.Rows[i].FindControl("hdnnoPRD");
                    CheckBox chkBrg = (CheckBox)grdPR.Rows[i].FindControl("chkBrg");

                    if (chkBrg.Checked == true)
                    {
                        cekx++;
                        cek = cek + "," + hdnnoBarang.Value;
                        noPRD = noPRD + "," + hdnnoPRD.Value;
                    }
                }

                if (cekx == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Cek detil transaksi.");
                }
                else
                {
                    CloseMessage();
                    SetInitialRow(cek, noPRD);
                    hdnPRD.Value = noPRD;
                    showHideFormKas(false, true, false);

                    if (cboTransaction.Text == "3")
                        showHidePPnPPh(false, true);
                    else
                        showHidePPnPPh(true, false);

                    if (cboTransaction.Text != "3")
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "CalculatePPn()", "CalculatePPn();", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "CalculatePPh()", "CalculatePPh();", true);

                    if (cboTransaction.Text != "3")
                        showDetilTotal(true, true);
                    else
                        showDetilTotal(false, false);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            showHideFormKas(true, false, false);
            loadReset();
        }

        protected void cboDana_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        protected void cboTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void grdPR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (ObjSys.GetKategori_Usaha == "Sekolah")
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4" || cboTransaction.SelectedValue == "5")
                    {
                        e.Row.Cells[3].Text = "Kode Barang";
                        e.Row.Cells[4].Text = "Nama Barang";
                        e.Row.Cells[5].Visible = true;
                        e.Row.Cells[6].Visible = true;
                        e.Row.Cells[7].Text = "Qty";
                        e.Row.Cells[8].Visible = true;
                        e.Row.Cells[9].Visible = true;
                        e.Row.Cells[10].Visible = true;
                    }
                    else if (cboTransaction.SelectedValue == "3")
                    {
                        e.Row.Cells[3].Text = "Kode Jasa";
                        e.Row.Cells[4].Text = "Nama Jasa";
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = false;
                        e.Row.Cells[7].Text = "Nilai";
                        e.Row.Cells[8].Visible = false;
                        e.Row.Cells[9].Visible = false;
                        e.Row.Cells[10].Visible = true;
                    }
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4" || cboTransaction.SelectedValue == "5")
                    {
                        e.Row.Cells[3].Text = "Kode Barang";
                        e.Row.Cells[4].Text = "Nama Barang";
                        e.Row.Cells[5].Visible = true;
                        e.Row.Cells[6].Visible = true;
                        e.Row.Cells[7].Text = "Qty";
                        e.Row.Cells[8].Visible = true;
                        e.Row.Cells[9].Visible = true;
                        e.Row.Cells[10].Visible = false;
                    }
                    else if (cboTransaction.SelectedValue == "3")
                    {
                        e.Row.Cells[3].Text = "Kode Jasa";
                        e.Row.Cells[4].Text = "Nama Jasa";
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = false;
                        e.Row.Cells[7].Text = "Nilai";
                        e.Row.Cells[8].Visible = false;
                        e.Row.Cells[9].Visible = false;
                        e.Row.Cells[10].Visible = false;
                    }
                }
            }

            if (ObjSys.GetKategori_Usaha == "Sekolah")
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4" || cboTransaction.SelectedValue == "5")
                    {
                        e.Row.Cells[5].Visible = true;
                        e.Row.Cells[6].Visible = true;
                        e.Row.Cells[8].Visible = true;
                        e.Row.Cells[9].Visible = true;
                        e.Row.Cells[10].Visible = true;
                    }
                    else if (cboTransaction.SelectedValue == "3")
                    {
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = false;
                        e.Row.Cells[8].Visible = false;
                        e.Row.Cells[9].Visible = false;
                        e.Row.Cells[10].Visible = true;
                    }
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4" || cboTransaction.SelectedValue == "5")
                    {
                        e.Row.Cells[5].Visible = true;
                        e.Row.Cells[6].Visible = true; 
                        e.Row.Cells[8].Visible = true;
                        e.Row.Cells[9].Visible = true;
                        e.Row.Cells[10].Visible = false;
                    }
                    else if (cboTransaction.SelectedValue == "3")
                    {
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = false; 
                        e.Row.Cells[8].Visible = false;
                        e.Row.Cells[9].Visible = false;
                        e.Row.Cells[10].Visible = false;
                    }
                }
            }
        }

        protected void grdPRDetil_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4")
                {
                    e.Row.Cells[1].Text = "Kode Barang";
                    e.Row.Cells[2].Text = "Nama Barang";
                    e.Row.Cells[3].Text = "Qty";
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[5].Text = "Harga Satuan";
                    e.Row.Cells[7].Visible = true;
                }
                else if (cboTransaction.SelectedValue == "3")
                {
                    e.Row.Cells[1].Text = "Kode Jasa";
                    e.Row.Cells[2].Text = "Nama Jasa";
                    e.Row.Cells[3].Text = "Nilai";
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Text = "Nilai Input";
                    e.Row.Cells[7].Visible = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4")
                {
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[7].Visible = true;
                }
                else if (cboTransaction.SelectedValue == "3")
                {
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[7].Visible = false;
                }
            }
        }

        protected void btnBatal_Click(object sender, EventArgs e)
        {
            loadReset();
        }

        protected void loadReset()
        {
            CloseMessage();
            ClearData();
            for (int i = 0; i < grdPR.Rows.Count; i++)
            {
                CheckBox chkBrg = (CheckBox)grdPR.Rows[i].FindControl("chkBrg");

                chkBrg.Checked = false;
            }
        }

        protected void cboPajak_TextChanged(object sender, EventArgs e)
        {
            loadPajak(cboPajak.Text);
        }

        protected void loadPajak(string Pajak = "")
        {
            decimal subTotal = 0, PPn = 0, Total = 0;
            for (int i = 0; i < grdPRDetil.Rows.Count; i++)
            {
                TextBox txtTotalharga = (TextBox)grdPRDetil.Rows[i].FindControl("txtTotalharga");

                subTotal += Convert.ToDecimal(txtTotalharga.Text);
            }

            txtSubTotal.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(subTotal).ToString());

            if (Pajak == "Tidak")
            {
                PPn = 0;
                Total = Convert.ToDecimal(subTotal + PPn);
                txtPPn.Text = ObjSys.IsFormatNumber(PPn.ToString());
                txtTotal.Text = ObjSys.IsFormatNumber(Total.ToString());
            }
            else
            {
                PPn = ((Convert.ToDecimal(subTotal.ToString()) * Convert.ToDecimal(hdnPersenPajak.Value) / 100));
                Total = Convert.ToDecimal(subTotal + PPn);
                txtPPn.Text = ObjSys.IsFormatNumber(PPn.ToString());
                txtTotal.Text = ObjSys.IsFormatNumber(Total.ToString());
            }
           
        }

    }
}