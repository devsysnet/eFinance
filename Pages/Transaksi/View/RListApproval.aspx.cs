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
    public partial class RListApproval : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadComboCabang();
                LoadComboTransaksi();
                LoadData(cboCabang.Text, cboTransaksi.Text);
            }
        }

        protected void LoadComboCabang()
        {
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
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang in (2,3,4) and parent = '" + ObjSys.GetCabangId + "') a ");
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

        }

        protected void LoadComboTransaksi()
        {
            cboTransaksi.DataSource = ObjDb.GetRows("select a.* from (select noParameterApprove as id, namaParameterApprove as name from MstParameterApprove where stsParameterApprove = 1) a");
            cboTransaksi.DataValueField = "id";
            cboTransaksi.DataTextField = "name";
            cboTransaksi.DataBind();
        }
        protected void LoadData(string cabang = "", string transaksi = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            ObjGlobal.Param.Add("noTransaksi", transaksi);
            grdListApproval.DataSource = ObjGlobal.GetDataProcedure("SPDataListApproval", ObjGlobal.Param);
            grdListApproval.DataBind();

            LoadDataGrid();
        }

        string previousCellValue = "";
        int previousCellCount = 1;
        protected void grdListApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //check if the row is a datarow and not the first row
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //cast the dataitem back to a row
                DataRowView row = e.Row.DataItem as DataRowView;

                //check if the current id matches the previous row
                if (previousCellValue == row["kdTrans"].ToString())
                {
                    //count the number of same cells
                    previousCellCount++;
                }
                else
                {
                    //span the rows for the first two cells
                    if (previousCellCount > 1)
                    {
                        grdListApproval.Rows[e.Row.RowIndex - previousCellCount].Cells[0].RowSpan = previousCellCount;
                        grdListApproval.Rows[e.Row.RowIndex - previousCellCount].Cells[1].RowSpan = previousCellCount;
                        grdListApproval.Rows[e.Row.RowIndex - previousCellCount].Cells[2].RowSpan = previousCellCount;
                        //hide the other cells in the column
                        for (int i = 1; i < previousCellCount; i++)
                        {
                            grdListApproval.Rows[(e.Row.RowIndex - previousCellCount) + i].Cells[0].Visible = false;
                            grdListApproval.Rows[(e.Row.RowIndex - previousCellCount) + i].Cells[1].Visible = false;
                            grdListApproval.Rows[(e.Row.RowIndex - previousCellCount) + i].Cells[2].Visible = false;
                        }
                    }

                    previousCellValue = row["kdTrans"].ToString();
                    previousCellCount = 1;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //use the footer row to create spanning for the last rows if needed
                if (previousCellCount > 1)
                {
                    grdListApproval.Rows[grdListApproval.Rows.Count - previousCellCount].Cells[0].RowSpan = previousCellCount;
                    grdListApproval.Rows[grdListApproval.Rows.Count - previousCellCount].Cells[1].RowSpan = previousCellCount;
                    grdListApproval.Rows[grdListApproval.Rows.Count - previousCellCount].Cells[2].RowSpan = previousCellCount;

                    //hide the other cells in the column
                    for (int i = 1; i < previousCellCount; i++)
                    {
                        grdListApproval.Rows[(grdListApproval.Rows.Count - previousCellCount) + i].Cells[0].Visible = false;
                        grdListApproval.Rows[(grdListApproval.Rows.Count - previousCellCount) + i].Cells[1].Visible = false;
                        grdListApproval.Rows[(grdListApproval.Rows.Count - previousCellCount) + i].Cells[2].Visible = false;
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(cboCabang.Text, cboTransaksi.Text);
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cboTransaksi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void LoadDataGrid()
        {
            for (int i = 0; i < grdListApproval.Rows.Count; i++)
            {
                if (cboTransaksi.SelectedValue == "2") //Budget
                {
                    grdListApproval.Columns[0].Visible = true;
                    grdListApproval.Columns[1].Visible = true;
                    grdListApproval.Columns[2].Visible = false;
                    grdListApproval.Columns[3].Visible = false;
                    grdListApproval.Columns[4].Visible = true;
                    grdListApproval.Columns[5].Visible = true;
                    grdListApproval.Columns[6].Visible = true;
                    grdListApproval.Columns[7].Visible = true;
                }
                else
                {
                    grdListApproval.Columns[0].Visible = false;
                    grdListApproval.Columns[1].Visible = false;
                    grdListApproval.Columns[2].Visible = true;
                    grdListApproval.Columns[3].Visible = true;
                    grdListApproval.Columns[4].Visible = true;
                    grdListApproval.Columns[5].Visible = true;
                    grdListApproval.Columns[6].Visible = true;
                    grdListApproval.Columns[7].Visible = true;
                }
            }
        }
    }
}