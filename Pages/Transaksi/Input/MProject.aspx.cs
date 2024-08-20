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
    public partial class MProject : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ObjSys.SessionCheck("MProject.aspx");
                //LoadDataCombo();
            }
        }

      

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (valid == true)
            {

                if (ObjDb.GetRows("SELECT * FROM mProject WHERE Project = '" + txtNama.Text + "' and noKontrak = '" + Textnokontrak.Text + "' ").Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Project sudah ada.");
                }
                else
                {
                    try
                    {
                        string Kode = ObjSys.GetCodeAutoNumberMaster("30", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("KodeProject", Kode);
                        ObjGlobal.Param.Add("Project", txtNama.Text);
                        ObjGlobal.Param.Add("tglProject", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("noKontrak", Textnokontrak.Text);
                        ObjGlobal.Param.Add("keterangan", txtUraian.Text);
                        ObjGlobal.Param.Add("stsProject", "1");
                        ObjGlobal.Param.Add("Nilai", Convert.ToDecimal(txtValue.Text).ToString());
                        ObjGlobal.Param.Add("stsApp", "0");
                        ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.GetDataProcedure("SPInsertProject", ObjGlobal.Param);

                        ClearData();
                        ObjSys.UpdateAutoNumberCode("30", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil disimpan.");
                    }
                    catch (Exception ex)
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
            ClearData();
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

        protected void ClearData()
        {
          
            txtNama.Text = "";
            txtUraian.Text = "";
            dtKas.Text = "";
            Textnokontrak.Text = "";
            txtUraian.Text = "";
            txtValue.Text = "0";
            CloseMessage();
        }
    }
}