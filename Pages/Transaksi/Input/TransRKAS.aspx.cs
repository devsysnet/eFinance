using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransRKAS : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDataTahun();
                ShowHideButton(false,false,false,false);
            }
        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("lblTahap", typeof(string)));
            dt.Columns.Add(new DataColumn("txtPersen", typeof(string)));
            dt.Columns.Add(new DataColumn("txtJumlah", typeof(string)));
            dr = dt.NewRow();
            dr["lblTahap"] = "Tahap";
            dr["txtPersen"] = string.Empty;
            dr["txtJumlah"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdTahap.DataSource = dt;
            grdTahap.DataBind();

        }

        protected void LoadDataTahun()
        {
            cboThn.DataSource = ObjDb.GetRows("SELECT DISTINCT id, name FROM ( "+
                "SELECT '0' id, ' ---Pilih Tahun--- ' name "+
                "union all "+
                "SELECT year(GETDATE()) id, CONVERT(varchar, year(GETDATE())) name "+
                "union all "+
                "SELECT CONVERT(varchar,tahun) id, CONVERT(varchar,tahun) name FROM TransRKAS_H WHERE noCabang = " + ObjSys.GetCabangId + " "+
                ") x");
            cboThn.DataValueField = "id";
            cboThn.DataTextField = "name";
            cboThn.DataBind();
            //cboThn.SelectedValue = (System.DateTime.Now.Year).ToString();
        }

        protected void LoadDataAKun()
        {
            cboAkun.DataSource = ObjDb.GetRows("select '0' id,' ---Pilih Akun--- ' name union all SELECT noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening WHERE sts = '2' and Grup = 'Danabos' ");
            cboAkun.DataValueField = "id";
            cboAkun.DataTextField = "name";
            cboAkun.DataBind();
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
                        Label lblTahap = (Label)grdTahap.Rows[i].FindControl("lblTahap");
                        TextBox txtPersen = (TextBox)grdTahap.Rows[i].FindControl("txtPersen");
                        TextBox txtJumlah = (TextBox)grdTahap.Rows[i].FindControl("txtJumlah");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["lblTahap"] = lblTahap.Text;
                        dtCurrentTable.Rows[i]["txtPersen"] = txtPersen.Text;
                        dtCurrentTable.Rows[i]["txtJumlah"] = txtJumlah.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdTahap.DataSource = dtCurrentTable;
                    grdTahap.DataBind();
                }
            }
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    int x = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Label lblTahap = (Label)grdTahap.Rows[i].FindControl("lblTahap");
                        TextBox txtPersen = (TextBox)grdTahap.Rows[i].FindControl("txtPersen");
                        TextBox txtJumlah = (TextBox)grdTahap.Rows[i].FindControl("txtJumlah");

                        lblTahap.Text = "Tahap " + x++;
                        txtPersen.Text = dt.Rows[i]["txtPersen"].ToString();
                        txtJumlah.Text = dt.Rows[i]["txtJumlah"].ToString();
                    }
                }
            }
        }
        protected void btnProses_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            int isi = 0;
            decimal totalPersen = 0;
            for (int i = 0; i < grdTahap.Rows.Count; i++)
            {
                Label lblTahap = (Label)grdTahap.Rows[i].FindControl("lblTahap");
                TextBox txtPersen = (TextBox)grdTahap.Rows[i].FindControl("txtPersen");
                TextBox txtJumlah = (TextBox)grdTahap.Rows[i].FindControl("txtJumlah");

                if (Convert.ToDecimal(txtPersen.Text) != 0)
                {
                    isi++;
                    totalPersen += Convert.ToDecimal(txtPersen.Text);
                }
            }
            if (cboThn.Text == "0")
            {
                message += ObjSys.CreateMessage("Tahun harus dipilih.");
                valid = false;
            }
            else if (txtJmlSiswa.Text == "")
            {
                message += ObjSys.CreateMessage("Jumlah siswa harus diisi.");
                valid = false;
            }
            else if (txtJmlPagu.Text == "")
            {
                message += ObjSys.CreateMessage("Jumlah pagu harus diisi.");
                valid = false;
            }
            else if (cboJmlTahap.Text == "")
            {
                message += ObjSys.CreateMessage("Jumlah tahap harus diisi.");
                valid = false;
            }
            else if (isi == 0)
            {
                message += ObjSys.CreateMessage("Data Tahapan harus diisi");
                valid = false;
            }
            else if (totalPersen > 100)
            {
                message += ObjSys.CreateMessage("Total Persen lebih dari 100%");
                valid = false;
            }
            if (valid == true && isi != 0 && totalPersen <= 100)
            {
                try
                {
                    ObjDb.Data.Clear();
                    ObjDb.Data.Add("tahun", cboThn.Text);
                    ObjDb.Data.Add("jmlSiswa", txtJmlSiswa.Text);
                    ObjDb.Data.Add("jmlPagu", Convert.ToDecimal(txtJmlPagu.Text).ToString());
                    ObjDb.Data.Add("jmlTahap", cboJmlTahap.Text);
                    ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                    ObjDb.Insert("TransRKAS_H", ObjDb.Data);

                    DataSet mySet = ObjDb.GetRows("SELECT noTRKAS FROM TransRKAS_H where tahun = " + cboThn.Text + " and noCabang = " + ObjSys.GetCabangId + "");
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    string noTRKAS = myRow["noTRKAS"].ToString();

                    for (int i = 0; i < grdTahap.Rows.Count; i++)
                    {
                        Label lblTahap = (Label)grdTahap.Rows[i].FindControl("lblTahap");
                        TextBox txtPersen = (TextBox)grdTahap.Rows[i].FindControl("txtPersen");
                        TextBox txtJumlah = (TextBox)grdTahap.Rows[i].FindControl("txtJumlah");

                        ObjDb.Data.Clear();
                        ObjDb.Data.Add("noTRKAS", noTRKAS);
                        ObjDb.Data.Add("tahapan", lblTahap.Text);
                        ObjDb.Data.Add("persen", Convert.ToDecimal(txtPersen.Text).ToString());
                        ObjDb.Data.Add("jumlah", Convert.ToDecimal(txtJumlah.Text).ToString());
                        ObjDb.Insert("TransRKAS_D", ObjDb.Data);

                    }

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noTRKAS", noTRKAS);
                    ObjGlobal.GetDataProcedure("SPInsertTRKAS_A", ObjGlobal.Param);

                    //LoadDataDetilRKAS(noTRKAS);

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                    ShowHideButton(false, false, false, true);
                    ShowHideGridAndForm(true, true, false);

                    loadDataFirst(cboThn.Text, ObjSys.GetCabangId);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Data berhasil disimpan.'); window.location='" + Request.RawUrl + "';", true);
                    //Response.Redirect(Request.RawUrl);

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

        protected void ClearData()
        {
            cboThn.Text = "0";
            txtJmlSiswa.Text = "";
            txtJmlPagu.Text = "";
            cboJmlTahap.Text = "";
            grdTahap.Visible = false;
            GridView1.Visible = false;
            ShowHideButton(false, false, false, false);
            //Response.Redirect(Request.RawUrl);
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
        }

        protected void ShowHideButton(bool Proses, bool Reset, bool Tambah, bool Load)
        {
            btnProses.Enabled = Proses;
            btnReset.Enabled = Reset;
            btnTambah.Enabled = Tambah;
            btnLoad.Visible = Load;
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
        protected void LoadDataDetilRKAS(string noTRKAS)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noTRKAS", noTRKAS);
            GridView1.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRKAS", ObjGlobal.Param);
            GridView1.DataBind();

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "4")
                {
                    GridView1.Columns[7].Visible = true;
                    GridView1.Columns[8].Visible = true;
                    GridView1.Columns[9].Visible = true;
                    GridView1.Columns[10].Visible = true;
                }
                else if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "3")
                {
                    GridView1.Columns[7].Visible = true;
                    GridView1.Columns[8].Visible = true;
                    GridView1.Columns[9].Visible = true;
                    GridView1.Columns[10].Visible = false;
                }
                else if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "2")
                {
                    GridView1.Columns[7].Visible = true;
                    GridView1.Columns[8].Visible = true;
                    GridView1.Columns[9].Visible = false;
                    GridView1.Columns[10].Visible = false;
                }
                else if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "1")
                {
                    GridView1.Columns[7].Visible = true;
                    GridView1.Columns[8].Visible = false;
                    GridView1.Columns[9].Visible = false;
                    GridView1.Columns[10].Visible = false;
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string noRek = GridView1.DataKeys[e.Row.RowIndex].Values[0].ToString();
                string noTRKAS = GridView1.DataKeys[e.Row.RowIndex].Values[1].ToString();
                //HiddenField hdnNoTRKASGrid = (HiddenField)GridView1.Rows[e.Row.RowIndex].FindControl("hdnNoTRKASGrid");
                
                GridView GridView2 = e.Row.FindControl("GridView2") as GridView;

                DataSet mySetH = ObjDb.GetRows("SELECT * FROM mRekening WHERE noRek = " + noRek + " and sts = '2'");
                if (mySetH.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowH = mySetH.Tables[0].Rows[0];
                    string noRekD = myRowH["noRek"].ToString();

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noTRKAS", noTRKAS);
                    ObjGlobal.Param.Add("noRekD", noRek);
                    GridView2.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRKASDetilAkun", ObjGlobal.Param);
                    GridView2.DataBind();

                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "4")
                        {
                            GridView2.Columns[5].Visible = true;
                            GridView2.Columns[6].Visible = true;
                            GridView2.Columns[7].Visible = true;
                            GridView2.Columns[8].Visible = true;
                        }
                        else if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "3")
                        {
                            GridView2.Columns[5].Visible = true;
                            GridView2.Columns[6].Visible = true;
                            GridView2.Columns[7].Visible = true;
                            GridView2.Columns[8].Visible = false;
                        }
                        else if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "2")
                        {
                            GridView2.Columns[5].Visible = true;
                            GridView2.Columns[6].Visible = true;
                            GridView2.Columns[7].Visible = false;
                            GridView2.Columns[8].Visible = false;
                        }
                        else if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "1")
                        {
                            GridView2.Columns[5].Visible = true;
                            GridView2.Columns[6].Visible = false;
                            GridView2.Columns[7].Visible = false;
                            GridView2.Columns[8].Visible = false;
                        }
                    }
                }

            }
        }
        
        protected void cboJmlTahap_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetInitialRow();
            for (int i = 1; i < Convert.ToInt32(cboJmlTahap.Text); i++)
            {
                AddNewRow();
            }
            ShowHideButton(true, true, false, false);
            grdTahap.Visible = true;
        }

        protected void cboThn_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataFirst(cboThn.Text, ObjSys.GetCabangId);
        }

        protected void loadDataFirst(string tahun = "", string cabang = "")
        {
            DataSet mySetH = ObjDb.GetRows("Select * from TransRKAS_H Where tahun = " + tahun + " and noCabang = " + cabang + " ");
            if (mySetH.Tables[0].Rows.Count > 0)
            {
                DataRow myRowH = mySetH.Tables[0].Rows[0];
                string ID = myRowH["noTRKAS"].ToString();
                cboThn.Text = myRowH["tahun"].ToString();
                txtJmlSiswa.Text = myRowH["jmlSiswa"].ToString();
                txtJmlPagu.Text = ObjSys.IsFormatNumber(myRowH["jmlPagu"].ToString());
                cboJmlTahap.Text = myRowH["jmlTahap"].ToString();

                txtJmlSiswa.Enabled = false;
                txtJmlPagu.Enabled = false;
                cboJmlTahap.Enabled = false;
                SetInitialRowUpdate(ID);
                LoadDataDetilRKAS(ID);
                ShowHideGridAndForm(true, true, false);
                ShowHideButton(false,false,true, false);
                grdTahap.Enabled = false;
                grdTahap.Visible = true;
            }
            else
            {
                //ClearData();
                txtJmlSiswa.Text = "";
                txtJmlPagu.Text = "";
                cboJmlTahap.Text = "";

                txtJmlSiswa.Enabled = true;
                txtJmlPagu.Enabled = true;
                cboJmlTahap.Enabled = true;
                ShowHideGridAndForm(true, false, false);
                ShowHideButton(false, false, false, false);
                grdTahap.Visible = false;
            }
        }

        private void SetInitialRowUpdate(string id = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("lblTahap", typeof(string)));
            dt.Columns.Add(new DataColumn("txtPersen", typeof(string)));
            dt.Columns.Add(new DataColumn("txtJumlah", typeof(string)));

            DataSet mySet = ObjDb.GetRows("Select * from TransRKAS_D Where noTRKAS = " + id + "");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["lblTahap"] = myRow["tahapan"].ToString();
                dr["txtPersen"] = myRow["persen"].ToString();
                dr["txtJumlah"] = ObjSys.IsFormatNumber(myRow["jumlah"].ToString());
                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdTahap.DataSource = dt;
            grdTahap.DataBind();

            SetPreviousData();

        }

        protected void setInitialRowGridUpdate(string id = "", string akun = "", string jenisBOS = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnnoTRKAS_AD", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnBrgX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtkdBrgX", typeof(string)));
            dt.Columns.Add(new DataColumn("lblKeteranganX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtVolumeX", typeof(string)));
            //dt.Columns.Add(new DataColumn("lblSatuanX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtHargaStnX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtJumlahX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTahap1X", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTahap2X", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTahap3X", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTahap4X", typeof(string)));

            string sql = "";
            if (jenisBOS == "27")
            {
                sql = "Select a.*, b.kdRek as kodeBarang,b.Ket as namaBarang " +
                    "from TransRKAS_AD a inner join mRekening b on a.noRek = b.noRek " +
                    "Where a.noTRKAS = " + id + " and a.noRek = " + akun + " and b.Grup = 'Danabos'";
            }
            else
            {
                sql = "Select a.*, b.kodeBarang,b.namaBarang " +
                    "from TransRKAS_AD a inner join mBarang b on a.noBarang = b.noBarang " +
                    "Where a.noTRKAS = " + id + " and a.noRek = " + akun + " ";
            }
            DataSet mySet = ObjDb.GetRows(sql);
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnnoTRKAS_AD"] = myRow["noTRKASAD"].ToString();
                dr["hdnBrgX"] = myRow["noBarang"].ToString();
                dr["txtkdBrgX"] = myRow["kodeBarang"].ToString();
                dr["lblKeteranganX"] = myRow["namaBarang"].ToString();
                dr["txtVolumeX"] = ObjSys.IsFormatNumber(myRow["volume"].ToString());
                //dr["lblSatuanX"] = string.Empty;
                dr["txtHargaStnX"] = ObjSys.IsFormatNumber(myRow["hargaSatuan"].ToString());
                dr["txtJumlahX"] = ObjSys.IsFormatNumber(myRow["jumlahAD"].ToString());
                dr["txtTahap1X"] = ObjSys.IsFormatNumber(myRow["tahap1"].ToString());
                dr["txtTahap2X"] = ObjSys.IsFormatNumber(myRow["tahap2"].ToString());
                dr["txtTahap3X"] = ObjSys.IsFormatNumber(myRow["tahap3"].ToString());
                dr["txtTahap4X"] = ObjSys.IsFormatNumber(myRow["tahap4"].ToString());
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTableX"] = dt;
            grdRekA.DataSource = dt;
            grdRekA.DataBind();

            SetPreviousDataGrid();
        }

        protected void setInitialRowGridBOSPgw(string akun = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnnoTRKAS_AD", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnBrgX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtkdBrgX", typeof(string)));
            dt.Columns.Add(new DataColumn("lblKeteranganX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtVolumeX", typeof(string)));
            //dt.Columns.Add(new DataColumn("lblSatuanX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtHargaStnX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtJumlahX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTahap1X", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTahap2X", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTahap3X", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTahap4X", typeof(string)));

            DataSet mySet = ObjDb.GetRows("Select kdRek, Ket from mRekening Where noRek = " + akun + "");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnnoTRKAS_AD"] = "0";
                dr["hdnBrgX"] = string.Empty;
                dr["txtkdBrgX"] = myRow["kdRek"].ToString();
                dr["lblKeteranganX"] = myRow["Ket"].ToString();
                dr["txtVolumeX"] = string.Empty;
                //dr["lblSatuanX"] = string.Empty;
                dr["txtHargaStnX"] = string.Empty;
                dr["txtJumlahX"] = string.Empty;
                dr["txtTahap1X"] = string.Empty;
                dr["txtTahap2X"] = string.Empty;
                dr["txtTahap3X"] = string.Empty;
                dr["txtTahap4X"] = string.Empty;
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTableX"] = dt;
            grdRekA.DataSource = dt;
            grdRekA.DataBind();

            SetPreviousDataGrid();
        }
        protected void setInitialRowGrid()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnnoTRKAS_AD", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnBrgX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtkdBrgX", typeof(string)));
            dt.Columns.Add(new DataColumn("lblKeteranganX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtVolumeX", typeof(string)));
            //dt.Columns.Add(new DataColumn("lblSatuanX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtHargaStnX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtJumlahX", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTahap1X", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTahap2X", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTahap3X", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTahap4X", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1; 
            dr["hdnnoTRKAS_AD"] = "0";
            dr["hdnBrgX"] = string.Empty;
            dr["txtkdBrgX"] = string.Empty;
            dr["lblKeteranganX"] = string.Empty;
            dr["txtVolumeX"] = string.Empty;
            //dr["lblSatuanX"] = string.Empty;
            dr["txtHargaStnX"] = string.Empty;
            dr["txtJumlahX"] = string.Empty;
            dr["txtTahap1X"] = string.Empty;
            dr["txtTahap2X"] = string.Empty;
            dr["txtTahap3X"] = string.Empty;
            dr["txtTahap4X"] = string.Empty;
            dt.Rows.Add(dr);
            
            ViewState["CurrentTableX"] = dt;
            grdRekA.DataSource = dt;
            grdRekA.DataBind();

            SetPreviousDataGrid();
        }

        private void SetPreviousDataGrid()
        {
            if (ViewState["CurrentTableX"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTableX"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HiddenField hdnnoTRKAS_AD = (HiddenField)grdRekA.Rows[i].FindControl("hdnnoTRKAS_AD");
                        HiddenField hdnBrgX = (HiddenField)grdRekA.Rows[i].FindControl("hdnBrgX");
                        TextBox txtkdBrgX = (TextBox)grdRekA.Rows[i].FindControl("txtkdBrgX");
                        Label lblKeteranganX = (Label)grdRekA.Rows[i].FindControl("lblKeteranganX");
                        TextBox txtVolumeX = (TextBox)grdRekA.Rows[i].FindControl("txtVolumeX");
                        //Label lblSatuanX = (Label)grdRekA.Rows[i].FindControl("lblSatuanX");
                        TextBox txtHargaStnX = (TextBox)grdRekA.Rows[i].FindControl("txtHargaStnX");
                        TextBox txtJumlahX = (TextBox)grdRekA.Rows[i].FindControl("txtJumlahX");
                        TextBox txtTahap1X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap1X");
                        TextBox txtTahap2X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap2X");
                        TextBox txtTahap3X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap3X");
                        TextBox txtTahap4X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap4X");

                        hdnnoTRKAS_AD.Value = dt.Rows[i]["hdnnoTRKAS_AD"].ToString();
                        hdnBrgX.Value = dt.Rows[i]["hdnBrgX"].ToString();
                        txtkdBrgX.Text = dt.Rows[i]["txtkdBrgX"].ToString();
                        lblKeteranganX.Text = dt.Rows[i]["lblKeteranganX"].ToString();
                        txtVolumeX.Text = dt.Rows[i]["txtVolumeX"].ToString();
                        //lblSatuanX.Text = dt.Rows[i]["lblSatuanX"].ToString();
                        txtHargaStnX.Text = dt.Rows[i]["txtHargaStnX"].ToString();
                        txtJumlahX.Text = dt.Rows[i]["txtJumlahX"].ToString();
                        txtTahap1X.Text = dt.Rows[i]["txtTahap1X"].ToString();
                        txtTahap2X.Text = dt.Rows[i]["txtTahap2X"].ToString();
                        txtTahap3X.Text = dt.Rows[i]["txtTahap3X"].ToString();
                        txtTahap4X.Text = dt.Rows[i]["txtTahap4X"].ToString();
                    }
                }
            }
        }

        private void AddNewRowGrid()
        {
            if (ViewState["CurrentTableX"] != null)
            {
                DataTable dtCurrentTableX = (DataTable)ViewState["CurrentTableX"];
                DataRow drCurrentRowX = null;
                if (dtCurrentTableX.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTableX.Rows.Count; i++)
                    {
                        HiddenField hdnnoTRKAS_AD = (HiddenField)grdRekA.Rows[i].FindControl("hdnnoTRKAS_AD");
                        HiddenField hdnBrgX = (HiddenField)grdRekA.Rows[i].FindControl("hdnBrgX");
                        TextBox txtkdBrgX = (TextBox)grdRekA.Rows[i].FindControl("txtkdBrgX");
                        Label lblKeteranganX = (Label)grdRekA.Rows[i].FindControl("lblKeteranganX");
                        TextBox txtVolumeX = (TextBox)grdRekA.Rows[i].FindControl("txtVolumeX");
                        //Label lblSatuanX = (Label)grdRekA.Rows[i].FindControl("lblSatuanX");
                        TextBox txtHargaStnX = (TextBox)grdRekA.Rows[i].FindControl("txtHargaStnX");
                        TextBox txtJumlahX = (TextBox)grdRekA.Rows[i].FindControl("txtJumlahX");
                        TextBox txtTahap1X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap1X");
                        TextBox txtTahap2X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap2X");
                        TextBox txtTahap3X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap3X");
                        TextBox txtTahap4X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap4X");

                        drCurrentRowX = dtCurrentTableX.NewRow();
                        dtCurrentTableX.Rows[i]["hdnnoTRKAS_AD"] = hdnnoTRKAS_AD.Value;
                        dtCurrentTableX.Rows[i]["hdnBrgX"] = hdnBrgX.Value;
                        dtCurrentTableX.Rows[i]["txtkdBrgX"] = txtkdBrgX.Text;
                        dtCurrentTableX.Rows[i]["lblKeteranganX"] = lblKeteranganX.Text;
                        dtCurrentTableX.Rows[i]["txtVolumeX"] = txtVolumeX.Text;
                        //dtCurrentTableX.Rows[i]["lblSatuanX"] = lblSatuanX.Text;
                        dtCurrentTableX.Rows[i]["txtHargaStnX"] = txtHargaStnX.Text;
                        dtCurrentTableX.Rows[i]["txtJumlahX"] = txtJumlahX.Text;
                        dtCurrentTableX.Rows[i]["txtTahap1X"] = txtTahap1X.Text;
                        dtCurrentTableX.Rows[i]["txtTahap2X"] = txtTahap2X.Text;
                        dtCurrentTableX.Rows[i]["txtTahap3X"] = txtTahap3X.Text;
                        dtCurrentTableX.Rows[i]["txtTahap4X"] = txtTahap4X.Text;

                    }
                    dtCurrentTableX.Rows.Add(drCurrentRowX);
                    ViewState["CurrentTableX"] = dtCurrentTableX;
                    grdRekA.DataSource = dtCurrentTableX;
                    grdRekA.DataBind();
                }
            }
            SetPreviousDataGrid();
        }
        
        protected void grdRekA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < grdRekA.Rows.Count; i++)
                {
                    if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "4")
                    {
                        grdRekA.Columns[6].Visible = true;
                        grdRekA.Columns[7].Visible = true;
                        grdRekA.Columns[8].Visible = true;
                        grdRekA.Columns[9].Visible = true;
                    }
                    else if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "3")
                    {
                        grdRekA.Columns[6].Visible = true;
                        grdRekA.Columns[7].Visible = true;
                        grdRekA.Columns[8].Visible = true;
                        grdRekA.Columns[9].Visible = false;
                    }
                    else if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "2")
                    {
                        grdRekA.Columns[6].Visible = true;
                        grdRekA.Columns[7].Visible = true;
                        grdRekA.Columns[8].Visible = false;
                        grdRekA.Columns[9].Visible = false;
                    }
                    else if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "1")
                    {
                        grdRekA.Columns[6].Visible = true;
                        grdRekA.Columns[7].Visible = false;
                        grdRekA.Columns[8].Visible = false;
                        grdRekA.Columns[9].Visible = false;
                    }
                }
            }
        }

        protected void btnAddrowRekD_Click(object sender, EventArgs e)
        {
            AddNewRowGrid();
        }

        protected void grdRekA_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                hdnRowDataIndex.Value = rowIndex.ToString();
                if (e.CommandName == "Select")
                {
                    hdnRow.Value = rowIndex.ToString();
                    HiddenField hdnNoRekD = (HiddenField)grdRekA.Rows[rowIndex].FindControl("hdnNoRekD");
                    HiddenField hdnNoTRKASD = (HiddenField)grdRekA.Rows[rowIndex].FindControl("hdnNoTRKASD");

                    CloseMessage();
                    loadDataBarang();
                    dlgBarang.Show();
                }
                else if (e.CommandName == "Clear")
                {
                    //HiddenField hdnnoTRKAS_AD = (HiddenField)grdRekA.Rows[rowIndex].FindControl("hdnnoTRKAS_AD");
                    HiddenField hdnBrgX = (HiddenField)grdRekA.Rows[rowIndex].FindControl("hdnBrgX");
                    TextBox txtkdBrgX = (TextBox)grdRekA.Rows[rowIndex].FindControl("txtkdBrgX");
                    Label lblKeteranganX = (Label)grdRekA.Rows[rowIndex].FindControl("lblKeteranganX");
                    TextBox txtVolumeX = (TextBox)grdRekA.Rows[rowIndex].FindControl("txtVolumeX");
                    //Label lblSatuanX = (Label)grdRekA.Rows[rowIndex].FindControl("lblSatuanX");
                    TextBox txtHargaStnX = (TextBox)grdRekA.Rows[rowIndex].FindControl("txtHargaStnX");
                    TextBox txtJumlahX = (TextBox)grdRekA.Rows[rowIndex].FindControl("txtJumlahX");
                    TextBox txtTahap1X = (TextBox)grdRekA.Rows[rowIndex].FindControl("txtTahap1X");
                    TextBox txtTahap2X = (TextBox)grdRekA.Rows[rowIndex].FindControl("txtTahap2X");
                    TextBox txtTahap3X = (TextBox)grdRekA.Rows[rowIndex].FindControl("txtTahap3X");
                    TextBox txtTahap4X = (TextBox)grdRekA.Rows[rowIndex].FindControl("txtTahap4X");

                    //hdnnoTRKAS_AD.Value = "";
                    hdnBrgX.Value = "";
                    txtkdBrgX.Text = "";
                    lblKeteranganX.Text = "";
                    txtVolumeX.Text = "";
                    //lblSatuanX.Text = "";
                    txtHargaStnX.Text = "";
                    txtJumlahX.Text = "";
                    txtTahap1X.Text = "";
                    txtTahap2X.Text = "";
                    txtTahap3X.Text = "";
                    txtTahap4X.Text = "";

                    //CalculateDetil();
                    //ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "funcsum", "funcsum();", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }


        protected void grdBarang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdBarang.PageIndex = e.NewPageIndex;
            loadDataBarang();
            dlgBarang.Show();
        }

        protected void grdBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = grdBarang.SelectedRow.RowIndex;
                int rowIndexHdn = Convert.ToInt32(hdnRowDataIndex.Value);

                string kodeBarang = (grdBarang.SelectedRow.FindControl("lblKdBrg") as Label).Text;
                string namaBarang = (grdBarang.SelectedRow.FindControl("lblBrg") as Label).Text;
                string noBarang = (grdBarang.SelectedRow.FindControl("hdnNoBrg") as HiddenField).Value;

                HiddenField hdnBrgX = (HiddenField)grdRekA.Rows[rowIndexHdn].FindControl("hdnBrgX");
                TextBox txtkdBrgX = (TextBox)grdRekA.Rows[rowIndexHdn].FindControl("txtkdBrgX");
                Label lblKeteranganX = (Label)grdRekA.Rows[rowIndexHdn].FindControl("lblKeteranganX");

                hdnBrgX.Value = noBarang;
                txtkdBrgX.Text = kodeBarang;
                lblKeteranganX.Text = namaBarang;

                dlgBarang.Hide();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void loadDataBarang()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdBarang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataMapCOA", ObjGlobal.Param);
            grdBarang.DataBind();
        }
        protected void btnTutup_Click(object sender, EventArgs e)
        {
            dlgBarang.Hide();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataBarang();
            dlgBarang.Show();
        }

        protected void btnSaveRekD_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            int isi = 0, selisih = 0;
            string tahap1x = "", tahap2x = "", tahap3x = "", tahap4x = "";
            decimal jumlahTahap = 0;
            for (int i = 0; i < grdRekA.Rows.Count; i++)
            {
                TextBox txtkdBrgX = (TextBox)grdRekA.Rows[i].FindControl("txtkdBrgX");
                TextBox txtVolumeX = (TextBox)grdRekA.Rows[i].FindControl("txtVolumeX");
                TextBox txtHargaStnX = (TextBox)grdRekA.Rows[i].FindControl("txtHargaStnX");
                TextBox txtJumlahX = (TextBox)grdRekA.Rows[i].FindControl("txtJumlahX");
                TextBox txtTahap1X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap1X");
                TextBox txtTahap2X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap2X");
                TextBox txtTahap3X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap3X");
                TextBox txtTahap4X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap4X");

                if (txtTahap1X.Text == "")
                    tahap1x = "0";
                else
                    tahap1x = Convert.ToDecimal(txtTahap1X.Text).ToString();
                if (txtTahap2X.Text == "")
                    tahap2x = "0";
                else
                    tahap2x = Convert.ToDecimal(txtTahap2X.Text).ToString();
                if (txtTahap3X.Text == "")
                    tahap3x = "0";
                else
                    tahap3x = Convert.ToDecimal(txtTahap3X.Text).ToString();
                if (txtTahap4X.Text == "")
                    tahap4x = "0";
                else
                    tahap4x = Convert.ToDecimal(txtTahap4X.Text).ToString();

                jumlahTahap = Convert.ToDecimal(tahap1x) + Convert.ToDecimal(tahap2x) + Convert.ToDecimal(tahap3x) + Convert.ToDecimal(tahap4x);

                if (txtkdBrgX.Text != "" && txtVolumeX.Text != "" && txtHargaStnX.Text != "")
                {
                    isi++;
                    if ((Convert.ToDecimal(txtJumlahX.Text) - Convert.ToDecimal(jumlahTahap) > 1) || (Convert.ToDecimal(jumlahTahap) - Convert.ToDecimal(txtJumlahX.Text) > 1))
                        selisih++;
                }
 
            }
            if (cboAkun.Text == "0")
            {
                message += ObjSys.CreateMessage("Akun harus dipilih.");
                valid = false;
            }
            if (isi == 0)
            {
                message += ObjSys.CreateMessage("Data harus diisi minumal 1.");
                valid = false;
            }
            if (selisih > 0)
            {
                message += ObjSys.CreateMessage("Total Nilai Tahap harus sama dengan Jumlah.");
                valid = false;
            }
            if (valid == true && isi != 0 && selisih == 0)
            {
                try
                {
                    DataSet mySet = ObjDb.GetRows("SELECT noTRKAS FROM TransRKAS_H where tahun = " + cboThn.Text + " and noCabang = " + ObjSys.GetCabangId + "");
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    string noTRKAS = myRow["noTRKAS"].ToString();
                    string tahap1 = "", tahap2 = "", tahap3 = "", tahap4 = "";
                    for (int i = 0; i < grdRekA.Rows.Count; i++)
                    {
                        HiddenField hdnnoTRKAS_AD = (HiddenField)grdRekA.Rows[i].FindControl("hdnnoTRKAS_AD");
                        HiddenField hdnBrgX = (HiddenField)grdRekA.Rows[i].FindControl("hdnBrgX");
                        HiddenField hdnNoRekD = (HiddenField)grdRekA.Rows[i].FindControl("hdnNoRekD");
                        TextBox txtkdBrgX = (TextBox)grdRekA.Rows[i].FindControl("txtkdBrgX");
                        TextBox txtVolumeX = (TextBox)grdRekA.Rows[i].FindControl("txtVolumeX");
                        TextBox txtHargaStnX = (TextBox)grdRekA.Rows[i].FindControl("txtHargaStnX");
                        TextBox txtJumlahX = (TextBox)grdRekA.Rows[i].FindControl("txtJumlahX");
                        TextBox txtTahap1X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap1X");
                        TextBox txtTahap2X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap2X");
                        TextBox txtTahap3X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap3X");
                        TextBox txtTahap4X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap4X");
                        if (txtTahap1X.Text == "")
                            tahap1 = "0";
                        else
                            tahap1 = Convert.ToDecimal(txtTahap1X.Text).ToString();
                        if (txtTahap2X.Text == "")
                            tahap2 = "0";
                        else
                            tahap2 = Convert.ToDecimal(txtTahap2X.Text).ToString();
                        if (txtTahap3X.Text == "")
                            tahap3 = "0";
                        else
                            tahap3 = Convert.ToDecimal(txtTahap3X.Text).ToString();
                        if (txtTahap4X.Text == "")
                            tahap4 = "0";
                        else
                            tahap4 = Convert.ToDecimal(txtTahap4X.Text).ToString();

                        if ((hdnnoTRKAS_AD.Value == "0" || hdnnoTRKAS_AD.Value == "") && txtkdBrgX.Text != "" && txtVolumeX.Text != "" && txtHargaStnX.Text != "")
                        {

                            DataSet mySetParent = ObjDb.GetRows("SELECT Parents FROM vHierarchy WHERE ChildId = " + cboAkun.Text + " ");
                            DataRow myRowParent = mySetParent.Tables[0].Rows[0];
                            string Parents = myRowParent["Parents"].ToString();

                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noTRKAS", noTRKAS);
                            ObjDb.Data.Add("noRek", cboAkun.Text);
                            ObjDb.Data.Add("noBarang", hdnBrgX.Value);
                            ObjDb.Data.Add("volume", Convert.ToDecimal(txtVolumeX.Text).ToString());
                            ObjDb.Data.Add("hargaSatuan", Convert.ToDecimal(txtHargaStnX.Text).ToString());
                            ObjDb.Data.Add("jumlahAD", Convert.ToDecimal(txtJumlahX.Text).ToString());
                            ObjDb.Data.Add("tahap1", tahap1);
                            ObjDb.Data.Add("tahap2", tahap2);
                            ObjDb.Data.Add("tahap3", tahap3);
                            ObjDb.Data.Add("tahap4", tahap4);
                            ObjDb.Insert("TransRKAS_AD", ObjDb.Data);

                            string sql = "update TransRKAS_A set jumlahA = jumlahA + " + Convert.ToDecimal(txtJumlahX.Text).ToString() + ", tahap1 = tahap1 + " + tahap1 + ", tahap2 = tahap2 + " + tahap2 + ", tahap3 = tahap3 + " + tahap3 + ", tahap4 = tahap4 + " + tahap4 + " where noTRKAS = " + noTRKAS + " and noRek = " + cboAkun.Text + " ";
                            ObjDb.ExecQuery(sql);

                            string sqlParent = "update TransRKAS_A set jumlahA = jumlahA + " + Convert.ToDecimal(txtJumlahX.Text).ToString() + ", tahap1 = tahap1 + " + tahap1 + ", tahap2 = tahap2 + " + tahap2 + ", tahap3 = tahap3 + " + tahap3 + ", tahap4 = tahap4 + " + tahap4 + " where noTRKAS = " + noTRKAS + " and noRek in (" + Parents + ") ";
                            ObjDb.ExecQuery(sqlParent);
                        }
                        else if (hdnnoTRKAS_AD.Value != "0" && txtkdBrgX.Text != "" && txtVolumeX.Text != "" && txtHargaStnX.Text != "")
                        {
                            DataSet mySetAwal = ObjDb.GetRows("SELECT jumlahAD, tahap1, tahap2, tahap3, tahap4 FROM TransRKAS_AD WHERE noTRKASAD = " + hdnnoTRKAS_AD.Value + " ");
                            DataRow myRowAwal = mySetAwal.Tables[0].Rows[0];
                            string jumlahAwal = myRowAwal["jumlahAD"].ToString();
                            string tahap1Awal = myRowAwal["tahap1"].ToString();
                            string tahap2Awal = myRowAwal["tahap2"].ToString();
                            string tahap3Awal = myRowAwal["tahap3"].ToString();
                            string tahap4Awal = myRowAwal["tahap4"].ToString();
                            
                            DataSet mySetParent = ObjDb.GetRows("SELECT Parents FROM vHierarchy WHERE ChildId = " + cboAkun.Text + " ");
                            DataRow myRowParent = mySetParent.Tables[0].Rows[0];
                            string Parents = myRowParent["Parents"].ToString();

                            ObjDb.Data.Clear();
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noTRKASAD", hdnnoTRKAS_AD.Value);
                            ObjDb.Data.Add("noBarang", hdnBrgX.Value);
                            ObjDb.Data.Add("volume", Convert.ToDecimal(txtVolumeX.Text).ToString());
                            ObjDb.Data.Add("hargaSatuan", Convert.ToDecimal(txtHargaStnX.Text).ToString());
                            ObjDb.Data.Add("jumlahAD", Convert.ToDecimal(txtJumlahX.Text).ToString());
                            ObjDb.Data.Add("tahap1", tahap1);
                            ObjDb.Data.Add("tahap2", tahap2);
                            ObjDb.Data.Add("tahap3", tahap3);
                            ObjDb.Data.Add("tahap4", tahap4);
                            ObjDb.Update("TransRKAS_AD", ObjDb.Data, ObjDb.Where);

                            string sql = "update TransRKAS_A set jumlahA = (jumlahA - " + Convert.ToDecimal(jumlahAwal) + ") + " + Convert.ToDecimal(txtJumlahX.Text) + ", tahap1 = (tahap1 - " + Convert.ToDecimal(tahap1Awal) + ") + " + tahap1 + ", tahap2 = (tahap2 - " + Convert.ToDecimal(tahap2Awal) + ") + " + tahap2 + ", tahap3 = (tahap3 - " + Convert.ToDecimal(tahap3Awal) + ") + " + tahap3 + ", tahap4 = (tahap4 - " + Convert.ToDecimal(tahap4Awal) + " ) + " + tahap4 + " where noTRKAS = " + noTRKAS + " and noRek = " + cboAkun.Text + " ";
                            ObjDb.ExecQuery(sql);

                            string sqlParent = "update TransRKAS_A set jumlahA = (jumlahA - " + Convert.ToDecimal(jumlahAwal) + ") + " + Convert.ToDecimal(txtJumlahX.Text) + ", tahap1 = (tahap1 - " + Convert.ToDecimal(tahap1Awal) +") + " + tahap1 + ", tahap2 = (tahap2 - " + Convert.ToDecimal(tahap2Awal) + ") + " + tahap2 + ", tahap3 = (tahap3 - " + Convert.ToDecimal(tahap3Awal) + ") + " + tahap3 + ", tahap4 = (tahap4 - " + Convert.ToDecimal(tahap4Awal) + " ) + " + tahap4 + " where noTRKAS = " + noTRKAS + " and noRek in (" + Parents + ") ";
                            ObjDb.ExecQuery(sqlParent);
                        }
                        else if ((hdnnoTRKAS_AD.Value != "0" || hdnnoTRKAS_AD.Value != "") && txtkdBrgX.Text == "" && txtVolumeX.Text == "" && txtHargaStnX.Text == "")
                        {
                            DataSet mySetAwal = ObjDb.GetRows("SELECT jumlahAD, tahap1, tahap2, tahap3, tahap4 FROM TransRKAS_AD WHERE noTRKASAD = " + hdnnoTRKAS_AD.Value + " ");
                            DataRow myRowAwal = mySetAwal.Tables[0].Rows[0];
                            string jumlahAwal = myRowAwal["jumlahAD"].ToString();
                            string tahap1Awal = myRowAwal["tahap1"].ToString();
                            string tahap2Awal = myRowAwal["tahap2"].ToString();
                            string tahap3Awal = myRowAwal["tahap3"].ToString();
                            string tahap4Awal = myRowAwal["tahap4"].ToString();
                            
                            DataSet mySetParent = ObjDb.GetRows("SELECT Parents FROM vHierarchy WHERE ChildId = " + cboAkun.Text + " ");
                            DataRow myRowParent = mySetParent.Tables[0].Rows[0];
                            string Parents = myRowParent["Parents"].ToString();

                            string sql = "update TransRKAS_A set jumlahA = (jumlahA - " + Convert.ToDecimal(jumlahAwal) + "), tahap1 = (tahap1 - " + Convert.ToDecimal(tahap1Awal) + "), tahap2 = (tahap2 - " + Convert.ToDecimal(tahap2Awal) + "), tahap3 = (tahap3 - " + Convert.ToDecimal(tahap3Awal) + "), tahap4 = (tahap4 - " + Convert.ToDecimal(tahap4Awal) + " ) where noTRKAS = " + noTRKAS + " and noRek = " + cboAkun.Text + " ";
                            ObjDb.ExecQuery(sql);

                            string sqlParent = "update TransRKAS_A set jumlahA = (jumlahA - " + Convert.ToDecimal(jumlahAwal) + "), tahap1 = (tahap1 - " + Convert.ToDecimal(tahap1Awal) + "), tahap2 = (tahap2 - " + Convert.ToDecimal(tahap2Awal) + "), tahap3 = (tahap3 - " + Convert.ToDecimal(tahap3Awal) + "), tahap4 = (tahap4 - " + Convert.ToDecimal(tahap4Awal) + " ) where noTRKAS = " + noTRKAS + " and noRek in (" + Parents + ") ";
                            ObjDb.ExecQuery(sqlParent);

                            string sqlDelete = "delete TransRKAS_AD where noTRKASAD = " + hdnnoTRKAS_AD.Value + " ";
                            ObjDb.ExecQuery(sqlDelete);
                        }
                    }

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                    ShowHideGridAndForm(false, false, true);
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

        protected void btnBatalRekD_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataFirst(cboThn.Text, ObjSys.GetCabangId);
            ShowHideGridAndForm(true, true, false);
        }

        protected void btnTambah_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataAKun();
            ShowHideGridAndForm(false, false, true);
            setInitialRowGrid();
            for (int i = 1; i < 2; i++)
            {
                AddNewRowGrid();
            }
            hdnJmlTahap.Value = cboJmlTahap.Text;
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView GridView2 = e.Row.FindControl("GridView2") as GridView;

                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "4")
                    {
                        GridView2.Columns[5].Visible = true;
                        GridView2.Columns[6].Visible = true;
                        GridView2.Columns[7].Visible = true;
                        GridView2.Columns[8].Visible = true;
                    }
                    else if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "3")
                    {
                        GridView2.Columns[5].Visible = true;
                        GridView2.Columns[6].Visible = true;
                        GridView2.Columns[7].Visible = true;
                        GridView2.Columns[8].Visible = false;
                    }
                    else if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "2")
                    {
                        GridView2.Columns[5].Visible = true;
                        GridView2.Columns[6].Visible = true;
                        GridView2.Columns[7].Visible = false;
                        GridView2.Columns[8].Visible = false;
                    }
                    else if (Convert.ToInt32(cboJmlTahap.Text).ToString() == "1")
                    {
                        GridView2.Columns[5].Visible = true;
                        GridView2.Columns[6].Visible = false;
                        GridView2.Columns[7].Visible = false;
                        GridView2.Columns[8].Visible = false;
                    }
                }
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            loadDataFirst(cboThn.Text, ObjSys.GetCabangId);
        }

        protected void cboAkun_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            DataSet mySet = ObjDb.GetRows("SELECT noTRKAS FROM TransRKAS_H where tahun = " + cboThn.Text + " and noCabang = " + ObjSys.GetCabangId + "");
            DataRow myRow = mySet.Tables[0].Rows[0];
            string noTRKAS = myRow["noTRKAS"].ToString();

            //Tipe Transaksi mRekening (27) Dana BOS Kepegawaian, (28) Dana BOS Barang dan Jasa
            DataSet mySetBOS = ObjDb.GetRows("SELECT jenis FROM mRekening where noRek = " + cboAkun.Text + " ");
            DataRow myRowBOS = mySetBOS.Tables[0].Rows[0];
            string jenisBOS = myRowBOS["jenis"].ToString();

            DataSet mySetH = ObjDb.GetRows("Select a.noTRKAS, b.noRek from TransRKAS_H a inner join TransRKAS_AD b on a.noTRKAS = b.noTRKAS "+
                "Where a.tahun = " + cboThn.Text + " and a.noCabang = " + ObjSys.GetCabangId + " and b.noRek = " + cboAkun.Text + "");
            if (mySetH.Tables[0].Rows.Count > 0)
            {
                setInitialRowGridUpdate(noTRKAS, cboAkun.Text, jenisBOS);
                if (jenisBOS == "28")
                    btnAddrowRekD.Enabled = true;
                else
                    btnAddrowRekD.Enabled = false;
            }
            else
            {
                if (jenisBOS == "28")
                {
                    setInitialRowGrid();
                    for (int i = 1; i < 2; i++)
                    {
                        AddNewRowGrid();
                    }
                    btnAddrowRekD.Enabled = true;
                }
                else
                {
                    setInitialRowGridBOSPgw(cboAkun.Text);
                    btnAddrowRekD.Enabled = false;
                }

            }

            ShowHideGridAndForm(false, false, true);

        }

        protected void txtVolumeX_TextChanged(object sender, EventArgs e)
        {
            CalculateDetil();
        }

        protected void txtHargaStnX_TextChanged(object sender, EventArgs e)
        {
            CalculateDetil();
        }

        protected void CalculateDetil()
        {
            for (int i = 0; i < grdRekA.Rows.Count; i++)
            {
                TextBox txtVolumeX = (TextBox)grdRekA.Rows[i].FindControl("txtVolumeX");
                TextBox txtHargaStnX = (TextBox)grdRekA.Rows[i].FindControl("txtHargaStnX");
                TextBox txtJumlahX = (TextBox)grdRekA.Rows[i].FindControl("txtJumlahX");
                TextBox txtTahap1X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap1X");
                TextBox txtTahap2X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap2X");
                TextBox txtTahap3X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap3X");
                TextBox txtTahap4X = (TextBox)grdRekA.Rows[i].FindControl("txtTahap4X");

                decimal jumlah = 0, tahap = 0;

                if (txtVolumeX.Text != ""  && txtHargaStnX.Text != "")
                {
                    jumlah = Convert.ToDecimal(txtVolumeX.Text) * Convert.ToDecimal(txtHargaStnX.Text);
                    txtJumlahX.Text = ObjSys.IsFormatNumber(jumlah.ToString());

                    tahap = Convert.ToDecimal(jumlah) / Convert.ToDecimal(hdnJmlTahap.Value);
                    
                    if (hdnJmlTahap.Value == "1")
                    {
                        txtTahap1X.Text = ObjSys.IsFormatNumber(tahap.ToString());
                    }
                    if (hdnJmlTahap.Value == "2")
                    {
                        txtTahap1X.Text = ObjSys.IsFormatNumber(tahap.ToString());
                        txtTahap2X.Text = ObjSys.IsFormatNumber(tahap.ToString());
                    }
                    if (hdnJmlTahap.Value == "3")
                    {
                        txtTahap1X.Text = ObjSys.IsFormatNumber(tahap.ToString());
                        txtTahap2X.Text = ObjSys.IsFormatNumber(tahap.ToString());
                        txtTahap3X.Text = ObjSys.IsFormatNumber(tahap.ToString());
                    }
                    if (hdnJmlTahap.Value == "4")
                    {
                        txtTahap1X.Text = ObjSys.IsFormatNumber(tahap.ToString());
                        txtTahap2X.Text = ObjSys.IsFormatNumber(tahap.ToString());
                        txtTahap3X.Text = ObjSys.IsFormatNumber(tahap.ToString());
                        txtTahap4X.Text = ObjSys.IsFormatNumber(tahap.ToString());
                    }
                }
            }
        }

    }
}