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
    public partial class ApproveStatusKrywn : System.Web.UI.Page
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
            ObjGlobal.Param.Add("noUser", ObjSys.GetUserId);
            ObjGlobal.Param.Add("noParameter", "6");
            grdStsKrywn.DataSource = ObjGlobal.GetDataProcedure("SPAlertStatusKaryawan", ObjGlobal.Param);
            grdStsKrywn.DataBind();
            if (grdStsKrywn.Rows.Count > 0)
                button.Visible = true;
            else
                button.Visible = false;
        }

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";

            int cekData = 0;
            for (int i = 0; i < grdStsKrywn.Rows.Count; i++)
            {
                RadioButton rdoApprove = (RadioButton)grdStsKrywn.Rows[i].FindControl("rdoApprove");
                RadioButton rdoNotApprove = (RadioButton)grdStsKrywn.Rows[i].FindControl("rdoNotApprove");
                if (rdoApprove.Checked == true || rdoNotApprove.Checked == true)
                    cekData++;
            }

            if (cekData == 0)
            {
                message = ObjSys.CreateMessage("Aksi approve / not approve harus di cek");
                alert = "error";
                valid = false;
            }

            try
            {
                if (valid == true)
                {
                    for (int i = 0; i < grdStsKrywn.Rows.Count; i++)
                    {
                        HiddenField hdnnoTransStsKrywn = (HiddenField)grdStsKrywn.Rows[i].FindControl("hdnnoTransStsKrywn");
                        HiddenField hdnnoKrywn = (HiddenField)grdStsKrywn.Rows[i].FindControl("hdnnoKrywn");
                        HiddenField hdnlvlApprove = (HiddenField)grdStsKrywn.Rows[i].FindControl("hdnlvlApprove");
                        HiddenField hdnStatus = (HiddenField)grdStsKrywn.Rows[i].FindControl("hdnStatus");
                        RadioButton rdoApprove = (RadioButton)grdStsKrywn.Rows[i].FindControl("rdoApprove");
                        RadioButton rdoNotApprove = (RadioButton)grdStsKrywn.Rows[i].FindControl("rdoNotApprove");

                        CheckBox chkCheck = (CheckBox)grdStsKrywn.Rows[i].FindControl("chkCheck");

                        if (chkCheck.Checked == true)
                        {
                            if (rdoApprove.Checked == true)
                            {
                                string levelAppv = "";
                                DataSet mySet = ObjDb.GetRows("select TOP 1 b.levelApprove from TransStsKrywn a " +
                                      "inner join MstApprove b on b.noParameterApprove = 6 where a.sts = 0 " +
                                      "and a.noTransStsKrywn='" + hdnnoTransStsKrywn.Value + "' ORDER BY b.levelApprove DESC");

                                DataRow myRow = mySet.Tables[0].Rows[0];
                                levelAppv = myRow["levelApprove"].ToString();
                                if (levelAppv == hdnlvlApprove.Value)
                                {
                                    ObjDb.Data.Clear();
                                    ObjDb.Where.Clear();
                                    ObjDb.Where.Add("noTransStsKrywn", hdnnoTransStsKrywn.Value);
                                    ObjDb.Data.Add("sts", "1");
                                    ObjDb.Update("TransStsKrywn", ObjDb.Data, ObjDb.Where);

                                    ObjDb.Data.Clear();
                                    ObjDb.Where.Clear();
                                    ObjDb.Where.Add("noKaryawan", hdnnoKrywn.Value);
                                    ObjDb.Data.Add("statusPeg", hdnStatus.Value);
                                    ObjDb.Update("MstKaryawan", ObjDb.Data, ObjDb.Where);
                                }

                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noTransStsKrywn", hdnnoTransStsKrywn.Value);
                                ObjDb.Data.Add("nouser", ObjSys.GetUserId);
                                ObjDb.Data.Add("Appke", hdnlvlApprove.Value);
                                ObjDb.Data.Add("stsApp", "1");
                                ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Insert("TransJenjangStatus", ObjDb.Data);

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
                    }

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data has been updated.");

                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage(alert, message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
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

        protected void rdo_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < grdStsKrywn.Rows.Count; i++)
            {
                RadioButton rdoApprove = (RadioButton)grdStsKrywn.Rows[i].FindControl("rdoApprove");
                RadioButton rdoNotApprove = (RadioButton)grdStsKrywn.Rows[i].FindControl("rdoNotApprove");
                CheckBox cekA = (CheckBox)grdStsKrywn.Rows[i].FindControl("chkCheck");

                if (rdoApprove.Checked == true || rdoNotApprove.Checked == true)
                    cekA.Checked = true;
                else
                    cekA.Checked = false;
            }
        }

        protected void grdStsKrywn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RadioButton rdoApprove = (RadioButton)e.Row.FindControl("rdoApprove");
                RadioButton rdoNotApprove = (RadioButton)e.Row.FindControl("rdoNotApprove");
                Label lblAlert = (Label)e.Row.FindControl("lblAlert");

                if (lblAlert.Text == "Available")
                {
                    rdoApprove.Enabled = true;
                    rdoNotApprove.Enabled = true;
                }
                else
                {
                    rdoApprove.Enabled = false;
                    rdoNotApprove.Enabled = false;
                }
            }
        }
    }
}