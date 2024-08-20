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
    public partial class MstDepartemenView : System.Web.UI.Page
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
            grddepartemen.DataSource = ObjDb.GetRows("SELECT a.departemen, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM Mstdepartemen a");
            grddepartemen.DataBind();
        }

        protected void LoadDataSearch()
        {
            grddepartemen.DataSource = ObjDb.GetRows("SELECT a.departemen, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM Mstdepartemen a WHERE a.departemen like '%" + txtSearch.Text + "%'");
            grddepartemen.DataBind();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
        }
        protected void grddepartemen_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grddepartemen.PageIndex = e.NewPageIndex;
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