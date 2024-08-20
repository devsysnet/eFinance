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
    public partial class TransPenjualanObat : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
                dtKas.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");


                SetInitialRow();
                for (int i = 1; i < 3; i++)
                {
                    AddNewRow();
                }

            }
        }

        #region loadData
        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdBank.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataProductObat", ObjGlobal.Param);
            grdBank.DataBind();


            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchPO.Text);
            grdPO.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPO", ObjGlobal.Param);
            grdPO.DataBind();

        }

        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {

            cbonorek.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct norek id, ket name FROM mRekening where jenis = '" + cbobyr.Text + "') a");
            cbonorek.DataValueField = "id";
            cbonorek.DataTextField = "name";
            cbonorek.DataBind();

        }

        #endregion

        #region setInitial & AddRow
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("lblDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQTY", typeof(string)));
            dt.Columns.Add(new DataColumn("txtUnit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtDebit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKredit", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["txtAccount"] = string.Empty;
            dr["hdnAccount"] = string.Empty;
            dr["txtUnit"] = string.Empty;
            dr["lblDescription"] = string.Empty;
            dr["txtQTY"] = string.Empty;
            dr["txtDebit"] = string.Empty;
            dr["txtKredit"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdKasBank.DataSource = dt;
            grdKasBank.DataBind();

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
                        HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdKasBank.Rows[i].FindControl("txtAccount");
                        TextBox txtQTY = (TextBox)grdKasBank.Rows[i].FindControl("txtQTY");
                        TextBox lblDescription = (TextBox)grdKasBank.Rows[i].FindControl("lblDescription");
                        TextBox txtUnit = (TextBox)grdKasBank.Rows[i].FindControl("txtUnit");
                        TextBox txtDebit = (TextBox)grdKasBank.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdKasBank.Rows[i].FindControl("txtKredit");

                        txtAccount.Text = dt.Rows[i]["txtAccount"].ToString();
                        hdnAccount.Value = dt.Rows[i]["hdnAccount"].ToString();
                        lblDescription.Text = dt.Rows[i]["lblDescription"].ToString();
                        txtQTY.Text = dt.Rows[i]["txtQTY"].ToString();
                        txtUnit.Text = dt.Rows[i]["txtUnit"].ToString();
                        txtDebit.Text = dt.Rows[i]["txtDebit"].ToString();
                        txtKredit.Text = dt.Rows[i]["txtKredit"].ToString();
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
                        HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdKasBank.Rows[i].FindControl("txtAccount");
                        TextBox txtQTY = (TextBox)grdKasBank.Rows[i].FindControl("txtQTY");
                        TextBox lblDescription = (TextBox)grdKasBank.Rows[i].FindControl("lblDescription");
                        TextBox txtUnit = (TextBox)grdKasBank.Rows[i].FindControl("txtUnit");
                        TextBox txtDebit = (TextBox)grdKasBank.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdKasBank.Rows[i].FindControl("txtKredit");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["txtAccount"] = txtAccount.Text;
                        dtCurrentTable.Rows[i]["hdnAccount"] = hdnAccount.Value;
                        dtCurrentTable.Rows[i]["lblDescription"] = lblDescription.Text;
                        dtCurrentTable.Rows[i]["txtQTY"] = txtQTY.Text;
                        dtCurrentTable.Rows[i]["txtDebit"] = txtDebit.Text;
                        dtCurrentTable.Rows[i]["txtDebit"] = txtDebit.Text;
                        dtCurrentTable.Rows[i]["txtKredit"] = txtKredit.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdKasBank.DataSource = dtCurrentTable;
                    grdKasBank.DataBind();
                }
            }
            SetPreviousData();
        }
        #endregion

        #region Select & Pagging

        protected void grdBank_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdBank.PageIndex = e.NewPageIndex;
            loadData();
            dlgBank.Show();
        }

        protected void grdBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;

                int rowIndexHdn = Convert.ToInt32(hdnParameterProd.Value);
                int rowIndex = grdBank.SelectedRow.RowIndex;

                string kdRek = (grdBank.SelectedRow.FindControl("lblKdRek") as Label).Text;
                string Ket = (grdBank.SelectedRow.FindControl("lblKet") as Label).Text;
                string noRek = (grdBank.SelectedRow.FindControl("hdnNoRek") as HiddenField).Value;
                string unit = (grdBank.SelectedRow.FindControl("lblSatuan") as Label).Text;
                string harga = (grdBank.SelectedRow.FindControl("lblHarga") as Label).Text;
                

                HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[rowIndexHdn].FindControl("hdnAccount");
                TextBox txtAccount = (TextBox)grdKasBank.Rows[rowIndexHdn].FindControl("txtAccount");
                TextBox lblDescription = (TextBox)grdKasBank.Rows[rowIndexHdn].FindControl("lblDescription");
                TextBox txtQTY = (TextBox)grdKasBank.Rows[rowIndexHdn].FindControl("txtQTY");
                TextBox txtUnit = (TextBox)grdKasBank.Rows[rowIndexHdn].FindControl("txtUnit");
                TextBox txtDebit = (TextBox)grdKasBank.Rows[rowIndexHdn].FindControl("txtDebit");

                //Account Boleh Sama 07092020
                //for (int i = 0; i < grdKasBank.Rows.Count; i++)
                //{
                //    TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].Cells[1].FindControl("txtAccount");
                //    if (kdRek == kdRekBank.Text)
                //    {
                //        message += ObjSys.CreateMessage("Akun : " + kdRek + " sudah terpilih.");
                //        valid = false;
                //    }
                //}

                if (valid == true)
                {
                    hdnAccount.Value = noRek;
                    txtAccount.Text = kdRek;
                    lblDescription.Text = Ket;
                    txtUnit.Text = unit;
                    txtDebit.Text = harga;


                    txtSearch.Text = "";
                    loadData();
                    dlgBank.Hide();

                    lblMessageError.Visible = false;
                }
                else
                {
                    dlgBank.Show();
                    lblMessageError.Text = ObjSys.GetMessage("error", message);
                    lblMessageError.Visible = true;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }


        
        #endregion

        #region Button
        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

      
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            //int cekData = 0;

            DataSet dataSaldoKas = ObjDb.GetRows("select isnull(MAX(Tgl),'1/1/1900') as tglMaxPost from tSaldokas where noCabang = '" + ObjSys.GetCabangId + "' and norek = '" + cbonorek.Text + "'");
            if (dataSaldoKas.Tables[0].Rows.Count > 0)
            {
                DataRow myRowSK = dataSaldoKas.Tables[0].Rows[0];
                string tglMaxPost = myRowSK["tglMaxPost"].ToString();
                // cek Post Saldo Kas Terakhir
                // sts = 0 udah posting, 1 = belum posting
                // cek Post Saldo Bulanan (Belum)
                if (Convert.ToDateTime(dtKas.Text) < Convert.ToDateTime(tglMaxPost))
                {
                    message += ObjSys.CreateMessage("Tanggal Penjualan harus lebih besar tanggal terakhir posting.");
                    valid = false;
                }
            }


            if (valid == true)
            {
                try
                {

                    string Kode = ObjSys.GetCodeAutoNumberNew("38", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("nomorKode", Kode);
                    ObjGlobal.Param.Add("tgl", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("jnsbyr", cbobyr.Text);
                    ObjGlobal.Param.Add("norek", cbonorek.Text);
                    ObjGlobal.Param.Add("uraian", txtRemark.Text);
                    ObjGlobal.Param.Add("sts", "0");
                    //ObjGlobal.Param.Add("total", Convert.ToDecimal(txtTotalKredit).ToString());
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("createdDate", ObjSys.GetNow);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);

                    ObjGlobal.ExecuteProcedure("SPInsertPenjualanObat_H", ObjGlobal.Param);
                    ObjSys.UpdateAutoNumberCodeNew("38", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                   
                    DataSet mySetH = ObjDb.GetRows("select noPenjualan from TranspenjualanObat_H where kdpenjualan = '" + Kode + "'");
                    if (mySetH.Tables[0].Rows.Count > 0)
                    {
                        DataRow myRowH = mySetH.Tables[0].Rows[0];
                        string noPenjualan = myRowH["noPenjualan"].ToString();
                        for (int i = 0; i < grdKasBank.Rows.Count; i++)
                        {
                            HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i].FindControl("hdnAccount");
                            TextBox txtQTY = (TextBox)grdKasBank.Rows[i].FindControl("txtQTY");
                            TextBox txtUnit = (TextBox)grdKasBank.Rows[i].FindControl("txtUnit");
                            TextBox txtDebit = (TextBox)grdKasBank.Rows[i].FindControl("txtDebit");
                            TextBox txtKredit = (TextBox)grdKasBank.Rows[i].FindControl("txtKredit");
                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("noBarang", hdnAccount.Value);
                            ObjGlobal.Param.Add("qty", Convert.ToDecimal(txtQTY.Text).ToString());
                            ObjGlobal.Param.Add("satuan", txtUnit.Text);
                            ObjGlobal.Param.Add("harga", Convert.ToDecimal(txtDebit.Text).ToString());
                            ObjGlobal.Param.Add("total", Convert.ToDecimal(txtKredit.Text).ToString());
                            ObjGlobal.Param.Add("noPenjualan", noPenjualan);
                            ObjGlobal.GetDataProcedure("SPInsertPenjualanObat_D", ObjGlobal.Param);
                        }
                    }
                   
                    ObjSys.UpdateAutoNumberCodeNew("38", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                    ClearData();
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil disimpan.");
                    
                    
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.Message);
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
            ClearData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
            dlgBank.Show();
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

            dtKas.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            SetInitialRow();
            for (int i = 1; i < 3; i++)
            {
                AddNewRow();
            }

            loadData();
           
           

        }

        
        #endregion

        protected void loadDataReimbersment()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtKodeReimbersment.Text);
            grdReimbersment.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataReimbersment", ObjGlobal.Param);
            grdReimbersment.DataBind();
        }

        protected void btnReimbersment_Click(object sender, ImageClickEventArgs e)
        {
            loadDataReimbersment();
            dlgReimbersment.Show();
        }

        protected void btnCariReimbersment_Click(object sender, EventArgs e)
        {
            loadDataReimbersment();
            dlgReimbersment.Show();
        }

        protected void grdReimbersment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdReimbersment.PageIndex = e.NewPageIndex;
            loadDataReimbersment();
            dlgReimbersment.Show();
        }

        protected void grdReimbersment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                CloseMessage();

                int rowIndex = grdReimbersment.SelectedRow.RowIndex;
                string Id = grdReimbersment.DataKeys[rowIndex].Values[0].ToString();
                string Kd = grdReimbersment.SelectedRow.Cells[1].Text;
                string nilai = grdReimbersment.SelectedRow.Cells[3].Text;
                string ket = grdReimbersment.SelectedRow.Cells[4].Text;


                
                //txtRemark.Text = ket;

             

                SetInitialRowReim(Id);

                dlgReimbersment.Hide();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        private void SetInitialRowReim(string Id = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("lblDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQTY", typeof(string)));
            dt.Columns.Add(new DataColumn("txtDebit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKredit", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadReimbursmentKasBank", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtAccount"] = myRow["kdRek"].ToString();
                dr["hdnAccount"] = myRow["noRek"].ToString();
                dr["lblDescription"] = myRow["Ket"].ToString();
                dr["txtQTY"] = string.Empty;
                dr["txtDebit"] = ObjSys.IsFormatNumber(myRow["value"].ToString());
                dr["txtKredit"] = string.Empty;
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grdKasBank.DataSource = dt;
            grdKasBank.DataBind();

            SetPreviousData();
        }

        protected void btnPO_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgPO.Show();
        }

        protected void btnSearchPO_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgPO.Show();
        }

        protected void grdPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdPO.PageIndex = e.NewPageIndex;
            loadData();
            dlgPO.Show();
        }

        protected void grdPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdPO.SelectedRow.RowIndex;
                string Id = grdPO.DataKeys[rowIndex].Values[0].ToString();
                string kodePO = grdPO.SelectedRow.Cells[1].Text;


                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noPO", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankPO", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["hargasatuan"].ToString());

                    //txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["hargasatuan"].ToString());
                }

                DataSet mySet = ObjDb.GetRows("select distinct a.noPO, c.noRek, c.kdRek, c.Ket, d.nSaldoHutang as hargasatuan " +
                    "from TransPO_D a inner join mBarang b on a.noBarang = b.noBarang inner join mRekening " +
                    "c on c.noRek = b.norek inner join thutang d on d.nopo = a.nopo where d.nohutang = '" + Id + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];
                string dpp = myRow["hargasatuan"].ToString();

               


                loadData();
                dlgPO.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnPR_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgPR.Show();
        }

        protected void btnSearchPR_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgPR.Show();
        }

        protected void grdPR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdPR.PageIndex = e.NewPageIndex;
            loadData();
            dlgPR.Show();
        }

        protected void grdPR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdPR.SelectedRow.RowIndex;
                string Id = grdPR.DataKeys[rowIndex].Values[0].ToString();
                string kodePR = grdPR.SelectedRow.Cells[1].Text;
                string uraian = grdPR.SelectedRow.Cells[4].Text;

               

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noPR", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankPR", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    TextBox txtQTY = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtQTY");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtQTY.Text = uraian;
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["hargasatuan"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["hargasatuan"].ToString());
                }
               

           
               

                txtRemark.Text = uraian;
                loadData();
                dlgPR.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnGaji_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgGaji.Show();
        }

        protected void btnIuran_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgIuran.Show();
        }

        protected void btnKasBon_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgKasBon.Show();
        }

        protected void btnTHR_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgTHR.Show();
        }

        protected void btnSearchGaji_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgGaji.Show();
        }

        protected void grdGaji_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdGaji.PageIndex = e.NewPageIndex;
            loadData();
            dlgGaji.Show();
        }

        protected void grdGaji_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdGaji.SelectedRow.RowIndex;
                string Id = grdGaji.DataKeys[rowIndex].Values[0].ToString();
                string kodegaji = grdGaji.SelectedRow.Cells[1].Text;

             

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noGaji", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankGaji", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                }

              


                loadData();
                dlgGaji.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnSearchIuran_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgIuran.Show();
        }

        protected void grdIuran_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdGaji.PageIndex = e.NewPageIndex;
            loadData();
            dlgIuran.Show();
        }

        protected void grdIuran_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdIuran.SelectedRow.RowIndex;
                string Id = grdIuran.DataKeys[rowIndex].Values[0].ToString();
                string kodeIuran = grdIuran.SelectedRow.Cells[1].Text;

             

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noIuran", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankIuran", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                }

            

               
                loadData();
                dlgIuran.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnSearchKasBon_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgKasBon.Show();
        }

        protected void grdKasBon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdKasBon.PageIndex = e.NewPageIndex;
            loadData();
            dlgKasBon.Show();
        }

        protected void grdKasBon_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdKasBon.SelectedRow.RowIndex;
                string Id = grdKasBon.DataKeys[rowIndex].Values[0].ToString();
                string kode = grdKasBon.SelectedRow.Cells[1].Text;

            

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noKasbon", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankKasBon", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                  
                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(txtDebit.Text);
                }


                loadData();
                dlgKasBon.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnSearchTHR_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgTHR.Show();
        }

        protected void grdTHR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdTHR.PageIndex = e.NewPageIndex;
            loadData();
            dlgTHR.Show();
        }

        protected void grdTHR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdTHR.SelectedRow.RowIndex;
                string Id = grdTHR.DataKeys[rowIndex].Values[0].ToString();
                string kodeTHR = grdTHR.SelectedRow.Cells[1].Text;

             

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noTHR", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankTHR", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                }

           


                loadData();
                dlgTHR.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
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
                    loadData();
                    //string value = (grdKasBank.SelectedRow.FindControl("hdnParameter") as HiddenField).Value;
                    //hdnParameterProd.Value = value;
                    dlgBank.Show();
                }
                else if (e.CommandName == "Clear")
                {
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[rowIndex].FindControl("txtAccount");
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[rowIndex].FindControl("hdnAccount");
                    TextBox lblDescription = (TextBox)grdKasBank.Rows[rowIndex].FindControl("lblDescription");
                    TextBox txtQTY = (TextBox)grdKasBank.Rows[rowIndex].FindControl("txtQTY");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[rowIndex].FindControl("txtDebit");
                    TextBox txtKredit = (TextBox)grdKasBank.Rows[rowIndex].FindControl("txtKredit");

                    txtAccount.Text = "";
                    hdnAccount.Value = "";
                    lblDescription.Text = "";
                    txtQTY.Text = "";
                    txtDebit.Text = "";
                    txtKredit.Text = "";

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();CalculateDiscount();", true);

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void btnSearchAbsensi_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgAbsensi.Show();
        }

        protected void grdAbsensi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdAbsensi.PageIndex = e.NewPageIndex;
            loadData();
            dlgAbsensi.Show();
        }

        protected void grdAbsensi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdAbsensi.SelectedRow.RowIndex;
                string Id = grdAbsensi.DataKeys[rowIndex].Values[0].ToString();
                string kodeAbsensi = grdAbsensi.SelectedRow.Cells[1].Text;

             
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noAbsensi", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankAbsensi", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                }

              
            
                loadData();
                dlgAbsensi.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnAbsen_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgAbsensi.Show();
        }

        protected void btnPRKas_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgPRKas.Show();
        }

        protected void btnSearchPRKas_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgPRKas.Show();
        }

        protected void grdPRKas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdPRKas.PageIndex = e.NewPageIndex;
            loadData();
            dlgPRKas.Show();
        }

        protected void grdPRKas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdPRKas.SelectedRow.RowIndex;
                string Id = grdPRKas.DataKeys[rowIndex].Values[0].ToString();
                string kodeKas = grdPRKas.SelectedRow.Cells[1].Text;

            

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noKasPR", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankPRKas", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                }


          

                loadData();
                dlgPRKas.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void txtDebit_TextChanged(object sender, EventArgs e)
        {
            TextBox txtDebit = (TextBox)sender;
            var row = (GridViewRow)txtDebit.NamingContainer;

            TextBox txtKredit = (TextBox)row.FindControl("txtKredit");
            if (txtDebit.Text != "0.00" && txtDebit.Text != "0")
                txtKredit.Enabled = false;
            else
                txtKredit.Enabled = true;
        }

        protected void txtKredit_TextChanged(object sender, EventArgs e)
        {
            TextBox txtKredit = (TextBox)sender;
            var row = (GridViewRow)txtKredit.NamingContainer;

            TextBox txtDebit = (TextBox)row.FindControl("txtDebit");
            if (txtKredit.Text != "0.00" && txtKredit.Text != "0")
                txtDebit.Enabled = false;
            else
                txtDebit.Enabled = true;
        }

       

    }
}