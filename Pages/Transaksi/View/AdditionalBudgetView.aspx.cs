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
    public partial class AdditionalBudgetView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

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
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPviewAdditonal", ObjGlobal.Param);
            grdAccount.DataBind();
        }

        protected void loadDataCombo()
        {
            if (ObjSys.GetstsCabang == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang) a");
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
        }
        #endregion

        protected void btnCari_Click(object sender, EventArgs e)
        {
            //LoadDataGrid();
        }

    }
}
