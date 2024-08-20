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
    public partial class TransArSiswa : System.Web.UI.Page
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
            cboKategori.DataSource = ObjDb.GetRows("select * from (select '0' as id, '---Pilih---' name union select noTransaksi as id, jenisTransaksi as name from mJenisTransaksi where nocabang='" + ObjSys.GetCabangId + "')x");
            cboKategori.DataValueField = "id";
            cboKategori.DataTextField = "name";
            cboKategori.DataBind();

            cboTA.DataSource = ObjDb.GetRows("select * from (select '' as id, '---Semua---' name union select distinct tahunAjaran as id, tahunAjaran as name from TransKelas where nocabang='" + ObjSys.GetCabangId + "')x");
            cboTA.DataValueField = "id";
            cboTA.DataTextField = "name";
            cboTA.DataBind();

           
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

         

        protected void btnPilihSiswa_Click(object sender, EventArgs e)
        {
            loadDataFirst();
            ShowHideGridAndForm(true, true);

            cboKelas.DataSource = ObjDb.GetRows("select * from (select '' as id, '---Semua---' as name union select distinct kelas as id, kelas as name from TransKelas where nocabang='" + ObjSys.GetCabangId + "' and tahunajaran='" + cboTA.Text + "')x");
            cboKelas.DataValueField = "id";
            cboKelas.DataTextField = "name";
            cboKelas.DataBind();
            //loadDataCombo();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
       
        {
            bool valid = true;
            string message = "", StrKode = "", alert = "";
            int cekData = 0;
            for (int i = 0; i < grdKelasSiswa.Rows.Count; i++)
            {
                CheckBox chkCheck = (CheckBox)grdKelasSiswa.Rows[i].FindControl("chkCheck");
                string itemId = grdKelasSiswa.DataKeys[i].Value.ToString();
                if (chkCheck.Checked == true)

                {
                    cekData++;
                    StrKode += Convert.ToDecimal(itemId).ToString() + ",";
                }
            }

            if (cekData == 0)
            {
                message = ObjSys.CreateMessage("Tidak ada data yang dipilih.");
                alert = "error";
                valid = false;
            }

            try
            {
                if (valid == true)
                {
                    if (cekData > 0)
                    {
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("hdnNoSiswa", StrKode);
                        ObjGlobal.Param.Add("tahunajaran", cboTA.Text);
                        ObjGlobal.Param.Add("noTransaksi", cboKategori.Text);
                        ObjGlobal.Param.Add("Tgl", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("nilaibayar", Convert.ToDecimal(TxtNilai.Text).ToString());
                        ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.ExecuteProcedure("SPInsertbayarsiswapilih", ObjGlobal.Param);
                       
                     
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil diposting");

                        loadDataCombo();

                        ShowHideGridAndForm(true, false);
                    }
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


        protected void btnSubmitpilih_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadDataFirst();
            ShowHideGridAndForm(true, true);
        }

        protected void loadDataFirst()
        {
           

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Kelas", cboKelas.SelectedValue);
            ObjGlobal.Param.Add("Cabang", ObjSys.GetCabangId);
            grdKelasSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataARSiswa", ObjGlobal.Param);
            grdKelasSiswa.DataBind();
            if (grdKelasSiswa.Rows.Count > 0)
                button.Visible = true;
            else
                button.Visible = false;

          

        }
        protected void grdKelasSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdKelasSiswa.PageIndex = e.NewPageIndex;
            loadDataFirst();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ShowHideGridAndForm(true, false);
        }

        protected void ClearData()
        {
            CloseMessage();
            TxtNilai.Text = "";
        }
    }
}