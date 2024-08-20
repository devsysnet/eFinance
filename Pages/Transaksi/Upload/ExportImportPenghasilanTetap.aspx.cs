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
    public partial class ExportImportPenghasilanTetap : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                //ObjSys.SessionCheck("ExportImportPenghasilanTetap.aspx");
                LoadDataCombo();
            }
        }



        protected void loadData(string cabang = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            grdGajiKar.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataGajiKaryawan", ObjGlobal.Param);
            grdGajiKar.DataBind();

            grdSaldoGL.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataGajiKaryawanDetil");
            grdSaldoGL.DataBind();
            if (grdSaldoGL.Rows.Count > 0)
            {
                btnSimpan.Visible = true;
                btnReset.Visible = true;
            }
            else
            {
                btnSimpan.Visible = false;
                btnReset.Visible = false;
            }
        }

        protected void cmdMode_Click(object sender, EventArgs e)
        {

        }
        protected void LoadDataCombo()
        {
            DataSet mySet = ObjDb.GetRows("select * from mcabang where stspusat = 1");

            DataRow myRow = mySet.Tables[0].Rows[0];
            string parent = myRow["nocabang"].ToString();
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("yayasan", parent);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", ObjSys.GetParentCabang);
            cboCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataWilayah", ObjGlobal.Param);
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();
            LoadDataCombo3(cboCabang.Text);

        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
        }

        protected void grdSaldoGL_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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

        protected void btnCari_Click(object sender, EventArgs e)
        {
            loadData(cboUnit.Text);
        }
        private void clearData()
        {

        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            //    try
            //    {
            //        string message = "";
            //        bool valid = true;

            //        if (valid == true)
            //        {

            //            ObjDb.Data.Clear();
            //            ObjDb.Data.Add("noKaryawan", hdnnoKaryawan.Value);
            //            ObjDb.Data.Add("sts", "1");
            //            ObjDb.Data.Add("nocabang", hdnnocabang.Value);
            //            ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
            //            ObjDb.Data.Add("createdDate", ObjSys.GetNow);
            //            ObjDb.Insert("MstGaji_H", ObjDb.Data);
            //            //--------------
            //            DataSet mySetH = ObjDb.GetRows("select * from MstGaji_H where nokaryawan = '" + hdnnoKaryawan.Value + "'");
            //            DataRow myRowH = mySetH.Tables[0].Rows[0];
            //            string noGajiKaryawan = myRowH["noGajiKaryawan"].ToString();
            //            string noKaryawand = myRowH["noKaryawan"].ToString();

            //            ObjGlobal.Param.Clear();
            //            ObjGlobal.Param.Add("nokaryawan", noKaryawand);
            //            ObjGlobal.Param.Add("noGajiKaryawan", noGajiKaryawan);
            //            ObjGlobal.Param.Add("gapok", Convert.ToDecimal(txtNilai.Text).ToString());
            //            ObjGlobal.ExecuteProcedure("SPInsertMstGajipokokD", ObjGlobal.Param);

            //            for (int i = 0; i < grdSaldoGL.Rows.Count; i++)
            //            {
            //                HiddenField hdnNoRek = (HiddenField)grdSaldoGL.Rows[i].FindControl("hdnNoRek");
            //                TextBox txtDebet = (TextBox)grdSaldoGL.Rows[i].FindControl("txtDebet");
            //                ObjGlobal.Param.Clear();
            //                ObjGlobal.Param.Add("nokaryawan", noKaryawand);
            //                ObjGlobal.Param.Add("noGajiKaryawan", noGajiKaryawan);
            //                ObjGlobal.Param.Add("noKomponenGaji", hdnNoRek.Value);
            //                ObjGlobal.Param.Add("nilai", Convert.ToDecimal(txtDebet.Text).ToString());
            //                ObjGlobal.ExecuteProcedure("SPInsertMstGajiD", ObjGlobal.Param);

            //            }
            //            loadData();
            //            this.ShowHideGridAndForm(true, false, false);
            //            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            //            ShowMessage("success", "Data berhasil disimpan");
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            //            ShowMessage("error", message);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            //        ShowMessage("error", ex.ToString());
            //    }
        }


        protected void grdGajiKar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdGajiKar.PageIndex = e.NewPageIndex;
            loadData(cboUnit.Text);

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false, false);
            clearData();
            CloseMessage();
        }
        protected void cboAksi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAksi.Text == "Import")
            {
                btnImport.Enabled = true;
                btnExport.Enabled = false;
            }
            else if (cboAksi.Text == "Export")
            {
                btnImport.Enabled = false;
                btnExport.Enabled = true;
            }
            else
            {
                btnImport.Enabled = false;
                btnExport.Enabled = false;
            }
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            bool valid = true;

            try
            {
                if (flUpload.HasFile == true)
                {
                    //Upload and save the file
                    string excelPath = Server.MapPath("~/Assets/UploadTable/") + Path.GetFileName(flUpload.PostedFile.FileName);
                    flUpload.SaveAs(excelPath);
                    string conString = string.Empty;
                    string extension = Path.GetExtension(flUpload.PostedFile.FileName);

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
                    DataSet mySetSts = ObjDb.GetRows("select distinct a.komponengaji from MstGaji_d l inner join MstGaji_H k on l.noGajiKaryawan=k.noGajiKaryawan inner join Mstkomponengaji a on a.nokomponengaji = l.nokomponengaji where   a.kategori in(0, 1, 5, 6, 7, 8)  and jenis = 1 and k.noCabang = " + cboUnit.Text + "  order by a.komponengaji");

                    foreach (DataRow row2 in dt.Rows)
                        {
                            string createdBy = Convert.ToString(ObjSys.GetUserId);
                            string noKaryawan = Convert.ToString(row2["noKaryawan"]);
                            string nama = Convert.ToString(row2["nama"]);
                            string nocabang = Convert.ToString(row2["noCabang"]);

                        string[] komponengajies = new string[mySetSts.Tables[0].Rows.Count];

                        for (int x = 0; x < mySetSts.Tables[0].Rows.Count; x++)
                            {
                                komponengajies[x] =  Convert.ToString(row2[mySetSts.Tables[0].Rows[x]["komponengaji"].ToString()]);

                            }
               

                         using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                            {

                                using (SqlCommand cmd = new SqlCommand("SPProcessImportKomponenGaji"))
                                {

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@createdBy", createdBy);
                                    cmd.Parameters.AddWithValue("@noKaryawan", noKaryawan);
                                    cmd.Parameters.AddWithValue("@nama", nama);
                                    cmd.Parameters.AddWithValue("@nocabang", nocabang);
                                    for (int z = 0; z < mySetSts.Tables[0].Rows.Count; z++)
                                    {
                                    cmd.Parameters.AddWithValue("@komponengaji"+z, komponengajies[z]);

                                    }
                               



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
                    ShowMessage("error", "tes");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
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
                            cmd = new SqlCommand("SPProssesExportGajiKaryawanNew", con);
                            cmd.Parameters.Add(new SqlParameter("@noCabang", cboUnit.Text));
                            cmd.Parameters.Add(new SqlParameter("@perwakilan", cboCabang.Text));

                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            //int nokolom = 1;
                            // creating an array 
                            //if (cboUnit.Text == "0")
                            //{
                            //    DataSet mySetSts = ObjDb.GetRows("select distinct a.komponengaji from MstGaji_d l inner join MstGaji_H k on l.noGajiKaryawan=k.noGajiKaryawan inner join Mstkomponengaji a on a.nokomponengaji = l.nokomponengaji where   a.kategori in(0, 1, 5, 6)  and jenis = 1 and k.noCabang = " + cboUnit.Text + "  order by a.komponengaji");

                            //}
                            //else
                            //{
                                DataSet mySetSts = ObjDb.GetRows("select distinct a.komponengaji from MstGaji_d l inner join MstGaji_H k on l.noGajiKaryawan=k.noGajiKaryawan inner join Mstkomponengaji a on a.nokomponengaji = l.nokomponengaji where   a.kategori in(0, 1, 5, 6, 7 ,8)  and jenis = 1 and k.noCabang = " + cboUnit.Text + "  order by a.komponengaji");
                            //}
                                for (int i = 1; i < mySetSts.Tables[0].Rows.Count + 3;i++)
                            {
                                int[] a_array = new int[] { i };

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
                                while (number < mySetSts.Tables[0].Rows.Count + 3)
                                {
                                    number = number + 1;
                                    comma += ',';
                                }
                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Data rows.
                                    no++;
                                    csv += row[column.ColumnName].ToString().Replace(",", ".") + ',';
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
                            Response.AddHeader("content-disposition", "attachment;filename=penghasilan_tetap " + ObjSys.GetNow + ".csv");
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
        protected void grdGajiKar_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataCombo3(cboCabang.Text);
            loadData(cboUnit.Text);

        }
        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadData(cboUnit.Text);

        }
        protected void LoadDataCombo3(string cabang = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("perwakilan", cabang);
            cboUnit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnit", ObjGlobal.Param);
            cboUnit.DataValueField = "id";
            cboUnit.DataTextField = "name";
            cboUnit.DataBind();

        }

    }
}