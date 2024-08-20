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
    public partial class AppThrbonus : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loaddatacombo();

                LoadData(cboCabang.Text);
            }
        }
        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(cboCabang.Text);
        }
        protected void loaddatacombo()
        {
            //cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPpilihanthn1");
            //cboYear.DataValueField = "id";
            //cboYear.DataTextField = "name";
            //cboYear.DataBind();

            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang in(2,3,4)) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang in (2,3) and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
        }
        protected void LoadData(string cabang="")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            ObjGlobal.Param.Add("noUser", ObjSys.GetUserId);
            ObjGlobal.Param.Add("noParameter", "9");
            grdGajiKala.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAprrovePendapatan", ObjGlobal.Param);
            grdGajiKala.DataBind();

            if (grdGajiKala.Rows.Count > 0)
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
            for (int i = 0; i < grdGajiKala.Rows.Count; i++)
            {
                RadioButton rdoApprove = (RadioButton)grdGajiKala.Rows[i].FindControl("rdoApprove");
                RadioButton rdoReject = (RadioButton)grdGajiKala.Rows[i].FindControl("rdoReject");
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
                    for (int i = 0; i < grdGajiKala.Rows.Count; i++)
                    {
                        HiddenField hdnnotransgaji = (HiddenField)grdGajiKala.Rows[i].FindControl("hdnnotransgaji");
                        HiddenField hdnnoKaryawan = (HiddenField)grdGajiKala.Rows[i].FindControl("hdnnoKaryawan");
                        HiddenField hdnlvlApprove = (HiddenField)grdGajiKala.Rows[i].FindControl("hdnlvlApprove");
                        RadioButton rdoApprove = (RadioButton)grdGajiKala.Rows[i].FindControl("rdoApprove");
                        RadioButton rdoReject = (RadioButton)grdGajiKala.Rows[i].FindControl("rdoReject");
                        TextBox txtKeterangan = (TextBox)grdGajiKala.Rows[i].FindControl("txtKeterangan");

                        CheckBox chkCheck = (CheckBox)grdGajiKala.Rows[i].FindControl("chkCheck");

                        if (chkCheck.Checked == true)
                        {
                            string levelAppv = "";

                                  DataSet mySet = ObjDb.GetRows("select TOP 1 b.levelApprove from TGajibulanan a " +
                                  "inner join MstApprove b on b.noParameterApprove = '9' where " +
                                  "a.noGajibln='" + hdnnotransgaji.Value + "' ORDER BY b.levelApprove DESC");

                                DataRow myRow = mySet.Tables[0].Rows[0];
                                levelAppv = myRow["levelApprove"].ToString();
                                if (levelAppv == hdnlvlApprove.Value)
                                {

                                    ObjDb.Data.Clear();
                                    ObjDb.Where.Clear();
                                    ObjDb.Where.Add("noGajibln", hdnnotransgaji.Value);
                                    ObjDb.Data.Add("stsApv", "1");
                                    ObjDb.Update("TGajibulanan", ObjDb.Data, ObjDb.Where);
                                }

                                if (rdoApprove.Checked == true)
                                {
                                    ObjDb.Data.Clear();
                                    ObjDb.Data.Add("notransgaji", hdnnotransgaji.Value);
                                    ObjDb.Data.Add("noUser", ObjSys.GetUserId);
                                    ObjDb.Data.Add("apprKe", hdnlvlApprove.Value);
                                    ObjDb.Data.Add("stsAppv", "1");
                                    ObjDb.Data.Add("reason", txtKeterangan.Text);
                                    ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                    ObjDb.Insert("Transpeendapatan_Jenjang", ObjDb.Data);
                                }
                             
                            

                            if (rdoReject.Checked == true)
                            {
                                string sql1 = "update TGajibulanan set stsapv = '2' where noGajibln = '" + hdnnotransgaji.Value + "'";
                                ObjDb.ExecQuery(sql1);

                                string sql2 = "delete Transpeendapatan_Jenjang where notransgaji = '" + hdnnotransgaji.Value + "'";
                                ObjDb.ExecQuery(sql2);

                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("notransgaji", hdnnotransgaji.Value);
                                ObjDb.Data.Add("noUser", ObjSys.GetUserId);
                                ObjDb.Data.Add("apprKe", hdnlvlApprove.Value);
                                ObjDb.Data.Add("reason", txtKeterangan.Text);
                                ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createDate", ObjSys.GetNow);
                                ObjDb.Insert("Transpendapatan_JenjangReject", ObjDb.Data);
                            }
                        }

                    }
                 }

                LoadData(cboCabang.Text);
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diupdate.");

               

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Invalid transaction data.");
            }


            try
            {
                string status = "", statusRjt = "";
                for (int i = 0; i < grdGajiKala.Rows.Count; i++)
                {
                    string itemId = grdGajiKala.DataKeys[i].Value.ToString();
                    CheckBox chkCheck = (CheckBox)grdGajiKala.Rows[i].FindControl("chkCheck");
                    RadioButton rdoApprove = (RadioButton)grdGajiKala.Rows[i].FindControl("rdoApprove");
                    RadioButton rdoReject = (RadioButton)grdGajiKala.Rows[i].FindControl("rdoReject");
                    if (chkCheck.Checked == true && (rdoApprove.Checked == true || rdoReject.Checked == true))
                    {
                        HiddenField tipewave = (HiddenField)grdGajiKala.Rows[i].FindControl("tipewave");
                        TextBox txtKeterangan = (TextBox)grdGajiKala.Rows[i].FindControl("txtKeterangan");
                        HiddenField levelApp = (HiddenField)grdGajiKala.Rows[i].FindControl("hdnLevelApp");
                        if (rdoApprove.Checked == true)
                            status = "1";
                        else
                            status = "0";
                        if (rdoReject.Checked == true)
                            statusRjt = "1";
                        else
                            statusRjt = "0";

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noGajibln", itemId);
                        ObjGlobal.Param.Add("stsapv", "1");
                        ObjGlobal.Param.Add("noUser", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("apprKe", levelApp.Value);
                        ObjGlobal.Param.Add("reason", txtKeterangan.Text);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("createdDate", ObjSys.GetNow);

                        if (statusRjt == "1")
                        {
                            ObjGlobal.ExecuteProcedure("SPUpdateStspendapatan", ObjGlobal.Param);
                        }
                        else
                        {
                            ObjGlobal.ExecuteProcedure("SPInsertApprovePendapatan", ObjGlobal.Param);
                        }
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
            for (int i = 0; i < grdGajiKala.Rows.Count; i++)
            {
                RadioButton rdoApprove = (RadioButton)grdGajiKala.Rows[i].FindControl("rdoApprove");
                RadioButton rdoReject = (RadioButton)grdGajiKala.Rows[i].FindControl("rdoReject");
                TextBox txtKeterangan = (TextBox)grdGajiKala.Rows[i].FindControl("txtKeterangan");
                CheckBox chkCheck = (CheckBox)grdGajiKala.Rows[i].FindControl("chkCheck");

                chkCheck.Checked = false;
                rdoApprove.Checked = false;
                rdoReject.Checked = false;
                txtKeterangan.Text = "";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(cboCabang.Text);
        }

        protected void rdo_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < grdGajiKala.Rows.Count; i++)
            {
                RadioButton rdoApprove = (RadioButton)grdGajiKala.Rows[i].FindControl("rdoApprove");
                RadioButton rdoReject = (RadioButton)grdGajiKala.Rows[i].FindControl("rdoReject");
                CheckBox cekA = (CheckBox)grdGajiKala.Rows[i].FindControl("chkCheck");

                if (rdoApprove.Checked == true || rdoReject.Checked == true)
                {
                    cekA.Checked = true;
                }
                else
                {
                    cekA.Checked = false;
                }
            }
        }

        protected void grdGajiKala_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < grdGajiKala.Rows.Count; i++)
            {
                RadioButton rdoApprove = (RadioButton)grdGajiKala.Rows[i].FindControl("rdoApprove");
                RadioButton rdoReject = (RadioButton)grdGajiKala.Rows[i].FindControl("rdoReject");
                CheckBox cekA = (CheckBox)grdGajiKala.Rows[i].FindControl("chkCheck");

                if (rdoApprove.Checked == true || rdoReject.Checked == true)
                {
                    cekA.Checked = true;
                }
                else
                {
                    cekA.Checked = false;
                }
            }
        }
    }
}