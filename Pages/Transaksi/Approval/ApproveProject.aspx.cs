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
    public partial class ApproveProject : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("noUser", ObjSys.GetUserId);
            ObjGlobal.Param.Add("noParameter", "7");
            grdProject.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataApproveProject", ObjGlobal.Param);
            grdProject.DataBind();
            if (grdProject.Rows.Count > 0)
                button.Visible = true;
            else
                button.Visible = false;
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

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            for (int i = 0; i < grdProject.Rows.Count; i++)
            {
                RadioButton rdoApprove = (RadioButton)grdProject.Rows[i].FindControl("rdoApprove");
                RadioButton rdoReject = (RadioButton)grdProject.Rows[i].FindControl("rdoReject");
                TextBox txtKeterangan = (TextBox)grdProject.Rows[i].FindControl("txtKeterangan");
                CheckBox chkCheck = (CheckBox)grdProject.Rows[i].FindControl("chkCheck");

                chkCheck.Checked = false;
                rdoApprove.Checked = false;
                rdoReject.Checked = false;
                txtKeterangan.Text = "";
            }
            showHideFormKas(true, false);
        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";

            int cekData = 0;
            for (int i = 0; i < grdProject.Rows.Count; i++)
            {
                RadioButton rdoApprove = (RadioButton)grdProject.Rows[i].FindControl("rdoApprove");
                RadioButton rdoReject = (RadioButton)grdProject.Rows[i].FindControl("rdoReject");
                if (rdoApprove.Checked == true || rdoReject.Checked == true)
                    cekData++;
            }

            if (cekData == 0)
            {
                message = ObjSys.CreateMessage("Aksi approve / reject harus di cek");
                alert = "error";
                valid = false;
            }

            try
            {
                if (valid == true)
                {
                    for (int i = 0; i < grdProject.Rows.Count; i++)
                    {
                        HiddenField hdnnoProject = (HiddenField)grdProject.Rows[i].FindControl("hdnnoProject");
                        HiddenField hdnlvlApprove = (HiddenField)grdProject.Rows[i].FindControl("hdnlvlApprove");                        
                        RadioButton rdoApprove = (RadioButton)grdProject.Rows[i].FindControl("rdoApprove");
                        RadioButton rdoReject = (RadioButton)grdProject.Rows[i].FindControl("rdoReject");
                        TextBox txtKeterangan = (TextBox)grdProject.Rows[i].FindControl("txtKeterangan");

                        CheckBox chkCheck = (CheckBox)grdProject.Rows[i].FindControl("chkCheck");

                        if (chkCheck.Checked == true)
                        {
                            string levelAppv = "";

                            DataSet mySet = ObjDb.GetRows("select TOP 1 b.levelApprove from mProject a " +
                                  "inner join MstApprove b on b.noParameterApprove = '7' where " +
                                  "a.nocabang = b.nocabang and a.noProject='" + hdnnoProject.Value + "' ORDER BY b.levelApprove DESC");

                            DataRow myRow = mySet.Tables[0].Rows[0];
                            levelAppv = myRow["levelApprove"].ToString();
                            if (levelAppv == hdnlvlApprove.Value)
                            {

                                ObjDb.Data.Clear();
                                ObjDb.Where.Clear();
                                ObjDb.Where.Add("noProject", hdnnoProject.Value);
                                ObjDb.Data.Add("stsApp", "1");
                                ObjDb.Update("mProject", ObjDb.Data, ObjDb.Where);
                            }

                            if (rdoApprove.Checked == true)
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noProject", hdnnoProject.Value);
                                ObjDb.Data.Add("noUser", ObjSys.GetUserId);
                                ObjDb.Data.Add("apprKe", hdnlvlApprove.Value);
                                ObjDb.Data.Add("stsAppr", "1");
                                ObjDb.Data.Add("reasson", txtKeterangan.Text);
                                ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Insert("mProject_jenjang", ObjDb.Data);
                            }

                            if (rdoReject.Checked == true)
                            {
                                string sql1 = "update mProject set stsApp = '2' where noProject = '" + hdnnoProject.Value + "'";
                                ObjDb.ExecQuery(sql1);

                                string sql2 = "delete mProject_jenjang where noProject = '" + hdnnoProject.Value + "'";
                                ObjDb.ExecQuery(sql2);

                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noProject", hdnnoProject.Value);
                                ObjDb.Data.Add("noUser", ObjSys.GetUserId);
                                ObjDb.Data.Add("reasson", txtKeterangan.Text);
                                ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Insert("mProject_jenjangReject", ObjDb.Data);
                            }
                        }
                        
                    }

                    loadData();
                    showHideFormKas(true, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diupdate.");
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

        protected void grdProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RadioButton rdoApprove = (RadioButton)e.Row.FindControl("rdoApprove");
                RadioButton rdoReject = (RadioButton)e.Row.FindControl("rdoReject");
                Label lblAlert = (Label)e.Row.FindControl("lblAlert");

                if (lblAlert.Text == "Available")
                {
                    rdoApprove.Enabled = true;
                    rdoReject.Enabled = true;
                }
                else
                {
                    rdoApprove.Enabled = false;
                    rdoReject.Enabled = false;
                }
            }
        }

        protected void rdo_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < grdProject.Rows.Count; i++)
            {
                RadioButton rdoApprove = (RadioButton)grdProject.Rows[i].FindControl("rdoApprove");
                RadioButton rdoReject = (RadioButton)grdProject.Rows[i].FindControl("rdoReject");
                CheckBox cekA = (CheckBox)grdProject.Rows[i].FindControl("chkCheck");

                if (rdoApprove.Checked == true || rdoReject.Checked == true)
                    cekA.Checked = true;
                else
                    cekA.Checked = false;
            }
        }

    }
}