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
    public partial class TransMutasiKaryawanupdate : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                cboCabang.DataSource = ObjDb.GetRows("select 0 id, 'Pilih Cabang' name union SELECT distinct noCabang id, namaCabang name FROM mCabang where stsCabang in (2,3,4)");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();

                cbojabatan.DataSource = ObjDb.GetRows("select 0 id, '--Pilih Jabatan' name union SELECT distinct nojabatan id, jabatan name FROM mstJabatan");
                cbojabatan.DataValueField = "id";
                cbojabatan.DataTextField = "name";
                cbojabatan.DataBind();

                 
            }
        }
        protected void cboTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTransaction.Text == "Mutasi")
            {
                formpindah.Visible = true;
                formjabatan.Visible = false;
            }
            else if (cboTransaction.Text == "Promosi" || cboTransaction.Text == "Demosi" || cboTransaction.Text == "MutasiJabatan")
            {
                formpindah.Visible = false;
                formjabatan.Visible = true;
            }
            else
            {
                formpindah.Visible = false;
                formjabatan.Visible = false;
            }
        }
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("nouser", ObjSys.GetUserId);
            grdPinjaman.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataMutasiKaryawan", ObjGlobal.Param);
            grdPinjaman.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdPinjaman_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdPinjaman.PageIndex = e.NewPageIndex;
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
            if (dttgl.Text == "")
            {
                message = ObjSys.CreateMessage("Tanggal Peminjaman harus di isi.");
                valid = false;
            }
            if (txtnama.Text == "")
            {
                message += ObjSys.CreateMessage("Peminjam harus di pilih.");
                valid = false;
            }


            if (valid == true)

            {
                try
                {
                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noMutasiKaryawan", hdnId.Value);
                    ObjDb.Data.Add("tglMutasi", Convert.ToDateTime(dttgl.Text).ToString("yyyy-MM-dd"));


                    ObjDb.Data.Add("kategori", cboTransaction.Text);

                    ObjDb.Data.Add("noSK", txtNoSK.Text);

                    if (cboTransaction.Text == "Mutasi")
                    {
                        ObjDb.Data.Add("kenocabang", cboCabang.Text);
                    }
                    else if (cboTransaction.Text == "Promosi" || cboTransaction.Text == "Demosi" || cboTransaction.Text == "MutasiJabatan")
                    {
                        ObjDb.Data.Add("nojabatan", cbojabatan.Text);

                    }
                  

                    ObjDb.Data.Add("keterangan", txtKeterangan.Text);
                    ObjDb.Update("Tmutasikaryawan", ObjDb.Data, ObjDb.Where);

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

        protected void grdPinjaman_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectEdit")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noPinjaman = grdPinjaman.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noPinjaman;
 

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noMutasiKaryawan", hdnId.Value);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadMutasiKaryawanDetil", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtKode.Text = myRow["KdMutasi"].ToString();
                    txtnama.Text = myRow["nama"].ToString();
                    dttgl.Text = Convert.ToDateTime(myRow["tglMutasi"].ToString()).ToString("yyyy-MM-dd");
                    cboTransaction.Text = myRow["kategori"].ToString();
                    cbojabatan.Text = myRow["noJabatan"].ToString();
                    cboCabang.Text = myRow["kenocabang"].ToString();
                    txtNoSK.Text = myRow["noSK"].ToString();
                    txtKeterangan.Text = myRow["keterangan"].ToString();

                    if (myRow["kategori"].ToString() == "Mutasi")
                    {
                        formpindah.Visible = true;
                        formjabatan.Visible = false;
                    }
                    else if (myRow["kategori"].ToString() == "Promosi" || myRow["kategori"].ToString() == "Demosi")
                    {
                        formpindah.Visible = false;
                        formjabatan.Visible = true;
                    }
                    else
                    {
                        formpindah.Visible = false;
                        formjabatan.Visible = false;
                    }
                    this.ShowHideGridAndForm(false, true);
                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noPinjaman = grdPinjaman.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noPinjaman;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noMutasiKaryawan", hdnId.Value);
                    ObjDb.Delete("Tmutasikaryawan", ObjDb.Where);

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
    }
}