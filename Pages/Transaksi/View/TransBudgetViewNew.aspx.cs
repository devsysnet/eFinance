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
    public partial class TransBudgetViewNew : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();

                DataSet mySet = ObjDb.GetRows("select systembudget from parameter ");
                DataRow myRow = mySet.Tables[0].Rows[0];
                string systembudget = myRow["systembudget"].ToString();

                if (systembudget == "Tahun Ajaran")
                {
                    cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPpilihanthn");
                    cboYear.DataValueField = "id";
                    cboYear.DataTextField = "name";
                    cboYear.DataBind();
                    loadData(cboYear.Text, cboCabang.Text);
                }
                else if (systembudget == "Tahunan")
                {
                    cboYear1.DataSource = ObjDb.GetRows("select a.date id, a.date name from (select year(GETDATE()) date union all select year(GETDATE()) - 3 date union all select year(GETDATE()) - 2 date union all select year(GETDATE()) - 1 date union all select year(GETDATE()) + 1 date ) a");
                    cboYear1.DataValueField = "id";
                    cboYear1.DataTextField = "name";
                    cboYear1.DataBind();
                    loadData(cboYear1.Text, cboCabang.Text);
                }
                else
                {
                    cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPpilihanthn");
                    cboYear.DataValueField = "id";
                    cboYear.DataTextField = "name";
                    cboYear.DataBind();
                    loadData(cboYear.Text, cboCabang.Text);
                }
            }
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
                            cmd = new SqlCommand("ViewRBudgetnew", con);
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
                            cmd.Parameters.Add(new SqlParameter("@thn", cboYear.Text));
                            

                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            //int nokolom = 1;
                            // creating an array 
                    
                                int[] a_array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13,14,15 };

                            foreach (DataColumn column in dt.Columns)
                            //foreach (int column in a_array)
                            {
                                //Add the Header row for CSV file.
                                csv += column.ColumnName + ',';
                                //csv += "column" + nokolom++ + ',';
                            }
                            //Remove comma in last
                            csv = csv.TrimEnd(',');

                            //Add new line.
                            csv += "\r\n";

                            foreach (DataRow row in dt.Rows)
                            {
                                int no = 1;
                                string comma = "";
                                int number = dt.Columns.Count;
                                while (number < 16)
                                {
                                    number = number + 1;
                                    comma += ',';
                                }
                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Data rows.
                                    no++;
                                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                }

                                //Remove comma in last
                                csv = csv.TrimEnd(',');

                                csv = csv + "" + comma;

                                //Add new line.
                                csv += "\r\n";
                            }

                            //Download the CSV file.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition", "attachment;filename=Budget " + ObjSys.GetNow + ".csv");
                            Response.Charset = "";
                            Response.ContentType = "application/text";
                            Response.Output.Write(csv);
                            Response.Flush();
                            Response.End();

                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            }
        }

        protected void loadDataCombo()
        {
              //Perwakilan Mardi Yuana Bogor GetstsPusat=1
            if (ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from ( SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4) and parent = " + ObjSys.GetCabangId + ") a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //unit
            else if (ObjSys.GetstsPusat == "2")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //admin Kantor Perwakilan Bogor GetstsPusat=3
            else if (ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (  SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4) and parent = " + ObjSys.GetParentCabang + ") a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4)) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

        }

        protected void loadData(string tahun = "", string cabang = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", tahun);
            ObjGlobal.Param.Add("nocabang", cabang);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("ViewRBudgetnew", ObjGlobal.Param);
            grdAccount.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", tahun);
            ObjGlobal.Param.Add("nocabang", cabang);
            GridView1.DataSource = ObjGlobal.GetDataProcedure("ViewRBudgetnew", ObjGlobal.Param);
            GridView1.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", tahun);
            ObjGlobal.Param.Add("nocabang", cabang);
            GridView2.DataSource = ObjGlobal.GetDataProcedure("ViewRBudgetnew", ObjGlobal.Param);
            GridView2.DataBind();


            DataSet mySet = ObjDb.GetRows("select systembudget from parameter ");
            DataRow myRow = mySet.Tables[0].Rows[0];
            string systembudget = myRow["systembudget"].ToString();

            if (systembudget == "Tahun Ajaran")
            {
                pajak.Visible = false;
                tahunAjaran.Visible = true;
                tahunan.Visible = false;
                divYear1.Visible = false;
                divYear.Visible = true;
                btnprintPajak.Visible = false;
                btnprintTahunAjaran.Visible = true;
            }
            else if (systembudget == "Tahunan")
            {
                pajak.Visible = false;
                tahunAjaran.Visible = false;
                tahunan.Visible = true;
                divYear1.Visible = true;
                divYear.Visible = false;
                btnprintPajak.Visible = false;
                btnprintTahunAjaran.Visible = false;
            }
            else
            {
                pajak.Visible = true;
                tahunAjaran.Visible = false;
                tahunan.Visible = false;
                divYear1.Visible = false;
                divYear.Visible = true;
                btnprintPajak.Visible = true;
                btnprintTahunAjaran.Visible = false;
            }
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(cboYear.Text, cboCabang.Text);
        }

        protected void cboYear1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(cboYear1.Text, cboCabang.Text);
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(cboYear.Text, cboCabang.Text);
        }
        protected void printPajak(object sender, EventArgs e)
        {
            HttpContext.Current.Session["ParamReport"] = null;
            Session["REPORTNAME"] = null;
            Session["REPORTTITLE"] = null;
            Param.Clear();
            Param.Add("thn", cboYear.Text);
            Param.Add("nocabang", cboCabang.Text);

            HttpContext.Current.Session.Add("ParamReport", Param);
            Session["REPORTNAME"] = "RbudgetPajak.rpt";
            Session["REPORTTILE"] = "Report Budget Pajak";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);
        }
        protected void printTahunAjaran(object sender, EventArgs e)
        {
            HttpContext.Current.Session["ParamReport"] = null;
            Session["REPORTNAME"] = null;
            Session["REPORTTITLE"] = null;
            Param.Clear();
            Param.Add("thn", cboYear.Text);
            Param.Add("nocabang", cboCabang.Text);

            HttpContext.Current.Session.Add("ParamReport", Param);
            Session["REPORTNAME"] = "RbudgetTahunAjaran.rpt";
            Session["REPORTTILE"] = "Report Budget Tahun Ajaran";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);
        }
    }
}
