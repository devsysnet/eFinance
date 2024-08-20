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
    public partial class Rpenyusutanasset : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();

            }
        }


        #region LoadData
        protected void btnPosting_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nolokasi", cboLokasi.Text);
            ObjGlobal.Param.Add("noSublokasi", cboSubLokasi.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPEdataasset1", ObjGlobal.Param);
            grdAccount.DataBind();

        }

        protected void loadDataCombo()
        {
            if (ObjSys.GetstsCabang == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'ALL' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

            if (ObjSys.GetstsCabang == "1")
            {
                cboLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct a.noLokasi id, a.Lokasi name FROM mLokasi a) a");
                cboLokasi.DataValueField = "id";
                cboLokasi.DataTextField = "name";
                cboLokasi.DataBind();
            }
            else
            {
                cboLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct a.noLokasi id, a.Lokasi name FROM mLokasi a where a.nocabang='" + ObjSys.GetCabangId + "' ) a");
                cboLokasi.DataValueField = "id";
                cboLokasi.DataTextField = "name";
                cboLokasi.DataBind();
            }
        }
        #endregion
        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi where noLokasi = '" + cboLokasi.Text + "') a");
            cboSubLokasi.DataValueField = "id";
            cboSubLokasi.DataTextField = "name";
            cboSubLokasi.DataBind();
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            //LoadDataGrid();
        }

    }
}
