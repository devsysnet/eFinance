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
    public partial class TransPengumuman : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

          

            }
        }
   
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            
            if (txtUraian.Text == "0")
            {
                message += ObjSys.CreateMessage("Uraian harus diisi.");
                valid = false;
            }
            if (drTanggal.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal harus diisi.");
                valid = false;
            }
            if (sdTanggal.Text == "")
            {
                message += ObjSys.CreateMessage("Nilai harus diisi.");
                valid = false;
            }
            
            if (valid == true)
            {
                try
                {

                    //string Kode = ObjSys.GetCodeAutoNumberNew("4", Convert.ToDateTime(dtTanggal.Text).ToString("yyyy-MM-dd"));

                    ObjDb.Data.Clear();
                    ObjDb.Data.Add("tglmulai", drTanggal.Text);
                    ObjDb.Data.Add("tglselesai", sdTanggal.Text);
                    ObjDb.Data.Add("jns", jns.Text);
                    ObjDb.Data.Add("uraian", txtUraian.Text);
                    ObjDb.Data.Add("nocabang", ObjSys.GetCabangId);
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                    ObjDb.Insert("TransPengumumanPerusahaan", ObjDb.Data);
                    
                    //ObjSys.UpdateAutoNumberCodeNew("4", Convert.ToDateTime(dtTanggal.Text).ToString("yyyy-MM-dd"));

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                    clearData();

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
            drTanggal.Text = "";
            sdTanggal.Text = "";
            jns.Text = "";
            txtUraian.Text = "";
            
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            clearData();
        }

     
    }
}