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
    public partial class MstApprovalHargaView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void SetInitialRow(string noApprove = "")
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("Jabatan", typeof(string)));
                dt.Columns.Add(new DataColumn("drNilai", typeof(string)));
                dt.Columns.Add(new DataColumn("keNilai", typeof(string)));

                DataSet mySet = ObjDb.GetRows("SELECT a.drNilai, a.keNilai, b.hakAkses from MstApproveNilai a " +
                    "inner join mAkses b on a.noAkses = b.noAkses where a.noApprove='" + noApprove + "'");
                foreach (DataRow myRow in mySet.Tables[0].Rows)
                {
                    dr = dt.NewRow();
                    dr["Jabatan"] = myRow["hakAkses"].ToString();
                    dr["drNilai"] = ObjSys.IsFormatNumber(myRow["drNilai"].ToString());
                    dr["keNilai"] = ObjSys.IsFormatNumber(myRow["keNilai"].ToString());
                    dt.Rows.Add(dr);
                }
                if (mySet.Tables[0].Rows.Count == 0)
                {
                    dr = dt.NewRow();
                    dr["Jabatan"] = string.Empty;
                    dr["drNilai"] = string.Empty;
                    dr["keNilai"] = string.Empty;
                    dt.Rows.Add(dr);
                }
                ViewState["CurrentTable"] = dt;
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
                SetPreviousData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
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
                        DropDownList cboJabatan = (DropDownList)grdApproval.Rows[i].FindControl("cboJabatan");
                        TextBox txtdrNilai = (TextBox)grdApproval.Rows[i].FindControl("txtdrNilai");
                        TextBox txtkeNilai = (TextBox)grdApproval.Rows[i].FindControl("txtkeNilai");

                        cboJabatan.Text = dt.Rows[i]["Jabatan"].ToString();
                        txtdrNilai.Text = dt.Rows[i]["drNilai"].ToString();
                        txtkeNilai.Text = dt.Rows[i]["keNilai"].ToString();
                    }
                }
            }
        }

        private void LoadData()
        {
            grdApprovalScheme.DataSource = ObjDb.GetRows("select * from MstApproveNilai a inner join " +
                "MstParameterApprove b on a.noParameterApprove = b.noParameterApprove inner join " +
                "mUser c on a.noUser = c.noUser inner join mCabang d on d.noCabang = c.noCabang inner join " +
                "mAkses e on e.noAkses = a.noAkses " +
                "where (c.namauser LIKE '%" + txtSearch.Text + "%' " +
                "or b.namaParameterApprove LIKE '%" + txtSearch.Text + "%') and a.peruntukan = '" + cboUntukHeader.Text + "' ");
            grdApprovalScheme.DataBind();
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
        }
        protected void ClearData()
        {

            CloseMessage();
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
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void grdApprovalScheme_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdApprovalScheme.PageIndex = e.NewPageIndex;
            CloseMessage();
            LoadData();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ShowHideGridAndForm(true, false, false);
            CloseMessage();
        }
        protected void grdApprovalScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = grdApprovalScheme.SelectedRow.RowIndex;
                string noApprove = grdApprovalScheme.DataKeys[rowIndex].Values[0].ToString();
                HiddenField hdnnoParameterApprove = (HiddenField)grdApprovalScheme.Rows[rowIndex].FindControl("hdnnoParameterApprove");
                hdnId.Value = noApprove;
                DataSet mySet = ObjDb.GetRows("SELECT * from MstApproveNilai a left join MstParameterApprove c on a.noParameterApprove=c.noParameterApprove where a.noApprove='" + noApprove + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];

                cboCategory.DataSource = ObjDb.GetRows("select a.* from (select '' no,'---Pilih Kategori---' nama " +
                "union all " +
                "SELECT distinct noParameterApprove no, namaParameterApprove nama FROM MstParameterApprove) a");
                cboCategory.DataValueField = "no";
                cboCategory.DataTextField = "nama";
                cboCategory.DataBind();

                cboCategory.Text = myRow["noParameterApprove"].ToString();
                cboUntuk.Text = myRow["peruntukan"].ToString();

                SetInitialRow(noApprove);
                ShowHideGridAndForm(false, true, false);
                CloseMessage();
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
    }
}