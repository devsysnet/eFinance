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
    public partial class TransDetailPembayaranView : System.Web.UI.Page
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
                //divtombol.Visible = false;
            }
        }

        #region LoadData
        protected void LoadDataGrid()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("year", cboYear.Text);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPViewDetailPembayaran", ObjGlobal.Param);
            grdAccount.DataBind();
            //divtombol.Visible = true;
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
                        ObjGlobal.Param.Add("year", cboYear.Text);
                        DataSet dataSet = ObjGlobal.GetDataProcedure("SPViewDetailPembayaran", ObjGlobal.Param);


                        String fileName = "rekap_tagihan_perThn" + ObjSys.GetNow + ".xls";
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




        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }


        #endregion

        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
        }


        protected void grdAccount_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();

            cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = "";
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Tagihan";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Pembayaran";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 1;
            cell.Text = "";
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            grdAccount.HeaderRow.Parent.Controls.AddAt(0, row);
        }
    }
}