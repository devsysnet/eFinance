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
    public partial class AppvkenaikanGaji : System.Web.UI.Page
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

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("noUser", ObjSys.GetUserId);
            ObjGlobal.Param.Add("noParameter", "5");
            grdGajiRutin.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAprrovenaikGaji", ObjGlobal.Param);
            grdGajiRutin.DataBind();

            for (int i = 0; i < grdGajiRutin.Rows.Count; i++)
            {
                TextBox dtReview = (TextBox)grdGajiRutin.Rows[i].FindControl("dtReview");
                dtReview.Enabled = false;
            }
            if (grdGajiRutin.Rows.Count > 0)
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";

            int cekData = 0;
            for (int i = 0; i < grdGajiRutin.Rows.Count; i++)
            {
                RadioButton rdoApprove = (RadioButton)grdGajiRutin.Rows[i].FindControl("rdoApprove");
                RadioButton rdoReject = (RadioButton)grdGajiRutin.Rows[i].FindControl("rdoReject");
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
                    for (int i = 0; i < grdGajiRutin.Rows.Count; i++)
                    {
                        HiddenField hdnnoNaikgaji = (HiddenField)grdGajiRutin.Rows[i].FindControl("hdnnoNaikgaji");
                        HiddenField hdnnoKaryawan = (HiddenField)grdGajiRutin.Rows[i].FindControl("hdnnoKaryawan");
                        HiddenField hdnlvlApprove = (HiddenField)grdGajiRutin.Rows[i].FindControl("hdnlvlApprove");
                        RadioButton rdoApprove = (RadioButton)grdGajiRutin.Rows[i].FindControl("rdoApprove");
                        RadioButton rdoReject = (RadioButton)grdGajiRutin.Rows[i].FindControl("rdoReject");
                        TextBox txtKeterangan = (TextBox)grdGajiRutin.Rows[i].FindControl("txtKeterangan");
                        TextBox dtReview = (TextBox)grdGajiRutin.Rows[i].FindControl("dtReview");

                        CheckBox chkCheck = (CheckBox)grdGajiRutin.Rows[i].FindControl("chkCheck");

                        if (chkCheck.Checked == true)
                        {
                            string levelAppv = "";

                            DataSet mySet = ObjDb.GetRows("select TOP 1 b.levelApprove from TransKenaikanGaji a " +
                                  "inner join MstApprove b on b.noParameterApprove = '5' where " +
                                  "a.nocabang = b.nocabang and a.noNaikgaji='" + hdnnoNaikgaji.Value + "' ORDER BY b.levelApprove DESC");

                            DataRow myRow = mySet.Tables[0].Rows[0];
                            levelAppv = myRow["levelApprove"].ToString();
                            if (levelAppv == hdnlvlApprove.Value)
                            {

                                ObjDb.Data.Clear();
                                ObjDb.Where.Clear();
                                ObjDb.Where.Add("noNaikgaji", hdnnoNaikgaji.Value);
                                ObjDb.Data.Add("stsApp", "1");
                                ObjDb.Update("TransKenaikanGaji", ObjDb.Data, ObjDb.Where);

                                ObjDb.Data.Clear();
                                ObjDb.Where.Clear();
                                ObjDb.Where.Add("nokaryawan", hdnnoKaryawan.Value);
                                ObjDb.Data.Add("stsrubahgaji", "1");
                                ObjDb.Update("MstKaryawan", ObjDb.Data, ObjDb.Where);

                              
                            }

                            if (rdoApprove.Checked == true)
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noNaikgaji", hdnnoNaikgaji.Value);
                                ObjDb.Data.Add("noUser", ObjSys.GetUserId);
                                ObjDb.Data.Add("apprKe", hdnlvlApprove.Value);
                                ObjDb.Data.Add("stsAppv", "1");
                                ObjDb.Data.Add("reason", txtKeterangan.Text);
                                ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Insert("TransNaikGaji_Jenjang", ObjDb.Data);
                            }

                            if (rdoReject.Checked == true)
                            {
                                string sql1 = "update TransKenaikanGaji set stsApp = '2', tglReview = '" + dtReview.Text + "' where noNaikgaji = '" + hdnnoNaikgaji.Value + "'";
                                ObjDb.ExecQuery(sql1);

                                string sql2 = "delete TransNaikGaji_Jenjang where noNaikgaji = '" + hdnnoNaikgaji.Value + "'";
                                ObjDb.ExecQuery(sql2);

                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noNaikgaji", hdnnoNaikgaji.Value);
                                ObjDb.Data.Add("noUser", ObjSys.GetUserId);
                                ObjDb.Data.Add("apprKe", hdnlvlApprove.Value);
                                ObjDb.Data.Add("reason", txtKeterangan.Text);
                                ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Insert("TransNaikGaji_JenjangReject", ObjDb.Data);
                            }
                        }

                    }



                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diupdate.");

                }
                
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
            for (int i = 0; i < grdGajiRutin.Rows.Count; i++)
            {
                RadioButton rdoApprove = (RadioButton)grdGajiRutin.Rows[i].FindControl("rdoApprove");
                RadioButton rdoReject = (RadioButton)grdGajiRutin.Rows[i].FindControl("rdoReject");
                TextBox txtKeterangan = (TextBox)grdGajiRutin.Rows[i].FindControl("txtKeterangan");
                CheckBox chkCheck = (CheckBox)grdGajiRutin.Rows[i].FindControl("chkCheck");

                chkCheck.Checked = false;
                rdoApprove.Checked = false;
                rdoReject.Checked = false;
                txtKeterangan.Text = "";
            }
        }

        protected void rdo_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < grdGajiRutin.Rows.Count; i++)
            {
                TextBox dtReview = (TextBox)grdGajiRutin.Rows[i].FindControl("dtReview");
                RadioButton rdoApprove = (RadioButton)grdGajiRutin.Rows[i].FindControl("rdoApprove");
                RadioButton rdoReject = (RadioButton)grdGajiRutin.Rows[i].FindControl("rdoReject");
                CheckBox cekA = (CheckBox)grdGajiRutin.Rows[i].FindControl("chkCheck");

                if (rdoApprove.Checked == true)
                {
                    cekA.Checked = true;
                    dtReview.Enabled = false;
                }
                else if (rdoReject.Checked == true)
                {
                    cekA.Checked = true;
                    dtReview.Enabled = true;
                }
                else
                {
                    dtReview.Enabled = false;
                    cekA.Checked = false;
                }
            }
        }
       
    }
}