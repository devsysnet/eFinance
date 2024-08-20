using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Update
{
    public partial class updlssiswa : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                LoadDataCombo();
                this.ShowHideGridAndForm(true, false);
              
            }
        }



        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdBarang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataklssiswaupd", ObjGlobal.Param);
            grdBarang.DataBind();

            IndexPakai();
        }


        protected void IndexPakai()
        {

            for (int i = 0; i < grdBarang.Rows.Count; i++)
            {
                //string itemId = grdBarang.DataKeys[i].Value.ToString();
                //Button btnSelectDelete = (Button)grdBarang.Rows[i].FindControl("btnSelectDelete");
                //btnSelectDelete.Enabled = true;

                //DataSet mySet = ObjDb.GetRows("Select distinct nosiswa from TransPiutang Where noTransaksi = '" + itemId + "'");
                //if (mySet.Tables[0].Rows.Count > 0)
                //    btnSelectDelete.Enabled = false;

            }

        }

      
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdBarang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdBarang.PageIndex = e.NewPageIndex;
            LoadData();
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false);
            LoadData();
        }

        protected void LoadDataCombo()
        {
          
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
          
            if (kelas.Text == "")
            {
                message += ObjSys.CreateMessage("Kelas Harus Diisi");
                valid = false;
            }
           
            if (valid == true)
            {

                try
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("notKelas", hdnId.Value);
                    ObjGlobal.Param.Add("kelas", kelas.Text);
                    ObjGlobal.Param.Add("modiBy", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPUpdateklssiswa", ObjGlobal.Param);

                    LoadData();
                    this.ShowHideGridAndForm(true, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diubah.");
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

        protected void grdBarang_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectEdit")
                {


                   
                        int rowIndex = int.Parse(e.CommandArgument.ToString());
                        string notkelas = grdBarang.DataKeys[rowIndex].Value.ToString();
                        hdnId.Value = notkelas;

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("notkelas", hdnId.Value);
                        DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataskelasdetail", ObjGlobal.Param);
                        DataRow myRow = mySet.Tables[0].Rows[0];
                        namasiswa.Text = myRow["namasiswa"].ToString();
                        kelas.Text = myRow["kelas"].ToString();




                    //LoadDataCombo();
                    this.ShowHideGridAndForm(false, true);
                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nobarang = grdBarang.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nobarang;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("notkelas", hdnId.Value);
                    ObjDb.Delete("transkelas", ObjDb.Where);

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus");
                    this.ShowHideGridAndForm(true, false);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void cbopelunasan_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }
    }
}