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

namespace eFinance.Pages.Transaksi.Input
{
    public partial class UploadAbsensiNew : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindGridview();

            }
        }

        private void BindGridview()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdUploadAbs.DataSource = ObjGlobal.GetDataProcedure("SPLoadAbsKrywn", ObjGlobal.Param);
            grdUploadAbs.DataBind();

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            bool valid = true;

            try
            {
                if (flUpload.HasFile == true)
                {
                    //Upload and save the file
                    string excelPath = Server.MapPath("~/Assets/UploadAbsenFile/") + Path.GetFileName(flUpload.PostedFile.FileName);
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

                        foreach (DataRow row2 in dt.Rows)
                        {
                            string noKaryawan = Convert.ToString(row2["noKaryawan"]);
                            string tgl = Convert.ToString(row2["tgl"]);
                            string jammasuk = Convert.ToString(row2["jammasuk"]);
                            string jamkeluar = Convert.ToString(row2["jamkeluar"]);
                            string terlambat = Convert.ToString(row2["terlambat"]);
                            string nocabang = Convert.ToString(row2["nocabang"]);


                            using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                            {
                                using (SqlCommand cmd = new SqlCommand("UpdateDataAbsensiNew"))
                                {

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@tgl", tgl);
                                    cmd.Parameters.AddWithValue("@noKaryawan", noKaryawan);
                                    cmd.Parameters.AddWithValue("@jammasuk", jammasuk);
                                    cmd.Parameters.AddWithValue("@jamkeluar", jamkeluar);
                                    cmd.Parameters.AddWithValue("@terlambat", terlambat);
                                    cmd.Parameters.AddWithValue("@nocabang", nocabang);
                                    cmd.Parameters.AddWithValue("@uploadby", ObjSys.GetUserId);
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

        protected void grdUploadAbs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdUploadAbs.PageIndex = e.NewPageIndex;
            BindGridview();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            MyCSV();
        }

        public void MyCSV()
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn("noKaryawan", typeof(string)),
                new DataColumn("namaKaryawan", typeof(string)),
                new DataColumn("tgl", typeof(string)),
                new DataColumn("jammasuk",typeof(string)),
                new DataColumn("jamkeluar",typeof(string)),
                new DataColumn("terlambat", typeof(string)),
                new DataColumn("nocabang", typeof(string))

            });
            DataRow dr = null;
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataAbsKrywnNew", ObjGlobal.Param);
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["noKaryawan"] = myRow["noKaryawan"].ToString();
                dr["namaKaryawan"] = myRow["namaKaryawan"].ToString();
                dr["tgl"] = myRow["tgl"].ToString();
                dr["jammasuk"] = myRow["jammasuk"].ToString();
                dr["jamkeluar"] = myRow["jamkeluar"].ToString();
                dr["terlambat"] = myRow["terlambat"].ToString();
                dr["nocabang"] = myRow["nocabang"].ToString();

                dt.Rows.Add(dr);
            }
            //dt.Rows.Add("08/17/2020", "3328081212700001", "07:00:30", "13:05:30");
            //dt.Rows.Add("08/17/2020", "3328081212700002", "07:10:30", "13:05:15");
            //dt.Rows.Add("08/17/2020", "3328081212700003", "07:15:15", "14:00:15");

            string csv = string.Empty;

            foreach (DataColumn column in dt.Columns)
            {
                csv += column.ColumnName + ",";
            }
            csv = csv.TrimEnd(',');
            csv += "\r\n";
            csv = csv.TrimEnd(',');
            foreach (DataRow row in dt.Rows)
            {
                string comma = "";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    csv += row[i].ToString().Replace(",", ";") + ',';
                }
                csv = csv.TrimEnd(',');
                csv = csv + "" + comma;
                csv += "\r\n";
            }

            context.Response.Write(csv);
            context.Response.ContentType = "text/csv";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "TemplateUploadAbsensi.csv");
            context.Response.End();
        }

    }
}