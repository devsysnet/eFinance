using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace eFinance.Pages.Transaksi.Posting
{
    public partial class PostingbulananGL : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                LoadDataCombo();
            }
        }

        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }

        protected void LoadData(string cabang = "0")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            cboYear.DataSource = ObjGlobal.GetDataProcedure("SPLoaddataposthn", ObjGlobal.Param);
            cboYear.DataValueField = "tgl";
            cboYear.DataTextField = "bulan";
            cboYear.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            cbobln.DataSource = ObjGlobal.GetDataProcedure("SPLoaddataposbln", ObjGlobal.Param);
            cbobln.DataValueField = "tgl";
            cbobln.DataTextField = "bulan";
            cbobln.DataBind();
        }
        protected void LoadDataCombo()
        {
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
            else if (ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4)) a");
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
        }
        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            string cabang = cboCabang.Text;
            LoadData(cabang);
        }
        #region 

        protected void btnPosting_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (valid == true)
            {

                if (ObjDb.GetRows("select distinct sts from tkasdetil where nocabang='" + cboCabang.Text + "' and sts=0 and month(tgl)='" + cbobln.Text + "' and year(tgl)='" + cboYear.Text + "' ").Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Masih ada Data yang Belum Posting Harian GL");
                }
              
                else
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("thn", cboYear.Text);
                    ObjGlobal.Param.Add("bln", cbobln.Text);
                    ObjGlobal.Param.Add("nocabang", cboCabang.Text);
                    ObjGlobal.ExecuteProcedure("SPProsesBulananGL", ObjGlobal.Param);
                   
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data Berhasil Di Posting");
                    LoadDataCombo();
                    LoadData();

                }
            }


            #endregion
        }

    }
}