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

namespace eFinance.Pages.Master.View
{
    public partial class MstKaryawanViewhrd : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();

        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                //ObjSys.SessionCheck("mstKaryawanUpdate.aspx");
                loadData();
                LoadDataCombo();
            }
        }
        private void SetInitialRowDocument(string noKaryawan = "")
        {
           
        }
        private void SetPreviousDataDocument()
        {
           
        }
        private void AddNewRowDocument()
        {
           
        }

        private void SetInitialRow(string noKaryawan = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("cbopendidikan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtnamasekolah", typeof(string)));
            dt.Columns.Add(new DataColumn("dtdrthn", typeof(string)));
            dt.Columns.Add(new DataColumn("dtspthn", typeof(string)));
            dt.Columns.Add(new DataColumn("txtNilai", typeof(string)));
            DataSet mySet = ObjDb.GetRows("select * from TPendindikanKaryawan WHERE noKaryawan = '" + noKaryawan + "'");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {

                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["cbopendidikan"] = myRow["noPendindikan"].ToString();
                dr["txtnamasekolah"] = myRow["namaSekolah"].ToString();
                dr["dtdrthn"] = Convert.ToDateTime(myRow["drthn"]).ToString("dd-MMM-yyyy");
                dr["dtspthn"] = Convert.ToDateTime(myRow["sdthn"]).ToString("dd-MMM-yyyy");
                dr["txtNilai"] = myRow["nilai"].ToString();

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["cbopendidikan"] = string.Empty;
                dr["txtnamasekolah"] = string.Empty;
                dr["dtdrthn"] = string.Empty;
                dr["dtspthn"] = string.Empty;
                dr["txtNilai"] = string.Empty;

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
                        DropDownList cbopendidikan = (DropDownList)grdDelivery.Rows[i].FindControl("cbopendidikan");
                        TextBox txtnmSekolah = (TextBox)grdDelivery.Rows[i].FindControl("txtnamasekolah");
                        TextBox dtdrthn = (TextBox)grdDelivery.Rows[i].FindControl("dtdrthn");
                        TextBox dtspthn = (TextBox)grdDelivery.Rows[i].FindControl("dtspthn");
                        TextBox txtNilai = (TextBox)grdDelivery.Rows[i].FindControl("txtNilai");

                        cbopendidikan.Text = dt.Rows[i]["cbopendidikan"].ToString();
                        txtnmSekolah.Text = dt.Rows[i]["txtnamasekolah"].ToString();
                        dtdrthn.Text = dt.Rows[i]["dtdrthn"].ToString();
                        dtspthn.Text = dt.Rows[i]["dtspthn"].ToString();
                        txtNilai.Text = dt.Rows[i]["txtNilai"].ToString();
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
                        DropDownList cbopendidikan = (DropDownList)grdDelivery.Rows[i].FindControl("cbopendidikan");
                        TextBox txtnmSekolah = (TextBox)grdDelivery.Rows[i].FindControl("txtnamasekolah");
                        TextBox dtdrthn = (TextBox)grdDelivery.Rows[i].FindControl("dtdrthn");
                        TextBox dtspthn = (TextBox)grdDelivery.Rows[i].FindControl("dtspthn");
                        TextBox txtNilai = (TextBox)grdDelivery.Rows[i].FindControl("txtNilai");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["cbopendidikan"] = cbopendidikan.Text;
                        dtCurrentTable.Rows[i]["txtnamasekolah"] = txtnmSekolah.Text;
                        dtCurrentTable.Rows[i]["dtdrthn"] = dtdrthn.Text;
                        dtCurrentTable.Rows[i]["dtspthn"] = dtspthn.Text;
                        dtCurrentTable.Rows[i]["txtNilai"] = txtNilai.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTableW"] = dtCurrentTable;
                    grdDelivery.DataSource = dtCurrentTable;
                    grdDelivery.DataBind();
                }
            }
            SetPreviousData();
        }

        private void SetInitialRowContact(string noKaryawan = "")
        {
           
        }

        private void SetPreviousDataContact()
        {
            
        }

        private void AddNewRowContact()
        {
           
        }
        protected void loadData()
        {
            grdCustomer.DataSource = ObjDb.GetRows("select a.* from MstKaryawan a inner join mCabang b on a.nocabang=b.noCabang inner join mCabang c on b.parent = c.noCabang where c.nocabang= '" + ObjSys.GetCabangId + "' and b.stsCabang = 2 union all select a.* from MstKaryawan a inner join mCabang b on a.nocabang = b.noCabang where b.noCabang = '" + ObjSys.GetCabangId + "'");
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
                            ObjDb.Where.Add("noKaryawan", itemRow);
                            ObjDb.Delete("mstKaryawan", ObjDb.Where);
                            //ObjDb.Delete("mstKaryawanCP", ObjDb.Where);
                            //ObjDb.Delete("mGudangCust", ObjDb.Where);
                            //ObjDb.Delete("mAlamatKirim", ObjDb.Where);
                        }
                    }
                    for (int i = 0; i < grdCustomer.Rows.Count; i++)
                    {
                        string itemId = grdCustomer.DataKeys[i].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdCustomer.Rows[i].FindControl("chkCheck");

                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noKaryawan", itemId);
                            ObjDb.Delete("mstKaryawan", ObjDb.Where);
                            //ObjDb.Delete("mstKaryawanCP", ObjDb.Where);
                            //ObjDb.Delete("mGudangCust", ObjDb.Where);
                            //ObjDb.Delete("mAlamatKirim", ObjDb.Where);
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


            cboAgama.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noAgama id, Agama name FROM mAgama where sts = '1') a");
            cboAgama.DataValueField = "id";
            cboAgama.DataTextField = "name";
            cboAgama.DataBind();

            cbostatusPTK.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noPTKP id, PTKP name FROM mstPTKP where sts = '1') a");
            cbostatusPTK.DataValueField = "id";
            cbostatusPTK.DataTextField = "name";
            cbostatusPTK.DataBind();

            cboGolPegawai.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noGolongan id, Golongan name FROM MstGolongan where sts = '1') a");
            cboGolPegawai.DataValueField = "id";
            cboGolPegawai.DataTextField = "name";
            cboGolPegawai.DataBind();

            cboDepartemen.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noDepartemen id, departemen name FROM Mstdepartemen where sts = '1') a");
            cboDepartemen.DataValueField = "id";
            cboDepartemen.DataTextField = "name";
            cboDepartemen.DataBind();

            cboBank.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noBank id, Bank name FROM mstbank where sts = '1') a");
            cboBank.DataValueField = "id";
            cboBank.DataTextField = "name";
            cboBank.DataBind();

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


            for (int i = 0; i < grdDelivery.Rows.Count; i++)
            {
                DropDownList cbopendidikan = (DropDownList)grdDelivery.Rows[i].FindControl("cbopendidikan");
                TextBox txtnmSekolah = (TextBox)grdDelivery.Rows[i].FindControl("txtnmSekolah");
                TextBox dtdrthn = (TextBox)grdDelivery.Rows[i].FindControl("dtdrthn");
                TextBox dtspthn = (TextBox)grdDelivery.Rows[i].FindControl("dtspthn");
                TextBox txtNilai = (TextBox)grdDelivery.Rows[i].FindControl("txtNilai");

                cbopendidikan.Text = "";
                txtnmSekolah.Text = "";
                dtdrthn.Text = "";
                dtspthn.Text = "";
                txtNilai.Text = "";
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
            //clearData();
            CloseMessage();
        }

        protected void grdCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdCustomer.SelectedRow.RowIndex;
                string noKaryawan = grdCustomer.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noKaryawan;

                DataSet MySet = ObjDb.GetRows("select * from  mstKaryawan where noKaryawan = '" + noKaryawan + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow myRow = MySet.Tables[0].Rows[0];

                    txtNama.Text = myRow["nama"].ToString();
                    txtNUPTK.Text = myRow["NUPTK"].ToString();
                    hdnnoKaryawan.Value = myRow["noKaryawan"].ToString();
                    txttempatlahir.Text = myRow["tempatlahir"].ToString();
                    dtlahir.Text = Convert.ToDateTime(myRow["tgllahir"]).ToString("dd-MMM-yyyy");
                    cboStatus.Text = myRow["jeniskelamin"].ToString();
                    cboAgama.Text = myRow["agama"].ToString();
                    cboKewarganegaraan.Text = myRow["kewarganegraan"].ToString();
                    //cboGolongandarah.Text = myRow["golongandarah"].ToString();
                    txttinggibadan.Text = Convert.ToDecimal(myRow["tinggibadan"]).ToString();
                    txtberatbadan.Text = Convert.ToDecimal(myRow["beratbadan"]).ToString();
                    cbostatusPTK.Text = myRow["tanggungan"].ToString();
                    cboPerkawinan.Text = myRow["perkawinan"].ToString();
                    dtMasuk.Text = Convert.ToDateTime(myRow["tglmasuk"]).ToString("dd-MMM-yyyy");
                    cboGolPegawai.Text = myRow["golongan"].ToString();
                    cboDepartemen.Text = myRow["dept"].ToString();
                    cboStskaryawan.Text = myRow["status"].ToString();
                    txtalamat.Text = myRow["alamat"].ToString();
                    txttelp.Text = myRow["telp"].ToString();
                    txthp.Text = myRow["hp"].ToString();
                    txtEmail.Text = myRow["email"].ToString();
                    cboBank.Text = myRow["bank"].ToString();
                    txtnorek.Text = myRow["norek"].ToString();
                    txnamaRekening.Text = myRow["namarek"].ToString();

                    SetInitialRow(noKaryawan);
                    SetInitialRowDocument(noKaryawan);
                    SetInitialRowContact(noKaryawan);



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