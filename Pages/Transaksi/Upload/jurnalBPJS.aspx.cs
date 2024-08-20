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
    public partial class jurnalBPJS : System.Web.UI.Page
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
            grdUploadVA.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBPJS", ObjGlobal.Param);
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
                    string excelPath = Server.MapPath("~/Assets/UploadBPJSFile/") + Path.GetFileName(flUpload.PostedFile.FileName);
                    flUpload.SaveAs(excelPath);
                    string conString = string.Empty;
                    string extension = Path.GetExtension(flUpload.PostedFile.FileName);

                 
                    if (cboflUpload.Text == "csv/txt")
                    {

                        string RefNo = ObjSys.GetCodeAutoNumberMaster("3", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));

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
                            bc.DestinationTableName = "UploadBPJS";
                            bc.BatchSize = dt.Rows.Count;
                            con.Open();

                            string query = "DELETE UploadBPJS";
                            SqlCommand cmd2 = new SqlCommand(query, con);
                            cmd2.ExecuteNonQuery();

                            bc.WriteToServer(dt);
                            bc.Close();
                            con.Close();
                        }
                        foreach (DataRow row2 in dt.Rows)
                        {
                            string nocabang = Convert.ToString(row2["nocabang"]);
                            string tgl = Convert.ToString(row2["tgl"]);
                            string JHT37 = Convert.ToString(row2["JHT37"]);
                            string JKMJKK = Convert.ToString(row2["JKMJKK"]);
                            string JHT2 = Convert.ToString(row2["JHT2"]);
                            string JP = Convert.ToString(row2["JP"]);
                            string TOTALIURAN = Convert.ToString(row2["TOTALIURAN"]);
                            string BPJS5 = Convert.ToString(row2["BPJS5"]);
                            string BPJS4 = Convert.ToString(row2["BPJS4"]);


                            using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                            {
                                using (SqlCommand cmd = new SqlCommand("SPjurnalBPJSunit"))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@nocabang", nocabang);
                                    cmd.Parameters.AddWithValue("@tgl", tgl);
                                    cmd.Parameters.AddWithValue("@JHT37", JHT37);
                                    cmd.Parameters.AddWithValue("@JKMJKK", JKMJKK);
                                    cmd.Parameters.AddWithValue("@JHT2", JHT2);
                                    cmd.Parameters.AddWithValue("@JP", JP);
                                    cmd.Parameters.AddWithValue("@TOTALIURAN", TOTALIURAN);
                                    cmd.Parameters.AddWithValue("@BPJS5", BPJS5);
                                    cmd.Parameters.AddWithValue("@BPJS4", BPJS4);
                                    cmd.Parameters.AddWithValue("@nocabangpst", ObjSys.GetCabangId);
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
            //string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=my-home-school;";
            string connectionString = WebConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;

         
        }

    }
}