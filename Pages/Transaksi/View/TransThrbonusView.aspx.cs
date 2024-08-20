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
    public partial class TransThrbonusView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        //buatcetakan
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                loadData();

            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            grdKas.DataSource = ObjGlobal.GetDataProcedure("SPViewnTHRBonus", ObjGlobal.Param);
            grdKas.DataBind();

       }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        protected void grdKas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdKas.PageIndex = e.NewPageIndex;
            loadData();
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
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
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

        protected void grdKas_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            //    ShowMessage("error", ex.ToString());
            //}
        }

        protected void grdKasView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "detail")
            //{
            //    try
            //    {
            //        int rowIndex = int.Parse(e.CommandArgument.ToString());
            //        HiddenField hdnIdPrint = (HiddenField)grdKas.Rows[rowIndex].FindControl("hdnIdPrint");
            //        HttpContext.Current.Session["ParamReport"] = null;
            //        Session["REPORTNAME"] = null;
            //        Session["REPORTTITLE"] = null;
            //        Param.Clear();
            //        Param.Add("nonaikgaji", hdnIdPrint.Value);
            //        HttpContext.Current.Session.Add("ParamReport", Param);
            //        Session["REPORTNAME"] = "Rptsuratgajinaikgol.rpt";
            //        Session["REPORTTILE"] = "Surat Permintaan Kenaikan Gaji";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);

            //    }
            //    catch (Exception ex)
            //    {
            //        Response.Write("Error:" + ex.ToString());
            //        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
            //    }
            //}
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

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }



        protected void btnReset_Click(object sender, EventArgs e)
        {
            showHideFormKas(true, false);
        }


    }
}