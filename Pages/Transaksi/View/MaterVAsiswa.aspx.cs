using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Collections;
using eFinance.GlobalApp;


namespace eFinance.Pages.Transaksi.View
{
    public partial class MaterVAsiswa : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataComboFirst();
                ShowHide(false, true);
            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("kategori", cboStatus.Text);
            ObjGlobal.Param.Add("kelas", cboKelas.Text);
            grdARSiswa.DataSource = ObjGlobal.GetDataProcedure("SPViewpVAsiswa", ObjGlobal.Param);
            grdARSiswa.DataBind();

        }
        protected void print(object sender, EventArgs e)
        {
            HttpContext.Current.Session["ParamReport"] = null;
            Session["REPORTNAME"] = null;
            Session["REPORTTITLE"] = null;
            Param.Clear();
            Param.Add("kelas", cboKelas.Text);
            Param.Add("kategori", cboStatus.Text);
            Param.Add("nocabang", cboCabang.Text);

            HttpContext.Current.Session.Add("ParamReport", Param);
            Session["REPORTNAME"] = "RFormSuratPembayaranVA.rpt";
            Session["REPORTTILE"] = "Form Surat Pembayaran VA";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);
        }
        protected void grdARSiswa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectEdit")
            {
                try
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    HiddenField hdnIdPrint = (HiddenField)grdARSiswa.Rows[rowIndex].FindControl("hdnIdPrint");

                    HttpContext.Current.Session["ParamReport"] = null;
                    Session["REPORTNAME"] = null;
                    Session["REPORTTITLE"] = null;
                    Param.Clear();
                    Param.Add("noSiswa", hdnIdPrint.Value);
                    Param.Add("nocabang", cboCabang.Text);

                    HttpContext.Current.Session.Add("ParamReport", Param);
                    Session["REPORTNAME"] = "RFormSuratPembayaranVA.rpt";
                    Session["REPORTTILE"] = "Form Surat Pembayaran VA";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);



                }
                catch (Exception ex)
                {
                    Response.Write("Error:" + ex.ToString());
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
                }
            }
        }
        protected void ShowHide(Boolean tgl, Boolean period)
        {
            //showhidePeriod.Visible = period;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
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
                            cmd = new SqlCommand("SPViewpVAsiswa", con);
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
                            cmd.Parameters.Add(new SqlParameter("@kategori", cboStatus.Text));
                            cmd.Parameters.Add(new SqlParameter("@kelas", cboKelas.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "Posisi_keuangan_" + ObjSys.GetNow + ".xls";
                            ViewHelper.DownloadExcel(Response, fileName, dt);
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            //ShowMessage("error", ex.ToString());
                        }

                    }
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

        protected void grdARSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdARSiswa.PageIndex = e.NewPageIndex;
            loadData();
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
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            showHideFormKas(true, false);
        }

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPrint.Visible = true;

            loadData();
        }

        private void loadDataComboFirst()
        {
            if (ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Unit---' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE stsCabang = 2 and parent = '" + ObjSys.GetParentCabang + "') a order by a.noUrut");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsPusat == "2")
            {
                cboCabang.DataSource = ObjDb.GetRows("SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsCabang = 2 and noCabang = '" + ObjSys.GetCabangId + "'");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Unit---' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE stsCabang = 2) a order by a.noUrut");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

            loadDataKelas(cboCabang.Text);
            loadDataJnsTransaksi(cboCabang.Text);
            loadDataPeriode(cboCabang.Text);
        }

        protected void loadDataKelas(string cboCabang = "")
        {
            if (cboCabang == "0")
            {
                cboKelas.DataSource = ObjDb.GetRows("select * from (select '' as id, '-Pilih Kelas-' as name)x");
                cboKelas.DataValueField = "id";
                cboKelas.DataTextField = "name";
                cboKelas.DataBind();
            }
            else
            {
                cboKelas.DataSource = ObjDb.GetRows("select * from (select '' as id, '-Pilih Kelas-' as name union all select distinct kelas as id, kelas as name from TransKelas where nocabang = '" + cboCabang + "')x");
                cboKelas.DataValueField = "id";
                cboKelas.DataTextField = "name";
                cboKelas.DataBind();
            }

        }

        protected void loadDataJnsTransaksi(string cboCabang = "")
        {
           
        }

        protected void loadDataPeriode(string cboCabang = "")
        {
          
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataKelas(cboCabang.Text);
            loadDataJnsTransaksi(cboCabang.Text);
            loadDataPeriode(cboCabang.Text);
            loadData();
        }

        protected void cboPeriode_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void cboKelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPrint.Visible = true;
            loadData();
        }

        protected void cboKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }
    }
}