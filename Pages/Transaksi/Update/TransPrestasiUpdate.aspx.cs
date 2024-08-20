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

namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransPrestasiUpdate : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();

            }
        }

        protected void LoadData()
        {
            DataTable myData = ObjDb.GetRowsDataTable("select a.noTransprestasi,a.tglPrestasi,a.keterangan,b.Prestasi,c.nama from transprestasi a inner join mPrestasi b on a. noPrestasi = b.noPrestasi inner join MstKaryawan c on a.noKaryawan = c.nokaryawan " +
                "where (b.Prestasi like '%" + txtSearchAwal.Text.Trim() + "%' " +
                "or c.nama like '%" + txtSearchAwal.Text.Trim() + "%') ");
            grdPRUpdate.DataSource = myData;
            grdPRUpdate.DataBind();

        }


        protected void LoadDataPeminta()
        {
            DataTable myData = ObjDb.GetRowsDataTable("select nokaryawan,idPeg,nama from mstkaryawan where (idPeg like '%" + txtSearchMinta.Text.Trim() + "%' or nama like '%" + txtSearchMinta.Text.Trim() + "%')");
            grdDataPeminta.DataSource = myData;
            grdDataPeminta.DataBind();
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
        protected void ClearData()
        {
            dtDate.Text = "";
            hdnNoUser.Value = "";
            txtNamaPeminta.Text = "";
            txtKeterangan.Text = "";


        }

        protected void btnBrowsePeminta_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataPeminta();
            dlgAddData.PopupControlID = "panelAddDataPeminta";
            dlgAddData.Show();
        }

        protected void grdDataPeminta_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRow = grdDataPeminta.SelectedIndex;
            txtNamaPeminta.Text = grdDataPeminta.Rows[selectedRow].Cells[2].Text;
            hdnNoUser.Value = (grdDataPeminta.SelectedRow.FindControl("hdnNoUserD") as HiddenField).Value;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";
            int cekData = 0;

            if (dtDate.Text == "")
            {
                message = ObjSys.CreateMessage("Tanggal Prestasi harus di isi.");
                valid = false;
            }
           
            else if (txtNamaPeminta.Text == "")
            {
                message = ObjSys.CreateMessage("Karyawan harus di pilih.");
                valid = false;
            }



          
            try
            {

                if (valid == true)
                {

                    ObjDb.Where.Clear();
                    ObjDb.Data.Clear();
                    ObjDb.Where.Add("noTransprestasi", hdnId.Value);
                    ObjDb.Data.Add("noPrestasi", cboProject.Text);
                    ObjDb.Data.Add("tglPrestasi", dtDate.Text);
                    ObjDb.Data.Add("noKaryawan", hdnNoUser.Value);
                    ObjDb.Data.Add("keterangan", txtKeterangan.Text);
                    ObjDb.Data.Add("modiBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modidate", ObjSys.GetNow);
                    ObjDb.Update("transprestasi", ObjDb.Data, ObjDb.Where);

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diubah");
                    this.ShowHideGridAndForm(true, false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }






        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false);
        }


        
        protected void btnCari_Click(object sender, EventArgs e)
        {

            dlgAddData.PopupControlID = "panelAddDataBiaya";
            dlgAddData.Show();
        }

        protected void btnMinta_Click(object sender, EventArgs e)
        {
            LoadDataPeminta();
            dlgAddData.PopupControlID = "panelAddDataPeminta";
            dlgAddData.Show();
        }

        protected void btnSearchAwal_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void grdPRUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdPRUpdate.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void grdPRUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectEdit")
                {

                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noPR = grdPRUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noPR;

                    DataSet mySet = ObjDb.GetRows("select a.noTransprestasi,a.tglPrestasi,a.keterangan,b.Prestasi,c.nama,a.noPrestasi,a.noKaryawan from transprestasi a inner join mPrestasi b on a. noPrestasi = b.noPrestasi inner join MstKaryawan c on a.noKaryawan = c.nokaryawan " +
                        "where a.noTransprestasi = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    dtDate.Text = Convert.ToDateTime(myRow["tglPrestasi"]).ToString("dd-MMM-yyyy");
                    txtNamaPeminta.Text = myRow["nama"].ToString();
                    hdnNoUser.Value = myRow["noKaryawan"].ToString();
                    txtKeterangan.Text = myRow["keterangan"].ToString();
        
                        cboProject.Text = myRow["noPrestasi"].ToString();
                        loadDataComboProject();
                   
                    

                    this.ShowHideGridAndForm(false, true);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate()", "Calculate();", true);

                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noTransprestasi = grdPRUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noTransprestasi;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noTransprestasi", hdnId.Value);
                    ObjDb.Delete("transprestasi", ObjDb.Where);

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

  
   

        
        protected void showHideProject(bool DivProject)
        {
            showProject.Visible = DivProject;
        }



        protected void loadDataComboProject()
        {
            cboProject.DataSource = ObjDb.GetRows("select '0' as no, '--Pilih Jenis Prestasi--' name union all select noPrestasi as no, Prestasi as name from mPrestasi where sts = '1'");
            cboProject.DataValueField = "no";
            cboProject.DataTextField = "name";
            cboProject.DataBind();
        }

       

    }
}