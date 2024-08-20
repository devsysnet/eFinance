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
    public partial class UploadRekening : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
                loadDataBudget(cboStatus.Text, cboYear.Text);
                ShowHide(false, false, false);
                thnUpdate.Visible = false;
            }
        }

        protected void loadData()
        {
            cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPpilihanthn1");
            cboYear.DataValueField = "id";
            cboYear.DataTextField = "name";
            cboYear.DataBind();

            cbothnupdate.DataSource = ObjGlobal.GetDataProcedureDataTable("SPpilihanthn1");
            cbothnupdate.DataValueField = "id";
            cbothnupdate.DataTextField = "name";
            cbothnupdate.DataBind();
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
                    string excelPath = Server.MapPath("~/Assets/UploadBudget/") + Path.GetFileName(flUpload.PostedFile.FileName);
                    flUpload.SaveAs(excelPath);
                    string conString = string.Empty;
                    string extension = Path.GetExtension(flUpload.PostedFile.FileName);

                
                        string filepath = excelPath;
                        StreamReader sr = new StreamReader(filepath);
                        string line = sr.ReadLine();
                        string[] value = line.Split(',');
                        DataTable dt = new DataTable();
                        DataRow row;
 
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("nocabang", ObjSys.GetCabangId);
                    ObjDb.Where.Add("tahun", cboYear.Text);
                    ObjDb.Delete("tRekening", ObjDb.Where);

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
                            //string Tahun = Convert.ToString(row2["Tahun"]);
                            string TGL_TRAN = Convert.ToString(row2["TGL_TRAN"]);
                            string TGL_EFEKTIF = Convert.ToString(row2["TGL_EFEKTIF"]);
                            string JAM_TRAN = Convert.ToString(row2["JAM_TRAN"]);
                            string SEQ = Convert.ToString(row2["SEQ"]);
                            string SALDO_AWAL_MUTASI = Convert.ToString(row2["SALDO_AWAL_MUTASI"]);
                            string MUTASI_DEBET = Convert.ToString(row2["MUTASI_DEBET"]);
                            string MUTASI_KREDIT = Convert.ToString(row2["MUTASI_KREDIT"]);
                            string SALDO_AKHIR_MUTASI = Convert.ToString(row2["SALDO_AKHIR_MUTASI"]);
                            string GLSIGN = Convert.ToString(row2["GLSIGN"]);
                            string KODE_TRAN = Convert.ToString(row2["KODE_TRAN"]);
                            string KODE_TRAN_TELLER = Convert.ToString(row2["KODE_TRAN_TELLER"]);
                            string TRREMK = Convert.ToString(row2["TRREMK"]);
                       

                            using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                            {

                                using (SqlCommand cmd = new SqlCommand("SPProcessImportRekening"))
                                {

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@noCabang", ObjSys.GetCabangId);
                                    cmd.Parameters.AddWithValue("@createdBy", ObjSys.GetUserId);
                                    cmd.Parameters.AddWithValue("@tahun", cboYear.Text);
                                    cmd.Parameters.AddWithValue("@TGL_TRAN", TGL_TRAN);
                                    cmd.Parameters.AddWithValue("@TGL_EFEKTIF", TGL_EFEKTIF);
                                    cmd.Parameters.AddWithValue("@JAM_TRAN", JAM_TRAN);
                                    cmd.Parameters.AddWithValue("@SEQ", SEQ);
                                    cmd.Parameters.AddWithValue("@SALDO_AWAL_MUTASI", SALDO_AWAL_MUTASI);
                                    cmd.Parameters.AddWithValue("@MUTASI_DEBET", MUTASI_DEBET);
                                    cmd.Parameters.AddWithValue("@MUTASI_KREDIT", MUTASI_KREDIT);
                                    cmd.Parameters.AddWithValue("@SALDO_AKHIR_MUTASI", SALDO_AKHIR_MUTASI);
                                    cmd.Parameters.AddWithValue("@GLSIGN", GLSIGN);
                                    cmd.Parameters.AddWithValue("@KODE_TRAN", KODE_TRAN);
                                    cmd.Parameters.AddWithValue("@KODE_TRAN_TELLER", KODE_TRAN_TELLER);
                                    cmd.Parameters.AddWithValue("@TRREMK", TRREMK);
 

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
            CloseMessage();
            loadDataBudget(cboStatus.Text, cboYear.Text);
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataBudget(cboStatus.Text, cboYear.Text);
        }

        protected void loadDataBudget(string status = "", string tahun = "")
        {
            if (status == "New")
            {
             

                if (ObjDb.GetRows("select * from tBudget_H where nocabang = '" + ObjSys.GetCabangId + "' and thn='" + cboYear.Text + "'").Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Budget sudah pernah diinput, silahkan pilih status revisi");
                }
            }
            else if (status == "Revisi")
            {

          
                if (ObjDb.GetRows("select * from tBudget_Jenjang where nocabang = '" + ObjSys.GetCabangId + "' and tahun='" + cboYear.Text + "' and apprke = '2'").Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Harus Approve Ke Level 2");
                }
            }
           

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
                            cmd = new SqlCommand("SPProcessExportRekening", con);
                            cmd.Parameters.Add(new SqlParameter("@cabang", ObjSys.GetCabangId));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            //int nokolom = 1;
                            // creating an array 
                            int[] a_array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
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
                            Response.AddHeader("content-disposition", "attachment;filename=Budget " + ObjSys.GetNow + ".csv");
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
            cboStatus.Text = "";
        }

        protected void cboAksi_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
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
                            cmd = new SqlCommand("SPProcessExportUpdateBudget", con);
                            cmd.Parameters.Add(new SqlParameter("@cabang", ObjSys.GetCabangId));
                            cmd.Parameters.Add(new SqlParameter("@tahun", cbothnupdate.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            //int nokolom = 1;
                            // creating an array 
                            int[] a_array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
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
                            Response.AddHeader("content-disposition", "attachment;filename=Budget_Update " + ObjSys.GetNow + ".csv");
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
        protected void ShowHide(bool Status, bool tahun, bool file)
        {
           
        }
    }
}