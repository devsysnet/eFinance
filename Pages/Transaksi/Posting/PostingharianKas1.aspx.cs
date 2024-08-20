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
    public partial class PostingharianKas1 : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        protected string execBind = string.Empty;
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                dtSampai.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                loadDataCombo();
                loadData();
                btnPosting.Visible = false;

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();

        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("tglMulai", dtMulai.Text);
            ObjGlobal.Param.Add("tglsampai", dtSampai.Text);
            ObjGlobal.Param.Add("noRek", cboAccount.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdHarianGL.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPostHarianKas1", ObjGlobal.Param);
            grdHarianGL.DataBind();
            if (grdHarianGL.Rows.Count > 0)
                btnPosting.Visible = true;
            else
                btnPosting.Visible = false;

        }
        protected void loadDataCombo()
        {
            cboAccount.DataSource = ObjDb.GetRows("select a.* from (select '0' id,' ---Pilih Akun--- ' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='2' and nocabang = '" + ObjSys.GetCabangId + "' union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='1') a order by a.id");
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
            loadData();
        }
        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnMode.Value.ToLower() == "confirm")
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("tgl", Convert.ToDateTime(dtMulai.Text).ToString("yyyy-MMM-dd"));
                    ObjGlobal.Param.Add("tgl1", Convert.ToDateTime(dtSampai.Text).ToString("yyyy-MMM-dd"));
                    ObjGlobal.Param.Add("kdrek", cboAccount.Text);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.ExecuteProcedure("SPpsotinghariankas1", ObjGlobal.Param);

                    

                }
                loadDataCombo();
                    loadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diposting");
               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        //protected void btnPosting_Click(object sender, EventArgs e)
        //{

        //    ObjGlobal.Param.Clear();
        //    ObjGlobal.Param.Add("tgl", Convert.ToDateTime(dtMulai.Text).ToString("yyyy-MMM-dd"));
        //    ObjGlobal.Param.Add("tgl1", Convert.ToDateTime(dtSampai.Text).ToString("yyyy-MMM-dd"));
        //    ObjGlobal.Param.Add("kdrek", cboAccount.Text);
        //    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
        //    ObjGlobal.ExecuteProcedure("SPpsotinghariankas1", ObjGlobal.Param);

        //    loadDataCombo();
        //    loadData();
        //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //    ShowMessage("success", "Data berhasil diposting");

        //}

        protected void cboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

            string tglMulai = "";

            if (cboAccount.SelectedValue == "")
            {
                dtMulai.Text = "";
            }

            else
            {
                DataSet myDate = ObjDb.GetRows("select norek,kdrek,min(tgl) as tglMulai from tkas where sts=1 and norek = '" + cboAccount.SelectedValue + "' and noCabang = '" + ObjSys.GetCabangId + "' group by norek,kdrek");
                if (myDate.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = myDate.Tables[0].Rows[0];
                    tglMulai = Convert.ToDateTime(MyRow["tglMulai"].ToString()).ToString("dd-MMM-yyyy");

                    dtMulai.Text = tglMulai;
                }
                else
                {
                    dtMulai.Text = "";
                }

            }

        }


    }
}