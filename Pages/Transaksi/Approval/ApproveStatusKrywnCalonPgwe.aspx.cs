using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace eFinance.Pages.Transaksi.Approval
{
    public partial class ApproveStatusKrywnCalonPgwe : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
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
        protected void CloseMessage()
        {
            ((Label)Master.FindControl("lblMessage")).Text = "";
            ((Label)Master.FindControl("lblMessage")).Visible = false;
        }

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdStsKrywn.DataSource = ObjGlobal.GetDataProcedure("SPAlertStatusKaryawanCalonPegawai", ObjGlobal.Param);
            grdStsKrywn.DataBind();
            if (grdStsKrywn.Rows.Count > 0)
                button.Visible = true;
            else
                button.Visible = false;
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < grdStsKrywn.Rows.Count; i++)
                {
                    HiddenField hdnnoKrywn = (HiddenField)grdStsKrywn.Rows[i].FindControl("hdnnoKrywn");
                    RadioButton rdoApprove = (RadioButton)grdStsKrywn.Rows[i].FindControl("rdoApprove");
                    RadioButton rdoNotApprove = (RadioButton)grdStsKrywn.Rows[i].FindControl("rdoNotApprove");

                    if (rdoApprove.Checked == true)
                    {
                        ObjDb.Data.Clear();
                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("noKaryawan", hdnnoKrywn.Value);
                        ObjDb.Data.Add("statusPeg", "1");
                        ObjDb.Data.Add("status", "1");
                        ObjDb.Update("MstKaryawan", ObjDb.Data, ObjDb.Where);
                    }
                    if (rdoNotApprove.Checked == true)
                    {
                        ObjDb.Data.Clear();
                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("noKaryawan", hdnnoKrywn.Value);
                        ObjDb.Data.Add("sts", "0");
                        ObjDb.Update("MstKaryawan", ObjDb.Data, ObjDb.Where);
                    }

                }

                LoadData();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data has been updated.");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Invalid transaction data.");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            for (int i = 0; i < grdStsKrywn.Rows.Count; i++)
            {
                RadioButton rdoApprove = (RadioButton)grdStsKrywn.Rows[i].FindControl("rdoApprove");
                RadioButton rdoNotApprove = (RadioButton)grdStsKrywn.Rows[i].FindControl("rdoNotApprove");

                rdoApprove.Checked = false;
                rdoNotApprove.Checked = false;
            }
        }
    }
}