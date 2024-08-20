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


namespace eFinance.Pages.Master.Update
{
    public partial class MstgajikaryawanUpdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                LoadDataCombo();
                loadData(cboUnit.Text);
            }
        }

        protected void loadData(string unit = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("unit", unit);
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdGajiKar.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataGajiKaryawanUpdate", ObjGlobal.Param);
            grdGajiKar.DataBind();
        }

        protected void cmdMode_Click(object sender, EventArgs e)
        {

        }
        protected void LoadDataCombo()
        {
            cboStskaryawan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Status-' name union all SELECT distinct nostatuspeg id, statuspegawai name FROM Mmststatuspegawai where sts = '1') a");
            cboStskaryawan.DataValueField = "id";
            cboStskaryawan.DataTextField = "name";
            cboStskaryawan.DataBind();

            cboGolPegawai.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Golongan-' name union all SELECT distinct noGolongan id, Golongan name FROM MstGolongan where sts = '1') a");
            cboGolPegawai.DataValueField = "id";
            cboGolPegawai.DataTextField = "name";
            cboGolPegawai.DataBind();

            cboDepartemen.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Departemen-' name union all SELECT distinct noDepartemen id, departemen name FROM Mstdepartemen where sts = '1') a");
            cboDepartemen.DataValueField = "id";
            cboDepartemen.DataTextField = "name";
            cboDepartemen.DataBind();

            DataSet mySet = ObjDb.GetRows("select * from mcabang where stspusat = 1");

            DataRow myRow = mySet.Tables[0].Rows[0];
            string parent = myRow["nocabang"].ToString();
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("yayasan", parent);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", ObjSys.GetParentCabang);
            cboCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataWilayah", ObjGlobal.Param);
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();
            LoadDataCombo3(cboCabang.Text);
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
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
            CloseMessage();
            loadData(cboUnit.Text);
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

                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noGajiKaryawan", hdnIndex.Value);
                    ObjDb.Data.Add("modiBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modiDate", ObjSys.GetNow);
                    ObjDb.Update("MstGaji_H", ObjDb.Data, ObjDb.Where);
                    //--------------
                    DataSet mySetH = ObjDb.GetRows("select * from MstGaji_H where noGajiKaryawan = '" + hdnIndex.Value + "'");
                    DataRow myRowH = mySetH.Tables[0].Rows[0];
                    string noGajiKaryawan = myRowH["noGajiKaryawan"].ToString();
                    string noKaryawand = myRowH["noKaryawan"].ToString();

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noGajiKaryawan", noGajiKaryawan);
                    ObjDb.Delete("MstGaji_D", ObjDb.Where);

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("nokaryawan", noKaryawand);
                    ObjGlobal.Param.Add("noGajiKaryawan", noGajiKaryawan);
                    ObjGlobal.Param.Add("gapok", Convert.ToDecimal(txtNilai.Text).ToString());
                    ObjGlobal.ExecuteProcedure("SPInsertMstGajipokokD", ObjGlobal.Param);

                    for (int i = 0; i < grdSaldoGL.Rows.Count; i++)
                    {
                        HiddenField hdnNoRek = (HiddenField)grdSaldoGL.Rows[i].FindControl("hdnNoRek");
                        TextBox txtDebet = (TextBox)grdSaldoGL.Rows[i].FindControl("txtDebet");
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("nokaryawan", noKaryawand);
                        ObjGlobal.Param.Add("noGajiKaryawan", noGajiKaryawan);
                        ObjGlobal.Param.Add("noKomponenGaji", hdnNoRek.Value);
                        ObjGlobal.Param.Add("nilai", Convert.ToDecimal(txtDebet.Text).ToString());
                        ObjGlobal.ExecuteProcedure("SPInsertMstGajiD", ObjGlobal.Param);

                    }
                    loadData(cboUnit.Text);
                    this.ShowHideGridAndForm(true, false, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan");
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
            loadData(cboUnit.Text);
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
                string noKaryawan = grdGajiKar.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noKaryawan;

                DataSet MySet = ObjDb.GetRows("select a.*,CAST(CONVERT(varchar, CAST(c.nilai AS Money), 1) AS varchar) as nilai,CAST(CONVERT(varchar, CAST(c.NilaiKWI AS Money), 1) AS varchar) as NilaiKWI, b.noGajiKaryawan from mstKaryawan a inner join MstGaji_H b on a.nokaryawan=b.nokaryawan inner join MstGaji_D c on b.noGajiKaryawan=c.noGajiKaryawan inner join Mstkomponengaji e on c.nokomponengaji=e.nokomponengaji where a.noKaryawan = '" + noKaryawan + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow myRow = MySet.Tables[0].Rows[0];

                    hdnIndex.Value = myRow["noGajiKaryawan"].ToString();
                    txtNama.Text = myRow["nama"].ToString();
                    txtNUPTK.Text = myRow["NUPTK"].ToString();
                    hdnnoKaryawan.Value = myRow["noKaryawan"].ToString();
                    hdnnocabang.Value = myRow["nocabang"].ToString();
                    if (String.IsNullOrEmpty(myRow["tglmasuk"].ToString()))
                        dtmasuk.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                    else
                        dtmasuk.Text = Convert.ToDateTime(myRow["tglmasuk"]).ToString("dd-MMM-yyyy");
                    cboGolPegawai.Text = myRow["golongan"].ToString();
                    cboDepartemen.Text = myRow["dept"].ToString();
                    txtNilai.Text = myRow["nilai"].ToString();
                    cboStskaryawan.Text = myRow["statusPeg"].ToString();

                    LoadDataCombo();

                    loadDetil(myRow["noGajiKaryawan"].ToString());

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
        protected void LoadDataCombo3(string cabang = "")
        {
      
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
                ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
                ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
                ObjGlobal.Param.Add("perwakilan", cabang);
                cboUnit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnit", ObjGlobal.Param);
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();


           

        }
        protected void loadDetil(string noGajiKaryawan = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noGajiKaryawan", noGajiKaryawan);
            grdSaldoGL.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataGajiKaryawanDetilUpdate", ObjGlobal.Param);
            grdSaldoGL.DataBind();
        }
        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(cboUnit.Text);

        }
        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(cboUnit.Text);
            LoadDataCombo3(cboCabang.Text);
        }
    }
}