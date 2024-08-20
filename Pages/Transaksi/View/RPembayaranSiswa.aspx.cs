using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.View
{
    public partial class RPembayaranSiswa : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataComboFirst();
                ShowHideGridAndForm(true, true);
            }
        }

        private void loadDataComboFirst()
        {
            //unit
            if (ObjSys.GetstsPusat == "2")
            {
                cboPerwakilanUnit.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboPerwakilanUnit.DataValueField = "id";
                cboPerwakilanUnit.DataTextField = "name";
                cboPerwakilanUnit.DataBind();
            }
            //admin Kantor Perwakilan Bogor GetstsPusat=3
            else if (ObjSys.GetstsPusat == "3")
            {
                cboPerwakilanUnit.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2) and parent = " + ObjSys.GetParentCabang + ") a");
                cboPerwakilanUnit.DataValueField = "id";
                cboPerwakilanUnit.DataTextField = "name";
                cboPerwakilanUnit.DataBind();
            }
            else
            {
                cboPerwakilanUnit.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2)) a");
                cboPerwakilanUnit.DataValueField = "id";
                cboPerwakilanUnit.DataTextField = "name";
                cboPerwakilanUnit.DataBind();
            }
            
            loadDataJnsTransaksi(cboPerwakilanUnit.Text);

            cboBulan.DataSource = ObjDb.GetRows("select * from (select '0' as id, '-Pilih Bulan-' as name union select distinct month(tgl) as id, DATENAME(mm, tgl) as name from TransPiutang)x");
            cboBulan.DataValueField = "id";
            cboBulan.DataTextField = "name";
            cboBulan.DataBind();
        }

        protected void cboPerwakilanUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataJnsTransaksi(cboPerwakilanUnit.Text);
        }

        protected void loadDataJnsTransaksi(string PerwakilanUnit = "")
        {
            if (PerwakilanUnit == "0")
            {
                cboJnsTrans.DataSource = ObjDb.GetRows("select * from (select '0' as id, '-Pilih Transaksi-' as name)x");
                cboJnsTrans.DataValueField = "id";
                cboJnsTrans.DataTextField = "name";
                cboJnsTrans.DataBind();

                cboTahun.DataSource = ObjDb.GetRows("select * from (select '0' as id, '-Pilih Tahun-' as name)x");
                cboTahun.DataValueField = "id";
                cboTahun.DataTextField = "name";
                cboTahun.DataBind();
            }
            else
            {
                cboJnsTrans.DataSource = ObjDb.GetRows("select * from (select '0' as id, '-Pilih Transaksi-' as name union select distinct a.noTransaksi as id, a.jenisTransaksi as name from mJenisTransaksi a inner join TransPiutang b on a.noTransaksi = b.noTransaksi where a.nocabang = '" + PerwakilanUnit + "')x");
                cboJnsTrans.DataValueField = "id";
                cboJnsTrans.DataTextField = "name";
                cboJnsTrans.DataBind();

                cboTahun.DataSource = ObjDb.GetRows("select * from (select '0' as id, '-Pilih Tahun-' as name union select distinct year(tgl) as id, CONVERT(varchar,year(tgl)) as name from TransPiutang where nocabang = '" + PerwakilanUnit + "')x");
                cboTahun.DataValueField = "id";
                cboTahun.DataTextField = "name";
                cboTahun.DataBind();
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

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataFirst();
        }

        protected void loadDataFirst()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Bulan", cboBulan.Text);
            ObjGlobal.Param.Add("Tahun", cboTahun.Text);
            ObjGlobal.Param.Add("jnsTrans", cboJnsTrans.Text);
            ObjGlobal.Param.Add("nocabang", cboPerwakilanUnit.Text);
            ObjGlobal.Param.Add("search", txtNamaSiswa.Text);
            grdBayarSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBayarTagihan", ObjGlobal.Param);
            grdBayarSiswa.DataBind();
        }
    }
}