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
    public partial class MstJabatan : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstJabatan.aspx");
                SetInitialRow();
                for (int i = 1; i <= 4; i++)
                {
                    AddNewRow();
                }
            }
        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dr["Column4"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdJabatan.DataSource = dt;
            grdJabatan.DataBind();
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
                        TextBox box1 = (TextBox)grdJabatan.Rows[i].FindControl("txtJabatan");
                        TextBox box2 = (TextBox)grdJabatan.Rows[i].FindControl("txtjammasuk");
                        TextBox box3 = (TextBox)grdJabatan.Rows[i].FindControl("txtjamkeluar");
                        DropDownList box4 = (DropDownList)grdJabatan.Rows[i].FindControl("cboStatus");
                        
                        box1.Text = dt.Rows[i]["Column1"].ToString();
                        box2.Text = dt.Rows[i]["Column2"].ToString();
                        box3.Text = dt.Rows[i]["Column3"].ToString();
                        box4.Text = dt.Rows[i]["Column4"].ToString();
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

                        TextBox box1 = (TextBox)grdJabatan.Rows[i].FindControl("txtJabatan");
                        TextBox box2 = (TextBox)grdJabatan.Rows[i].FindControl("txtjammasuk");
                        TextBox box3 = (TextBox)grdJabatan.Rows[i].FindControl("txtjamkeluar");
                        DropDownList box4 = (DropDownList)grdJabatan.Rows[i].FindControl("cboStatus");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = box1.Text;
                        dtCurrentTable.Rows[i]["Column2"] = box2.Text;
                        dtCurrentTable.Rows[i]["Column3"] = box3.Text;
                        dtCurrentTable.Rows[i]["Column4"] = box4.Text;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grdJabatan.DataSource = dtCurrentTable;
                    grdJabatan.DataBind();
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
                for (int i = 0; i < grdJabatan.Rows.Count; i++)
                {
                    TextBox txtJabatan = (TextBox)grdJabatan.Rows[i].FindControl("txtJabatan");
                    TextBox txtjammasuk = (TextBox)grdJabatan.Rows[i].FindControl("txtjammasuk");
                    TextBox txtjamkeluar = (TextBox)grdJabatan.Rows[i].FindControl("txtjamkeluar");
                    DropDownList txtStatus = (DropDownList)grdJabatan.Rows[i].FindControl("cboStatus");
                    DataSet mySet = ObjDb.GetRows("Select * from MstJabatan where Jabatan= '" + txtJabatan.Text + "'");
                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + txtJabatan.Text + "</b> gagal disimpan,  Jabatan sudah terdaftar sebelumnya.");
                        count2 += 1;
                    }
                    else
                    {
                        if (txtJabatan.Text != "")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("Jabatan", txtJabatan.Text);
                            ObjDb.Data.Add("sts", "1");
                            ObjDb.Data.Add("potonganabsen", txtStatus.Text);
                            ObjDb.Data.Add("setjammasuk", txtjammasuk.Text);
                            ObjDb.Data.Add("setjamkeluar", txtjamkeluar.Text);
                            ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                            ObjDb.Insert("MstJabatan", ObjDb.Data);
                            message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + txtJabatan.Text + "</b> data tersimpan.");
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