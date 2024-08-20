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
    public partial class uploaddatasiswa : System.Web.UI.Page
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
            //cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadparameter");
            //cboYear.DataValueField = "id";
            //cboYear.DataTextField = "name";
            //cboYear.DataBind();
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
                    string excelPath = Server.MapPath("~/Assets/UploadSiswa/") + Path.GetFileName(flUpload.PostedFile.FileName);
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

                        string noSiswa = Convert.ToString(row2["noSiswa"]);
                        string namaSiswa = Convert.ToString(row2["namaSiswa"]);
                        string nik = Convert.ToString(row2["nik"]);
                        string nis = Convert.ToString(row2["nis"]);
                        string nisn = Convert.ToString(row2["nisn"]);
                        string gender = Convert.ToString(row2["gender"]);
                        string agama = Convert.ToString(row2["agama"]);
                        string Alamat = Convert.ToString(row2["Alamat"]);
                        string kota = Convert.ToString(row2["kota"]);
                        string tgllahir = Convert.ToString(row2["tgllahir"]);
                        string kotalahir = Convert.ToString(row2["kotalahir"]);
                        string namaOrangtua = Convert.ToString(row2["namaOrangtua"]);
                        string telp = Convert.ToString(row2["telp"]);
                        string novirtual = Convert.ToString(row2["novirtual"]);
                        string noBank = Convert.ToString(row2["noBank"]);
                        string keterangan = Convert.ToString(row2["keterangan"]);
                        string keterangan1 = Convert.ToString(row2["keterangan1"]);
                        string keterangan2 = Convert.ToString(row2["keterangan2"]);
                        string nocabang = Convert.ToString(row2["nocabang"]);
                        string tglmasuk = Convert.ToString(row2["tglmasuk"]);
                        string sts = Convert.ToString(row2["sts"]);
                        string emailparent = Convert.ToString(row2["emailparent"]);



                        using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                        {

                            using (SqlCommand cmd = new SqlCommand("SPProcessImportSiswa"))
                            {
                                
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@createdBy", ObjSys.GetUserId);
                                cmd.Parameters.AddWithValue("@namaSiswa", namaSiswa);
                                cmd.Parameters.AddWithValue("@noSiswa", noSiswa);
                                cmd.Parameters.AddWithValue("@nik", nik);
                                cmd.Parameters.AddWithValue("@nis", nis);
                                cmd.Parameters.AddWithValue("@nisn", nisn);
                                cmd.Parameters.AddWithValue("@gender", gender);
                                cmd.Parameters.AddWithValue("@agama", agama);
                                cmd.Parameters.AddWithValue("@Alamat", Alamat);
                                cmd.Parameters.AddWithValue("@kota", kota);
                                cmd.Parameters.AddWithValue("@tgllahir", tgllahir);
                                cmd.Parameters.AddWithValue("@kotalahir", kotalahir);
                                cmd.Parameters.AddWithValue("@namaOrangtua", namaOrangtua);
                                cmd.Parameters.AddWithValue("@telp", telp);
                                cmd.Parameters.AddWithValue("@novirtual", novirtual);
                                cmd.Parameters.AddWithValue("@noBank", noBank);
                                cmd.Parameters.AddWithValue("@keterangan", keterangan);
                                cmd.Parameters.AddWithValue("@keterangan1", keterangan1);
                                cmd.Parameters.AddWithValue("@keterangan2", keterangan2);
                                cmd.Parameters.AddWithValue("@nocabang", nocabang);
                                cmd.Parameters.AddWithValue("@tglmasuk", tglmasuk);
                                cmd.Parameters.AddWithValue("@sts", sts);
                                cmd.Parameters.AddWithValue("@emailparent", emailparent);

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
                            cmd = new SqlCommand("SPProcessExportdatasiswa", con);
                            cmd.Parameters.Add(new SqlParameter("@cabang", ObjSys.GetCabangId));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            //int nokolom = 1;
                            // creating an array 
                            int[] a_array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21,22 };
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
                            Response.AddHeader("content-disposition", "attachment;filename=msiswa " + ObjSys.GetNow + ".csv");
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
            //showhidetahun.Visible = tahun;
            showhidefile.Visible = file;
        }
    }
}