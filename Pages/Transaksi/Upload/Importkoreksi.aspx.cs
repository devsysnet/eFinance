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
using System.Data.SqlClient;
using System.Web.Configuration;

namespace eFinance.Pages.Transaksi.Upload
{
    public partial class Importkoreksi : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridview();

            }
        }

        private void BindGridview()
        {

            ObjGlobal.Param.Clear();
            grdUploadVA.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataKoreksi", ObjGlobal.Param);
            grdUploadVA.DataBind();

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";

            try
            {
                if (flUpload.HasFile == true)
                {
                    //Upload and save the file
                    string excelPath = Server.MapPath("~/Assets/Uploadkoreksi/") + Path.GetFileName(flUpload.PostedFile.FileName);
                    flUpload.SaveAs(excelPath);
                    string conString = string.Empty;
                    string extension = Path.GetExtension(flUpload.PostedFile.FileName);

                  
                    if (cboflUpload.Text == "csv/txt")
                    {

                     
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

                        using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                        {
                            SqlBulkCopy bc = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.TableLock);
                            bc.DestinationTableName = "Uploadkoreksi";
                            bc.BatchSize = dt.Rows.Count;
                            con.Open();

                            string query = "DELETE Uploadkoreksi";
                            SqlCommand cmd2 = new SqlCommand(query, con);
                            cmd2.ExecuteNonQuery();

                            bc.WriteToServer(dt);
                            bc.Close();
                            con.Close();
                        }
                        foreach (DataRow row2 in dt.Rows)
                        {
                            string kodetran = Convert.ToString(row2["kodetran"]);
                            string kdreklama = Convert.ToString(row2["kdreklama"]);
                            string kdrekbaru = Convert.ToString(row2["kdrekbaru"]);
                           


                            using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                            {
                                using (SqlCommand cmd = new SqlCommand("UpdateDataGL"))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@kodetran", kodetran);
                                    cmd.Parameters.AddWithValue("@kdreklama", kdreklama);
                                    cmd.Parameters.AddWithValue("@kdrekbaru", kdrekbaru);
                                    cmd.Connection = con;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }

                            }

                        }

                
                    }


                    BindGridview();

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diUpdate");

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

        private void ConnectionMySQL()
        {
            
            string connectionString = WebConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;

        }

    }
}