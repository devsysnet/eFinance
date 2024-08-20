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
namespace eFinance.Pages.Transaksi.View
{
    public partial class Trackingapproveanggaran : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadIndexYayasan();
                loadDataCombo();
              
                LoadData(cboPerwakilan.Text);
            }
        }

        protected void LoadIndexYayasan()
        {
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
            DataRow myRow = mySet.Tables[0].Rows[0];
            hdnnoYysn.Value = myRow["noCabang"].ToString();
        }

        protected void LoadData(string perwakilan = "0")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("perwakilan", perwakilan);
            grdSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDTrackingAppbudget", ObjGlobal.Param);
            grdSiswa.DataBind();

        }

        protected void loadDataCombo()
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
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where stspusat=0 and stscabang=1) a");
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
          
        }

       
      

        protected void grdSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdSiswa.PageIndex = e.NewPageIndex;
            LoadData(cboPerwakilan.Text);
        }

    
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            //tabForm.Visible = DivForm;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false);
            LoadData(cboPerwakilan.Text);
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadDataComboKelas();
        }

        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            LoadData(cboPerwakilan.Text);
            showhidedropdown(false);
        }

      

      
        protected void showhidedropdown(bool showhideclass)
        {
            //divclass.Visible = showhideclass;
        }

        protected void grdSiswa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnJmlTunggakan = (HiddenField)e.Row.FindControl("hdnJmlTunggakan");
                Label lblnik = (Label)e.Row.FindControl("lblnik");
                Label lblnis = (Label)e.Row.FindControl("lblnis");
                Label lblnisn = (Label)e.Row.FindControl("lblnisn");
                Label lblNamaSiswa = (Label)e.Row.FindControl("lblNamaSiswa");
                Label lblkelas = (Label)e.Row.FindControl("lblkelas");
                Label lbllokasiUnit = (Label)e.Row.FindControl("lbllokasiUnit");

                //if (hdnJmlTunggakan.Value != "0")
                //{
                //    lblnik.ForeColor = Color.Red;
                //    lblnis.ForeColor = Color.Red;
                //    lblnisn.ForeColor = Color.Red;
                //    lblNamaSiswa.ForeColor = Color.Red;
                //    lblkelas.ForeColor = Color.Red;
                //    lbllokasiUnit.ForeColor = Color.Red;
                //}
                //else
                //{
                //    lblnik.ForeColor = Color.Gray;
                //    lblnis.ForeColor = Color.Gray;
                //    lblnisn.ForeColor = Color.Gray;
                //    lblNamaSiswa.ForeColor = Color.Gray;
                //    lblkelas.ForeColor = Color.Gray;
                //    lbllokasiUnit.ForeColor = Color.Gray;
                //}
            }
        }


    }
}