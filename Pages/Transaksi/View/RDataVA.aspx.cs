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
    public partial class RDataVA : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadIndexYayasan();
                loadDataCombo();
                loadDataComboKelas();
                
                LoadData(cboPerwakilan.Text, cboUnit.Text, cboKelas.Text);
            }
        }

        protected void LoadIndexYayasan()
        {
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
            DataRow myRow = mySet.Tables[0].Rows[0];
            hdnnoYysn.Value = myRow["noCabang"].ToString();
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
                            cmd = new SqlCommand("ReportDataVA", con);
                            cmd.Parameters.Add(new SqlParameter("@kelas", cboKelas.Text));
                            cmd.Parameters.Add(new SqlParameter("@perwakilan", cboPerwakilan.Text));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboUnit.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "listtagihaVA_" + ObjSys.GetNow + ".xls";
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

        protected void LoadData(string perwakilan = "0", string unit = "0", string kelas = "0", string search = "", string cbotahunajaran = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("perwakilan", perwakilan);
            ObjGlobal.Param.Add("noCabang", unit);
            ObjGlobal.Param.Add("kelas", kelas);
            //ObjGlobal.Param.Add("Search", search);
            //ObjGlobal.Param.Add("tahunajaran", cbotahunajaran);
            grdSiswa.DataSource = ObjGlobal.GetDataProcedure("ReportDataVA", ObjGlobal.Param);
            grdSiswa.DataBind();

        }

        protected void loadDataCombo()
        {

            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Perwakilan' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and noCabang=" + ObjSys.GetCabangId + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where noCabang=" + ObjSys.GetParentCabang + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            LoadDataCombo2();

            cbotahunajaran.DataSource = ObjDb.GetRows("select a.* from (select distinct tahunAjaran,tahunAjaran from TransKelas) a");
            cbotahunajaran.DataValueField = "id";
            cbotahunajaran.DataTextField = "name";
            cbotahunajaran.DataBind();

        }

        protected void LoadDataCombo2()
        {
            if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {
                cboUnit.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + ObjSys.GetParentCabang + " and nocabang=" + ObjSys.GetCabangId + " and stspusat=0 and stscabang=2) a");
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();
            }
            else
            {
                cboUnit.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Unit' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + cboPerwakilan.Text + " and stspusat=0 and stscabang=2) a");
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();
            }

            loadDataComboKelas();
        }

        protected void loadDataComboKelas()
        {
            if (cboUnit.Text == "0")
            {
                cboKelas.DataSource = ObjDb.GetRows("select * from (select '-' as id, 'Semua Kelas' as name union select distinct ltrim(kelas) as id, ltrim(kelas) as name from TransKelas)x");
                cboKelas.DataValueField = "id";
                cboKelas.DataTextField = "name";
                cboKelas.DataBind();
            }
            else
            {
                cboKelas.DataSource = ObjDb.GetRows("select * from (select '-' as id, 'Semua Kelas' as name union select distinct ltrim(kelas) as id, ltrim(kelas) as name from TransKelas where nocabang='" + cboUnit.Text + "')x");
                cboKelas.DataValueField = "id";
                cboKelas.DataTextField = "name";
                cboKelas.DataBind();
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
            LoadData(cboPerwakilan.Text, cboUnit.Text, cboKelas.Text);
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataComboKelas();
        }

        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataCombo2();
            LoadData(cboPerwakilan.Text, cboUnit.Text, cboKelas.Text);
        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataComboKelas();
    
            LoadData(cboPerwakilan.Text, cboUnit.Text, cboKelas.Text);

        }

        protected void cboKelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(cboPerwakilan.Text, cboUnit.Text, cboKelas.Text);
        }

        protected void showhidedropdown(bool showhideclass)
        {
            divclass.Visible = showhideclass;
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(cboPerwakilan.Text, cboUnit.Text, cboKelas.Text, txtSearch.Text);
        }

       
    }
}