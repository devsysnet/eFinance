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
    public partial class MstApprovalHargaInput : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstApprovalHargaInput.aspx");
                loadComboKategori();
                showhide.Visible = false;
                showhideSemua.Visible = false;
                showhideUnitPerwakilan.Visible = false;
                showhidebutton.Visible = false;

                SetInitialRowDetil();
                for (int i = 1; i <= 2; i++)
                    AddNewRowDetil();

                SetInitialRow();
                for (int i = 1; i <= 2; i++)
                    AddNewRow();

            }
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("Jabatan", typeof(string)));
            dt.Columns.Add(new DataColumn("drNilai", typeof(string)));
            dt.Columns.Add(new DataColumn("keNilai", typeof(string)));
            dr = dt.NewRow();
            dr["Jabatan"] = string.Empty;
            dr["drNilai"] = string.Empty;
            dr["keNilai"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdApproval.DataSource = dt;
            grdApproval.DataBind();

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
                        DropDownList cboJabatan = (DropDownList)grdApproval.Rows[i].FindControl("cboJabatan");
                        TextBox txtdrNilai = (TextBox)grdApproval.Rows[i].FindControl("txtdrNilai");
                        TextBox txtkeNilai = (TextBox)grdApproval.Rows[i].FindControl("txtkeNilai");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Jabatan"] = cboJabatan.Text;
                        dtCurrentTable.Rows[i]["drNilai"] = txtdrNilai.Text;
                        dtCurrentTable.Rows[i]["keNilai"] = txtkeNilai.Text;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grdApproval.DataSource = dtCurrentTable;
                    grdApproval.DataBind();
                }
            }
            SetPreviousData();
        }

        private void SetInitialRowDetil()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("IDUser", typeof(string)));
            dt.Columns.Add(new DataColumn("noUser", typeof(string)));
            dt.Columns.Add(new DataColumn("drNilai", typeof(string)));
            dt.Columns.Add(new DataColumn("keNilai", typeof(string)));
            dr = dt.NewRow();
            dr["IDUser"] = string.Empty;
            dr["noUser"] = string.Empty;
            dr["drNilai"] = string.Empty;
            dr["keNilai"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable2"] = dt;
            grdApprovalUnit.DataSource = dt;
            grdApprovalUnit.DataBind();

            SetPreviousDataDetil();
        }
        private void SetPreviousDataDetil()
        {
            if (ViewState["CurrentTable2"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable2"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtIDUser = (TextBox)grdApprovalUnit.Rows[i].FindControl("txtIDUser");
                        HiddenField hdnnoUserD = (HiddenField)grdApprovalUnit.Rows[i].FindControl("hdnnoUserD");
                        TextBox txtdrNilai = (TextBox)grdApprovalUnit.Rows[i].FindControl("txtdrNilai");
                        TextBox txtkeNilai = (TextBox)grdApprovalUnit.Rows[i].FindControl("txtkeNilai");

                        txtIDUser.Text = dt.Rows[i]["IDUser"].ToString();
                        hdnnoUserD.Value = dt.Rows[i]["noUser"].ToString();
                        txtdrNilai.Text = dt.Rows[i]["drNilai"].ToString();
                        txtkeNilai.Text = dt.Rows[i]["keNilai"].ToString();

                    }
                }
            }
        }

        private void AddNewRowDetil()
        {
            if (ViewState["CurrentTable2"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable2"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtIDUser = (TextBox)grdApprovalUnit.Rows[i].FindControl("txtIDUser");
                        HiddenField hdnnoUserD = (HiddenField)grdApprovalUnit.Rows[i].FindControl("hdnnoUserD");
                        TextBox txtdrNilai = (TextBox)grdApprovalUnit.Rows[i].FindControl("txtdrNilai");
                        TextBox txtkeNilai = (TextBox)grdApprovalUnit.Rows[i].FindControl("txtkeNilai");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["IDUser"] = txtIDUser.Text;
                        dtCurrentTable.Rows[i]["noUser"] = hdnnoUserD.Value;
                        dtCurrentTable.Rows[i]["drNilai"] = txtdrNilai.Text;
                        dtCurrentTable.Rows[i]["keNilai"] = txtkeNilai.Text;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable2"] = dtCurrentTable;

                    grdApprovalUnit.DataSource = dtCurrentTable;
                    grdApprovalUnit.DataBind();
                }
            }
            SetPreviousDataDetil();
        }

        private void loadComboKategori()
        {
            cboCategory.DataSource = ObjDb.GetRows("select a.* from (select '' no,'---Pilih Kategori---' nama "+
                "union all "+ 
                "SELECT distinct noParameterApprove no, namaParameterApprove nama FROM MstParameterApprove) a");
            cboCategory.DataValueField = "no";
            cboCategory.DataTextField = "nama";
            cboCategory.DataBind();
        
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

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void btnAddRowDetil_Click(object sender, EventArgs e)
        {
            AddNewRowDetil();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (cboStatus.Text == "perUnit" || cboStatus.Text == "perPerwakilan")
            {
                if (cboUnitperwakilan.Text == "0")
                {
                    valid = false;
                    message += ObjSys.CreateMessage("Unit / Perwakilan harus dipilih.");
                }
                if (cboUntuk.Text == "-")
                {
                    valid = false;
                    message += ObjSys.CreateMessage("Peruntukan harus dipilih.");
                }
                if (cboCategory.Text == "0")
                {
                    valid = false;
                    message += ObjSys.CreateMessage("Kategori harus dipilih.");
                }
                int ada = 0;
                DataSet mySetAda = ObjDb.GetRows("Select * from MstApproveNilai where noParameterApprove = '" + cboCategory.Text + "' and noCabang = '" + cboUnitperwakilan.Text + "'");
                if (mySetAda.Tables[0].Rows.Count > 0)
                {
                    ada = 1;
                }

                if (ada == 1)
                {
                    valid = false;
                    message += ObjSys.CreateMessage("Data sudah terdaftar.");
                }
                if (valid == true)
                {
                    try
                    {
                        for (int i = 0; i < grdApprovalUnit.Rows.Count; i++)
                        {
                            TextBox txtIDUser = (TextBox)grdApprovalUnit.Rows[i].FindControl("txtIDUser");
                            HiddenField hdnnoUserD = (HiddenField)grdApprovalUnit.Rows[i].FindControl("hdnnoUserD");
                            TextBox txtdrNilai = (TextBox)grdApprovalUnit.Rows[i].FindControl("txtdrNilai");
                            TextBox txtkeNilai = (TextBox)grdApprovalUnit.Rows[i].FindControl("txtkeNilai");

                            string drNilai = "0", keNilai = "0";
                            if (txtdrNilai.Text != "")
                                drNilai = txtdrNilai.Text;
                            if (txtkeNilai.Text != "")
                                keNilai = txtkeNilai.Text;

                            string noAkses = "0";
                            DataSet mySetAkses = ObjDb.GetRows("select distinct top 1 noAkses from tAkses where noUser = '" + hdnnoUserD.Value + "'");
                            if (mySetAkses.Tables[0].Rows.Count > 0)
                            {
                                DataRow myRowAkses = mySetAkses.Tables[0].Rows[0];
                                noAkses = myRowAkses["noAkses"].ToString();
                            }

                            if (txtIDUser.Text != "")
                            {
                                string category = cboCategory.Text;
                                int lvlUser = (i + 1);
                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noParameterApprove", category);
                                ObjDb.Data.Add("noAkses", noAkses);
                                ObjDb.Data.Add("peruntukan", cboUntuk.Text);
                                ObjDb.Data.Add("noUser", hdnnoUserD.Value);
                                ObjDb.Data.Add("loginID", txtIDUser.Text);
                                ObjDb.Data.Add("drNilai", Convert.ToDecimal(drNilai).ToString());
                                ObjDb.Data.Add("keNilai", Convert.ToDecimal(keNilai).ToString());
                                ObjDb.Data.Add("lvlUser", lvlUser.ToString());
                                ObjDb.Data.Add("noCabang", cboUnitperwakilan.Text);
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Insert("MstApproveNilai", ObjDb.Data);

                            }

                        }
                        ClearData();
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil disimpan.");
                        loadComboKategori();
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

            else
            {
                if (cboCategory.Text == "0")
                {
                    valid = false;
                    message += ObjSys.CreateMessage("Kategori harus dipilih.");
                }
                if (cboUntuk.Text == "-")
                {
                    valid = false;
                    message += ObjSys.CreateMessage("Peruntukan harus dipilih.");
                }

                int cekisi = 0;
                for (int i = 0; i < grdApproval.Rows.Count; i++)
                {
                    DropDownList cboJabatan = (DropDownList)grdApproval.Rows[i].FindControl("cboJabatan");
                    if (cboJabatan.Text != "0")
                    {
                        cekisi++;
                    }
                    for (int j = i + 1; j < grdApproval.Rows.Count; j++)
                    {
                        DropDownList cboJabatanX = (DropDownList)grdApproval.Rows[j].FindControl("cboJabatan");
                        if (cboJabatan.Text == cboJabatanX.Text)
                        {
                            if (cboJabatan.Text != "0" && cboJabatanX.Text != "0")
                            {
                                message += ObjSys.CreateMessage("Jabatan baris ke-" + j + " tidak boleh sama.");
                                valid = false;
                            }
                        }
                    }
                }
                if (cekisi == 0)
                {
                    message += ObjSys.CreateMessage("Jabatan harus dipilih.");
                    valid = false;
                }

                int ada = 0;
                DataSet mySetAda = ObjDb.GetRows("Select * from MstApproveNilai where noParameterApprove = '" + cboCategory.Text + "' and peruntukan = '" + cboUntuk.Text + "'");
                if (mySetAda.Tables[0].Rows.Count > 0)
                {
                    ada = 1;
                }

                if (ada == 1)
                {
                    valid = false;
                    message += ObjSys.CreateMessage("Data sudah terdaftar.");
                }

                //int adaData = 0;
                if (cboUntuk.Text == "Unit")
                {
                    DataSet mySetD = ObjDb.GetRows("select * from mCabang where stsCabang in(2,3)");
                    if (mySetD.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow myRowD in mySetD.Tables[0].Rows)
                        {
                            string noCabang = myRowD["noCabang"].ToString();
                            string cabang = myRowD["namaCabang"].ToString();
                            string parent = "0";
                            DataSet mySet = ObjDb.GetRows("SELECT parent FROM mCabang where noCabang = '" + noCabang + "'");
                            DataRow myRow = mySet.Tables[0].Rows[0];
                            parent = myRow["parent"].ToString();

                            for (int i = 0; i < grdApproval.Rows.Count; i++)
                            {
                                DropDownList cboJabatan = (DropDownList)grdApproval.Rows[i].FindControl("cboJabatan");

                                string Sql = "";
                                Sql = "select x.*,y.userID,y.noUser,y.namauser,a.noAkses,a.hakAkses from ( " +
                                      "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 1 and stsCabang = 0 " +
                                      "union " +
                                      "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and nocabang = '" + parent + "' " +
                                      "union " +
                                      "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 0 and stsCabang in(2,3) and noCabang = '" + noCabang + "' " +
                                      ") x inner join mUser y on y.noCabang = x.id inner join tAkses z on z.noUser = y.noUser " +
                                      "inner join mAkses a  on a.noAkses = z.noAkses where y.userID <> 'admin' and " +
                                      "a.hakAkses = '" + cboJabatan.Text + "' " +
                                      "and(a.hakAkses like '%Kepala Yayasan%' or a.hakAkses like '%Kepala Perwakilan%' or a.hakAkses like '%Kepala Sekolah%')";

                                DataSet mySet2 = ObjDb.GetRows(Sql);
                                if (mySet2.Tables[0].Rows.Count == 0)
                                {
                                    valid = false;
                                    message += ObjSys.CreateMessage("Data user jabatan " + cboJabatan.Text + " Cabang " + cabang + " tidak ditemukan.");
                                }
                            }
                        }
                    }
                }

                if (cboUntuk.Text == "Perwakilan")
                {
                    //DataSet mySetD = ObjDb.GetRows("select * from mCabang where stsCabang = '3'");
                    DataSet mySetD = ObjDb.GetRows("select * from mCabang where stsCabang = '1'");
                    if (mySetD.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow myRowD in mySetD.Tables[0].Rows)
                        {
                            string noCabang = myRowD["noCabang"].ToString();
                            string cabang = myRowD["namaCabang"].ToString();
                            string parent = "0";
                            DataSet mySet = ObjDb.GetRows("SELECT parent FROM mCabang where noCabang = '" + noCabang + "'");
                            DataRow myRow = mySet.Tables[0].Rows[0];
                            parent = myRow["parent"].ToString();

                            for (int i = 0; i < grdApproval.Rows.Count; i++)
                            {
                                DropDownList cboJabatan = (DropDownList)grdApproval.Rows[i].FindControl("cboJabatan");

                                if (cboJabatan.SelectedValue != "0")
                                {
                                    string Sql = "";
                                    //Sql = "select x.*,y.userID,y.noUser,y.namauser,a.noAkses,a.hakAkses from ( " +
                                    //      "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 1 and stsCabang = 0 " +
                                    //      "union " +
                                    //      "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and nocabang = '" + parent + "' " +
                                    //      "union " +
                                    //      "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 0 and stsCabang = 3 and noCabang = '" + noCabang + "' " +
                                    //      ") x inner join mUser y on y.noCabang = x.id inner join tAkses z on z.noUser = y.noUser " +
                                    //      "inner join mAkses a  on a.noAkses = z.noAkses where y.userID <> 'admin' and " +
                                    //      "a.hakAkses = '" + cboJabatan.Text + "' " +
                                    //      "and(a.hakAkses like '%Kepala Yayasan%' or a.hakAkses like '%Kepala Perwakilan%' or a.hakAkses like '%Bendahara%')";

                                    Sql = "select x.*,y.userID,y.noUser,y.namauser,a.noAkses,a.hakAkses from ( " +
                                          "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 1 and stsCabang = 0 " +
                                          "union " +
                                          "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and nocabang = '" + parent + "' " +
                                          ") x inner join mUser y on y.noCabang = x.id inner join tAkses z on z.noUser = y.noUser " +
                                          "inner join mAkses a  on a.noAkses = z.noAkses where y.userID <> 'admin' and " +
                                          "a.hakAkses = '" + cboJabatan.Text + "' " +
                                          "and(a.hakAkses like '%Kepala Yayasan%' or a.hakAkses like '%Kepala Perwakilan%' or a.hakAkses like '%Bendahara%')";
                                    DataSet mySet2 = ObjDb.GetRows(Sql);
                                    if (mySet2.Tables[0].Rows.Count == 0)
                                    {
                                        valid = false;
                                        message += ObjSys.CreateMessage("Data user " + cboJabatan.Text + " Cabang " + cabang + "  tidak ditemukan.");
                                    }
                                }
                            }
                        }
                    }
                }

                if (cboUntuk.Text == "Yayasan")
                {
                    //DataSet mySetD = ObjDb.GetRows("select * from mCabang where stsCabang = '4'");
                    DataSet mySetD = ObjDb.GetRows("select * from mCabang where stsCabang = '0'");
                    if (mySetD.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow myRowD in mySetD.Tables[0].Rows)
                        {
                            string noCabang = myRowD["noCabang"].ToString();
                            string cabang = myRowD["namaCabang"].ToString();
                            string parent = "0";
                            DataSet mySet = ObjDb.GetRows("SELECT parent FROM mCabang where noCabang = '" + noCabang + "'");
                            DataRow myRow = mySet.Tables[0].Rows[0];
                            parent = myRow["parent"].ToString();

                            for (int i = 0; i < grdApproval.Rows.Count; i++)
                            {
                                DropDownList cboJabatan = (DropDownList)grdApproval.Rows[i].FindControl("cboJabatan");

                                if (cboJabatan.SelectedValue != "0")
                                {
                                    string Sql = "";
                                    //Sql = "select x.*,y.userID,y.noUser,y.namauser,a.noAkses,a.hakAkses from ( " +
                                    //      "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 1 and stsCabang = 0 " +
                                    //      "union " +
                                    //      "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 0 and stsCabang = 4 and noCabang = '" + noCabang + "' " +
                                    //      ") x inner join mUser y on y.noCabang = x.id inner join tAkses z on z.noUser = y.noUser " +
                                    //      "inner join mAkses a  on a.noAkses = z.noAkses where y.userID <> 'admin' and " +
                                    //      "a.hakAkses = '" + cboJabatan.Text + "' " +
                                    //      "and(a.hakAkses like '%Kepala Yayasan%' or a.hakAkses like '%Bendahara%')";

                                    Sql = "select x.*,y.userID,y.noUser,y.namauser,a.noAkses,a.hakAkses from ( " +
                                          "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 1 and stsCabang = 0 " +
                                          ") x inner join mUser y on y.noCabang = x.id inner join tAkses z on z.noUser = y.noUser " +
                                          "inner join mAkses a  on a.noAkses = z.noAkses where y.userID <> 'admin' and " +
                                          "a.hakAkses = '" + cboJabatan.Text + "' " +
                                          "and(a.hakAkses like '%Kepala Yayasan%' or a.hakAkses like '%Bendahara%')";

                                    DataSet mySet2 = ObjDb.GetRows(Sql);
                                    if (mySet2.Tables[0].Rows.Count == 0)
                                    {
                                        valid = false;
                                        message += ObjSys.CreateMessage("Data user " + cboJabatan.Text + " Cabang " + cabang + "  tidak ditemukan.");
                                    }
                                }
                            }
                        }
                    }
                }

                if (valid == true)
                {
                    try
                    {

                        if (cboUntuk.Text == "Unit")
                        {
                            DataSet mySetD = ObjDb.GetRows("select * from mCabang where stsCabang='2'");
                            if (mySetD.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow myRowD in mySetD.Tables[0].Rows)
                                {
                                    string noCabang = myRowD["noCabang"].ToString();
                                    string parent = "0";
                                    DataSet mySet = ObjDb.GetRows("SELECT parent FROM mCabang where noCabang = '" + noCabang + "'");
                                    DataRow myRow = mySet.Tables[0].Rows[0];
                                    parent = myRow["parent"].ToString();

                                    for (int i = 0; i < grdApproval.Rows.Count; i++)
                                    {
                                        DropDownList cboJabatan = (DropDownList)grdApproval.Rows[i].FindControl("cboJabatan");
                                        TextBox txtdrNilai = (TextBox)grdApproval.Rows[i].FindControl("txtdrNilai");
                                        TextBox txtkeNilai = (TextBox)grdApproval.Rows[i].FindControl("txtkeNilai");

                                        string drNilai = "0", keNilai = "0";
                                        if (txtdrNilai.Text != "")
                                            drNilai = txtdrNilai.Text;
                                        if (txtkeNilai.Text != "")
                                            keNilai = txtkeNilai.Text;

                                        if (cboJabatan.SelectedValue != "0")
                                        {
                                            string Sql = "";
                                            Sql = "select x.*,y.userID,y.noUser,y.namauser,a.noAkses,a.hakAkses from ( " +
                                                  "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 1 and stsCabang = 0 " +
                                                  "union " +
                                                  "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and nocabang = '" + parent + "' " +
                                                  "union " +
                                                  "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and noCabang = '" + noCabang + "' " +
                                                  ") x inner join mUser y on y.noCabang = x.id inner join tAkses z on z.noUser = y.noUser " +
                                                  "inner join mAkses a  on a.noAkses = z.noAkses where y.userID <> 'admin' and " +
                                                  "a.hakAkses = '" + cboJabatan.Text + "' " +
                                                  "and(a.hakAkses like '%Kepala Yayasan%' or a.hakAkses like '%Kepala Perwakilan%' or a.hakAkses like '%Kepala Sekolah%')";

                                            DataSet mySet2 = ObjDb.GetRows(Sql);
                                            if (mySet2.Tables[0].Rows.Count > 0)
                                            {
                                                DataRow myRow2 = mySet2.Tables[0].Rows[0];
                                                string noUser = myRow2["noUser"].ToString();
                                                string userid = myRow2["userID"].ToString();
                                                string noAkses = myRow2["noAkses"].ToString();

                                                int lvlUser = (i + 1);
                                                ObjDb.Data.Clear();
                                                ObjDb.Data.Add("noParameterApprove", cboCategory.Text);
                                                ObjDb.Data.Add("noAkses", noAkses);
                                                ObjDb.Data.Add("peruntukan", cboUntuk.Text);
                                                ObjDb.Data.Add("noUser", noUser);
                                                ObjDb.Data.Add("loginID", userid);
                                                ObjDb.Data.Add("drNilai", Convert.ToDecimal(drNilai).ToString());
                                                ObjDb.Data.Add("keNilai", Convert.ToDecimal(keNilai).ToString());
                                                ObjDb.Data.Add("lvlUser", lvlUser.ToString());
                                                ObjDb.Data.Add("noCabang", noCabang);
                                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                                ObjDb.Insert("MstApproveNilai", ObjDb.Data);

                                                ClearData();
                                                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                                                ShowMessage("success", "Data berhasil disimpan.");
                                                loadComboKategori();
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                                                ShowMessage("error", "Data User dengan jabatan tersebut tidak ditemukan.");
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        else if (cboUntuk.Text == "Perwakilan")
                        {
                            //DataSet mySetD = ObjDb.GetRows("select * from mCabang where stsCabang = '3'");
                            DataSet mySetD = ObjDb.GetRows("select * from mCabang where stsCabang = '1'");
                            if (mySetD.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow myRowD in mySetD.Tables[0].Rows)
                                {
                                    string noCabang = myRowD["noCabang"].ToString();
                                    string parent = "0";
                                    DataSet mySet = ObjDb.GetRows("SELECT parent FROM mCabang where noCabang = '" + noCabang + "'");
                                    DataRow myRow = mySet.Tables[0].Rows[0];
                                    parent = myRow["parent"].ToString();

                                    for (int i = 0; i < grdApproval.Rows.Count; i++)
                                    {
                                        DropDownList cboJabatan = (DropDownList)grdApproval.Rows[i].FindControl("cboJabatan");
                                        TextBox txtdrNilai = (TextBox)grdApproval.Rows[i].FindControl("txtdrNilai");
                                        TextBox txtkeNilai = (TextBox)grdApproval.Rows[i].FindControl("txtkeNilai");

                                        string drNilai = "0", keNilai = "0";
                                        if (txtdrNilai.Text != "")
                                            drNilai = txtdrNilai.Text;
                                        if (txtkeNilai.Text != "")
                                            keNilai = txtkeNilai.Text;

                                        if (cboJabatan.SelectedValue != "0")
                                        {
                                            string Sql = "";
                                            //Sql = "select x.*,y.userID,y.noUser,y.namauser,a.noAkses,a.hakAkses from ( " +
                                            //      "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 1 and stsCabang = 0 " +
                                            //      "union " +
                                            //      "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and nocabang = '" + parent + "' " +
                                            //      "union " +
                                            //      "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 0 and stsCabang = 3 and noCabang = '" + noCabang + "' " +
                                            //      ") x inner join mUser y on y.noCabang = x.id inner join tAkses z on z.noUser = y.noUser " +
                                            //      "inner join mAkses a  on a.noAkses = z.noAkses where y.userID <> 'admin' and " +
                                            //      "a.hakAkses = '" + cboJabatan.Text + "' " +
                                            //      "and(a.hakAkses like '%Kepala Yayasan%' or a.hakAkses like '%Kepala Perwakilan%' or a.hakAkses like '%Bendahara%')";

                                            Sql = "select x.*,y.userID,y.noUser,y.namauser,a.noAkses,a.hakAkses from ( " +
                                                 "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 1 and stsCabang = 0 " +
                                                 "union " +
                                                 "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and nocabang = '" + parent + "' " +
                                                 ") x inner join mUser y on y.noCabang = x.id inner join tAkses z on z.noUser = y.noUser " +
                                                 "inner join mAkses a  on a.noAkses = z.noAkses where y.userID <> 'admin' and " +
                                                 "a.hakAkses = '" + cboJabatan.Text + "' " +
                                                 "and(a.hakAkses like '%Kepala Yayasan%' or a.hakAkses like '%Kepala Perwakilan%' or a.hakAkses like '%Bendahara%')";
                                            DataSet mySet2 = ObjDb.GetRows(Sql);
                                            if (mySet2.Tables[0].Rows.Count > 0)
                                            {
                                                DataRow myRow2 = mySet2.Tables[0].Rows[0];
                                                string noUser = myRow2["noUser"].ToString();
                                                string userid = myRow2["userID"].ToString();
                                                string noAkses = myRow2["noAkses"].ToString();

                                                int lvlUser = (i + 1);
                                                ObjDb.Data.Clear();
                                                ObjDb.Data.Add("noParameterApprove", cboCategory.Text);
                                                ObjDb.Data.Add("noAkses", noAkses);
                                                ObjDb.Data.Add("peruntukan", cboUntuk.Text);
                                                ObjDb.Data.Add("noUser", noUser);
                                                ObjDb.Data.Add("loginID", userid);
                                                ObjDb.Data.Add("drNilai", Convert.ToDecimal(drNilai).ToString());
                                                ObjDb.Data.Add("keNilai", Convert.ToDecimal(keNilai).ToString());
                                                ObjDb.Data.Add("lvlUser", lvlUser.ToString());
                                                ObjDb.Data.Add("noCabang", noCabang);
                                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                                ObjDb.Insert("MstApproveNilai", ObjDb.Data);

                                                ClearData();
                                                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                                                ShowMessage("success", "Data berhasil disimpan.");
                                                loadComboKategori();
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                                                ShowMessage("error", "Data User dengan jabatan tersebut tidak ditemukan.");
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        else if (cboUntuk.Text == "Yayasan")
                        {
                            //DataSet mySetD = ObjDb.GetRows("select * from mCabang where stsCabang = '4'");
                            DataSet mySetD = ObjDb.GetRows("select * from mCabang where stsCabang = '0'");
                            if (mySetD.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow myRowD in mySetD.Tables[0].Rows)
                                {
                                    string noCabang = myRowD["noCabang"].ToString();
                                    string parent = "0";
                                    DataSet mySet = ObjDb.GetRows("SELECT parent FROM mCabang where noCabang = '" + noCabang + "'");
                                    DataRow myRow = mySet.Tables[0].Rows[0];
                                    parent = myRow["parent"].ToString();

                                    for (int i = 0; i < grdApproval.Rows.Count; i++)
                                    {
                                        DropDownList cboJabatan = (DropDownList)grdApproval.Rows[i].FindControl("cboJabatan");
                                        TextBox txtdrNilai = (TextBox)grdApproval.Rows[i].FindControl("txtdrNilai");
                                        TextBox txtkeNilai = (TextBox)grdApproval.Rows[i].FindControl("txtkeNilai");

                                        string drNilai = "0", keNilai = "0";
                                        if (txtdrNilai.Text != "")
                                            drNilai = txtdrNilai.Text;
                                        if (txtkeNilai.Text != "")
                                            keNilai = txtkeNilai.Text;

                                        if (cboJabatan.SelectedValue != "0")
                                        {
                                            string Sql = "";
                                            //Sql = "select x.*,y.userID,y.noUser,y.namauser,a.noAkses,a.hakAkses from ( " +
                                            //      "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 1 and stsCabang = 0 " +
                                            //      "union " +
                                            //      "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 0 and stsCabang = 4 and noCabang = '" + noCabang + "' " +
                                            //      ") x inner join mUser y on y.noCabang = x.id inner join tAkses z on z.noUser = y.noUser " +
                                            //      "inner join mAkses a  on a.noAkses = z.noAkses where y.userID <> 'admin' and " +
                                            //      "a.hakAkses = '" + cboJabatan.Text + "' " +
                                            //      "and(a.hakAkses like '%Kepala Yayasan%' or a.hakAkses like '%Bendahara%')";
                                            Sql = "select x.*,y.userID,y.noUser,y.namauser,a.noAkses,a.hakAkses from ( " +
                                                  "SELECT distinct nocabang as id, namaCabang as name, parent, noUrut, stsPusat, stsCabang FROM vCabang WHERE stsPusat = 1 and stsCabang = 0 " +
                                                  ") x inner join mUser y on y.noCabang = x.id inner join tAkses z on z.noUser = y.noUser " +
                                                  "inner join mAkses a  on a.noAkses = z.noAkses where y.userID <> 'admin' and " +
                                                  "a.hakAkses = '" + cboJabatan.Text + "' " +
                                                  "and(a.hakAkses like '%Kepala Yayasan%' or a.hakAkses like '%Bendahara%')";

                                            DataSet mySet2 = ObjDb.GetRows(Sql);
                                            if (mySet2.Tables[0].Rows.Count > 0)
                                            {
                                                DataRow myRow2 = mySet2.Tables[0].Rows[0];
                                                string noUser = myRow2["noUser"].ToString();
                                                string userid = myRow2["userID"].ToString();
                                                string noAkses = myRow2["noAkses"].ToString();

                                                int lvlUser = (i + 1);
                                                ObjDb.Data.Clear();
                                                ObjDb.Data.Add("noParameterApprove", cboCategory.Text);
                                                ObjDb.Data.Add("noAkses", noAkses);
                                                ObjDb.Data.Add("peruntukan", cboUntuk.Text);
                                                ObjDb.Data.Add("noUser", noUser);
                                                ObjDb.Data.Add("loginID", userid);
                                                ObjDb.Data.Add("drNilai", Convert.ToDecimal(drNilai).ToString());
                                                ObjDb.Data.Add("keNilai", Convert.ToDecimal(keNilai).ToString());
                                                ObjDb.Data.Add("lvlUser", lvlUser.ToString());
                                                ObjDb.Data.Add("noCabang", noCabang);
                                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                                ObjDb.Insert("MstApproveNilai", ObjDb.Data);

                                                ClearData();
                                                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                                                ShowMessage("success", "Data berhasil disimpan.");
                                                loadComboKategori();
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                                                ShowMessage("error", "Data User dengan jabatan tersebut tidak ditemukan.");
                                            }
                                        }

                                    }
                                }
                            }
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
            

        }
        protected void ClearData()
        {
            cboStatus.Text = "";
            showhide.Visible = false;
            showhideSemua.Visible = false;
            showhideUnitPerwakilan.Visible = false;
            showhidebutton.Visible = false;
            cboUntuk.Text = "-";
            cboCategory.Text = "0";

            SetInitialRowDetil();
            for (int i = 1; i <= 2; i++)
                AddNewRowDetil();

            SetInitialRow();
            for (int i = 1; i <= 2; i++)
                AddNewRow();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            ClearData();
        }

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStatus.Text == "perUnit" || cboStatus.Text == "perPerwakilan")
            {
                showhide.Visible = true;
                showhideSemua.Visible = false;
                showhideUnitPerwakilan.Visible = true;
                showhidebutton.Visible = true;
                btnAddRowDetil.Visible = true;
                btnAddRow.Visible = false;

            }
            else if (cboStatus.Text == "semuaUnitPerwakilan")
            {
                showhide.Visible = false;
                showhideSemua.Visible = true;
                showhideUnitPerwakilan.Visible = false;
                showhidebutton.Visible = true;
                btnAddRowDetil.Visible = false;
                btnAddRow.Visible = true;
            }

            LoadDataUnit();
        }

        protected void LoadDataUnit()
        {
            if (cboStatus.Text == "perUnit")
            {
                cboUnitperwakilan.DataSource = ObjDb.GetRows("select a.* from (select '' no,'---Pilih Unit---' nama " +
                    "union all " +
                    "select noCabang as no, namaCabang as nama from vCabang where stsCabang = 2) a");
                cboUnitperwakilan.DataValueField = "no";
                cboUnitperwakilan.DataTextField = "nama";
                cboUnitperwakilan.DataBind();
            }
            else if (cboStatus.Text == "perPerwakilan")
            {
                cboUnitperwakilan.DataSource = ObjDb.GetRows("select a.* from (select '' no,'---Pilih Perwakilan---' nama " +
                    "union all " +
                    "select noCabang as no, namaCabang as nama from vCabang where stsCabang in (3,4)) a");
                cboUnitperwakilan.DataValueField = "no";
                cboUnitperwakilan.DataTextField = "nama";
                cboUnitperwakilan.DataBind();
            }

            
        }

        protected void grdApprovalUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                loadDataUser();
                string value = (grdApprovalUnit.SelectedRow.FindControl("hdnParameter") as HiddenField).Value;
                hdnParameterDetil.Value = value;
                dlgUser.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void loadDataUser()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdUser.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUserApproval", ObjGlobal.Param);
            grdUser.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataUser();
            dlgUser.Show();
        }

        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdUser.PageIndex = e.NewPageIndex;
            loadDataUser();
            dlgUser.Show();
        }

        protected void grdUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = Convert.ToInt32(hdnParameterDetil.Value);
                string userID = (grdUser.SelectedRow.FindControl("lblUserID") as Label).Text;
                string nouser = (grdUser.SelectedRow.FindControl("hdnNoUser") as HiddenField).Value;

                HiddenField hdnnoUserD = (HiddenField)grdApprovalUnit.Rows[rowIndex - 1].FindControl("hdnnoUserD");
                TextBox txtIDUser = (TextBox)grdApprovalUnit.Rows[rowIndex - 1].FindControl("txtIDUser");

                int cek = 0;
                for (int i = 0; i < grdApprovalUnit.Rows.Count; i++)
                {
                    TextBox txtUserIDx = (TextBox)grdApprovalUnit.Rows[i].Cells[1].FindControl("txtIDUser");
                    if (userID == txtUserIDx.Text)
                        cek += 1;
                }
                hdnnoUserD.Value = nouser;
                txtIDUser.Text = userID;

                loadDataUser();
                dlgUser.Hide();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }


    }
}