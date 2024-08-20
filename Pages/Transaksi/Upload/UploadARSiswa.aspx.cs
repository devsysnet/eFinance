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
    public partial class UploadARSiswa : System.Web.UI.Page
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
            cboKategori.DataSource = ObjDb.GetRows("select * from (select '0' as id, '---Pilih---' name union select noTransaksi as id, jenisTransaksi as name from mJenisTransaksi where nocabang='" + ObjSys.GetCabangId + "' and posbln<>'1')x");
            cboKategori.DataValueField = "id";
            cboKategori.DataTextField = "name";
            cboKategori.DataBind();

            cboTA.DataSource = ObjDb.GetRows("select * from (select '' as id, '---Semua---' name union select distinct tahunAjaran as id, tahunAjaran as name from TransKelas where nocabang='" + ObjSys.GetCabangId + "')x");
            cboTA.DataValueField = "id";
            cboTA.DataTextField = "name";
            cboTA.DataBind();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "";

            if (cboTA.Text == "")
            {
                message += ObjSys.CreateMessage("Tahun Ajaran harus dipilih.");
                valid = false;
            }

            if (dtBayar.Text == "")
            {
                message += ObjSys.CreateMessage("Tgl Bayar harus dipilih.");
                valid = false;
            }

            if (cboKategori.Text == "0")
            {
                message += ObjSys.CreateMessage("Jenis Pembayaran harus dipilih.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                    
                    if (flUpload.HasFile == true)
                    {
                        //Upload and save the file
                        string excelPath = Server.MapPath("~/Assets/UploadARFile/") + Path.GetFileName(flUpload.PostedFile.FileName);
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

                            decimal totalbayar = 0;
                            foreach (DataRow row2 in dt.Rows)
                            {
                                string nopiutang = Convert.ToString(row2["nopiutang"]);
                                string nosiswa = Convert.ToString(row2["nosiswa"]);
                                string nis = Convert.ToString(row2["nis"]);
                                string namasiswa = Convert.ToString(row2["namasiswa"]);
                                string kelas = Convert.ToString(row2["kelas"]);
                                string nilai_bayar = Convert.ToString(row2["nilai_bayar"]);

                                using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                                {
                                    using (SqlCommand cmd = new SqlCommand("UploadDataARSiswa"))
                                    {

                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@nopiutang", nopiutang);
                                        cmd.Parameters.AddWithValue("@nosiswa", nosiswa);
                                        cmd.Parameters.AddWithValue("@nis", nis);
                                        cmd.Parameters.AddWithValue("@nilaibayar", nilai_bayar);
                                        cmd.Parameters.AddWithValue("@tahunajaran", cboTA.Text);
                                        cmd.Parameters.AddWithValue("@noTransaksi", cboKategori.Text);
                                        cmd.Parameters.AddWithValue("@tglbayar", Convert.ToDateTime(dtBayar.Text).ToString("yyyy-MM-dd"));
                                        cmd.Parameters.AddWithValue("@nocabang", ObjSys.GetCabangId);
                                        cmd.Parameters.AddWithValue("@uploadby", ObjSys.GetUserId);
                                        cmd.Connection = con;
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }

                                }

                                totalbayar += Convert.ToDecimal(row2["nilai_bayar"]);
                            }

                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("tahunajaran", cboTA.Text);
                            ObjGlobal.Param.Add("noTransaksi", cboKategori.Text);
                            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                            ObjGlobal.Param.Add("nilaibayar", Convert.ToDecimal(totalbayar).ToString());
                            ObjGlobal.Param.Add("uploadby", ObjSys.GetUserId);
                            ObjGlobal.GetDataProcedure("UploadDataARSiswa_tKasDetil", ObjGlobal.Param);

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
            dt.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn("nopiutang", typeof(string)),
                new DataColumn("nosiswa", typeof(string)),
                new DataColumn("nis", typeof(string)),

                new DataColumn("namasiswa", typeof(string)),
                new DataColumn("kelas", typeof(string)),
                new DataColumn("nilai_bayar", typeof(string))
            });
            DataRow dr = null;
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("thnajaran", cboTA.Text);
            ObjGlobal.Param.Add("nojnstransaksi", cboKategori.Text);
            DataSet mySet = ObjGlobal.GetDataProcedure("spdownloaddtsiswa", ObjGlobal.Param);
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["noPiutang"] = myRow["nopiutang"].ToString();
                dr["nosiswa"] = myRow["nosiswa"].ToString();
                dr["nis"] = myRow["nis"].ToString();
                dr["namasiswa"] = myRow["namasiswa"].ToString();
                dr["kelas"] = myRow["kelas"].ToString();
                dr["nilai_bayar"] = myRow["nilaibayar"].ToString();
                dt.Rows.Add(dr);
            }
            //dt.Rows.Add("111111", "50000");
            //dt.Rows.Add("222222", "50000");
            //dt.Rows.Add("333333", "50000");

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
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "TemplateUploadPiutangSiswa.csv");
            context.Response.End();
        }
    }
}