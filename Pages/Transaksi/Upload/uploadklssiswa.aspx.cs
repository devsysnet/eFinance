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
    public partial class uploadklssiswa : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
                //loadDataBudget(cboStatus.Text, cboYear.Text);
                ShowHide(false, false, false);
            }
        }

        protected void loadData()
        {
            cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadparameter");
            cboYear.DataValueField = "id";
            cboYear.DataTextField = "name";
            cboYear.DataBind();
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

        protected void btnImport_Click(object sender, EventArgs e)
        {
            bool valid = true;

            try
            {
                if (flUpload.HasFile == true)
                {
                    //Upload and save the file
                    string excelPath = Server.MapPath("~/Assets/Uploadkelas/") + Path.GetFileName(flUpload.PostedFile.FileName);
                    flUpload.SaveAs(excelPath);
                    string conString = string.Empty;
                    string extension = Path.GetExtension(flUpload.PostedFile.FileName);

                    //if (cboStatus.Text == "New")
                    //{
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
                            string notkelas = Convert.ToString(row2["notkelas"]);
                            string noSiswa = Convert.ToString(row2["noSiswa"]);
                            string namaSiswa = Convert.ToString(row2["namaSiswa"]);
                            string kelas = Convert.ToString(row2["kelas"]);
                            string tahunAjaran = Convert.ToString(row2["tahunAjaran"]);
                            string nilaibayar = Convert.ToString(row2["nilaibayar"]);
                            string sts = Convert.ToString(row2["sts"]);
                           


                            using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                            {

                                using (SqlCommand cmd = new SqlCommand("SPProcessImportkelas"))
                                {

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@noCabang", ObjSys.GetCabangId);
                                    cmd.Parameters.AddWithValue("@createdBy", ObjSys.GetUserId);
                                    cmd.Parameters.AddWithValue("@nosiswa", noSiswa);
                                    cmd.Parameters.AddWithValue("@notkelas", notkelas);
                                    cmd.Parameters.AddWithValue("@kelas", kelas);
                                    cmd.Parameters.AddWithValue("@nilaibayar", nilaibayar);
                                    cmd.Parameters.AddWithValue("@thnajaran", tahunAjaran);
                                    cmd.Parameters.AddWithValue("@sts", sts);
                                    
                                    cmd.Connection = con;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }


                        }


                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil diupload.");
                        clearData();
                        //loadDataBudget(cboStatus.Text, cboYear.Text);

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

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CloseMessage();
            //loadDataBudget(cboStatus.Text, cboYear.Text);
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CloseMessage();
            //loadDataBudget(cboStatus.Text, cboYear.Text);
        }

        //protected void loadDataBudget(string status = "", string tahun = "")
        //{
        //if (status == "New")
        //{
            //btnImport.Enabled = true;
            //btnExport.Enabled = false;
        //    if (ObjDb.GetRows("select * from tBudget_H where nocabang = '" + ObjSys.GetCabangId + "' and thn='" + cboYear.Text + "'").Tables[0].Rows.Count > 0)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //        ShowMessage("error", "Budget sudah pernah diinput, silahkan pilih status revisi");
        //        btnImport.Enabled = false;
        //    }
        //}
        //    {
        //        btnImport.Enabled = false;
        //        btnExport.Enabled = false;
        //    }

        //}

        protected void btnExport_Click(object sender, EventArgs e)
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
                            cmd = new SqlCommand("SPProcessExportTranskelas", con);
                            cmd.Parameters.Add(new SqlParameter("@cabang", ObjSys.GetCabangId));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            //int nokolom = 1;
                            // creating an array 
                            int[] a_array = new int[] { 1, 2, 3, 4, 5, 6, 7,8 };
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
                                while (number < 14)
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
                            Response.AddHeader("content-disposition", "attachment;filename=transkelas " + ObjSys.GetNow + ".csv");
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

        protected void clearData()
        {
            //cboStatus.Text = "";
        }

        protected void cboAksi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAksi.Text == "Import")
            {
                ShowHide(true, true, true);
                btnImport.Enabled = true;
                btnExport.Enabled = false;
            }
            else if (cboAksi.Text == "Export")
            {
                ShowHide(false, false, false);
                btnImport.Enabled = false;
                btnExport.Enabled = true;
            }
            else
            {
                btnImport.Enabled = false;
                btnExport.Enabled = false;
            }
        }

        protected void ShowHide(bool Status, bool tahun, bool file)
        {
            //showhidestatus.Visible = Status;
            showhidetahun.Visible = tahun;
            showhidefile.Visible = file;
        }
    }
}