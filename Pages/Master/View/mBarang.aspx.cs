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
    public partial class mBarang : System.Web.UI.Page
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
            grdBarang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBarangView", ObjGlobal.Param);
            grdBarang.DataBind();

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

        protected void LoadDataCombo(string cbojnsBarang)
        {
            cboKategori.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Kategori Barang---' name union all SELECT distinct nokategori id, kategori name FROM mkategori where jns = '" + cbojnsBarang + "' ) a");
            cboKategori.DataValueField = "id";
            cboKategori.DataTextField = "name";
            cboKategori.DataBind();

        }

        protected void cbojnsBarang_SelectedIndexChanged(object sender, EventArgs e)
        {

            cboKategori.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Kategori Barang---' name union all SELECT distinct nokategori id, kategori name FROM mkategori where jns = '" + cbojnsBarang + "' ) a");
            cboKategori.DataValueField = "id";
            cboKategori.DataTextField = "name";
            cboKategori.DataBind();
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
                    ObjGlobal.Param.Add("noBarang", hdnId.Value);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataBarangDetil", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtKode.Text = myRow["kodeBarang"].ToString();
                    txtnamaBarang.Text = myRow["namaBarang"].ToString();
                    satuan1.Text = myRow["satuan"].ToString();
                    konfersi1.Text = myRow["konfersi"].ToString();
                    satuan2.Text = myRow["satuanbesar"].ToString();
                    konfersi2.Text = myRow["konfersibesar"].ToString();
                    satuan3.Text = myRow["satuanbesar1"].ToString();
                    konfersi3.Text = myRow["konfersi1"].ToString();
                    satuan4.Text = myRow["satuanbesar2"].ToString();
                    konfersi4.Text = myRow["konfersi2"].ToString();
                    satuan5.Text = myRow["sataunbesar3"].ToString();
                    konfersi5.Text = myRow["konfersi3"].ToString();
                    harga.Text = myRow["harga"].ToString();
                    cbojnsBarang.Text = myRow["jns"].ToString();
                    LoadDataCombo(cbojnsBarang.Text);
                    cboKategori.Text = myRow["kategoriBarang"].ToString();
                    keterangan.Text = myRow["Uraian"].ToString();
                   
                    if (cbojnsBarang.Text == "1")
                        showhidekode.Visible = true;
                    else
                        showhidekode.Visible = false;

                    txtKodeAsset.Text = myRow["kodeAset"].ToString();

                    this.ShowHideGridAndForm(false, true);
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