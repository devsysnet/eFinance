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
    public partial class TransApproveGaji : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loaddatacombo();
                loaddata(cboYear.Text, cboCabang.Text);

            }
        }
        protected void loaddatacombo()
        {
            cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPpilihanthn1");
            cboYear.DataValueField = "id";
            cboYear.DataTextField = "name";
            cboYear.DataBind();

            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang in(2,3,4)) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang in(2,3,4) and parent='" + ObjSys.GetCabangId + "') a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();

            }
            if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang in(2,3,4)) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang in(2) and nocabang='" + ObjSys.GetCabangId + "') a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang in(2,3) and parent='" + ObjSys.GetParentCabang + "') a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
        }
        protected void loaddata(string tahun = "", string cabang = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            ObjGlobal.Param.Add("thn", tahun);
            ObjGlobal.Param.Add("nouser", ObjSys.GetUserId);
            ObjGlobal.Param.Add("noParameter", "11");
            grdGajiKala.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAprrovepotongan", ObjGlobal.Param);
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

           
            try
            {
                if (valid == true) { 

                    for (int i = 0; i < grdGajiKala.Rows.Count; i++)
                    {
                        HiddenField hdnnotransgaji = (HiddenField)grdGajiKala.Rows[i].FindControl("hdnnotransgaji");
                        HiddenField hdnnoKaryawan = (HiddenField)grdGajiKala.Rows[i].FindControl("hdnnoKaryawan");
                        HiddenField hdnlvlApprove = (HiddenField)grdGajiKala.Rows[i].FindControl("hdnlvlApprove");
                        RadioButton rdoApprove = (RadioButton)grdGajiKala.Rows[i].FindControl("rdoApprove");
                        RadioButton rdoReject = (RadioButton)grdGajiKala.Rows[i].FindControl("rdoReject");
                        TextBox txtKeterangan = (TextBox)grdGajiKala.Rows[i].FindControl("txtKeterangan");

                        CheckBox chkCheck = (CheckBox)grdGajiKala.Rows[i].FindControl("chkCheck");

                        string levelAppv = "";

                        DataSet mySet = ObjDb.GetRows("select TOP 1 b.levelApprove from TGajibulanan a " +
                        "inner join MstApprove b on b.noParameterApprove = '11' where " +
                        "a.nocabang = b.nocabang and a.noGajibln='" + hdnnotransgaji.Value + "' ORDER BY b.levelApprove DESC");

                        DataRow myRow = mySet.Tables[0].Rows[0];
                        levelAppv = myRow["levelApprove"].ToString();
                        if (levelAppv == hdnlvlApprove.Value)
                        {

                            ObjDb.Data.Clear();
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noGajibln", hdnnotransgaji.Value);
                            ObjDb.Data.Add("stsApv", "1");
                            ObjDb.Data.Add("nilai", Convert.ToDecimal(txtKeterangan.Text).ToString());
                            ObjDb.Update("TGajibulanan", ObjDb.Data, ObjDb.Where);

                        }
                        ObjDb.Data.Clear();

                        ObjDb.Data.Add("notransgaji", hdnnotransgaji.Value);
                        ObjDb.Data.Add("noUser", ObjSys.GetUserId);
                        ObjDb.Data.Add("apprKe", hdnlvlApprove.Value);
                        ObjDb.Data.Add("stsAppv", "1");
                        //ObjDb.Data.Add("reason", txtKeterangan.Text);
                        ObjDb.Data.Add("noCabang", cboCabang.Text);
                        ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                        ObjDb.Insert("Transpotongan_Jenjang", ObjDb.Data);

                    }

                loaddata(cboYear.Text, cboCabang.Text);
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
        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loaddata(cboYear.Text, cboCabang.Text);
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loaddata(cboYear.Text, cboCabang.Text);

            string levelApprove = "0";
            DataSet mySeta = ObjDb.GetRows("select levelApprove, nouser, loginid from TGajibulanan a  inner join MstApprove b on b.noParameterApprove = '11' and b.nocabang = '" + cboCabang.Text + "' and   nouser = '" + ObjSys.GetUserId + "'");

            DataRow myRowa = mySeta.Tables[0].Rows[0];
            levelApprove = myRowa["levelApprove"].ToString();

            string nouser = "0";
            DataSet mySetac = ObjDb.GetRows("select  top 1  levelApprove, nouser, loginid from TGajibulanan a  inner join MstApprove b on b.noParameterApprove = '11' and b.nocabang = '" + cboCabang.Text + "' order by levelApprove asc");

            DataRow myRowac = mySetac.Tables[0].Rows[0];
            nouser = myRowac["nouser"].ToString();

            DataSet mySetaa = ObjDb.GetRows("select distinct nouser from Transpotongan_Jenjang where nocabang = '" + cboCabang.Text + "' and nouser = '" + nouser + "'");

            if (levelApprove == "1" && mySetaa.Tables[0].Rows.Count.ToString() == "0")
            {
                btnSubmit.Enabled = true;
            }
            else if (levelApprove == "1" && mySetaa.Tables[0].Rows.Count.ToString() == "1")
            {
                btnSubmit.Enabled = false;

            }
            else
            {
                if (mySetaa.Tables[0].Rows.Count.ToString() == "0")
                {
                    btnSubmit.Enabled = false;

                }
                else if (mySetaa.Tables[0].Rows.Count.ToString() == "1")
                {
                    btnSubmit.Enabled = true;

                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loaddata(cboYear.Text, cboCabang.Text);
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