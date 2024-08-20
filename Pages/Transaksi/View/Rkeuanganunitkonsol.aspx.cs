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
    public partial class Rkeuanganunitkonsol : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();

                cboYear1.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
                cboYear1.DataValueField = "id";
                cboYear1.DataTextField = "name";
                cboYear1.DataBind();

            }
        }


        #region LoadData
        protected void btnSearch_Click(object sender, EventArgs e)
        {
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("thn", cboYear.Text);
                ObjGlobal.Param.Add("thn1", cboYear1.Text);
                ObjGlobal.Param.Add("bln", cboMonth.Text);
                ObjGlobal.Param.Add("bln1", cboMonth1.Text);
                ObjGlobal.Param.Add("nocabang", cboCabang.Text);
                ObjGlobal.Param.Add("type", cboType.Text);
                grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPReportRabsallunit", ObjGlobal.Param);
                grdAccount.DataBind();
              
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
                        ObjGlobal.Param.Add("thn1", cboYear1.Text);
                        ObjGlobal.Param.Add("bln", cboMonth.Text);
                        ObjGlobal.Param.Add("bln1", cboMonth1.Text);
                        ObjGlobal.Param.Add("nocabang", cboCabang.Text);
                        ObjGlobal.Param.Add("type", cboType.Text);
                        DataSet dataSet = ObjGlobal.GetDataProcedure("SPReportRabsallunit", ObjGlobal.Param);


                        String fileName = "Report_Keuangan" + ObjSys.GetNow + ".xls";
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






        protected void loadDataCombo()
        {
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang in(2,3,4)) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stscabang in(2,3,4) and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stscabang in(2,3,4)  " +
                       "union " +
                       "SELECT distinct a.nocabang id, a.namaCabang name FROM vCabang a inner join vcabang b on a.nocabang=b.parent where b.nocabang = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stscabang in(2,3,4) " +
                        "union " +
                        "SELECT distinct a.nocabang id, a.namaCabang name FROM vCabang a inner join vcabang b on a.nocabang=b.parent) a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

        }
        #endregion
        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }

    }
}