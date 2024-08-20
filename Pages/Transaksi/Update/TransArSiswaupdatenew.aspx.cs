using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransArSiswaupdatenew : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                LoadDataCombo();
            }
        }

        //protek index yang dipakai transaksi
        protected void IndexPakai()
        {
            for (int i = 0; i < grdSiswa.Rows.Count; i++)
            {
                string itemId = grdSiswa.DataKeys[i].Value.ToString();
                Button btnSelectDel = (Button)grdSiswa.Rows[i].FindControl("btnSelectDel");

                //DataSet mySet = ObjDb.GetRows("Select top 1 noSiswa from TransKelas Where noSiswa = '" + itemId + "'");
                //if (mySet.Tables[0].Rows.Count > 0)
                //    btnSelectDel.Enabled = false;
                //else
                    btnSelectDel.Enabled = true;
            }

        }

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("TahunAjaran", cboTahunAjaran.SelectedValue);
            ObjGlobal.Param.Add("Kelas", cboKelas.SelectedValue);
            ObjGlobal.Param.Add("JnsTrans", cboJnsTrans.SelectedValue);
            ObjGlobal.Param.Add("Cabang", ObjSys.GetCabangId);
            grdSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataupdateAR", ObjGlobal.Param);
            grdSiswa.DataBind();

            IndexPakai();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdSiswa.PageIndex = e.NewPageIndex;
            LoadData();
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
            LoadData();
        }

        protected void LoadDataCombo()
        {
            cboTahunAjaran.DataSource = ObjDb.GetRows("select * from (select '' as id, '-Pilih Tahun Ajaran-' as name union select distinct tahunajaran as id, tahunajaran as name from TransPiutang where nocabang = '" + ObjSys.GetCabangId + "')x");
            cboTahunAjaran.DataValueField = "id";
            cboTahunAjaran.DataTextField = "name";
            cboTahunAjaran.DataBind();

            cboKelas.DataSource = ObjDb.GetRows("select * from (select '' as id, '-Pilih Kelas-' as name union select distinct kelas as id, kelas as name from TransKelas where nocabang = '" + ObjSys.GetCabangId + "')x");
            cboKelas.DataValueField = "id";
            cboKelas.DataTextField = "name";
            cboKelas.DataBind();

            cboJnsTrans.DataSource = ObjDb.GetRows("select * from (select 0 as id, '-Pilih Transaksi-' as name union select distinct noTransaksi as id, jenisTransaksi as name from mJenisTransaksi where nocabang = '" + ObjSys.GetCabangId + "')x");
            cboJnsTrans.DataValueField = "id";
            cboJnsTrans.DataTextField = "name";
            cboJnsTrans.DataBind();
        }



        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            
            if (valid == true)
            {
               
                try
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("nopiutang", hdnId.Value);
                    ObjGlobal.Param.Add("nilai", Convert.ToDecimal(nilai.Text).ToString());
                    ObjGlobal.Param.Add("tgl", Convert.ToDateTime(dttgltrs.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.GetDataProcedure("SPUpdatetranspiutangnew", ObjGlobal.Param);

                    ShowHideGridAndForm(true, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diubah.");
                    LoadData();
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

        protected void grdSiswa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectEdit")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nopiutang = grdSiswa.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nopiutang;

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("nopiutang", hdnId.Value);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDatapiutangDetil", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    nama.Text = myRow["namaSiswa"].ToString();
                    nis.Text = myRow["nis"].ToString();
                    kelas.Text = myRow["kelas"].ToString();
                    jenistransaksi.Text = myRow["jenistransaksi"].ToString();
                    dttgltrs.Text = Convert.ToDateTime(myRow["Tgl"]).ToString("dd-MMM-yyyy");
                    nilai.Text = myRow["nilai"].ToString();

                    LoadDataCombo();
                    this.ShowHideGridAndForm(false, true);
                }
                //else if (e.CommandName == "SelectDelete")
                //{
                //    int rowIndex = int.Parse(e.CommandArgument.ToString());
                //    string noSiswa = grdSiswa.DataKeys[rowIndex].Value.ToString();
                //    hdnId.Value = noSiswa;

                //    ObjDb.Where.Clear();
                //    ObjDb.Where.Add("nopiutang", hdnId.Value);
                //    ObjDb.Delete("transpiutang", ObjDb.Where);

                //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //    ShowMessage("success", "Success Deleted");
                //    this.ShowHideGridAndForm(true, false);
                //    LoadData();
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

    }
}