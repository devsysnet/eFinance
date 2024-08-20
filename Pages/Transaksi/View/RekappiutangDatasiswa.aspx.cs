using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.View
{
    public partial class RekappiutangDatasiswa : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                loadDataKelas();
                //LoadData(cboCabang.Text, cboKelas.Text, txtSearch.Text);
            }
        }

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("kelas", cboKelas.Text);
            ObjGlobal.Param.Add("noCabang", cboCabang.Text);
            ObjGlobal.Param.Add("notransaksi", cbojnstransaksi.Text);
            grdSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataSiswarekap", ObjGlobal.Param);
            grdSiswa.DataBind();

        }

        protected void loadDataCombo()
        {
            cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();
            loadDataKelas();

            cbojnstransaksi.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct notransaksi id,jenistransaksi name FROM mJenistranski where nocabang='" + ObjSys.GetCabangId + "') a");
            cbojnstransaksi.DataValueField = "id";
            cbojnstransaksi.DataTextField = "name";
            cbojnstransaksi.DataBind();
        }

        protected void loadDataKelas()
        {
            cboKelas.DataSource = ObjDb.GetRows("select * from (select '' as id, '---Semua Kelas---' as name union select distinct kelas as id, kelas as name from TransKelas where nocabang='" + cboCabang.Text + "')x");
            cboKelas.DataValueField = "id";
            cboKelas.DataTextField = "name";
            cboKelas.DataBind();
        }

        protected void grdSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdSiswa.PageIndex = e.NewPageIndex;
            //LoadData(cboCabang.Text, cboKelas.Text, txtSearch.Text);
        }

        protected void grdSiswa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = grdSiswa.SelectedRow.RowIndex;
                string noSiswa = grdSiswa.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noSiswa;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noSiswa", hdnId.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataSiswa_Detail", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                txtNama.Text = myRow["namaSiswa"].ToString();
                txtNIK.Text = myRow["nik"].ToString();
                txtNIS.Text = myRow["nis"].ToString();
                txtNISN.Text = myRow["nisn"].ToString();
                txtJK.Text = myRow["jk"].ToString();
                txtAgama.Text = myRow["agama"].ToString();
                txtAlamat.Text = myRow["alamat"].ToString();
                txtTglLahir.Text = Convert.ToDateTime(myRow["tgllahir"]).ToString("dd-MMM-yyyy");
                txtOrtu.Text = myRow["namaOrangtua"].ToString();
                txtTelp.Text = myRow["telp"].ToString();
                txtVA.Text = myRow["novirtual"].ToString();
                txtKota.Text = myRow["Kota"].ToString();
                txtKotaLhr.Text = myRow["kotaLahir"].ToString();
                txtKet1.Text = myRow["ket1"].ToString();
                txtKet2.Text = myRow["ket2"].ToString();
                txtKet3.Text = myRow["ket3"].ToString();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noSiswa", hdnId.Value);
                grdPiutSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPiutSiswa", ObjGlobal.Param);
                grdPiutSiswa.DataBind();

                this.ShowHideGridAndForm(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false);
            //LoadData(cboCabang.Text, cboKelas.Text, txtSearch.Text);
        }

        protected void cboKelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            //LoadData(cboCabang.Text, cboKelas.Text, txtSearch.Text);
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            CloseMessage();
            //LoadData(cboCabang.Text, cboKelas.Text, txtSearch.Text);
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataKelas();
            //LoadData(cboCabang.Text, cboKelas.Text, txtSearch.Text);
        }
    }
}