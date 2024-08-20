using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.View
{
    public partial class mDatasiswa : System.Web.UI.Page
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
                           
                               cmd = new SqlCommand("SPLoadDataexportSiswa", con);
                            cmd.Parameters.Add(new SqlParameter("@cabang", cboCabang.Text));
                            cmd.Parameters.Add(new SqlParameter("@kelas", cboKelas.Text));

                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            //int nokolom = 1;
                            // creating an array 
                            int[] a_array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
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
                                while (number < 13)
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
                            Response.AddHeader("content-disposition", "attachment;filename=msiswa " + ObjSys.GetNow + ".csv");
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
        protected void LoadData(string cabang = "0", string kelas = "", string cari = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", cari);
            ObjGlobal.Param.Add("kelas", kelas);
            ObjGlobal.Param.Add("noCabang", cabang);
            grdSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataSiswa", ObjGlobal.Param);
            grdSiswa.DataBind();
            
        }

        protected void loadDataCombo()
        {
            //Perwakilan Mardi Yuana Bogor GetstsPusat=1
            if (ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4) and parent = " + ObjSys.GetCabangId + ") a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //unit
            else if (ObjSys.GetstsPusat == "2")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //admin Kantor Perwakilan Bogor GetstsPusat=3
            else if (ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4) and parent = " + ObjSys.GetParentCabang + ") a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4)) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

            loadDataKelas(cboCabang.Text);
        }

        protected void loadDataKelas(string cabang = "0")
        {
            DataSet mySet = ObjDb.GetRows("select tahunAjaran from parameter ");
            DataRow myRow = mySet.Tables[0].Rows[0];
            string tahunAjaran = myRow["tahunAjaran"].ToString();
            cboKelas.DataSource = ObjDb.GetRows("select * from (select '' as id, '---Semua Kelas---' as name union select distinct kelas as id, kelas as name from TransKelas where nocabang='" + cabang + "' and tahunAjaran = '" + tahunAjaran +"')x");
            cboKelas.DataValueField = "id";
            cboKelas.DataTextField = "name";
            cboKelas.DataBind();
        }
        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataKelas(cboCabang.Text);
           // LoadData(cboCabang.Text, cboKelas.Text, txtSearch.Text);
        }
        protected void grdSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdSiswa.PageIndex = e.NewPageIndex;
            LoadData(cboCabang.Text, cboKelas.Text, txtSearch.Text);
        }

        protected void grdSiswa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = grdSiswa.SelectedRow.RowIndex;
                string noSiswa = grdSiswa.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noSiswa;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noSiswa", hdnId.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataSiswa_Detail", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                txtNama.Text = myRow["namaSiswa"].ToString();
                txtNIK.Text = myRow["nik"].ToString();
                txtNIS.Text = myRow["nis"].ToString();
                txtNISN.Text = myRow["nisn"].ToString();
                txtJK.Text = myRow["jk"].ToString();
                txtAgama.Text = myRow["agama"].ToString();
                txtAlamat.Text = myRow["alamat"].ToString();
                txtTglLahir.Text = Convert.ToDateTime(myRow["tgllahir"]).ToString("dd-MMM-yyyy");
                txtOrtu.Text = myRow["namaOrangtua"].ToString();
                txtTelp.Text = myRow["telp"].ToString();
                txtVA.Text = myRow["novirtual"].ToString();
                txtKota.Text = myRow["Kota"].ToString();
                txtKotaLhr.Text = myRow["kotaLahir"].ToString();
                txtKet1.Text = myRow["ket1"].ToString();
                txtKet2.Text = myRow["ket2"].ToString();
                txtKet3.Text = myRow["ket3"].ToString();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noSiswa", hdnId.Value);
                ObjGlobal.Param.Add("nocabang", cboCabang.Text);
                grdPiutSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPiutSiswa", ObjGlobal.Param);
                grdPiutSiswa.DataBind();

                this.ShowHideGridAndForm(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false);
            LoadData(cboCabang.Text, cboKelas.Text, txtSearch.Text);
        }

        protected void cboKelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData(cboCabang.Text, cboKelas.Text, txtSearch.Text);
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData(cboCabang.Text, cboKelas.Text, txtSearch.Text);
        }

        
    }
}