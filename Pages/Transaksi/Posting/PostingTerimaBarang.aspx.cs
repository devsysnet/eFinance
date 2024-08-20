using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Posting
{
    public partial class PostingTerimaBarang : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();

            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("transaksi", cboTransaction.Text);
            grdReceive.DataSource = ObjGlobal.GetDataProcedure("SPPostTerimaBarang", ObjGlobal.Param);
            grdReceive.DataBind();

            if (grdReceive.Rows.Count > 0)
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

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string message = "";
            try
            {
                int count = 0;
                for (int g = 0; g < grdReceive.Rows.Count; g++)
                {
                    CheckBox check = (CheckBox)grdReceive.Rows[g].FindControl("chkCheck");

                    if (check.Checked == true)
                    {
                        count = 1;
                    }
                }
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Belum di Pilih.");
                }
                else
                {
                    for (int i = 0; i < grdReceive.Rows.Count; i++)
                    {
                        CheckBox check = (CheckBox)grdReceive.Rows[i].FindControl("chkCheck");
                        HiddenField hdnnoTerima = (HiddenField)grdReceive.Rows[i].FindControl("hdnnoTerima");
                        HiddenField hdnjenis = (HiddenField)grdReceive.Rows[i].FindControl("hdnjenis");
                        if (check.Checked == true)
                        {
                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("nosuratjalan", hdnnoTerima.Value);
                            ObjGlobal.Param.Add("jenis", hdnjenis.Value);
                            ObjGlobal.GetDataProcedure("SPInsertHutangReceive", ObjGlobal.Param);   
                        }
                    }

                    loadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil di posting.");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
                
            }
        }

        protected void cboTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void grdReceive_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4")
                {
                    e.Row.Cells[8].Text = "Nama Barang";
                    e.Row.Cells[9].Text = "Qty Terima";
                }
                else if (cboTransaction.SelectedValue == "3")
                {
                    e.Row.Cells[8].Text = "Nama Jasa";
                    e.Row.Cells[9].Text = "Nilai Terima";
                }
            }
        }
    }
}