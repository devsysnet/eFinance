using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransTerimaBarangSalesUpdate : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCombo();
                dtMulai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                dtSampai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                loadData();

            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            grdReceiveUpdate.DataSource = ObjGlobal.GetDataProcedure("SPUpdateTerimaBarang", ObjGlobal.Param);
            grdReceiveUpdate.DataBind();

        }

        protected void loadCombo()
        {
            cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang= '" + ObjSys.GetCabangId + "') a");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();
        
    }

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
            dt.Columns.Add(new DataColumn("txtQtyterima", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnQtyTerima", typeof(string)));
            dt.Columns.Add(new DataColumn("dtProduk", typeof(string)));
            dt.Columns.Add(new DataColumn("expDt", typeof(string)));
            dt.Columns.Add(new DataColumn("txtbatchnumber", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPTerimabarangDUpdate", ObjGlobal.Param);

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
                dr["dtProduk"] = Convert.ToDateTime(myRow["prodate"]).ToString("dd-MMM-yyyy");
                dr["expDt"] = Convert.ToDateTime(myRow["expd"]).ToString("dd-MMM-yyyy");
                dr["txtbatchnumber"] = myRow["batchnum"].ToString();

                dr["txtQtyterima"] = ObjSys.IsFormatNumber(myRow["qtyTerima"].ToString());
                dr["hdnQtyTerima"] = myRow["qtyTerima"].ToString();

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
                dr["dtProduk"] = string.Empty;
                dr["expDt"] = string.Empty;
                dr["txtbatchnumber"] = string.Empty;
                dr["txtCatatan"] = string.Empty;
                dr["txtQtyterima"] = string.Empty;
                dr["hdnQtyTerima"] = string.Empty;

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
                        TextBox txtQtyterima = (TextBox)grdReceive.Rows[i].FindControl("txtQtyterima");
                        HiddenField hdnQtyTerima = (HiddenField)grdReceive.Rows[i].FindControl("hdnQtyTerima");
                        TextBox dtProduk = (TextBox)grdReceive.Rows[i].FindControl("dtProduk");
                        TextBox expDt = (TextBox)grdReceive.Rows[i].FindControl("expDt");
                        TextBox txtbatchnumber = (TextBox)grdReceive.Rows[i].FindControl("txtbatchnumber");


                        txtkodebrg.Text = dt.Rows[i]["txtkodebrg"].ToString();
                        hdnAccount.Value = dt.Rows[i]["hdnAccount"].ToString();
                        lblnamaBarang.Text = dt.Rows[i]["lblnamaBarang"].ToString();
                        txtQty.Text = dt.Rows[i]["txtQty"].ToString();
                        txtsatuan.Text = dt.Rows[i]["txtsatuan"].ToString();
                        txtQtySisa.Text = dt.Rows[i]["txtQtySisa"].ToString();
                        txtCatatan.Text = dt.Rows[i]["txtCatatan"].ToString();
                        txtQtyterima.Text = dt.Rows[i]["txtQtyterima"].ToString();
                        dtProduk.Text = dt.Rows[i]["dtProduk"].ToString();
                        expDt.Text = dt.Rows[i]["expDt"].ToString();
                        txtbatchnumber.Text = dt.Rows[i]["txtbatchnumber"].ToString();
                        hdnQtyTerima.Value = dt.Rows[i]["hdnQtyTerima"].ToString();
                    }
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void grdReceiveUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdReceiveUpdate.PageIndex = e.NewPageIndex;
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

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
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
            else if (txtsuratjalan.Text == "")
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
                    
         
                    decimal qtyAwal = Convert.ToDecimal(txtQtySisa.Text) + Convert.ToDecimal(txtQtyterima.Text);

                    string nilai = "0";
                    if (txtQtyterima.Text != "0.00")
                        nilai = Convert.ToDecimal(txtQtyterima.Text).ToString();

                    if (nilai == "0")
                    {
                        message = ObjSys.CreateMessage("Qty Terima " + lblnamaBarang.Text + " harus > 0.");
                        alert = "error";
                        valid = false;
                    }
                    if (Convert.ToDecimal(txtQtyterima.Text) > Convert.ToDecimal(qtyAwal))
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
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noTerima", hdnId.Value);
                    ObjGlobal.Param.Add("tglTerima", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("penerima", txtPenerima.Text);
                    ObjGlobal.Param.Add("catatanpenerima", txtKeterangan.Text);
                    ObjGlobal.Param.Add("suratjalan", txtsuratjalan.Text);
                    ObjGlobal.Param.Add("updatedBy", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPUpdateTerimaBrgD2", ObjGlobal.Param);

                    for (int i = 0; i < grdReceive.Rows.Count; i++)
                    {
                        TextBox txtkodebrg = (TextBox)grdReceive.Rows[i].FindControl("txtkodebrg");
                        HiddenField hdnAccount = (HiddenField)grdReceive.Rows[i].FindControl("hdnAccount");
                        TextBox txtQty = (TextBox)grdReceive.Rows[i].FindControl("txtQty");
                        TextBox txtQtyterima = (TextBox)grdReceive.Rows[i].FindControl("txtQtyterima");
                        TextBox dtProduk = (TextBox)grdReceive.Rows[i].FindControl("dtProduk");
                        TextBox expDt = (TextBox)grdReceive.Rows[i].FindControl("expDt");
                        TextBox txtbatchnumber = (TextBox)grdReceive.Rows[i].FindControl("txtbatchnumber");
                        HiddenField hdnQtyTerima = (HiddenField)grdReceive.Rows[i].FindControl("hdnQtyTerima");

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noTerima", hdnId.Value);
                        ObjGlobal.Param.Add("noPRD", hdnnoPRD.Value);
                        ObjGlobal.Param.Add("nobarang", hdnAccount.Value);
                        ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                        ObjGlobal.Param.Add("dtProduk", Convert.ToDateTime(dtProduk.Text).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("batchnumber", txtbatchnumber.Text);
                        ObjGlobal.Param.Add("expDt", Convert.ToDateTime(expDt.Text).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("hdnQtyTerima", Convert.ToDecimal(hdnQtyTerima.Value).ToString());
                        ObjGlobal.Param.Add("txtQtyterima", Convert.ToDecimal(txtQtyterima.Text).ToString());
                        ObjGlobal.GetDataProcedure("SPUpdateTerimaBrgDD2", ObjGlobal.Param);

                    }


                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data Berhasil Disimpan");
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

        protected void grdReceiveUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SelectEdit")
                {
                    CloseMessage();
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string id = grdReceiveUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = id;

                    DataSet myData = ObjDb.GetRows("select a.*,c.jenis,b.noPO,d.noGudang,d.noLokasiGudang from  TransReceive_H a inner join " +
                        "TransPR_D b on a.noPRD = b.noPRD inner join TransPR_H c on c.noPR = b.noPR inner join TransReceive_D d on a.noTerima = d.noTerima " +
                        "where a.noTerima = '" + id + "'");
                    DataRow myRow = myData.Tables[0].Rows[0];

                    hdnnoPRD.Value = myRow["noPRD"].ToString();
                    hdnJnsPR.Value = myRow["jenis"].ToString();
                    txtKodeTerima.Text = myRow["kodeTerima"].ToString();
 

                    dtPO.Text = Convert.ToDateTime(myRow["tglTerima"]).ToString("dd-MMM-yyyy");
                    txtPenerima.Text = myRow["penerima"].ToString();
                    txtKeterangan.Text = myRow["catatanpenerima"].ToString();
                    txtsuratjalan.Text = myRow["nosuratjalan"].ToString();
                    string noPO = myRow["noPO"].ToString();

                    DataSet dataTerima = ObjDb.GetRows("select a.* from TransReceive_H a inner join TransPR_D b on a.noPRD = b.noPRD " +
                    "where a.noPRD <> '" + hdnnoPRD.Value + "' and b.noPO = '" + noPO + "' and a.noTerima <> '" + hdnId.Value + "' ");
                    if (dataTerima.Tables[0].Rows.Count > 0)
                    {
                        dtPO.Enabled = false;
                        txtsuratjalan.Enabled = false;
                    }
                    else
                    {
                        dtPO.Enabled = true;
                        txtsuratjalan.Enabled = true;
                    }

                    SetInitialRow(hdnId.Value);
                    showHideFormKas(false, true);

                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string id = grdReceiveUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = id;

                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noTerima", hdnId.Value);
                    DataSet mySetH2 = ObjDb.GetRows("Select a.noPRD, b.qtyTerima from TransReceive_D b inner join TransReceive_H a " +
                        "on a.noTerima = b.noTerima Where b.noTerima = '" + hdnId.Value + "'");
                    if (mySetH2.Tables[0].Rows.Count > 0)
                    {
                        string sql3 = "delete TAsset where noTerima = '" + hdnId.Value + "'";
                        ObjDb.ExecQuery(sql3);

                        DataRow myRowH2 = mySetH2.Tables[0].Rows[0];
                        string noPRD = myRowH2["noPRD"].ToString();
                        string qtyTerima = Convert.ToDecimal(myRowH2["qtyTerima"]).ToString();
                        string sql = "update TransPR_D set qtyTerima = qtyTerima - " + qtyTerima + " where noPRD = '" + noPRD + "'";
                        ObjDb.ExecQuery(sql);

                        DataTable myDataPO = ObjDb.GetRowsDataTable("select a.qty from TransPO_D a inner join TransPR_D b on a.noPOd = b.noPOD where b.noPRD = '" + noPRD + "'");
                        string totalPO = myDataPO.Rows[0]["qty"].ToString();

                        DataTable myDataRv = ObjDb.GetRowsDataTable("select qtyTerima from TransPR_D where noPRD = '" + noPRD + "'");
                        string totalRv = myDataRv.Rows[0]["qtyTerima"].ToString();

                        if (Convert.ToDecimal(totalPO) == Convert.ToDecimal(totalRv))
                        {
                            string sql2 = "update TransPO_D set ststeriima = 0 where noPOd = '" + noPRD + "'";
                            ObjDb.ExecQuery(sql2);
                        }


                    }
                    ObjDb.Delete("TransReceive_D", ObjDb.Where);
                    ObjDb.Delete("TransReceive_H", ObjDb.Where);

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
    }
}