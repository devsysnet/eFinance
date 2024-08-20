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
    public partial class PostingHarianGLNew1 : System.Web.UI.Page
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
            cboYear.DataSource = ObjGlobal.GetDataProcedure("SPLoaddataposthnall", ObjGlobal.Param);
            cboYear.DataValueField = "tgl";
            cboYear.DataTextField = "bulan";
            cboYear.DataBind();

            ObjGlobal.Param.Clear();
            cbobln.DataSource = ObjGlobal.GetDataProcedure("SPLoaddataposblnall", ObjGlobal.Param);
            cbobln.DataValueField = "tgl";
            cbobln.DataTextField = "bulan";
            cbobln.DataBind();
        }


        protected void btnPosting_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", StrKode = "", alert = "", nocabang1 = "";

            if (ObjDb.GetRows("select distinct sts from tsaldobln where sts=1 and month(tgl)='" + cbobln.Text + "' and year(tgl)='" + cboYear.Text + "'").Tables[0].Rows.Count <= 0)
            {
                message = ObjSys.CreateMessage("Belum Posting Akhir Bulan");
                alert = "error";
                valid = false;
            }

            else
            {
                if (ObjDb.GetRows("select distinct sts from tkasdetil where sts=1  and month(tgl)='" + cbobln.Text + "' and year(tgl)='" + cboYear.Text + "'").Tables[0].Rows.Count > 0)
                {
                    message = ObjSys.CreateMessage("Data sudah Diposting");
                    alert = "error";
                    valid = false;

                }
            }

            //try
            //{
               

                if (valid == true)
                {
                  
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("thn1", cboYear.Text);
                        ObjGlobal.Param.Add("bln1", cbobln.Text);
                        ObjGlobal.ExecuteProcedure("SPProsesPostingHarianGLALL", ObjGlobal.Param);
                       
                      
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil diposting");
                        LoadData();
                 }

              
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage(alert, message);
                }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            //    ShowMessage("error", ex.ToString());
            //}
        }
    }
}