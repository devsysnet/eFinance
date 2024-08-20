using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using eFinance.GlobalApp;

namespace eFinance.Pages.Master.View
{
    public partial class MstResumeTrackingView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //LoadIndexYayasan();
                loadDataCombo();
                //loadDataComboKelas();
    
                //LoadData();
            }
        }

        //protected void LoadIndexYayasan()
        //{
        //    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
        //    DataRow myRow = mySet.Tables[0].Rows[0];
        //    hdnnoYysn.Value = myRow["noCabang"].ToString();
        //}


        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cboCabang.Text);
            //ObjGlobal.Param.Add("noCabangload", ObjSys.GetCabangId);
            grdSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadTracking", ObjGlobal.Param);
            grdSiswa.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
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
                           cmd = new SqlCommand("SPLoadTracking",con);
                            cmd.Parameters.Add(new SqlParameter("@noCabang", cboCabang.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "Report_Tracking" + ObjSys.GetNow + ".xls";
                            ViewHelper.DownloadExcel(Response, fileName, dt);
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

        protected void loadDataCombo()
        {

            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from ( SELECT distinct nocabang id, namaCabang name FROM vCabang) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from ( SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from ( SELECT distinct nocabang id, namaCabang name FROM vCabang) a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "3")
            {

                cboCabang.DataSource = ObjDb.GetRows("select a.* from ( SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE nocabang = '" + ObjSys.GetParentCabang + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetParentCabang + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from ( SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
        }

        //protected void LoadDataCombo2()
        //{
           

        //    loadDataComboKelas();
        //}

        //protected void loadDataComboKelas()
        //{
           
        //}

        protected void grdSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdSiswa.PageIndex = e.NewPageIndex;
            LoadData();
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
            LoadData();
        }

        //protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    loadDataComboKelas();
        //}

        //protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadDataCombo2();
        //    LoadData();
            
        //}

        //protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    loadDataComboKelas();
            
        //    LoadData();

        //}

       
        protected void grdSiswa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnJmlTunggakan = (HiddenField)e.Row.FindControl("hdnJmlTunggakan");
                Label lblnik = (Label)e.Row.FindControl("lblnik");
                Label lblnis = (Label)e.Row.FindControl("lblnis");
                Label lblnisn = (Label)e.Row.FindControl("lblnisn");
                Label lblNamaSiswa = (Label)e.Row.FindControl("lblNamaSiswa");
                Label lblkelas = (Label)e.Row.FindControl("lblkelas");
                Label lbllokasiUnit = (Label)e.Row.FindControl("lbllokasiUnit");

                if (hdnJmlTunggakan.Value == "2")
                {
                    lblnik.ForeColor = Color.Red;
                    lblnis.ForeColor = Color.Red;
                    lblnisn.ForeColor = Color.Red;
                    lblNamaSiswa.ForeColor = Color.Red;
                    lblkelas.ForeColor = Color.Red;
                    lbllokasiUnit.ForeColor = Color.Red;
                }
                else
                {
                    lblnik.ForeColor = Color.Black;
                    lblnis.ForeColor = Color.Black;
                    lblnisn.ForeColor = Color.Black;
                    lblNamaSiswa.ForeColor = Color.Black;
                    lblkelas.ForeColor = Color.Black;
                    lbllokasiUnit.ForeColor = Color.Black;
                }
            }
        }
    }
}