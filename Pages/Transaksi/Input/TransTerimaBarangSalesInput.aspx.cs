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
    public partial class TransTerimaBarangSalesInput : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 
                loadData();
            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdKas.DataSource = ObjGlobal.GetDataProcedure("SPTerimaBarang1", ObjGlobal.Param);
            grdKas.DataBind();
        }
       

        #region setInitial & AddRow
        private void SetInitialRow(string Id = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnnoPR", typeof(string)));
            dt.Columns.Add(new DataColumn("txtkodebrg", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("lblnamaBarang", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQty", typeof(string)));
            dt.Columns.Add(new DataColumn("txtsatuan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQtySisa", typeof(string)));
            dt.Columns.Add(new DataColumn("txtCatatan", typeof(string)));


            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPTerimabarangD", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnnoPR"] = myRow["noPR"].ToString();
                dr["txtkodebrg"] = myRow["kodeBarang"].ToString();
                dr["hdnAccount"] = myRow["noBarang"].ToString();
                dr["lblnamaBarang"] = myRow["namaBarang"].ToString();
                dr["txtsatuan"] = myRow["satuan"].ToString();
                dr["txtQty"] = ObjSys.IsFormatNumber(myRow["qty"].ToString());
                dr["txtQtySisa"] = ObjSys.IsFormatNumber(myRow["qtySisa"].ToString());
                dr["txtCatatan"] = myRow["keterangan"].ToString();

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnnoPR"] = string.Empty;
                dr["txtkodebrg"] = string.Empty;
                dr["hdnAccount"] = string.Empty;
                dr["lblnamaBarang"] = string.Empty;
                dr["txtQty"] = string.Empty;
                dr["txtQtySisa"] = string.Empty;
                dr["txtsatuan"] = string.Empty;
                dr["txtCatatan"] = string.Empty;

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdReceive.DataSource = dt;
            grdReceive.DataBind();

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
                        HiddenField HiddennoPRD = (HiddenField)grdReceive.Rows[i].FindControl("HiddennoPRD");
                        HiddenField hdnnoPR = (HiddenField)grdReceive.Rows[i].FindControl("hdnnoPR");
                        HiddenField hdnAccount = (HiddenField)grdReceive.Rows[i].FindControl("hdnAccount");
                        TextBox txtkodebrg = (TextBox)grdReceive.Rows[i].FindControl("txtkodebrg");
                        TextBox txtQty = (TextBox)grdReceive.Rows[i].FindControl("txtQty");
                        Label lblnamaBarang = (Label)grdReceive.Rows[i].FindControl("lblnamaBarang");
                        TextBox txtQtySisa = (TextBox)grdReceive.Rows[i].FindControl("txtQtySisa");
                        TextBox txtsatuan = (TextBox)grdReceive.Rows[i].FindControl("txtsatuan");
                        TextBox txtCatatan = (TextBox)grdReceive.Rows[i].FindControl("txtCatatan");

                        txtkodebrg.Text = dt.Rows[i]["txtkodebrg"].ToString();
                        hdnAccount.Value = dt.Rows[i]["hdnAccount"].ToString();
                        lblnamaBarang.Text = dt.Rows[i]["lblnamaBarang"].ToString();
                        txtQty.Text = dt.Rows[i]["txtQty"].ToString();
                        txtsatuan.Text = dt.Rows[i]["txtsatuan"].ToString();
                        txtQtySisa.Text = dt.Rows[i]["txtQtySisa"].ToString();
                        txtCatatan.Text = dt.Rows[i]["txtCatatan"].ToString();
                    }
                }
            }
        }
        #endregion

        protected void grdKas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            loadData();
            grdKas.PageIndex = e.NewPageIndex;
        }

        protected void grdKas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearData();
                CloseMessage();
                int rowIndex = grdKas.SelectedRow.RowIndex;
                string id = grdKas.DataKeys[rowIndex].Values[0].ToString();
                HiddenField HiddennoPRD = (HiddenField)grdKas.Rows[rowIndex].FindControl("HiddennoPRD");
                HiddenField hdnnoPR = (HiddenField)grdKas.Rows[rowIndex].FindControl("hdnnoPR");
                hdnId.Value = id;
                hdnnoPRD.Value = HiddennoPRD.Value;
                DataTable myData = ObjDb.GetRowsDataTable("select distinct a.jenis, c.kodePO, c.noPO from TransPR_H a " +
                    "inner join TransPR_D b on a.noPR = b.noPR inner join TransPO_H c on c.noPO = b.noPO " +
                    "where a.noPR = '" + hdnnoPR.Value + "'");
                hdnJnsPR.Value = myData.Rows[0]["jenis"].ToString();
                txtkdPO.Text = myData.Rows[0]["kodePO"].ToString();
                string noPO = myData.Rows[0]["noPO"].ToString();

                DataSet dataTerima = ObjDb.GetRows("select a.tglTerima, a.nosuratjalan from TransReceive_H a inner join TransPR_D b on a.noPRD = b.noPRD " +
                    "where a.noPRD <> '" + hdnnoPRD.Value + "' and b.noPO = '" + noPO + "'");
                if (dataTerima.Tables[0].Rows.Count > 0)
                {
                    DataRow myRow = dataTerima.Tables[0].Rows[0];
                    dtPO.Text = Convert.ToDateTime(myRow["tglTerima"]).ToString("dd-MMM-yyyy");
                    txtSuratJalan.Text = myRow["nosuratjalan"].ToString();

                    dtPO.Enabled = false;
                    txtSuratJalan.Enabled = false;
                }
                else
                {
                    dtPO.Text = "";
                    txtSuratJalan.Text = "";
                    dtPO.Enabled = true;
                    txtSuratJalan.Enabled = true;
                }

                SetInitialRow(hdnId.Value);
                showHideFormKas(false, true);
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

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            showHideFormKas(true, false);
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";

            if (dtPO.Text == "")
            {
                message = ObjSys.CreateMessage("Tgl Terima harus di isi.");
                alert = "error";
                valid = false;
            }
            else if (txtPenerima.Text == "")
            {
                message = ObjSys.CreateMessage("Penerima harus di isi.");
                alert = "error";
                valid = false;
            }
           
            else if (txtSuratJalan.Text == "")
            {
                message = ObjSys.CreateMessage("No Surat Jalan harus di isi.");
                alert = "error";
                valid = false;
            }
            else
            {
                for (int i = 0; i < grdReceive.Rows.Count; i++)
                {
                    Label lblnamaBarang = (Label)grdReceive.Rows[i].FindControl("lblnamaBarang");
                    TextBox txtQtySisa = (TextBox)grdReceive.Rows[i].FindControl("txtQtySisa");
                    TextBox txtQtyterima = (TextBox)grdReceive.Rows[i].FindControl("txtQtyterima");


                    string nilai = "0";
                    if (txtQtyterima.Text != "0.00")
                        nilai = Convert.ToDecimal(txtQtyterima.Text).ToString();

                    if (nilai == "0")
                    {
                        message = ObjSys.CreateMessage("Qty Terima " + lblnamaBarang.Text + " harus > 0.");
                        alert = "error";
                        valid = false;
                    }
                    if (Convert.ToDecimal(txtQtyterima.Text) > Convert.ToDecimal(txtQtySisa.Text))
                    {
                        message = ObjSys.CreateMessage("Qty Terima harus lebih kecil atau sama dengan Qty Sisa");
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

                    Kode = ObjSys.GetCodeAutoNumberNew("10", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kodeTerima", Kode);
                    ObjGlobal.Param.Add("noPRD", hdnnoPRD.Value);
                    ObjGlobal.Param.Add("tglTerima", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("penerima", txtPenerima.Text);
                    ObjGlobal.Param.Add("catatanpenerima", txtKeterangan.Text);
                    ObjGlobal.Param.Add("suratjalan", txtSuratJalan.Text);
                    ObjGlobal.Param.Add("stsalokasi", "0");
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("createddate", ObjSys.GetDate);
                    
                    ObjGlobal.GetDataProcedure("SPUpdateTerimaBrgD", ObjGlobal.Param);

                    ObjSys.UpdateAutoNumberCode("10", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));

                    DataSet mySetH = ObjDb.GetRows("Select * from TransReceive_H Where KodeTerima = '" + Kode + "'");
                    if (mySetH.Tables[0].Rows.Count > 0)
                    {
                        DataRow myRowH = mySetH.Tables[0].Rows[0];
                        string noTerima = myRowH["noTerima"].ToString();

                        for (int i = 0; i < grdReceive.Rows.Count; i++)
                        {
                            TextBox txtkodebrg = (TextBox)grdReceive.Rows[i].FindControl("txtkodebrg");
                            HiddenField hdnAccount = (HiddenField)grdReceive.Rows[i].FindControl("hdnAccount");
                            TextBox txtQty = (TextBox)grdReceive.Rows[i].FindControl("txtQty");
                            TextBox txtQtyterima = (TextBox)grdReceive.Rows[i].FindControl("txtQtyterima");
                            TextBox dtProduk = (TextBox)grdReceive.Rows[i].FindControl("dtProduk");
                            TextBox expDt = (TextBox)grdReceive.Rows[i].FindControl("expDt");
                            TextBox txtbatchnumber = (TextBox)grdReceive.Rows[i].FindControl("txtbatchnumber");

                            ObjGlobal.Param.Clear();
                        
                           
                            ObjGlobal.Param.Add("noTerima", noTerima);
                            ObjGlobal.Param.Add("noPRD", hdnnoPRD.Value);
                            ObjGlobal.Param.Add("nobarang", hdnAccount.Value);
                            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                            ObjGlobal.Param.Add("dtProduk", Convert.ToDateTime(dtProduk.Text).ToString("yyyy-MM-dd"));
                            ObjGlobal.Param.Add("expDt", Convert.ToDateTime(expDt.Text).ToString("yyyy-MM-dd"));
                            ObjGlobal.Param.Add("batchnumber", txtbatchnumber.Text);
                            ObjGlobal.Param.Add("txtQtyterima", Convert.ToDecimal(txtQtyterima.Text).ToString());
                            ObjGlobal.GetDataProcedure("SPUpdateTerimaBrgDDsales", ObjGlobal.Param);

                        }
                    }

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan");
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

        protected void ClearData()
        {
            txtKeterangan.Text = "";
            txtPenerima.Text = "";
        }

        protected void grdReceive_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (hdnJnsPR.Value == "1")
                {
                    e.Row.Cells[1].Text = "Kode Barang";
                    e.Row.Cells[2].Text = "Nama Barang";
                    e.Row.Cells[3].Text = "Qty";
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[5].Text = "Qty Sisa";
                    e.Row.Cells[6].Text = "Qty Terima";
                }
                else if (hdnJnsPR.Value == "3")
                {
                    e.Row.Cells[1].Text = "Kode Jasa";
                    e.Row.Cells[2].Text = "Nama Jasa";
                    e.Row.Cells[3].Text = "Nilai";
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Text = "Nilai Sisa";
                    e.Row.Cells[6].Text = "Nilai Terima";
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (hdnJnsPR.Value == "1")
                {
                    e.Row.Cells[4].Visible = true;
                }
                else if (hdnJnsPR.Value == "3")
                {
                    e.Row.Cells[4].Visible = false;
                }
            }
        }
    }
}