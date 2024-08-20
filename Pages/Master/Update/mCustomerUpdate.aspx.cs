using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;

namespace eFinance.Pages.Master.Update
{
    public partial class mCustomerUpdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();

        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                //ObjSys.SessionCheck("mCustomerUpdate.aspx");
                loadData();
                LoadDataCombo();
            }
        }
        private void SetInitialRowDocument(string noCust = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string)));
            DataSet mySet = ObjDb.GetRows("select * from mDocumentCust where noCust = '" + noCust + "' ");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = myRow["namaSurat"].ToString();
                dr["Column2"] = Convert.ToDateTime(myRow["tglMasaberlakuAwal"].ToString()).ToString("yyyy-MM-dd");
                dr["Column3"] = Convert.ToDateTime(myRow["tglMasaberlakuAkhir"].ToString()).ToString("yyyy-MM-dd");
                dr["Column4"] = string.Empty;
                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = string.Empty;
                dr["Column2"] = string.Empty;
                dr["Column3"] = string.Empty;
                dr["Column4"] = string.Empty;
                dt.Rows.Add(dr);

            }
            ViewState["CurrentTableG"] = dt;
            grdDocument.DataSource = dt;
            grdDocument.DataBind();
            SetPreviousDataDocument();
        }
        private void SetPreviousDataDocument()
        {
            if (ViewState["CurrentTableG"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTableG"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox box1 = (TextBox)grdDocument.Rows[i].FindControl("txtNamaSurat");
                        TextBox hid1 = (TextBox)grdDocument.Rows[i].FindControl("dtAwal");
                        TextBox box2 = (TextBox)grdDocument.Rows[i].FindControl("dtAkhir");
                        FileUpload flUpload = (FileUpload)grdDocument.Rows[i].FindControl("flUpload");

                        box1.Text = dt.Rows[i]["Column1"].ToString();
                        hid1.Text = dt.Rows[i]["Column2"].ToString();
                        box2.Text = dt.Rows[i]["Column3"].ToString();
                        string filename = flUpload.FileName;
                        filename = dt.Rows[i]["Column4"].ToString();
                    }
                }
            }
        }
        private void AddNewRowDocument()
        {
            if (ViewState["CurrentTableG"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableG"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox box1 = (TextBox)grdDocument.Rows[i].FindControl("txtNamaSurat");
                        TextBox hid1 = (TextBox)grdDocument.Rows[i].FindControl("dtAwal");
                        TextBox box2 = (TextBox)grdDocument.Rows[i].FindControl("dtAkhir");
                        FileUpload flUpload = (FileUpload)grdDocument.Rows[i].FindControl("flUpload");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = box1.Text;
                        dtCurrentTable.Rows[i]["Column2"] = hid1.Text;
                        dtCurrentTable.Rows[i]["Column3"] = box2.Text;
                        dtCurrentTable.Rows[i]["Column4"] = flUpload.FileName;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTableG"] = dtCurrentTable;
                    grdDocument.DataSource = dtCurrentTable;
                    grdDocument.DataBind();
                }
            }
            SetPreviousDataDocument();
        }
        private void SetInitialRow(string noCust = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string)));
            dt.Columns.Add(new DataColumn("Column5", typeof(string)));
            DataSet mySet = ObjDb.GetRows("select * from mAlamatkirim a left join mGudangCust b on a.noGudangCust = b.noGudangCust WHERE a.noCust = '" + noCust + "'");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = myRow["alamat"].ToString();
                dr["Column2"] = myRow["wilayah"].ToString();
                dr["Column3"] = myRow["notelp"].ToString();
                dr["Column4"] = myRow["contact"].ToString();
                dr["Column5"] = myRow["nmGudangCust"].ToString();
                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = string.Empty;
                dr["Column2"] = string.Empty;
                dr["Column3"] = string.Empty;
                dr["Column4"] = string.Empty;
                dr["Column5"] = string.Empty;

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTableW"] = dt;
            grdDelivery.DataSource = dt;
            grdDelivery.DataBind();
            SetPreviousData();
        }
        private void SetPreviousData()
        {
            if (ViewState["CurrentTableW"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTableW"];
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
            if (ViewState["CurrentTableW"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableW"];
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
                    ViewState["CurrentTableW"] = dtCurrentTable;
                    grdDelivery.DataSource = dtCurrentTable;
                    grdDelivery.DataBind();
                }
            }
            SetPreviousData();
        }

        private void SetInitialRowContact(string noCust = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnNoCP", typeof(string)));
            dt.Columns.Add(new DataColumn("txtName", typeof(string)));
            dt.Columns.Add(new DataColumn("dtLahir", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAlamat", typeof(string)));
            dt.Columns.Add(new DataColumn("txtNomorHP", typeof(string)));
            dt.Columns.Add(new DataColumn("txtBagian", typeof(string)));
            dt.Columns.Add(new DataColumn("txtJabatan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTelp", typeof(string)));
            dt.Columns.Add(new DataColumn("txtEmail", typeof(string)));
            DataSet mySet = ObjDb.GetRows("select * from mCustomerCP where noCust = '" + noCust + "'");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnNoCP"] = myRow["noCP"].ToString();
                dr["txtName"] = myRow["namaCP"].ToString();
                dr["dtLahir"] = Convert.ToDateTime(myRow["tglLahirCP"]).ToString("dd-MMM-yyyy");
                dr["txtAlamat"] = myRow["alamatCP"].ToString();
                dr["txtNomorHP"] = myRow["noHPCP"].ToString();
                dr["txtBagian"] = myRow["bagianCP"].ToString();
                dr["txtJabatan"] = myRow["jabatanCP"].ToString();
                dr["txtTelp"] = myRow["noTelpCP"].ToString();
                dr["txtEmail"] = myRow["mailCP"].ToString();
                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnNoCP"] = string.Empty;
                dr["txtName"] = string.Empty;
                dr["dtLahir"] = string.Empty;
                dr["txtAlamat"] = string.Empty;
                dr["txtNomorHP"] = string.Empty;
                dr["txtBagian"] = string.Empty;
                dr["txtJabatan"] = string.Empty;
                dr["txtTelp"] = string.Empty;
                dr["txtEmail"] = string.Empty;

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdContact.DataSource = dt;
            grdContact.DataBind();
            SetPreviousDataContact();
        }

        private void SetPreviousDataContact()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HiddenField hdnNoCP = (HiddenField)grdContact.Rows[i].FindControl("hdnNoCP");
                        TextBox txtName = (TextBox)grdContact.Rows[i].FindControl("txtName");
                        TextBox dtLahir = (TextBox)grdContact.Rows[i].FindControl("dtLahir");
                        TextBox txtAlamat = (TextBox)grdContact.Rows[i].FindControl("txtAlamat");
                        TextBox txtNomorHP = (TextBox)grdContact.Rows[i].FindControl("txtNomorHP");
                        TextBox txtBagian = (TextBox)grdContact.Rows[i].FindControl("txtBagian");
                        TextBox txtJabatan = (TextBox)grdContact.Rows[i].FindControl("txtJabatan");
                        TextBox txtTelp = (TextBox)grdContact.Rows[i].FindControl("txtTelp");
                        TextBox txtEmail = (TextBox)grdContact.Rows[i].FindControl("txtEmail");

                        hdnNoCP.Value = dt.Rows[i]["hdnNoCP"].ToString();
                        txtName.Text = dt.Rows[i]["txtName"].ToString();
                        dtLahir.Text = dt.Rows[i]["dtLahir"].ToString();
                        txtAlamat.Text = dt.Rows[i]["txtAlamat"].ToString();
                        txtNomorHP.Text = dt.Rows[i]["txtNomorHP"].ToString();
                        txtBagian.Text = dt.Rows[i]["txtBagian"].ToString();
                        txtJabatan.Text = dt.Rows[i]["txtJabatan"].ToString();
                        txtTelp.Text = dt.Rows[i]["txtTelp"].ToString();
                        txtEmail.Text = dt.Rows[i]["txtEmail"].ToString();
                    }
                }
            }
        }

        private void AddNewRowContact()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        HiddenField hdnNoCP = (HiddenField)grdContact.Rows[i].FindControl("hdnNoCP");
                        TextBox txtName = (TextBox)grdContact.Rows[i].FindControl("txtName");
                        TextBox dtLahir = (TextBox)grdContact.Rows[i].FindControl("dtLahir");
                        TextBox txtAlamat = (TextBox)grdContact.Rows[i].FindControl("txtAlamat");
                        TextBox txtNomorHP = (TextBox)grdContact.Rows[i].FindControl("txtNomorHP");
                        TextBox txtBagian = (TextBox)grdContact.Rows[i].FindControl("txtBagian");
                        TextBox txtJabatan = (TextBox)grdContact.Rows[i].FindControl("txtJabatan");
                        TextBox txtTelp = (TextBox)grdContact.Rows[i].FindControl("txtTelp");
                        TextBox txtEmail = (TextBox)grdContact.Rows[i].FindControl("txtEmail");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["hdnNoCP"] = hdnNoCP.Value;
                        dtCurrentTable.Rows[i]["txtName"] = txtName.Text;
                        dtCurrentTable.Rows[i]["dtLahir"] = dtLahir.Text;
                        dtCurrentTable.Rows[i]["txtAlamat"] = txtAlamat.Text;
                        dtCurrentTable.Rows[i]["txtNomorHP"] = txtNomorHP.Text;
                        dtCurrentTable.Rows[i]["txtBagian"] = txtBagian.Text;
                        dtCurrentTable.Rows[i]["txtJabatan"] = txtJabatan.Text;
                        dtCurrentTable.Rows[i]["txtTelp"] = txtTelp.Text;
                        dtCurrentTable.Rows[i]["txtEmail"] = txtEmail.Text;

                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grdContact.DataSource = dtCurrentTable;
                    grdContact.DataBind();
                }
            }
            SetPreviousDataContact();
        }
        protected void loadData()
        {
            grdCustomer.DataSource = ObjDb.GetRows("select * from mCustomer where namaCust LIKE '%" + txtSearch.Text + "%' or kdCust LIKE '%" + txtSearch.Text + "%'");
            grdCustomer.DataBind();
            CloseMessage();
        }

        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnMode.Value.ToLower() == "deleteall")
                {
                    /*DELETE ALL SELECTED*/

                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noCust", itemRow);
                            ObjDb.Delete("mCustomer", ObjDb.Where);
                            ObjDb.Delete("mCustomerCP", ObjDb.Where);
                            ObjDb.Delete("mGudangCust", ObjDb.Where);
                            ObjDb.Delete("mAlamatKirim", ObjDb.Where);
                        }
                    }
                    for (int i = 0; i < grdCustomer.Rows.Count; i++)
                    {
                        string itemId = grdCustomer.DataKeys[i].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdCustomer.Rows[i].FindControl("chkCheck");

                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noCust", itemId);
                            ObjDb.Delete("mCustomer", ObjDb.Where);
                            ObjDb.Delete("mCustomerCP", ObjDb.Where);
                            ObjDb.Delete("mGudangCust", ObjDb.Where);
                            ObjDb.Delete("mAlamatKirim", ObjDb.Where);
                        }
                    }
                }
                /*END DELETE ALL SELECTED*/
                loadData();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil dihapus.");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
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
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
        }

        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdCustomer.Rows)
                {
                    string index = grdCustomer.DataKeys[gvrow.RowIndex].Value.ToString();
                    if (userdetails.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)gvrow.FindControl("chkCheck");
                        myCheckBox.Checked = true;
                    }
                }
            }
        }
        private void SaveCheckedValues()
        {
            ArrayList userdetails = new ArrayList();
            foreach (GridViewRow gvrow in grdCustomer.Rows)
            {
                string index = grdCustomer.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdCustomer.Rows[gvrow.RowIndex].FindControl("chkCheck");
                bool result = chkCheck.Checked;
                // Check in the Session
                if (ViewState["CHECKED_ITEMS"] != null)
                    userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
                if (result)
                {
                    if (!userdetails.Contains(index))
                        userdetails.Add(index);
                }
                else
                    userdetails.Remove(index);
            }
            if (userdetails != null && userdetails.Count > 0)
                ViewState["CHECKED_ITEMS"] = userdetails;
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
            loadData();
        }
        private void clearData()
        {
            txtAlamat.Text = "";
            // txtAlamatCP.Text = "";
            txtAlamatKores.Text = "";
            txtAlamatNPWP.Text = "";
            //txtBagianCP.Text = "";
            txtEmail.Text = "";
            //   txtEmailCP.Text = "";
            //   txtJabatanCP.Text = "";
            txtKota.Text = "";
            txtKota2.Text = "";
            txtKota3.Text = "";
            txtKredit.Text = "";
            txtNama.Text = "";
            txtNamaAlias.Text = "";
            /// txtNamaCp.Text = "";
            txtNamaNPWP.Text = "";
            txtNoFax.Text = "";
            txtNoFax2.Text = "";
            // txtNoHpCP.Text = "";
            txtNoNPWP.Text = "";
            txtNoPKP.Text = "";
            //txtNoTelpCP.Text = "";
            txtTelp.Text = "";
            txtTelpKores.Text = "";
            txtTerm.Text = "";
            txtWebsite.Text = "";
            for (int i = 0; i < grdDelivery.Rows.Count; i++)
            {
                TextBox txtDelivery = (TextBox)grdDelivery.Rows[i].FindControl("txtDelivery");
                TextBox txtRegion = (TextBox)grdDelivery.Rows[i].FindControl("txtRegion");
                TextBox txtPhone = (TextBox)grdDelivery.Rows[i].FindControl("txtPhone");
                TextBox txtContact = (TextBox)grdDelivery.Rows[i].FindControl("txtContact");
                TextBox txtGudang = (TextBox)grdDelivery.Rows[i].FindControl("txtGudang");

                txtDelivery.Text = "";
                txtRegion.Text = "";
                txtPhone.Text = "";
                txtContact.Text = "";
                txtGudang.Text = "";
            }
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            CloseMessage();
            bool valid = true;
            ObjDb.Data.Clear();
            ObjDb.Where.Clear();
            string fileNameUpload = "";

            if (txtNama.Text == "")
            {
                message += ObjSys.CreateMessage("Nama Product tidak boleh kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noCust", hdnNoCust.Value);
                    ObjDb.Data.Add("namaCust", txtNama.Text);
                    ObjDb.Data.Add("namaAlias", txtNamaAlias.Text);
                    ObjDb.Data.Add("alamatCust", txtAlamat.Text);
                    ObjDb.Data.Add("kotaCust", txtKota.Text);
                    ObjDb.Data.Add("noTelpCust", txtTelp.Text);
                    ObjDb.Data.Add("noFaxCust", txtNoFax.Text);
                    ObjDb.Data.Add("emailCust", txtEmail.Text);
                    ObjDb.Data.Add("websiteCust", txtWebsite.Text);
                    ObjDb.Data.Add("alamatKores", txtAlamatKores.Text);
                    ObjDb.Data.Add("kotaKores", txtKota2.Text);
                    ObjDb.Data.Add("noTelpKores", txtTelpKores.Text);
                    ObjDb.Data.Add("noFaxKores", txtNoFax2.Text);
                    ObjDb.Data.Add("termCust", txtTerm.Text);
                    ObjDb.Data.Add("stsPajakCust", cboSts.SelectedValue);
                    ObjDb.Data.Add("noNPWPCust", txtNoNPWP.Text);
                    ObjDb.Data.Add("namaNPWPCust", txtNamaNPWP.Text);
                    ObjDb.Data.Add("alamatNPWPCust", txtAlamatNPWP.Text);
                    ObjDb.Data.Add("kotaNPWPCust", txtKota3.Text);
                    ObjDb.Data.Add("tglNPWPCust", dtNPWP.Text);
                    ObjDb.Data.Add("noPKPCust", txtNoPKP.Text);
                    ObjDb.Data.Add("tglPKPCust", dtPKP.Text);
                    ObjDb.Data.Add("stsWAPU", cboStsWAPU.SelectedValue);
                    ObjDb.Data.Add("stsCust", "1");
                    ObjDb.Data.Add("modifiedBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modifiedDate", ObjSys.GetNow);
                    if (txtKredit.Text != "")
                    {
                        ObjDb.Data.Add("kreditlimit", Convert.ToDecimal(txtKredit.Text).ToString());
                        ObjDb.Data.Add("sisakreditlimit", Convert.ToDecimal(txtKredit.Text).ToString());
                    }
                    ObjDb.Data.Add("salesman", cboSalesman.SelectedValue);
                    ObjDb.Data.Add("noGroupCust", cboGroupCust.SelectedValue);
                    ObjDb.Data.Add("cetakppn", cboCetak.SelectedValue);
                    ObjDb.Data.Add("nik", txtNIK.Text);
                    ObjDb.Update("mCustomer", ObjDb.Data, ObjDb.Where);
                    //--------------
                    DataSet mySetH = ObjDb.GetRows("select * from mCustomer where namaCust = '" + txtNama.Text + "'");
                    DataRow myRowH = mySetH.Tables[0].Rows[0];
                    string noCust = myRowH["noCust"].ToString();

                    for (int i = 0; i < grdDelivery.Rows.Count; i++)
                    {
                        TextBox txtDelivery = (TextBox)grdDelivery.Rows[i].FindControl("txtDelivery");
                        TextBox txtRegion = (TextBox)grdDelivery.Rows[i].FindControl("txtRegion");
                        TextBox txtPhone = (TextBox)grdDelivery.Rows[i].FindControl("txtPhone");
                        TextBox txtContact = (TextBox)grdDelivery.Rows[i].FindControl("txtContact");
                        TextBox txtGudang = (TextBox)grdDelivery.Rows[i].FindControl("txtGudang");

                        if (ObjDb.GetRows("select * from mGudangCust where nmGudangCust = '" + txtGudang.Text + "'").Tables[0].Rows.Count > 0)
                        {
                            ObjDb.Where.Add("nmGudangCust", txtGudang.Text);
                            ObjDb.Delete("mGudangCust", ObjDb.Where);

                            string KodeGud = ObjSys.GetCodeAutoNumber("64", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noCust", noCust);
                            ObjDb.Data.Add("kdGudangCust", KodeGud);
                            ObjDb.Data.Add("nmGudangCust", txtGudang.Text);
                            ObjDb.Data.Add("sts", "1");
                            ObjDb.Data.Add("createBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("createDate", ObjSys.GetNow);
                            ObjDb.Insert("mGudangCust", ObjDb.Data);
                            ObjSys.UpdateAutoNumberCode("64", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));

                            DataSet mySetHH = ObjDb.GetRows("select * from mGudangCust where nmGudangCust = '" + txtGudang.Text + "'");
                            DataRow myRowHH = mySetHH.Tables[0].Rows[0];
                            string noGudCust = myRowHH["noGudangCust"].ToString();
                            if (ObjDb.GetRows("select * from mAlamatKirim where contact = '" + txtContact.Text + "' and wilayah = '" + txtRegion.Text + "'").Tables[0].Rows.Count > 0)
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Where.Clear();
                                ObjDb.Where.Add("noCust", noCust);
                                ObjDb.Data.Add("alamat", txtDelivery.Text);
                                ObjDb.Data.Add("wilayah", txtRegion.Text);
                                ObjDb.Data.Add("notelp", txtPhone.Text);
                                ObjDb.Data.Add("contact", txtContact.Text);
                                ObjDb.Data.Add("nogudangCust", noGudCust);
                                ObjDb.Update("mAlamatKirim", ObjDb.Data, ObjDb.Where);
                            }
                            else
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noCust", noCust);
                                ObjDb.Data.Add("alamat", txtDelivery.Text);
                                ObjDb.Data.Add("wilayah", txtRegion.Text);
                                ObjDb.Data.Add("notelp", txtPhone.Text);
                                ObjDb.Data.Add("contact", txtContact.Text);
                                ObjDb.Data.Add("nogudangCust", noGudCust);
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Insert("mAlamatKirim", ObjDb.Data);
                            }
                        }
                        else
                        {
                            ObjDb.Where.Add("nmGudangCust", txtGudang.Text);
                            ObjDb.Delete("mGudangCust", ObjDb.Where);

                            string KodeGud = ObjSys.GetCodeAutoNumber("64", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noCust", noCust);
                            ObjDb.Data.Add("kdGudangCust", KodeGud);
                            ObjDb.Data.Add("nmGudangCust", txtGudang.Text);
                            ObjDb.Data.Add("sts", "1");
                            ObjDb.Data.Add("createBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("createDate", ObjSys.GetNow);
                            ObjDb.Insert("mGudangCust", ObjDb.Data);
                            ObjSys.UpdateAutoNumberCode("64", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));

                            DataSet mySetHH = ObjDb.GetRows("select * from mGudangCust where nmGudangCust = '" + txtGudang.Text + "'");
                            DataRow myRowHH = mySetHH.Tables[0].Rows[0];
                            string noGudCust = myRowHH["noGudangCust"].ToString();
                            if (ObjDb.GetRows("select * from mAlamatKirim where contact = '" + txtContact.Text + "' and wilayah = '" + txtRegion.Text + "'").Tables[0].Rows.Count > 0)
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Where.Clear();
                                ObjDb.Where.Add("noCust", noCust);
                                ObjDb.Data.Add("alamat", txtDelivery.Text);
                                ObjDb.Data.Add("wilayah", txtRegion.Text);
                                ObjDb.Data.Add("notelp", txtPhone.Text);
                                ObjDb.Data.Add("contact", txtContact.Text);
                                if (txtGudang.Text != "")
                                    ObjDb.Data.Add("nogudangCust", noGudCust);
                                ObjDb.Update("mAlamatKirim", ObjDb.Data, ObjDb.Where);
                            }
                            else
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noCust", noCust);
                                ObjDb.Data.Add("alamat", txtDelivery.Text);
                                ObjDb.Data.Add("wilayah", txtRegion.Text);
                                ObjDb.Data.Add("notelp", txtPhone.Text);
                                ObjDb.Data.Add("contact", txtContact.Text);
                                if (txtGudang.Text != "")
                                    ObjDb.Data.Add("nogudangCust", noGudCust);
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Insert("mAlamatKirim", ObjDb.Data);
                            }
                        }
                    }
                    for (int i = 0; i < grdContact.Rows.Count; i++)
                    {
                        HiddenField hdnNoCP = (HiddenField)grdContact.Rows[i].FindControl("hdnNoCP");
                        TextBox txtNamaCp = (TextBox)grdContact.Rows[i].FindControl("txtName");
                        TextBox txtBagianCP = (TextBox)grdContact.Rows[i].FindControl("txtBagian");
                        TextBox txtAlamatCP = (TextBox)grdContact.Rows[i].FindControl("txtAlamat");
                        TextBox txtJabatanCP = (TextBox)grdContact.Rows[i].FindControl("txtJabatan");
                        TextBox txtTelptCP = (TextBox)grdContact.Rows[i].FindControl("txtTelp");
                        TextBox txtNomorHPCP = (TextBox)grdContact.Rows[i].FindControl("txtNomorHP");
                        TextBox txtEmailtCP = (TextBox)grdContact.Rows[i].FindControl("txtEmail");
                        TextBox dtLahirCP = (TextBox)grdContact.Rows[i].FindControl("dtLahir");

                        if (txtNamaCp.Text != "")
                        {
                            if (ObjDb.GetRows("select * from mCustomerCP where noCP = '" + hdnNoCP.Value + "'").Tables[0].Rows.Count > 0)
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Where.Clear();
                                ObjDb.Where.Add("noCP", hdnNoCP.Value);
                                ObjDb.Data.Add("namaCP", txtNamaCp.Text);
                                ObjDb.Data.Add("bagianCP", txtBagianCP.Text);
                                ObjDb.Data.Add("alamatCP", txtAlamatCP.Text);
                                ObjDb.Data.Add("jabatanCP", txtJabatanCP.Text);
                                ObjDb.Data.Add("noTelpCP", txtTelptCP.Text);
                                ObjDb.Data.Add("noHPCP", txtNomorHPCP.Text);
                                ObjDb.Data.Add("mailCP", txtEmailtCP.Text);
                                ObjDb.Data.Add("tglLahirCP", dtLahirCP.Text);
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Update("mCustomerCP", ObjDb.Data, ObjDb.Where);
                            }
                            else
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noCust", noCust);
                                ObjDb.Data.Add("namaCP", txtNamaCp.Text);
                                ObjDb.Data.Add("bagianCP", txtBagianCP.Text);
                                ObjDb.Data.Add("alamatCP", txtAlamatCP.Text);
                                ObjDb.Data.Add("jabatanCP", txtJabatanCP.Text);
                                ObjDb.Data.Add("noTelpCP", txtTelptCP.Text);
                                ObjDb.Data.Add("noHPCP", txtNomorHPCP.Text);
                                ObjDb.Data.Add("mailCP", txtEmailtCP.Text);
                                ObjDb.Data.Add("tglLahirCP", dtLahirCP.Text);
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Insert("mCustomerCP", ObjDb.Data);
                            }
                        }
                    }

                    for (int i = 0; i < grdDocument.Rows.Count; i++)
                    {
                        TextBox txtNamaCp1 = (TextBox)grdDocument.Rows[i].FindControl("txtNamaSurat");
                        TextBox txtBagianCP1 = (TextBox)grdDocument.Rows[i].FindControl("dtAwal");
                        TextBox txtAlamatCP1 = (TextBox)grdDocument.Rows[i].FindControl("dtAkhir");
                        FileUpload flUpload = (FileUpload)grdDocument.Rows[i].FindControl("flUpload");

                        if (txtNamaCp1.Text != "")
                        {
                            if (ObjDb.GetRows("select * from mDocumentCust where namaSurat = '" + txtNamaCp1.Text + "'").Tables[0].Rows.Count > 0)
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noCust", noCust);
                                ObjDb.Data.Add("namaSurat", txtNamaCp1.Text);
                                ObjDb.Data.Add("tglMasaberlakuAwal", txtBagianCP1.Text);
                                ObjDb.Data.Add("tglMasaberlakuAkhir", txtAlamatCP1.Text);
                                if (flUpload.HasFile == true)
                                {
                                    ObjDb.Data.Add("upload", flUpload.FileName);
                                    fileNameUpload = flUpload.FileName;
                                    flUpload.SaveAs(Server.MapPath("~/Assets/DocScreening/" + flUpload.FileName));
                                }
                                ObjDb.Update("mDocumentCust", ObjDb.Data);
                            }
                            else
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noCust", noCust);
                                ObjDb.Data.Add("namaSurat", txtNamaCp1.Text);
                                ObjDb.Data.Add("tglMasaberlakuAwal", txtBagianCP1.Text);
                                ObjDb.Data.Add("tglMasaberlakuAkhir", txtAlamatCP1.Text);
                                if (flUpload.HasFile == true)
                                {
                                    ObjDb.Data.Add("upload", flUpload.FileName);
                                    fileNameUpload = flUpload.FileName;
                                    flUpload.SaveAs(Server.MapPath("~/Assets/DocScreening/" + flUpload.FileName));
                                }
                                ObjDb.Insert("mDocumentCust", ObjDb.Data);
                            }
                        }
                    }

                    this.ShowHideGridAndForm(true, false, false);
                    loadData();
                    clearData();
                    ShowMessage("success", "Data berhasil disimpan.");
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

        protected void grdCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCustomer.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false, false);
            clearData();
            CloseMessage();
        }

        protected void grdCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdCustomer.SelectedRow.RowIndex;
                string noQoutation = grdCustomer.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noQoutation;

                DataSet MySet = ObjDb.GetRows("select * from  mCustomer where noCust = '" + noQoutation + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow myRow = MySet.Tables[0].Rows[0];

                    txtNama.Text = myRow["namaCust"].ToString();
                    hdnNoCust.Value = myRow["noCust"].ToString();
                    txtNamaAlias.Text = myRow["namaAlias"].ToString();
                    txtAlamat.Text = myRow["alamatCust"].ToString();
                    txtKota.Text = myRow["kotaCust"].ToString();
                    txtTelp.Text = myRow["noTelpCust"].ToString();
                    txtNoFax.Text = myRow["noFaxCust"].ToString();
                    txtEmail.Text = myRow["emailCust"].ToString();
                    txtWebsite.Text = myRow["websiteCust"].ToString();
                    txtAlamatKores.Text = myRow["alamatKores"].ToString();
                    txtKota2.Text = myRow["kotaKores"].ToString();
                    txtWebsite.Text = myRow["websiteCust"].ToString();
                    txtAlamatKores.Text = myRow["alamatKores"].ToString();
                    txtTelpKores.Text = myRow["noTelpKores"].ToString();
                    txtNoFax2.Text = myRow["noFaxKores"].ToString();
                    txtTerm.Text = myRow["termCust"].ToString();
                    cboSts.SelectedValue = myRow["stsPajakCust"].ToString();
                    txtNoNPWP.Text = myRow["noNPWPCust"].ToString();
                    txtNamaNPWP.Text = myRow["namaNPWPCust"].ToString();
                    txtAlamatNPWP.Text = myRow["alamatNPWPCust"].ToString();
                    txtKota3.Text = myRow["kotaNPWPCust"].ToString();
                    dtNPWP.Text = Convert.ToDateTime(myRow["tglNPWPCust"]).ToString("dd-MMM-yyyy");
                    txtNoPKP.Text = myRow["noPKPCust"].ToString();
                    dtPKP.Text = Convert.ToDateTime(myRow["tglPKPCust"]).ToString("dd-MMM-yyyy");
                    cboStsWAPU.SelectedValue = myRow["stsWAPU"].ToString();
                    txtKredit.Text = myRow["kreditlimit"].ToString();
                    cboSalesman.SelectedItem.Text = myRow["salesman"].ToString();
                    cboGroupCust.SelectedValue = myRow["noGroupCust"].ToString();
                    cboCetak.SelectedValue = myRow["cetakppn"].ToString();
                    txtNIK.Text = myRow["nik"].ToString();

                    SetInitialRow(noQoutation);
                    SetInitialRowDocument(noQoutation);
                    SetInitialRowContact(noQoutation);



                    //for (int i = 1; i <= 4; i++)
                    //{
                    //    AddNewRow();
                    //}
                    this.ShowHideGridAndForm(false, true, false);
                    CloseMessage();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Tidak ada.");
                }

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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void btnAddContact_Click(object sender, EventArgs e)
        {
            AddNewRowContact();
        }

        protected void btnAddDocument_Click(object sender, EventArgs e)
        {
            AddNewRowDocument();
        }
    }
}