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
    public partial class UploadVAUK : System.Web.UI.Page
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
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdUploadVA.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataVAUK", ObjGlobal.Param);
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
                    string excelPath = Server.MapPath("~/Assets/UploadVAUKFile/") + Path.GetFileName(flUpload.PostedFile.FileName);
                    flUpload.SaveAs(excelPath);
                    string conString = string.Empty;
                    string extension = Path.GetExtension(flUpload.PostedFile.FileName);

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
                            bc.DestinationTableName = "UploadVAUK";
                            bc.BatchSize = dt.Rows.Count;
                            con.Open();

                            string query = "DELETE UploadVAUK WHERE noCabang = " + ObjSys.GetCabangId + " ";
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
                                using (SqlCommand cmd = new SqlCommand("UpdateDataVAUK"))
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
                        //ObjGlobal.GetDataProcedure("insertlebihbayar", ObjGlobal.Param);

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