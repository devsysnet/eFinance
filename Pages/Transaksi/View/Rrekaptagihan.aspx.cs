using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.View
{
    public partial class Rrekaptagihan : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();
                LoadIndexYayasan();
                loadDataPerwakilan();

            }
        }
        protected void LoadIndexYayasan()
        {
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
            DataRow myRow = mySet.Tables[0].Rows[0];
            hdnYayasan.Value = myRow["noCabang"].ToString();
        }
        #region LoadData
        protected void btnPosting_Click(object sender, EventArgs e)
        {
            btnExport.Visible = true;
            DataSet mySet = ObjDb.GetRows("select stsCabang from mcabang where nocabang = " + cboCabang.Text);
            DataRow myRow = mySet.Tables[0].Rows[0];
            var stsCabang = myRow["stsCabang"].ToString();
            if (stsCabang == "3")
            {
                unit.Visible = false;
                yayasan.Visible = true;
                btnExport.Visible = false;
                Button2.Visible = true;
            }
            else
            {
                btnExport.Visible = true;
                Button2.Visible = false;

                unit.Visible = true;
                yayasan.Visible = false;
            }
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", cboYear.Text);
            ObjGlobal.Param.Add("bln", cboMonth.Text);
            ObjGlobal.Param.Add("kelas", cboKelas.Text);
            ObjGlobal.Param.Add("jenis", cboJnsTrans.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPRekapARsiswa", ObjGlobal.Param);
            grdAccount.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", cboYear.Text);
            ObjGlobal.Param.Add("bln", cboMonth.Text);
            ObjGlobal.Param.Add("kelas", cboKelas.Text);
            ObjGlobal.Param.Add("jenis", cboJnsTrans.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            grdAccount1.DataSource = ObjGlobal.GetDataProcedure("SPRekapARsiswa1", ObjGlobal.Param);
            grdAccount1.DataBind();

        }
        protected void btnExport_Click(object sender, EventArgs e)
        {

        

            try
            {
                
                    using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                    {

                        SqlCommand cmd = new SqlCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        DataTable dt = new DataTable();
                        try
                        {
                            cmd = new SqlCommand("SPRekapARsiswa", con);
                            cmd.Parameters.Add(new SqlParameter("@thn", cboYear.Text));
                            cmd.Parameters.Add(new SqlParameter("@bln", cboMonth.Text));
                            cmd.Parameters.Add(new SqlParameter("@kelas", cboKelas.Text));
                            cmd.Parameters.Add(new SqlParameter("@jenis", cboJnsTrans.Text));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            int nokolom = 1;
                            // creating an array 
                            int[] a_array = new int[] { 1, 2, 3, 4, 5, 6, 7  };
                            //foreach (DataColumn column in dt.Columns)
                            foreach (int column in a_array)
                            {
                                //Add the Header row for CSV file.
                                //csv += column.ColumnName + ',';
                                csv += "column" + nokolom++ + ',';
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
                                while (number < 7)
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
                            Response.AddHeader("content-disposition", "attachment;filename=Rekap_Tagihan_Siswa_Belum_Bayar_Bulan_" + cboMonth.Text + "_Tahun_"+cboYear.Text+".csv");
                            Response.Charset = "";
                            Response.ContentType = "application/text";
                            Response.Output.Write(csv);
                            Response.Flush();
                            Response.End();

                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        }

                    }
             
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            }
        }
        protected void loadDataPerwakilan()
        {
            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang
            //if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            //{
            //    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang) a order by urutan");
            //    cboCabang.DataValueField = "id";
            //    cboCabang.DataTextField = "name";
            //    cboCabang.DataBind();
            //}
            //else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            //{
            //    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang) a ");
            //    cboCabang.DataValueField = "id";
            //    cboCabang.DataTextField = "name";
            //    cboCabang.DataBind();
            //}
            //else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            //{
            //    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
            //            "union " +
            //            "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
            //    cboCabang.DataValueField = "id";
            //    cboCabang.DataTextField = "name";
            //    cboCabang.DataBind();
            //}
            //else
            //{
            //    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
            //    cboCabang.DataValueField = "id";
            //    cboCabang.DataTextField = "name";
            //    cboCabang.DataBind();
            //}

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", ObjSys.GetParentCabang);
            cboPerwakilan.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataKantorPerwakilan", ObjGlobal.Param);
            cboPerwakilan.DataValueField = "id";
            cboPerwakilan.DataTextField = "name";
            cboPerwakilan.DataBind();

            loadDataUnit(cboPerwakilan.Text);
        }
        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataUnit(cboPerwakilan.Text);
        }
        protected void loadDataUnit(string perwakilan = "0")
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", perwakilan);
            ObjGlobal.Param.Add("parentCabang", ObjSys.GetParentCabang);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            cboCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitPerwakilan2", ObjGlobal.Param);
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();

            loadDataDetil(cboCabang.Text);

        }

        protected void loadDataDetil(string cabang = "")
        {
            cboKelas.DataSource = ObjDb.GetRows("select * from (select '--Pilih Kelas--' as id, '--Pilih Kelas--' as name union select distinct kelas as id, kelas as name from TransKelas where nocabang='" + cabang + "')x");
            cboKelas.DataValueField = "name";
            cboKelas.DataTextField = "name";
            cboKelas.DataBind();

            cboJnsTrans.DataSource = ObjDb.GetRows("select * from (select '0' as id, '--Pilih Semua--' as name union select distinct noTransaksi as id, jenisTransaksi as name from mJenisTransaksi where nocabang='" + cabang + "')x");
            cboJnsTrans.DataValueField = "id";
            cboJnsTrans.DataTextField = "name";
            cboJnsTrans.DataBind();
        }
        #endregion

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataDetil(cboCabang.Text);
            DataSet mySet = ObjDb.GetRows("select stsCabang from mcabang where nocabang = " + cboCabang.Text);
            DataRow myRow = mySet.Tables[0].Rows[0];
            var stsCabang = myRow["stsCabang"].ToString();
            if(stsCabang == "3")
            {
                cboKelas.Visible = false;
            }else
            {
                cboKelas.Visible = true;
            }
        }

        
    }
}