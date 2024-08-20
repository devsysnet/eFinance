using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.Input
{
    public partial class MstKompetensi : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstKompetensi.aspx");
                LoadData();
                SetInitialRow();
                for (int i = 1; i <= 4; i++)
                {
                    AddNewRow();
                }
            }
        }
        protected void LoadData()
        {

        }
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
            grdKompetensi.DataSource = dt;
            grdKompetensi.DataBind();
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
                        TextBox box1 = (TextBox)grdKompetensi.Rows[i].FindControl("txtKompetensi");
                        box1.Text = dt.Rows[i]["Column1"].ToString();
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

                        TextBox box1 = (TextBox)grdKompetensi.Rows[i].FindControl("txtKompetensi");
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = box1.Text;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grdKompetensi.DataSource = dtCurrentTable;
                    grdKompetensi.DataBind();
                }
            }
            SetPreviousData();
        }
        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            int count = 0, count2 = 0;
            try
            {
                for (int i = 0; i < grdKompetensi.Rows.Count; i++)
                {
                    TextBox txtKompetensi = (TextBox)grdKompetensi.Rows[i].FindControl("txtKompetensi");
                    DataSet mySet = ObjDb.GetRows("Select * from mKompetensi where kompetensi= '" + txtKompetensi.Text + "'");
                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + txtKompetensi.Text + "</b> gagal disimpan,  Kompetensi sudah terdaftar sebelumnya.");
                        count2 += 1;
                    }
                    else
                    {
                        if (txtKompetensi.Text != "")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("kompetensi", txtKompetensi.Text);
                            ObjDb.Data.Add("sts", "1");
                            ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                            ObjDb.Insert("mKompetensi", ObjDb.Data);
                            message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + txtKompetensi.Text + "</b> data tersimpan.");
                            count += 1;
                        }
                    }


                }

                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                if (count == 0)
                {
                    ShowMessage("error", "Data gagal disimpan." + message);
                }
                else if (count2 > 0)
                {
                    ShowMessage("warning", "Data berhasil disimpan." + message);
                }
                else
                {
                    ShowMessage("success", "Data berhasil disimpan.");
                    ClearData();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Invalid transaction data to database.");
            }
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

        protected void ClearData()
        {
            SetInitialRow();
            for (int i = 1; i <= 4; i++)
            {
                AddNewRow();
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
            CloseMessage();
        }
    }
}