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
    public partial class TransPelunasanPiutang : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        #region setInitial
        private void SetInitialRow(string Id = "", string noMataUang = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnPiutang", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnNoCus", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnNoMataUang", typeof(string)));
            dt.Columns.Add(new DataColumn("lblFaktur", typeof(string)));
            dt.Columns.Add(new DataColumn("lblNilaiFaktur", typeof(string)));
            dt.Columns.Add(new DataColumn("lblKurs", typeof(string)));
            dt.Columns.Add(new DataColumn("txtSaldoPiut", typeof(string)));
            dt.Columns.Add(new DataColumn("txtNilaiBayar", typeof(string)));
            dt.Columns.Add(new DataColumn("txtBank", typeof(string)));
            dt.Columns.Add(new DataColumn("txtPendapatan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtUangMuka", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTotalBayar", typeof(string)));
            dt.Columns.Add(new DataColumn("txtSaldo", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", Id);
            ObjGlobal.Param.Add("noMataUang", noMataUang);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataPelunasanPiutangGridD", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnPiutang"] = myRow["noPiutang"].ToString();
                dr["hdnNoCus"] = myRow["noCus"].ToString();
                dr["hdnNoMataUang"] = myRow["noMataUang"].ToString();
                dr["lblFaktur"] = myRow["noTrans"].ToString();
                dr["lblNilaiFaktur"] = ObjSys.IsFormatNumber(myRow["Nilai"].ToString());
                dr["lblKurs"] = ObjSys.IsFormatNumber(myRow["kursPiutang"].ToString());
                dr["txtSaldoPiut"] = ObjSys.IsFormatNumber(myRow["nSaldoPiutang"].ToString());
                dr["txtNilaiBayar"] = "0.00";
                dr["txtBank"] = "0.00";
                dr["txtPendapatan"] = "0.00";
                dr["txtUangMuka"] = "0.00";
                dr["txtTotalBayar"] = "0.00";
                dr["txtSaldo"] = "0.00";

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnPiutang"] = string.Empty;
                dr["hdnNoCus"] = string.Empty;
                dr["hdnNoMataUang"] = string.Empty;
                dr["lblFaktur"] = string.Empty;
                dr["lblNilaiFaktur"] = string.Empty;
                dr["lblKurs"] = string.Empty;
                dr["txtSaldoPiut"] = "0.00";
                dr["txtNilaiBayar"] = "0.00";
                dr["txtBank"] = "0.00";
                dr["txtPendapatan"] = "0.00";
                dr["txtUangMuka"] = "0.00";
                dr["txtTotalBayar"] = "0.00";
                dr["txtSaldo"] = "0.00";

                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grdPelunasanDetail.DataSource = dt;
            grdPelunasanDetail.DataBind();

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
                        HiddenField hdnPiutang = (HiddenField)grdPelunasanDetail.Rows[i].FindControl("hdnPiutang");
                        HiddenField hdnNoCus = (HiddenField)grdPelunasanDetail.Rows[i].FindControl("hdnNoCus");
                        HiddenField hdnNoMataUang = (HiddenField)grdPelunasanDetail.Rows[i].FindControl("hdnNoMataUang");
                        Label lblFaktur = (Label)grdPelunasanDetail.Rows[i].FindControl("lblFaktur");
                        Label lblNilaiFaktur = (Label)grdPelunasanDetail.Rows[i].FindControl("lblNilaiFaktur");
                        Label lblKurs = (Label)grdPelunasanDetail.Rows[i].FindControl("lblKurs");
                        TextBox txtSaldoPiut = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtSaldoPiut");
                        TextBox txtNilaiBayar = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtNilaiBayar");
                        TextBox txtBank = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtBank");
                        TextBox txtPendapatan = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtPendapatan");
                        TextBox txtUangMuka = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtUangMuka");
                        TextBox txtTotalBayar = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtTotalBayar");
                        TextBox txtSaldo = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtSaldo");
                        CheckBox check = (CheckBox)grdPelunasanDetail.Rows[i].FindControl("chkCheck");

                        hdnPiutang.Value = dt.Rows[i]["hdnPiutang"].ToString();
                        hdnNoCus.Value = dt.Rows[i]["hdnNoCus"].ToString();
                        hdnNoMataUang.Value = dt.Rows[i]["hdnNoMataUang"].ToString();
                        lblFaktur.Text = dt.Rows[i]["lblFaktur"].ToString();
                        lblNilaiFaktur.Text = dt.Rows[i]["lblNilaiFaktur"].ToString();
                        lblKurs.Text = dt.Rows[i]["lblKurs"].ToString();
                        txtSaldoPiut.Text = dt.Rows[i]["txtSaldoPiut"].ToString();
                        txtNilaiBayar.Text = dt.Rows[i]["txtNilaiBayar"].ToString();
                        txtBank.Text = dt.Rows[i]["txtBank"].ToString();
                        txtPendapatan.Text = dt.Rows[i]["txtPendapatan"].ToString();
                        txtUangMuka.Text = dt.Rows[i]["txtUangMuka"].ToString();
                        txtTotalBayar.Text = dt.Rows[i]["txtTotalBayar"].ToString();
                        txtSaldo.Text = dt.Rows[i]["txtSaldo"].ToString();
                    }
                }
            }
        }
        #endregion

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdPelunasan.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPelunasanPiutang1", ObjGlobal.Param);
            grdPelunasan.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void grdPelunasan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdPelunasan.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void grdPelunasan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdPelunasan.SelectedRow.RowIndex;
                string Id = grdPelunasan.DataKeys[rowIndex].Values[0].ToString();
                HiddenField hdnMataUang = (HiddenField)grdPelunasan.Rows[rowIndex].FindControl("hdnMataUang");
                HiddenField hdnCust = (HiddenField)grdPelunasan.Rows[rowIndex].FindControl("hdnCust");
                hdnId.Value = Id;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("Id", hdnId.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataPelunasanPiutangD1", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                lblKodeBayar.Text = myRow["nomorkode"].ToString();
                lblCustomer.Text = myRow["customer"].ToString();
                lblNilaiBayar.Text = ObjSys.IsFormatNumber(myRow["nilaipelunasan"].ToString());
                hdnNilaiBayar.Value = myRow["nilaipelunasan"].ToString();
                lblTanggal.Text = Convert.ToDateTime(myRow["tgl"]).ToString("dd-MMM-yyyy");

                SetInitialRow(hdnCust.Value, hdnMataUang.Value);
                showHideForm(false, true);
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

        protected void ClearData()
        {
            CloseMessage();

            LoadData();
            SetInitialRow("0", "0");
            showHideForm(true, false);
        }

        protected void showHideForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                bool valid = true;

                string totalbayar = "0";
                for (int i = 0; i < grdPelunasanDetail.Rows.Count; i++)
                {
                    CheckBox check = (CheckBox)grdPelunasanDetail.Rows[i].FindControl("chkCheck");

                    if (check.Checked == true)
                    {
                        TextBox txtTotalBayar = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtTotalBayar");
                        totalbayar += Convert.ToDecimal(txtTotalBayar.Text).ToString();
                    }
                }
                //if (Convert.ToDecimal(hdnNilaiBayar.Value) != (Convert.ToDecimal(totalbayar)))
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //    ShowMessage("error", "Nilai Bayar dan Grand Total Bayar harus sama.");
                //    valid = false;
                //}

                if (valid == true)
                {
                    for (int i = 0; i < grdPelunasanDetail.Rows.Count; i++)
                    {
                        HiddenField hdnPiutang = (HiddenField)grdPelunasanDetail.Rows[i].FindControl("hdnPiutang");
                        HiddenField hdnNoCus = (HiddenField)grdPelunasanDetail.Rows[i].FindControl("hdnNoCus");
                        HiddenField hdnNoMataUang = (HiddenField)grdPelunasanDetail.Rows[i].FindControl("hdnNoMataUang");
                        Label lblFaktur = (Label)grdPelunasanDetail.Rows[i].FindControl("lblFaktur");
                        Label lblNilaiFaktur = (Label)grdPelunasanDetail.Rows[i].FindControl("lblNilaiFaktur");
                        Label lblKurs = (Label)grdPelunasanDetail.Rows[i].FindControl("lblKurs");
                        TextBox txtSaldoPiut = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtSaldoPiut");
                        TextBox txtNilaiBayar = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtNilaiBayar");
                        TextBox txtBank = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtBank");
                        TextBox txtPendapatan = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtPendapatan");
                        TextBox txtUangMuka = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtUangMuka");
                        TextBox txtTotalBayar = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtTotalBayar");
                        TextBox txtSaldo = (TextBox)grdPelunasanDetail.Rows[i].FindControl("txtSaldo");
                        CheckBox check = (CheckBox)grdPelunasanDetail.Rows[i].FindControl("chkCheck");

                        if (check.Checked == true)
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("kdBayar", lblKodeBayar.Text);
                            ObjDb.Data.Add("Faktur", lblFaktur.Text);
                            ObjDb.Data.Add("Jnsbayar", "Piutang");
                            ObjDb.Data.Add("tglBayar", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                            ObjDb.Data.Add("Bayar", Convert.ToDecimal(txtNilaiBayar.Text).ToString());
                            ObjDb.Data.Add("bayarRp", (Convert.ToDecimal(lblKurs.Text) * Convert.ToDecimal(txtNilaiBayar.Text)).ToString());
                            ObjDb.Data.Add("bAdm", Convert.ToDecimal(txtBank.Text).ToString());
                            ObjDb.Data.Add("bAdmrp", (Convert.ToDecimal(lblKurs.Text) * Convert.ToDecimal(txtBank.Text)).ToString());
                            ObjDb.Data.Add("uangmuka", Convert.ToDecimal(txtUangMuka.Text).ToString());
                            ObjDb.Data.Add("uangmukarp", (Convert.ToDecimal(lblKurs.Text) * Convert.ToDecimal(txtUangMuka.Text)).ToString());
                            ObjDb.Data.Add("saldo", Convert.ToDecimal(txtSaldo.Text).ToString());
                            ObjDb.Data.Add("pendapatan", Convert.ToDecimal(txtPendapatan.Text).ToString());
                            ObjDb.Insert("tBayar", ObjDb.Data);

                            //--Update tPiutang--//
                            decimal hasil = 0;
                            decimal hasil1 = 0;
                            DataSet mySet = ObjDb.GetRows("select * from tPiutang where noPiutang = '" + hdnPiutang.Value + "'");
                            DataRow myRow = mySet.Tables[0].Rows[0];
                            string saldo = myRow["nSaldoPiutang"].ToString();
                            string noInv = myRow["noInv"].ToString();
                            string noMataUang = myRow["noMataUang"].ToString();
                            string kurs = myRow["kursPiutang"].ToString();
                            string noCust = myRow["nocus"].ToString();

                            if (Convert.ToDecimal(txtUangMuka.Text) != 0)
                            {
                                string KodeUM = ObjSys.GetCodeAutoNumberNew("35", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));

                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("kdUangMuka", KodeUM);
                                ObjDb.Data.Add("Invoice", noInv);
                                ObjDb.Data.Add("noMataUang", noMataUang);
                                ObjDb.Data.Add("kursMataUang", Convert.ToDecimal(kurs).ToString());
                                ObjDb.Data.Add("nilaiSisa", Convert.ToDecimal(txtUangMuka.Text).ToString());
                                ObjDb.Data.Add("nilaiRp", (Convert.ToDecimal(lblKurs.Text) * Convert.ToDecimal(txtUangMuka.Text)).ToString());
                                ObjDb.Data.Add("nSaldoUangMuka", Convert.ToDecimal(txtUangMuka.Text).ToString());
                                ObjDb.Data.Add("nSaldoRpUangMuka", (Convert.ToDecimal(lblKurs.Text) * Convert.ToDecimal(txtUangMuka.Text)).ToString());
                                ObjDb.Data.Add("noCust", noCust);
                                ObjDb.Data.Add("sts", "0");
                                ObjDb.Data.Add("tgl", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                                ObjDb.Insert("tUangMukaCust", ObjDb.Data);

                                ObjSys.UpdateAutoNumberCode("35", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                            }

                            hasil = Convert.ToDecimal(txtSaldo.Text);
                            hasil1 = Convert.ToDecimal(lblKurs.Text) * Convert.ToDecimal(txtSaldo.Text);

                            ObjDb.Where.Clear();
                            ObjDb.Data.Clear();
                            ObjDb.Where.Add("noPiutang", hdnPiutang.Value);
                            ObjDb.Data.Add("nSaldoPiutang", hasil.ToString());
                            ObjDb.Data.Add("nSaldoRpPiutang", hasil1.ToString());
                            ObjDb.Update("tPiutang", ObjDb.Data, ObjDb.Where);

                            DataSet mySetPiutang = ObjDb.GetRows("Select * from tPiutang where noCus = '" + hdnNoCus.Value + "' and noMataUang = '" + hdnNoMataUang.Value + "' and nSaldoPiutang > 0");
                            if (mySetPiutang.Tables[0].Rows.Count == 0)
                            {
                                //--Update tPelunasan--//
                                ObjDb.Where.Clear();
                                ObjDb.Data.Clear();
                                ObjDb.Where.Add("nopelunasan", hdnId.Value);
                                ObjDb.Data.Add("stsln", "0");
                                ObjDb.Update("tPelunasan", ObjDb.Data, ObjDb.Where);
                            }
                        }
                    }

                    ClearData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}