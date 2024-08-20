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
    public partial class MstPendindikanView : System.Web.UI.Page
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
            grdpendindikan.DataSource = ObjDb.GetRows("SELECT a.pendindikan, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM Mstpendindikan a");
            grdpendindikan.DataBind();
        }

        protected void LoadDataSearch()
        {
            grdpendindikan.DataSource = ObjDb.GetRows("SELECT a.pendindikan, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM Mstpendindikan a WHERE a.pendindikan like '%" + txtSearch.Text + "%'");
            grdpendindikan.DataBind();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
        }
        protected void grdpendindikan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdpendindikan.PageIndex = e.NewPageIndex;
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