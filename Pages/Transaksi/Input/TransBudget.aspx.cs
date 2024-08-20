using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransBudget : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loaddata();
            }
        }
        protected void loaddata()
        {
            cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPpilihanthn1");
            cboYear.DataValueField = "id";
            cboYear.DataTextField = "name";
            cboYear.DataBind();

          
            ObjGlobal.Param.Clear();
            grdBudget.DataSource = ObjGlobal.GetDataProcedure("SPViewInsertBudget", ObjGlobal.Param);
            grdBudget.DataBind();

            ObjGlobal.Param.Clear();
            grdBudget1.DataSource = ObjGlobal.GetDataProcedure("SPViewInsertBudget", ObjGlobal.Param);
            grdBudget1.DataBind();

            DataSet mySet = ObjDb.GetRows("select systembudget from parameter ");
            DataRow myRow = mySet.Tables[0].Rows[0];
            string systembudget = myRow["systembudget"].ToString();

            if(systembudget == "Tahun Ajaran")
            {
                pajak.Visible = false;
                tahunAjaran.Visible = true;
            }
            else
            {
                pajak.Visible = true;
                tahunAjaran.Visible = false;
            }
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (ObjDb.GetRows("select * from tBudget_H where nocabang = '" + ObjSys.GetCabangId + "' and thn='" + cboYear.Text + "'").Tables[0].Rows.Count > 0)
            {
               
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Budget Sudah Diinput");
            }
            else
            {

                if (valid == true)
            {
                try
                {
                  
                        for (int i = 0; i < grdBudget.Rows.Count; i++)
                        {
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

                            HiddenField hdnNoRek1 = (HiddenField)grdBudget1.Rows[i].FindControl("hdnNoRek");
                            TextBox txtJanuari1 = (TextBox)grdBudget1.Rows[i].FindControl("txtJanuari");
                            TextBox txtFebuari1 = (TextBox)grdBudget1.Rows[i].FindControl("txtFebuari");
                            TextBox txtMaret1 = (TextBox)grdBudget1.Rows[i].FindControl("txtMaret");
                            TextBox txtApril1 = (TextBox)grdBudget1.Rows[i].FindControl("txtApril");
                            TextBox txtMei1 = (TextBox)grdBudget1.Rows[i].FindControl("txtMei");
                            TextBox txtJuni1 = (TextBox)grdBudget1.Rows[i].FindControl("txtJuni");
                            TextBox txtJuli1= (TextBox)grdBudget1.Rows[i].FindControl("txtJuli");
                            TextBox txtAgustus1 = (TextBox)grdBudget1.Rows[i].FindControl("txtAgustus");
                            TextBox txtSeptember1 = (TextBox)grdBudget1.Rows[i].FindControl("txtSeptember");
                            TextBox txtOktober1 = (TextBox)grdBudget1.Rows[i].FindControl("txtOktober");
                            TextBox txtNovember1 = (TextBox)grdBudget1.Rows[i].FindControl("txtNovember");
                            TextBox txtDesember1 = (TextBox)grdBudget1.Rows[i].FindControl("txtDesember");

                            DataSet mySet = ObjDb.GetRows("select systembudget from parameter ");
                            DataRow myRow = mySet.Tables[0].Rows[0];
                            string systembudget = myRow["systembudget"].ToString();

                            if (systembudget == "Tahun Ajaran")
                            {
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("thn", cboYear.Text);
                                ObjGlobal.Param.Add("norek", hdnNoRek1.Value);
                                ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                                ObjGlobal.Param.Add("keterangan", "sa");
                                ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                                ObjGlobal.GetDataProcedure("SPInsertBudgetH", ObjGlobal.Param);

                                DataSet mySet1 = ObjDb.GetRows("select * from tBudget_H where norek = '" + hdnNoRek1.Value + "' and nocabang='" + ObjSys.GetCabangId + "' and thn='" + cboYear.Text + "'");
                                DataRow myRow1 = mySet1.Tables[0].Rows[0];
                                string noPO1 = myRow1["noBudget"].ToString();

                                string jan = "0", feb = "0", mrt = "0", apr = "0", mei = "0", jun = "0", jul = "0", ags = "0", spt = "0", okt = "0", nov = "0", des = "0";
                                if (txtJanuari1.Text != "")
                                    jan = txtJanuari1.Text;
                                if (txtFebuari1.Text != "")
                                    feb = txtFebuari1.Text;
                                if (txtMaret1.Text != "")
                                    mrt = txtMaret1.Text;
                                if (txtApril1.Text != "")
                                    apr = txtApril1.Text;
                                if (txtMei1.Text != "")
                                    mei = txtMei1.Text;
                                if (txtJuni1.Text != "")
                                    jun = txtJuni1.Text;
                                if (txtJuli1.Text != "")
                                    jul = txtJuli1.Text;
                                if (txtAgustus1.Text != "")
                                    ags = txtAgustus1.Text;
                                if (txtSeptember1.Text != "")
                                    spt = txtSeptember1.Text;
                                if (txtOktober1.Text != "")
                                    okt = txtOktober1.Text;
                                if (txtNovember1.Text != "")
                                    nov = txtNovember1.Text;
                                if (txtDesember1.Text != "")
                                    des = txtDesember1.Text;

                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("noBudget", noPO1);
                                ObjGlobal.Param.Add("budget1", Convert.ToDecimal(jan).ToString());
                                ObjGlobal.Param.Add("budget2", Convert.ToDecimal(feb).ToString());
                                ObjGlobal.Param.Add("budget3", Convert.ToDecimal(mrt).ToString());
                                ObjGlobal.Param.Add("budget4", Convert.ToDecimal(apr).ToString());
                                ObjGlobal.Param.Add("budget5", Convert.ToDecimal(mei).ToString());
                                ObjGlobal.Param.Add("budget6", Convert.ToDecimal(jun).ToString());
                                ObjGlobal.Param.Add("budget7", Convert.ToDecimal(jul).ToString());
                                ObjGlobal.Param.Add("budget8", Convert.ToDecimal(ags).ToString());
                                ObjGlobal.Param.Add("budget9", Convert.ToDecimal(spt).ToString());
                                ObjGlobal.Param.Add("budget10", Convert.ToDecimal(okt).ToString());
                                ObjGlobal.Param.Add("budget11", Convert.ToDecimal(nov).ToString());
                                ObjGlobal.Param.Add("budget12", Convert.ToDecimal(des).ToString());
                                ObjGlobal.GetDataProcedure("SPInsertBudgetD", ObjGlobal.Param);
                            }
                            else
                            {
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("thn", cboYear.Text);
                                ObjGlobal.Param.Add("norek", hdnNoRek.Value);
                                ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                                ObjGlobal.Param.Add("keterangan", "sa");
                                ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                                ObjGlobal.GetDataProcedure("SPInsertBudgetH", ObjGlobal.Param);

                                DataSet mySet2 = ObjDb.GetRows("select * from tBudget_H where norek = '" + hdnNoRek.Value + "' and nocabang='" + ObjSys.GetCabangId + "' and thn='" + cboYear.Text + "'");
                                DataRow myRow2 = mySet2.Tables[0].Rows[0];
                                string noPO = myRow2["noBudget"].ToString();

                                string jan = "0", feb = "0", mrt = "0", apr = "0", mei = "0", jun = "0", jul = "0", ags = "0", spt = "0", okt = "0", nov = "0", des = "0";
                                if (txtJanuari.Text != "")
                                    jan = txtJanuari.Text;
                                if (txtFebuari.Text != "")
                                    feb = txtFebuari.Text;
                                if (txtMaret.Text != "")
                                    mrt = txtMaret.Text;
                                if (txtApril.Text != "")
                                    apr = txtApril.Text;
                                if (txtMei.Text != "")
                                    mei = txtMei.Text;
                                if (txtJuni.Text != "")
                                    jun = txtJuni.Text;
                                if (txtJuli.Text != "")
                                    jul = txtJuli.Text;
                                if (txtAgustus.Text != "")
                                    ags = txtAgustus.Text;
                                if (txtSeptember.Text != "")
                                    spt = txtSeptember.Text;
                                if (txtOktober.Text != "")
                                    okt = txtOktober.Text;
                                if (txtNovember.Text != "")
                                    nov = txtNovember.Text;
                                if (txtDesember.Text != "")
                                    des = txtDesember.Text;

                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("noBudget", noPO);
                                ObjGlobal.Param.Add("budget1", Convert.ToDecimal(jan).ToString());
                                ObjGlobal.Param.Add("budget2", Convert.ToDecimal(feb).ToString());
                                ObjGlobal.Param.Add("budget3", Convert.ToDecimal(mrt).ToString());
                                ObjGlobal.Param.Add("budget4", Convert.ToDecimal(apr).ToString());
                                ObjGlobal.Param.Add("budget5", Convert.ToDecimal(mei).ToString());
                                ObjGlobal.Param.Add("budget6", Convert.ToDecimal(jun).ToString());
                                ObjGlobal.Param.Add("budget7", Convert.ToDecimal(jul).ToString());
                                ObjGlobal.Param.Add("budget8", Convert.ToDecimal(ags).ToString());
                                ObjGlobal.Param.Add("budget9", Convert.ToDecimal(spt).ToString());
                                ObjGlobal.Param.Add("budget10", Convert.ToDecimal(okt).ToString());
                                ObjGlobal.Param.Add("budget11", Convert.ToDecimal(nov).ToString());
                                ObjGlobal.Param.Add("budget12", Convert.ToDecimal(des).ToString());
                                ObjGlobal.GetDataProcedure("SPInsertBudgetD", ObjGlobal.Param);
                            }
                            

                            ShowMessage("success", "Data berhasil disimpan.");
                        clearData();
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
        protected void clearData()
        {
           
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
    }

}
