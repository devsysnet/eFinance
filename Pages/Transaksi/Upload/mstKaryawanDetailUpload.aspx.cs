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

namespace eFinance.Pages.Master.Update
{
    public partial class mstKaryawanDetailUpload : System.Web.UI.Page
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
                LoadDataCombo();
            }
        }



        protected void loadData(string cabang = "")
        {
           
        }

        protected void cmdMode_Click(object sender, EventArgs e)
        {

        }
        protected void LoadDataCombo()
        {


            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang
            //pusat
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE noCabang = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //perwakilan
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //unit
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
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
                    if(cboDetil.Text == "identitas")
                    {
                        DataSet mySetSts = ObjDb.GetRows("SELECT identitas from mIdentitas order by identitas");

                        foreach (DataRow row2 in dt.Rows)
                        {
                            string createdBy = Convert.ToString(ObjSys.GetUserId);
                            string noKaryawan = Convert.ToString(row2["noKaryawan"]);
                            string nama = Convert.ToString(row2["nama"]);
                            string nocabang = Convert.ToString(row2["noCabang"]);

                            string[] identitases = new string[mySetSts.Tables[0].Rows.Count];

                            for (int x = 0; x < mySetSts.Tables[0].Rows.Count; x++)
                            {
                                identitases[x] = Convert.ToString(row2[mySetSts.Tables[0].Rows[x]["identitas"].ToString()]);

                            }


                            using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                            {

                                using (SqlCommand cmd = new SqlCommand("SPProcessImportIdentitas"))
                                {

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@createdBy", createdBy);
                                    cmd.Parameters.AddWithValue("@noKaryawan", noKaryawan);
                                    cmd.Parameters.AddWithValue("@nama", nama);
                                    cmd.Parameters.AddWithValue("@nocabang", nocabang);
                                    for (int z = 0; z < mySetSts.Tables[0].Rows.Count; z++)
                                    {
                                        cmd.Parameters.AddWithValue("@identitas" + z, identitases[z]);

                                    }




                                    cmd.Connection = con;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
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
                            cmd = new SqlCommand("SPProssesExportInputDetailKaryawan", con);
                            cmd.Parameters.Add(new SqlParameter("@noCabang", cboUnit.Text));
                            cmd.Parameters.Add(new SqlParameter("@perwakilan", cboCabang.Text));
                            cmd.Parameters.Add(new SqlParameter("@detail", cboDetil.Text));

                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            //int nokolom = 1;
                            // creating an array 
                            DataSet mySetSts = ObjDb.GetRows("SELECT identitas from mIdentitas    order by Identitas");
                            for (int i = 1; i < mySetSts.Tables[0].Rows.Count + 3; i++)
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
                            Response.AddHeader("content-disposition", "attachment;filename=inpu_detail_ " + ObjSys.GetNow + ".csv");
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
            //string message = "";
            //bool valid = true;
            //try
            //{
            //    int rowIndex = grdGajiKar.SelectedRow.RowIndex;
            //    string noKaryawan = grdGajiKar.DataKeys[rowIndex].Values[0].ToString();
            //    hdnId.Value = noKaryawan;

            //    DataSet MySet = ObjDb.GetRows("select a.*,CAST(CONVERT(varchar, CAST(isnull(b.nilai,0) AS Money), 1) AS varchar) as nilai from mstKaryawan a left join viewGolongan b on a.golongan=b.gol where noKaryawan = '" + noKaryawan + "'");
            //    if (MySet.Tables[0].Rows.Count > 0)
            //    {
            //        DataRow myRow = MySet.Tables[0].Rows[0];

            //        txtNama.Text = myRow["nama"].ToString();
            //        txtNUPTK.Text = myRow["NUPTK"].ToString();
            //        hdnnoKaryawan.Value = myRow["noKaryawan"].ToString();
            //        hdnnocabang.Value = myRow["nocabang"].ToString();
            //        if (String.IsNullOrEmpty(myRow["tglmasuk"].ToString()))
            //            dtmasuk.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            //        else
            //            dtmasuk.Text = Convert.ToDateTime(myRow["tglmasuk"]).ToString("dd-MMM-yyyy");
            //        cboGolPegawai.Text = myRow["golongan"].ToString();
            //        cboDepartemen.Text = myRow["dept"].ToString();
            //        txtNilai.Text = myRow["nilai"].ToString();
            //        cboStskaryawan.Text = myRow["statusPeg"].ToString();

            //        this.ShowHideGridAndForm(false, true, false);
            //        CloseMessage();
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            //        ShowMessage("error", "Data Tidak ada.");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    if (valid == false)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            //        ShowMessage("error", message);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            //        ShowMessage("error", ex.ToString());
            //    }
            //}
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
            if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
                ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
                ObjGlobal.Param.Add("perwakilan", cabang);
                ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
                cboUnit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitgajikaryawan", ObjGlobal.Param);
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();

            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "3")
            {

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
                ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
                ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
                ObjGlobal.Param.Add("perwakilan", cabang);
                cboUnit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitgajikaryawan", ObjGlobal.Param);
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();

            }
            else
            {
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
                ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
                ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
                ObjGlobal.Param.Add("perwakilan", cabang);
                cboUnit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitgajikaryawan", ObjGlobal.Param);
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();


            }

        }

    }
}