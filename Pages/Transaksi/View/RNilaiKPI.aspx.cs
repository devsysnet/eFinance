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
    public partial class RNilaiKPI : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //loadDataCombo();
                LoadIndexYayasan();
                loadCombo();
                LoadData(cboPerwakilan.Text, cboCabang.Text);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();

            LoadData(cboPerwakilan.Text, cboCabang.Text);


        }
        protected void LoadIndexYayasan()
        {
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
            DataRow myRow = mySet.Tables[0].Rows[0];
            hdnnoYysn.Value = myRow["noCabang"].ToString();
        }
        protected void LoadData(string perwakilan = "0", string unit = "0")
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("perwakilan", perwakilan);
            ObjGlobal.Param.Add("nocabang", unit);
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            grdnilaikpi.DataSource = ObjGlobal.GetDataProcedure("sploadasoalkpi", ObjGlobal.Param);
            grdnilaikpi.DataBind();
            //grdHarianGL.DataSource = ObjDb.GetRowsDataTable("select t.kdRek,d.ket,e.namaCabang, t.tgl, t.Nilai from tSaldokas t inner join mRekening d on t.kdRek = d.kdRek inner join mCabang e on t.noCabang = e.noCabang inner join (select kdrek, max(tgl) as MaxDate from tSaldokas where nocabang='" + cboCabang.Text + "' group by kdrek) tm on t.kdrek = tm.kdrek and t.tgl = tm.MaxDate where t.nocabang='" + cboCabang.Text + "'");
            //grdHarianGL.DataBind();


        }
        protected void grdSiswa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = grdnilaikpi.SelectedRow.RowIndex;
                string noSiswa = grdnilaikpi.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noSiswa;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noUjian", hdnId.Value);
 
                grddetailnilaikpi.DataSource = ObjGlobal.GetDataProcedure("spLoadDataNilaiKPIDetail", ObjGlobal.Param);
                grddetailnilaikpi.DataBind();

 
     
                this.ShowHideGridAndForm(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false);
            LoadData(cboPerwakilan.Text,cboCabang.Text);
        }
        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataUnit(cboPerwakilan.Text);
            //LoadData(cboPerwakilan.Text, cboCabang.Text);
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        protected void loadCombo()
        {
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Perwakilan' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and noCabang=" + ObjSys.GetCabangId + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where noCabang=" + ObjSys.GetParentCabang + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "3")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where noCabang=" + ObjSys.GetParentCabang + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where  stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            loadDataUnit(cboPerwakilan.Text);
        }
        protected void loadDataUnit(string perwakilan = "0")
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", perwakilan);
            cboCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitPerwakilan1", ObjGlobal.Param);
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();


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

        protected void grdnilaikpi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdnilaikpi.PageIndex = e.NewPageIndex;
            //LoadData();
        }


    }
}
