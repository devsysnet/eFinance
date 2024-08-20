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
using System.Web.UI.DataVisualization.Charting;

namespace eFinance.Pages.Transaksi.View
{
    public partial class RBudgetvsRealisasiTable : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPpilihanthn");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();
                loadDataCombo();

                //divtombol.Visible = false;


                DataSet mySet1 = ObjDb.GetRows("select systembudget from parameter ");
                DataRow myRow1 = mySet1.Tables[0].Rows[0];
                string systembudget = myRow1["systembudget"].ToString();

                if (systembudget == "Tahun Ajaran")
                {
                    pajak.Visible = false;
                    //tahunAjaran.Visible = true;
                }
                else
                {
                    pajak.Visible = true;
                    //tahunAjaran.Visible = false;
                }
            }
        }

        #region LoadData
        protected void LoadDataGrid()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", cboYear.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPbudgetvsrealisasi1", ObjGlobal.Param);
            grdAccount.DataBind();

            //ObjGlobal.Param.Clear();
            //ObjGlobal.Param.Add("thn", cboYear.Text);
            //ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            //GridView1.DataSource = ObjGlobal.GetDataProcedure("SPbudgetvsrealisasi1", ObjGlobal.Param);
            //GridView1.DataBind();
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
                    using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                    {

                        SqlCommand cmd = new SqlCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        DataTable dt = new DataTable();
                        try
                        {
                            cmd = new SqlCommand("SPbudgetvsrealisasi1", con);
                            cmd.Parameters.Add(new SqlParameter("@thn", cboYear.Text));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "Report_budget" + ObjSys.GetNow + ".xls";
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

        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }


        #endregion

        protected void btnCari_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", cboYear.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPbudgetvsrealisasi1", ObjGlobal.Param);
            grdAccount.DataBind();

            //ObjGlobal.Param.Clear();
            //ObjGlobal.Param.Add("thn", cboYear.Text);
            //ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            //GridView1.DataSource = ObjGlobal.GetDataProcedure("SPbudgetvsrealisasi1", ObjGlobal.Param);
            //GridView1.DataBind();
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
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            string cabang = Request.QueryString.Get("cabang");
            string tahun = Request.QueryString.Get("tahun");

            if (cabang != null && cabang != "")
            {
                cboCabang.SelectedValue = cabang;
                cboYear.SelectedValue = tahun;
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("thn", cboYear.SelectedValue);
                ObjGlobal.Param.Add("nocabang", cboCabang.SelectedValue);
                grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPbudgetvsrealisasi1", ObjGlobal.Param);
                grdAccount.DataBind();

                //ObjGlobal.Param.Clear();
                //ObjGlobal.Param.Add("thn", cboYear.SelectedValue);
                //ObjGlobal.Param.Add("nocabang", cboCabang.SelectedValue);
                //GridView1.DataSource = ObjGlobal.GetDataProcedure("SPbudgetvsrealisasi1", ObjGlobal.Param);
                //GridView1.DataBind();
            }


        }


    
      
        //protected void GridView1_DataBound(object sender, EventArgs e)
        //{
        //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        //    TableHeaderCell cell = new TableHeaderCell();

      
        //        cell = new TableHeaderCell();
        //        cell.ColumnSpan = 2;
        //        cell.Text = "";
        //        cell.BorderWidth = 1;
        //        row.Controls.Add(cell);
        //        cell = new TableHeaderCell();
        //        cell.Text = "Juli";
        //        cell.CssClass = "text-center";
        //        cell.ColumnSpan = 2;
        //        cell.BorderWidth = 1;
        //        row.Controls.Add(cell);

        //        cell = new TableHeaderCell();
        //        cell.Text = "Agustus";
        //        cell.CssClass = "text-center";
        //        cell.ColumnSpan = 2;
        //        cell.BorderWidth = 1;
        //        row.Controls.Add(cell);

        //        cell = new TableHeaderCell();
        //        cell.Text = "September";
        //        cell.CssClass = "text-center";
        //        cell.ColumnSpan = 2;
        //        cell.BorderWidth = 1;
        //        row.Controls.Add(cell);

        //        cell = new TableHeaderCell();
        //        cell.Text = "Oktober";
        //        cell.CssClass = "text-center";
        //        cell.ColumnSpan = 2;
        //        cell.BorderWidth = 1;
        //        row.Controls.Add(cell);

        //        cell = new TableHeaderCell();
        //        cell.Text = "November";
        //        cell.CssClass = "text-center";
        //        cell.ColumnSpan = 2;
        //        cell.BorderWidth = 1;
        //        row.Controls.Add(cell);

        //        cell = new TableHeaderCell();
        //        cell.Text = "Desmber";
        //        cell.CssClass = "text-center";
        //        cell.ColumnSpan = 2;
        //        cell.BorderWidth = 1;
        //        row.Controls.Add(cell);
        //        cell = new TableHeaderCell();
        //        cell.Text = "Januari";
        //        cell.CssClass = "text-center";
        //        cell.ColumnSpan = 2;
        //        cell.BorderWidth = 1;
        //        row.Controls.Add(cell);

        //        cell = new TableHeaderCell();
        //        cell.Text = "Februari";
        //        cell.CssClass = "text-center";
        //        cell.ColumnSpan = 2;
        //        cell.BorderWidth = 1;
        //        row.Controls.Add(cell);

        //        cell = new TableHeaderCell();
        //        cell.Text = "Maret";
        //        cell.CssClass = "text-center";
        //        cell.ColumnSpan = 2;
        //        cell.BorderWidth = 1;
        //        row.Controls.Add(cell);

        //        cell = new TableHeaderCell();
        //        cell.Text = "April";
        //        cell.CssClass = "text-center";
        //        cell.ColumnSpan = 2;
        //        cell.BorderWidth = 1;
        //        row.Controls.Add(cell);

        //        cell = new TableHeaderCell();
        //        cell.Text = "Mei";
        //        cell.CssClass = "text-center";
        //        cell.ColumnSpan = 2;
        //        cell.BorderWidth = 1;
        //        row.Controls.Add(cell);

        //        cell = new TableHeaderCell();
        //        cell.Text = "Juni";
        //        cell.CssClass = "text-center";
        //        cell.ColumnSpan = 2;
        //        cell.BorderWidth = 1;
        //        row.Controls.Add(cell);

          

        //    //GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
        //}

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
                cell.Text = "Januari";
                cell.CssClass = "text-center";
                cell.ColumnSpan = 2;
                cell.BorderWidth = 1;
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "Februari";
                cell.CssClass = "text-center";
                cell.ColumnSpan = 2;
                cell.BorderWidth = 1;
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "Maret";
                cell.CssClass = "text-center";
                cell.ColumnSpan = 2;
                cell.BorderWidth = 1;
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "April";
                cell.CssClass = "text-center";
                cell.ColumnSpan = 2;
                cell.BorderWidth = 1;
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "Mei";
                cell.CssClass = "text-center";
                cell.ColumnSpan = 2;
                cell.BorderWidth = 1;
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "Juni";
                cell.CssClass = "text-center";
                cell.ColumnSpan = 2;
                cell.BorderWidth = 1;
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "Juli";
                cell.CssClass = "text-center";
                cell.ColumnSpan = 2;
                cell.BorderWidth = 1;
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "Agustus";
                cell.CssClass = "text-center";
                cell.ColumnSpan = 2;
                cell.BorderWidth = 1;
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "September";
                cell.CssClass = "text-center";
                cell.ColumnSpan = 2;
                cell.BorderWidth = 1;
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "Oktober";
                cell.CssClass = "text-center";
                cell.ColumnSpan = 2;
                cell.BorderWidth = 1;
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "November";
                cell.CssClass = "text-center";
                cell.ColumnSpan = 2;
                cell.BorderWidth = 1;
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "Desmber";
                cell.CssClass = "text-center";
                cell.ColumnSpan = 2;
                cell.BorderWidth = 1;
                row.Controls.Add(cell);



            grdAccount.HeaderRow.Parent.Controls.AddAt(0, row);
        }
    }
}