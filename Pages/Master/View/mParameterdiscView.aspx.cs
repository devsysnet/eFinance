using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.View
{
    public partial class mParameterdiscView : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                //ObjSys.SessionCheck("MProjectupdate.aspx");
                LoadData();
                //LoadDataCombo();
            }
        }

        //protected void IndexPakai()
        //{
        //    for (int i = 0; i < grdCabang.Rows.Count; i++)
        //    {
        //        string itemId = grdCabang.DataKeys[i].Value.ToString();
        //        CheckBox chkCheck = (CheckBox)grdCabang.Rows[i].FindControl("chkCheck");


        //        DataSet mySet1 = ObjDb.GetRows("Select nocabang from unionparamcabang Where nocabang = '" + ObjSys.GetCabangId + "'");
        //        if (mySet1.Tables[0].Rows.Count > 0)
        //            chkCheck.Visible = false;
        //        else
        //            chkCheck.Visible = true;

        //    }

        //}

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataupdparameterDisc", ObjGlobal.Param);
            grdCabang.DataBind();
            //IndexPakai();
            loadCombo();
        }

        protected void grdCabang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues_View();
            grdCabang.PageIndex = e.NewPageIndex;
            LoadData();
            PopulateCheckedValues_View();
        }

        protected void loadCombo()
        {
            cbojnstransaksi.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih Jenis Transaksi---' name union select noTransaksi as id, jenisTransaksi as name from mJenisTransaksi where nocabang='" + ObjSys.GetCabangId + "') a");
            cbojnstransaksi.DataValueField = "id";
            cbojnstransaksi.DataTextField = "name";
            cbojnstransaksi.DataBind();

            cbonorek.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Debit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis='23' and sts='2') a");
            cbonorek.DataValueField = "id";
            cbonorek.DataTextField = "name";
            cbonorek.DataBind();

        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
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

        protected void ClearData()
        {
            //cboTA.Text = "";
            //dtKas.Text = "";
            //dtKas1.Text = "";
            //cboitunggaji.Text = "";
            //cboPotonggaji.Text = "";
            //txtNilai.Text = "";
            LoadData();
            CloseMessage();
            showHideForm(true, false);
        }

        protected void showHideForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void grdCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = grdCabang.SelectedRow.RowIndex;
                hdnID.Value = grdCabang.DataKeys[rowIndex].Values[0].ToString();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("Id", hdnID.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataupdparameterdiscdet", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                namaDiskon.Text = myRow["namaDiskon"].ToString();
                dtsistem.Text = myRow["drtgl"].ToString();
                dtsistem1.Text = myRow["sdtgl"].ToString();
                cbojnstransaksi.Text = myRow["notransaksi"].ToString();
                cboStatus.Text = myRow["jns"].ToString();
                txtnilai.Text = myRow["nilai"].ToString();
                cbonorek.Text = myRow["norek"].ToString();

                CloseMessage();
                showHideForm(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        private void PopulateCheckedValues_View()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdCabang.Rows)
                {
                    string index = grdCabang.DataKeys[gvrow.RowIndex].Value.ToString();
                    if (userdetails.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)gvrow.FindControl("chkCheck");
                        myCheckBox.Checked = true;
                    }
                }
            }
        }
        private void SaveCheckedValues_View()
        {
            ArrayList userdetails = new ArrayList();
            for (int i = 0; i < grdCabang.Rows.Count; i++)
            {
                string index = grdCabang.DataKeys[i].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdCabang.Rows[i].FindControl("chkCheck");
                bool result = chkCheck.Checked;
                // Check in the Session
                if (ViewState["CHECKED_ITEMS"] != null)
                    userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
                if (result)
                {
                    if (!userdetails.Contains(index))
                        userdetails.Add(index);
                }
                else
                    userdetails.Remove(index);
            }
            if (userdetails != null && userdetails.Count > 0)
                ViewState["CHECKED_ITEMS"] = userdetails;
        }

        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnMode.Value.ToLower() == "deleteall")
                {
                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noParamdisc", itemRow);
                            ObjDb.Delete("ParameterDisc", ObjDb.Where);
                        }
                    }
                    for (int i = 0; i < grdCabang.Rows.Count; i++)
                    {
                        string itemId = grdCabang.DataKeys[i].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdCabang.Rows[i].FindControl("chkCheck");

                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noParamdisc", itemId);
                            ObjDb.Delete("ParameterDisc", ObjDb.Where);
                        }
                    }
                    /*END DELETE ALL SELECTED*/
                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus.");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
    }
}