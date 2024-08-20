using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Data.Sql;
using System.Data.SqlClient;
using InfoSoftGlobal;
using System.Text;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace eFinance.Pages.Transaksi.View
{
    public partial class RTracking : System.Web.UI.Page
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
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang) a ");
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

            cboTahun.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataTahun");
            cboTahun.DataValueField = "id";
            cboTahun.DataTextField = "name";
            cboTahun.DataBind();
            cboTahun.SelectedValue = (System.DateTime.Now.Year).ToString();

        }

        protected void LoadDataTracking(string cabang = "0", string user = "", string tahun = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            ObjGlobal.Param.Add("noUser", user);
            ObjGlobal.Param.Add("tahun", tahun);
            if (cabang != "0" || cabang != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);
                }
            }
            grdTracking.DataSource = ObjGlobal.GetDataProcedure("SPViewtreakinginput", ObjGlobal.Param);
            grdTracking.DataBind();
        }

        protected void grdTracking_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();

            cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = "";
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Jan";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Feb";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Mar";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Apr";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "May";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Jun";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Jul";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Aug";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Sep";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Okt";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Nov";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Dec";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            grdTracking.HeaderRow.Parent.Controls.AddAt(0, row);
        }

        protected void grdTracking_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "cursor:help;");
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Alternate)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#96c6ea'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");
                    e.Row.BackColor = Color.FromName("#ffffff");
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#96c6ea'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#d7e9f7'");
                    e.Row.BackColor = Color.FromName("#d7e9f7");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDataTracking(cboCabang.Text, ObjSys.GetUserId, cboTahun.Text);
        }
    }
}