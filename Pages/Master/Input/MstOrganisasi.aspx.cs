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
    public partial class MstOrganisasi : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
 
                SetInitialRow();
                for (int i = 1; i < 5; i++)
                {
                    AddNewRow();
                }
                cboCurrencyTrans.DataSource = ObjDb.GetRows("select a.* from ( SELECT distinct nokaryawan id, nama name FROM mstkaryawan  WHERE nocabang = '" + ObjSys.GetCabangId + "') a");
                cboCurrencyTrans.DataValueField = "id";
                cboCurrencyTrans.DataTextField = "name";
                cboCurrencyTrans.DataBind();
            }
        }

        #region rows
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
            grdMemoJurnal.DataSource = dt;
            grdMemoJurnal.DataBind();
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
                        TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                        HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                        TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");
                        TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtRemark");
                        TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtKredit");

                        txtAccount.Text = dt.Rows[i]["Column1"].ToString();
                        hdnAccount.Value = dt.Rows[i]["Column2"].ToString();
                        txtDescription.Text = dt.Rows[i]["Column3"].ToString();
                        txtRemark.Text = dt.Rows[i]["Column4"].ToString();
                        if (dt.Rows[i]["Column5"].ToString() == "")
                            txtDebit.Text = "0.00";
                        else
                            txtDebit.Text = dt.Rows[i]["Column5"].ToString();
                        if (dt.Rows[i]["Column6"].ToString() == "")
                            txtKredit.Text = "0.00";
                        else
                            txtKredit.Text = dt.Rows[i]["Column6"].ToString();
                    }
                }
            }
        }
        protected void grdMemoJurnal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                txtHdnPopup.Value = rowIndex.ToString();
                if (e.CommandName == "Clear")
                {
                    TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtAccount");
                    HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[rowIndex].FindControl("hdnAccount");
                    TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtDescription");
                    TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtRemark");
                    TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtDebit");
                    TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtKredit");

                    txtAccount.Text = "";
                    hdnAccount.Value = "";
                    txtDescription.Text = "";
                    txtRemark.Text = "";
                    txtDebit.Text = "";
                    txtKredit.Text = "";

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();CalculateDiscount();", true);

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
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
                        TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                        HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                        TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");
                        TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtRemark");
                        TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtKredit");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtAccount.Text;
                        dtCurrentTable.Rows[i]["Column2"] = hdnAccount.Value;
                        dtCurrentTable.Rows[i]["Column3"] = txtDescription.Text;
                        dtCurrentTable.Rows[i]["Column4"] = txtRemark.Text;
                        dtCurrentTable.Rows[i]["Column5"] = txtDebit.Text;
                        dtCurrentTable.Rows[i]["Column6"] = txtKredit.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdMemoJurnal.DataSource = dtCurrentTable;
                    grdMemoJurnal.DataBind();
                }
            }
            SetPreviousData();
        }
        #endregion
        protected void CloseMessage()
        {
            ((Label)Master.FindControl("lblMessage")).Text = "";
            ((Label)Master.FindControl("lblMessage")).Visible = false;
        } 
        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }
        protected void grdMemoJurnal_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kdRek = (grdMemoJurnal.SelectedRow.FindControl("txtAccount") as TextBox).Text;
            if (kdRek != "")
            {
                string kodeRek = (grdMemoJurnal.SelectedRow.FindControl("txtAccount") as TextBox).Text;

                mpe.Show();
                string value = (grdMemoJurnal.SelectedRow.FindControl("txtHdnValue") as HiddenField).Value;
                txtHdnPopup.Value = value;
                txtSearch.Text = "";
            }
            if (kdRek == "")
            {
                LoadDataPanel();
                mpe.Show();
                string value = (grdMemoJurnal.SelectedRow.FindControl("txtHdnValue") as HiddenField).Value;
                txtHdnPopup.Value = value;
                txtSearch.Text = "";
            }

        }
        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataPanel();
        }
        #region PopUpProduct
        private void LoadDataPanel()
        {
            grdProduct.DataSource = ObjDb.GetRows("select * from mstKaryawan where    nocabang = '" + ObjSys.GetCabangId + "' and nokaryawan != '" + cboCurrencyTrans.Text +"'  ");
            grdProduct.DataBind();
        }
        protected void btnCariProduct_Click(object sender, EventArgs e)
        {
            LoadDataPanel();
            mpe.Show();
        }

        protected void grdProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(txtHdnPopup.Value);
            string prodno = "", prodnm = "", noprod = "";
            prodno = (grdProduct.SelectedRow.FindControl("lblKodeReagent") as Label).Text;
            prodnm = (grdProduct.SelectedRow.FindControl("lblNamaReagent") as Label).Text;
            noprod = (grdProduct.SelectedRow.FindControl("hidNoReagent") as HiddenField).Value;

            TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[rowIndex - 1].FindControl("txtAccount");
            TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[rowIndex - 1].FindControl("txtDescription");
            HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[rowIndex - 1].FindControl("hdnAccount");

            int noCek = 0;
            for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
            {
                TextBox txtAccountt = (TextBox)grdMemoJurnal.Rows[i].Cells[1].FindControl("txtAccount");
                if (txtAccountt.Text == prodno)
                {
                    noCek += 1;
                }
            }
            //if (noCek > 0)
            //{
            //    Response.Write("<script>alert('Account yang dipilih tidak boleh sama !');</script>");
            //    mpe.Show();
            //}
            //else
            {
                txtAccount.Text = prodno;
                txtDescription.Text = prodnm;
                hdnAccount.Value = noprod;
            }

            mpe.Hide();
        }

        protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProduct.PageIndex = e.NewPageIndex;
            LoadDataPanel();
            mpe.Show();
        }
        #endregion


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (cboCurrencyTrans.Text == "0")
            {
                message += ObjSys.CreateMessage("Atasan harus dipilih.");
                valid = false;
            }

            DataSet protekpos = ObjDb.GetRows("SELECT atasan FROM mOrganisasi WHERE nocabang = '" + ObjSys.GetCabangId + "' and atasan='" + cboCurrencyTrans.Text + "' ");
            if (protekpos.Tables[0].Rows.Count > 0)
            {
  
                 message += ObjSys.CreateMessage("Sudah Posting Tahunan GL");
                 valid = false;
                     
            }


 
            if (valid == true)
            {
                try
                {
     

                    for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
                    {
                        HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                        TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");
                        TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtRemark");
                        TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtKredit");

                        if (txtAccount.Text != "")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("atasan", cboCurrencyTrans.Text);
                            ObjDb.Data.Add("bawahan", hdnAccount.Value);
                            ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                            ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                            ObjDb.Insert("mOrganisasi", ObjDb.Data);

                        }

                    }

                    ShowMessage("success", "Data berhasil disimpan.");
                    clearData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }
        private void clearData()
        {
            for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
            {
                HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");
                TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtRemark");
                TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDebit");
                TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtKredit");

                hdnAccount.Value = "";
                txtAccount.Text = "";
                txtDescription.Text = "";
                txtRemark.Text = "";
                txtDebitTotal.Text = "";
                txtKreditTotal.Text = "";
                SetPreviousData();
    
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            clearData();
        }

    }
}