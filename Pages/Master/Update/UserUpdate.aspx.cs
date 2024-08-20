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

namespace eFinance.Pages.Master.Update
{
    public partial class UserUpdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("UserUpdate.aspx");
                LoadDataUpdate();
            }
        }

        //protek index yang dipakai transaksi
        protected void IndexPakai()
        {
            for (int i = 0; i < grdUserView.Rows.Count; i++)
            {
                string itemId = grdUserView.DataKeys[i].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdUserView.Rows[i].FindControl("chkCheck");

                
            }

        }
        protected void LoadDataUpdate()
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdUserView.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUpdateUser", ObjGlobal.Param);
            grdUserView.DataBind();

            IndexPakai();
        }
        protected void LoadData()
        {
            cboCabang.DataSource = ObjDb.GetRows("select a.* from (select '' id,'---Pilih Cabang---' name union all select noCabang id, '( ' + kdCabang + ' ) ' + namaCabang name from mCabang) a");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();

        }
        
        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdUserView.Rows)
                {
                    string index = grdUserView.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdUserView.Rows)
            {
                string index = grdUserView.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdUserView.Rows[gvrow.RowIndex].FindControl("chkCheck");
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

        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnMode.Value.ToLower() == "deleteall")
                {
                    /*DELETE ALL SELECTED*/
                    int cek = 0;
                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {

                            string fullPath = "";
                            DataSet mySet = ObjDb.GetRows("SELECT fotoUser FROM mUser WHERE noUser = '" + itemRow + "'");
                            DataRow myRow = mySet.Tables[0].Rows[0];
                            string logo = myRow["fotoUser"].ToString();

                            fullPath = Server.MapPath("~/assets/images/user/") + logo;
                            if (System.IO.File.Exists(fullPath))
                                System.IO.File.Delete(fullPath);

                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noUser", itemRow);
                            ObjDb.Delete("MstTaskAlert_D", ObjDb.Where);
                            ObjDb.Delete("tAkses", ObjDb.Where);
                            ObjDb.Delete("mUser", ObjDb.Where);

                        }
                    }
                    foreach (GridViewRow gvrow in grdUserView.Rows)
                    {
                        string itemId = grdUserView.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdUserView.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            cek++;
                            string fullPath = "";
                            DataSet mySet = ObjDb.GetRows("SELECT fotoUser FROM mUser WHERE noUser = '" + itemId + "'");
                            DataRow myRow = mySet.Tables[0].Rows[0];
                            string logo = myRow["fotoUser"].ToString();

                            fullPath = Server.MapPath("~/assets/images/user/") + logo;
                            if (System.IO.File.Exists(fullPath))
                                System.IO.File.Delete(fullPath);

                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noUser", itemId);
                            ObjDb.Delete("MstTaskAlert_D", ObjDb.Where);
                            ObjDb.Delete("tAkses", ObjDb.Where);
                            ObjDb.Delete("mUser", ObjDb.Where);
                            
                        }
                    }

                    if (cek > 0)
                    {
                        ShowMessage("success", "Data yang dipilih berhasil dihapus.");
                    }
                    else
                    {
                        ShowMessage("error", "Tidak ada data yang dipilih.");
                    }

                    /*END DELETE ALL SELECTED*/
                    LoadDataUpdate();
                    this.ShowHideGridAndForm(true, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        protected void ClearData()
        {
            //cbogudang.Text = "0";
            //cboKategori.Text = "1";
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
            CloseMessage();
            ShowHideGridAndForm(true, false);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (chkPassword.Checked == true)
            {
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    message += ObjSys.CreateMessage("Konfirmasi password harus sama dengan password pertama.");
                    valid = false;
                }
            }

            if (valid == true)
            {
                try
                {
                    string fileName = "";

                    DataSet mySetH = ObjDb.GetRows("Select * from mUser Where userID = '" + txtUserID.Text + "' and noCabang ='" + cboCabang.Text + "' and noUser <> '" + hdnId.Value +  "'");
                    if (mySetH.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "UserID sudah terdaftar di Cabang terpilih.");
                    }
                    else
                    {
                        ObjDb.Data.Clear();
                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("noUser", hdnId.Value);
                        ObjDb.Data.Add("noCabang", cboCabang.Text);
                        ObjDb.Data.Add("userID", txtUserID.Text);
                        ObjDb.Data.Add("namaUser", txtFullName.Text);
                        if (chkPassword.Checked == true)
                            ObjDb.Data.Add("passwordUser", ObjSys.Sha1(txtPassword.Text));
                        ObjDb.Data.Add("emailUser", txtEmail.Text);
                        ObjDb.Data.Add("telp", txtNoTelp.Text);
                        ObjDb.Data.Add("ketUser", txtKeterangan.Text);
                        if (chkIsActive.Checked == true)
                            ObjDb.Data.Add("stsUser", "1");
                        else
                            ObjDb.Data.Add("stsUser", "0");
                        ObjDb.Data.Add("levelUser", "0");

                        string fullPath = "";
                        if (cekGnt.Checked == true)
                        {
                            string logo = hdnFileImage.Value;

                            fullPath = Server.MapPath("~/assets/images/user/") + logo;
                            if (System.IO.File.Exists(fullPath))
                                System.IO.File.Delete(fullPath);

                            if (flUpload.HasFile)
                            {
                                ObjDb.Data.Add("fotoUser", flUpload.FileName);
                                fileName = flUpload.FileName;
                                flUpload.SaveAs(Server.MapPath("~/assets/images/user/" + flUpload.FileName));
                            }
                        }
                        else
                        {
                            if (flUpload.HasFile)
                            {
                                ObjDb.Data.Add("fotoUser", flUpload.FileName);
                                fileName = flUpload.FileName;
                                flUpload.SaveAs(Server.MapPath("~/assets/images/user/" + flUpload.FileName));
                            }
                        }


                        ObjDb.Update("mUser", ObjDb.Data, ObjDb.Where);

                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("noUser", hdnId.Value);
                        ObjDb.Delete("tAkses", ObjDb.Where);

                        DataTable myTableGroup = (DataTable)ViewState["DataGroup"];
                        foreach (DataRow myRowx in myTableGroup.Select("stsPilih=1"))
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noUser", hdnId.Value);
                            ObjDb.Data.Add("noAkses", myRowx["noAkses"].ToString());
                            ObjDb.Insert("tAkses", ObjDb.Data);
                        }

                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("noUser", hdnId.Value);
                        ObjDb.Delete("MstTaskAlert_D", ObjDb.Where);

                        DataTable myTable = (DataTable)ViewState["DataAlert"];
                        foreach (DataRow myRowx in myTable.Select("stsPilih=1"))
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noTaskAlert", myRowx["noTaskAlert"].ToString());
                            ObjDb.Data.Add("nouser", hdnId.Value);
                            ObjDb.Insert("MstTaskAlert_D", ObjDb.Data);
                        }

                        ClearData();
                        ShowHideGridAndForm(true, false);
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil diubah.");
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
        
        protected void btnCari_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataUpdate();
        }
        
        protected void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPassword.Checked == true)
            {
                txtPassword.Enabled = true;
                txtConfirmPassword.Enabled = true;
            }
            else
            {
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
            }
        }
       
        protected void grdUserView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdUserView.PageIndex = e.NewPageIndex;
            LoadDataUpdate();
        }
        
        protected void LoadDataKaryawan()
        {
            DataTable myData = ObjDb.GetRowsDataTable("select * from mstkaryawan where (idPeg like '%" + txtSearchKaryawan.Text.Trim() + "%' or nama like '%" + txtSearchKaryawan.Text.Trim() + "%') and noCabang = '" + cboCabang.Text + "'");
            grdDataKaryawan.DataSource = myData;
            grdDataKaryawan.DataBind();
        }

        protected void btnKaryawan_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataKaryawan();
            dlgKaryawan.Show();
        }

        protected void btnSearchTask_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataTaskAlert();
            dlgtaskAlert.Show();
        }

        protected void grsTaskAlert_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UpdateChecked();
            grsTaskAlert.PageIndex = e.NewPageIndex;
            LoadDataTaskAlert();
            dlgtaskAlert.Show();
        }

        protected void grdDataKaryawan_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRow = grdDataKaryawan.SelectedIndex;
            txtFullName.Text = grdDataKaryawan.Rows[selectedRow].Cells[2].Text;
        }

        protected void btnSearchKaryawan_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataKaryawan();
            dlgKaryawan.Show();
        }

        protected void btnAlert_Click(object sender, ImageClickEventArgs e)
        {
            dlgtaskAlert.Show();
        }

        protected void BindingModelInput()
        {
            DataTable myData = (DataTable)ViewState["DataAlert"];
            myData.DefaultView.RowFilter = "stsPilih=1";
            grdTaskAlert.DataSource = myData;
            grdTaskAlert.DataBind();
        }

        protected void UpdateChecked()
        {
            DataTable myData = (DataTable)ViewState["DataAlert"];
            for (int i = 0; i < grsTaskAlert.Rows.Count; i++)
            {
                CheckBox chkCheck = (CheckBox)grsTaskAlert.Rows[i].FindControl("chkSelectSub");
                string itemId = grsTaskAlert.DataKeys[i].Value.ToString();

                DataRow[] rowCek = myData.Select("noTaskAlert='" + itemId + "'");
                if (chkCheck.Checked == true)
                    rowCek[0]["stsPilih"] = "1";
                else
                    rowCek[0]["stsPilih"] = "0";
                myData.AcceptChanges();
                myData.Rows[i].SetModified();

            }

            ViewState["DataAlert"] = myData;
        }

        protected void LoadDataTaskAlert()
        {
            if (ViewState["DataAlert"] == null)
            {
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("Search", txtSearchTask.Text);
                ObjGlobal.Param.Add("noUser", hdnId.Value);
                ViewState["DataAlert"] = ObjGlobal.GetDataProcedureDataTable("SPLoadTaskAlertUpdate", ObjGlobal.Param);
            }
            DataTable myData = (DataTable)ViewState["DataAlert"];
            myData.DefaultView.RowFilter = "namaTask like '%" + txtSearchTask.Text + "%'";
            grsTaskAlert.DataSource = myData;
            grsTaskAlert.DataBind();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            UpdateChecked();
            BindingModelInput();
            dlgtaskAlert.Hide();
        }

        protected void cekGnt_CheckedChanged(object sender, EventArgs e)
        {
            if (cekGnt.Checked == true)
                flUpload.Enabled = true;
            else
                flUpload.Enabled = false;
        }

        protected void grsTaskAlert_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnStsPilih = (HiddenField)e.Row.FindControl("hdnStsPilih");
                CheckBox chkCheck = (CheckBox)e.Row.FindControl("chkSelectSub");

                if (hdnStsPilih.Value == "1")
                    chkCheck.Checked = true;
                else
                    chkCheck.Checked = false;
            }
        }

        protected void grdUserView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "Select")
                {

                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noUser = grdUserView.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noUser;

                    DataSet MySet = ObjDb.GetRows("select * from mUser where noUser='" + noUser + "'");
                    if (MySet.Tables[0].Rows.Count > 0)
                    {
                        DataRow MyRow = MySet.Tables[0].Rows[0];
                        cboCabang.Text = MyRow["noCabang"].ToString();
                        txtUserID.Text = MyRow["userID"].ToString();
                        txtFullName.Text = MyRow["namaUser"].ToString();
                        txtEmail.Text = MyRow["emailUser"].ToString();
                        txtNoTelp.Text = MyRow["telp"].ToString();
                        txtKeterangan.Text = MyRow["ketUser"].ToString();
                        
                        ViewState["DataGroup"] = null;
                        LoadDataGroupUser();
                        BindingModelInputGroup();

                        if (MyRow["stsUser"].ToString() == "1")
                            chkIsActive.Checked = true;
                        else
                            chkIsActive.Checked = false;

                        ViewState["DataAlert"] = null;
                        LoadDataTaskAlert();
                        BindingModelInput();

                        imgItem.ImageUrl = "~/assets/images/user/" + MyRow["fotouser"].ToString();
                        hdnFileImage.Value = MyRow["fotouser"].ToString();

                        if (hdnFileImage.Value == "")
                        {
                            imgItem.ImageUrl = "~/assets/images/empty.jpg";
                            flUpload.Enabled = true;
                            cekGnt.Visible = false;
                            lblec.Visible = false;
                        }
                        else
                        {
                            imgItem.ImageUrl = "~/assets/images/user/" + MyRow["fotouser"].ToString();
                            flUpload.Enabled = false;
                            cekGnt.Visible = true;
                            lblec.Visible = true;
                        }

                        ShowHideGridAndForm(false, true);
                        LoadData();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Data Tidak ada.");
                    }
                }
                else if (e.CommandName == "Reset")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nouser = grdUserView.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nouser;

                    DataSet MySet = ObjDb.GetRows("select * from mUser where noUser='" + hdnId.Value + "'");
                    DataRow MyRow = MySet.Tables[0].Rows[0];
                    string userID = MyRow["userID"].ToString();

                        ObjDb.Where.Clear();
                    ObjDb.Where.Add("noUser", hdnId.Value);
                    ObjDb.Data.Add("passwordUser", ObjSys.Sha1("123"));
                    ObjDb.Update("mUser", ObjDb.Data, ObjDb.Where);

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Password User Id "+ userID + " berhasil direset!");
                    this.ShowHideGridAndForm(true, false);
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void btnGroup_Click(object sender, ImageClickEventArgs e)
        {
            dlgGroup.Show();
        }

        protected void btnSearchGroup_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataGroupUser();
            dlgGroup.Show();
        }

        protected void grdGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UpdateCheckedGroup();
            grdGroup.PageIndex = e.NewPageIndex;
            LoadDataGroupUser();
            dlgGroup.Show();
        }

        protected void grdGroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnStsPilih = (HiddenField)e.Row.FindControl("hdnStsPilih");
                CheckBox chkCheck = (CheckBox)e.Row.FindControl("chkSelectSub");

                if (hdnStsPilih.Value == "1")
                    chkCheck.Checked = true;
                else
                    chkCheck.Checked = false;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            UpdateCheckedGroup();
            BindingModelInputGroup();
            dlgGroup.Hide();
        }

        protected void BindingModelInputGroup()
        {
            DataTable myData = (DataTable)ViewState["DataGroup"];
            myData.DefaultView.RowFilter = "stsPilih=1";
            grdGroupUser.DataSource = myData;
            grdGroupUser.DataBind();
        }

        protected void UpdateCheckedGroup()
        {
            DataTable myData = (DataTable)ViewState["DataGroup"];
            for (int i = 0; i < grdGroup.Rows.Count; i++)
            {
                CheckBox chkCheck = (CheckBox)grdGroup.Rows[i].FindControl("chkSelectSub");
                string itemId = grdGroup.DataKeys[i].Value.ToString();

                DataRow[] rowCek = myData.Select("noAkses='" + itemId + "'");
                if (chkCheck.Checked == true)
                    rowCek[0]["stsPilih"] = "1";
                else
                    rowCek[0]["stsPilih"] = "0";
                myData.AcceptChanges();
                myData.Rows[i].SetModified();

            }

            ViewState["DataGroup"] = myData;
        }

        protected void LoadDataGroupUser()
        {
            if (ViewState["DataGroup"] == null)
            {
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("Search", txtSearchGroup.Text);
                ObjGlobal.Param.Add("noUser", hdnId.Value);
                ViewState["DataGroup"] = ObjGlobal.GetDataProcedureDataTable("SPLoadGroupUpdate", ObjGlobal.Param);
            }
            DataTable myData = (DataTable)ViewState["DataGroup"];
            myData.DefaultView.RowFilter = "hakAkses like '%" + txtSearchGroup.Text + "%'";
            grdGroup.DataSource = myData;
            grdGroup.DataBind();
        }
    }
}