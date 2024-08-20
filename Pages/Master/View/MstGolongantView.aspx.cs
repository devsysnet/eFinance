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
    public partial class MstGolongantView : System.Web.UI.Page
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
            grdGolongan.DataSource = ObjDb.GetRows("SELECT a.Golongan, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts,pangkat,ruang,urutan FROM MstGolongan a");
            grdGolongan.DataBind();
        }

        protected void LoadDataSearch()
        {
            grdGolongan.DataSource = ObjDb.GetRows("SELECT a.Golongan, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts,pangkat,ruang,urutan FROM MstGolongan a WHERE a.Golongan like '%" + txtSearch.Text + "%'");
            grdGolongan.DataBind();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
        }
        protected void grdGolongan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdGolongan.PageIndex = e.NewPageIndex;
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