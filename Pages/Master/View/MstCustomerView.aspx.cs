using System;
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
    public partial class MstCustomerView : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();

        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {

                loadData();
                LoadDataCombo();
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
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dr["Column4"] = string.Empty;
            dr["Column5"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdDelivery.DataSource = dt;
            grdDelivery.DataBind();
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
                        TextBox box1 = (TextBox)grdDelivery.Rows[i].FindControl("txtDelivery");
                        TextBox hid1 = (TextBox)grdDelivery.Rows[i].FindControl("txtRegion");
                        TextBox box2 = (TextBox)grdDelivery.Rows[i].FindControl("txtPhone");
                        TextBox box3 = (TextBox)grdDelivery.Rows[i].FindControl("txtContact");
                        TextBox box4 = (TextBox)grdDelivery.Rows[i].FindControl("txtGudang");

                        box1.Text = dt.Rows[i]["Column1"].ToString();
                        hid1.Text = dt.Rows[i]["Column2"].ToString();
                        box2.Text = dt.Rows[i]["Column3"].ToString();
                        box3.Text = dt.Rows[i]["Column4"].ToString();
                        box4.Text = dt.Rows[i]["Column5"].ToString();
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
                        TextBox box1 = (TextBox)grdDelivery.Rows[i].FindControl("txtDelivery");
                        TextBox hid1 = (TextBox)grdDelivery.Rows[i].FindControl("txtRegion");
                        TextBox box2 = (TextBox)grdDelivery.Rows[i].FindControl("txtPhone");
                        TextBox box3 = (TextBox)grdDelivery.Rows[i].FindControl("txtContact");
                        TextBox box4 = (TextBox)grdDelivery.Rows[i].FindControl("txtGudang");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = box1.Text;
                        dtCurrentTable.Rows[i]["Column2"] = hid1.Text;
                        dtCurrentTable.Rows[i]["Column3"] = box2.Text;
                        dtCurrentTable.Rows[i]["Column4"] = box3.Text;
                        dtCurrentTable.Rows[i]["Column5"] = box4.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdDelivery.DataSource = dtCurrentTable;
                    grdDelivery.DataBind();
                }
            }
            SetPreviousData();
        }

        protected void loadData()
        {
            grdCustomer.DataSource = ObjDb.GetRows("select * from mCustomer where namaCust LIKE '%" + txtSearch.Text + "%' or kdCust LIKE '%" + txtSearch.Text + "%'");
            grdCustomer.DataBind();
        }

        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                ObjDb.Where.Clear();
                ObjDb.Data.Clear();
                ObjDb.Where.Add("noCust", hdnId.Value);

                if (hdnMode.Value.ToLower() == "edit")
                {
                    DataSet mySet = ObjDb.GetRows("select * from  mCustomer where noCust = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtNama.Text = myRow["namaCust"].ToString();
                    hdnNoCust.Value = myRow["noCust"].ToString();
                    txtNamaAlias.Text = myRow["namaAlias"].ToString();
                    txtAlamat.Text = myRow["alamatCust"].ToString();
                    txtAgama.Text = myRow["AgamaCust"].ToString();
                    txtTelp.Text = myRow["noTelpCust"].ToString();
                    txtNoFax.Text = myRow["noFaxCust"].ToString();
                    txtEmail.Text = myRow["emailCust"].ToString();
                    txtWebsite.Text = myRow["websiteCust"].ToString();
                    txtAlamatKores.Text = myRow["alamatKores"].ToString();
                    txtAgama2.Text = myRow["AgamaKores"].ToString();
                    txtWebsite.Text = myRow["websiteCust"].ToString();
                    txtAlamatKores.Text = myRow["alamatKores"].ToString();
                    txtTelpKores.Text = myRow["noTelpKores"].ToString();
                    txtNoFax2.Text = myRow["noFaxKores"].ToString();
                    txtTerm.Text = myRow["termCust"].ToString();
                    cboSts.SelectedValue = myRow["stsPajakCust"].ToString();
                    txtNoNPWP.Text = myRow["noNPWPCust"].ToString();
                    txtNamaNPWP.Text = myRow["namaNPWPCust"].ToString();
                    txtAlamatNPWP.Text = myRow["alamatNPWPCust"].ToString();
                    txtAgama3.Text = myRow["AgamaNPWPCust"].ToString();
                    dtNPWP.Text = Convert.ToDateTime(myRow["tglNPWPCust"]).ToString("dd-MMM-yyyy");
                    txtNoPKP.Text = myRow["noPKPCust"].ToString();
                    dtPKP.Text = Convert.ToDateTime(myRow["tglPKPCust"]).ToString("dd-MMM-yyyy");
                    cboStsWAPU.SelectedValue = myRow["stsWAPU"].ToString();
                    txtKredit.Text = myRow["kreditlimit"].ToString();
                    cboSalesman.SelectedItem.Text = myRow["salesman"].ToString();
                    cboGroupCust.SelectedValue = myRow["noGroupCust"].ToString();
                    cboCetak.SelectedValue = myRow["cetakppn"].ToString();

                    grdDelivery.DataSource = ObjDb.GetRows("select * from mAlamatkirim a inner join mGudangCust b on a.noGudangCust = b.noGudangCust WHERE a.noCust = '" + hdnId.Value + "'");
                    grdDelivery.DataBind();

                    DataSet mySett = ObjDb.GetRows("select * from mCustomerCP where noCust = '" + hdnId.Value + "'");
                    DataRow myRoww = mySett.Tables[0].Rows[0];

                    txtNamaCp.Text = myRoww["namaCP"].ToString();
                    txtBagianCP.Text = myRoww["bagianCP"].ToString();
                    txtAlamatCP.Text = myRoww["alamatCP"].ToString();
                    txtJabatanCP.Text = myRoww["jabatanCP"].ToString();
                    txtNoTelpCP.Text = myRoww["noTelpCP"].ToString();
                    txtNoHpCP.Text = myRoww["noHPCP"].ToString();
                    txtEmailCP.Text = myRoww["mailCP"].ToString();
                    dtTglLahir.Text = Convert.ToDateTime(myRoww["tglLahirCP"]).ToString("dd-MMM-yyyy");

                    for (int i = 1; i <= 4; i++)
                    {
                        AddNewRow();
                    }
                    this.ShowHideGridAndForm(false, true, false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
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

        protected void LoadDataCombo()
        {
            cboGroupCust.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noGroupCust id, GroupCust name FROM mGroupcust ) a");
            cboGroupCust.DataValueField = "id";
            cboGroupCust.DataTextField = "name";
            cboGroupCust.DataBind();

            cboSalesman.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct namauser id, namauser name FROM muser where stssales = '1') a");
            cboSalesman.DataValueField = "id";
            cboSalesman.DataTextField = "name";
            cboSalesman.DataBind();

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false, false);
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            loadData();
        }
        protected void grdCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCustomer.PageIndex = e.NewPageIndex;
            loadData();
        }
    }
}