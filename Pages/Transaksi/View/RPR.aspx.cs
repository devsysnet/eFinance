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
    public partial class RPR : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCombo();
                loadData(cboYear.Text, cboCabang.Text);
            }
        }

        protected void loadData(string tahun = "", string cabang = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("year", tahun);
            ObjGlobal.Param.Add("nocabang", cabang);
            grdPRView.DataSource = ObjGlobal.GetDataProcedure("SPReportPR", ObjGlobal.Param);
            grdPRView.DataBind();
            if (grdPRView.Rows.Count > 0)
                btnConvertExcel.Visible = true;
            else
                btnConvertExcel.Visible = false;
        }

        protected void loadCombo()
        {
            cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
            cboYear.DataValueField = "id";
            cboYear.DataTextField = "name";
            cboYear.DataBind();

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
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData(cboYear.Text, cboCabang.Text);
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

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        
        protected void btnConvertExcel_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";
            try
            {
                if (valid == true)
                {
                    using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                    {

                        SqlCommand cmd = new SqlCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        DataTable dt = new DataTable();
                        try
                        {
                            cmd = new SqlCommand("SPReportPR", con);
                            cmd.Parameters.Add(new SqlParameter("year", cboYear.Text));
                            cmd.Parameters.Add(new SqlParameter("nocabang", cboCabang.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            //int nokolom = 1;
                            // creating an array 
                            int[] a_array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                            foreach (DataColumn column in dt.Columns)
                            //foreach (int column in a_array)
                            {
                                //Add the Header row for CSV file.
                                csv += column.ColumnName + ',';
                                //csv += "column" + nokolom++ + ',';
                            }
                            //Remove comma in last
                            csv = csv.TrimEnd(',');

                            //Add new line.
                            csv += "\r\n";

                            foreach (DataRow row in dt.Rows)
                            {
                                int no = 1;
                                string comma = "";
                                int number = dt.Columns.Count;
                                while (number < 11)
                                {
                                    number = number + 1;
                                    comma += ',';
                                }
                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Data rows.
                                    no++;
                                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                }

                                //Remove comma in last
                                csv = csv.TrimEnd(',');

                                csv = csv + "" + comma;

                                //Add new line.
                                csv += "\r\n";
                            }

                            //Download the CSV file.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition", "attachment;filename=Report PR " + cboYear.Text + ".csv");
                            Response.Charset = "";
                            Response.ContentType = "application/text";
                            Response.Output.Write(csv);
                            Response.Flush();
                            Response.End();

                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            ShowMessage("error", ex.ToString());
                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage(alert, message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        string previousCellValue = "";
        int previousCellCount = 1;
        protected void grdPRView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //check if the row is a datarow and not the first row
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //cast the dataitem back to a row
                DataRowView row = e.Row.DataItem as DataRowView;

                //check if the current id matches the previous row
                if (previousCellValue == row["Kode_PR"].ToString())
                {
                    //count the number of same cells
                    previousCellCount++;
                }
                else
                {
                    //span the rows for the first two cells
                    if (previousCellCount > 1)
                    {
                        grdPRView.Rows[e.Row.RowIndex - previousCellCount].Cells[0].RowSpan = previousCellCount;
                        grdPRView.Rows[e.Row.RowIndex - previousCellCount].Cells[1].RowSpan = previousCellCount;
                        grdPRView.Rows[e.Row.RowIndex - previousCellCount].Cells[2].RowSpan = previousCellCount;
                        grdPRView.Rows[e.Row.RowIndex - previousCellCount].Cells[3].RowSpan = previousCellCount;                        //hide the other cells in the column
                        for (int i = 1; i < previousCellCount; i++)
                        {
                            grdPRView.Rows[(e.Row.RowIndex - previousCellCount) + i].Cells[0].Visible = false;
                            grdPRView.Rows[(e.Row.RowIndex - previousCellCount) + i].Cells[1].Visible = false;
                            grdPRView.Rows[(e.Row.RowIndex - previousCellCount) + i].Cells[2].Visible = false;
                            grdPRView.Rows[(e.Row.RowIndex - previousCellCount) + i].Cells[3].Visible = false;
                        }
                    }

                    previousCellValue = row["Kode_PR"].ToString();
                    previousCellCount = 1;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //use the footer row to create spanning for the last rows if needed
                if (previousCellCount > 1)
                {
                    grdPRView.Rows[grdPRView.Rows.Count - previousCellCount].Cells[0].RowSpan = previousCellCount;
                    grdPRView.Rows[grdPRView.Rows.Count - previousCellCount].Cells[1].RowSpan = previousCellCount;
                    grdPRView.Rows[grdPRView.Rows.Count - previousCellCount].Cells[2].RowSpan = previousCellCount;
                    grdPRView.Rows[grdPRView.Rows.Count - previousCellCount].Cells[3].RowSpan = previousCellCount;

                    //hide the other cells in the column
                    for (int i = 1; i < previousCellCount; i++)
                    {
                        grdPRView.Rows[(grdPRView.Rows.Count - previousCellCount) + i].Cells[0].Visible = false;
                        grdPRView.Rows[(grdPRView.Rows.Count - previousCellCount) + i].Cells[1].Visible = false;
                        grdPRView.Rows[(grdPRView.Rows.Count - previousCellCount) + i].Cells[2].Visible = false;
                        grdPRView.Rows[(grdPRView.Rows.Count - previousCellCount) + i].Cells[3].Visible = false;
                    }
                }
            }
        }
    }
}