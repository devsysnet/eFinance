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
    public partial class TransPencarianUpdate : System.Web.UI.Page
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
           
            grdAssetUpdate.DataSource = ObjDb.GetRows(" SELECT a.nodeposito,a.kdTransaksi,a.nomor,a.nominal,a.NamaBank,a.tglDeposito,a.tglJatuhTempo,b.Jenis, CASE WHEN a.Jenis = 1 THEN 'Saldo Awal' WHEN a.jenis = 2 THEN 'Pengeluaran Bank' ELSE 'Lain-Lain' END AS jenisTransaksi FROM Tdeposito a inner join MstJenisasset b on a.jenis = b.nojnsasset where  ( kdTransaksi Like '%" + txtSearch.Text + "%' OR nomor Like '%" + txtSearch.Text + "%' ) and a.tglJatuhTempo<='"+  ObjSys.GetDate + "' and a.sts='0' and a.nocabang = '" + ObjSys.GetCabangId + "'");
            grdAssetUpdate.DataBind();

            cbojenissrt.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nojnsasset id, Jenis name FROM MstJenisasset where sts=1) a order by a.id");
            cbojenissrt.DataValueField = "id";
            cbojenissrt.DataTextField = "name";
            cbojenissrt.DataBind();

            cboAccount.DataSource = ObjDb.GetRows("select a.* from (select '0' id,' ---Pilih Akun--- ' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='2' and nocabang = '" + ObjSys.GetCabangId + "') a order by a.id");
            cboAccount.DataValueField = "id";
            cboAccount.DataTextField = "name";
            cboAccount.DataBind();

        }

        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbojenistransaksi.Text == "2")
            //    showhidekode.Visible = true;
            //else
            //    showhidekode.Visible = false;
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (nomorGiro.Text == "")
            {
                message += ObjSys.CreateMessage("Nomor Giro harus diisi.");
                valid = false;
            }
            if (tglGiro.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal Giro harus diisi.");
                valid = false;
            }
            if (tglJatuhTempo.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal Jatuh Tempo harus diisi.");
                valid = false;
            }
            if (nominal.Text == "")
            {
                message += ObjSys.CreateMessage("Nominal harus diisi.");
                valid = false;
            }


            if (valid == true)
            {
                try
                {
                    string Kode = "";
                    Kode = ObjSys.GetCodeAutoNumberNewCustom("1", Convert.ToDateTime(tglcair.Text).ToString("yyyy-MM-dd"), Convert.ToInt32(cboAccount.Text));

                    ObjGlobal.Param.Clear();
                   
                    ObjGlobal.Param.Add("nomorKode", Kode);
                    ObjGlobal.Param.Add("nogiro", hdnId.Value);
                    ObjGlobal.Param.Add("tglcair", tglcair.Text);
                    ObjGlobal.Param.Add("tglJatuhTempo", tglJatuhTempo.Text);
                    ObjGlobal.Param.Add("norekbank", cboAccount.Text);
                    ObjGlobal.Param.Add("nominal", Convert.ToDecimal(nominal.Text).ToString());
                    ObjGlobal.Param.Add("nomor", nomorGiro.Text);
                    ObjGlobal.Param.Add("Namabank", namabank.Text);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPInsertPencairan", ObjGlobal.Param);

                    ObjSys.UpdateAutoNumberCodeNew("1", Convert.ToDateTime(tglcair.Text).ToString("yyyy-MM-dd"));

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                    LoadData();
                    ShowHideGridAndForm(true, false,false);

                }
                catch (Exception ex)
                {
                    if (valid == false)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", message);
                    }
                    else
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
        protected void btnSimpanPerpanjang_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

  
            if (tanggalJatuhTempo.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal Jatuh Tempo harus diisi.");
                valid = false;
            }
            if (nominal1.Text == "")
            {
                message += ObjSys.CreateMessage("Nominal harus diisi.");
                valid = false;
            }


            if (valid == true)
            {
                try
                {
                    string Kode = "";
                    
                    ObjGlobal.Param.Clear();

                    ObjGlobal.Param.Add("nomorKode", Kode);
                    ObjGlobal.Param.Add("nogiro", hdnId.Value);
                    ObjGlobal.Param.Add("tglJatuhTempo", tanggalJatuhTempo.Text);
                    ObjGlobal.Param.Add("uraian", uraian1.Text);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPInsertPerpanjang", ObjGlobal.Param);

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                    LoadData();
                    ShowHideGridAndForm(true, false, false);

                }
                catch (Exception ex)
                {
                    if (valid == false)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", message);
                    }
                    else
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

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm,bool DivForm1)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabform1.Visible = DivForm1;

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
            ShowHideGridAndForm(true, false,false);
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

                    DataSet mySet = ObjDb.GetRows("select nodeposito,kdTransaksi,jenistransaksi,norekbank,Jenis,nomor,tglDeposito,tglJatuhTempo,nominal,NamaBank,norek,deskripsi " +
                        "from Tdeposito  where nodeposito = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    this.ShowHideGridAndForm(false, true,false);
                    
                    kdTransaksi.Text = myRow["kdTransaksi"].ToString();
                    nomorGiro.Text = myRow["nomor"].ToString();
                    cbojenissrt.Text = myRow["Jenis"].ToString();

                    tglGiro.Text = Convert.ToDateTime(myRow["tglDeposito"]).ToString("dd-MMM-yyyy");
                    tglJatuhTempo.Text = Convert.ToDateTime(myRow["tglJatuhTempo"]).ToString("dd-MMM-yyyy");
                    nominal.Text = ObjSys.IsFormatNumber(myRow["nominal"].ToString());
                    namabank.Text = myRow["NamaBank"].ToString();
                    deskripsi.Text = myRow["deskripsi"].ToString();
                  
                    



                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noGiro = grdAssetUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noGiro;

                    DataSet mySet = ObjDb.GetRows("select nodeposito,kdTransaksi,jenistransaksi,norekbank,Jenis,nomor,tglDeposito,tglJatuhTempo,nominal,NamaBank,norek,deskripsi " +
                        "from Tdeposito  where nodeposito = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    this.ShowHideGridAndForm(false, false,true);

                    kodetransaksi.Text = myRow["kdTransaksi"].ToString();
                    nomor.Text = myRow["nomor"].ToString();
                
                    tanggal.Text = Convert.ToDateTime(myRow["tglDeposito"]).ToString("dd-MMM-yyyy");
                    tanggalJatuhTempo.Text = Convert.ToDateTime(myRow["tglJatuhTempo"]).ToString("dd-MMM-yyyy");
                    nominal1.Text = ObjSys.IsFormatNumber(myRow["nominal"].ToString());
                    bank.Text = myRow["NamaBank"].ToString();
                    uraian1.Text = myRow["deskripsi"].ToString();
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
