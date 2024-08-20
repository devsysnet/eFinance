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
    public partial class TransGiro : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                showhidekode.Visible = false;
            }
        }

        protected void loadDataCombo()
        {
            cbojenissrt.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nojnsasset id, Jenis name FROM MstJenisasset where sts=1) a order by a.id");
            cbojenissrt.DataValueField = "id";
            cbojenissrt.DataTextField = "name";
            cbojenissrt.DataBind();

            cboRek.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct norek id, ket name FROM mrekening where jenis=40 and stsaktif=1 and nocabang='"+ ObjSys.GetCabangId + "' ) a");
            cboRek.DataValueField = "id";
            cboRek.DataTextField = "name";
            cboRek.DataBind();

        
        }

        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbojenistransaksi.Text == "2")
                showhidekode.Visible = true;
            else
                showhidekode.Visible = false;


            cborekeningbank.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct norek id, ket name FROM mrekening where jenis=2 and stsaktif=1 and nocabang='" + ObjSys.GetCabangId + "' ) a");
            cborekeningbank.DataValueField = "id";
            cborekeningbank.DataTextField = "name";
            cborekeningbank.DataBind();

        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (nomorGiro.Text == "")
            {
                message += ObjSys.CreateMessage("Nomor Giro harus diisi.");
                valid = false;
            }
            if (tglGiro.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal Giro harus diisi.");
                valid = false;
            }
            if (tglJatuhTempo.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal Jatuh Tempo harus diisi.");
                valid = false;
            }
            if (nominal.Text == "")
            {
                message += ObjSys.CreateMessage("Nominal harus diisi.");
                valid = false;
            }
            if (cboRek.Text == "")
            {
                message += ObjSys.CreateMessage("Akun harus dipilih.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {

                    string Kode = ObjSys.GetCodeAutoNumberNew("30", Convert.ToDateTime(tglGiro.Text).ToString("yyyy-MM-dd"));

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kdGiro", Kode);
                    ObjGlobal.Param.Add("jenistransaksi", cbojenistransaksi.Text);
                    ObjGlobal.Param.Add("cbojenissrt", cbojenissrt.Text);
                    ObjGlobal.Param.Add("norekbank", cborekeningbank.Text);
                    ObjGlobal.Param.Add("jenis", cbojenissrt.Text);
                    ObjGlobal.Param.Add("nomor", nomorGiro.Text);
                    ObjGlobal.Param.Add("Namabank", namabank.Text);
                    ObjGlobal.Param.Add("tglDeposito", tglGiro.Text);
                    ObjGlobal.Param.Add("tglJatuhTempo", tglJatuhTempo.Text);
                    ObjGlobal.Param.Add("nominal", Convert.ToDecimal(nominal.Text).ToString());
                    ObjGlobal.Param.Add("jnsbunga", cbojnsbunga.Text);
                    ObjGlobal.Param.Add("bunga", Convert.ToDecimal(bunga.Text).ToString());
                    ObjGlobal.Param.Add("norek", cboRek.Text);
                    ObjGlobal.Param.Add("deskripsi", deskripsi.Text);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPInsertGiro", ObjGlobal.Param);

                    ObjSys.UpdateAutoNumberCodeNew("30", Convert.ToDateTime(tglGiro.Text).ToString("yyyy-MM-dd"));

                    //clearData();

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                   

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
            nomorGiro.Text = "";
            tglGiro.Text = "";
            tglJatuhTempo.Text = "";
            nominal.Text = "0";
            cbojenissrt.Text = "";
            deskripsi.Text = "";
            namabank.Text = "";
           
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            clearData();
        }

        protected void btnBrowseAsset_Click(object sender, ImageClickEventArgs e)
        {
            CloseMessage();
            LoadDataAsset();
            dlgAddData.Show();
        }

        protected void LoadDataAsset()
        {
            grdDataAsset.DataSource = ObjDb.GetRows("select noRek, ket, kdRek from mrekening where jenis = 2 and (ket like '%" + txtSearch.Text + "%' or kdRek like '%" + txtSearch.Text + "%')");

            grdDataAsset.DataBind();
           
        }
        protected void btnAsset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataAsset();
            dlgAddData.Show();
        }

        protected void grdDataAsset_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRow = grdDataAsset.SelectedIndex;
            namabank.Text = grdDataAsset.Rows[selectedRow].Cells[2].Text;
            //hdnnorek.Value = (grdDataAsset.SelectedRow.FindControl("hdnnorek") as HiddenField).Value;

        }

        protected void grdDataAsset_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdDataAsset.PageIndex = e.NewPageIndex;
            LoadDataAsset();
            dlgAddData.Show();
        }

       
    }
}