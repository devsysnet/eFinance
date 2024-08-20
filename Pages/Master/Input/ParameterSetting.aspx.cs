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

namespace eFinance.Pages.Master.Input
{
    public partial class ParameterSetting : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();
        protected void CreateViewStateRekCash(string index)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("noRek")
            });
            if (ViewState["RekCashState" + index] == null)
            {
                ArrayList userdetails = new ArrayList();
                ViewState["CHECKED_ITEMS" + index] = userdetails;
                ViewState["RekCashState" + index] = dt;
            }

        }
        protected void CreateViewStateRekBank(string index)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("noRek")
            });
            if (ViewState["RekBankState" + index] == null)
            {
                ArrayList userdetails = new ArrayList();
                ViewState["CHECKED_ITEMS" + index] = userdetails;
                ViewState["RekBankState" + index] = dt;
            }

        }
        protected void CreateViewStateUser(string index)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("noUser")
            });
            if (ViewState["UserState" + index] == null)
            {
                ArrayList userdetails = new ArrayList();
                ViewState["CHECKED_ITEMS" + index] = userdetails;
                ViewState["UserState" + index] = dt;
            }

        }

        private void SetInitialRowAJ()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("cboJenis", typeof(string)));
            dt.Columns.Add(new DataColumn("cboPosisi", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKdRek", typeof(string)));
            dt.Columns.Add(new DataColumn("lblKet", typeof(string)));
            dt.Columns.Add(new DataColumn("cboMataUang", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnNoRek", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["cboJenis"] = string.Empty;
            dr["cboPosisi"] = string.Empty;
            dr["txtKdRek"] = string.Empty;
            dr["lblKet"] = string.Empty;
            dr["cboMataUang"] = string.Empty;
            dr["hdnNoRek"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdAutoJurnal.DataSource = dt;
            grdAutoJurnal.DataBind();
        }
        private void SetPreviousDataAJ()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList cboJenis = (DropDownList)grdAutoJurnal.Rows[i].FindControl("cboJenis");
                        DropDownList cboPosisi = (DropDownList)grdAutoJurnal.Rows[i].FindControl("cboPosisi");
                        TextBox txtKdRek = (TextBox)grdAutoJurnal.Rows[i].FindControl("txtKdRek");
                        Label lblKet = (Label)grdAutoJurnal.Rows[i].FindControl("lblKet");
                        HiddenField hdnNoRek = (HiddenField)grdAutoJurnal.Rows[i].FindControl("hdnNoRek");
                        DropDownList cboMataUang = (DropDownList)grdAutoJurnal.Rows[i].FindControl("cboMataUang");

                        cboJenis.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-------' name union all SELECT distinct tipeAJ id, tipeAJ name FROM autoAJ ) a");
                        cboJenis.DataValueField = "id";
                        cboJenis.DataTextField = "name";
                        cboJenis.DataBind();

                        cboMataUang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-------' name union all SELECT noMataUang id, kodematauang name FROM mmataUang ) a");
                        cboMataUang.DataValueField = "id";
                        cboMataUang.DataTextField = "name";
                        cboMataUang.DataBind();

                        cboPosisi.Text = dt.Rows[i]["cboPosisi"].ToString();
                        txtKdRek.Text = dt.Rows[i]["txtKdRek"].ToString();
                        lblKet.Text = dt.Rows[i]["lblKet"].ToString();
                        hdnNoRek.Value = dt.Rows[i]["hdnNoRek"].ToString();
                    }
                }
            }
        }
        private void AddNewRowAJ()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {

                        DropDownList cboJenis = (DropDownList)grdAutoJurnal.Rows[i].FindControl("cboJenis");
                        DropDownList cboPosisi = (DropDownList)grdAutoJurnal.Rows[i].FindControl("cboPosisi");
                        TextBox txtKdRek = (TextBox)grdAutoJurnal.Rows[i].FindControl("txtKdRek");
                        Label lblKet = (Label)grdAutoJurnal.Rows[i].FindControl("lblKet");
                        HiddenField hdnNoRek = (HiddenField)grdAutoJurnal.Rows[i].FindControl("hdnNoRek");
                        DropDownList cboMataUang = (DropDownList)grdAutoJurnal.Rows[i].FindControl("cboMataUang");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["cboJenis"] = cboJenis.Text;
                        dtCurrentTable.Rows[i]["cboPosisi"] = cboPosisi.Text;
                        dtCurrentTable.Rows[i]["txtKdRek"] = txtKdRek.Text;
                        dtCurrentTable.Rows[i]["lblKet"] = lblKet.Text;
                        dtCurrentTable.Rows[i]["cboMataUang"] = cboMataUang.Text;
                        dtCurrentTable.Rows[i]["hdnNoRek"] = hdnNoRek.Value;

                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grdAutoJurnal.DataSource = dtCurrentTable;
                    grdAutoJurnal.DataBind();
                }
            }
            SetPreviousDataAJ();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ObjSys.SessionCheck("ParameterSetting.aspx");
                SetInitialRowAJ();
                for (int i = 1; i <= 2; i++)
                {
                    AddNewRowAJ();
                }
                LoadData();
                LoadDataCombo();
                loadDataBox();
            }
        }
        protected void LoadDataCombo()
        {
            cboTransaksi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-------' name union all SELECT distinct TransaksiAJ id, TransaksiAJ name FROM autoAJ ) a");
            cboTransaksi.DataValueField = "id";
            cboTransaksi.DataTextField = "name";
            cboTransaksi.DataBind();
        }
        protected void LoadData()
        {
            lblSignerInvoice.Text = ObjSys.GetUserName;
            DataSet data = ObjDb.GetRows("select * from setNumber");
            grdTransaksiKode.DataSource = data;
            grdTransaksiKode.DataBind();
        }
        protected void LoadDataRekening2()
        {

            if (ViewState["RekCashState"] == null)
            {
                ViewState["RekCashState"] = ObjDb.GetRowsDataTable("select noRek,kdrek,ket,0 stsPilih from mrekening where jenis='1' order by kdrek");
            }
            DataTable myData = (DataTable)ViewState["RekCashState"];
            myData.DefaultView.RowFilter = "";
            grdAddRekCash.DataSource = myData;
            grdAddRekCash.DataBind();
        }
        protected void LoadDataRekening3()
        {

            if (ViewState["RekBankState"] == null)
            {
                ViewState["RekBankState"] = ObjDb.GetRowsDataTable("select noRek,kdrek,ket,0 stsPilih from mrekening where jenis='2' order by kdrek");
            }
            DataTable myData = (DataTable)ViewState["RekBankState"];
            myData.DefaultView.RowFilter = "";
            grdAddBank.DataSource = myData;
            grdAddBank.DataBind();
        }
        protected void LoadDataRekeningAJ()
        {
            ObjGlobal.Param.Clear();
            //ObjGlobal.Param.Add("jenisRekening", hdnJenisRekening.Value);
            grdRekAJ.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekening", ObjGlobal.Param);
            grdRekAJ.DataBind();
        }
        protected void LoadDataUser()
        {

            if (ViewState["UserState"] == null)
            {
                ViewState["UserState"] = ObjDb.GetRowsDataTable("select noUser,namauser,0 stsPilih from mUser");
            }
            DataTable myData = (DataTable)ViewState["UserState"];
            myData.DefaultView.RowFilter = "";
            grdUser.DataSource = myData;
            grdUser.DataBind();
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
            txtCompanyEmail.Text = "";
            txtNama.Text = "";
            txtPhone.Text = "";
            txtAlamat.Text = "";
            txtBalanceSheet.Text = "";
            //txtBankAccount.Text = "";
            //txtSuratJalan2.Text = "";
            //txtSuratJalan.Text = "";
            //txtDO.Text = "";
            //txtPenandaTangan.Text = "";
            //txtCashAccount.Text = "";
            txtthnajaran.Text = "";
            dtsistem.Text = "";
            txtFax.Text = "";
            //txtPOLocalSignature.Text = "";
            txtNama.Text = "";
            //txtPreviousProfit.Text = "";
            //txtCurrentProfit.Text = "";
            //txtInvoicee.Text = "";
            cboTransaksi.Text = "0";
        }

        protected void btnSimpanCompany_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (txtNama.Text == "")
            {
                message += ObjSys.CreateMessage("Company Name tidak boleh kosong.");
                valid = false;
            }
            if (txtAlamat.Text == "")
            {
                message += ObjSys.CreateMessage("Alamat tidak boleh kosong.");
                valid = false;
            }
            if (txtPhone.Text == "")
            {
                message += ObjSys.CreateMessage("Nomor Telepon tidak boleh kosong.");
                valid = false;
            }
            if (txtCompanyEmail.Text == "")
            {
                message += ObjSys.CreateMessage("Email tidak boleh kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                    string fileName = "";
                        ObjDb.Where.Clear();
                        ObjDb.Data.Add("nmPerusahaan", txtNama.Text);
                        ObjDb.Data.Add("Alamat", txtAlamat.Text);
                        ObjDb.Data.Add("phone", txtPhone.Text);
                        ObjDb.Data.Add("fax", txtFax.Text);
                        ObjDb.Data.Add("Email", txtCompanyEmail.Text);
                        if (flUpload.HasFile)
                        {
                            ObjDb.Data.Add("logo", flUpload.FileName);
                            fileName = flUpload.FileName;
                            flUpload.SaveAs(Server.MapPath("~/Assets/images/CompanyProfile/" + flUpload.FileName));
                        }
                        ObjDb.Data.Add("modifiedBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("modifiedDate", ObjSys.GetNow);
                        ObjDb.Update("Parameter", ObjDb.Data, ObjDb.Where);
                    //ClearData();
                    ShowMessage("success", "Data berhasil disimpan.");
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
        protected void btnSimpanKode_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                for (int i = 0; i < grdTransaksiKode.Rows.Count; i++)
                {
                    string itemId = grdTransaksiKode.DataKeys[i].Value.ToString();
                    TextBox txtKode = (TextBox)grdTransaksiKode.Rows[i].FindControl("txtKode");
                  
                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noSN", itemId);
                    ObjDb.Data.Add("kodeSN", txtKode.Text);
                    ObjDb.Update("setnumber", ObjDb.Data, ObjDb.Where);

                }
                ShowMessage("success", "Data berhasil disimpan.");
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
        protected void btnSimpanPurchasing_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (txtthnajaran.Text == "")
            {
                message += ObjSys.CreateMessage("Tahun Ajaran tidak boleh kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                   
                    ObjDb.Where.Clear();
                    ObjDb.Data.Add("tahunAjaran", txtthnajaran.Text);
                    ObjDb.Data.Add("mulaisistem", dtsistem.Text);
                    ObjDb.Data.Add("gajithn", txtgajithn.Text);
                    ObjDb.Data.Add("gajigol", txtgajigol.Text);
                    ObjDb.Data.Add("alertgaji", txtalertgaji.Text);
                    ObjDb.Data.Add("setjamMasuk", txtjammasuk.Text);
                    ObjDb.Data.Add("setjamKeluar", txtjamkeluar.Text);
                    ObjDb.Data.Add("pnaikgol", Convert.ToDecimal(txtpersengol.Text).ToString());
                    ObjDb.Data.Add("pnaikrutin", Convert.ToDecimal(txtpersenrutin.Text).ToString());
                    ObjDb.Data.Add("systemkas", cbosystemkas.Text);
                    ObjDb.Data.Add("systembudget", cbosystembudget.Text);
                    ObjDb.Data.Add("pelunasan", cbopelunasan.Text);
                    ObjDb.Data.Add("jurnalgaji", cbodasarjurnal.Text);
                    ObjDb.Data.Add("pembayarangaji", cbobayargaji.Text);
                    ObjDb.Data.Add("pindahjrntagihan", cboakunpiutang.Text);
                    ObjDb.Data.Add("modifiedBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modifiedDate", ObjSys.GetNow);
                    ObjDb.Data.Add("smtphost", smtp.Text);
                    ObjDb.Data.Add("emailsender", emailsender.Text);
                    ObjDb.Data.Add("passwordsender", passwordsender.Text);

                    ObjDb.Update("Parameter", ObjDb.Data, ObjDb.Where);

                    //ClearData();
                    ShowMessage("success", "Data berhasil disimpan.");
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
        protected void btnSimpanFinance_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;


            if (txtBalanceSheet.Text == "")
            {
                message += ObjSys.CreateMessage("Balance Sheet tidak boleh kosong.");
                valid = false;
            }
            if (txtProfitLoss.Text == "")
            {
                message += ObjSys.CreateMessage("Profit Loss tidak boleh kosong.");
                valid = false;
            }
            //if (txtCurrentProfit.Text == "")
            //{
            //    message += ObjSys.CreateMessage("Current Profit tidak boleh kosong.");
            //    valid = false;
            //}
            //if (txtPreviousProfit.Text == "")
            //{
            //    message += ObjSys.CreateMessage("Previous Profit tidak boleh kosong.");
            //    valid = false;
            //}
            if (txtPPN.Text == "")
            {
                message += ObjSys.CreateMessage("PPN tidak boleh kosong.");
                valid = false;
            }
            if (txtPPH.Text == "")
            {
                message += ObjSys.CreateMessage("PPH tidak boleh kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                 
                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Data.Add("signInv", lblSignerInvoice.Text);
                    ObjDb.Data.Add("PosisiKeuangan", txtBalanceSheet.Text);
                    ObjDb.Data.Add("Aktivitas", txtProfitLoss.Text);
                    ObjDb.Data.Add("beginningDate", dtBeginningDate.Text);
                    ObjDb.Data.Add("RLtahunlalu", "");
                    ObjDb.Data.Add("RLberjalan", "");
                    ObjDb.Data.Add("ppn", Convert.ToDecimal(txtPPN.Text).ToString());
                    ObjDb.Data.Add("pph", Convert.ToDecimal(txtPPH.Text).ToString());
                    ObjDb.Data.Add("systemkas", cbobasis.Text);
                    ObjDb.Update("Parameter", ObjDb.Data, ObjDb.Where);

                    //ClearData();
                    ShowMessage("success", "Data berhasil disimpan.");
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
        protected void btnSimpanAJ_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;


            if (cboTransaksi.Text == "0")
            {
                message += ObjSys.CreateMessage("Traksaksi harus dipilih.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                   
                    for (int i = 0; i < grdAutoJurnal.Rows.Count; i++)
                    {
                        DropDownList cboJenis = (DropDownList)grdAutoJurnal.Rows[i].FindControl("cboJenis");
                        DropDownList cboPosisi = (DropDownList)grdAutoJurnal.Rows[i].FindControl("cboPosisi");
                        TextBox txtKdRek = (TextBox)grdAutoJurnal.Rows[i].FindControl("txtKdRek");
                        Label lblKet = (Label)grdAutoJurnal.Rows[i].FindControl("lblKet");
                        HiddenField hdnNoRek = (HiddenField)grdAutoJurnal.Rows[i].FindControl("hdnNoRek");
                        DropDownList cboMataUang = (DropDownList)grdAutoJurnal.Rows[i].FindControl("cboMataUang");


                        ObjDb.Data.Clear();
                        ObjDb.Data.Add("transaksiAJ", cboTransaksi.SelectedItem.ToString());
                        ObjDb.Data.Add("tipeAJ", cboJenis.Text);
                        ObjDb.Data.Add("posAJ", cboPosisi.Text);
                        ObjDb.Data.Add("noRek", hdnNoRek.Value);
                        ObjDb.Data.Add("noMataUang", cboMataUang.Text);
                        ObjDb.Data.Add("Jenis", "");
                        ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("createdDate", ObjSys.GetDate);
                        ObjDb.Insert("autoAJ", ObjDb.Data);
                    }
                    //ClearData();
                    ShowMessage("success", "Data berhasil disimpan.");
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
        protected void btnSimpanOther_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (txtNama.Text == "")
            {
                message += ObjSys.CreateMessage("Company Name tidak boleh kosong.");
                valid = false;
            }
            if (txtAlamat.Text == "")
            {
                message += ObjSys.CreateMessage("Alamat tidak boleh kosong.");
                valid = false;
            }
            if (txtPhone.Text == "")
            {
                message += ObjSys.CreateMessage("Nomor Telepon tidak boleh kosong.");
                valid = false;
            }
            if (txtCompanyEmail.Text == "")
            {
                message += ObjSys.CreateMessage("Email tidak boleh kosong.");
                valid = false;
            }
           
            if (valid == true)
            {
                try
                {
                   
                    ObjDb.Where.Clear();
                    
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                    ObjDb.Update("Parameter", ObjDb.Data, ObjDb.Where);
                    // }
                    //ClearData();
                    loadDataBox();
                    ShowMessage("success", "Data berhasil disimpan.");
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
        protected void loadDataBox()
        {
            DataSet MySet = ObjDb.GetRows("select * from Parameter");
            if (MySet.Tables[0].Rows.Count > 0)
            {
                DataRow MyRow = MySet.Tables[0].Rows[0];
                txtNama.Text = MyRow["nmPerusahaan"].ToString();
                txtAlamat.Text = MyRow["Alamat"].ToString();
                txtPhone.Text = MyRow["Phone"].ToString();
                txtFax.Text = MyRow["fax"].ToString();
                txtCompanyEmail.Text = MyRow["Email"].ToString();
                dtsistem.Text = MyRow["mulaisistem"].ToString();
                txtthnajaran.Text = MyRow["tahunAjaran"].ToString();
                txtgajithn.Text = MyRow["gajithn"].ToString();
                txtgajigol.Text = MyRow["gajigol"].ToString();
                txtalertgaji.Text = MyRow["alertgaji"].ToString();
                txtPPN.Text = MyRow["ppn"].ToString();
                cbobasis.Text = MyRow["systemkas"].ToString();
                txtPPH.Text = MyRow["pph"].ToString();
                txtBalanceSheet.Text = MyRow["posisikeuangan"].ToString();
                txtProfitLoss.Text = MyRow["Aktivitas"].ToString();
                cbosystemkas.Text = MyRow["systemkas"].ToString();
                cbosystembudget.Text = MyRow["systembudget"].ToString();
                cbopelunasan.Text = MyRow["pelunasan"].ToString();
                cbodasarjurnal.Text = MyRow["jurnalgaji"].ToString();
                cbobayargaji.Text = MyRow["pelunasan"].ToString();
                cboakunpiutang.Text = MyRow["pindahjrntagihan"].ToString();
                //txtCurrentProfit.Text = MyRow["RLberjalan"].ToString();
                //txtPreviousProfit.Text = MyRow["RLTahunlalu"].ToString();
                dtBeginningDate.Text = MyRow["mulaisistem"].ToString();
                smtp.Text = MyRow["smtphost"].ToString();
                emailsender.Text = MyRow["emailsender"].ToString();
                passwordsender.Text = MyRow["passwordsender"].ToString();

                //txtPenandaTangan.Text = MyRow["penandaTangan"].ToString();
                //txtInvoicee.Text = MyRow["invoice"].ToString();
            }
        }
        //protected void btnSimpanSignature_Click(object sender, EventArgs e)
        //{
        //    string message = "";
        //    bool valid = true;
        //    ObjDb.Data.Clear();

        //    if (txtSuratJalan.Text == "")
        //    {
        //        message += ObjSys.CreateMessage("Surat Jalan Name tidak boleh kosong.");
        //        valid = false;
        //    }
        //    if (txtSuratJalan2.Text == "")
        //    {
        //        message += ObjSys.CreateMessage("Surat Jalan 2 tidak boleh kosong.");
        //        valid = false;
        //    }
        //    if (txtDO.Text == "")
        //    {
        //        message += ObjSys.CreateMessage("DO Telepon tidak boleh kosong.");
        //        valid = false;
        //    }
        //    if (txtPOLocalSignature.Text == "")
        //    {
        //        message += ObjSys.CreateMessage("PO Local tidak boleh kosong.");
        //        valid = false;
        //    }
        //    if (txtPenandaTangan.Text == "")
        //    {
        //        message += ObjSys.CreateMessage("Penanda Tangan tidak boleh kosong.");
        //        valid = false;
        //    }
        //    if (valid == true)
        //    {
        //        try
        //        {
        //            //string fileName = "";
        //            //if (ObjDb.GetRows("select * from Parameter where nmPerusahaan = '" + txtNama.Text + "'").Tables[0].Rows.Count > 0)
        //            //{
        //            //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //            //    ShowMessage("error", "Nama Perusahaan sudah ada.");
        //            //}
        //            //else
        //            //{
        //            //ObjDb.Where.Clear();
        //            //ObjDb.Data.Add("suratJalan", txtSuratJalan.Text);
        //            //ObjDb.Data.Add("suratJalan2", txtSuratJalan2.Text);
        //            //ObjDb.Data.Add("DO", txtDO.Text);
        //            //ObjDb.Data.Add("POLocalSignature", txtPOLocalSignature.Text);
        //            //ObjDb.Data.Add("penandaTangan", txtPenandaTangan.Text);
        //            //ObjDb.Data.Add("invoice", txtInvoicee.Text);
        //            //ObjDb.Update("Parameter", ObjDb.Data, ObjDb.Where);
        //            // }
        //            //ClearData();
        //            ShowMessage("success", "Data berhasil disimpan.");
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //            ShowMessage("error", ex.ToString());
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //        ShowMessage("error", message);
        //    }
        //}

        protected void btnResetSignature_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        protected void btnResetCompany_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        protected void btnResetPurchasing_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        protected void btnResetFinance_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        protected void btnResetAJ_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        protected void btnResetOther_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        protected void btnAddRowAJ_Click(object sender, EventArgs e)
        {
            AddNewRowAJ();
        }

        protected void btnImgAddNoRekACash_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataRekening2();
            dlgAddRekCash.Show();
        }
        protected void btnImgAddNoRekBank_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataRekening3();
            dlgAddRekBank.Show();
        }
        protected void btnImgSelectUser_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataUser();
            dlgUser.Show();
        }

        protected void btnSubmitNoRek_Click(object sender, EventArgs e)
        {
            string dataKdRek = "";
            DataTable myData = (DataTable)ViewState["RekCashState"];
            for (int i = 0; i < grdAddRekCash.Rows.Count; i++)
            {
                CheckBox chkCheck = (CheckBox)grdAddRekCash.Rows[i].FindControl("chkCheck");
                string kdRek = grdAddRekCash.Rows[i].Cells[1].Text;
                DataRow[] rowCek = myData.Select("noRek='" + grdAddRekCash.DataKeys[i].Value.ToString() + "'");

                if (chkCheck.Checked == true)
                {
                    rowCek[0]["stsPilih"] = "1";
                    dataKdRek += kdRek + '^';
                }
                else
                    rowCek[0]["stsPilih"] = "0";
                myData.AcceptChanges();
                myData.Rows[i].SetModified();

            }
            //txtCashAccount.Text = dataKdRek.TrimEnd('^').ToString();
            ViewState["RekCashState"] = myData;
        }
        protected void btnSubmitRekBank_Click(object sender, EventArgs e)
        {
            string dataKdRek = "";
            DataTable myData = (DataTable)ViewState["RekBankState"];
            for (int i = 0; i < grdAddBank.Rows.Count; i++)
            {
                string kdRek = grdAddBank.Rows[i].Cells[1].Text;
                CheckBox chkCheck = (CheckBox)grdAddBank.Rows[i].FindControl("chkCheck");
                DataRow[] rowCek = myData.Select("noRek='" + grdAddBank.DataKeys[i].Value.ToString() + "'");
                if (chkCheck.Checked == true)
                {
                    rowCek[0]["stsPilih"] = "1";
                    dataKdRek += kdRek + '^';
                }
                else
                    rowCek[0]["stsPilih"] = "0";
                myData.AcceptChanges();
                myData.Rows[i].SetModified();

            }
            //txtBankAccount.Text = dataKdRek.TrimEnd('^').ToString();
            ViewState["RekBankState"] = myData;
        }
        protected void btnSubmitUser_Click(object sender, EventArgs e)
        {
            string dataNamaUser = "";
            DataTable myData = (DataTable)ViewState["UserState"];
            for (int i = 0; i < grdUser.Rows.Count; i++)
            {
                CheckBox chkCheck = (CheckBox)grdUser.Rows[i].FindControl("chkCheck");
                string namaUser = grdUser.Rows[i].Cells[1].Text;
                DataRow[] rowCek = myData.Select("noUser='" + grdUser.DataKeys[i].Value.ToString() + "'");

                if (chkCheck.Checked == true)
                {
                    rowCek[0]["stsPilih"] = "1";
                    dataNamaUser += namaUser + '^';
                }
                else
                    rowCek[0]["stsPilih"] = "0";
                myData.AcceptChanges();
                myData.Rows[i].SetModified();

            }
            lblSignerInvoice.Text = dataNamaUser.TrimEnd('^').ToString();
            ViewState["UserState"] = myData;
        }

        protected void grdAutoJurnal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                hdnRowIndexAJ.Value = rowIndex.ToString();
                if (e.CommandName == "Browse")
                {
                    //hdnJenisRekening.Value = "";
                    LoadDataRekeningAJ();
                    lblPesanRekAJ.Visible = false;
                    dlgRekeningAJ.Show();
                }
                else if (e.CommandName == "Empty")
                {
                    TextBox txtKdRek = (TextBox)grdAutoJurnal.Rows[rowIndex].FindControl("txtKdRek");
                    HiddenField hdnNoRek = (HiddenField)grdAutoJurnal.Rows[rowIndex].FindControl("hdnNoRek");
                    txtKdRek.Text = "";
                    hdnNoRek.Value = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Invalid transaction data.");
            }
        }
        protected void grdAddRekCash_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnStsPilih = (HiddenField)e.Row.FindControl("hdnStsPilih");
                CheckBox chkCheck = (CheckBox)e.Row.FindControl("chkCheck");

                if (hdnStsPilih.Value == "1")
                    chkCheck.Checked = true;
                else
                    chkCheck.Checked = false;
            }
        }
        protected void grdAddBank_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnStsPilih = (HiddenField)e.Row.FindControl("hdnStsPilih");
                CheckBox chkCheck = (CheckBox)e.Row.FindControl("chkCheck");

                if (hdnStsPilih.Value == "1")
                    chkCheck.Checked = true;
                else
                    chkCheck.Checked = false;
            }
        }
        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnStsPilih = (HiddenField)e.Row.FindControl("hdnStsPilih");
                CheckBox chkCheck = (CheckBox)e.Row.FindControl("chkCheck");

                if (hdnStsPilih.Value == "1")
                    chkCheck.Checked = true;
                else
                    chkCheck.Checked = false;
            }
        }
        protected void grdRekAJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;
                int rowIndexHdn = Convert.ToInt32(hdnRowIndexAJ.Value);
                int rowIndex = grdRekAJ.SelectedRow.RowIndex;
                string kdRek = grdRekAJ.SelectedRow.Cells[1].Text;
                string noRek = grdRekAJ.DataKeys[rowIndex].Values[0].ToString();

                TextBox txtKdRek = (TextBox)grdAutoJurnal.Rows[rowIndexHdn].FindControl("txtKdRek");
                HiddenField hdnNoRek = (HiddenField)grdAutoJurnal.Rows[rowIndexHdn].FindControl("hdnNoRek");

                for (int i = 0; i < grdAutoJurnal.Rows.Count; i++)
                {
                    HiddenField hdnNoRekCek = (HiddenField)grdAutoJurnal.Rows[i].FindControl("hdnNoRek");
                    if (noRek == hdnNoRekCek.Value)
                    {
                        message += ObjSys.CreateMessage("Account Code : " + kdRek + " has choose.");
                        valid = false;
                    }

                }

                if (valid == true)
                {
                    dlgRekeningAJ.Hide();
                    txtKdRek.Text = kdRek;
                    hdnNoRek.Value = noRek;
                }
                else
                {
                    dlgRekeningAJ.Show();
                    lblPesanRekAJ.Text = ObjSys.GetMessage("error", message);
                    lblPesanRekAJ.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void grdRekAJ_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRekAJ.PageIndex = e.NewPageIndex;
            LoadDataRekeningAJ();
            dlgRekeningAJ.Show();
        }
        
        private void loadDataUser()
        {
            grdUserNew.DataSource = ObjDb.GetRows("select * from mUser");
            grdUserNew.DataBind();
        }

        protected void grdUserNew_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUserNew.PageIndex = e.NewPageIndex;
            loadDataUser();
        }

        protected void grdUserNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            string prodno = "", prodnm = "", noprod = "";
            prodno = (grdUserNew.SelectedRow.FindControl("lblInstansi") as Label).Text;
            noprod = (grdUserNew.SelectedRow.FindControl("hdnInstansi") as HiddenField).Value;

           
            mpe3.Hide();
        }

        protected void btnCariUser_Click(object sender, EventArgs e)
        {
            loadDataUser();
        }

        protected void btnPenanda_Click(object sender, ImageClickEventArgs e)
        {
            mpe3.Show();
            loadDataUser();
        }


    }
}