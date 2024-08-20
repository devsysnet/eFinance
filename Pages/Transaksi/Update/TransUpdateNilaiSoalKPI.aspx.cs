using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransUpdateNilaiSoalKPI : System.Web.UI.Page
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

        private void SetInitialRow(string Id = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnnoujiand", typeof(string)));
            dt.Columns.Add(new DataColumn("soalkpi", typeof(string)));
            dt.Columns.Add(new DataColumn("nilaikpi", typeof(string)));

 
          
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noUjian", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("spLoadDataNilaiKPIDetail", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnnoujiand"] = myRow["noujiand"].ToString();
                dr["soalkpi"] = myRow["soalkpi"].ToString();
                dr["nilaikpi"] = myRow["nilai"].ToString();
                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnnoujiand"] = string.Empty;
                dr["soalkpi"] = string.Empty;
                dr["nilaikpi"] = string.Empty;
               
                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grddetailnilaikpi.DataSource = dt;
            grddetailnilaikpi.DataBind();

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
                        HiddenField hdnnoujiand = (HiddenField)grddetailnilaikpi.Rows[i].FindControl("hdnnoujiand");
                        Label soalkpi = (Label)grddetailnilaikpi.Rows[i].FindControl("soalkpi");
                        TextBox nilaikpi = (TextBox)grddetailnilaikpi.Rows[i].FindControl("nilaikpi");

                        nilaikpi.Text = dt.Rows[i]["nilaikpi"].ToString();
                        hdnnoujiand.Value = dt.Rows[i]["hdnnoujiand"].ToString();
                        soalkpi.Text = dt.Rows[i]["soalkpi"].ToString();
                        
                    }
                }
            }
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
 
                ObjDb.Data.Clear();
                ObjDb.Where.Clear();
                ObjDb.Where.Add("noujian", hdnId.Value);
                ObjDb.Data.Add("tgl", tgl.Text);
                ObjDb.Data.Add("nocabang", unit.Text);
                ObjDb.Data.Add("nokaryawan", karyawan.Text);
                ObjDb.Data.Add("nosoal", tipesoal.Text);
                ObjDb.Data.Add("deskripsi", keterangan.Text);
               
                ObjDb.Update("TranssoalKPI_H", ObjDb.Data, ObjDb.Where);

                for (int i = 0; i < grdnilaikpi.Rows.Count; i++)
                {
                  
                    HiddenField hdnnoujiand = (HiddenField)grdnilaikpi.Rows[i].FindControl("hdnnoujiand");
                    TextBox nilaikpi = (TextBox)grdnilaikpi.Rows[i].FindControl("nilaikpi");

                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noujiand", hdnnoujiand.Value);
                    ObjDb.Data.Add("nilai", nilaikpi.Text);
                
                    ObjDb.Update("TranssoalKPI_d", ObjDb.Data, ObjDb.Where);

                }



             
                ShowHideGridAndForm(true, false);
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil diperbaharui.");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }
        protected void LoadDataSoal()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nosoal", "");
            tipesoal.DataSource = ObjGlobal.GetDataProcedure("SPloadDataSoalKPIinput", ObjGlobal.Param);
            tipesoal.DataValueField = "id";
            tipesoal.DataTextField = "name";
            tipesoal.DataBind();


        }
        protected void LoadDataUnitt()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            unit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitNew", ObjGlobal.Param);
            unit.DataValueField = "id";
            unit.DataTextField = "name";
            unit.DataBind();

            LoadDataKaryawan(unit.Text);
        }
        protected void LoadDataKaryawan(string unit = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("perwakilan", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("cabang", unit);
            karyawan.DataSource = ObjGlobal.GetDataProcedure("SPloadDataKaryawanUnit", ObjGlobal.Param);
            karyawan.DataValueField = "id";
            karyawan.DataTextField = "name";
            karyawan.DataBind();
        }
        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataKaryawan(unit.Text);

        }
        protected void grdnilaikpi_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = grdnilaikpi.SelectedRow.RowIndex;
            string noujian = grdnilaikpi.DataKeys[rowIndex].Values[0].ToString();
            hdnId.Value = noujian;

            LoadDataUnitt();
            LoadDataKaryawan();

            LoadDataSoal();

            DataSet mySet = ObjDb.GetRows("select * from TranssoalKPI_H where noujian = '" + hdnId.Value + "'");
            DataRow myRow = mySet.Tables[0].Rows[0];

            tgl.Text = Convert.ToDateTime(myRow["tgl"]).ToString("dd-MMM-yyyy");
            unit.Text = myRow["nocabang"].ToString();
            karyawan.Text = myRow["nokaryawan"].ToString();
            tipesoal.Text = myRow["nosoal"].ToString();
            keterangan.Text = myRow["deskripsi"].ToString();

            SetInitialRow(hdnId.Value);

            this.ShowHideGridAndForm(false, true);
        }
        protected void grdnilaikpi_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                
                  if (e.CommandName == "delete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noujian = grddetailnilaikpi.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noujian;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noujian", noujian);
                    ObjDb.Delete("TranssoalKPI_d", ObjDb.Where);

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noujian", noujian);
                    ObjDb.Delete("TranssoalKPI_H", ObjDb.Where);

  
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus");
                    this.ShowHideGridAndForm(true, false);
                }
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
            LoadData(cboPerwakilan.Text, cboCabang.Text);
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
