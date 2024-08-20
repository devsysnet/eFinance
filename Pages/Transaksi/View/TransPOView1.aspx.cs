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
    public partial class TransPOView1 : System.Web.UI.Page
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
                dtMulai.Text = Convert.ToDateTime(ObjSys.GetDate).ToString("dd-MMM-yyyy");
                dtSampai.Text = Convert.ToDateTime(ObjSys.GetDate).ToString("dd-MMM-yyyy");
                loadData();

            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            grdPO.DataSource = ObjGlobal.GetDataProcedure("SPViewPO", ObjGlobal.Param);
            grdPO.DataBind();

        }

        protected void loadCombo()
        {
            cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang= '" + ObjSys.GetCabangId + "') a");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();
        }

        #region setInitial & AddRow
        private void SetInitialRow(string Id = "", string jnsPO = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtkodebrg", typeof(string)));
            dt.Columns.Add(new DataColumn("lblnamaBarang", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQty", typeof(string)));
            dt.Columns.Add(new DataColumn("txtStn", typeof(string)));
            dt.Columns.Add(new DataColumn("txtBudgetPR", typeof(string)));
            dt.Columns.Add(new DataColumn("txthargaPO", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTotal", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("Id", Id);
            ObjGlobal.Param.Add("jnsPO", jnsPO);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPViewpodetail", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtkodebrg"] = myRow["kodeBarang"].ToString();
                dr["lblnamaBarang"] = myRow["namaBarang"].ToString();
                dr["txtQty"] = ObjSys.IsFormatNumber(myRow["qty"].ToString());
                dr["txtStn"] = myRow["satuan"].ToString();
                dr["txtBudgetPR"] = ObjSys.IsFormatNumber(myRow["budget"].ToString());
                dr["txthargaPO"] = ObjSys.IsFormatNumber(myRow["hargasatuan"].ToString());
                dr["txtTotal"] = ObjSys.IsFormatNumber(myRow["total"].ToString());

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtkodebrg"] = string.Empty;
                dr["lblnamaBarang"] = string.Empty;
                dr["txtQty"] = string.Empty;
                dr["txtStn"] = string.Empty;
                dr["txtBudgetPR"] = string.Empty;
                dr["txthargaPO"] = string.Empty;
                dr["txtTotal"] = string.Empty;

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdPODetil.DataSource = dt;
            grdPODetil.DataBind();

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
                        TextBox txtkodebrg = (TextBox)grdPODetil.Rows[i].FindControl("txtkodebrg");
                        Label lblnamaBarang = (Label)grdPODetil.Rows[i].FindControl("lblnamaBarang");
                        TextBox txtStn = (TextBox)grdPODetil.Rows[i].FindControl("txtStn");
                        TextBox txtQty = (TextBox)grdPODetil.Rows[i].FindControl("txtQty");
                        TextBox txtBudgetPR = (TextBox)grdPODetil.Rows[i].FindControl("txtBudgetPR");
                        TextBox txthargaPO = (TextBox)grdPODetil.Rows[i].FindControl("txthargaPO");
                        TextBox txtTotal = (TextBox)grdPODetil.Rows[i].FindControl("txtTotal");

                        txtkodebrg.Text = dt.Rows[i]["txtkodebrg"].ToString();
                        lblnamaBarang.Text = dt.Rows[i]["lblnamaBarang"].ToString();
                        txtQty.Text = dt.Rows[i]["txtQty"].ToString();
                        txtStn.Text = dt.Rows[i]["txtStn"].ToString();
                        txtBudgetPR.Text = dt.Rows[i]["txtBudgetPR"].ToString();
                        txthargaPO.Text = dt.Rows[i]["txthargaPO"].ToString();
                        txtTotal.Text = dt.Rows[i]["txtTotal"].ToString();
                    }
                }
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        protected void grdPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPO.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void loadPajakPPh()
        {
            string persenPPn = "0", persenPPh = "0";
            string sql = "select isnull(persenPajak,0) as persenPajak, isnull(pph,0) as persenPPh from Parameter";
            DataSet mySet = ObjDb.GetRows(sql);
            if (mySet.Tables[0].Rows.Count > 0)
            {
                persenPPn = mySet.Tables[0].Rows[0]["persenPajak"].ToString();
                persenPPh = mySet.Tables[0].Rows[0]["persenPPh"].ToString();
            }

            lblPersenPPn.Text = persenPPn;
            hdnPersenPajak.Value = persenPPn;
            hdnPersenPPh.Value = persenPPh;

        }

        protected void showHidePPnPPh(bool DivPPn, bool DivPPh)
        {
            showPajak.Visible = DivPPn;
            showPPh.Visible = DivPPh;

        }

        protected void showDetilTotal(bool DivDetil1, bool DivDetil2)
        {
            showDetil.Visible = DivDetil1;
            showDetil2.Visible = DivDetil2;
        }
        protected void grdPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = grdPO.SelectedRow.RowIndex;
                string id = grdPO.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = id;

                DataSet mySet = ObjDb.GetRows("select a.*,b.namaSupplier from TransPO_H a inner join mSupplier b on a.nosup=b.nosupplier where noPO = '" + hdnId.Value + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];

                txtKodePo.Text = myRow["kodePO"].ToString();
                txtSupplier.Text = myRow["namaSupplier"].ToString();
                dtKas.Text = Convert.ToDateTime(myRow["tglPO"]).ToString("dd-MMM-yyyy");
                hdntipePO.Value = myRow["tipePO"].ToString();
                SetInitialRow(hdnId.Value, hdntipePO.Value);
                txtKeterangan.Text = myRow["Keterangan"].ToString();
                showHideFormKas(false, true);

                if (hdntipePO.Value == "3")
                {
                    cboPPh.Text = myRow["stsPPh"].ToString();
                    showHidePPnPPh(false, true);
                }
                else
                {
                    cboPajak.Text = myRow["stsPPn"].ToString();
                    showHidePPnPPh(true, false);
                }

                loadPajakPPh();

                if (hdntipePO.Value != "3")
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "CalculatePPn()", "CalculatePPn();", true);
                else
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "CalculatePPh()", "CalculatePPh();", true);

                if (hdntipePO.Value != "3")
                    showDetilTotal(true, true);
                else
                    showDetilTotal(false, false);


                grdPODetil.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
                showHideFormKas(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void grdPOView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detail")
            {
                try
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    HiddenField hdnIdPrint = (HiddenField)grdPO.Rows[rowIndex].FindControl("hdnIdPrint");
                    HttpContext.Current.Session["ParamReport"] = null;
                    Session["REPORTNAME"] = null;
                    Session["REPORTTITLE"] = null;
                    Param.Clear();
                    Param.Add("noPO", hdnIdPrint.Value);
                    HttpContext.Current.Session.Add("ParamReport", Param);
                    Session["REPORTNAME"] = "ReportPO.rpt";
                    Session["REPORTTILE"] = "Report PO";
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            showHideFormKas(true, false);
        }

        protected void grdPODetil_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (hdntipePO.Value == "1")
                {
                    e.Row.Cells[1].Text = "Kode Barang";
                    e.Row.Cells[2].Text = "Nama Barang";
                    e.Row.Cells[3].Text = "Qty";
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[5].Text = "Harga Satuan";
                    e.Row.Cells[7].Visible = true;
                }
                else if (hdntipePO.Value == "3")
                {
                    e.Row.Cells[1].Text = "Kode Jasa";
                    e.Row.Cells[2].Text = "Nama Jasa";
                    e.Row.Cells[3].Text = "Nilai";
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Text = "Nilai Input";
                    e.Row.Cells[7].Visible = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (hdntipePO.Value == "1")
                {
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[7].Visible = true;
                }
                else if (hdntipePO.Value == "3")
                {
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[7].Visible = false;
                }
            }
        }
    }
}