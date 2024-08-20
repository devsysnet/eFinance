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
    public partial class MstPangkatView : System.Web.UI.Page
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
            grdPangkat.DataSource = ObjDb.GetRows("SELECT a.Pangkat, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM MstPangkat a");
            grdPangkat.DataBind();
        }

        protected void LoadDataSearch()
        {
            grdPangkat.DataSource = ObjDb.GetRows("SELECT a.Pangkat, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM MstPangkat a WHERE a.Pangkat like '%" + txtSearch.Text + "%'");
            grdPangkat.DataBind();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
        }
        protected void grdPangkat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPangkat.PageIndex = e.NewPageIndex;
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