using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransBudgetUpdate : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ShowHideGridAndForm(true, false);
                loadDataCombo();
                cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPpilihanthn");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();
                LoadDataAwal();
            }
        }

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void LoadDataAwal()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Tahun", cboYear.SelectedValue);
            ObjGlobal.Param.Add("Cabang", cboCabang.SelectedValue);
            grdBudgetAwal.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBgtUpd", ObjGlobal.Param);
            grdBudgetAwal.DataBind();

            for(int x = 0; x < grdBudgetAwal.Rows.Count; x++)
            {
                HiddenField hdnsts = (HiddenField)grdBudgetAwal.Rows[x].FindControl("hdnsts");
                Button stsApprove = (Button)grdBudgetAwal.Rows[x].FindControl("stsApprove");
                Button stsReject = (Button)grdBudgetAwal.Rows[x].FindControl("stsReject");
                Button stsBlmAppr = (Button)grdBudgetAwal.Rows[x].FindControl("stsBlmAppr");
                if (hdnsts.Value == "1")
                {
                    stsApprove.Visible = true;
                    stsReject.Visible = false;
                    stsBlmAppr.Visible = false;
                }
                else if(hdnsts.Value == "2")
                {
                    stsApprove.Visible = false;
                    stsReject.Visible = true;
                    stsBlmAppr.Visible = false;
                }
                else if (hdnsts.Value == "0")
                {
                    stsApprove.Visible = false;
                    stsReject.Visible = false;
                    stsBlmAppr.Visible = true;
                }
            }
        }

        #region LoadData

        protected void LoadDataAwal2(string tahun, string cabang)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", tahun);
            ObjGlobal.Param.Add("nocabang", cabang);
            grdBudget.DataSource = ObjGlobal.GetDataProcedure("ViewRBudgetUpdate", ObjGlobal.Param);
            grdBudget.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", tahun);
            ObjGlobal.Param.Add("nocabang", cabang);
            GridView1.DataSource = ObjGlobal.GetDataProcedure("ViewRBudgetUpdate", ObjGlobal.Param);
            GridView1.DataBind();

            DataSet mySet = ObjDb.GetRows("select systembudget from parameter ");
            DataRow myRow = mySet.Tables[0].Rows[0];
            string systembudget = myRow["systembudget"].ToString();

            if (systembudget == "Tahun Ajaran")
            {
                pajak.Visible = false;
                tahunAjaran.Visible = true;
            }
            else
            {
                pajak.Visible = true;
                tahunAjaran.Visible = false;
            }
            if (grdBudget.Rows.Count > 0)
                button.Visible = true;
            else
                button.Visible = false;
        }

        protected void loadDataCombo()
        {
            if (ObjSys.GetstsCabang == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'ALL' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

        }
        #endregion

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

                if (valid == true)
                {
                    try
                    {

                        for (int i = 0; i < grdBudget.Rows.Count; i++)
                        {
                            HiddenField hdnnoBudgetD = (HiddenField)grdBudget.Rows[i].FindControl("hdnnoBudgetD");
                            HiddenField hdnnoBudget = (HiddenField)grdBudget.Rows[i].FindControl("hdnnoBudget");
                            HiddenField hdnNoRek = (HiddenField)grdBudget.Rows[i].FindControl("hdnNoRek");
                            TextBox txtJanuari = (TextBox)grdBudget.Rows[i].FindControl("txtJanuari");
                            TextBox txtFebuari = (TextBox)grdBudget.Rows[i].FindControl("txtFebuari");
                            TextBox txtMaret = (TextBox)grdBudget.Rows[i].FindControl("txtMaret");
                            TextBox txtApril = (TextBox)grdBudget.Rows[i].FindControl("txtApril");
                            TextBox txtMei = (TextBox)grdBudget.Rows[i].FindControl("txtMei");
                            TextBox txtJuni = (TextBox)grdBudget.Rows[i].FindControl("txtJuni");
                            TextBox txtJuli = (TextBox)grdBudget.Rows[i].FindControl("txtJuli");
                            TextBox txtAgustus = (TextBox)grdBudget.Rows[i].FindControl("txtAgustus");
                            TextBox txtSeptember = (TextBox)grdBudget.Rows[i].FindControl("txtSeptember");
                            TextBox txtOktober = (TextBox)grdBudget.Rows[i].FindControl("txtOktober");
                            TextBox txtNovember = (TextBox)grdBudget.Rows[i].FindControl("txtNovember");
                            TextBox txtDesember = (TextBox)grdBudget.Rows[i].FindControl("txtDesember");


                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("noBudget", hdnnoBudget.Value);
                            ObjGlobal.Param.Add("noBudgetD", hdnnoBudgetD.Value);
                            ObjGlobal.Param.Add("budget1", Convert.ToDecimal(txtJanuari.Text).ToString());
                            ObjGlobal.Param.Add("budget2", Convert.ToDecimal(txtFebuari.Text).ToString());
                            ObjGlobal.Param.Add("budget3", Convert.ToDecimal(txtMaret.Text).ToString());
                            ObjGlobal.Param.Add("budget4", Convert.ToDecimal(txtApril.Text).ToString());
                            ObjGlobal.Param.Add("budget5", Convert.ToDecimal(txtMei.Text).ToString());
                            ObjGlobal.Param.Add("budget6", Convert.ToDecimal(txtJuni.Text).ToString());
                            ObjGlobal.Param.Add("budget7", Convert.ToDecimal(txtJuli.Text).ToString());
                            ObjGlobal.Param.Add("budget8", Convert.ToDecimal(txtAgustus.Text).ToString());
                            ObjGlobal.Param.Add("budget9", Convert.ToDecimal(txtSeptember.Text).ToString());
                            ObjGlobal.Param.Add("budget10", Convert.ToDecimal(txtOktober.Text).ToString());
                            ObjGlobal.Param.Add("budget11", Convert.ToDecimal(txtNovember.Text).ToString());
                            ObjGlobal.Param.Add("budget12", Convert.ToDecimal(txtDesember.Text).ToString());
                            ObjGlobal.GetDataProcedure("SPUpdateBudgetD", ObjGlobal.Param);
                           
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            ShowMessage("success", "Data berhasil diubah.");
                            this.ShowHideGridAndForm(true, false);
                            LoadDataAwal();
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
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", message);
                }
            
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false);
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


        protected void grdBudgetAwal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdBudgetAwal.PageIndex = e.NewPageIndex;
            LoadDataAwal();
        }

        protected void grdBudgetAwal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectEdit")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    HiddenField hdnThn = (HiddenField)grdBudgetAwal.Rows[rowIndex].FindControl("hdnThn");
                    HiddenField hdnCbg = (HiddenField)grdBudgetAwal.Rows[rowIndex].FindControl("hdnCbg");

                    LoadDataAwal2(hdnThn.Value, hdnCbg.Value);
                    this.ShowHideGridAndForm(false, true);
                }
                else if (e.CommandName == "SelectDelete")
                { 
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    HiddenField hdnThn = (HiddenField)grdBudgetAwal.Rows[rowIndex].FindControl("hdnThn");
                    HiddenField hdnCbg = (HiddenField)grdBudgetAwal.Rows[rowIndex].FindControl("hdnCbg");

                    DataSet mySetH2 = ObjDb.GetRows("select noBudget from tBudget_D "+
                        "where noBudget in (select noBudget from tBudget_H where thn='" + hdnThn.Value + "' "+
                        "and nocabang='" + hdnCbg.Value + "')");
                    if (mySetH2.Tables[0].Rows.Count > 0)
                    {
                        DataRow myRowH2 = mySetH2.Tables[0].Rows[0];
                        string noBudget = myRowH2["noBudget"].ToString();
                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("noBudget", noBudget);
                        ObjDb.Delete("tbudget_D", ObjDb.Where);
                    }

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("thn", hdnThn.Value);
                    ObjDb.Where.Add("nocabang", hdnCbg.Value);
                    ObjDb.Delete("tbudget_H", ObjDb.Where);


                    LoadDataAwal();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Success Deleted");
                    this.ShowHideGridAndForm(true, false);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDataAwal();
            CloseMessage();
        }
    }
    
}
