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
    public partial class MstCabang : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstCabang.aspx");
                ClearData();
                LoadDataCombo();
            }
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (txtNama.Text == "")
            {
                message += ObjSys.CreateMessage("Nama cabang tidak boleh kosong.");
                valid = false;
            }

            if (ObjDb.GetRows("SELECT * FROM mCabang WHERE namaCabang = '" + txtNama.Text + "'").Tables[0].Rows.Count > 0)
            {
                message += ObjSys.CreateMessage("Nama Cabang sudah ada.");
                valid = false;
            }

            if (valid == true)
            {

                try
                {
                    string Kode = ObjSys.GetCodeAutoNumberMaster("58", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kode", Kode);
                    ObjGlobal.Param.Add("nama", txtNama.Text);
                    ObjGlobal.Param.Add("alamat", txtAlamat.Text);
                    ObjGlobal.Param.Add("Telp", txtNoTelp.Text);
                    ObjGlobal.Param.Add("noFax", txtNoFax.Text);
                    ObjGlobal.Param.Add("kodePOS", txtKodePos.Text);
                    ObjGlobal.Param.Add("email", txtEmail.Text);
                    ObjGlobal.Param.Add("kota", txtKota.Text);
                    ObjGlobal.Param.Add("office", txtOffice.Text);
                    if (cboStatus.Text == "0")
                    {
                        ObjGlobal.Param.Add("stsPusat", "1");
                        ObjGlobal.Param.Add("stsCabang", "0");
                        ObjGlobal.Param.Add("nourut", "1");
                    }
                    if (cboStatus.Text == "1")
                    {
                        ObjGlobal.Param.Add("stsPusat", "0");
                        ObjGlobal.Param.Add("stsCabang", "1");
                        ObjGlobal.Param.Add("nourut", "2");
                    }
                    if (cboStatus.Text == "2")
                    {
                        ObjGlobal.Param.Add("stsPusat", "0");
                        ObjGlobal.Param.Add("stsCabang", "2");
                        ObjGlobal.Param.Add("nourut", "3");
                    }
                    if (cboStatus.Text == "3")
                    {
                        ObjGlobal.Param.Add("stsPusat", "0");
                        ObjGlobal.Param.Add("stsCabang", "3");
                        ObjGlobal.Param.Add("nourut", "3");
                    }
                    if (cboStatus.Text == "4")
                    {
                        ObjGlobal.Param.Add("stsPusat", "0");
                        ObjGlobal.Param.Add("stsCabang", "3");
                        ObjGlobal.Param.Add("nourut", "3");
                    }
                    ObjGlobal.Param.Add("jnssekolah", cboUnit.Text);
                    ObjGlobal.Param.Add("parent", cboParent.Text);
                    ObjGlobal.Param.Add("mhs", mhs.Text);
                    ObjGlobal.Param.Add("allPelunasan", allPelunasan.Text);
                    ObjGlobal.Param.Add("cetakVoucher", cetakVoucher.Text);
                    ObjGlobal.Param.Add("Kategori", Kategori.Text);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPInsertCabang", ObjGlobal.Param);

                    ClearData();
                    ObjSys.UpdateAutoNumberCode("58", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
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
            txtAlamat.Text = "";
            txtEmail.Text = "";
            txtKodePos.Text = "";
            txtKota.Text = "";
            txtNama.Text = "";
            txtNoFax.Text = "";
            txtNoTelp.Text = "";
            txtOffice.Text = "";
            cboStatus.Text = "";
        }

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ObjDb.GetRows("SELECT * FROM mCabang WHERE stsCabang = '" + cboStatus.Text + "'").Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Cabang Pusat sudah ada.");
            }
        }

        protected void LoadDataCombo()
        {
            cboParent.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noCabang id, namaCabang name FROM mCabang where stscabang = '0' or stscabang = '1' ) a");
            cboParent.DataValueField = "id";
            cboParent.DataTextField = "name";
            cboParent.DataBind();

        }
    }
}