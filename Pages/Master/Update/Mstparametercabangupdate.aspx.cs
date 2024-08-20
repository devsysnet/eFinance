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


namespace eFinance.Pages.Master.Update
{
    public partial class Mstparametercabangupdate : System.Web.UI.Page
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
                LoadData();
                showhidekode.Visible = false;
                //LoadDataCombo();
            }
        }

        protected void IndexPakai()
        {
            for (int i = 0; i < grdCabang.Rows.Count; i++)
            {
                string itemId = grdCabang.DataKeys[i].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdCabang.Rows[i].FindControl("chkCheck");


                DataSet mySet1 = ObjDb.GetRows("Select nocabang from unionparamcabang Where nocabang = '" + ObjSys.GetCabangId + "'");
                if (mySet1.Tables[0].Rows.Count > 0)
                    chkCheck.Visible = false;
                else
                    chkCheck.Visible = true;

            }

        }

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataupdparametercabang", ObjGlobal.Param);
            grdCabang.DataBind();
            IndexPakai();
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

            cboTA.DataSource = ObjDb.GetRows("select distinct tahunAjaran as id, tahunAjaran as name from Parameter");
            cboTA.DataValueField = "id";
            cboTA.DataTextField = "name";
            cboTA.DataBind();

            cbobank.DataSource = ObjDb.GetRows("select a.* from (SELECT 0 id, '---Pilih Bank---' name union SELECT distinct norek id,ket name FROM mRekening where jenis='2' and sts='2' and nocabang=" + ObjSys.GetCabangId + ") a");
            cbobank.DataValueField = "id";
            cbobank.DataTextField = "name";
            cbobank.DataBind();
        }

        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbojnsbyr.Text == "PaymentGateWay")
                showhidekode.Visible = true;

            else
                showhidekode.Visible = false;
           

        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (valid == true)
            {
                try
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("Id", hdnID.Value);
                    ObjGlobal.Param.Add("tahunajaran", cboTA.Text);
                    ObjGlobal.Param.Add("mulaithnajaran", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("akhirthnajaran", Convert.ToDateTime(dtKas1.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("dtdari", Convert.ToDateTime(dtdari.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("dtsampai", Convert.ToDateTime(dtsampai.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("absenkegaji", cboitunggaji.Text);
                    ObjGlobal.Param.Add("absenpotonggaji", cboPotonggaji.Text);
                    ObjGlobal.Param.Add("jammasuk", jammasuk.Text);
                    ObjGlobal.Param.Add("jamkeluar", jamkeluar.Text);
                    ObjGlobal.Param.Add("upahminimum", Convert.ToDecimal(txtNilai.Text).ToString());
                    ObjGlobal.Param.Add("biayaadmbank", Convert.ToDecimal(biayaadmbank.Text).ToString());

                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("penggajian", cbogaji.Text);
                    ObjGlobal.Param.Add("modifiedBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("jenisbyr", cbojnsbyr.Text);
                    ObjGlobal.Param.Add("bankpayment", cbobank.Text);

                    ObjGlobal.Param.Add("kdbank", kdbank.Text);
                    ObjGlobal.Param.Add("jnsbank", cbojnsbank.Text);
                    ObjGlobal.Param.Add("kepalasekolah", kepalasekolah.Text);
                    ObjGlobal.Param.Add("ipcabang", ipcabang.Text);
                    ObjGlobal.Param.Add("cuti", cboscuti.Text);
                    ObjGlobal.GetDataProcedure("SPUpdateparametercabang", ObjGlobal.Param);

                    ClearData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diupdate.");
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
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
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataupdparametercabangdet", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                biayaadmbank.Text = myRow["biayaadmbank"].ToString();
                dtKas.Text = myRow["mulaithnajaran"].ToString();
                dtKas1.Text = myRow["akhirthnajaran"].ToString();
                cboitunggaji.Text = myRow["absengaji"].ToString();
                dtdari.Text = myRow["tglmulai"].ToString();
                dtsampai.Text = myRow["tglakhir"].ToString();
                cboPotonggaji.Text = myRow["absempotonggaji"].ToString();
                jammasuk.Text = myRow["jammasuk"].ToString();
                jamkeluar.Text = myRow["jamkeluar"].ToString();
                txtNilai.Text = myRow["upahminimum"].ToString();
                cbojnsbyr.Text = myRow["jenisbyr"].ToString();
                cbobank.Text= myRow["bankpayment"].ToString();
                kepalasekolah.Text= myRow["kepalasekolah"].ToString();
                cbojnsbank.Text= myRow["jnsadm"].ToString();
                ipcabang.Text = myRow["ipcabang"].ToString();
                cboscuti.Text= myRow["strategicuti"].ToString();
                kdbank.Text= myRow["kodebank"].ToString();

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
                            ObjDb.Where.Add("noparametercb", itemRow);
                            ObjDb.Delete("Parametercabang", ObjDb.Where);
                        }
                    }
                    for (int i = 0; i < grdCabang.Rows.Count; i++)
                    {
                        string itemId = grdCabang.DataKeys[i].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdCabang.Rows[i].FindControl("chkCheck");

                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noparametercb", itemId);
                            ObjDb.Delete("Parametercabang", ObjDb.Where);
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