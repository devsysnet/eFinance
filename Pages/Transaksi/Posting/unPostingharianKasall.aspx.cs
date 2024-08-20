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
    public partial class unPostingharianKasall : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                //btnPosting.Visible = false;

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            //loadData();

        }


        protected void loadDataCombo()
        {
            //cboAccount.DataSource = ObjDb.GetRows("select a.* from (select '0' id,' ---Pilih Akun--- ' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='2' and nocabang = '" + ObjSys.GetCabangId + "' union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='1') a order by a.id");
            //cboAccount.DataValueField = "id";
            //cboAccount.DataTextField = "name";
            //cboAccount.DataBind();


            cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4)) a");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();

            LoadDataAccount(cboCabang.Text);
        }


        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataAccount(cboCabang.Text);
        }

        protected void LoadDataAccount(string cabang)
        {
            cboAccount.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='2' and nocabang = '" + cabang + "' union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='1') a order by a.id");
            cboAccount.DataValueField = "id";
            cboAccount.DataTextField = "name";
            cboAccount.DataBind();
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

        }

        protected void btnPosting_Click(object sender, EventArgs e)
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("tgl", Convert.ToDateTime(dtMulai.Text).ToString("yyyy-MMM-dd"));
            ObjGlobal.Param.Add("kdrek", cboAccount.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.ExecuteProcedure("SPunpsotinghariankas", ObjGlobal.Param);

            loadDataCombo();
            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            ShowMessage("success", "Data berhasil diposting");

        }




    }
}
