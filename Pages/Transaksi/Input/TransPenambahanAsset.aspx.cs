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
    public partial class TransPenambahanAsset : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCombo();
            }
        }
        protected void LoadCombo()
        {
            cboLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noLokasi id, Lokasi name FROM mLokasi ) a");
            cboLokasi.DataValueField = "id";
            cboLokasi.DataTextField = "name";
            cboLokasi.DataBind();
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
            LoadCombo();
            txtNamaBarang.Text = "";
            txtNamaAsset.Text = "";
            cboLokasi.Text = "0";
            txtNilai.Text = "";
            txtLokasi.Text = "";
            txtUraian.Text = "";
        }

        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi where noLokasi = '" + cboLokasi.Text + "') a");
            cboSubLokasi.DataValueField = "id";
            cboSubLokasi.DataTextField = "name";
            cboSubLokasi.DataBind();
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (txtNamaAsset.Text == "")
            {
                message += ObjSys.CreateMessage("Nama Asset tidak boleh kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {

                    string Kode = ObjSys.GetCodeAutoNumberNew("104", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kdtambahAsset", Kode);
                    ObjGlobal.Param.Add("tgl", dtTanggal.Text);
                    ObjGlobal.Param.Add("noAsset", hdnNoAsset.Value);
                    ObjGlobal.Param.Add("noSubLokasi", cboSubLokasi.Text);
                    ObjGlobal.Param.Add("nolokasi", cboLokasi.Text);
                    ObjGlobal.Param.Add("nilaiTambah", Convert.ToDecimal(txtNilai.Text).ToString());
                    ObjGlobal.Param.Add("nilaibukusblm", Convert.ToDecimal(txtNilai.Text).ToString());
                    ObjGlobal.Param.Add("keterangan", txtUraian.Text);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPInsertTransTambahAsset", ObjGlobal.Param);
                    ObjSys.UpdateAutoNumberCodeNew("104", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));


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

        protected void btnResetRek_Click(object sender, EventArgs e)
        {
            clearData();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataSet mySet = ObjDb.GetRows("select * from tAsset a inner join mLokasi b on a.nolokasi = b.nolokasi inner join mSubLokasi c on a.noSubLokasi = c.noSubLokasi where a.noLokasi = '" + cboLokasi.Text + "' and a.noSubLokasi = '" + cboSubLokasi.Text + "' and a.noCabang = '" + ObjSys.GetCabangId + "' and (a.namaAsset like '%" + txtNamaBarang.Text + "%' or a.kodeAsset like '%" + txtNamaBarang.Text + "%')");
            DataRow myRow = mySet.Tables[0].Rows[0];
            string namaAsset = myRow["namaAsset"].ToString();
            string lokasi = myRow["Lokasi"].ToString();
            string SubLokasi = myRow["SubLokasi"].ToString();
            string noAsset = myRow["noAset"].ToString();

            txtNamaAsset.Text = namaAsset;
            hdnNoAsset.Value = noAsset;
            txtLokasi.Text = lokasi + "/" + SubLokasi;
        }
    }
}