﻿using System;
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
    public partial class RincomeStatement1 : System.Web.UI.Page
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
                loadCombo();
                cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();
                btnExport.Visible = false;
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
                            cmd = new SqlCommand("SPviewRRugilaba", con);
                            cmd.Parameters.Add(new SqlParameter("@thn", cboYear.Text));
                            cmd.Parameters.Add(new SqlParameter("@bln", cboMonth.Text));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboUnit.Text));
                            cmd.Parameters.Add(new SqlParameter("@jns", cboreport.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "Aktivitas_" + ObjSys.GetNow + ".xls";
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

        #region LoadData
 

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData(cboUnit.Text);
            btnExport.Visible = true;

        }
        protected void LoadIndexYayasan()
        {
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
            DataRow myRow = mySet.Tables[0].Rows[0];
            hdnnoYysn.Value = myRow["noCabang"].ToString();
        }
        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataUnit(cboCabang.Text);
            //LoadData(cboPerwakilan.Text, cboCabang.Text);
        }

        protected void loadData(string unit = "0")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", cboYear.Text);
            ObjGlobal.Param.Add("bln", cboMonth.Text);
            ObjGlobal.Param.Add("jns", cboreport.Text);
            ObjGlobal.Param.Add("nocabang", unit);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPviewRRugilaba", ObjGlobal.Param);
            grdAccount.DataBind();
        }

        protected void loadCombo()
        {
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Perwakilan' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and stspusat=0 and stscabang=1) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and noCabang=" + ObjSys.GetCabangId + " and stspusat=0 and stscabang=1) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where noCabang=" + ObjSys.GetParentCabang + " and stspusat=0 and stscabang=1) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where noCabang=" + ObjSys.GetParentCabang + " and stspusat=0 and stscabang=1) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where  stspusat=0 and stscabang=1) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            loadDataUnit(cboCabang.Text);

        }
        protected void loadDataUnit(string perwakilan = "0")
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", perwakilan);
            cboUnit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitPerwakilan1", ObjGlobal.Param);
            cboUnit.DataValueField = "id";
            cboUnit.DataTextField = "name";
            cboUnit.DataBind();


        }

        #endregion

    }
}
