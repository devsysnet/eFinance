using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace eFinance.Pages.Transaksi.Posting
{
    public partial class unPostHarianGLALL : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadData();
            }
        }

        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            cboYear.DataSource = ObjGlobal.GetDataProcedure("SPLoaddataposthnall", ObjGlobal.Param);
            cboYear.DataValueField = "tgl";
            cboYear.DataTextField = "bulan";
            cboYear.DataBind();

            ObjGlobal.Param.Clear();
            cbobln.DataSource = ObjGlobal.GetDataProcedure("SPLoaddataposblnall", ObjGlobal.Param);
            cbobln.DataValueField = "tgl";
            cbobln.DataTextField = "bulan";
            cbobln.DataBind();
        }

        #region 

        protected void btnPosting_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (valid == true)
            {

                //if (ObjDb.GetRows("select distinct sts from tkasdetil where sts=0 and month(tgl)='" + cbobln.Text + "' and year(tgl)='" + cboYear.Text + "' ").Tables[0].Rows.Count > 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //    ShowMessage("error", "Masih ada Data yang Belum Posting Harian GL");
                //}

                //else
                //{
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("thn", cboYear.Text);
                    ObjGlobal.Param.Add("bln", cbobln.Text);
                    ObjGlobal.ExecuteProcedure("SPProsesunpostharianGLALL", ObjGlobal.Param);

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data Berhasil Di Posting");
                    LoadData();

                //}
            }


            #endregion
        }

    }
}