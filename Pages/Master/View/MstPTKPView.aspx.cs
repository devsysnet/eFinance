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
    public partial class MstPTKPView : System.Web.UI.Page
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
            grdPTKP.DataSource = ObjDb.GetRows("SELECT a.PTKP,CAST(CONVERT(VARCHAR, CAST(a.nilai AS MONEY), 1) AS VARCHAR) as nilai, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM MstPTKP a");
            grdPTKP.DataBind();
        }

        protected void LoadDataSearch()
        {
            grdPTKP.DataSource = ObjDb.GetRows("SELECT a.PTKP,CAST(CONVERT(VARCHAR, CAST(a.nilai AS MONEY), 1) AS VARCHAR) as nilai, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM MstPTKP a WHERE a.PTKP like '%" + txtSearch.Text + "%'");
            grdPTKP.DataBind();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
        }
        protected void grdPTKP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPTKP.PageIndex = e.NewPageIndex;
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