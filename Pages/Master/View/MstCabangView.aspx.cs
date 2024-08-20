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
    public partial class MstCabangView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataMasterCabang", ObjGlobal.Param);
            grdCabang.DataBind();
        }

        protected void grdCabang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCabang.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            {
                CloseMessage();
                int rowIndex = grdCabang.SelectedRow.RowIndex;
                hdnID.Value = grdCabang.DataKeys[rowIndex].Values[0].ToString();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("Id", hdnID.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataMasterCabang_Detail", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                lblAlamat.Text = myRow["alamatCabang"].ToString();
                lblEmail.Text = myRow["emailCabang"].ToString();
                lblKodePos.Text = myRow["kodePosCabang"].ToString();
                lblKota.Text = myRow["kotaCabang"].ToString();
                lblNama.Text = myRow["namaCabang"].ToString();
                lblNoFax.Text = myRow["noFaxCabang"].ToString();
                lblNoTelp.Text = myRow["noTelpCabang"].ToString();
                lblOffice.Text = myRow["namaOfficerFin"].ToString();
                lblkategoriusaha.Text = myRow["kategoriusaha"].ToString();

                if (myRow["stsCabang"].ToString() == "0")
                    lblStatus.Text = "Pusat";
                else if (myRow["stsCabang"].ToString() == "1")
                    lblStatus.Text = "Perwakilan";
                else if (myRow["stsPusat"].ToString() == "2")
                    lblStatus.Text = "Unit";
                else if (myRow["stsCabang"].ToString() == "3")
                    lblStatus.Text = "Kantor Perwakilan";
                else if (myRow["stsCabang"].ToString() == "4")
                    lblStatus.Text = "Kantor Pusat";

                if (myRow["mhs"].ToString() == "1")
                    labelmhs.Text = "Ya";
                else
                    labelmhs.Text = "Tidak";


                if (myRow["allpelunasan"].ToString() == "1")
                    labelallpelunasan.Text = "Ya";
                else
                    labelallpelunasan.Text = "Tidak";

                if (myRow["cetakvoucher"].ToString() == "1")
                    labelcetakvoucher.Text = "Ya";
                else
                    labelcetakvoucher.Text = "Tidak";



                ShowHideGridForm(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            clearData();
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

        protected void ShowHideGridForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void clearData() 
        {
            LoadData();
            CloseMessage();
            ShowHideGridForm(true,false);
        }
    }
}