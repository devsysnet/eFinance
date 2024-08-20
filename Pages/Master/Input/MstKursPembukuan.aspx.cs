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
    public partial class MstKursPembukuan : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstKursPembukuan.aspx");
                SetInitialRow();
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
            DataSet mySet = ObjDb.GetRows("select * from mMataUang");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = string.Empty;
                dr["Column2"] = myRow["kodeMataUang"].ToString();
                dr["Column3"] = myRow["noMataUang"].ToString();

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = string.Empty;
                dr["Column2"] = string.Empty;
                dr["Column3"] = string.Empty;

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdInstansi.DataSource = dt;
            grdInstansi.DataBind();

            SetPreviousData();
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
                        TextBox txtInstansi = (TextBox)grdInstansi.Rows[i].FindControl("txtInstansi");
                        Label lblCurrency = (Label)grdInstansi.Rows[i].FindControl("lblCurrency");
                        HiddenField hdnCurrency = (HiddenField)grdInstansi.Rows[i].FindControl("hdnCurrency");


                        if (dt.Rows[i]["Column2"].ToString() == "IDR")
                            txtInstansi.Text = "1.00";
                        else
                            txtInstansi.Text = dt.Rows[i]["Column1"].ToString();
                        lblCurrency.Text = dt.Rows[i]["Column2"].ToString();
                        hdnCurrency.Value = dt.Rows[i]["Column3"].ToString();
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
                        TextBox txtInstansi = (TextBox)grdInstansi.Rows[i].FindControl("txtInstansi");
                        Label lblCurrency = (Label)grdInstansi.Rows[i].FindControl("lblCurrency");
                        HiddenField hdnCurrency = (HiddenField)grdInstansi.Rows[i].FindControl("hdnCurrency");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtInstansi.Text;
                        dtCurrentTable.Rows[i]["Column2"] = lblCurrency.Text;
                        dtCurrentTable.Rows[i]["Column3"] = hdnCurrency.Value;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdInstansi.DataSource = dtCurrentTable;
                    grdInstansi.DataBind();
                }
            }
            SetPreviousData();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (valid == true)
            {
                try
                {
                    for (int i = 0; i < grdInstansi.Rows.Count; i++)
                    {
                        TextBox txtInstansi = (TextBox)grdInstansi.Rows[i].FindControl("txtInstansi");
                        HiddenField hdnCurrency = (HiddenField)grdInstansi.Rows[i].FindControl("hdnCurrency");
                        DataSet mySet = ObjDb.GetRows("Select * from tKursPembukuan where tglKursBuku = '" + cboTahun.Text + "-" + cboBulan.Text + "-01" + "' and noMataUang='" + hdnCurrency.Value + "'");
                        if (mySet.Tables[0].Rows.Count > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            ShowMessage("error", "Data sudah terdaftar.");
                            //string error = Convert.ToDecimal(txtIdUser.Text).ToString();
                        }
                        else
                        {
                            if (txtInstansi.Text != "")
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noMataUang", hdnCurrency.Value);
                                ObjDb.Data.Add("tglKursBuku", cboTahun.Text + "-" + cboBulan.Text + "-01");
                                ObjDb.Data.Add("nilaiKursBuku", txtInstansi.Text);
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Insert("tKursPembukuan", ObjDb.Data);

                                ShowMessage("success", "Data berhasil disimpan.");
                            }
                        }
                    }
                    //clearData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }

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
    }
}