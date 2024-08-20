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
    public partial class PostingARTutupthn : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();

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

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            cboMonth.DataSource = ObjGlobal.GetDataProcedure("SPLoaddatathnajaranbaru");
            cboMonth.DataValueField = "thnlama";
            cboMonth.DataTextField = "thnlama";
            cboMonth.DataBind();

            ObjGlobal.Param.Clear();
            cboYear.DataSource = ObjGlobal.GetDataProcedure("SPLoaddatathnajaranlama");
            cboYear.DataValueField = "thn";
            cboYear.DataTextField = "thn";
            cboYear.DataBind();
        }


        protected void btnPosting_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (valid == true)
            {
                //if (ObjDb.GetRows("select distinct MONTH(tgl) from TransPiutang a inner join mJenisTransaksi b on a.noTransaksi=b.noTransaksi where b.posbln=1 and b.sts=1 and a.nocabang='" + ObjSys.GetCabangId + "' and month(a.tgl)='" + cboMonth.Text + "' and Year(a.tgl)='" + cboYear.Text + "'").Tables[0].Rows.Count > 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //    ShowMessage("error", "Bulan Ini Sudah Pernah DiPosting");
                //}

                //else
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("thn", cboYear.Text);
                    ObjGlobal.Param.Add("bln", cboMonth.Text);
                    ObjGlobal.Param.Add("tgl", Convert.ToDateTime(cboYear.Text + '/' + cboMonth.Text + '/' + 1).ToString("yyyy-MMM-dd"));
                    ObjGlobal.Param.Add("tgljttempo", Convert.ToDateTime(cboYear.Text + '/' + cboMonth.Text + '/' + 10).ToString("yyyy-MMM-dd"));
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.ExecuteProcedure("SPProsesARbulanan", ObjGlobal.Param);

                    LoadData();
                    showHideForm(true, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil di posting");

                }
            }
        }

        protected void showHideForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

    }
}
