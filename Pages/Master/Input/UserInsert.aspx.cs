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
    public partial class UserInsert : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("UserInsert.aspx");
                LoadData();
                LoadDataTaskAlert();
                BindingModelInput();

                LoadDataGroupUser();
                BindingModelInputGroup();
            }
        }
        protected void LoadData()
        {
            cboCabang.DataSource = ObjDb.GetRows("select a.* from (select '' id,'--Select Branch--' name union all select noCabang id, '( ' + kdCabang + ' ) ' + namaCabang name from mCabang) a");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();

        }

        protected void ClearData()
        {
            ViewState.Clear();
            txtUserID.Text = "";
            txtFullName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            cboCabang.SelectedValue = "0";
            txtEmail.Text = "";
            txtKeterangan.Text = "";
            chkIsActive.Checked = false;
            clearGrid();
            LoadData();
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
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                message += ObjSys.CreateMessage("Konfirmasi password harus sama dengan password pertama.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                    string fileName = "";

                    DataSet mySetH = ObjDb.GetRows("Select * from mUser Where userID = '" + txtUserID.Text + "' and noCabang ='" + cboCabang.Text + "'");
                    if (mySetH.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "UserID sudah terdaftar di Cabang terpilih.");
                    }
                    else
                    {
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noCabang", cboCabang.Text);
                        ObjGlobal.Param.Add("noJabatan", "1");
                        ObjGlobal.Param.Add("userID", txtUserID.Text);
                        ObjGlobal.Param.Add("parent", "0");
                        ObjGlobal.Param.Add("namaUser", txtFullName.Text);
                        ObjGlobal.Param.Add("passwordUser", ObjSys.Sha1(txtPassword.Text));
                        ObjGlobal.Param.Add("emailUser", txtEmail.Text);
                        ObjGlobal.Param.Add("telp", txtNoTelp.Text);
                        ObjGlobal.Param.Add("ketUser", txtKeterangan.Text);
                        if (chkIsActive.Checked == true)
                            ObjGlobal.Param.Add("stsUser", "1");
                        else
                            ObjGlobal.Param.Add("stsUser", "0");
                            ObjGlobal.Param.Add("levelUser", "0");

                        //if (flUpload.HasFile)
                        //{
                        //    ObjGlobal.Param.Add("fotoUser", flUpload.FileName);
                        //    fileName = flUpload.FileName;
                        //    flUpload.SaveAs(Server.MapPath("~/assets/images/user/" + flUpload.FileName));
                        //}
                        ObjGlobal.Param.Add("fotoUser", flUpload.FileName);
                        ObjGlobal.ExecuteProcedure("insertmUser", ObjGlobal.Param);

                        if (flUpload.HasFile)
                        {
                            
                            fileName = flUpload.FileName;
                            flUpload.SaveAs(Server.MapPath("~/assets/images/user/" + flUpload.FileName));
                        }

                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("userID", txtUserID.Text);
                        DataSet mySet = ObjDb.Select("mUser", "*", ObjDb.Where);
                        DataRow myRow = mySet.Tables[0].Rows[0];

                        DataTable myTable = (DataTable)ViewState["DataAlert"];
                        foreach (DataRow myRowx in myTable.Select("stsPilih=1"))
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noTaskAlert", myRowx["noTaskAlert"].ToString());
                            ObjDb.Data.Add("nouser", myRow["noUser"].ToString());
                            ObjDb.Insert("MstTaskAlert_D", ObjDb.Data);
                        }

                        DataTable myTableGroup = (DataTable)ViewState["DataGroup"];
                        foreach (DataRow myRowx in myTableGroup.Select("stsPilih=1"))
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noUser", myRow["noUser"].ToString());
                            ObjDb.Data.Add("noAkses", myRowx["noAkses"].ToString());
                            ObjDb.Insert("tAkses", ObjDb.Data);
                        }
                    }

                    ClearData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
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


        protected void btnKaryawan_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataKaryawan();
            dlgKaryawan.Show();
        }

        protected void LoadDataKaryawan()
        {
            DataTable myData = ObjDb.GetRowsDataTable("select * from mstkaryawan where (idPeg like '%" + txtSearchKaryawan.Text.Trim() + "%' or nama like '%" + txtSearchKaryawan.Text.Trim() + "%') and noCabang = '" + cboCabang.Text + "'");
            grdDataKaryawan.DataSource = myData;
            grdDataKaryawan.DataBind();
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

        protected void grsTaskAlert_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UpdateChecked();
            grsTaskAlert.PageIndex = e.NewPageIndex;
            LoadDataTaskAlert();
            dlgtaskAlert.Show();
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
                ViewState["DataAlert"] = ObjGlobal.GetDataProcedureDataTable("SPLoadTaskAlert", ObjGlobal.Param);
            }
            DataTable myData = (DataTable)ViewState["DataAlert"];
            myData.DefaultView.RowFilter = "namaTask like '%" + txtSearchTask.Text + "%'";
            grsTaskAlert.DataSource = myData;
            grsTaskAlert.DataBind();
        }

        protected void btnSearchTask_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataTaskAlert();
            dlgtaskAlert.Show();
        }

        protected void BindingModelInput()
        {
            DataTable myData = (DataTable)ViewState["DataAlert"];
            myData.DefaultView.RowFilter = "stsPilih=1";
            grdTaskAlert.DataSource = myData;
            grdTaskAlert.DataBind();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            UpdateChecked();
            BindingModelInput();
            dlgtaskAlert.Hide();
        }

        protected void clearGrid()
        {
            ViewState["DataAlert"] = null;
            LoadDataTaskAlert();
            BindingModelInput();
            ViewState["DataGroup"] = null;
            LoadDataGroupUser();
            BindingModelInputGroup();
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

        protected void LoadDataGroupUser()
        {
            if (ViewState["DataGroup"] == null)
            {
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("Search", txtSearchGroup.Text);
                ViewState["DataGroup"] = ObjGlobal.GetDataProcedureDataTable("SPLoadGroup", ObjGlobal.Param);
            }
            DataTable myData = (DataTable)ViewState["DataGroup"];
            myData.DefaultView.RowFilter = "hakAkses like '%" + txtSearchGroup.Text + "%'";
            grdGroup.DataSource = myData;
            grdGroup.DataBind();
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
    }
}