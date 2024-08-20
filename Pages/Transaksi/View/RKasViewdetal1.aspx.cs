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
    public partial class RKasViewdetal1 : System.Web.UI.Page
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
                //divtombol.Visible = false;
            }
        }


        #region LoadData
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", cboYear.Text);
            ObjGlobal.Param.Add("bln", cboMonth.Text);
            ObjGlobal.Param.Add("kdrek", txtSearch.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("Posisi", Posisi.Text);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPviewRdetailcekbankjurnal", ObjGlobal.Param);
            grdAccount.DataBind();
            //divtombol.Visible = true;
        }


        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }
        protected void loadDataCombo()
        {
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang in(2,3,4) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stscabang in(2,3,4) and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stscabang in(2,3,4) and parent = '" + ObjSys.GetCabangId + "') a ");
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
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where stscabang in(2,3,4) and nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

        }
        #endregion

        protected void btnSearchCOA_Click(object sender, EventArgs e)
        {
            dlgAkun.Show();
            loadDataAkun();
        }

        protected void loadDataAkun()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cboCabang.Text);
            ObjGlobal.Param.Add("search", txtSearchAccount.Text);
            grdAkun.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAkun", ObjGlobal.Param);
            grdAkun.DataBind();
        }

        protected void grdAkun_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = grdAkun.SelectedRow.RowIndex;

            string kodeAkun = (grdAkun.SelectedRow.FindControl("lblkdAkun") as Label).Text;
            string namaAkun = (grdAkun.SelectedRow.FindControl("lblKet") as Label).Text;
            string noAkun = (grdAkun.SelectedRow.FindControl("hdnnoAkun") as HiddenField).Value;

            txtSearch.Text = kodeAkun;

            dlgAkun.Hide();
        }

        protected void btnTutup_Click(object sender, EventArgs e)
        {
            dlgAkun.Hide();
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
        }

        protected void btnSearchAccount_Click(object sender, EventArgs e)
        {
            dlgAkun.Show();
            loadDataAkun();
        }
    }
}