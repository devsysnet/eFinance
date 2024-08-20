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
    public partial class kesanggupansiswa : System.Web.UI.Page
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
            cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadTahunajaran");
            cboYear.DataValueField = "id";
            cboYear.DataTextField = "name";
            cboYear.DataBind();

        }
    }


    #region LoadData
    protected void btnPosting_Click(object sender, EventArgs e)
    {
        ObjGlobal.Param.Clear();
        ObjGlobal.Param.Add("thn", cboYear.Text);
        ObjGlobal.Param.Add("kelas", cboKelas.Text);
        ObjGlobal.Param.Add("jenis", cboJnsTrans.Text);
        ObjGlobal.Param.Add("nocabang", cboCabang.Text);
        grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPARSiswa", ObjGlobal.Param);
        grdAccount.DataBind();

    }

    protected void loadDataCombo()
    {
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stsPusat = 0 and stsCabang = 2) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and noCabang = '" + ObjSys.GetCabangId + "' " +
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

            cboKelas.DataSource = ObjDb.GetRows("select * from (select '0' as id, 'ALL' as name union select distinct kelas as id, kelas as name from TransKelas where nocabang='" + ObjSys.GetCabangId + "')x");
            cboKelas.DataValueField = "id";
            cboKelas.DataTextField = "name";
            cboKelas.DataBind();

            cboJnsTrans.DataSource = ObjDb.GetRows("select * from (select '' as id, '-Pilih Transaksi-' as name union select distinct noTransaksi as id, jenisTransaksi as name from mJenisTransaksi where nocabang='" + ObjSys.GetCabangId + "')x");
            cboJnsTrans.DataValueField = "id";
            cboJnsTrans.DataTextField = "name";
            cboJnsTrans.DataBind();
        }
        #endregion
        //protected void btnPrint_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        HttpContext.Current.Session["ParamReport"] = null;
        //        Session["REPORTNAME"] = null;
        //        Session["REPORTTITLE"] = null;
        //        Param.Clear();
        //        Param.Add("bln", cboMonth.Text);
        //        Param.Add("thn", cboYear.Text);
        //        Param.Add("sts", cbosts.Text);
        //        Param.Add("nocabang", cboCabang.Text);
        //        HttpContext.Current.Session.Add("ParamReport", Param);
        //        Session["REPORTNAME"] = "ARSiswaPrint.rpt";
        //        Session["REPORTTILE"] = "Report AR Siswa";
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);

        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("Error:" + ex.ToString());
        //        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
        //    }
        //}
        protected void btnCari_Click(object sender, EventArgs e)
    {
        //LoadDataGrid();
    }

}
}