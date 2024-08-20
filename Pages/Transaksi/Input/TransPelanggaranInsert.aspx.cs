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
namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransPelanggaranInsert : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataComboProject();
                ClearData();
            }
        }





        private void LoadDataTotal(string noPelanggaran = "", string noKaryawan = "")
        {
            DataSet mySetH = ObjDb.GetRows("select MAX(tot) as total from  TransPelanggaran where  noPelanggaran = '" + noPelanggaran + "' and noKaryawan = '" + noKaryawan + "'");

            DataRow myRowH = mySetH.Tables[0].Rows[0];
            if(myRowH["total"].ToString() == "")
            {
                txtTotal.Text = "0";

            }
            else
            {
                txtTotal.Text = myRowH["total"].ToString();

            }

        }

        protected void loadDataComboProject()
        {

            cboProject.DataSource = ObjDb.GetRows("select '0' as no, '--Pilih Jenis Pelanggaran--' name union all select noPelanggaran as no, Pelanggaran as name from mPelanggaran where sts = '1'");
            cboProject.DataTextField = "name";
            cboProject.DataValueField = "no";
            cboProject.DataBind();
        }
     
        protected void cboProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnJenisPelanggaran.Value = cboProject.SelectedValue;
            LoadDataTotal(hdnJenisPelanggaran.Value, hdnNoUser.Value);
        }
        protected void LoadDataPeminta()
        {
            DataTable myData = ObjDb.GetRowsDataTable("select * from MstKaryawan where (idPeg like '%" + txtSearchMinta.Text.Trim() + "%' or nama like '%" + txtSearchMinta.Text.Trim() + "%')");
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
            cboProject.Text = "0";
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
                message = ObjSys.CreateMessage("Tanggal  harus di isi.");
                valid = false;
            }
            else if (cboProject.Text == "0")
            {
                message = ObjSys.CreateMessage("Jenis Pelanggaran harus di pilih.");
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
                    DataSet mySetH = ObjDb.GetRows("Select * from TransPelanggaran where  noPelanggaran = '" + cboProject.Text + "' and noKaryawan = '"+ hdnNoUser.Value+ "'");

                    if (mySetH.Tables[0].Rows.Count == 0)
                    {
                   
                            string total = "1";
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noPelanggaran", cboProject.Text);
                            ObjDb.Data.Add("tglPelanggaran", dtDate.Text);
                            ObjDb.Data.Add("noKaryawan", hdnNoUser.Value);
                            ObjDb.Data.Add("tot", total.ToString());
                            ObjDb.Data.Add("Keterangan", txtKeterangan.Text);
                            ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                            ObjDb.Insert("TransPelanggaran", ObjDb.Data);
                       
                    }
                    else
                    {
                        DataSet mySet = ObjDb.GetRows("Select * from TransPelanggaran where  noPelanggaran = '" + cboProject.Text + "' and noKaryawan = '" + hdnNoUser.Value + "'");
                        DataRow myRows = mySet.Tables[0].Rows[0];
                        string noPelanggaran = myRows["noPelanggaran"].ToString();
                        string noKaryawan = myRows["noKaryawan"].ToString();
                        
                            string total = "1";
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noPelanggaran", cboProject.Text);
                            ObjDb.Data.Add("tglPelanggaran", dtDate.Text);
                            ObjDb.Data.Add("noKaryawan", hdnNoUser.Value);
                            ObjDb.Data.Add("tot", total.ToString());
                            ObjDb.Data.Add("Keterangan", txtKeterangan.Text);
                            ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                            ObjDb.Insert("TransPelanggaran", ObjDb.Data);
                       
                    }
                        
                    
                   


                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan");
                    ClearData();

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
            ClearData();
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


        protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (ObjSys.GetKategori_Usaha == "Sekolah")
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[10].Visible = true;
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[10].Visible = true;
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[10].Visible = false;
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[10].Visible = false;
                }
            }
        }
    }
}