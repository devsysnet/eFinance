using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Collections;

namespace eFinance.Pages.Master.View
{
    public partial class mKategoribrgview : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }

        private void loadData()
        {
            grdKategoriBrg.DataSource = ObjDb.GetRows("select a.noKategori,case when a.jns=1 then 'Asset' when a.jns=2 then 'Non Asset' when a.jns=3 then 'Jasa' when a.jns=4 then 'Inventaris' when a.jns=5 then 'Sales' end as jns, a.Kategori, b.kdrek+' - '+b.ket as ket " +
                "from mKategori a left join mrekening b on a.norek = b.norek WHERE a.kategori LIKE '%" + txtSearch.Text + "%'");
            grdKategoriBrg.DataBind();
        }
        protected void grdKategoriBrg_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdKategoriBrg.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }
        protected void CloseMessage()
        {
            ((Label)Master.FindControl("lblMessage")).Text = "";
            ((Label)Master.FindControl("lblMessage")).Visible = false;
        }

    }
}