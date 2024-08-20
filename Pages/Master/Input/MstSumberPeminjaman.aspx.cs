using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
namespace eFinance.Pages.Master.Input
{
    public partial class MstSumberPeminjaman : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdInstansi.DataSource = dt;
            grdInstansi.DataBind();
        }
        private void SetPreviousData()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtSumber = (TextBox)grdInstansi.Rows[i].FindControl("txtSumber");

                        txtSumber.Text = dt.Rows[i]["Column1"].ToString();
                    }
                }
            }
        }
        private void AddNewRow()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtSumber = (TextBox)grdInstansi.Rows[i].FindControl("txtSumber");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtSumber.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdInstansi.DataSource = dtCurrentTable;
                    grdInstansi.DataBind();
                }
            }
            SetPreviousData();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetInitialRow();
                for (int i = 1; i < 5; i++)
                {
                    AddNewRow();
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
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
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (valid == true)
            {
                try
                {
                    for (int i = 0; i < grdInstansi.Rows.Count; i++)
                    {
                        TextBox txtSumber = (TextBox)grdInstansi.Rows[i].FindControl("txtSumber");

                        //if (ObjDb.GetRows("SELECT * FROM tsumber_peminjaman WHERE sumber = '" + txtSumber.Text + "'").Tables[0].Rows.Count > 0)
                        //{
                        //    message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + txtSumber.Text + "</b> tidak tersimpan karena nama sumber peminjaman sudah terdaftar sebelumnya.");
                        //    count2 += 1;
                        //}
                        //else
                        //{

                            if (txtSumber.Text != "")

                            {
                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("sumber", txtSumber.Text);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Insert("tsumber_peminjaman", ObjDb.Data);
                        //    }
                        }
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                      
                            ShowMessage("success", "Data berhasil disimpan.");
                            clearData();
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }

            }
        }
        private void clearData()
        {
            for (int i = 0; i < grdInstansi.Rows.Count; i++)
            {
                TextBox txtSumber = (TextBox)grdInstansi.Rows[i].FindControl("txtSumber");

                txtSumber.Text = "";

                SetInitialRow();
                for (int g = 1; g < 5; g++)
                {
                    AddNewRow();
                }
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearData();
        }
    }
}