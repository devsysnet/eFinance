using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.View
{
    public partial class RLokasiAsset : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        //buat cetakan
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadComboCabang();
                LoadComboFirst();
                LoadData(cboCabang.Text, cboCariLokasi.Text, cboCariSubLokasi.Text, txtSearchAsset.Text);
            }
        }

        protected void LoadData(string cabang = "0", string lokasi = "0", string sublokasi = "0", string search = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            ObjGlobal.Param.Add("nolokasi", lokasi);
            ObjGlobal.Param.Add("noSublokasi", sublokasi);
            ObjGlobal.Param.Add("search", search);
            grdAssetUpdate.DataSource = ObjGlobal.GetDataProcedure("SPEdataasset", ObjGlobal.Param);
            grdAssetUpdate.DataBind();

        }

        protected void LoadComboCabang()
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
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang) a ");
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

            LoadComboFirst();
        }
        protected void LoadComboFirst()
        {
            cboCariLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Semua Lokasi---' name union all SELECT distinct noLokasi id, Lokasi name FROM mLokasi where nocabang='" + cboCabang.Text + "' ) a");
            cboCariLokasi.DataValueField = "id";
            cboCariLokasi.DataTextField = "name";
            cboCariLokasi.DataBind();

            cboCariSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Semua Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi where noLokasi = '" + cboCariLokasi.Text + "') a");
            cboCariSubLokasi.DataValueField = "id";
            cboCariSubLokasi.DataTextField = "name";
            cboCariSubLokasi.DataBind();

            LoadData(cboCabang.Text, cboCariLokasi.Text, cboCariSubLokasi.Text, txtSearchAsset.Text);

        }

        protected void cboCariLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(cboCabang.Text, cboCariLokasi.Text, cboCariSubLokasi.Text, txtSearchAsset.Text);

            cboCariSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Semua Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi where noLokasi = '" + cboCariLokasi.Text + "') a");
            cboCariSubLokasi.DataValueField = "id";
            cboCariSubLokasi.DataTextField = "name";
            cboCariSubLokasi.DataBind();

        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            LoadComboFirst();
            LoadData(cboCabang.Text, cboCariLokasi.Text, cboCariSubLokasi.Text, txtSearchAsset.Text);

        }
        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();

            LoadData(cboCabang.Text, cboCariLokasi.Text, cboCariSubLokasi.Text, txtSearchAsset.Text);

            cboSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Semua Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi ) a");
            cboSubLokasi.DataValueField = "id";
            cboSubLokasi.DataTextField = "name";
            cboSubLokasi.DataBind();

        }

        protected void cboCariSubLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData(cboCabang.Text, cboCariLokasi.Text, cboCariSubLokasi.Text, txtSearchAsset.Text);
        }

        protected void txtSearchAsset_TextChanged(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData(cboCabang.Text, cboCariLokasi.Text, cboCariSubLokasi.Text, txtSearchAsset.Text);
        }

        protected void LoadCombo()
        {
            cboLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Lokasi---' name union all SELECT distinct noLokasi id, Lokasi name FROM mLokasi  ) a");
            cboLokasi.DataValueField = "id";
            cboLokasi.DataTextField = "name";
            cboLokasi.DataBind();

            cboSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi ) a");
            cboSubLokasi.DataValueField = "id";
            cboSubLokasi.DataTextField = "name";
            cboSubLokasi.DataBind();

            cboKelompok.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Kelompok---' name union all SELECT distinct nomAsset id, kelompok+'  ( '+case when jenis=1 then 'Garis Lurus' when jenis=2 then 'Saldo Menurun' else '' end+'  )' as  name FROM mAsset ) a");
            cboKelompok.DataValueField = "id";
            cboKelompok.DataTextField = "name";
            cboKelompok.DataBind();

            cborekdb.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih COA Debet---' name union all SELECT distinct norek id,ket  name FROM mRekening where jenis=8) a");
            cborekdb.DataValueField = "id";
            cborekdb.DataTextField = "name";
            cborekdb.DataBind();

            cborekkd.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih COA Kredit---' name union all SELECT distinct norek id,ket  name FROM mRekening where jenis=7 ) a");
            cborekkd.DataValueField = "id";
            cborekkd.DataTextField = "name";
            cborekkd.DataBind();
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

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        protected void btnResetRek_Click(object sender, EventArgs e)
        {
            CloseMessage();
            ShowHideGridAndForm(true,false);
        }

        protected void grdAssetUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdAssetUpdate.PageIndex = e.NewPageIndex;
            LoadData(cboCabang.Text, cboCariLokasi.Text, cboCariSubLokasi.Text, txtSearchAsset.Text);
        }

        protected void grdAssetUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                string noAset = grdAssetUpdate.DataKeys[rowIndex].Value.ToString();
                hdnId.Value = noAset;
                if (e.CommandName == "detail")
                {

                    HiddenField hdnIdPrint = (HiddenField)grdAssetUpdate.Rows[rowIndex].FindControl("hdnIdPrint");
           

                    HttpContext.Current.Session["ParamReport"] = null;
                    Session["REPORTNAME"] = null;
                    Session["REPORTTITLE"] = null;
                    Param.Clear();
                    Param.Add("noAset", hdnIdPrint.Value);
                    HttpContext.Current.Session.Add("ParamReport", Param);
                    Session["REPORTNAME"] = "ReportKodeAsset.rpt";
                    Session["REPORTTILE"] = "Report Kode Asset";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);


                }

                if(e.CommandName == "SelectEdit")
                {
                   

                    DataSet mySet = ObjDb.GetRows("select isnull(noBarangAsset,0) as noBarangAsset, kodeAsset, namaAsset, tglAsset, " +
                        "isnull(nolokasi,0) as nolokasi, isnull(noSubLokasi,0) as noSubLokasi, isnull(nilaiPerolehan,0) as nilaiPerolehan, " +
                        "uraian, isnull(nokelompok,0) as nokelompok, isnull(norekdb,0) as norekdb, isnull(norekkd,0) as norekkd, danaBOS, " +
                        "ketLainnya, keadaanAset " +
                        "from tAsset where noAset = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    hdnBarang.Value = myRow["noBarangAsset"].ToString();
                    txtKode.Text = myRow["kodeAsset"].ToString();
                    txtNamaBarang.Text = myRow["namaAsset"].ToString();
                    dtTanggal.Text = Convert.ToDateTime(myRow["tglAsset"]).ToString("dd-MMM-yyyy");
                    cboLokasi.Text = myRow["nolokasi"].ToString();
                    cboSubLokasi.Text = myRow["noSubLokasi"].ToString();
                    txtNilai.Text = ObjSys.IsFormatNumber(myRow["nilaiPerolehan"].ToString());
                    txtUraian.Text = myRow["uraian"].ToString();
                    cboKelompok.Text = myRow["nokelompok"].ToString();
                    cborekdb.Text = myRow["norekdb"].ToString();
                    cborekkd.Text = myRow["norekkd"].ToString();
                    cboDana.Text = myRow["danaBOS"].ToString();
                    if (cboDana.Text == "Lain")
                        showhideLainnya.Visible = true;
                    else
                        showhideLainnya.Visible = false;
                    LoadCombo();
                    this.ShowHideGridAndForm(false, true);
                    txtKetLainnya.Text = myRow["ketLainnya"].ToString();
                    cboKeadaan.Text = myRow["keadaanAset"].ToString();
                }
               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

     
    }
}