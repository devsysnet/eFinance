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
    public partial class uploadjurnalmemo : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
            }
        }

        private void loadDataCombo()
        {
            cboKategori.DataSource = ObjDb.GetRows("select * from (select '0' as id, '---Pilih---' name union select noIndex as id, JnsTransmemo as name from mJenisTransaksimemo)x");
            cboKategori.DataValueField = "id";
            cboKategori.DataTextField = "name";
            cboKategori.DataBind();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "";

           if (dtBayar.Text == "")
            {
                message += ObjSys.CreateMessage("Tgl Transaksi harus dipilih.");
                valid = false;
            }

            if (cboKategori.Text == "0")
            {
                message += ObjSys.CreateMessage("Jenis Transaksi harus dipilih.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {

                    if (flUpload.HasFile == true)
                    {
                        //Upload and save the file
                        string excelPath = Server.MapPath("~/Assets/UploadGlFile/") + Path.GetFileName(flUpload.PostedFile.FileName);
                        flUpload.SaveAs(excelPath);
                        string conString = string.Empty;
                        string extension = Path.GetExtension(flUpload.PostedFile.FileName);

                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("nocabangupload", ObjSys.GetCabangId);
                        ObjDb.Where.Add("nojnstransaksi", cboKategori.Text);
                        ObjDb.Delete("uploadjurnalmemo", ObjDb.Where);

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

                            //decimal totalbayar = 0;
                            foreach (DataRow row2 in dt.Rows)
                            {
                                string norek = Convert.ToString(row2["norek"]);
                                string kdrek = Convert.ToString(row2["kdrek"]);
                                string ket = Convert.ToString(row2["ket"]);
                                string jenis = Convert.ToString(row2["jenis"]);
                                string nilai = Convert.ToString(row2["nilai"]);
                                string nocabang = Convert.ToString(row2["nocabang"]);
                                string namacabang = Convert.ToString(row2["namacabang"]);

                                using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                                {
                                    using (SqlCommand cmd = new SqlCommand("uploadjurnalmemotable"))
                                    {

                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@nojnstransaksi", cboKategori.Text);
                                        cmd.Parameters.AddWithValue("@norek", norek);
                                        cmd.Parameters.AddWithValue("@kdrek", kdrek);
                                        cmd.Parameters.AddWithValue("@ket", ket);
                                        cmd.Parameters.AddWithValue("@jenis", jenis);
                                        cmd.Parameters.AddWithValue("@nilai", nilai);
                                        cmd.Parameters.AddWithValue("@tgl", Convert.ToDateTime(dtBayar.Text).ToString("yyyy-MM-dd"));
                                        cmd.Parameters.AddWithValue("@nocabang", nocabang);
                                        cmd.Parameters.AddWithValue("@namacabang", namacabang);
                                        cmd.Parameters.AddWithValue("@nocabangupload", ObjSys.GetCabangId);
                                         cmd.Connection = con;
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }

                                }

                            }

                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("nojnstransaksi", cboKategori.Text);
                            ObjGlobal.Param.Add("nocabangupload", ObjSys.GetCabangId);
                            ObjGlobal.Param.Add("tgl", Convert.ToDateTime(dtBayar.Text).ToString("yyyy-MM-dd"));
                            ObjGlobal.Param.Add("uploadby", ObjSys.GetUserId);
                            ObjGlobal.GetDataProcedure("SPUploadjurnalmemo", ObjGlobal.Param);

                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            ShowMessage("success", "Data berhasil diupload.");

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            ShowMessage("error", "File Extention harus .csv / .txt.");
                        }

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
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
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
                new DataColumn("norek", typeof(string)),
                new DataColumn("kdrek", typeof(string)),
                new DataColumn("ket", typeof(string)),
                new DataColumn("jenis", typeof(string)),
                new DataColumn("nilai", typeof(string)),
                new DataColumn("nocabang", typeof(string)),
                new DataColumn("namacabang", typeof(string))
            });
            DataRow dr = null;
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", ObjSys.GetParentCabang);
            ObjGlobal.Param.Add("nojnstransaksi", cboKategori.Text);
            DataSet mySet = ObjGlobal.GetDataProcedure("spdownformatjurnalmemo", ObjGlobal.Param);
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["norek"] = myRow["norek"].ToString();
                dr["kdrek"] = myRow["kdrek"].ToString();
                dr["ket"] = myRow["ket"].ToString();
                dr["jenis"] = myRow["jenis"].ToString();
                dr["nilai"] = myRow["nilai"].ToString();
                dr["nocabang"] = myRow["nocabang"].ToString();
                dr["namacabang"] = myRow["namacabang"].ToString();
                dt.Rows.Add(dr);
            }
         
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
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "TemplateUploadJurnalmemo.csv");
            context.Response.End();
        }
    }
}