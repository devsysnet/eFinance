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
    public partial class TransPinjamanupdate : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
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
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdPinjaman.DataSource = ObjGlobal.GetDataProcedure("SPLoadDatapinjaman", ObjGlobal.Param);
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
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noPinjamKaryawan", hdnId.Value);
                    ObjGlobal.Param.Add("nilaipinjam", Convert.ToDecimal(txtPinjaman.Text).ToString());
                    ObjGlobal.Param.Add("angsuran", Convert.ToDecimal(txtangsuran.Text).ToString());
                    ObjGlobal.Param.Add("uraian", txtUraian.Text);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPUpdatepinjaman", ObjGlobal.Param);

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
                    ObjGlobal.Param.Add("noPinjamKaryawan", hdnId.Value);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadpinjamanDetil", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtKode.Text = myRow["KodePinjam"].ToString();
                    txtnama.Text = myRow["nama"].ToString();
                    dttgl.Text = myRow["tglpinjam"].ToString();
                    txtPinjaman.Text = ObjSys.IsFormatNumber(myRow["nilaiPinjaman"].ToString());
                    txtangsuran.Text = ObjSys.IsFormatNumber(myRow["angsuran"].ToString());
                    txtUraian.Text = myRow["Uraian"].ToString();

                    this.ShowHideGridAndForm(false, true);
                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noPinjaman = grdPinjaman.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noPinjaman;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noPinjamKaryawan", hdnId.Value);
                    ObjDb.Delete("TransPinjamankaryawan", ObjDb.Where);

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