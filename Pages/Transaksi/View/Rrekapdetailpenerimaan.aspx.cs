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
    public partial class Rrekapdetailpenerimaan : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                //LoadData();
                LoadData(cboCabang.Text, cboYear.Text);

            }
            else
            {
                //LoadData(cboCabang.Text, cboYear.Text);
            }
        }

        protected void LoadData(string cabang = "0", string thn = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", thn);
            ObjGlobal.Param.Add("noCabang", cabang);
            grdSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDatarekapdetpenerimaan", ObjGlobal.Param);
            grdSiswa.DataBind();

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";
            try
            {
                if (valid == true)
                {


                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    try
                    {
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("thn", cboYear.Text);
                       ObjGlobal.Param.Add("nocabang", cboCabang.Text);
                        DataSet dataSet = ObjGlobal.GetDataProcedure("SPLoadDatarekapdetpenerimaan", ObjGlobal.Param);


                        String fileName = "rekap_Detail_penerimaan_" + ObjSys.GetNow + ".xls";
                        ViewHelper.DownloadExcel(Response, fileName, dataSet.Tables[0]);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", ex.ToString());
                    }

                    //}
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



        //protected void btnExport_Click(object sender, EventArgs e)
        //{
        //    bool valid = true;
        //    string message = "", alert = "";
        //    try
        //    {
        //        if (valid == true)
        //        {
        //            using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
        //            {

        //                SqlCommand cmd = new SqlCommand();
        //                SqlDataAdapter da = new SqlDataAdapter();
        //                DataTable dt = new DataTable();
        //                try
        //                {
        //                    cmd = new SqlCommand("SPLoadDatarekapdetpenerimaan", con);
        //                    cmd.Parameters.Add(new SqlParameter("@thn", cboYear.Text));
        //                    cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    da.SelectCommand = cmd;
        //                    da.Fill(dt);

        //                    String fileName = "Detail_Penerimaan" + ObjSys.GetNow + ".xls";
        //                    ViewHelper.DownloadExcel(Response, fileName, dt);
        //                }
        //                catch (Exception ex)
        //                {
        //                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //                    //ShowMessage("error", ex.ToString());
        //                }

        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //            //ShowMessage(alert, message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //        //ShowMessage("error", ex.ToString());
        //    }
        //}

        protected void loadDataCombo()
        {
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
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang) a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //perwakilan
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE parent = '" + ObjSys.GetCabangId + "') a ");
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

            cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
            cboYear.DataValueField = "id";
            cboYear.DataTextField = "name";
            cboYear.DataBind();

        }


        protected void grdSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdSiswa.PageIndex = e.NewPageIndex;
            LoadData(cboCabang.Text, cboYear.Text);
        }

        protected void grdSiswa_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;

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
            LoadData(cboCabang.Text, cboYear.Text);
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData(cboCabang.Text, cboYear.Text);
        }

        //protected void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    CloseMessage();
        //    LoadData(cboCabang.Text, cboYear.Text);
        //}

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData(cboCabang.Text, cboYear.Text);
        }
    }
}