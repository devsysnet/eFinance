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
    public partial class mtabelpiutang : System.Web.UI.Page
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
            grdkomponengaji.DataSource = ObjDb.GetRows("select a.noHutangpiut,Hutang,b.Ket as debet,c.Ket as kredit,case when a.sts='1' then 'Aktif' when a.sts='0' then 'Tidak Aktif' end as sts from mTabelhutang a inner join mRekening b on a.norek=b.noRek inner join mRekening c on a.norekkredit = c.noRek ");
            grdkomponengaji.DataBind();
        }

        protected void LoadDataSearch()
        {
            grdkomponengaji.DataSource = ObjDb.GetRows("SELECT a.hutang,b.ket as debet,b.ket as kredit, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM mTabelhutang a inner join mrekening b on a.norek=b.norek inner join mrekening c on a.norekkredit=c.norek WHERE a.hutang like '%" + txtSearch.Text + "%'");
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