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
//Connection for MySQL
//using MySql.Data.MySqlClient;

namespace eFinance.Pages.Master.Input
{
    public partial class UploadVA : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("UploadVA.aspx");
                BindGridview();

            }
        }

        private void BindGridview()
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdUploadVA.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataVA", ObjGlobal.Param);
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
                    string excelPath = Server.MapPath("~/Assets/UploadVAFile/") + Path.GetFileName(flUpload.PostedFile.FileName);
                    flUpload.SaveAs(excelPath);
                    string conString = string.Empty;
                    string extension = Path.GetExtension(flUpload.PostedFile.FileName);

                    //if (cboflUpload.Text == "xls/xlsx")
                    //{
                    //    switch (extension)
                    //    {
                    //        case ".xls": //Excel 97-03
                    //            conString = WebConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    //            break;
                    //        case ".xlsx": //Excel 07 or higher
                    //            conString = WebConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                    //            break;
                    //            //default:
                    //            //   ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    //            //   ShowMessage("error", "Format file upload salah.");
                    //            //   valid = false;
                    //            //   return;
                    //    }

                    //    conString = string.Format(conString, excelPath);
                    //    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    //    {
                    //        excel_con.Open();
                    //        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    //        DataTable dtExcelData = new DataTable();

                    //        //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                    //        dtExcelData.Columns.AddRange(new DataColumn[3] {
                    //        new DataColumn("postingDate", typeof(string)),
                    //        new DataColumn("VAno", typeof(string)),
                    //        new DataColumn("VAAmount", typeof(decimal))
                    //        });

                    //        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                    //        {
                    //            oda.Fill(dtExcelData);
                    //        }
                    //        excel_con.Close();

                    //        using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                    //        {

                    //            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    //            {
                    //                //Set the database table name
                    //                sqlBulkCopy.DestinationTableName = "UploadVA";

                    //                //[OPTIONAL]: Map the Excel columns with that of the database table
                    //                sqlBulkCopy.ColumnMappings.Add("postingDate", "postingDate");
                    //                sqlBulkCopy.ColumnMappings.Add("VAno", "VAno");
                    //                sqlBulkCopy.ColumnMappings.Add("VAAmount", "VAAmount");
                    //                con.Open();

                    //                string query = "TRUNCATE TABLE UploadVA";
                    //                SqlCommand cmd2 = new SqlCommand(query, con);
                    //                cmd2.ExecuteNonQuery();

                    //                sqlBulkCopy.WriteToServer(dtExcelData);
                    //                con.Close();
                    //            }

                    //        }
                    //        foreach (DataRow row in dtExcelData.Rows)
                    //        {
                    //            string postingDate = Convert.ToString(row["postingDate"]);
                    //            string VAno = Convert.ToString(row["VAno"]);
                    //            string VAAmount = Convert.ToString(row["VAAmount"]).ToString();


                    //            using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                    //            {
                    //                using (SqlCommand cmd = new SqlCommand("UpdateDataVA"))
                    //                {

                    //                    cmd.CommandType = CommandType.StoredProcedure;
                    //                    cmd.Parameters.AddWithValue("@postingDate", postingDate);
                    //                    cmd.Parameters.AddWithValue("@VAno", VAno);
                    //                    cmd.Parameters.AddWithValue("@VAAmount", VAAmount);
                    //                    cmd.Parameters.AddWithValue("@noCabang", ObjSys.GetCabangId);
                    //                    cmd.Connection = con;
                    //                    con.Open();
                    //                    cmd.ExecuteNonQuery();
                    //                    con.Close();
                    //                }

                    //            }

                    //            //for database MySQL
                    //            // START
                    //            //#region MySqlConnection Connection   

                    //            //string connectionString = WebConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
                    //            //using (MySqlConnection conn = new MySqlConnection(connectionString))
                    //            //{

                    //            //    using (MySqlCommand cmdMysql = new MySqlCommand("UpdateDataPayment"))
                    //            //    {
                    //            //        cmdMysql.CommandType = CommandType.StoredProcedure;
                    //            //        cmdMysql.Parameters.AddWithValue("@VAno", VAno);
                    //            //        cmdMysql.Parameters.AddWithValue("@VAAmount", VAAmount);
                    //            //        cmdMysql.Connection = conn;
                    //            //        conn.Open();
                    //            //        cmdMysql.ExecuteNonQuery();
                    //            //        conn.Close();
                    //            //    }
                    //            //}

                    //            //FINISH

                    //        }

                    //    }
                    //}
                    //else 
                    if (cboflUpload.Text == "csv/txt")
                    {

                        string RefNo = ObjSys.GetCodeAutoNumberMaster("26", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));

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
                            bc.DestinationTableName = "UploadVA";
                            bc.BatchSize = dt.Rows.Count;
                            con.Open();

                            string query = "DELETE UploadVA WHERE noCabang = " + ObjSys.GetCabangId + " ";
                            SqlCommand cmd2 = new SqlCommand(query, con);
                            cmd2.ExecuteNonQuery();

                            bc.WriteToServer(dt);
                            bc.Close();
                            con.Close();
                        }
                        foreach (DataRow row2 in dt.Rows)
                        {
                            string postingdate = Convert.ToString(row2["postingdate"]);
                            string kodebank = Convert.ToString(row2["kodebank"]);
                            string vano = Convert.ToString(row2["vano"]);
                            string vaamount = Convert.ToString(row2["vaamount"]);
                          

                            using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                            {
                                using (SqlCommand cmd = new SqlCommand("UpdateDataVA"))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@postingdate", postingdate);
                                    cmd.Parameters.AddWithValue("@kodebank", kodebank);
                                    cmd.Parameters.AddWithValue("@vano", vano);
                                    cmd.Parameters.AddWithValue("@vaamount", vaamount);
                                    cmd.Parameters.AddWithValue("@refno", RefNo);
                                    cmd.Parameters.AddWithValue("@nocabang", ObjSys.GetCabangId);
                                    cmd.Connection = con;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }

                            }

                        }

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("refno", RefNo);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.GetDataProcedure("UpdateDatakasVA", ObjGlobal.Param);
                      

                        ObjSys.UpdateAutoNumberMaster("26", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));

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

            //using (MySqlConnection con = new MySqlConnection(connectionString))
            //{
            //    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM "))
            //    {
            //        using (MySqlDataAdapter sda = new MySqlDataAdapter())
            //        {
            //            cmd.Connection = con;
            //            sda.SelectCommand = cmd;
            //            using (DataTable dt = new DataTable())
            //            {
            //                sda.Fill(dt);
            //                //GridView1.DataSource = dt;
            //                //GridView1.DataBind();
            //            }
            //        }
            //    }
            //}

        }

    }
}