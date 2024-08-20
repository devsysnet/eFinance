using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using eFinance.GlobalApp;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.View
{
    public partial class RpencapaianPenerimaan : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadIndexYayasan();
                loadDataCombo();
                //loadData(cboCabang.Text);
            }
        }
        protected void LoadIndexYayasan()
        {
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
            DataRow myRow = mySet.Tables[0].Rows[0];
            hdnYayasan.Value = myRow["noCabang"].ToString();
        }
        //protected void tabelDinamis(DataSet ds)
        //{
        //    // Initialize columns
        //    List<String> datacolumns = new List<String> {
        //        //"No",
        //        "namaCabang"
        //    };

        //    // Clear and add initial columns
        //    grdAccount.Columns.Clear();
        //    foreach (String datacolumn in datacolumns)
        //    {
        //        BoundField bfield = new BoundField();
        //        bfield.HeaderText = datacolumn;
        //        bfield.DataField = datacolumn;
        //        grdAccount.Columns.Add(bfield);
        //    }

        //    // Add new columns
        //    DataTable headerTable = ds.Tables[0];
        //    int headerLength = headerTable.Rows.Count;
        //    for (int i = 0; i < headerLength; i++)
        //    {
        //        String labelJenis = headerTable.Rows[i]["bln_thn"].ToString();
        //        String jenis = headerTable.Rows[i]["bln_thn"].ToString();
        //        BoundField bfield = new BoundField();
        //        bfield.HeaderText = jenis;
        //        bfield.DataField = jenis;
        //        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        //        grdAccount.Columns.Add(bfield);
        //    }


        //    // Fill data if exists
        //    if (ds.Tables.Count > 1)
        //    {
        //        DataTable dataTable = ds.Tables[1];
        //        grdAccount.DataSource = dataTable;
        //    }
        //    grdAccount.DataBind();
        //}

        #region LoadData
       
        protected void loadData(string unit = "0")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("perwakilan", cboCabang.Text);
            ObjGlobal.Param.Add("nocabang", unit);

            grdAccount.DataSource =  ObjGlobal.GetDataProcedure("SPLoadDataPencapaianPenerimaan", ObjGlobal.Param);
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
                        ObjGlobal.Param.Add("perwakilan", cboCabang.Text);
                        ObjGlobal.Param.Add("nocabang", cboUnit.Text);
                        DataSet dataSet = ObjGlobal.GetDataProcedure("SPLoadDataPencapaianPenerimaan", ObjGlobal.Param);


                        String fileName = "pencampaian_dan_penerimaan" + ObjSys.GetNow + ".xls";
                        ViewHelper.DownloadExcel(Response, fileName, dataSet.Tables[0]);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        //ShowMessage("error", ex.ToString());
                    }

                    //}
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
        protected void loadDataCombo()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", ObjSys.GetCabangId);
            //ObjGlobal.Param.Add("perwakilan", cboPerwakilan.Text);
            cboCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataKantorPerwakilan", ObjGlobal.Param);
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();
           
            
            LoadDataCombo3(cboCabang.Text);


        }


        #endregion
        protected void grdAccount_dataBound(object sender, EventArgs e)
        {

            grdAccount.HeaderRow.Cells[0].CssClass = "text-center";

            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();

            cell = new TableHeaderCell();
            cell.Text = "";
            cell.CssClass = "text-center";
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Juli";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Agustus";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "September";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Oktober";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "November";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Desember";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Januari";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Februari";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Maret";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "April";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Mei";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Juni";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);


            grdAccount.HeaderRow.Parent.Controls.AddAt(0, row);
        }
        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataCombo3(cboCabang.Text);
            grdAccount.HeaderRow.Cells[0].CssClass = "text-center";
            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "getAmount();", true);

            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();

            cell = new TableHeaderCell();
            cell.Text = "";
            cell.CssClass = "text-center";
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Juli";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Agustus";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "September";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Oktober";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "November";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Desember";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Januari";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Februari";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Maret";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "April";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Mei";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Juni";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

        }
        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(cboUnit.Text);
            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "getAmount();", true);


        }
        protected void Click(object sender, EventArgs e)
        {
            loadData(cboCabang.Text);
            grdAccount.HeaderRow.Cells[0].CssClass = "text-center";
            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "getAmount();", true);

            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();

            cell = new TableHeaderCell();
            cell.Text = "";
            cell.CssClass = "text-center";
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Juli";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Agustus";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "September";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Oktober";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "November";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Desember";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Januari";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Februari";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Maret";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "April";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Mei";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Juni";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 3;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

        }
        protected void LoadDataCombo3(string cabang = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", cabang);
            //ObjGlobal.Param.Add("perwakilan", cboPerwakilan.Text);
            cboUnit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitPerwakilan1", ObjGlobal.Param);
            cboUnit.DataValueField = "id";
            cboUnit.DataTextField = "name";
            cboUnit.DataBind();

        }
    }
}