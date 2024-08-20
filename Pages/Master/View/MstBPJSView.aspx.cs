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
    public partial class MstBPJSView : System.Web.UI.Page
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
            grdBPJS.DataSource = ObjDb.GetRows("SELECT a.BPJS,CAST(CONVERT(VARCHAR, CAST(a.persenperusahan AS MONEY), 1) AS VARCHAR) as persenperusahan,CAST(CONVERT(VARCHAR, CAST(a.persenkaryawan AS MONEY), 1) AS VARCHAR) as persenkaryawan, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts,b.ket,case when a.kategori='1' then 'Iuran KWI' when a.kategori='2' then 'Iuran BPJS Kesehatan' when a.kategori='3' then 'Iuran BPJS Ketenagakerjaan' end as kategori FROM MstBPJS a inner join mrekening b on a.norek=b.norek");
            grdBPJS.DataBind();
        }

        protected void LoadDataSearch()
        {
            grdBPJS.DataSource = ObjDb.GetRows("SELECT a.BPJS,CAST(CONVERT(VARCHAR, CAST(a.persenperusahan AS MONEY), 1) AS VARCHAR) as persenperusahan,CAST(CONVERT(VARCHAR, CAST(a.persenkaryawan AS MONEY), 1) AS VARCHAR) as persenkaryawan, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts,b.ket,case when a.kategori='1' then 'Iuran KWI' when a.kategori='2' then 'Iuran BPJS Kesehatan' when a.kategori='3' then 'Iuran BPJS Ketenagakerjaan' end as kategori FROM MstBPJS a inner join mrekening b on a.norek=b.norek WHERE a.BPJS like '%" + txtSearch.Text + "%'");
            grdBPJS.DataBind();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
        }
        protected void grdBPJS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdBPJS.PageIndex = e.NewPageIndex;
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