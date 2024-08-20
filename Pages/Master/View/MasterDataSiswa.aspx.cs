using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;

namespace eFinance.Pages.Master.View
{
    public partial class MasterDataSiswa : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadIndexYayasan();
                loadDataCombo();
                loadDataComboKelas();
                if (cboUnit.Text == "0")
                    showhidedropdown(false);
                else
                    showhidedropdown(true);
                LoadData(cboPerwakilan.Text, cboUnit.Text, cboKelas.Text, txtSearch.Text);
            }
        }

        protected void LoadIndexYayasan()
        {
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
            DataRow myRow = mySet.Tables[0].Rows[0];
            hdnnoYysn.Value = myRow["noCabang"].ToString();
        }

        protected void LoadData(string perwakilan = "0", string unit = "0", string kelas = "0", string search = "",string cbotahunajaran="")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("perwakilan", perwakilan);
            ObjGlobal.Param.Add("unit", unit);
            ObjGlobal.Param.Add("kelas", kelas);
            ObjGlobal.Param.Add("Search", search);
            //ObjGlobal.Param.Add("tahunajaran", cbotahunajaran);
            grdSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadMasterDataSiswa", ObjGlobal.Param);
            grdSiswa.DataBind();
            
        }

        protected void loadDataCombo()
        {

            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Perwakilan' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where stspusat=0 and stscabang=1 and nocabang=" + ObjSys.GetCabangId + ") a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where noCabang=" + ObjSys.GetParentCabang + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where  stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            LoadDataCombo2();

            //cbotahunajaran.DataSource = ObjDb.GetRows("select a.* from (select distinct tahunAjaran,tahunAjaran from TransKelas) a");
            //cbotahunajaran.DataValueField = "id";
            //cbotahunajaran.DataTextField = "name";
            //cbotahunajaran.DataBind();
           
        }

        protected void LoadDataCombo2()
        {
            if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {
                cboUnit.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + ObjSys.GetParentCabang + " and nocabang=" + ObjSys.GetCabangId + " and stspusat=0 and stscabang=2) a");
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();
            }
            else
            {
                cboUnit.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Unit' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + cboPerwakilan.Text + " and stspusat=0 and stscabang=2) a");
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();
            }

            loadDataComboKelas();
        }

        protected void loadDataComboKelas()
        {
            if (cboUnit.Text == "0")
            {
                cboKelas.DataSource = ObjDb.GetRows("select * from (select '-' as id, 'Semua Kelas' as name union select distinct ltrim(kelas) as id, ltrim(kelas) as name from TransKelas)x");
                cboKelas.DataValueField = "id";
                cboKelas.DataTextField = "name";
                cboKelas.DataBind();
            }
            else
            {
                cboKelas.DataSource = ObjDb.GetRows("select * from (select '-' as id, 'Semua Kelas' as name union select distinct ltrim(kelas) as id, ltrim(kelas) as name from TransKelas where nocabang='" + cboUnit.Text + "')x");
                cboKelas.DataValueField = "id";
                cboKelas.DataTextField = "name";
                cboKelas.DataBind();
            }
        }

        protected void grdSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdSiswa.PageIndex = e.NewPageIndex;
            LoadData(cboPerwakilan.Text, cboUnit.Text, cboKelas.Text, txtSearch.Text);
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
                ObjGlobal.Param.Add("nocabang", cboUnit.Text);
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
            LoadData(cboPerwakilan.Text, cboUnit.Text, cboKelas.Text, txtSearch.Text);
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataComboKelas();
        }

        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataCombo2();
            LoadData(cboPerwakilan.Text, cboUnit.Text, cboKelas.Text, txtSearch.Text);
            showhidedropdown(false);
        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataComboKelas();
            if (cboUnit.Text == "0")
            {
                showhidedropdown(false);
            }
            else
            {
                showhidedropdown(true);
            }
            LoadData(cboPerwakilan.Text, cboUnit.Text, cboKelas.Text, txtSearch.Text);
                
        }

        protected void cboKelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(cboPerwakilan.Text, cboUnit.Text, cboKelas.Text, txtSearch.Text);
        }

        protected void showhidedropdown(bool showhideclass)
        {
            divclass.Visible = showhideclass;
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(cboPerwakilan.Text, cboUnit.Text, cboKelas.Text, txtSearch.Text);
        }

        protected void grdSiswa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnJmlTunggakan = (HiddenField)e.Row.FindControl("hdnJmlTunggakan");
                Label lblnik = (Label)e.Row.FindControl("lblnik");
                Label lblnis = (Label)e.Row.FindControl("lblnis");
                Label lblnisn = (Label)e.Row.FindControl("lblnisn");
                Label lblNamaSiswa = (Label)e.Row.FindControl("lblNamaSiswa");
                Label lblkelas = (Label)e.Row.FindControl("lblkelas");
                Label lbllokasiUnit = (Label)e.Row.FindControl("lbllokasiUnit");

                if (hdnJmlTunggakan.Value != "0")
                {
                    lblnik.ForeColor = Color.Red;
                    lblnis.ForeColor = Color.Red;
                    lblnisn.ForeColor = Color.Red;
                    lblNamaSiswa.ForeColor = Color.Red;
                    lblkelas.ForeColor = Color.Red;
                    lbllokasiUnit.ForeColor = Color.Red;
                }
                else
                {
                    lblnik.ForeColor = Color.Gray;
                    lblnis.ForeColor = Color.Gray;
                    lblnisn.ForeColor = Color.Gray;
                    lblNamaSiswa.ForeColor = Color.Gray;
                    lblkelas.ForeColor = Color.Gray;
                    lbllokasiUnit.ForeColor = Color.Gray;
                }
            }
        }
    }
}