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
    public partial class MstJabatanView : System.Web.UI.Page
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
            grdJabatan.DataSource = ObjDb.GetRows("SELECT a.Jabatan, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts,case when potonganabsen=0 then 'Tidak Dipotong' else 'Dipotong' end as potongan,setjammasuk,setjamkeluar FROM MstJabatan a");
            grdJabatan.DataBind();
        }

        protected void LoadDataSearch()
        {
            grdJabatan.DataSource = ObjDb.GetRows("SELECT a.Jabatan, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts,case when potonganabsen=0 then 'Tidak Dipotong' else 'Dipotong' end as potongan,setjammasuk,setjamkeluar FROM MstJabatan a WHERE a.Jabatan like '%" + txtSearch.Text + "%'");
            grdJabatan.DataBind();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
        }
        protected void grdJabatan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdJabatan.PageIndex = e.NewPageIndex;
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