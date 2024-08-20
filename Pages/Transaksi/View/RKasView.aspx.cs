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
    public partial class RKasView : System.Web.UI.Page
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
                divtombol.Visible = false;
            }
        }

        protected void loadData()
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("tahun", cboYear.Text);
            //ObjGlobal.Param.Add("jnsbank", ObjSys.Getjnsbank);
            grdHarianGL.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataKasView", ObjGlobal.Param);
            grdHarianGL.DataBind();
            divtombol.Visible = true;

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
                            cmd = new SqlCommand("SPLoadDataKasView", con);
                            cmd.Parameters.Add(new SqlParameter("@tahun", cboYear.Text));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            //int nokolom = 1;
                            // creating an array 
                            int[] a_array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8,9,10,11 };
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
                                while (number < 11)
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
                            Response.AddHeader("content-disposition", "attachment;filename=Buku_Kas " + ObjSys.GetNow + ".csv");
                            Response.Charset = "";
                            Response.ContentType = "application/text";
                            Response.Output.Write(csv);
                            Response.Flush();
                            Response.End();

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

            //Perwakilan Mardi Yuana Bogor GetstsPusat=1
            if (ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4) and parent = " + ObjSys.GetCabangId + ") a");
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
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4) and parent = " + ObjSys.GetParentCabang + ") a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4)) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            LoadDataAccount(cboCabang.Text);


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

        protected void grdHarianGL_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdHarianGL.PageIndex = e.NewPageIndex;
            //LoadData();
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataAccount(cboCabang.Text);
        }

        protected void LoadDataAccount(string cabang)
        {

          

        }

        protected void btnPosting_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void grdHarianGL_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "SelectHarian")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                HiddenField hdnbulan = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnbulan");
                HiddenField hdnbln = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnbln");
                HiddenField hdnthn = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnthn");
                HiddenField hdnnorek = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnnorek");
                HiddenField hdnnocabang = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnnocabang");

                DataSet mySetRek = ObjDb.GetRows("SELECT ket FROM mRekening where norek = " + hdnnorek.Value + " ");
                DataRow myRowRek = mySetRek.Tables[0].Rows[0];
                string ket = myRowRek["ket"].ToString();

                DataSet mySetUnit = ObjDb.GetRows("SELECT namaCabang FROM mCabang where nocabang = " + hdnnocabang.Value + " ");
                DataRow myRowUnit = mySetUnit.Tables[0].Rows[0];
                string unit = myRowUnit["namaCabang"].ToString();

                lblbln.Text = hdnbulan.Value;
                lblthn.Text = hdnthn.Value;
                lblrek.Text = ket;
                lblunit.Text = unit;
                hdnblnHarianKas.Value = hdnbln.Value;
                hdnthnHarianKas.Value = hdnthn.Value;
                hdnnorekHarianKas.Value = hdnnorek.Value;
                hdnnocabangHarianKas.Value = hdnnocabang.Value;
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("norek", hdnnorek.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_BukuHarian", ObjGlobal.Param);
                grdAccount.DataBind();

                dlgHarianKas.Show();

            }
            if (e.CommandName == "SelectDetail")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                HiddenField hdnbulan = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnbulan");
                HiddenField hdnbln = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnbln");
                HiddenField hdnthn = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnthn");
                HiddenField hdnnorek = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnnorek");
                HiddenField hdnnocabang = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnnocabang");

                DataSet mySetUnit = ObjDb.GetRows("SELECT namaCabang FROM mCabang where nocabang = " + hdnnocabang.Value + " ");
                DataRow myRowUnit = mySetUnit.Tables[0].Rows[0];
                string unit = myRowUnit["namaCabang"].ToString();

                lblbln2.Text = hdnbulan.Value;
                lblthn2.Text = hdnthn.Value;
                lblunit2.Text = unit;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                GridView1.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_DetilCashPendapatan", ObjGlobal.Param);
                GridView1.DataBind();


                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                grdAccount1.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_DetilCashPengeluaran", ObjGlobal.Param);
                grdAccount1.DataBind();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                grdAccount2.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_DetilCashRKMasuk", ObjGlobal.Param);
                grdAccount2.DataBind();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                grdAccount3.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_DetilCashRKKeluar", ObjGlobal.Param);
                grdAccount3.DataBind();

                dlgDetilKas.Show();
            }

            if (e.CommandName == "SelectDetailkas")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                HiddenField hdnbulan = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnbulan");
                HiddenField hdnbln = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnbln");
                HiddenField hdnthn = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnthn");
                HiddenField hdnnorek = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnnorek");
                HiddenField hdnnocabang = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnnocabang");


                DataSet mySetUnit = ObjDb.GetRows("SELECT namaCabang FROM mCabang where nocabang = " + hdnnocabang.Value + " ");
                DataRow myRowUnit = mySetUnit.Tables[0].Rows[0];
                string unit = myRowUnit["namaCabang"].ToString();

                Label3.Text = hdnbulan.Value;
                Label4.Text = hdnthn.Value;
                Label6.Text = unit;


                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("norek", hdnnorek.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                GridView2.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_DetilPenerimaan", ObjGlobal.Param);
                GridView2.DataBind();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("norek", hdnnorek.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                GridView3.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_DetilPengeluaran", ObjGlobal.Param);
                GridView3.DataBind();

                grddeatailbln.Show();
            }
        }

    }
}
