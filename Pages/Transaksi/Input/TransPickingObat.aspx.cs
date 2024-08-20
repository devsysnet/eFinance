using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eFinance.Pages.Transaksi.Input
{

    public partial class TransPickingObat : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ShowHideGridAndForm(true, false);
                loadAwal();
            }
        }

        private void SetInitialRow(string id = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKode", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnKode", typeof(string)));
            dt.Columns.Add(new DataColumn("lblDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("lblStok", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQty", typeof(string)));
            dt.Columns.Add(new DataColumn("lblSatuan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtHrg", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKet", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnIndexSA", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDetilObat", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtKode"] = myRow["kodeBarang"].ToString();
                dr["hdnKode"] = myRow["noBarang"].ToString();
                dr["lblDescription"] = myRow["namaBarang"].ToString();
                dr["lblStok"] = ObjSys.IsFormatNumber(myRow["stok"].ToString());
                dr["txtQty"] = ObjSys.IsFormatNumber(myRow["qty"].ToString());
                dr["lblSatuan"] = myRow["satuan"].ToString();
                dr["txtHrg"] = ObjSys.IsFormatNumber(myRow["harga"].ToString());
                dr["txtKet"] = myRow["keterangan"].ToString();
                dr["hdnIndexSA"] = myRow["noSA"].ToString();
                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdObat.DataSource = dt;
            grdObat.DataBind();

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
                        HiddenField hdnKode = (HiddenField)grdObat.Rows[i].FindControl("hdnKode");
                        TextBox txtKode = (TextBox)grdObat.Rows[i].FindControl("txtKode");
                        Label lblDescription = (Label)grdObat.Rows[i].FindControl("lblDescription");
                        Label lblStok = (Label)grdObat.Rows[i].FindControl("lblStok");
                        TextBox txtQty = (TextBox)grdObat.Rows[i].FindControl("txtQty");
                        Label lblSatuan = (Label)grdObat.Rows[i].FindControl("lblSatuan");
                        TextBox txtHrg = (TextBox)grdObat.Rows[i].FindControl("txtHrg");
                        TextBox txtKet = (TextBox)grdObat.Rows[i].FindControl("txtKet");
                        HiddenField hdnIndexSA = (HiddenField)grdObat.Rows[i].FindControl("hdnIndexSA");

                        txtKode.Text = dt.Rows[i]["txtKode"].ToString();
                        hdnKode.Value = dt.Rows[i]["hdnKode"].ToString();
                        lblDescription.Text = dt.Rows[i]["lblDescription"].ToString();
                        lblStok.Text = dt.Rows[i]["lblStok"].ToString();
                        txtQty.Text = dt.Rows[i]["txtQty"].ToString();
                        lblSatuan.Text = dt.Rows[i]["lblSatuan"].ToString();
                        txtHrg.Text = dt.Rows[i]["txtHrg"].ToString();
                        txtKet.Text = dt.Rows[i]["txtKet"].ToString();
                        hdnIndexSA.Value = dt.Rows[i]["hdnIndexSA"].ToString();
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
                        HiddenField hdnKode = (HiddenField)grdObat.Rows[i].FindControl("hdnKode");
                        TextBox txtKode = (TextBox)grdObat.Rows[i].FindControl("txtKode");
                        Label lblDescription = (Label)grdObat.Rows[i].FindControl("lblDescription");
                        Label lblStok = (Label)grdObat.Rows[i].FindControl("lblStok");
                        TextBox txtQty = (TextBox)grdObat.Rows[i].FindControl("txtQty");
                        Label lblSatuan = (Label)grdObat.Rows[i].FindControl("lblSatuan");
                        TextBox txtHrg = (TextBox)grdObat.Rows[i].FindControl("txtHrg");
                        TextBox txtKet = (TextBox)grdObat.Rows[i].FindControl("txtKet");
                        HiddenField hdnIndexSA = (HiddenField)grdObat.Rows[i].FindControl("hdnIndexSA");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["txtKode"] = txtKode.Text;
                        dtCurrentTable.Rows[i]["hdnKode"] = hdnKode.Value;
                        dtCurrentTable.Rows[i]["lblDescription"] = lblDescription.Text;
                        dtCurrentTable.Rows[i]["lblStok"] = lblStok.Text;
                        dtCurrentTable.Rows[i]["txtQty"] = txtQty.Text;
                        dtCurrentTable.Rows[i]["lblSatuan"] = lblSatuan.Text;
                        dtCurrentTable.Rows[i]["txtHrg"] = txtHrg.Text;
                        dtCurrentTable.Rows[i]["txtKet"] = txtKet.Text;
                        dtCurrentTable.Rows[i]["hdnIndexSA"] = hdnIndexSA.Value;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdObat.DataSource = dtCurrentTable;
                    grdObat.DataBind();
                }
            }
            SetPreviousData();
        }

        protected void loadAwal()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nokaryawan", ObjSys.GetUserId);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdRegister.DataSource = ObjGlobal.GetDataProcedure("SPLoadRegisterPicking", ObjGlobal.Param);
            grdRegister.DataBind();
        }

        protected void loadAwalHistory(string nopasien)
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noPasien", nopasien);
            grdRegisterHistory.DataSource = ObjGlobal.GetDataProcedure("SPLoadRegisterHistory", ObjGlobal.Param);
            grdRegisterHistory.DataBind();
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void grdRegister_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRegister.PageIndex = e.NewPageIndex;
            loadAwal();
        }

        protected void grdRegister_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectDetil")
            {
                CloseMessage();
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                string noMedik = grdRegister.DataKeys[rowIndex].Value.ToString();
                hdnId.Value = noMedik;

                DataSet mySet = ObjDb.GetRows("select * from Transmedik_h a inner join mPasien b on a.noPasien=b.noPasien where a.noMedik = '" + hdnId.Value + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];

                hdnReg.Value = myRow["noPasien"].ToString();
                txtNoReg.Text = myRow["noRegister"].ToString();
                txtNamaReg.Text = myRow["namaPasien"].ToString();
                txtAlamatReg.Text = myRow["alamat"].ToString();
                txtThn.Text = myRow["umurth"].ToString();
                txtBln.Text = myRow["umurbln"].ToString();
                dtPeriksa.Text = Convert.ToDateTime(myRow["tglmedik"]).ToString("dd-MMM-yyyy");
                txtBerat.Text = myRow["beratbadan"].ToString();
                txtTinggi.Text = myRow["tinggibadan"].ToString();
                txtShBadan.Text = myRow["suhubadan"].ToString();
                txttekanan.Text = myRow["tekanandarah"].ToString();
                txtAnamnesa.Text = myRow["anamnesa"].ToString();
                txtDiagnosa.Text = myRow["diagnosa"].ToString();
                txtTindakan.Text = myRow["tindakan"].ToString();

                txtBiaya.Text = ObjSys.IsFormatNumber(myRow["biayaTindakan"].ToString());
                txtBiayaObat.Text = ObjSys.IsFormatNumber(myRow["biayaObat"].ToString());
                txtTotal.Text = ObjSys.IsFormatNumber(myRow["TotalBiaya"].ToString());

                ShowHideGridAndForm(false, true);
                SetInitialRow(hdnId.Value);
                loadAwalHistory(hdnReg.Value);

            }
        }

        protected void grdObat_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            hdnParameterObat.Value = rowIndex.ToString();
            if (e.CommandName == "Select")
            {
                loadDataObat();
                dlgObat.Show();
            }
        }

        protected void loadDataObat()
        {
            grdObatDetil.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataObat");
            grdObatDetil.DataBind();
        }

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void grdObatDetil_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndexHdn = Convert.ToInt32(hdnParameterObat.Value);
            int rowIndex = grdObatDetil.SelectedRow.RowIndex;

            string kdObat = (grdObatDetil.SelectedRow.FindControl("lblKdObat") as Label).Text;
            string Obat = (grdObatDetil.SelectedRow.FindControl("lblKet") as Label).Text;
            string noObat = (grdObatDetil.SelectedRow.FindControl("hdnNoObat") as HiddenField).Value;
            string Satuan = (grdObatDetil.SelectedRow.FindControl("lblSat") as Label).Text;
            string Qty = (grdObatDetil.SelectedRow.FindControl("hdnQty") as HiddenField).Value;
            string Harga = (grdObatDetil.SelectedRow.FindControl("hdnHarga") as HiddenField).Value;  
            string indexSA = (grdObatDetil.SelectedRow.FindControl("hdnNoSA") as HiddenField).Value;

            HiddenField hdnKode = (HiddenField)grdObat.Rows[rowIndexHdn].FindControl("hdnKode");
            TextBox txtKode = (TextBox)grdObat.Rows[rowIndexHdn].FindControl("txtKode");
            Label lblDescription = (Label)grdObat.Rows[rowIndexHdn].FindControl("lblDescription");
            Label lblSatuan = (Label)grdObat.Rows[rowIndexHdn].FindControl("lblSatuan");
            Label lblStok = (Label)grdObat.Rows[rowIndexHdn].FindControl("lblStok");
            TextBox txtHrg = (TextBox)grdObat.Rows[rowIndexHdn].FindControl("txtHrg");
            HiddenField hdnIndexSA = (HiddenField)grdObat.Rows[rowIndexHdn].FindControl("hdnIndexSA"); 

            hdnKode.Value = noObat;
            txtKode.Text = kdObat;
            lblDescription.Text = Obat;
            lblStok.Text = ObjSys.IsFormatNumber(Qty);
            lblSatuan.Text = Satuan;
            txtHrg.Text = ObjSys.IsFormatNumber(Harga);
            hdnIndexSA.Value = indexSA;
            dlgObat.Hide();


        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

            ShowHideGridAndForm(true, false);
            //ClearData();
        }

        protected void ClearData()
        {
            hdnId.Value = "0";
            txtBerat.Text = "";
            txtShBadan.Text = "";
            txttekanan.Text = "";
            txtAnamnesa.Text = "";
            txtDiagnosa.Text = "";
            txtTindakan.Text = "";
            SetInitialRow();
            for (int i = 1; i < 3; i++)
            {
                AddNewRow();
            }
            txtBiaya.Text = "0.00";
            txtBiayaObat.Text = "0.00";
            txtTotal.Text = "0.00";
        }

        protected void btnProses_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "";
            int cekData = 0;

            for (int i = 0; i < grdObat.Rows.Count; i++)
            {
                TextBox txtQty = (TextBox)grdObat.Rows[i].FindControl("txtQty");
                TextBox txtHrg = (TextBox)grdObat.Rows[i].FindControl("txtHrg");
                TextBox txtKode = (TextBox)grdObat.Rows[i].FindControl("txtKode");
                if (txtKode.Text != "" || txtQty.Text != "" || txtHrg.Text != "")
                {
                    cekData++;
                }
            }

            if (cekData == 0)
            {
                message = ObjSys.CreateMessage("Tidak ada data yang dipilih.");
                valid = false;
            }

            try
            {
                if (valid == true && cekData > 0)
                {

                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noMedik", hdnId.Value);
                    ObjDb.Data.Add("BiayaTindakan", Convert.ToDecimal(txtBiaya.Text).ToString());
                    ObjDb.Data.Add("BiayaObat", Convert.ToDecimal(txtBiayaObat.Text).ToString());
                    ObjDb.Data.Add("TotalBiaya", Convert.ToDecimal(txtTotal.Text).ToString());
                    ObjDb.Data.Add("sts", "2");
                    ObjDb.Data.Add("modiBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modidate", ObjSys.GetNow);
                    ObjDb.Update("Transmedik_h", ObjDb.Data, ObjDb.Where);


                    for (int i = 0; i < grdObat.Rows.Count; i++)
                    {
                        HiddenField hdnKode = (HiddenField)grdObat.Rows[i].FindControl("hdnKode");
                        TextBox txtQty = (TextBox)grdObat.Rows[i].FindControl("txtQty");
                        TextBox txtHrg = (TextBox)grdObat.Rows[i].FindControl("txtHrg");
                        TextBox txtKet = (TextBox)grdObat.Rows[i].FindControl("txtKet");
                        Label lblSatuan = (Label)grdObat.Rows[i].FindControl("lblSatuan");
                        Label lblStok = (Label)grdObat.Rows[i].FindControl("lblStok");
                        HiddenField hdnIndexSA = (HiddenField)grdObat.Rows[i].FindControl("hdnIndexSA");

                        if (hdnKode.Value != "" && hdnIndexSA.Value != "")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noMedik", hdnIndexSA.Value);
                            ObjDb.Delete("Transmedik_Obat", ObjDb.Where);

                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noMedik", hdnId.Value);
                            ObjDb.Data.Add("noBrg", hdnKode.Value);
                            ObjDb.Data.Add("qty", Convert.ToDecimal(txtQty.Text).ToString());
                            ObjDb.Data.Add("satuan", lblSatuan.Text);
                            ObjDb.Data.Add("harga", Convert.ToDecimal(txtHrg.Text).ToString());
                            ObjDb.Data.Add("totalharga", Convert.ToDecimal(Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtHrg.Text)).ToString());
                            ObjDb.Data.Add("keterangan", txtKet.Text);
                            ObjDb.Data.Add("noSA", hdnIndexSA.Value);
                            ObjDb.Insert("Transmedik_Obat", ObjDb.Data);

                            ObjDb.Data.Clear();
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noSA", hdnIndexSA.Value);
                            ObjDb.Where.Add("noProduct", hdnKode.Value);
                            ObjDb.Data.Add("sisaSASMT", Convert.ToDecimal(Convert.ToDecimal(lblStok.Text) - Convert.ToDecimal(txtQty.Text)).ToString());
                            ObjDb.Update("tSaldoAging", ObjDb.Data, ObjDb.Where);
                        }
                        
                    }
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan");
                    ClearData();
                    ShowHideGridAndForm(true, false);
                    loadAwal();
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
    }
}