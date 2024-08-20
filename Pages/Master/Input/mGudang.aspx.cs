using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.Input
{
    public partial class mGudang : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("mGudang.aspx");
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
        protected void ClearData()
        {
            txtKet.Text = "";
            txtNamaGudang.Text = "";
            cboJenisGudang.Text = "0";
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();


            if (txtkdGudang.Text == "")
            {
                message += ObjSys.CreateMessage("Kode Gudang tidak boleh kosong.");
                valid = false;
            }

            if (txtNamaGudang.Text == "")
            {
                message += ObjSys.CreateMessage("Nama Gudang tidak boleh kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {

                    if (ObjDb.GetRows("select * from Gudang where kdGudang = '" + txtkdGudang.Text + "'").Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Kode Gudang sudah ada.");
                    }

                    if (ObjDb.GetRows("select * from Gudang where namagudang = '" + txtNamaGudang.Text + "'").Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Kode Gudang sudah ada.");
                    }
                    else
                    {
                        //string Kode = ObjSys.GetCodeAutoNumberMasterGudang("60", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                        ObjDb.Data.Add("kdGudang", txtkdGudang.Text);
                        ObjDb.Data.Add("namaGudang", txtNamaGudang.Text);
                        ObjDb.Data.Add("ketGudang", txtKet.Text);
                        ObjDb.Data.Add("actGudang", "A");
                        ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                        ObjDb.Data.Add("stsGudang", cboJenisGudang.Text);
                        ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                        ObjDb.Insert("Gudang", ObjDb.Data);
                        ObjSys.UpdateAutoNumberCode("60", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                    }
                    ClearData();
                    ShowMessage("success", "Data berhasil disimpan.");
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
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}