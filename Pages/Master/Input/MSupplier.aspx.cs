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
    public partial class MSupplier : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ObjSys.SessionCheck("MSupplier.aspx");
                loadDataCombo();
            }
        }

        private void loadDataCombo()
        {
            cboKota.DataSource = ObjDb.GetRows("select 0 as noKota, '---Pilih Kota---' as Kota union all select noKota, Kota from mKota");
            cboKota.DataValueField = "noKota";
            cboKota.DataTextField = "Kota";
            cboKota.DataBind();

            cbosupp.DataSource = ObjDb.GetRows("select 0 as nojnssup, '---Pilih Jenis Supplier---' as Jenissup union all select nojnssup, Jenissup from mJenissupplier");
            cbosupp.DataValueField = "nojnssup";
            cbosupp.DataTextField = "Jenissup";
            cbosupp.DataBind();

        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (namaSupplier.Text == "")
            {
                message += ObjSys.CreateMessage("Nama Supplier tidak boleh kosong.");
                valid = false;
            }

            if (ObjDb.GetRows("SELECT * FROM mSupplier WHERE namaSupplier = '" + namaSupplier.Text + "'").Tables[0].Rows.Count > 0)
            {
                message += ObjSys.CreateMessage("Nama Supplier sudah ada.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                    string Kode = ObjSys.GetCodeAutoNumberNew("6", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kodeSupplier", Kode);
                    ObjGlobal.Param.Add("namaSupplier", namaSupplier.Text);
                    ObjGlobal.Param.Add("npwp", npwp.Text);
                    ObjGlobal.Param.Add("Alamat", Alamat.Text);
                    ObjGlobal.Param.Add("alamatnpwp", AlamatNPWP.Text);
                    ObjGlobal.Param.Add("Kota", cboKota.Text);
                    ObjGlobal.Param.Add("nojnssup", cbosupp.Text);
                    ObjGlobal.Param.Add("telpKantor", telpKantor.Text);
                    ObjGlobal.Param.Add("Fax", Fax.Text);
                    ObjGlobal.Param.Add("email", email.Text);
                    ObjGlobal.Param.Add("namaPIC", namaPIC.Text);
                    ObjGlobal.Param.Add("telpPIC", telpPIC.Text);
                    ObjGlobal.Param.Add("noaccount", noaccount.Text);
                    ObjGlobal.Param.Add("Bank", Bank.Text);
                    ObjGlobal.Param.Add("keterangan", keterangan.Text);
                    ObjGlobal.Param.Add("keterangan1", keterangan1.Text);
                    ObjGlobal.Param.Add("keterangan2", keterangan2.Text);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.GetDataProcedure("SPInsertDatasSupplier", ObjGlobal.Param);
                    ClearData();

                    ObjSys.UpdateAutoNumberCodeNew("6", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
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
            CloseMessage();
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
            namaSupplier.Text = "";
            npwp.Text = "";
            telpKantor.Text = "";
            Fax.Text = "";
            Alamat.Text = "";
            AlamatNPWP.Text = "";
            email.Text = "";
            namaPIC.Text = "";
            telpPIC.Text = "";
            noaccount.Text = "";
            Bank.Text = "";
            keterangan.Text = "";
            keterangan1.Text = "";
            keterangan2.Text = "";
        }
    }
}