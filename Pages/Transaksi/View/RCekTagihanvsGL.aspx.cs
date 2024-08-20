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
    public partial class RCekTagihanvsGL : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataPerwakilan();
                cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();

            }
        }

        #region LoadData
        protected void btnPosting_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", cboYear.Text);
            //ObjGlobal.Param.Add("bln", cboMonth.Text);
            //ObjGlobal.Param.Add("kelas", cboKelas.Text);
            //ObjGlobal.Param.Add("jenis", cboJnsTrans.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPRcektagihanvsgl", ObjGlobal.Param);
            grdAccount.DataBind();

        }

        protected void loadDataPerwakilan()
        {
            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang
            //pusat
            //if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            //{
            //    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang) a order by urutan");
            //    cboCabang.DataValueField = "id";
            //    cboCabang.DataTextField = "name";
            //    cboCabang.DataBind();
            //}
            //kantor pusat
            //else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            //{
            //    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang) a ");
            //    cboCabang.DataValueField = "id";
            //    cboCabang.DataTextField = "name";
            //    cboCabang.DataBind();
            //}
            //perwakilan
            //else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            //{
            //    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
            //            "union " +
            //            "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
            //    cboCabang.DataValueField = "id";
            //    cboCabang.DataTextField = "name";
            //    cboCabang.DataBind();
            //}
            //unit
            //else
            //{
            //    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
            //    cboCabang.DataValueField = "id";
            //    cboCabang.DataTextField = "name";
            //    cboCabang.DataBind();
            //}

            //ObjGlobal.Param.Clear();
            //ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            //ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            //ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            //ObjGlobal.Param.Add("parentCabang", ObjSys.GetParentCabang);
            //cboPerwakilan.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataKantorPerwakilan", ObjGlobal.Param);
            //cboPerwakilan.DataValueField = "id";
            //cboPerwakilan.DataTextField = "name";
            //cboPerwakilan.DataBind();

            loadDataUnit();
        }

        protected void loadDataUnit()
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", ObjSys.GetParentCabang);
            //ObjGlobal.Param.Add("perwakilan", cboPerwakilan.Text);
            cboCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitPerwakilan1", ObjGlobal.Param);
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();

            loadDataDetil(cboCabang.Text);

        }

        protected void loadDataDetil(string cabang = "")
        {
            //cboKelas.DataSource = ObjDb.GetRows("select * from (select '--Pilih Kelas--' as id, '--Pilih Kelas--' as name union select distinct kelas as id, kelas as name from TransKelas where nocabang='" + cabang + "')x");
            //cboKelas.DataValueField = "name";
            //cboKelas.DataTextField = "name";
            //cboKelas.DataBind();

            //cboJnsTrans.DataSource = ObjDb.GetRows("select * from (select '0' as id, '--Pilih Semua--' as name union select distinct noTransaksi as id, jenisTransaksi as name from mJenisTransaksi where nocabang='" + cabang + "')x");
            //cboJnsTrans.DataValueField = "id";
            //cboJnsTrans.DataTextField = "name";
            //cboJnsTrans.DataBind();
        }
        #endregion

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataDetil(cboCabang.Text);
        }

        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataUnit();
        }
    }
}


