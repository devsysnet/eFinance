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
    public partial class TranskenaikanGajiberkalaview : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
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
            ObjGlobal.Param.Add("bln", cboMonth.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPLoaddatanaikgajinonrutin1", ObjGlobal.Param);
            grdAccount.DataBind();

        }

        protected void loadDataCombo()
        {
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

        }
        #endregion

        protected void btnCari_Click(object sender, EventArgs e)
        {
            //LoadDataGrid();
        }

    }
}