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
    public partial class MstBiaya : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        public Dictionary<string, string> Param = new Dictionary<string, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstBiaya.aspx");
                SetInitialRoww();
                for (int g = 1; g < 2; g++)
                {
                    AddNewRoww();
                }
            }
        }
        private void SetInitialRoww()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdBiaya.DataSource = dt;
            grdBiaya.DataBind();
        }
        private void SetPreviousDataw()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtJenis = (TextBox)grdBiaya.Rows[i].FindControl("txtJenis");
                        TextBox txtCOA = (TextBox)grdBiaya.Rows[i].FindControl("txtCOA");
                        HiddenField hdnNoCOA = (HiddenField)grdBiaya.Rows[i].FindControl("hdnNoCOA");

                        txtJenis.Text = dt.Rows[i]["Column1"].ToString();
                        txtCOA.Text = dt.Rows[i]["Column2"].ToString();
                        hdnNoCOA.Value = dt.Rows[i]["Column3"].ToString();
                    }
                }
            }
        }
        private void AddNewRoww()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtJenis = (TextBox)grdBiaya.Rows[i].FindControl("txtJenis");
                        TextBox txtCOA = (TextBox)grdBiaya.Rows[i].FindControl("txtCOA");
                        HiddenField hdnNoCOA = (HiddenField)grdBiaya.Rows[i].FindControl("hdnNoCOA");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtJenis.Text;
                        dtCurrentTable.Rows[i]["Column2"] = txtCOA.Text;
                        dtCurrentTable.Rows[i]["Column3"] = hdnNoCOA.Value;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdBiaya.DataSource = dtCurrentTable;
                    grdBiaya.DataBind();
                }
            }
            SetPreviousDataw();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRoww();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (valid == true)
            {
                try
                {
                    for (int i = 0; i < grdBiaya.Rows.Count; i++)
                    {
                        TextBox txtJenis = (TextBox)grdBiaya.Rows[i].FindControl("txtJenis");
                        TextBox txtCOA = (TextBox)grdBiaya.Rows[i].FindControl("txtCOA");
                        HiddenField hdnNoCOA = (HiddenField)grdBiaya.Rows[i].FindControl("hdnNoCOA");

                        if (txtJenis.Text != "")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("JenisBiaya", txtJenis.Text);
                            ObjDb.Data.Add("noRek", hdnNoCOA.Value);
                            ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                            ObjDb.Insert("mbiaya", ObjDb.Data);
                        }
                    }

                    ShowMessage("success", "Data berhasil disimpan.");
                    clearData();
                }
                catch (Exception ex)
                {
                    if (valid == false)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", message);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", ex.ToString());
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
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
        protected void clearData()
        {
            for (int i = 0; i < grdBiaya.Rows.Count; i++)
            {
                TextBox txtJenis = (TextBox)grdBiaya.Rows[i].FindControl("txtJenis");
                TextBox txtCOA = (TextBox)grdBiaya.Rows[i].FindControl("txtCOA");
                HiddenField hdnNoCOA = (HiddenField)grdBiaya.Rows[i].FindControl("hdnNoCOA");

                txtJenis.Text = "";
                txtCOA.Text = "";
                hdnNoCOA.Value = "";
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearData();
        }
        private void LoadDataPanel()
        {

            grdProduct.DataSource = ObjDb.GetRows("select * from mRekening where Grup='Rugi Laba' and Kelompok like '%Biaya%'");
            grdProduct.DataBind();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataPanel();
        }

        protected void btnCariProduct_Click(object sender, EventArgs e)
        {
            LoadDataPanel();
        }

        protected void grdProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(txtHdnPopup.Value);
            string prodno = "", prodnm = "", noprod = "", qty = "";
            prodno = (grdProduct.SelectedRow.FindControl("lblKodeReagent") as Label).Text;
            prodnm = (grdProduct.SelectedRow.FindControl("lblNamaReagent") as Label).Text;
            noprod = (grdProduct.SelectedRow.FindControl("hidNoReagent") as HiddenField).Value;
            //qty = (grdProduct.SelectedRow.FindControl("hdnQty") as HiddenField).Value;

            TextBox txtCOA = (TextBox)grdBiaya.Rows[rowIndex - 1].FindControl("txtCOA");
            HiddenField hdnNoCOA = (HiddenField)grdBiaya.Rows[rowIndex - 1].FindControl("hdnNoCOA");

            int noCek = 0;
            for (int i = 0; i < grdBiaya.Rows.Count; i++)
            {
                TextBox txtCOAn = (TextBox)grdBiaya.Rows[i].Cells[1].FindControl("txtCOA");
                if (txtCOAn.Text == prodno)
                {
                    noCek += 1;
                }
            }
            if (noCek > 0)
            {
                Response.Write("<script>alert('COA yang dipilih tidak boleh sama !');</script>");
                mpe.Show();
            }
            else
            {
                txtCOA.Text = prodno;
                hdnNoCOA.Value = noprod;

            }
            mpe.Hide();
        }

        protected void grdBiaya_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadDataPanel();
            mpe.Show();
            string value = (grdBiaya.SelectedRow.FindControl("txtHdnValue") as HiddenField).Value;
            txtHdnPopup.Value = value;
        }

        protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProduct.PageIndex = e.NewPageIndex;
            LoadDataPanel();
            mpe.Show();
        }
    }
}