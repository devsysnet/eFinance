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
    public partial class TransArSiswaView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataComboFirst();
                ShowHide(false, true);
                //loadDataKelas("0");
                //loadDataJnsTransaksi("0");
                //loadDataPeriode("0");

            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("nokategori", cboKategori.Text);
            ObjGlobal.Param.Add("periode", cboPeriode.Text);
            ObjGlobal.Param.Add("kelas", cboKelas.Text);
            grdARSiswa.DataSource = ObjGlobal.GetDataProcedure("SPViewpiutangsiswa", ObjGlobal.Param);
            grdARSiswa.DataBind();

        }

        protected void ShowHide(Boolean tgl, Boolean period)
        {
                  showhidePeriod.Visible = period;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void grdARSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdARSiswa.PageIndex = e.NewPageIndex;
            loadData();
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

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            showHideFormKas(true, false);
        }

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
              loadData();
        }

        private void loadDataComboFirst()
        {
            if (ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Unit---' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE stsCabang = 2 and parent = '" + ObjSys.GetParentCabang + "') a order by a.noUrut");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsPusat == "2")
            {
                cboCabang.DataSource = ObjDb.GetRows("SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsCabang = 2 and noCabang = '" + ObjSys.GetCabangId + "'");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Unit---' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE stsCabang = 2) a order by a.noUrut");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

            loadDataKelas(cboCabang.Text);
            loadDataJnsTransaksi(cboCabang.Text);
            loadDataPeriode(cboCabang.Text);
        }

        protected void loadDataKelas(string nocabang = "")
        {
           
                cboKelas.DataSource = ObjDb.GetRows("select '' as id, '-Pilih Kelas-' as name union all select distinct kelas as id, kelas as name from TransKelas where nocabang = '" + nocabang + "'");
                cboKelas.DataValueField = "id";
                cboKelas.DataTextField = "name";
                cboKelas.DataBind();
           // }

        }

        protected void loadDataJnsTransaksi(string nocabang = "")
        {
           
                cboKategori.DataSource = ObjDb.GetRows("select '' as id, '---Pilih Transaksi---' as name union select distinct noTransaksi as id, jenisTransaksi as name from mJenisTransaksi where nocabang = '" + nocabang + "'");
                cboKategori.DataValueField = "id";
                cboKategori.DataTextField = "name";
                cboKategori.DataBind();
         

        }

        protected void loadDataPeriode(string nocabang = "")
        {
            
                cboPeriode.DataSource = ObjDb.GetRows("select * from (select '' as id, '-Pilih Periode-' name union select distinct tahunAjaran as id, tahunAjaran as name from transkelas where nocabang = '" + nocabang + "')x");
                cboPeriode.DataValueField = "id";
                cboPeriode.DataTextField = "name";
                cboPeriode.DataBind();
        
        }


        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataKelas(cboCabang.Text);
            loadDataJnsTransaksi(cboCabang.Text);
            loadDataPeriode(cboCabang.Text);
        }

        //protected void cboPeriode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CloseMessage();
       
        //}

        //protected void cboKelas_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CloseMessage();
     
        //}

        //protected void cboKategori_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CloseMessage();
        
        //}
    }
}