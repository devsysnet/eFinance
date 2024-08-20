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
    public partial class ExportImportBudgetYayasan : System.Web.UI.Page
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
                ShowHide(false, false, false,false);
                thnUpdate.Visible = false;
                DataSet mySet = ObjDb.GetRows("select systembudget from parameter ");
                DataRow myRow = mySet.Tables[0].Rows[0];
                string systembudget = myRow["systembudget"].ToString();

                if (systembudget == "Tahunan")
                {
                    btnimporttahunan.Visible = true;
                    btnImport.Visible = false;

                }
                else
                {
                    btnimporttahunan.Visible = false;
                    btnImport.Visible = true;
                }
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

                    if (cboStatus.Text == "New")
                    {
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
                            //string Tahun = Convert.ToString(row2["Tahun"]);
                            string Kode_Akun = Convert.ToString(row2["Kode_Akun"]);
                            string Nama_Akun = Convert.ToString(row2["Nama_Akun"]);
                            string Qty = Convert.ToString(row2["Qty"]);
                            string Satuan = Convert.ToString(row2["Satuan"]);
                            string HargaSatuan = Convert.ToString(row2["HargaSatuan"]);
                            string Januari = Convert.ToString(row2["Januari"]);
                            string Februari = Convert.ToString(row2["Februari"]);
                            string Maret = Convert.ToString(row2["Maret"]);
                            string April = Convert.ToString(row2["April"]);
                            string Mei = Convert.ToString(row2["Mei"]);
                            string Juni = Convert.ToString(row2["Juni"]);
                            string Juli = Convert.ToString(row2["Juli"]);
                            string Agustus = Convert.ToString(row2["Agustus"]);
                            string September = Convert.ToString(row2["September"]);
                            string Oktober = Convert.ToString(row2["Oktober"]);
                            string November = Convert.ToString(row2["November"]);
                            string Desember = Convert.ToString(row2["Desember"]);


                            using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                            {

                                using (SqlCommand cmd = new SqlCommand("SPProcessImportBudgetyayasan"))
                                {

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@noCabang", ObjSys.GetCabangId);
                                    cmd.Parameters.AddWithValue("@createdBy", ObjSys.GetUserId);
                                    cmd.Parameters.AddWithValue("@tahun", cboYear.Text);
                                    cmd.Parameters.AddWithValue("@jenis", cbojenis.Text);
                                    cmd.Parameters.AddWithValue("@kdrek", Kode_Akun);
                                    cmd.Parameters.AddWithValue("@ket", Nama_Akun);
                                    cmd.Parameters.AddWithValue("@Qty", Qty);
                                    cmd.Parameters.AddWithValue("@Satuan", Satuan);
                                    cmd.Parameters.AddWithValue("@HargaSatuan", HargaSatuan);
                                    cmd.Parameters.AddWithValue("@budget1", Januari);
                                    cmd.Parameters.AddWithValue("@budget2", Februari);
                                    cmd.Parameters.AddWithValue("@budget3", Maret);
                                    cmd.Parameters.AddWithValue("@budget4", April);
                                    cmd.Parameters.AddWithValue("@budget5", Mei);
                                    cmd.Parameters.AddWithValue("@budget6", Juni);
                                    cmd.Parameters.AddWithValue("@budget7", Juli);
                                    cmd.Parameters.AddWithValue("@budget8", Agustus);
                                    cmd.Parameters.AddWithValue("@budget9", September);
                                    cmd.Parameters.AddWithValue("@budget10", Oktober);
                                    cmd.Parameters.AddWithValue("@budget11", November);
                                    cmd.Parameters.AddWithValue("@budget12", Desember);

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
                        loadDataBudget(cboStatus.Text, cboYear.Text);

                    }


                    if (cboStatus.Text == "Revisi")
                    {
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
                            //string Tahun = Convert.ToString(row2["Tahun"]);
                            string Kode_Akun = Convert.ToString(row2["Kode_Akun"]);
                            string Nama_Akun = Convert.ToString(row2["Nama_Akun"]);
                            string Qty = Convert.ToString(row2["Qty"]);
                            string Satuan = Convert.ToString(row2["Satuan"]);
                            string HargaSatuan = Convert.ToString(row2["HargaSatuan"]);
                            string Januari = Convert.ToString(row2["Januari"]);
                            string Februari = Convert.ToString(row2["Februari"]);
                            string Maret = Convert.ToString(row2["Maret"]);
                            string April = Convert.ToString(row2["April"]);
                            string Mei = Convert.ToString(row2["Mei"]);
                            string Juni = Convert.ToString(row2["Juni"]);
                            string Juli = Convert.ToString(row2["Juli"]);
                            string Agustus = Convert.ToString(row2["Agustus"]);
                            string September = Convert.ToString(row2["September"]);
                            string Oktober = Convert.ToString(row2["Oktober"]);
                            string November = Convert.ToString(row2["November"]);
                            string Desember = Convert.ToString(row2["Desember"]);

                            using (SqlConnection con2 = new SqlConnection(ObjDb.ConDb()))
                            {
                                using (SqlCommand cmd2 = new SqlCommand("SPProcessImportBudgetRevisiyayasan"))
                                {

                                    cmd2.CommandType = CommandType.StoredProcedure;
                                    cmd2.Parameters.AddWithValue("@noCabang", ObjSys.GetCabangId);
                                    cmd2.Parameters.AddWithValue("@createdBy", ObjSys.GetUserId);
                                    cmd2.Parameters.AddWithValue("@tahun", cboYear.Text);
                                    cmd2.Parameters.AddWithValue("@jenis", cbojenis.Text);
                                    cmd2.Parameters.AddWithValue("@kdrek", Kode_Akun);
                                    cmd2.Parameters.AddWithValue("@ket", Nama_Akun);
                                    cmd2.Parameters.AddWithValue("@Qty", Qty);
                                    cmd2.Parameters.AddWithValue("@Satuan", Satuan);
                                    cmd2.Parameters.AddWithValue("@Hargasatuan", HargaSatuan);
                                    cmd2.Parameters.AddWithValue("@budget1", Januari);
                                    cmd2.Parameters.AddWithValue("@budget2", Februari);
                                    cmd2.Parameters.AddWithValue("@budget3", Maret);
                                    cmd2.Parameters.AddWithValue("@budget4", April);
                                    cmd2.Parameters.AddWithValue("@budget5", Mei);
                                    cmd2.Parameters.AddWithValue("@budget6", Juni);
                                    cmd2.Parameters.AddWithValue("@budget7", Juli);
                                    cmd2.Parameters.AddWithValue("@budget8", Agustus);
                                    cmd2.Parameters.AddWithValue("@budget9", September);
                                    cmd2.Parameters.AddWithValue("@budget10", Oktober);
                                    cmd2.Parameters.AddWithValue("@budget11", November);
                                    cmd2.Parameters.AddWithValue("@budget12", Desember);

                                    cmd2.Connection = con2;
                                    con2.Open();
                                    cmd2.ExecuteNonQuery();
                                    con2.Close();
                                }
                            }
                        }
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil direvisi.");
                        clearData();
                        loadDataBudget(cboStatus.Text, cboYear.Text);
                    }
                    if (cboStatus.Text == "Update")
                    {
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
                            string noBudget = Convert.ToString(row2["noBudget"]);
                            string Kode_Akun = Convert.ToString(row2["Kode_Akun"]);
                            string Nama_Akun = Convert.ToString(row2["Nama_Akun"]);
                            string Qty = Convert.ToString(row2["Qty"]);
                            string Satuan = Convert.ToString(row2["Satuan"]);
                            string HargaSatuan = Convert.ToString(row2["HargaSatuan"]);
                            string Januari = Convert.ToString(row2["Januari"]);
                            string Februari = Convert.ToString(row2["Februari"]);
                            string Maret = Convert.ToString(row2["Maret"]);
                            string April = Convert.ToString(row2["April"]);
                            string Mei = Convert.ToString(row2["Mei"]);
                            string Juni = Convert.ToString(row2["Juni"]);
                            string Juli = Convert.ToString(row2["Juli"]);
                            string Agustus = Convert.ToString(row2["Agustus"]);
                            string September = Convert.ToString(row2["September"]);
                            string Oktober = Convert.ToString(row2["Oktober"]);
                            string November = Convert.ToString(row2["November"]);
                            string Desember = Convert.ToString(row2["Desember"]);

                            using (SqlConnection con2 = new SqlConnection(ObjDb.ConDb()))
                            {
                                using (SqlCommand cmd2 = new SqlCommand("SPProcessImportBudgetUpdate"))
                                {

                                    cmd2.CommandType = CommandType.StoredProcedure;
                                    cmd2.Parameters.AddWithValue("@noCabang", ObjSys.GetCabangId);
                                    cmd2.Parameters.AddWithValue("@createdBy", ObjSys.GetUserId);
                                    cmd2.Parameters.AddWithValue("@tahun", cboYear.Text);
                                    cmd2.Parameters.AddWithValue("@jenis", cbojenis.Text);
                                    cmd2.Parameters.AddWithValue("@noBudget", noBudget);
                                    cmd2.Parameters.AddWithValue("@kdrek", Kode_Akun);
                                    cmd2.Parameters.AddWithValue("@ket", Nama_Akun);
                                    cmd2.Parameters.AddWithValue("@Qty", Qty);
                                    cmd2.Parameters.AddWithValue("@Satuan", Satuan);
                                    cmd2.Parameters.AddWithValue("@Hargasatuan", HargaSatuan);
                                    cmd2.Parameters.AddWithValue("@budget1", Januari);
                                    cmd2.Parameters.AddWithValue("@budget2", Februari);
                                    cmd2.Parameters.AddWithValue("@budget3", Maret);
                                    cmd2.Parameters.AddWithValue("@budget4", April);
                                    cmd2.Parameters.AddWithValue("@budget5", Mei);
                                    cmd2.Parameters.AddWithValue("@budget6", Juni);
                                    cmd2.Parameters.AddWithValue("@budget7", Juli);
                                    cmd2.Parameters.AddWithValue("@budget8", Agustus);
                                    cmd2.Parameters.AddWithValue("@budget9", September);
                                    cmd2.Parameters.AddWithValue("@budget10", Oktober);
                                    cmd2.Parameters.AddWithValue("@budget11", November);
                                    cmd2.Parameters.AddWithValue("@budget12", Desember);

                                    cmd2.Connection = con2;
                                    con2.Open();
                                    cmd2.ExecuteNonQuery();
                                    con2.Close();
                                }
                            }
                        }
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil diupdate.");
                        clearData();
                        loadDataBudget(cboStatus.Text, cboYear.Text);
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
        protected void btnImporttahunan_Click(object sender, EventArgs e)
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

                    if (cboStatus.Text == "New")
                    {
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
                            //string Tahun = Convert.ToString(row2["Tahun"]);
                            string Kode_Akun = Convert.ToString(row2["Kode_Akun"]);
                            string Nama_Akun = Convert.ToString(row2["Nama_Akun"]);
                            string Nilai = Convert.ToString(row2["Nilai"]);


                            using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                            {

                                using (SqlCommand cmd = new SqlCommand("SPProcessImportBudgettahunan"))
                                {

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@noCabang", ObjSys.GetCabangId);
                                    cmd.Parameters.AddWithValue("@createdBy", ObjSys.GetUserId);
                                    cmd.Parameters.AddWithValue("@tahun", cboYear.Text);
                                    cmd.Parameters.AddWithValue("@kdrek", Kode_Akun);
                                    cmd.Parameters.AddWithValue("@ket", Nama_Akun);
                                    cmd.Parameters.AddWithValue("@nilai", Nilai);

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
                        loadDataBudget(cboStatus.Text, cboYear.Text);

                    }


                    if (cboStatus.Text == "Revisi")
                    {
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
                            //string Tahun = Convert.ToString(row2["Tahun"]);
                            string Kode_Akun = Convert.ToString(row2["Kode_Akun"]);
                            string Nama_Akun = Convert.ToString(row2["Nama_Akun"]);
                            string Nilai = Convert.ToString(row2["Nilai"]);


                            using (SqlConnection con2 = new SqlConnection(ObjDb.ConDb()))
                            {
                                using (SqlCommand cmd2 = new SqlCommand("SPProcessImportBudgetRevisitahunan"))
                                {

                                    cmd2.CommandType = CommandType.StoredProcedure;
                                    cmd2.Parameters.AddWithValue("@noCabang", ObjSys.GetCabangId);
                                    cmd2.Parameters.AddWithValue("@createdBy", ObjSys.GetUserId);
                                    cmd2.Parameters.AddWithValue("@tahun", cboYear.Text);
                                    cmd2.Parameters.AddWithValue("@kdrek", Kode_Akun);
                                    cmd2.Parameters.AddWithValue("@ket", Nama_Akun);
                                    cmd2.Parameters.AddWithValue("@nilai", Nilai);

                                    cmd2.Connection = con2;
                                    con2.Open();
                                    cmd2.ExecuteNonQuery();
                                    con2.Close();
                                }
                            }
                        }
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil direvisi.");
                        clearData();
                    }
                    if (cboStatus.Text == "Update")
                    {
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
                            string noBudget = Convert.ToString(row2["noBudget"]);
                            string Kode_Akun = Convert.ToString(row2["Kode_Akun"]);
                            string Nama_Akun = Convert.ToString(row2["Nama_Akun"]);
                            string Nilai = Convert.ToString(row2["Nilai"]);


                            using (SqlConnection con2 = new SqlConnection(ObjDb.ConDb()))
                            {
                                using (SqlCommand cmd2 = new SqlCommand("SPProcessImportBudgetUpdateTahunan"))
                                {

                                    cmd2.CommandType = CommandType.StoredProcedure;
                                    cmd2.Parameters.AddWithValue("@noCabang", ObjSys.GetCabangId);
                                    cmd2.Parameters.AddWithValue("@createdBy", ObjSys.GetUserId);
                                    cmd2.Parameters.AddWithValue("@tahun", cboYear.Text);
                                    cmd2.Parameters.AddWithValue("@noBudget", noBudget);
                                    cmd2.Parameters.AddWithValue("@kdrek", Kode_Akun);
                                    cmd2.Parameters.AddWithValue("@ket", Nama_Akun);
                                    cmd2.Parameters.AddWithValue("@nilai", Nilai);

                                    cmd2.Connection = con2;
                                    con2.Open();
                                    cmd2.ExecuteNonQuery();
                                    con2.Close();
                                }
                            }
                        }
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil diupdate.");
                        clearData();
                        loadDataBudget(cboStatus.Text, cboYear.Text);
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
            DataSet mySet = ObjDb.GetRows("select systembudget from parameter ");
            DataRow myRow = mySet.Tables[0].Rows[0];
            string systembudget = myRow["systembudget"].ToString();
            if (status == "New")
            {
                if (systembudget == "Tahunan")
                {
                    btnimporttahunan.Visible = true;
                    btnImport.Visible = false;
                    btnimporttahunan.Enabled = true;


                }
                else
                {
                    btnimporttahunan.Visible = false;
                    btnImport.Visible = true;
                    btnImport.Enabled = true;

                }
                btnExport.Enabled = false;

                if (ObjDb.GetRows("select * from tBudget_H where nocabang = '" + ObjSys.GetCabangId + "' and thn='" + cboYear.Text + "'").Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Budget sudah pernah diinput, silahkan pilih status revisi");
                    btnimporttahunan.Enabled = false;
                    btnImport.Enabled = false;
                }
            }
            else if (status == "Revisi")
            {

                if (systembudget == "Tahunan")
                {
                    btnimporttahunan.Visible = true;
                    btnImport.Visible = false;
                    btnimporttahunan.Enabled = true;
                }
                else
                {
                    btnimporttahunan.Visible = false;
                    btnImport.Visible = true;
                    btnImport.Enabled = true;
                }
                btnExport.Enabled = false;
                //if (ObjDb.GetRows("select * from tBudget_Jenjang where nocabang = '" + ObjSys.GetCabangId + "' and tahun='" + cboYear.Text + "' and apprke = '2'").Tables[0].Rows.Count == 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //    ShowMessage("error", "Harus Approve Ke Level 2");
                //    btnimporttahunan.Enabled = false;
                //    btnImport.Enabled = false;
                //}
            }
            else
            {


                if (systembudget == "Tahunan")
                {
                    btnimporttahunan.Visible = true;
                    btnImport.Visible = false;

                }
                else
                {
                    btnimporttahunan.Visible = false;
                    btnImport.Visible = true;
                }
                btnExport.Enabled = false;
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
                            cmd = new SqlCommand("SPProcessExportBudget", con);
                            cmd.Parameters.Add(new SqlParameter("@cabang", ObjSys.GetCabangId));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            //int nokolom = 1;
                            // creating an array 
                            int nox = 0;
                            DataSet mySet = ObjDb.GetRows("select systembudget from parameter ");
                            DataRow myRow = mySet.Tables[0].Rows[0];
                            string systembudget = myRow["systembudget"].ToString();
                            if (systembudget == "Tahunan")
                            {
                                int[] a_array = new int[] { 1, 2, 3 };
                                nox = 3;
                            }
                            else
                            {
                                int[] a_array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                                nox = 14;

                            }

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
                                while (number < nox)
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

            DataSet mySet = ObjDb.GetRows("select systembudget from parameter ");
            DataRow myRow = mySet.Tables[0].Rows[0];
            string systembudget = myRow["systembudget"].ToString();


            if (cboAksi.Text == "Import")
            {
                ShowHide(true, true, true,true);

                btnUpdate.Visible = false;

                btnExport.Visible = true;
                thnUpdate.Visible = false;

                if (systembudget == "Tahunan")
                {
                    btnimporttahunan.Visible = true;
                    btnImport.Visible = false;
                    btnimporttahunan.Enabled = true;

                }
                else
                {
                    btnimporttahunan.Visible = false;
                    btnImport.Visible = true;
                    btnImport.Enabled = true;

                }
            }
            else if (cboAksi.Text == "Export")
            {
                ShowHide(false, false, false,false);

                btnExport.Enabled = true;

                btnExport.Visible = true;
                thnUpdate.Visible = false;
                btnUpdate.Visible = false;
                if (systembudget == "Tahunan")
                {
                    btnimporttahunan.Visible = true;
                    btnImport.Visible = false;
                    btnimporttahunan.Enabled = false;

                }
                else
                {
                    btnimporttahunan.Visible = false;
                    btnImport.Visible = true;
                    btnImport.Enabled = false;

                }
            }
            else
            {
                ShowHide(false, false, false,false);

                btnimporttahunan.Visible = false;
                btnImport.Visible = false;
                btnExport.Visible = false;
                thnUpdate.Visible = true;
                btnUpdate.Visible = true;

            }
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
                            int nox = 0;
                            DataSet mySet = ObjDb.GetRows("select systembudget from parameter ");
                            DataRow myRow = mySet.Tables[0].Rows[0];
                            string systembudget = myRow["systembudget"].ToString();
                            if (systembudget == "Tahunan")
                            {
                                int[] a_array = new int[] { 1, 2, 3, 4 };
                                nox = 4;
                            }
                            else
                            {
                                int[] a_array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                                nox = 15;

                            }

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
                                while (number < nox)
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
        protected void ShowHide(bool Status, bool tahun, bool file,bool jenis)
        {
            showhidestatus.Visible = Status;
            showhidetahun.Visible = tahun;
            showhidefile.Visible = file;
            showhidejenis.Visible = jenis;
        }
    }
}