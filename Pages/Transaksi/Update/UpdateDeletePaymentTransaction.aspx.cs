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

namespace eFinance.Pages.Transaksi.Update
{
    public partial class UpdateDeletePaymentTransaction : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                LoadIndexYayasan();
                loadDataCombo();
            }
        }
        protected void LoadIndexYayasan()
        {
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
            DataRow myRow = mySet.Tables[0].Rows[0];
            hdnnoYysn.Value = myRow["noCabang"].ToString();
        }

        protected void loadDataCombo()
        {

            //if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            //{
            //    cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Perwakilan' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and stspusat=0 and stscabang=1) a");
            //    cboPerwakilan.DataValueField = "id";
            //    cboPerwakilan.DataTextField = "name";
            //    cboPerwakilan.DataBind();
            //}
            //else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            //{
            //    cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and noCabang=" + ObjSys.GetCabangId + " and stspusat=0 and stscabang=1) a");
            //    cboPerwakilan.DataValueField = "id";
            //    cboPerwakilan.DataTextField = "name";
            //    cboPerwakilan.DataBind();
            //}
            //else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            //{
            //    cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where noCabang=" + ObjSys.GetParentCabang + " and stspusat=0 and stscabang=1) a");
            //    cboPerwakilan.DataValueField = "id";
            //    cboPerwakilan.DataTextField = "name";
            //    cboPerwakilan.DataBind();
            //}

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("yayasan", hdnnoYysn.Value);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", ObjSys.GetParentCabang);
            cboPerwakilan.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataWilayah", ObjGlobal.Param);
            cboPerwakilan.DataValueField = "id";
            cboPerwakilan.DataTextField = "name";
            cboPerwakilan.DataBind();

            LoadDataCombo2();
        }

        protected void LoadDataCombo2()
        {
            //if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            //{
            //    cboUnit.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + ObjSys.GetParentCabang + " and nocabang=" + ObjSys.GetCabangId + " and stspusat=0 and stscabang=2) a");
            //    cboUnit.DataValueField = "id";
            //    cboUnit.DataTextField = "name";
            //    cboUnit.DataBind();
            //}
            //else
            //{
            //    cboUnit.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Unit' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + cboPerwakilan.Text + " and stspusat=0 and stscabang=2) a");
            //    cboUnit.DataValueField = "id";
            //    cboUnit.DataTextField = "name";
            //    cboUnit.DataBind();
            //}

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("perwakilan", cboPerwakilan.Text);
            cboUnit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnit", ObjGlobal.Param);
            cboUnit.DataValueField = "id";
            cboUnit.DataTextField = "name";
            cboUnit.DataBind();

        }


        protected void LoadData(string perwakilan  , string unit  )
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("perwakilan", perwakilan);
            ObjGlobal.Param.Add("unit", unit);
            ObjGlobal.Param.Add("dtmulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            grdPTKP.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUpdateDeletePaymentTransaction", ObjGlobal.Param);
            grdPTKP.DataBind();

        }
        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
 
                if (hdnMode.Value.ToLower() == "edit")
                {
                    this.ShowHideGridAndForm(false, true, false);

                    DataSet mySet = ObjDb.GetRows("SELECT reference_id , payment_transaction_id, va, name, code,  convert(varchar, payment_date, 106) as payment_date, amount FROM payment_transaction WHERE payment_transaction_id = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    lblCabang.Text = myRow["reference_id"].ToString();
                    lblva.Text = myRow["va"].ToString();
                    lblnama.Text = myRow["name"].ToString();
                    lbltgl.Text = myRow["payment_date"].ToString();
                    lblCode.Text = myRow["code"].ToString();
                 
                    txtNominal.Text = myRow["amount"].ToString();
                   


                    this.ShowHideGridAndForm(false, true, false);

                }
        
                else if (hdnMode.Value.ToLower() == "deleteall")
                {
                    /*DELETE ALL SELECTED*/

                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("payment_transaction_id", itemRow);
                            ObjDb.Delete("payment_transaction", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdPTKP.Rows)
                    {
                        string itemId = grdPTKP.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdPTKP.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("payment_transaction_id", itemId);
                            ObjDb.Delete("payment_transaction", ObjDb.Where);
                        }
                    }
                    /*END DELETE ALL SELECTED*/
                    LoadData(cboPerwakilan.Text, cboUnit.Text);
                    ShowMessage("success", "Data yang dipilih berhasil dihapus.");
                    this.ShowHideGridAndForm(true, false, false);
                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Invalid transaction data.");
            }
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
        }
        protected void ClearData()
        {
            CloseMessage();
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();
            this.ShowHideGridAndForm(true, false, false);
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (txtNominal.Text == "")
            {
                message += ObjSys.CreateMessage("Nilai tidak boleh kosong.");
                valid = false;
            }
         

            if (valid == true)
            {
                try
                {

                    ObjDb.Where.Clear();
                    ObjDb.Data.Clear();
                    ObjDb.Where.Add("payment_transaction_id", hdnId.Value);
                    ObjDb.Data.Add("amount", Convert.ToDecimal(txtNominal.Text).ToString());
                    ObjDb.Update("payment_transaction", ObjDb.Data, ObjDb.Where);

                    this.ShowHideGridAndForm(true, false, false);

                    LoadData(cboPerwakilan.Text, cboUnit.Text);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diupdate.");

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Invalid transaction data.");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }

        protected void grdPTKP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues();
            grdPTKP.PageIndex = e.NewPageIndex;
       
                LoadData(cboPerwakilan.Text, cboUnit.Text);
           
            PopulateCheckedValues();
        }
        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataCombo2();
        }


        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadData(cboPerwakilan.Text, cboUnit.Text);
        }
        /*START SAVE CHECKBOX SELECTED IN ROWS*/
        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdPTKP.Rows)
                {
                    string index = grdPTKP.DataKeys[gvrow.RowIndex].Value.ToString();
                    if (userdetails.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)gvrow.FindControl("chkCheck");
                        myCheckBox.Checked = true;
                    }
                }
            }
        }
        private void SaveCheckedValues()
        {
            ArrayList userdetails = new ArrayList();
            foreach (GridViewRow gvrow in grdPTKP.Rows)
            {
                string index = grdPTKP.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdPTKP.Rows[gvrow.RowIndex].FindControl("chkCheck");
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
        /*END SAVE CHECKBOX SELECTED IN ROWS*/
    }
}