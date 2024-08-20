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
    public partial class MstTarifPajakView : System.Web.UI.Page
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
            grdtarifpajak.DataSource = ObjDb.GetRows("SELECT CAST(CONVERT(VARCHAR, CAST(a.tarifpajak AS MONEY), 1) AS VARCHAR) as tarifpajak,CAST(CONVERT(VARCHAR, CAST(a.tarifpajak1 AS MONEY), 1) AS VARCHAR) as tarifpajak1,CAST(CONVERT(VARCHAR, CAST(a.persen AS MONEY), 1) AS VARCHAR) as persen,a.tingkat, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM Msttarifpajak a");
            grdtarifpajak.DataBind();
        }

        protected void LoadDataSearch()
        {
            grdtarifpajak.DataSource = ObjDb.GetRows("SELECT CAST(CONVERT(VARCHAR, CAST(a.tarifpajak AS MONEY), 1) AS VARCHAR) as tarifpajak,CAST(CONVERT(VARCHAR, CAST(a.tarifpajak1 AS MONEY), 1) AS VARCHAR) as tarifpajak1,CAST(CONVERT(VARCHAR, CAST(a.persen AS MONEY), 1) AS VARCHAR) as persen,a.tingkat, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM Msttarifpajak a WHERE a.tarifpajak like '%" + txtSearch.Text + "%'");
            grdtarifpajak.DataBind();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
        }
        protected void grdtarifpajak_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdtarifpajak.PageIndex = e.NewPageIndex;
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