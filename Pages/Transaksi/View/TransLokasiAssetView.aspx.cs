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

namespace eFinance.Pages.Transaksi.View
{
    public partial class TransLokasiAssetView : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadComboCabang();
                loadDataFirstCombo();
                loadDataFirst(cboCabang.Text, cboLokasi.Text, cboSubLokasi.Text, txtSearch.Text);
            }
            
        }

        protected void loadDataFirst(string cabang = "0", string lokasi = "0", string sublokasi = "0", string cari = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Cabang", cabang);
            ObjGlobal.Param.Add("noLokasi", lokasi);
            ObjGlobal.Param.Add("noSublokasi", sublokasi);
            ObjGlobal.Param.Add("cari", cari);
            grdLokasiAssetView.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPenerimaanAssetView", ObjGlobal.Param);
            grdLokasiAssetView.DataBind();

            if (grdLokasiAssetView.Rows.Count > 0)
                button.Visible = true;
            else
                button.Visible = false;
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

            loadDataFirstCombo();
        }

        protected void loadDataFirstCombo()
        {
            cboLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Semua Lokasi---' name union all SELECT distinct noLokasi id, Lokasi name FROM mLokasi where nocabang='" + cboCabang.Text + "' ) a");
            cboLokasi.DataValueField = "id";
            cboLokasi.DataTextField = "name";
            cboLokasi.DataBind();

            cboSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Semua Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi where noLokasi = '" + cboLokasi.Text + "') a");
            cboSubLokasi.DataValueField = "id";
            cboSubLokasi.DataTextField = "name";
            cboSubLokasi.DataBind();

            loadDataFirst(cboCabang.Text, cboLokasi.Text, cboSubLokasi.Text, txtSearch.Text);

        }

        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {

            CloseMessage();

            loadDataFirst(cboCabang.Text, cboLokasi.Text, cboSubLokasi.Text, txtSearch.Text);

            cboSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Semua Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi where noLokasi = '" + cboLokasi.Text + "') a");
            cboSubLokasi.DataValueField = "id";
            cboSubLokasi.DataTextField = "name";
            cboSubLokasi.DataBind();


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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            clearData();
        }

        protected void clearData()
        {
            cboLokasi.Text = "0";
            cboSubLokasi.Text = "0";
            txtSearch.Text = "";
            loadDataFirst(cboCabang.Text, cboLokasi.Text, cboSubLokasi.Text, txtSearch.Text);
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadDataFirst(cboCabang.Text, cboLokasi.Text, cboSubLokasi.Text, txtSearch.Text);
        }

        protected void cboSubLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataFirst(cboCabang.Text, cboLokasi.Text, cboSubLokasi.Text, txtSearch.Text);

        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataFirstCombo();
            loadDataFirst(cboCabang.Text, cboLokasi.Text, cboSubLokasi.Text, txtSearch.Text);

        }
    }
}