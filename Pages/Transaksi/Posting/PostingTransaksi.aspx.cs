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
    public partial class PostingTransaksi : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadDataCombo();
                LoadData(cboCabang.Text);
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
            cbojenis.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataMJenisTransaksi", ObjGlobal.Param);
            cbojenis.DataValueField = "id";
            cbojenis.DataTextField = "name";
            cbojenis.DataBind();

            cbothnajaran.DataSource = ObjDb.GetRows("select * from (select '0' as id, '--Pilih Tahun Ajaran--' as name union select distinct tahunajaran as id, tahunajaran as name from TransKelas where sts=1 and nocabang=" + ObjSys.GetCabangId + ")x");
            cbothnajaran.DataValueField = "id";
            cbothnajaran.DataTextField = "name";
            cbothnajaran.DataBind();
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
            LoadData(cboCabang.Text);
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
                 ObjGlobal.Param.Clear();
            
                 ObjGlobal.Param.Add("nocabang", cboCabang.Text);
                 ObjGlobal.Param.Add("noTransaksi", cbojenis.Text);
                 ObjGlobal.Param.Add("tahunAjaran", cbothnajaran.Text);
                 ObjGlobal.Param.Add("createdby", ObjSys.GetUserId);
                 ObjGlobal.ExecuteProcedure("SPPostingTranspiutang", ObjGlobal.Param);
                 ObjGlobal.ExecuteProcedure("SPhapusTampungantagihan", ObjGlobal.Param);

                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                 ShowMessage("success", "Data Berhasil Di Posting");
                 LoadDataCombo();
                 LoadData(cboCabang.Text);
            }


            #endregion
        }

    }
}