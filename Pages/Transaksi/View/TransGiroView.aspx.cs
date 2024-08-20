using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace eFinance.Pages.Transaksi.View
{
    public partial class TransGiroView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            cborekeningbank.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct norek id, ket name FROM mrekening where jenis=2 and stsaktif=1 and nocabang='" + ObjSys.GetCabangId + "' ) a");
            cborekeningbank.DataValueField = "id";
            cborekeningbank.DataTextField = "name";
            cborekeningbank.DataBind();

            cbojenissrt.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nojnsasset id, Jenis name FROM MstJenisasset where sts=1) a order by a.id");
            cbojenissrt.DataValueField = "id";
            cbojenissrt.DataTextField = "name";
            cbojenissrt.DataBind();

            cboRek.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct norek id, ket name FROM mrekening where jenis=40 and stsaktif=1 and nocabang='" + ObjSys.GetCabangId + "' ) a");
            cboRek.DataValueField = "id";
            cboRek.DataTextField = "name";
            cboRek.DataBind();
            if (!IsPostBack)
            {
                LoadData();


            }
        }

        protected void LoadData()
        {
            //grdAssetUpdate.DataSource = ObjDb.GetRows(" SELECT * FROM tGiro where tglGiro BETWEEN " + tglFrom.Text + " AND " + tglTo.Text + " and ( nomorGiro Like '%" + txtSearch.Text + "%' OR kdTransaksi Like '%"  +txtSearch.Text+ "%' )");
            //grdAssetUpdate.DataBind();
            grdAssetUpdate.DataSource = ObjDb.GetRows(" SELECT a.nodeposito,a.kdTransaksi,a.nomor,a.NamaBank,a.tglDeposito,a.tglJatuhTempo,b.Jenis, CASE WHEN a.Jenis = 1 THEN 'Saldo Awal' WHEN a.jenis = 2 THEN 'Pengeluaran Bank' ELSE 'Lain-Lain' END AS jenisTransaksi,nominal FROM Tdeposito a inner join MstJenisasset b on a.jenis = b.nojnsasset where a. nocabang='" + ObjSys.GetCabangId + "'and ( kdTransaksi Like '%" + txtSearch.Text + "%' OR nomor Like '%" + txtSearch.Text + "%' )");
            grdAssetUpdate.DataBind();
        }
        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbojenistransaksi.Text == "2")
                showhidekode.Visible = true;
            else
                showhidekode.Visible = false;
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

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        protected void clearData()
        {
            nomorGiro.Text = "";
            tglGiro.Text = "";
            tglJatuhTempo.Text = "";
            nominal.Text = "0";
            cbojenissrt.Text = "";
            deskripsi.Text = "";
            namabank.Text = "";
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            ShowHideGridAndForm(true, false);
        }

        protected void btnBrowseAsset_Click(object sender, ImageClickEventArgs e)
        {
            CloseMessage();
            LoadDataAsset();
            dlgAddData.Show();
        }

        protected void LoadDataAsset()
        {
            grdDataAsset.DataSource = ObjDb.GetRows("select noRek, ket, kdRek from mrekening where jenis = 2 and (ket like '%" + txtSearch.Text + "%' or kdRek like '%" + txtSearch.Text + "%')");

            grdDataAsset.DataBind();

        }
        protected void btnAsset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataAsset();
            dlgAddData.Show();
        }

        //protected void grdDataAsset_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int selectedRow = grdDataAsset.SelectedIndex;
        //    namarek.Text = grdDataAsset.Rows[selectedRow].Cells[2].Text;
        //    hdnnorek.Value = (grdDataAsset.SelectedRow.FindControl("hdnnorek") as HiddenField).Value;

        //}

        protected void grdDataAsset_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdDataAsset.PageIndex = e.NewPageIndex;
            LoadDataAsset();
            dlgAddData.Show();
        }
        protected void btnSearchAsset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }
        protected void grdAssetUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdAssetUpdate.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void grdAssetUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();


                if (e.CommandName == "SelectEdit")
                {


                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noGiro = grdAssetUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noGiro;

                    DataSet mySet = ObjDb.GetRows("select nodeposito,kdTransaksi,jenistransaksi,isnull(norekbank,0) as norekbank,Jenis,nomor,tglDeposito,tglJatuhTempo,nominal,NamaBank,norek,deskripsi " +
                        "from Tdeposito  where nodeposito = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    this.ShowHideGridAndForm(false, true);
                    if (myRow["jenistransaksi"].ToString() == "2")
                        showhidekode.Visible = true;
                    else
                        showhidekode.Visible = false;



                    cbojenistransaksi.Text = myRow["jenistransaksi"].ToString();
                    kdTransaksi.Text = myRow["kdTransaksi"].ToString();
                    nomorGiro.Text = myRow["nomor"].ToString();
                    cbojenissrt.Text = myRow["Jenis"].ToString();

                    tglGiro.Text = Convert.ToDateTime(myRow["tglDeposito"]).ToString("dd-MMM-yyyy");
                    tglJatuhTempo.Text = Convert.ToDateTime(myRow["tglJatuhTempo"]).ToString("dd-MMM-yyyy");
                    nominal.Text = ObjSys.IsFormatNumber(myRow["nominal"].ToString());
                    namabank.Text = myRow["NamaBank"].ToString();
                    deskripsi.Text = myRow["deskripsi"].ToString();
                    cboRek.Text = myRow["norek"].ToString();
                    cborekeningbank.Text = myRow["norekbank"].ToString();



                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noGiro = grdAssetUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noGiro;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("nodeposito", hdnId.Value);
                    ObjDb.Delete("Tdeposito", ObjDb.Where);

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