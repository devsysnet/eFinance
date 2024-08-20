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
    public partial class TransAdditionalBudget : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDataParent();
                dtAdd.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            }
        }
        protected void LoadDataParent()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearch.Text);
            grdRekening.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAdditional", ObjGlobal.Param);
            grdRekening.DataBind();
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
        protected void imgButtonProduct_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataParent();
            dlgParentAccount.Show();
        }

        protected void grdRekening_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdRekening.PageIndex = e.NewPageIndex;
            LoadDataParent();
            dlgParentAccount.Show();
        }

        protected void grdRekening_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = grdRekening.SelectedRow.RowIndex;
                string Id = grdRekening.DataKeys[rowIndex].Values[0].ToString();
                string kdRek = grdRekening.SelectedRow.Cells[1].Text;

                hdnNoRek.Value = Id;
                txtRekening.Text = kdRek;
                LoadDataParent();
                dlgParentAccount.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataParent();
            dlgParentAccount.Show();
        }
        protected void clearData()
        {
            txtRemark.Text = "";
            txtRekening.Text = "";
            txtNilai.Text = "";
            dtAdd.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            hdnNoRek.Value = "";


        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();
            
            if (valid == true)
            {
                try
                {
                    if (txtRekening.Text != "")
                    {
                        string Kode = ObjSys.GetCodeAutoNumberNew("106", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                        ObjDb.Data.Clear();
                        ObjDb.Data.Add("kdAdditional", Kode);
                        ObjDb.Data.Add("tgl", dtAdd.Text);
                        ObjDb.Data.Add("norek", hdnNoRek.Value);
                        ObjDb.Data.Add("nocabag", ObjSys.GetCabangId);
                        ObjDb.Data.Add("nilai", Convert.ToDecimal(txtNilai.Text).ToString());
                        ObjDb.Data.Add("remaks", txtRemark.Text);
                        ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("cretedDate", ObjSys.GetNow);
                        ObjDb.Insert("tAdditinalBudget", ObjDb.Data);
                        ObjSys.UpdateAutoNumberCodeNew("106", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));

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

        protected void btnResetRek_Click(object sender, EventArgs e)
        {
            clearData();
        }
    }
}