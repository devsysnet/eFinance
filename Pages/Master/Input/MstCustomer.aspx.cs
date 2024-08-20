using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;

namespace eFinance.Pages.Master.Input
{
    public partial class MstCustomer : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstCustomer.aspx");
                SetInitialRow();
                for (int i = 1; i <= 4; i++)
                {
                    AddNewRow();
                }
                LoadDataCombo();

                SetInitialRowContact();
                for (int i = 1; i < 1; i++)
                {
                    AddNewRowContact();
                }
                SetInitialRowDocument();
                for (int i = 1; i < 1; i++)
                {
                    AddNewRowDocument();
                }
            }
        }

        private void SetInitialRowDocument()
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
            ViewState["CurrentTableD"] = dt;
            grdDocument.DataSource = dt;
            grdDocument.DataBind();
        }
        private void SetPreviousDataDocument()
        {
            if (ViewState["CurrentTableD"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTableD"];
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
            if (ViewState["CurrentTableD"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableD"];
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
                    ViewState["CurrentTableD"] = dtCurrentTable;
                    grdDocument.DataSource = dtCurrentTable;
                    grdDocument.DataBind();
                }
            }
            SetPreviousDataDocument();
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

        private void SetInitialRowContact()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtName", typeof(string)));
            dt.Columns.Add(new DataColumn("dtLahir", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAlamat", typeof(string)));
            dt.Columns.Add(new DataColumn("txtNomorHP", typeof(string)));
            dt.Columns.Add(new DataColumn("txtBagian", typeof(string)));
            dt.Columns.Add(new DataColumn("txtJabatan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtTelp", typeof(string)));
            dt.Columns.Add(new DataColumn("txtEmail", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["txtName"] = string.Empty;
            dr["dtLahir"] = string.Empty;
            dr["txtAlamat"] = string.Empty;
            dr["txtNomorHP"] = string.Empty;
            dr["txtBagian"] = string.Empty;
            dr["txtJabatan"] = string.Empty;
            dr["txtTelp"] = string.Empty;
            dr["txtEmail"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTableC"] = dt;
            grdContact.DataSource = dt;
            grdContact.DataBind();
        }

        private void SetPreviousDataContact()
        {
            if (ViewState["CurrentTableC"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTableC"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtName = (TextBox)grdContact.Rows[i].FindControl("txtName");
                        TextBox dtLahir = (TextBox)grdContact.Rows[i].FindControl("dtLahir");
                        TextBox txtAlamat = (TextBox)grdContact.Rows[i].FindControl("txtAlamat");
                        TextBox txtNomorHP = (TextBox)grdContact.Rows[i].FindControl("txtNomorHP");
                        TextBox txtBagian = (TextBox)grdContact.Rows[i].FindControl("txtBagian");
                        TextBox txtJabatan = (TextBox)grdContact.Rows[i].FindControl("txtJabatan");
                        TextBox txtTelp = (TextBox)grdContact.Rows[i].FindControl("txtTelp");
                        TextBox txtEmail = (TextBox)grdContact.Rows[i].FindControl("txtEmail");

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
            if (ViewState["CurrentTableC"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableC"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtName = (TextBox)grdContact.Rows[i].FindControl("txtName");
                        TextBox dtLahir = (TextBox)grdContact.Rows[i].FindControl("dtLahir");
                        TextBox txtAlamat = (TextBox)grdContact.Rows[i].FindControl("txtAlamat");
                        TextBox txtNomorHP = (TextBox)grdContact.Rows[i].FindControl("txtNomorHP");
                        TextBox txtBagian = (TextBox)grdContact.Rows[i].FindControl("txtBagian");
                        TextBox txtJabatan = (TextBox)grdContact.Rows[i].FindControl("txtJabatan");
                        TextBox txtTelp = (TextBox)grdContact.Rows[i].FindControl("txtTelp");
                        TextBox txtEmail = (TextBox)grdContact.Rows[i].FindControl("txtEmail");

                        drCurrentRow = dtCurrentTable.NewRow();
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
                    ViewState["CurrentTableC"] = dtCurrentTable;

                    grdContact.DataSource = dtCurrentTable;
                    grdContact.DataBind();
                }
            }
            SetPreviousDataContact();
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
        private void clearData()
        {
            txtAlamat.Text = "";
            txtAlamatKores.Text = "";
            txtAlamatNPWP.Text = "";
            txtEmail.Text = "";
            txtKota.Text = "";
            txtKota2.Text = "";
            txtKota3.Text = "";
            txtKredit.Text = "";
            txtNama.Text = "";
            txtNamaAlias.Text = "";
            txtNamaNPWP.Text = "";
            txtNoFax.Text = "";
            txtNoFax2.Text = "";
            txtNoNPWP.Text = "";
            txtNoPKP.Text = "";
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
            for (int i = 0; i < grdContact.Rows.Count; i++)
            {
                TextBox txtNamaCp = (TextBox)grdContact.Rows[i].FindControl("txtName");
                TextBox txtBagianCP = (TextBox)grdContact.Rows[i].FindControl("txtBagian");
                TextBox txtAlamatCP = (TextBox)grdContact.Rows[i].FindControl("txtAlamat");
                TextBox txtJabatanCP = (TextBox)grdContact.Rows[i].FindControl("txtJabatan");
                TextBox txtTelptCP = (TextBox)grdContact.Rows[i].FindControl("txtTelp");
                TextBox txtNomorHPCP = (TextBox)grdContact.Rows[i].FindControl("txtNomorHP");
                TextBox txtEmailtCP = (TextBox)grdContact.Rows[i].FindControl("txtEmail");
                TextBox dtLahirCP = (TextBox)grdContact.Rows[i].FindControl("dtLahir");

                txtNamaCp.Text = "";
                txtBagianCP.Text = "";
                txtAlamatCP.Text = "";
                txtJabatanCP.Text = "";
                txtTelptCP.Text = "";
                txtNomorHPCP.Text = "";
                txtEmailtCP.Text = "";
                dtLahirCP.Text = "";
            }
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (txtNama.Text == "")
            {
                message += ObjSys.CreateMessage("Nama tidak boleh kosong.");
                valid = false;
            }
            if (txtNIK.Text == "" && cboSts.Text == "0")
            {
                message += ObjSys.CreateMessage("NIK tidak boleh kosong.");
                valid = false;
            }
            if (txtNIK.Text != "" && cboSts.Text == "1")
            {
                message += ObjSys.CreateMessage("NIK tidak boleh di isi.");
                valid = false;
            }
            if (txtNoNPWP.Text == "" && cboSts.Text == "1")
            {
                message += ObjSys.CreateMessage("NPWP tidak boleh kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                    if (ObjDb.GetRows("select * from mCustomer where namaCust = '" + txtNama.Text + "'").Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Nama Customer sudah ada.");
                    }
                    else if (ObjDb.GetRows("select * from mCustomer where noNPWPCust = '" + txtNoNPWP.Text + "'").Tables[0].Rows.Count > 0 && txtNoNPWP.Text != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "No NPWP sudah terpakai.");
                    }
                    else
                    {

                        //string Kode = ObjSys.GetCodeAutoNumber("63", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                        string Kode = ObjSys.GetCodeAutoNumberNew("8", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                        ObjDb.Data.Clear();
                        ObjDb.Data.Add("kdCust", Kode);
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
                        ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                        if (txtKredit.Text != "")
                        {
                            ObjDb.Data.Add("kreditlimit", Convert.ToDecimal(txtKredit.Text).ToString());
                            ObjDb.Data.Add("sisakreditlimit", Convert.ToDecimal(txtKredit.Text).ToString());
                        }
                        else
                        {
                            ObjDb.Data.Add("kreditlimit", "0");
                            ObjDb.Data.Add("sisakreditlimit", "0");
                        }
                        ObjDb.Data.Add("salesman", cboSalesman.SelectedValue);
                        ObjDb.Data.Add("noGroupCust", cboGroupCust.SelectedValue);
                        ObjDb.Data.Add("cetakppn", cboCetak.SelectedValue);
                        ObjDb.Data.Add("nik", txtNIK.Text);
                        ObjDb.Insert("mCustomer", ObjDb.Data);
                        //ObjSys.UpdateAutoNumberCode("63", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                        ObjSys.UpdateAutoNumberCodeNew("8", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
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
                            if (txtDelivery.Text != "" && txtRegion.Text != "" && txtPhone.Text != "" && txtContact.Text != "" && txtGudang.Text != "")
                            {
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

                        for (int i = 0; i < grdContact.Rows.Count; i++)
                        {
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

                        for (int i = 0; i < grdDocument.Rows.Count; i++)
                        {
                            TextBox txtNamaCp = (TextBox)grdDocument.Rows[i].FindControl("txtNamaSurat");
                            TextBox txtBagianCP = (TextBox)grdDocument.Rows[i].FindControl("dtAwal");
                            TextBox txtAlamatCP = (TextBox)grdDocument.Rows[i].FindControl("dtAkhir");
                            FileUpload flUpload = (FileUpload)grdDocument.Rows[i].FindControl("flUpload");

                            if (txtNamaCp.Text != "")
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noCust", noCust);
                                ObjDb.Data.Add("namaSurat", txtNamaCp.Text);
                                ObjDb.Data.Add("tglMasaberlakuAwal", txtBagianCP.Text);
                                ObjDb.Data.Add("tglMasaberlakuAkhir", txtAlamatCP.Text);
                                if (flUpload.HasFile)
                                {
                                    ObjDb.Data.Add("upload", flUpload.FileName);
                                    flUpload.SaveAs(Server.MapPath("~/Assets/DocScreening/" + flUpload.FileName));
                                }
                                ObjDb.Insert("mDocumentCust", ObjDb.Data);
                            }
                        }
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
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearData();
        }

        protected void btnAddContact_Click(object sender, EventArgs e)
        {
            AddNewRowContact();
        }

        protected void btnAddDocument_Click(object sender, EventArgs e)
        {
            AddNewRowDocument();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }
    }
}