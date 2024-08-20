﻿using System;
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
    public partial class MstidentitasView : System.Web.UI.Page
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
            grdIdentitas.DataSource = ObjDb.GetRows("SELECT a.Identitas, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM mIdentitas a");
            grdIdentitas.DataBind();
        }

        protected void LoadDataSearch()
        {
            grdIdentitas.DataSource = ObjDb.GetRows("SELECT a.Identitas, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM mIdentitas a WHERE a.Identitas like '%" + txtSearch.Text + "%'");
            grdIdentitas.DataBind();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
        }
        protected void grdIdentitas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdIdentitas.PageIndex = e.NewPageIndex;
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