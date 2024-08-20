using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransAlokasiTagihan : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData("");

            }
        }

        protected void LoadData(string text)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", text);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdAssetUpdate.DataSource = ObjGlobal.GetDataProcedure("SPLoadAlokasiTagihan", ObjGlobal.Param);
            grdAssetUpdate.DataBind();

        }
        protected void tes(object sender, EventArgs e)
        {

            LoadData(txtSearchAsset.Text);

        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            int count = 0, selisih = 0;
            for (int i = 0; i < grdARSiswa.Rows.Count; i++)
            {

                CheckBox chkCheck = (CheckBox)grdARSiswa.Rows[i].FindControl("chkCheck");
                HiddenField amount = (HiddenField)grdARSiswa.Rows[i].FindControl("amount");
                TextBox txtbayar = (TextBox)grdARSiswa.Rows[i].FindControl("txtbayar");
                if (chkCheck.Checked == true && txtbayar.Text != "")
                    count++;

            }

            if (count == 0)
            {
                message += ObjSys.CreateMessage("Data harus dipilih.");
                valid = false;
            }
           
            if (valid == true)
            {
                try
                {
                    bool valid1 = true;
                    for (int x = 0; x < grdARSiswa.Rows.Count; x++)
                    {
                        
                        CheckBox chkCheck = (CheckBox)grdARSiswa.Rows[x].FindControl("chkCheck");
                        HiddenField hdnNnopiutang = (HiddenField)grdARSiswa.Rows[x].FindControl("hdnNnopiutang");
                        HiddenField hdnNoSiswa = (HiddenField)grdARSiswa.Rows[x].FindControl("hdnNoSiswa");
                        HiddenField hdnpayment_transaction_id = (HiddenField)grdARSiswa.Rows[x].FindControl("hdnpayment_transaction_id");
                        HiddenField amount = (HiddenField)grdARSiswa.Rows[x].FindControl("amount");
                        HiddenField saldo = (HiddenField)grdARSiswa.Rows[x].FindControl("saldo");
                        HiddenField hdntglbayar = (HiddenField)grdARSiswa.Rows[x].FindControl("hdntglbayar1");
                        TextBox txtbayar = (TextBox)grdARSiswa.Rows[x].FindControl("txtbayar");
                        if (chkCheck.Checked == true)
                        {
                            if (Convert.ToDecimal(txtbayar.Text) > Convert.ToDecimal(saldo.Value))
                            {
                                message += ObjSys.CreateMessage("Nilai bayar harus sama dengan saldo");
                                valid1 = false;
                            }

                            if (valid1 == true)
                            {

                                        if (txtAmount1.Text == txtTotal.Text)
                                    {
                                        ObjGlobal.Param.Clear();
                                        ObjGlobal.Param.Add("nopiutang", hdnNnopiutang.Value);
                                        ObjGlobal.Param.Add("nilaibayar", Convert.ToDecimal(txtbayar.Text).ToString());
                                        ObjGlobal.Param.Add("tglbayar", Convert.ToDateTime(hdntglbayar.Value).ToString("yyyy-MM-dd"));
                                        ObjGlobal.Param.Add("nosiswa", hdnNoSiswa.Value);
                                        ObjGlobal.GetDataProcedure("SPUpdatePiutangVA", ObjGlobal.Param);

                                        ObjGlobal.Param.Clear();
                                        ObjGlobal.Param.Add("nosiswa", hdnNoSiswa.Value);
                                        ObjGlobal.Param.Add("payment_transaction_id", hdnpayment_transaction_id.Value);

                                        ObjGlobal.GetDataProcedure("SPUpdatestatuspayment", ObjGlobal.Param);
                                    }
                            }

                        }
                    }
                    if (txtAmount1.Text != txtTotal.Text)
                    {
                        message += ObjSys.CreateMessage("Nilai bayar harus sama dengan nilai");
                        valid1 = false;
                    }
                    if (valid1 == true)
                    {
                        DataSet mySet = ObjDb.GetRows("select top 1 norekbank from mJenisTransaksi where kategori='Open' and nocabang='" + ObjSys.GetCabangId + "'");
                        DataRow myRow = mySet.Tables[0].Rows[0];
                        string norekbak = myRow["norekbank"].ToString();

                        //Init tanya rizal
                        HiddenField tglbayar = (HiddenField)grdARSiswa.Rows[0].FindControl("hdntglbayar1");

                        string Kode = ObjSys.GetCodeAutoNumberNew("1", Convert.ToDateTime(tglbayar.Value).ToString("yyyy-MM-dd"));

                        string kdrekkas = "";
                        DataSet dataSN = ObjDb.GetRows("SELECT kdrek FROM mRekening WHERE norek = '" + norekbak + "'");
                        if (dataSN.Tables[0].Rows.Count > 0)
                        {
                            DataRow myRowSn = dataSN.Tables[0].Rows[0];
                            kdrekkas = myRowSn["kdrek"].ToString();
                        }

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("nomorKode", Kode);
                        ObjGlobal.Param.Add("type", "Kas/Bank Masuk");
                        ObjGlobal.Param.Add("Tgl", Convert.ToDateTime(tglbayar.Value).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("Uraian", "Pembayaran Siswa");
                        ObjGlobal.Param.Add("noMataUang", "0");
                        ObjGlobal.Param.Add("kursKas", "1");
                        ObjGlobal.Param.Add("Nilai", Convert.ToDecimal(txtTotal.Text).ToString());
                        ObjGlobal.Param.Add("nilaiRp", Convert.ToDecimal(txtTotal.Text).ToString());
                        ObjGlobal.Param.Add("sts", "1");
                        ObjGlobal.Param.Add("StsApv", "0");
                        ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("createdDate", ObjSys.GetNow);
                        ObjGlobal.GetDataProcedure("SPInputKasPelunasanNewva1", ObjGlobal.Param); ;

                        ObjSys.UpdateAutoNumberCodeNew("1", Convert.ToDateTime(tglbayar.Value).ToString("yyyy-MM-dd"));
                        for (int i = 0; i < grdARSiswa.Rows.Count; i++)
                        {
                            HiddenField hdnNoPiut = (HiddenField)grdARSiswa.Rows[i].FindControl("hdnNnopiutang");
                            CheckBox chkCheck = (CheckBox)grdARSiswa.Rows[i].FindControl("chkCheck");

                            if (chkCheck.Checked == true)
                            {
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("noPiutang", hdnNoPiut.Value);
                                ObjGlobal.Param.Add("nomorKode", Kode);
                                ObjGlobal.GetDataProcedure("SPUpdateNomorKodeTransPiutang", ObjGlobal.Param);
                            }
                        }

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("nomorKode", Kode);
                        ObjGlobal.Param.Add("type", "Kas/Bank Masuk");
                        ObjGlobal.Param.Add("Tgl", Convert.ToDateTime(tglbayar.Value).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                        ObjGlobal.GetDataProcedure("SPInputKasHistoryBayarPelunasan", ObjGlobal.Param);


                        DataSet mySet1 = ObjDb.GetRows("select * from tkas where nomorKode = '" + Kode + "'");
                        DataRow myRow1 = mySet1.Tables[0].Rows[0];
                        string Id = myRow1["noKas"].ToString();

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noKas", Id);
                        ObjGlobal.Param.Add("kdTran", Kode);
                        ObjGlobal.Param.Add("jenisTran", "Pembayaran Siswa");
                        ObjGlobal.Param.Add("noTran", Id);
                        ObjGlobal.Param.Add("Debet", Convert.ToDecimal(txtTotal.Text).ToString());
                        ObjGlobal.Param.Add("Kredit", "0");
                        ObjGlobal.Param.Add("sts", "0");
                        ObjGlobal.Param.Add("Tgl", Convert.ToDateTime(tglbayar.Value).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("createdDate", ObjSys.GetNow);
                        ObjGlobal.GetDataProcedure("SPInputKasDetilPelunasanVA", ObjGlobal.Param);


                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noKas", Id);
                        ObjGlobal.Param.Add("kdTran", Kode);
                        ObjGlobal.Param.Add("jenisTran", "Pembayaran Siswa");
                        ObjGlobal.Param.Add("noTran", Id);
                        ObjGlobal.Param.Add("Debet", "0");
                        ObjGlobal.Param.Add("sts", "0");
                        ObjGlobal.Param.Add("Tgl", Convert.ToDateTime(tglbayar.Value).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("createdDate", ObjSys.GetNow);
                        ObjGlobal.GetDataProcedure("SPInputKasDetilPelunasan2NewVA", ObjGlobal.Param);



                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil disimpan.");
                        LoadData("");
                        ShowHideGridAndForm(true, false);
                    }else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", message);
                    }
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

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            ShowHideGridAndForm(true, false);
        }



        protected void grdAssetUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdAssetUpdate.PageIndex = e.NewPageIndex;
            LoadData("");
        }

        protected void grdAssetUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectEdit")
                {

                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noSiswa = grdAssetUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noSiswa;
                    HiddenField payment_transaction_id = (HiddenField)grdAssetUpdate.Rows[rowIndex].FindControl("payment_transaction_id");
                    HiddenField totalbayar = (HiddenField)grdAssetUpdate.Rows[rowIndex].FindControl("totalbayar");

                    ObjGlobal.Param.Add("noSiswa", noSiswa);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("payment_transaction_id", payment_transaction_id.Value);
                    DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadAlokasiTagihanDetail", ObjGlobal.Param);
                    grdARSiswa.DataSource = mySetH;
                    grdARSiswa.DataBind();
                    this.ShowHideGridAndForm(false, true);
                    DataRow myRow = mySetH.Tables[0].Rows[0];
                    txtAmount1.Text = myRow["nilaiTotal"].ToString();
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