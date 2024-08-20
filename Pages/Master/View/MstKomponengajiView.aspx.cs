using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.View
{
    public partial class MstKomponengajiView : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            grdkomponengaji.DataSource = ObjDb.GetRows("SELECT a.nokomponengaji, a.komponengaji,case when a.kategori = 1 then 'Pendapatan' when a.kategori = 2 then 'Potongan' when a.kategori = 3 then 'Claim' when a.kategori = 4 then 'Pinjaman' when a.kategori = 5 then 'Pendapatan BPJS'  end kategori,case when a.jenis = 0 then 'Tidak Tetap' else 'Tetap' end jenis,case when a.pph21 = 0 then 'Tidak' else 'Ya' end pph21,case when a.absensi = 1 then 'Ya' else 'Tidak' end penambah,case when a.bpjs = 0 then 'Tidak' else 'Ya' end bpjs,case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM Mstkomponengaji a");
            grdkomponengaji.DataBind();
        }

        protected void LoadDataSearch()
        {
            grdkomponengaji.DataSource = ObjDb.GetRows("SELECT a.nokomponengaji, a.komponengaji,case when a.kategori = 1 then 'Pendapatan' when a.kategori = 2 then 'Potongan' when a.kategori = 3 then 'Claim' when a.kategori = 4 then 'Pinjaman' when a.kategori = 5 then 'Pendapatan BPJS'  end kategori,case when a.jenis = 0 then 'Tidak Tetap' else 'Tetap' end jenis,case when a.pph21 = 0 then 'Tidak' else 'Ya' end pph21,case when a.absensi = 1 then 'Ya' else 'Tidak' end penambah,case when a.bpjs = 0 then 'Tidak' else 'Ya' end bpjs,case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM Mstkomponengaji a WHERE a.komponengaji like '%" + txtSearch.Text + "%'");
            grdkomponengaji.DataBind();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
        }
        protected void grdkomponengaji_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdkomponengaji.PageIndex = e.NewPageIndex;
            if (txtSearch.Text != "")
            {
                LoadDataSearch();
            }
            else
            {
                LoadData();
            }
        }
    }
}