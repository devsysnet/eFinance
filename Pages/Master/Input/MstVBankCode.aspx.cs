using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

namespace eFinance.Pages.Master.Input
{
    public partial class MstVBankCode : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstVBankCode.aspx");
                loadData();
                for (int i = 1; i <= 4; i++)
                {
                    AddNewRow();
                }
                loadDataCombo();
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
            dt.Columns.Add(new DataColumn("Column5", typeof(string)));
            dt.Columns.Add(new DataColumn("Column6", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dr["Column4"] = string.Empty;
            dr["Column5"] = string.Empty;
            dr["Column6"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdVBankCode.DataSource = dt;
            grdVBankCode.DataBind();
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
                        DropDownList box1 = (DropDownList)grdVBankCode.Rows[i].FindControl("cboCurrency");
                        Label hid1 = (Label)grdVBankCode.Rows[i].FindControl("lblCode");
                        Label box2 = (Label)grdVBankCode.Rows[i].FindControl("lblName");
                        TextBox box3 = (TextBox)grdVBankCode.Rows[i].FindControl("txtBank");
                        DropDownList box4 = (DropDownList)grdVBankCode.Rows[i].FindControl("cboKategori");
                        HiddenField hid2 = (HiddenField)grdVBankCode.Rows[i].FindControl("hdnKategori");

                        box1.Text = dt.Rows[i]["Column1"].ToString();
                        hid1.Text = dt.Rows[i]["Column2"].ToString();
                        box2.Text = dt.Rows[i]["Column3"].ToString();
                        box3.Text = dt.Rows[i]["Column4"].ToString();
                        hid2.Value = dt.Rows[i]["Column5"].ToString();
                        box4.Text = dt.Rows[i]["Column6"].ToString();
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
                        DropDownList box1 = (DropDownList)grdVBankCode.Rows[i].FindControl("cboCurrency");
                        Label hid1 = (Label)grdVBankCode.Rows[i].FindControl("lblCode");
                        Label box2 = (Label)grdVBankCode.Rows[i].FindControl("lblName");
                        TextBox box3 = (TextBox)grdVBankCode.Rows[i].FindControl("txtBank");
                        DropDownList box4 = (DropDownList)grdVBankCode.Rows[i].FindControl("cboKategori");
                        HiddenField hid2 = (HiddenField)grdVBankCode.Rows[i].FindControl("hdnKategori");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = box1.Text;
                        dtCurrentTable.Rows[i]["Column2"] = hid1.Text;
                        dtCurrentTable.Rows[i]["Column3"] = box2.Text;
                        dtCurrentTable.Rows[i]["Column4"] = box3.Text;
                        dtCurrentTable.Rows[i]["Column5"] = hid2.Value;
                        dtCurrentTable.Rows[i]["Column6"] = box4.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdVBankCode.DataSource = dtCurrentTable;
                    grdVBankCode.DataBind();
                }
            }
            SetPreviousData();
        }
        private void loadDataCombo()
        {
            for (int g = 0; g < grdVBankCode.Rows.Count; g++)
            {
                DropDownList cboCurrency = (DropDownList)grdVBankCode.Rows[g].FindControl("cboCurrency");

                cboCurrency.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noMataUang id, namaMataUang name FROM mMataUang ) a");
                cboCurrency.DataValueField = "id";
                cboCurrency.DataTextField = "name";
                cboCurrency.DataBind();
            }
        }
        private void loadData()
        {
            grdVBankCode.DataSource = ObjDb.GetRows("Select * from mRekening where jenis = '1' or jenis = '2'");
            grdVBankCode.DataBind();
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
            ObjDb.Data.Clear();

            if (valid == true)
            {
                try
                {
                    for (int i = 0; i < grdVBankCode.Rows.Count; i++)
                    {
                        DropDownList cboCurrency = (DropDownList)grdVBankCode.Rows[i].FindControl("cboCurrency");
                        Label lblCode = (Label)grdVBankCode.Rows[i].FindControl("lblCode");
                        Label lblName = (Label)grdVBankCode.Rows[i].FindControl("lblName");
                        TextBox txtBank = (TextBox)grdVBankCode.Rows[i].FindControl("txtBank");
                        DropDownList cboKategori = (DropDownList)grdVBankCode.Rows[i].FindControl("cboKategori");

                        if (txtBank.Text != "")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("kdrek", lblCode.Text);
                            ObjDb.Data.Add("noMataUang", cboCurrency.SelectedValue);
                            ObjDb.Data.Add("kodeVoucher", txtBank.Text);
                            ObjDb.Data.Add("stsKasBank", cboKategori.Text);
                            ObjDb.Data.Add("createBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("createDate", ObjSys.GetNow);
                            ObjDb.Insert("tBank", ObjDb.Data);

                            ObjDb.Data.Clear();
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("kdrek", lblCode.Text);
                            ObjDb.Data.Add("jenis", "0");
                            ObjDb.Update("mRekening", ObjDb.Data, ObjDb.Where);
                        }
                        ShowMessage("success", "Data berhasil disimpan.");
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }
            }
            loadData();
        }

        protected void btnResetRek_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdVBankCode.Rows.Count; i++)
            {
                TextBox txtBank = (TextBox)grdVBankCode.Rows[i].FindControl("txtBank");
                txtBank.Text = "";
            }
        }

        protected void grdVBankCode_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnKategori = (HiddenField)e.Row.FindControl("hdnKategori");
                DropDownList cboKategori = (DropDownList)e.Row.FindControl("cboKategori");

                cboKategori.Text = hdnKategori.Value;
            }
        }

    }
}