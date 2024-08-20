using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;


namespace eFinance.Pages.Transaksi.Input
{
    public partial class Transbagitampungan : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            //execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                LoadDataCombo();
                loadData();

            }
        }



        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cboCabang.Text);
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdGajiKar.DataSource = ObjGlobal.GetDataProcedure("SPLoadDatatampungan", ObjGlobal.Param);
            grdGajiKar.DataBind();
           
          
        }

      
       protected void LoadDataParent()
       {
            cboCabangParent.DataSource = ObjDb.GetRows("select a.* from (SELECT '0' id, '---Pilih Perwakilan---' name UNION SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1) a ");
            cboCabangParent.DataValueField = "id";
            cboCabangParent.DataTextField = "name";
            cboCabangParent.DataBind();
        }

        protected void LoadDataChild(string parent = "0")
        {
            cboCabang1.DataSource = ObjDb.GetRows("SELECT '0' id, '---Pilih Unit---' name UNION SELECT distinct nocabang id,namaCabang name FROM vCabang where stscabang=2 and parent = " + parent + "");
            cboCabang1.DataValueField = "id";
            cboCabang1.DataTextField = "name";
            cboCabang1.DataBind();
        }
        protected void LoadDataCombo()
        {
    
            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang
            //pusat
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE noCabang = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //perwakilan
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //unit
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
        }

        protected void grdSaldoGL_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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

        protected void btnCari_Click(object sender, EventArgs e)
        {
            loadData();
        }
        private void clearData()
        {

        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;

                if (valid == true)
                {
                    if (ObjDb.GetRows("SELECT * FROM Ttampunganpengeluaran_D WHERE notampungan = '" + hdnotampungan.Value + "' and noCabang = '" + cboCabang1.Text + "'").Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Sudah Pernah Dialokasikan ke Cabang Ini");
                    }
                    else if (cboCabang1.Text == "0")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Cabang Harus Dipilih");
                    }
                    else if (Convert.ToDecimal(txtTotalDebit.Text) > Convert.ToDecimal(txtSaldo.Text))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Nilai Alokasi Tidak Boleh Lebih besar dari Saldo");
                    }

                    else
                    {
                        string Kode = "";
                        Kode = ObjSys.GetCodeAutoNumberNew("32", Convert.ToDateTime(dtmasuk.Text).ToString("yyyy-MM-dd"));

                        for (int i = 0; i < grdSaldoGL.Rows.Count; i++)
                            {
                                HiddenField hdnNoRek = (HiddenField)grdSaldoGL.Rows[i].FindControl("hdnNoRek");
                                TextBox txtDebet = (TextBox)grdSaldoGL.Rows[i].FindControl("txtDebit");
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("kdtran", Kode);
                                ObjGlobal.Param.Add("norek",hdnNoRek.Value);
                                ObjGlobal.Param.Add("notampungan", hdnotampungan.Value);
                                ObjGlobal.Param.Add("dtmasuk", Convert.ToDateTime(dtmasuk.Text).ToString("yyyy-MM-dd"));
                                ObjGlobal.Param.Add("nocabang", cboCabang1.Text);
                                ObjGlobal.Param.Add("uraian", txtRemark.Text);
                                ObjGlobal.Param.Add("nilai", Convert.ToDecimal(txtDebet.Text).ToString());
                                ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                                ObjGlobal.Param.Add("createdDate", ObjSys.GetNow);
                                ObjGlobal.ExecuteProcedure("SpInserttkasdetailtamp", ObjGlobal.Param);

                        }

                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("kdtran", Kode);
                                ObjGlobal.Param.Add("notampungan", hdnotampungan.Value);
                                ObjGlobal.Param.Add("dtmasuk", Convert.ToDateTime(dtmasuk.Text).ToString("yyyy-MM-dd"));
                                ObjGlobal.Param.Add("nocabang", cboCabang1.Text);
                                ObjGlobal.Param.Add("uraian", txtRemark.Text);
                                ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                                ObjGlobal.Param.Add("createdDate", ObjSys.GetNow);
                                ObjGlobal.ExecuteProcedure("SpInserttkasdetailtamplawan", ObjGlobal.Param);

                                ObjSys.UpdateAutoNumberCodeNew("32", Convert.ToDateTime(dtmasuk.Text).ToString("yyyy-MM-dd"));

                        loadData();


                            this.ShowHideGridAndForm(true, false, false);
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            ShowMessage("success", "Data berhasil disimpan");
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


        protected void grdGajiKar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdGajiKar.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false, false);
            clearData();
            CloseMessage();
        }

        protected void grdGajiKar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdGajiKar.SelectedRow.RowIndex;
                string notampungan = grdGajiKar.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = notampungan;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("notampungan", notampungan);
                grdSaldoGL.DataSource = ObjGlobal.GetDataProcedure("SPLoadDatatampunganDetail", ObjGlobal.Param);
                grdSaldoGL.DataBind();
                if (grdSaldoGL.Rows.Count > 0)
                {
                    btnSimpan.Visible = true;
                    btnReset.Visible = true;
                }
                else
                {
                    btnSimpan.Visible = false;
                    btnReset.Visible = false;
                }

                DataSet MySet = ObjDb.GetRows("select a.notampungan,a.tgl,c.jenisTampungan,b.nomorKode,CAST(CONVERT(varchar, CAST(a.nilai AS Money), 1) AS varchar) as nilai,CAST(CONVERT(varchar, CAST(a.saldo AS Money), 1) AS varchar) as saldo,b.uraian from Ttampunganpengeluaran a inner join tKas b on a.nokas=b.noKas inner join mTampPengeluaran_H c on a.nomtampungan=c.nomtampungan  where a.notampungan = '" + notampungan + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow myRow = MySet.Tables[0].Rows[0];

                    txtjenis.Text = myRow["jenisTampungan"].ToString();
                    txtNomorkode.Text = myRow["nomorkode"].ToString();
                    txtRemark.Text = myRow["uraian"].ToString();
                    hdnotampungan.Value = myRow["notampungan"].ToString();
                    dtmasuk.Text = Convert.ToDateTime(myRow["tgl"]).ToString("dd-MMM-yyyy");
                    txtNilai.Text = myRow["nilai"].ToString();
                    txtSaldo.Text = myRow["saldo"].ToString();

                    LoadDataParent();
                    LoadDataChild(cboCabangParent.Text);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);

                    this.ShowHideGridAndForm(false, true, false);
                    CloseMessage();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Tidak ada.");
                }
                

            }
            catch (Exception ex)
            {
                if (valid == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", message);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }
            }
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void cboCabangParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataChild(cboCabangParent.Text);
        }
    }
}