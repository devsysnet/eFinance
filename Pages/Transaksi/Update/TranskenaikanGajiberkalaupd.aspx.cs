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
    public partial class TranskenaikanGajiberkalaupd : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                LoadData();
                this.ShowHideGridAndForm(true, false);
                loadDataCombo();
            }
        }

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdBarang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDatakenaikangjberkala", ObjGlobal.Param);
            grdBarang.DataBind();
        }


        private void loadDataCombo()
        {
            cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
            cboYear.DataValueField = "id";
            cboYear.DataTextField = "name";
            cboYear.DataBind();
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



        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (txtnama.Text == "")
            {
                message += ObjSys.CreateMessage("Nama Karyawan Tidak Boleh Kosong.");
                valid = false;
            }


            if (valid == true)

            {
                //if (ObjDb.GetRows("SELECT * FROM TransPinjamankaryawan WHERE namaBarang = '" + txtnama.Text + "' and nobarang <> '" + hdnId.Value + "'").Tables[0].Rows.Count > 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //    ShowMessage("error", "Data Siswa sudah ada.");
                //}
                //else
                {

                    try
                    {
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noNaikkala", hdnId.Value);
                        ObjGlobal.Param.Add("jenis", cboStatus.Text);
                        ObjGlobal.Param.Add("bln", cboMonth.Text);
                        ObjGlobal.Param.Add("thn", cboYear.Text);
                        ObjGlobal.Param.Add("nilai", Convert.ToDecimal(txtPinjaman.Text).ToString());
                        ObjGlobal.Param.Add("modiBy", ObjSys.GetUserId);
                        ObjGlobal.GetDataProcedure("SPUpdatenaikberkala", ObjGlobal.Param);

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
                    string nobarang = grdBarang.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nobarang;

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noNaikkala", hdnId.Value);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadnaikgajiDetil", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtKode.Text = myRow["kdtran"].ToString();
                    txtnama.Text = myRow["nama"].ToString();
                    cboMonth.Text= myRow["bln"].ToString();
                    cboYear.Text = myRow["thn"].ToString();
                    dttgl.Text = myRow["tglpinjam"].ToString();
                    txtPinjaman.Text = myRow["nilaiPinjaman"].ToString();
                    cboStatus.Text = myRow["jenis"].ToString();
                    this.ShowHideGridAndForm(false, true);
                   
                }
                else
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nobarang = grdBarang.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nobarang;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noNaikkala", hdnId.Value);
                    ObjDb.Delete("TranskenaikanGajiBerkala", ObjDb.Where);

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Success Deleted");
                    this.ShowHideGridAndForm(true, false);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
    }
}