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
    public partial class TransArSiswaupdate : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                ShowHideGridAndForm(true, false);
            }
        }

        private void loadDataCombo()
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

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";
            
            try
            {
                if (valid == true)
                {
                    
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("TahunAjaran", cboTahunAjaran.SelectedValue);
                    ObjGlobal.Param.Add("Kelas", cboKelas.SelectedValue);
                    ObjGlobal.Param.Add("JnsTrans", cboJnsTrans.SelectedValue);
                    ObjGlobal.Param.Add("Cabang", ObjSys.GetCabangId);
                    ObjGlobal.ExecuteProcedure("SPDeletebayarsiswapilih", ObjGlobal.Param);

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil didelete");

                    loadDataCombo();
                    ShowHideGridAndForm(true, false);
                    
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage(alert, message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            if (cboJnsTrans.Text != "0" && cboKelas.Text != "" && cboTahunAjaran.Text != "")
            {
                loadDataFirst();
                ShowHideGridAndForm(true, true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Data pencarian harus dipilih");
                ShowHideGridAndForm(true, false);
            }
                        
        }

        protected void loadDataFirst()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("TahunAjaran", cboTahunAjaran.SelectedValue);
            ObjGlobal.Param.Add("Kelas", cboKelas.SelectedValue);
            ObjGlobal.Param.Add("JnsTrans", cboJnsTrans.SelectedValue);
            ObjGlobal.Param.Add("Cabang", ObjSys.GetCabangId);
            grdKelasSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPelunasanAR", ObjGlobal.Param);
            grdKelasSiswa.DataBind();
            if (grdKelasSiswa.Rows.Count > 0)
            {
                //button.Visible = true;
            }
            else
            {
                //button.Visible = false;
            }


        }
        protected void grdKelasSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdKelasSiswa.PageIndex = e.NewPageIndex;
            loadDataFirst();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataFirst();
            ShowHideGridAndForm(true, true);
        }

 
    }
}