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


namespace eFinance.Pages.Transaksi.Approval
{
    public partial class verifikasigaji : System.Web.UI.Page
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
                loadData();
            }
        }



        protected void loadData()
        {
            grdCustomer.DataSource = ObjDb.GetRows("select a.*,b.departemen from MstKaryawan a inner join MstGaji_H c on a.nokaryawan=c.nokaryawan left join Mstdepartemen b on a.dept=b.nodepartemen where nama LIKE '%" + txtSearch.Text + "%' and c.verifikasi='0' and a.nocabang='" + cboCabang.Text + "' and a.nokaryawan not in (select nokaryawan from TGajibulanan)");
            grdCustomer.DataBind();
            CloseMessage();
        }

        protected void cmdMode_Click(object sender, EventArgs e)
        {

        }
        protected void LoadDataCombo()
        {

            cboGolPegawai.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noGolongan id, Golongan name FROM MstGolongan where sts = '1') a");
            cboGolPegawai.DataValueField = "id";
            cboGolPegawai.DataTextField = "name";
            cboGolPegawai.DataBind();

            cboDepartemen.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noDepartemen id, departemen name FROM Mstdepartemen where sts = '1') a");
            cboDepartemen.DataValueField = "id";
            cboDepartemen.DataTextField = "name";
            cboDepartemen.DataBind();

            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang
            //pusat
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang in(2,3,4)) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang where stscabang in(2,3,4)) a ");
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
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "' and stscabang in(2,3,4)) a");
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

                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noGajiKaryawan", hdnIndex.Value);
                    ObjDb.Data.Add("verifikasi", "1");
                    ObjDb.Data.Add("modiBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modiDate", ObjSys.GetNow);
                    ObjDb.Update("MstGaji_H", ObjDb.Data, ObjDb.Where);
                    //--------------
                    DataSet mySetH = ObjDb.GetRows("select * from MstGaji_H where noGajiKaryawan = '" + hdnIndex.Value + "'");
                    DataRow myRowH = mySetH.Tables[0].Rows[0];
                    string noGajiKaryawan = myRowH["noGajiKaryawan"].ToString();
                    string noKaryawand = myRowH["noKaryawan"].ToString();
                    loadData();
                    this.ShowHideGridAndForm(true, false, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil di Verifikasi");
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

        protected void btnSimpan1_Click(object sender, EventArgs e)
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
                    ObjDb.Data.Add("verifikasi", "2");
                    ObjDb.Data.Add("modiBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modiDate", ObjSys.GetNow);
                    ObjDb.Update("MstGaji_H", ObjDb.Data, ObjDb.Where);
                    //--------------
                    DataSet mySetH = ObjDb.GetRows("select * from MstGaji_H where noGajiKaryawan = '" + hdnIndex.Value + "'");
                    DataRow myRowH = mySetH.Tables[0].Rows[0];
                    string noGajiKaryawan = myRowH["noGajiKaryawan"].ToString();
                    string noKaryawand = myRowH["noKaryawan"].ToString();
                    loadData();
                    this.ShowHideGridAndForm(true, false, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil di Verifikasi");
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

        protected void grdCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCustomer.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false, false);
            clearData();
            CloseMessage();
        }

        protected void grdCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdCustomer.SelectedRow.RowIndex;
                string noKaryawan = grdCustomer.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noKaryawan;

                DataSet MySet = ObjDb.GetRows("select a.*,CAST(CONVERT(varchar, CAST(c.nilai AS Money), 1) AS varchar) as nilai, b.noGajiKaryawan from mstKaryawan a inner join MstGaji_H b on a.nokaryawan=b.nokaryawan inner join MstGaji_D c on b.noGajiKaryawan=c.noGajiKaryawan inner join Mstkomponengaji e on c.nokomponengaji=e.nokomponengaji where a.noKaryawan = '" + noKaryawan + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow myRow = MySet.Tables[0].Rows[0];

                    hdnIndex.Value = myRow["noGajiKaryawan"].ToString();
                    txtNama.Text = myRow["nama"].ToString();
                    txtNUPTK.Text = myRow["NUPTK"].ToString();
                    hdnnoKaryawan.Value = myRow["noKaryawan"].ToString();
                    hdnnocabang.Value = myRow["nocabang"].ToString();
                    dtlahir.Text = Convert.ToDateTime(myRow["tgllahir"]).ToString("dd-MMM-yyyy");
                    cboGolPegawai.Text = myRow["golongan"].ToString();
                    cboDepartemen.Text = myRow["dept"].ToString();
                    txtNilai.Text = myRow["nilai"].ToString();

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

        protected void loadDetil(string noGajiKaryawan = "")
        {
            grdSaldoGL.DataSource = ObjDb.GetRowsDataTable("select a.nokomponengaji, a.komponengaji, b.nilai " +
                "from Mstkomponengaji a inner join MstGaji_D b on a.nokomponengaji = b.nokomponengaji " +
                "where a.kategori=1 and a.jenis=1 and b.noGajiKaryawan = " + noGajiKaryawan + "");
            grdSaldoGL.DataBind();
            //if (grdSaldoGL.Rows.Count > 0)
            //{
            //    btnSimpan.Visible = true;
            //    btnReset.Visible = true;
            //}
            //else
            //{
            //    btnSimpan.Visible = false;
            //    btnReset.Visible = false;
            //}
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }
    }
}