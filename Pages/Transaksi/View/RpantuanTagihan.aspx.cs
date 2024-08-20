using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using eFinance.GlobalApp;

namespace eFinance.Pages.Transaksi.View
{
    public partial class RpantuanTagihan : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();
                LoadJudul(cboCabang.Text,cboYear.Text, cboMonth.Text);
                judul.Visible = false;
                periode.Visible = false;
            }
        }
        protected void LoadJudul(string nocabang, string thn,string bln)
        {
            DataSet mySet = ObjDb.GetRows("select namacabang from mcabang where nocabang = '" + nocabang + "' ");
            DataRow myRow = mySet.Tables[0].Rows[0];
            string namacabang = myRow["namacabang"].ToString();

            judul.InnerText = "EVALUASI " + namacabang.ToUpper();

            string namabln = "";
            if(bln == "1")
            {
                namabln = "Januari";

            }
            else if (bln == "2")
            {
                namabln = "Februari";

            }
            else if (bln == "3")
            {
                namabln = "Maret";

            }
            else if (bln == "4")
            {
                namabln = "April";

            }
            else if (bln == "5")
            {
                namabln = "Mei";

            }
            else if (bln == "6")
            {
                namabln = "Juni";

            }
            else if (bln == "7")
            {
                namabln = "Juli";

            }
            else if (bln == "8")
            {
                namabln = "Agustus";

            }
            else if (bln == "9")
            {
                namabln = "September";

            }
            else if (bln == "10")
            {
                namabln = "Oktober";

            }
            else if (bln == "11")
            {
                namabln = "November";

            }
            else if (bln == "12")
            {
                namabln = "Desember";

            }

            periode.InnerText = "Periode " + namabln + " " + thn;

        }
        protected void printPajak(object sender, EventArgs e)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "printPajak();", true);
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
                            cmd = new SqlCommand("SPviewRpantuan", con);
                            cmd.Parameters.Add(new SqlParameter("@thn", cboYear.Text));
                            cmd.Parameters.Add(new SqlParameter("@bln", cboMonth.Text));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
                            cmd.Parameters.Add(new SqlParameter("@jns", cboreport.Text));
                            cmd.Parameters.Add(new SqlParameter("@tahunajaran", cbothnajaran.Text));
                            cmd.Parameters.Add(new SqlParameter("@jnsrpt", cbojnsrpt.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "Report_evaluasi" + ObjSys.GetNow + ".xls";
                            ViewHelper.DownloadExcel(Response, fileName, dt);
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            //ShowMessage("error", ex.ToString());
                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    //ShowMessage(alert, message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //ShowMessage("error", ex.ToString());
            }
        }

        #region LoadData
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", cboYear.Text);
            ObjGlobal.Param.Add("bln", cboMonth.Text);
            ObjGlobal.Param.Add("jns", cboreport.Text);
            ObjGlobal.Param.Add("tahunajaran", cbothnajaran.Text);
            ObjGlobal.Param.Add("jnsrpt", cbojnsrpt.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPviewRpantuan", ObjGlobal.Param);
            grdAccount.DataBind();
            LoadJudul(cboCabang.Text,cboYear.Text, cboMonth.Text);
            judul.Visible = true;
            periode.Visible = true;

        }

        protected void loadDataCombo()
        {
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang in(2,3,4) and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang) a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang where nocabang = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                DataSet mySet = ObjDb.GetRows("select parent from mcabang where nocabang = '" + ObjSys.GetCabangId + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];
                hdnParent.Value = myRow["parent"].ToString();
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "' " +
                     " union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE parent = '" + hdnParent.Value + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

            cbothnajaran.DataSource = ObjDb.GetRows("SELECT distinct tahunajaran id, tahunajaran name FROM transpiutang where tahunajaran<>''");
            cbothnajaran.DataValueField = "id";
            cbothnajaran.DataTextField = "name";
            cbothnajaran.DataBind();
        }

        #endregion

    }
}