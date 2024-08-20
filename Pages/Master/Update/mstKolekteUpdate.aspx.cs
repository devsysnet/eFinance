using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;
namespace eFinance.Pages.Master.Update
{
    public partial class mstKolekteUpdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
                LoadData();
                loadDataCombo();
                cboCurrencyTrans.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noMataUang id, namaMataUang name FROM mMataUang) a");
                cboCurrencyTrans.DataValueField = "id";
                cboCurrencyTrans.DataTextField = "name";
                cboCurrencyTrans.DataBind();
            }
        }

        protected void cboCurrencyTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nilai = "0.00";
            DataSet mySetH = ObjDb.GetRows("select TOP 1 nilaiKursPajak from tKursPajak where noMatauang = '" + cboCurrencyTrans.SelectedValue + "'");
            DataSet mySetHx = ObjDb.GetRows("select stsDefault from mmatauang where noMatauang = '" + cboCurrencyTrans.SelectedValue + "'");
            DataRow myRowHx = mySetHx.Tables[0].Rows[0];


            //if (myRowHx["stsDefault"].ToString() == "1")
            //{
            //    nilai = myRowHx["stsDefault"].ToString();
            //}
            //else
            //{
            //    DataRow myRowH = mySetH.Tables[0].Rows[0];
            //    if (mySetH.Tables[0].Rows.Count > 0)
            //    {
            //        nilai = myRowH["nilaiKursPajak"].ToString();
            //    }
            //    else
            //    {
            //        nilai = "0.00";
            //    }


            //}

            if (myRowHx["stsDefault"].ToString() == "1")
            {
                nilai = myRowHx["stsDefault"].ToString();
            }
            else
            {
                nilai = "0.00";
            }

            txtkursrate.Visible = true;
            Kurstrs2.Visible = true;
            txtkursrate.Text = ObjSys.IsFormatNumber(nilai);
        }
        protected void loadDataCombo()
        {
            //Perwakilan Mardi Yuana Bogor GetstsPusat=1
            if (ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4) and parent = " + ObjSys.GetCabangId + ") a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //unit
            else if (ObjSys.GetstsPusat == "2")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //admin Kantor Perwakilan Bogor GetstsPusat=3
            else if (ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4) and parent = " + ObjSys.GetParentCabang + ") a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4)) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4)) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

        }

        #region rows
        private void SetInitialRow(string kd = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string)));
            dt.Columns.Add(new DataColumn("Column5", typeof(string)));

            dr = dt.NewRow();
            DataSet mySet = ObjDb.GetRows("select * from mstjeniskolekte_d a inner join mRekening b on a.noRek = b.noRek WHERE a.nokolekte = '" + kd + "' ");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = myRow["kdRek"].ToString();
                dr["Column2"] = myRow["noRek"].ToString();
                dr["Column3"] = myRow["Ket"].ToString();
                dr["Column4"] = myRow["jenis"].ToString();
                dr["Column5"] = myRow["nokolekted"].ToString();
                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr["RowNumber"] = 1;
                dr["Column1"] = string.Empty;
                dr["Column2"] = string.Empty;
                dr["Column3"] = string.Empty;
                dr["Column4"] = string.Empty;
                dr["Column5"] = string.Empty;


                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdMemoJurnal.DataSource = dt;
            grdMemoJurnal.DataBind();
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                        HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                        HiddenField txtHdnValue = (HiddenField)grdMemoJurnal.Rows[i].FindControl("txtHdnValue");
                        DropDownList cbojns = (DropDownList)grdMemoJurnal.Rows[i].FindControl("cbojns");
                        TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");

                        
                        txtAccount.Text = dt.Rows[i]["Column1"].ToString();
                        hdnAccount.Value = dt.Rows[i]["Column2"].ToString();
                        txtHdnValue.Value = dt.Rows[i]["Column5"].ToString();
                        txtDescription.Text = dt.Rows[i]["Column3"].ToString();
                        cbojns.Text = dt.Rows[i]["Column4"].ToString();
                      
                    }
                }
            }
        }
        private void AddNewRow()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                        HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                        HiddenField txtHdnValue = (HiddenField)grdMemoJurnal.Rows[i].FindControl("txtHdnValue");
                        TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");
                        DropDownList cbojns = (DropDownList)grdMemoJurnal.Rows[i].FindControl("cbojns");
             
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtAccount.Text;
                        dtCurrentTable.Rows[i]["Column2"] = hdnAccount.Value;
                        dtCurrentTable.Rows[i]["Column3"] = txtDescription.Text;
                        dtCurrentTable.Rows[i]["Column4"] = cbojns.Text;
                        dtCurrentTable.Rows[i]["Column5"] = txtHdnValue.Value;


                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdMemoJurnal.DataSource = dtCurrentTable;
                    grdMemoJurnal.DataBind();
                }
            }
            SetPreviousData();
        }
        #endregion
        protected void txtAccount_TextChanged(object sender, EventArgs e)
        {
            autoComplete();
        }

        protected void autoComplete()
        {
            for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
            {
                HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("kdRek", txtAccount.Text.Replace(" ", ""));
                DataSet mySet = ObjGlobal.GetDataProcedure("SPmrekening", ObjGlobal.Param);

                foreach (DataRow myRow in mySet.Tables[0].Rows)
                {
                    txtAccount.Text = myRow["kdRek"].ToString();
                    hdnAccount.Value = myRow["noRek"].ToString();
                    txtDescription.Text = myRow["Ket"].ToString();
                }
            }
        }
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdMemoJurnalD.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataKolekte", ObjGlobal.Param);
            grdMemoJurnalD.DataBind();
        }

        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnMode.Value.ToLower() == "deleteall")
                {
                    /*DELETE ALL SELECTED*/

                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("nokolekte", itemRow);
                            ObjDb.Delete("mstjeniskolekte_h", ObjDb.Where);

                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("nokolekte", itemRow);
                            ObjDb.Delete("mstjeniskolekte_d", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdMemoJurnalD.Rows)
                    {
                        string itemId = grdMemoJurnalD.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdMemoJurnalD.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("nokolekte", itemId);
                            ObjDb.Delete("mstjeniskolekte_h", ObjDb.Where);

                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("nokolekte", itemId);
                            ObjDb.Delete("mstjeniskolekte_d", ObjDb.Where);
                        }
                    }
                }
                /*END DELETE ALL SELECTED*/
                LoadData();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil dihapus.");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
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
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        protected void grdMemoJurnalD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMemoJurnalD.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void grdMemoJurnalD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdMemoJurnalD.SelectedRow.RowIndex;
                string noPO = grdMemoJurnalD.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noPO;
                DataSet MySet = ObjDb.GetRows("select * from mstjeniskolekte_h where nokolekte = '" + noPO + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0];
                    txtKode.Text = MyRow["jeniskolekte"].ToString();
                    hdnkolekte.Value = MyRow["nokolekte"].ToString();
                   
               
                    SetInitialRow(noPO);
             
                    CloseMessage();
                    this.ShowHideGridAndForm(false, true);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Tidak ada.");
                }

            }
            catch (Exception ex)
            {
                if (valid == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", message);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }
            }
        }

        protected void btnAddrow_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (txtKode.Text == "")
            {
                message += ObjSys.CreateMessage("Jenis Kolekte harus dipilih.");
                valid = false;
            }
         
            if (valid == true)
            {
                try
                {
                    ObjDb.Where.Clear();
                    ObjDb.Data.Clear();

                    ObjDb.Where.Add("nokolekte", hdnId.Value);
                    ObjDb.Data.Add("JenisKolekte", txtKode.Text);
                    ObjDb.Update("mstjeniskolekte_h", ObjDb.Data, ObjDb.Where);


                    for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
                    {
                        HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                        HiddenField txtHdnValue = (HiddenField)grdMemoJurnal.Rows[i].FindControl("txtHdnValue");
                        TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                        DropDownList cbojns = (DropDownList)grdMemoJurnal.Rows[i].FindControl("cbojns");
                     

                        if (txtAccount.Text != "")
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Data.Clear();

                            ObjDb.Where.Add("nokolekted", txtHdnValue.Value);
                            ObjDb.Data.Add("norek", hdnAccount.Value);
                            ObjDb.Data.Add("jenis", cbojns.Text);
                            ObjDb.Update("mstjeniskolekte_d", ObjDb.Data, ObjDb.Where);
                        }

                    }
                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diubah.");
                    this.ShowHideGridAndForm(true, false);
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


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false);
        }

        private void clearData()
        {
            txtKode.Text = "";

            for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
            {
                HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");
                TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtRemark");
                TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDebit");
                TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtKredit");

                hdnAccount.Value = "";
                txtAccount.Text = "";
                txtDescription.Text = "";
                txtRemark.Text = "";
                txtDebit.Text = "";
                txtKredit.Text = "";
                txtDebitTotal.Text = "";
                txtKreditTotal.Text = "";
                cboType.Text = "0";
                dtDate.Text = "";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearData();
            LoadData();
            this.ShowHideGridAndForm(true, false);
            CloseMessage();
        }

        protected void LoadDisable()
        {
            for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
            {
                TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                ImageButton imgButtonProduct = (ImageButton)grdMemoJurnal.Rows[i].FindControl("imgButtonProduct");
                TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");
                TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtRemark");
                TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDebit");
                TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtKredit");

                DataSet MySet2 = ObjDb.GetRows("select * from tKas where nomorKode = '" + hdnId.Value + "'");
                DataRow MyRow2 = MySet2.Tables[0].Rows[0];
                string kdRek = MyRow2["kdRek"].ToString();

                if (kdRek == txtAccount.Text)
                {
                    txtAccount.Enabled = false;
                    imgButtonProduct.Enabled = false;
                    txtDescription.Enabled = false;
                    txtRemark.Enabled = false;
                    txtDebit.Enabled = false;
                    txtKredit.Enabled = false;
                }
                else
                {
                    txtAccount.Enabled = true;
                    imgButtonProduct.Enabled = true;
                    txtDescription.Enabled = true;
                    txtRemark.Enabled = true;
                    txtDebit.Enabled = true;
                    txtKredit.Enabled = true;
                }
            }
        }

        //protected void grdMemoJurnal_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadDataPanel();
        //    mpe.Show();
        //    string value = (grdMemoJurnal.SelectedRow.FindControl("txtHdnValue") as HiddenField).Value;
        //    txtHdnPopup.Value = value;
        //    txtSearch.Text = "";
        //}
        protected void grdMemoJurnal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                txtHdnPopup.Value = rowIndex.ToString();
                if (e.CommandName == "Select")
                {
                    TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtAccount");
                    if (txtAccount.Text != "")
                    {
                        LoadDataPanelkdRek(txtAccount.Text);
                        mpe.Show();
                        //string value = (grdMemoJurnal.SelectedRow.FindControl("txtHdnValue") as HiddenField).Value;
                        //txtHdnPopup.Value = value;
                        txtSearch.Text = "";
                
                    }

                    if (txtAccount.Text == "")
                    {
                        LoadDataPanel();
                        mpe.Show();
                        //string value = (grdMemoJurnal.SelectedRow.FindControl("txtHdnValue") as HiddenField).Value;
                        //txtHdnPopup.Value = value;
                        txtSearch.Text = "";
                   
                    }

                }
                else if (e.CommandName == "Clear")
                {
                    TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtAccount");
                    HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[rowIndex].FindControl("hdnAccount");
                    TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtDescription");
                    TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtRemark");
                    TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtDebit");
                    TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtKredit");

                    txtAccount.Text = "";
                    hdnAccount.Value = "";
                    txtDescription.Text = "";
                    txtRemark.Text = "";
                    txtDebit.Text = "";
                    txtKredit.Text = "";

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();CalculateDiscount();", true);

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        private void LoadDataPanelkdRek(string kodeRek = "")
        {
            grdProduct.DataSource = ObjDb.GetRows("select * from mRekening where (kdRek like '%" + kodeRek + "%' and sts = '2') or (Ket like '%" + kodeRek + "%' and sts='2')");
            grdProduct.DataBind();
        }
        private void LoadDataPanel()
        {
            grdProduct.DataSource = ObjDb.GetRows("select * from mRekening where  (kdRek like '%" + TextBox1.Text + "%' and sts = '2') or (Ket like '%" + TextBox1.Text + "%' and sts='2')");
            grdProduct.DataBind();
        }
        protected void btnCariProduct_Click(object sender, EventArgs e)
        {
            LoadDataPanel();
            mpe.Show();
        }

        protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProduct.PageIndex = e.NewPageIndex;
            LoadDataPanel();
            mpe.Show();
        }

        protected void grdProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(txtHdnPopup.Value);
            string prodno = "", prodnm = "", noprod = "";
            prodno = (grdProduct.SelectedRow.FindControl("lblKodeReagent") as Label).Text;
            prodnm = (grdProduct.SelectedRow.FindControl("lblNamaReagent") as Label).Text;
            noprod = (grdProduct.SelectedRow.FindControl("hidNoReagent") as HiddenField).Value;

            TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtAccount");
            TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtDescription");
            HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[rowIndex].FindControl("hdnAccount");

            //int noCek = 0;
            for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
            {
                //TextBox txtAccountt = (TextBox)grdMemoJurnal.Rows[i].Cells[1].FindControl("txtAccount");
                //if (txtAccountt.Text == prodno)
                //{
                //    noCek += 1;
                //}
                //}
                //if (noCek > 0)
                //{
                //    Response.Write("<script>alert('Account yang dipilih tidak boleh sama !');</script>");
                //    mpe.Show();
                //}
                //else
                //{
                txtAccount.Text = prodno;
                txtDescription.Text = prodnm;
                hdnAccount.Value = noprod;
            }
          
            mpe.Hide();
        }
    }
}