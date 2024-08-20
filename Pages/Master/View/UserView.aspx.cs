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



namespace eFinance.Pages.Master.View
{
    public partial class UserView : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDataUpdate();
                LoadData();
            }
        }
        protected void LoadDataUpdate()
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdUserView.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUpdateUser", ObjGlobal.Param);
            grdUserView.DataBind();
        }
        protected void LoadData()
        {
            cboCabang.DataSource = ObjDb.GetRows("select a.* from (select '' id,'-------' name union all select noCabang id, '( ' + kdCabang + ' ) ' + namaCabang name from mCabang) a");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();

        }

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
            CloseMessage();
            ShowHideGridAndForm(true, false);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDataUpdate();
        }

        
        protected void grdUserView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUserView.PageIndex = e.NewPageIndex;
            LoadDataUpdate();
        }
        protected void grdUserView_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdUserView.SelectedRow.RowIndex;
                string noUser = grdUserView.DataKeys[rowIndex].Values[0].ToString();
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
                        imgItem.ImageUrl = "~/assets/images/empty.jpg";
                    
                    else
                        imgItem.ImageUrl = "~/assets/images/user/" + MyRow["fotouser"].ToString();
                    
                    ShowHideGridAndForm(false, true);
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

        protected void BindingModelInput()
        {
            DataTable myData = (DataTable)ViewState["DataAlert"];
            myData.DefaultView.RowFilter = "stsPilih=1";
            grdTaskAlert.DataSource = myData;
            grdTaskAlert.DataBind();
        }

        protected void LoadDataTaskAlert()
        {
            
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", "");
            ObjGlobal.Param.Add("noUser", hdnId.Value);
            ViewState["DataAlert"] = ObjGlobal.GetDataProcedureDataTable("SPLoadTaskAlertUpdate", ObjGlobal.Param);

        }

        protected void LoadDataGroupUser()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", "");
            ObjGlobal.Param.Add("noUser", hdnId.Value);
            ViewState["DataGroup"] = ObjGlobal.GetDataProcedureDataTable("SPLoadGroupUpdate", ObjGlobal.Param);

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