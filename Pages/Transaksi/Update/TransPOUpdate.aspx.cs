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
using System.IO;

namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransPOUpdate : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        //buatcetakan
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCombo();
                dtMulai.Text = Convert.ToDateTime(ObjSys.GetDate).ToString("dd-MMM-yyyy");
                dtSampai.Text = Convert.ToDateTime(ObjSys.GetDate).ToString("dd-MMM-yyyy");
                loadData();

            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            grdPO.DataSource = ObjGlobal.GetDataProcedure("SPViewPOUpdate", ObjGlobal.Param);
            grdPO.DataBind();

        }

        protected void loadCombo()
        {
            cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang= '" + ObjSys.GetCabangId + "') a");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();
        }

        #region setInitial & AddRow
        private void SetInitialRow(string Id = "", string jnsPO = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnnoPOD", typeof(string)));
            dt.Columns.Add(new DataColumn("txtkodebrg", typeof(string)));
            dt.Columns.Add(new DataColumn("lblnamaBarang", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQtyBesar", typeof(string)));
            dt.Columns.Add(new DataColumn("txtsatuanBesar", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQty", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQtykecil", typeof(string)));
            dt.Columns.Add(new DataColumn("txtStn", typeof(string)));
            dt.Columns.Add(new DataColumn("txtBudgetPR", typeof(string)));
            dt.Columns.Add(new DataColumn("txthargaPO", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTotal", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("Id", Id);
            ObjGlobal.Param.Add("jnsPO", jnsPO);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPViewpodetail", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnnoPOD"] = myRow["noPOD"].ToString();
                dr["txtkodebrg"] = myRow["kodeBarang"].ToString();
                dr["lblnamaBarang"] = myRow["namaBarang"].ToString();
                dr["txtsatuanBesar"] = myRow["satbesar"].ToString();
                dr["txtQtyBesar"] = ObjSys.IsFormatNumber(myRow["qtybesar"].ToString());
                dr["txtQty"] = ObjSys.IsFormatNumber(myRow["qty"].ToString());
                dr["txtQtykecil"] = ObjSys.IsFormatNumber(myRow["qtykecil"].ToString());
                dr["txtStn"] = myRow["satuan"].ToString();
                dr["txtBudgetPR"] = ObjSys.IsFormatNumber(myRow["budget"].ToString());
                dr["txthargaPO"] = ObjSys.IsFormatNumber(myRow["hargasatuan"].ToString());
                dr["txtTotal"] = ObjSys.IsFormatNumber(myRow["total"].ToString());

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnnoPOD"] = 0;
                dr["txtkodebrg"] = string.Empty;
                dr["lblnamaBarang"] = string.Empty;
                dr["txtQtyBesar"] = string.Empty;
                dr["txtsatuanBesar"] = string.Empty;
                dr["txtQty"] = string.Empty;
                dr["txtQtykecil"] = string.Empty;
                dr["txtStn"] = string.Empty;
                dr["txtBudgetPR"] = string.Empty;
                dr["txthargaPO"] = string.Empty;
                dr["txtTotal"] = string.Empty;

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdPODetil.DataSource = dt;
            grdPODetil.DataBind();

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
                        HiddenField hdnnoPOD = (HiddenField)grdPODetil.Rows[i].FindControl("hdnnoPOD");
                        TextBox txtkodebrg = (TextBox)grdPODetil.Rows[i].FindControl("txtkodebrg");
                        Label lblnamaBarang = (Label)grdPODetil.Rows[i].FindControl("lblnamaBarang");
                        TextBox txtQtyBesar = (TextBox)grdPODetil.Rows[i].FindControl("txtQtyBesar");
                        TextBox txtsatuanBesar = (TextBox)grdPODetil.Rows[i].FindControl("txtsatuanBesar");
                        TextBox txtStn = (TextBox)grdPODetil.Rows[i].FindControl("txtStn");
                        TextBox txtQty = (TextBox)grdPODetil.Rows[i].FindControl("txtQty");
                        TextBox txtQtykecil = (TextBox)grdPODetil.Rows[i].FindControl("txtQtykecil");
                        TextBox txtBudgetPR = (TextBox)grdPODetil.Rows[i].FindControl("txtBudgetPR");
                        TextBox txthargaPO = (TextBox)grdPODetil.Rows[i].FindControl("txthargaPO");
                        TextBox txtTotal = (TextBox)grdPODetil.Rows[i].FindControl("txtTotal");

                        hdnnoPOD.Value = dt.Rows[i]["hdnnoPOD"].ToString();
                        txtkodebrg.Text = dt.Rows[i]["txtkodebrg"].ToString();
                        lblnamaBarang.Text = dt.Rows[i]["lblnamaBarang"].ToString();
                        txtQtyBesar.Text = dt.Rows[i]["txtQtyBesar"].ToString();
                        txtsatuanBesar.Text = dt.Rows[i]["txtsatuanBesar"].ToString();
                        txtQty.Text = dt.Rows[i]["txtQty"].ToString();
                        txtQtykecil.Text = dt.Rows[i]["txtQtykecil"].ToString();
                        txtStn.Text = dt.Rows[i]["txtStn"].ToString();
                        txtBudgetPR.Text = dt.Rows[i]["txtBudgetPR"].ToString();
                        txthargaPO.Text = dt.Rows[i]["txthargaPO"].ToString();
                        txtTotal.Text = dt.Rows[i]["txtTotal"].ToString();
                    }
                }
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void grdPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPO.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void grdPO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SelectEdit")
                {
                    CloseMessage();
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string id = grdPO.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = id;

                    DataSet mySet = ObjDb.GetRows("select a.*,b.kodeSupplier,b.namaSupplier,b.alamat,b.telpKantor from TransPO_H a inner join mSupplier b on a.nosup=b.nosupplier where noPO = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtKodePO.Text = myRow["kodePO"].ToString();
                    hdnNoSupp.Value = myRow["noSup"].ToString();
                    txtIDSup.Text = myRow["kodeSupplier"].ToString();
                    lblNama.Text = myRow["namaSupplier"].ToString();
                    lblAddress.Text = myRow["alamat"].ToString();
                    lblPhoneFax.Text = myRow["telpKantor"].ToString();
                    txtKeterangan.Text = myRow["Keterangan"].ToString();
                    dtPO.Text = Convert.ToDateTime(myRow["tglPO"]).ToString("dd-MMM-yyyy");
                    hdntipePO.Value = myRow["tipePO"].ToString();
                    
                    SetInitialRow(hdnId.Value, hdntipePO.Value);
                    
                    // ditutup karena Nilai PO dari input tidak harus perkalian qty*hargapo,
                    //tetapi bisa diubah
                    //ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
                    showHideFormKas(false, true);

                    if (hdntipePO.Value == "3")
                    {
                        cboPPh.Text = myRow["stsPPh"].ToString();
                        showHidePPnPPh(false, true);
                    }
                    else
                    {
                        cboPajak.Text = myRow["stsPPn"].ToString();
                        showHidePPnPPh(true, false);
                    }

                    loadPajakPPh();

                    if (hdntipePO.Value != "3")
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "CalculatePPn()", "CalculatePPn();", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "CalculatePPh()", "CalculatePPh();", true);

                    if (hdntipePO.Value != "3")
                        showDetilTotal(true, true);
                    else
                        showDetilTotal(false, false);

                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string id = grdPO.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = id;

                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noPO", hdnId.Value);
                    DataSet mySetH2 = ObjDb.GetRows("Select * from TransPR_D Where noPO = '" + hdnId.Value + "' and kodeTerima is null");
                    if (mySetH2.Tables[0].Rows.Count > 0)
                    {
                        DataRow myRowH2 = mySetH2.Tables[0].Rows[0];
                        string sql = "update TransPR_D set sts = 0, stsD = 1, noPO = null, noPOD = null where noPO = '" + hdnId.Value + "'";
                        ObjDb.ExecQuery(sql);
                    }
                    ObjDb.Delete("TransPO_D", ObjDb.Where);
                    ObjDb.Delete("TransPO_H", ObjDb.Where);

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Success Deleted");
                    showHideFormKas(true, false);
                    loadData();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.ToString());
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
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

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            showHideFormKas(true, false);
        }

        protected void imgButtonSup_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataPanel();
            dlgSupplier.Show();
        }

        private void LoadDataPanel()
        {
            grdSupp.DataSource = ObjDb.GetRows("select * from mSupplier where (kodeSupplier like '%"+ txtSearch.Text + "%' or namaSupplier like '%" + txtSearch.Text + "%')");
            grdSupp.DataBind();
        }

        protected void grdSupp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string namaCust = "", noCust = "", kodeCust = "", addres = "",  telp = "";
            kodeCust = (grdSupp.SelectedRow.FindControl("lblKodeSup") as Label).Text;
            namaCust = (grdSupp.SelectedRow.FindControl("lblNamaSup") as Label).Text;
            noCust = (grdSupp.SelectedRow.FindControl("hidNoSup") as HiddenField).Value;
            addres = (grdSupp.SelectedRow.FindControl("lblaAddress") as Label).Text;
            telp = (grdSupp.SelectedRow.FindControl("lblNoTel") as Label).Text;

            lblNama.Text = namaCust;
            hdnNoSupp.Value = noCust;
            txtIDSup.Text = kodeCust;
            lblAddress.Text = addres;
            lblPhoneFax.Text = telp;
            dlgSupplier.Hide();
        }

        protected void grdSupp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSupp.PageIndex = e.NewPageIndex;
            LoadDataPanel();
            dlgSupplier.Show();
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataPanel();
            dlgSupplier.Show();
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
                for (int i = 0; i < grdPODetil.Rows.Count; i++)
                {
                    Label lblnamaBarang = (Label)grdPODetil.Rows[i].FindControl("lblnamaBarang");
                    TextBox txthargaPO = (TextBox)grdPODetil.Rows[i].FindControl("txthargaPO");
                    TextBox txtBudgetPR = (TextBox)grdPODetil.Rows[i].FindControl("txtBudgetPR");

                    string nilai = "0";
                    if (txthargaPO.Text != "0.00")
                        nilai = Convert.ToDecimal(txthargaPO.Text).ToString();

                    if (nilai == "0")
                    {
                        message = ObjSys.CreateMessage("Harga PO " + lblnamaBarang.Text + " harus > 0.");
                        alert = "error";
                        valid = false;
                    }
                    if (Convert.ToDecimal(txthargaPO.Text) > Convert.ToDecimal(txtBudgetPR.Text))
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

                    ObjDb.Data.Clear();
                    ObjDb.Where.Add("noPO", hdnId.Value);
                    ObjDb.Data.Add("tglPO", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));
                    ObjDb.Data.Add("nosup", hdnNoSupp.Value);
                    ObjDb.Data.Add("Keterangan", txtKeterangan.Text);
                    if (hdntipePO.Value == "3")
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
                    ObjDb.Data.Add("modiBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modidate", ObjSys.GetDate);
                    ObjDb.Update("TransPO_H", ObjDb.Data, ObjDb.Where);

                    for (int i = 0; i < grdPODetil.Rows.Count; i++)
                    {
                        HiddenField hdnnoPOD = (HiddenField)grdPODetil.Rows[i].FindControl("hdnnoPOD");
                        TextBox txtkodebrg = (TextBox)grdPODetil.Rows[i].FindControl("txtkodebrg"); 
                        TextBox txtQtyBesar = (TextBox)grdPODetil.Rows[i].FindControl("txtQtyBesar");
                        TextBox txtSatuanBesar = (TextBox)grdPODetil.Rows[i].FindControl("txtSatuanBesar");
                        TextBox txtQty = (TextBox)grdPODetil.Rows[i].FindControl("txtQty");
                        TextBox txtQtykecil = (TextBox)grdPODetil.Rows[i].FindControl("txtQtykecil");
                        TextBox txtStn = (TextBox)grdPODetil.Rows[i].FindControl("txtStn");
                        TextBox txthargaPO = (TextBox)grdPODetil.Rows[i].FindControl("txthargaPO");
                        TextBox txtTotal = (TextBox)grdPODetil.Rows[i].FindControl("txtTotal");

                        ObjDb.Data.Clear();
                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("noPOD", hdnnoPOD.Value);
                        DataSet mySetH2 = ObjDb.GetRows("Select nobarang from mbarang Where kodeBarang = '" + txtkodebrg.Text + "'");
                        if (mySetH2.Tables[0].Rows.Count > 0)
                        {
                            DataRow myRowH2 = mySetH2.Tables[0].Rows[0];
                            string nobarang = myRowH2["nobarang"].ToString();
                            ObjDb.Data.Add("nobarang", nobarang);
                        }

                        if (hdntipePO.Value == "3")
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

                        ObjDb.Data.Add("satuan", txtStn.Text);
                        ObjDb.Data.Add("hargasatuan", Convert.ToDecimal(txthargaPO.Text).ToString());
                        ObjDb.Data.Add("totalHarga", Convert.ToDecimal(txtTotal.Text).ToString());
                        ObjDb.Data.Add("ststeriima", "0");
                        ObjDb.Update("TransPO_D", ObjDb.Data, ObjDb.Where);

                    }

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diubah");
                    showHideFormKas(true, false);
                    loadData();

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

        protected void grdPODetil_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (hdntipePO.Value == "1")
                {
                    e.Row.Cells[1].Text = "Kode Barang";
                    e.Row.Cells[2].Text = "Nama Barang";
                    e.Row.Cells[3].Visible = true;
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[5].Text = "Qty Kecil"; 
                    e.Row.Cells[6].Visible = true;
                    e.Row.Cells[7].Visible = true;
                    e.Row.Cells[8].Text = "Harga Satuan";
                    e.Row.Cells[10].Visible = true;
                }
                else if (hdntipePO.Value == "3")
                {
                    e.Row.Cells[1].Text = "Kode Jasa";
                    e.Row.Cells[2].Text = "Nama Jasa";
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Text = "Nilai";
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[8].Text = "Nilai Input";
                    e.Row.Cells[10].Visible = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (hdntipePO.Value == "1")
                {
                    e.Row.Cells[3].Visible = true;
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[6].Visible = true;
                    e.Row.Cells[7].Visible = true;
                    e.Row.Cells[10].Visible = true;
                }
                else if (hdntipePO.Value == "3")
                {
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[10].Visible = false;
                }
            }

        }

        protected void cboPajak_TextChanged(object sender, EventArgs e)
        {
            loadPajak(cboPajak.Text);
        }

        protected void loadPajak(string Pajak = "")
        {
            decimal subTotal = 0, PPn = 0, Total = 0;
            for (int i = 0; i < grdPODetil.Rows.Count; i++)
            {
                TextBox txtTotalharga = (TextBox)grdPODetil.Rows[i].FindControl("txtTotalharga");

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