﻿using System;
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
namespace eFinance.Pages.Transaksi.View
{
    public partial class TransPelanggaranView : System.Web.UI.Page
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
            DataTable myData = ObjDb.GetRowsDataTable("select a.*,c.Pelanggaran,b.nama from TransPelanggaran a inner join MstKaryawan b on a.noKaryawan = b.nokaryawan inner  join mPelanggaran c on a.noPelanggaran = c.noPelanggaran where a.noKaryawan in ( select a.nokaryawan  from MstKaryawan a inner join TransPelanggaran b on a.nokaryawan=b.noKaryawan group by a.nokaryawan,nama,b.noPelanggaran) and tot in ( select MAX(tot) as total from MstKaryawan a inner join TransPelanggaran b on a.nokaryawan=b.noKaryawan group by a.nokaryawan,nama,b.noPelanggaran) and a.noPelanggaran in ( select b.noPelanggaran as total from MstKaryawan a inner join TransPelanggaran b on a.nokaryawan=b.noKaryawan group by a.nokaryawan,nama,b.noPelanggaran) " +
                "and (c.Pelanggaran like '%" + txtSearchAwal.Text.Trim() + "%' " +
                "or b.nama like '%" + txtSearchAwal.Text.Trim() + "%') ");
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
            LoadDataTotal(hdnJenisPelanggaran.Value, hdnNoUser.Value);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";
            int cekData = 0;

            if (dtDate.Text == "")
            {
                message = ObjSys.CreateMessage("Tanggal Pelanggaran harus di isi.");
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
                    ObjDb.Where.Add("noTransPelanggaran", hdnId.Value);
                    ObjDb.Data.Add("noPelanggaran", cboProject.Text);
                    ObjDb.Data.Add("tglPelanggaran", dtDate.Text);
                    ObjDb.Data.Add("noKaryawan", hdnNoUser.Value);
                    ObjDb.Data.Add("tot", txtSkor.Text);
                    ObjDb.Data.Add("keterangan", txtKeterangan.Text);
                    ObjDb.Data.Add("modiBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modidate", ObjSys.GetNow);
                    ObjDb.Update("transPelanggaran", ObjDb.Data, ObjDb.Where);

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
        private void LoadDataTotal(string noPelanggaran = "", string noKaryawan = "")
        {
            DataSet mySetH = ObjDb.GetRows("select MAX(tot) as total from  TransPelanggaran where  noPelanggaran = '" + noPelanggaran + "' and noKaryawan = '" + noKaryawan + "'");

            DataRow myRowH = mySetH.Tables[0].Rows[0];
            if (myRowH["total"].ToString() == "")
            {
                txtSkor.Text = "0";

            }
            else
            {
                txtSkor.Text = myRowH["total"].ToString();

            }

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

                    DataSet mySet = ObjDb.GetRows("select a.noTransPelanggaran,a.tglPelanggaran,a.keterangan,b.Pelanggaran,c.nama,a.noKaryawan,a.tot,a.noPelanggaran from transPelanggaran a inner join mPelanggaran b on a.noPelanggaran = b.noPelanggaran inner join MstKaryawan c on a.noKaryawan = c.nokaryawan " +
                        "where a.noTransPelanggaran = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    dtDate.Text = Convert.ToDateTime(myRow["tglPelanggaran"]).ToString("dd-MMM-yyyy");
                    txtNamaPeminta.Text = myRow["nama"].ToString();
                    hdnNoUser.Value = myRow["noKaryawan"].ToString();
                    txtKeterangan.Text = myRow["keterangan"].ToString();
                    txtSkor.Text = myRow["tot"].ToString();
                    cboProject.Text = myRow["Pelanggaran"].ToString();



                    this.ShowHideGridAndForm(false, true);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate()", "Calculate();", true);

                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noTransprestasi = grdPRUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noTransprestasi;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noTransPelanggaran", hdnId.Value);
                    ObjDb.Delete("transPelanggaran", ObjDb.Where);

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







    }
}