using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;


namespace eFinance.Pages.Transaksi.Upload
{
    public partial class ExportImportTableTransaksiLive : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
                loadShowExportImport();
                loadEnableDisableAksi();
            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);

            cboMonth.DataSource = ObjGlobal.GetDataProcedure("SPLoadBulanSaldoBln", ObjGlobal.Param);
            cboMonth.DataValueField = "bln";
            cboMonth.DataTextField = "bulan";
            cboMonth.DataBind();

            cboYear.DataSource = ObjGlobal.GetDataProcedure("SPLoadTahunSaldoBln", ObjGlobal.Param);
            cboYear.DataValueField = "thn";
            cboYear.DataTextField = "thn";
            cboYear.DataBind();

            cboTabel.DataSource = ObjGlobal.GetDataProcedure("SPLoadTabelExportTransaksiLive");
            cboTabel.DataValueField = "namaTabel";
            cboTabel.DataTextField = "descTabel";
            cboTabel.DataBind();

        }

        protected void loadEnableDisableAksi()
        {
            DataSet mySetSts = ObjDb.GetRows("select a.parent from mCabang a inner join mUser b on a.noCabang=b.noCabang where a.stsCabang=3 and b.noUser='" + ObjSys.GetUserId + "' ");
            if (mySetSts.Tables[0].Rows.Count > 0)
            {
                DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                string parent = myRowSts["parent"].ToString();

                DataSet mySetUnit = ObjDb.GetRows("SELECT parent, STUFF((SELECT ', ' + CAST(noCabang AS VARCHAR(10))[text()] " +
                            "FROM mCabang WHERE parent = t.parent and stsCabang = 2 FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ' ') nocabangUnit " +
                            "FROM mCabang t where t.parent = '" + parent + "' and t.stsCabang = 2 GROUP BY parent ");

                if (mySetUnit.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowUnit = mySetUnit.Tables[0].Rows[0];
                    string nocabangUnit = myRowUnit["nocabangUnit"].ToString();

                    string month = "0", year = "0";
                    if (cboMonth.Text != "")
                        month = cboMonth.Text;

                    if (cboYear.Text != "")
                        year = cboYear.Text;

                    DataSet mySet = ObjDb.GetRows("select isnull(count(distinct nocabang),0) as jmlcabang from tSaldobln where noCabang in (" + nocabangUnit + ") and month(tgl)='" + month + "' and year(tgl)='" + year + "' ");
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    string jmlUnitdiPerwakilan = myRow["jmlcabang"].ToString();

                    DataSet mySet2 = ObjDb.GetRows("select isnull(count(noCabang),0) as jmlcabang from mCabang where parent=" + parent + " and stsCabang=2");
                    DataRow myRow2 = mySet2.Tables[0].Rows[0];
                    string jmlUnit = myRow2["jmlcabang"].ToString();

                    if (Convert.ToInt32(jmlUnitdiPerwakilan) != Convert.ToInt32(jmlUnit))
                        btnImport.Enabled = false;
                    else
                        btnImport.Enabled = true;

                }

            }
            
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

        protected void btnExport_Click(object sender, EventArgs e)
        {

            bool valid = true;
            string message = "", alert = "";

            if (cboTabel.Text == "")
            {
                message = ObjSys.CreateMessage("Tabel harus dipilih.");
                alert = "error";
                valid = false;
            }

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
                            cmd = new SqlCommand("SPProcessExportLive", con);
                            cmd.Parameters.Add(new SqlParameter("@cabang", ObjSys.GetCabangId));
                            cmd.Parameters.Add(new SqlParameter("@table", cboTabel.Text));
                            cmd.Parameters.Add(new SqlParameter("@bulan", cboMonth.Text));
                            cmd.Parameters.Add(new SqlParameter("@tahun", cboYear.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            int nokolom = 1;
                            // creating an array 
                            int[] a_array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
                            //foreach (DataColumn column in dt.Columns)
                            foreach (int column in a_array)
                            {
                                //Add the Header row for CSV file.
                                //csv += column.ColumnName + ',';
                                csv += "column"+nokolom+++ ',';
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
                                while (number < 30)
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
                            Response.AddHeader("content-disposition", "attachment;filename=" + cboTabel.Text + ".csv");
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

        protected void cboAksi_TextChanged(object sender, EventArgs e)
        {
            loadShowExportImport();
            CloseMessage();
        }

        protected void loadShowExportImport()
        {
            if (cboAksi.Text == "")
            {
                showImport.Visible = false;
                showExport.Visible = false;
                btnImport.Enabled = false;
                btnExport.Enabled = false;
            }
            else if (cboAksi.Text == "Import")
            {
                showImport.Visible = true;
                showExport.Visible = false;
                btnImport.Enabled = true;
                btnExport.Enabled = false;
            }
            else if (cboAksi.Text == "Export")
            {
                showImport.Visible = false;
                showExport.Visible = true;
                btnImport.Enabled = false;
                btnExport.Enabled = true;
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            bool valid = true;

            try
            {
                if (flUpload.HasFile == true)
                {
                    //Upload and save the file
                    string excelPath = Server.MapPath("~/Assets/UploadTable/") + Path.GetFileName(flUpload.PostedFile.FileName);
                    flUpload.SaveAs(excelPath);
                    string conString = string.Empty;
                    string extension = Path.GetExtension(flUpload.PostedFile.FileName);

                    //if (cboflUpload.Text == "csv/txt")
                    //{
                        string filepath = excelPath;
                        StreamReader sr = new StreamReader(filepath);
                        string line = sr.ReadLine();
                        string[] value = line.Split(',');
                        DataTable dt = new DataTable();
                        DataRow row;
                        foreach (string dc in value)
                        {
                            dt.Columns.Add(new DataColumn(dc));
                        }

                        while (!sr.EndOfStream)
                        {
                            value = sr.ReadLine().Split(',');
                            if (value.Length == dt.Columns.Count)
                            {
                                row = dt.NewRow();
                                row.ItemArray = value;
                                dt.Rows.Add(row);
                            }
                        }

                        foreach (DataRow row2 in dt.Rows)
                        {
                            string column1 = Convert.ToString(row2["column1"]);
                            string column2 = Convert.ToString(row2["column2"]);
                            string column3 = Convert.ToString(row2["column3"]);
                            string column4 = Convert.ToString(row2["column4"]);
                            string column5 = Convert.ToString(row2["column5"]);
                            string column6 = Convert.ToString(row2["column6"]);
                            string column7 = Convert.ToString(row2["column7"]);
                            string column8 = Convert.ToString(row2["column8"]);
                            string column9 = Convert.ToString(row2["column9"]);
                            string column10 = Convert.ToString(row2["column10"]);
                            string column11 = Convert.ToString(row2["column11"]);
                            string column12 = Convert.ToString(row2["column12"]);
                            string column13 = Convert.ToString(row2["column13"]);
                            string column14 = Convert.ToString(row2["column14"]);
                            string column15 = Convert.ToString(row2["column15"]);
                            string column16 = Convert.ToString(row2["column16"]);
                            string column17 = Convert.ToString(row2["column17"]);
                            string column18 = Convert.ToString(row2["column18"]);
                            string column19 = Convert.ToString(row2["column19"]);
                            string column20 = Convert.ToString(row2["column20"]);
                            string column21 = Convert.ToString(row2["column21"]);
                            string column22 = Convert.ToString(row2["column22"]);
                            string column23 = Convert.ToString(row2["column23"]);
                            string column24 = Convert.ToString(row2["column24"]);
                            string column25 = Convert.ToString(row2["column25"]);
                            string column26 = Convert.ToString(row2["column26"]);
                            string column27 = Convert.ToString(row2["column27"]);
                            string column28 = Convert.ToString(row2["column28"]);
                            string column29 = Convert.ToString(row2["column29"]);
                            string column30 = Convert.ToString(row2["column30"]);

                        using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                            {
                                using (SqlCommand cmd = new SqlCommand("SPProcessImport"))
                                {

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@table", cboTabel.Text);

                                    cmd.Parameters.AddWithValue("@column1", column1);
                                    cmd.Parameters.AddWithValue("@column2", column2);
                                    cmd.Parameters.AddWithValue("@column3", column3);
                                    cmd.Parameters.AddWithValue("@column4", column4);
                                    cmd.Parameters.AddWithValue("@column5", column5);
                                    cmd.Parameters.AddWithValue("@column6", column6);
                                    cmd.Parameters.AddWithValue("@column7", column7);
                                    cmd.Parameters.AddWithValue("@column8", column8);
                                    cmd.Parameters.AddWithValue("@column9", column9);
                                    cmd.Parameters.AddWithValue("@column10", column10);
                                    cmd.Parameters.AddWithValue("@column11", column11);
                                    cmd.Parameters.AddWithValue("@column12", column12);
                                    cmd.Parameters.AddWithValue("@column13", column13);
                                    cmd.Parameters.AddWithValue("@column14", column14);
                                    cmd.Parameters.AddWithValue("@column15", column15);
                                    cmd.Parameters.AddWithValue("@column16", column16);
                                    cmd.Parameters.AddWithValue("@column17", column17);
                                    cmd.Parameters.AddWithValue("@column18", column18);
                                    cmd.Parameters.AddWithValue("@column19", column19);
                                    cmd.Parameters.AddWithValue("@column20", column20);
                                    cmd.Parameters.AddWithValue("@column21", column21);
                                    cmd.Parameters.AddWithValue("@column22", column22);
                                    cmd.Parameters.AddWithValue("@column23", column23);
                                    cmd.Parameters.AddWithValue("@column24", column24);
                                    cmd.Parameters.AddWithValue("@column25", column25);
                                    cmd.Parameters.AddWithValue("@column26", column26);
                                    cmd.Parameters.AddWithValue("@column27", column27);
                                    cmd.Parameters.AddWithValue("@column28", column28);
                                    cmd.Parameters.AddWithValue("@column29", column29);
                                    cmd.Parameters.AddWithValue("@column30", column30);

                                    cmd.Connection = con;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }

                            }

                        }
                    //}

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diupload.");

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "File harus di pilih.");
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            cboAksi.Text = "";
            cboTabel.Text = "";
            loadData();
            loadShowExportImport();
            CloseMessage();

        }
    }
}