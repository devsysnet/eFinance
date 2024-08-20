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

namespace eFinance.Pages.Transaksi.Posting
{
    public partial class PostingInvoice : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected string execBind = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }
        private void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearch.Text);
            grdPosting.DataSource = ObjGlobal.GetDataProcedure("SPLoadDatatPostingInvoice", ObjGlobal.Param);
            grdPosting.DataBind();

            if (grdPosting.Rows.Count > 0)
                button.Visible = true;
            else
                button.Visible = false;
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
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
        protected void btnReset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdPosting.Rows.Count; i++)
            {
                CheckBox chkCheck = (CheckBox)grdPosting.Rows[i].FindControl("chkCheck");
                chkCheck.Checked = false;
            }
        }

        protected void grdPosting_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdPosting.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnPosting_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "";


            try
            {
                int countSelect = 0;
                for (int i = 0; i < grdPosting.Rows.Count; i++)
                {
                    CheckBox chkCheck = (CheckBox)grdPosting.Rows[i].FindControl("chkCheck");
                    if (chkCheck.Checked == true)
                    {
                        countSelect++;
                    }
                }
                if (valid == true)
                {
                    if (countSelect == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Tidak ada data yang dipilih.");
                    }
                    else
                    {
                        for (int g = 0; g < grdPosting.Rows.Count; g++)
                        {
                            CheckBox chkCheck = (CheckBox)grdPosting.Rows[g].FindControl("chkCheck");

                            if (chkCheck.Checked == true)
                            {
                                HiddenField hdnnoInv = (HiddenField)grdPosting.Rows[g].FindControl("hdnnoInv");

                                //ObjGlobal.Param.Clear();
                                //ObjGlobal.Param.Add("noInv", hdnnoInv.Value);
                                //DataTable data = ObjGlobal.GetDataProcedureDataTable("SPLoadDatatPostingDODetail", ObjGlobal.Param);
                                //foreach (DataRow myRow in data.Rows)
                                //{
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("noInv", hdnnoInv.Value);
                                ObjGlobal.GetDataProcedure("SPInsertPiutang", ObjGlobal.Param);

                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("noInv", hdnnoInv.Value);
                                ObjGlobal.GetDataProcedure("SPInsertJurnalPiutang1", ObjGlobal.Param);

                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("noInv", hdnnoInv.Value);
                                ObjGlobal.GetDataProcedure("SPUpdatestsInv", ObjGlobal.Param);



                                //}
                            }
                        }

                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil.");
                        loadData();
                    }
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
    }
}