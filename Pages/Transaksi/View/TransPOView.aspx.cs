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
    public partial class TransPOView : System.Web.UI.Page
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
                loadCombo();
                loadData();

            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            grdKas.DataSource = ObjGlobal.GetDataProcedure("SPViewPO", ObjGlobal.Param);
            grdKas.DataBind();

       }

        protected void loadCombo()
        {
            cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang= '" + ObjSys.GetCabangId + "') a");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();
        }

        #region setInitial & AddRow
        private void SetInitialRow(string Id = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnKasBank", typeof(string)));
            dt.Columns.Add(new DataColumn("lblbarang", typeof(string)));
            dt.Columns.Add(new DataColumn("lblqty", typeof(string)));
            dt.Columns.Add(new DataColumn("lblSatuan", typeof(string)));
            dt.Columns.Add(new DataColumn("lblharga", typeof(string)));
            dt.Columns.Add(new DataColumn("lblTotal", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPViewpodetail", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnKasBank"] = myRow["noPoD"].ToString();
                dr["lblbarang"] = myRow["namaBarang"].ToString();
                dr["lblqty"] = ObjSys.IsFormatNumber(myRow["qty"].ToString());
                dr["lblSatuan"] = myRow["Satuan"].ToString();
                dr["lblharga"] = ObjSys.IsFormatNumber(myRow["hargasatuan"].ToString());
                dr["lblTotal"] = ObjSys.IsFormatNumber(myRow["Total"].ToString());

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnKasBank"] = string.Empty;
                dr["lblbarang"] = string.Empty;
                dr["lblqty"] = string.Empty;
                dr["lblSatuan"] = string.Empty;
                dr["lblharga"] = string.Empty;
                dr["lblTotal"] = string.Empty;

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdKasBank.DataSource = dt;
            grdKasBank.DataBind();

            SetPreviousData();
        }
        private void SetPreviousData()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HiddenField hdnKasBank = (HiddenField)grdKasBank.Rows[i].FindControl("hdnKasBank");
                        Label lblbarang = (Label)grdKasBank.Rows[i].FindControl("lblbarang");
                        Label lblqty = (Label)grdKasBank.Rows[i].FindControl("lblqty");
                        Label lblSatuan = (Label)grdKasBank.Rows[i].FindControl("lblSatuan");
                        Label lblharga = (Label)grdKasBank.Rows[i].FindControl("lblharga");
                        Label lblTotal = (Label)grdKasBank.Rows[i].FindControl("lblTotal");

                        
                        lblbarang.Text = dt.Rows[i]["lblbarang"].ToString();
                        lblqty.Text = dt.Rows[i]["lblqty"].ToString();
                        lblSatuan.Text = dt.Rows[i]["lblSatuan"].ToString();
                        lblharga.Text = dt.Rows[i]["lblharga"].ToString();
                        lblTotal.Text = dt.Rows[i]["lblTotal"].ToString();

                    }
                }
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        protected void grdKas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdKas.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void grdKas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = grdKas.SelectedRow.RowIndex;
                string id = grdKas.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = id;

                DataSet mySet = ObjDb.GetRows("select a.*,b.namaSupplier from TransPO_H a inner join mSupplier b on a.nosup=b.nosupplier where noPO = '" + hdnId.Value + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];

                txtAccount1.Text = myRow["kodePO"].ToString();
                dtKas.Text = Convert.ToDateTime(myRow["tglPO"]).ToString("dd-MMM-yyyy");
                txtRemark.Text = myRow["namaSupplier"].ToString();

                grdKasBank.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
                showHideFormKas(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void grdKasView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detail")
            {
                try
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    HiddenField hdnIdPrint = (HiddenField)grdKas.Rows[rowIndex].FindControl("hdnIdPrint");
                    HttpContext.Current.Session["ParamReport"] = null;
                    Session["REPORTNAME"] = null;
                    Session["REPORTTITLE"] = null;
                    Param.Clear();
                    Param.Add("noKas", hdnIdPrint.Value);
                    HttpContext.Current.Session.Add("ParamReport", Param);
                    Session["REPORTNAME"] = "Rptvoucherbanknew.rpt";
                    Session["REPORTTILE"] = "Report Cash Bank";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);

                }
                catch (Exception ex)
                {
                    Response.Write("Error:" + ex.ToString());
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
                }
            }
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

        protected void prosesDebitOrKredit(string id = "")
        {
          
            }
        }
}

//protected void btnReset_Click(object sender, EventArgs e)
//{
//    showHideFormKas(true, false);
//}
