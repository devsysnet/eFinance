using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.Input
{
    public partial class MJenisTrs : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ObjSys.SessionCheck("MJenisTrs.aspx");
                loadDataCombo();
                cekKategori_Usaha();
                divjeniskegiatan.Visible = false;
            }
        }

        protected void cekKategori_Usaha()
        {
            if (ObjSys.GetKategori_Usaha == "Sekolah")
            {
                divDanaBOSH.Visible = true;
                divkodeVA.Visible = true;
                divdenda.Visible = true;
                divurutan.Visible = true;
                divlevel.Visible = true;
                divBank.Visible = true;
                divopenclose.Visible = true;

            }
            else
            {
                divDanaBOSH.Visible = false;
                divkodeVA.Visible = false;
                divdenda.Visible = false;
                divurutan.Visible = false;
                divlevel.Visible = false;
                divBank.Visible = false;
            }
        }

        private void loadDataCombo()
        {
        
            if (ObjSys.GetKategori_Usaha == "Sekolah")
            {
                if (ObjDb.GetRows("select systemkas from Parameter WHERE systemkas = 'Cash'").Tables[0].Rows.Count > 0)
                {
                    cbonorekDb.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Debit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis in('11') and sts='2' and nokegiatan='0') a");
                    cbonorekDb.DataValueField = "id";
                    cbonorekDb.DataTextField = "name";
                    cbonorekDb.DataBind();
                }
                if (ObjDb.GetRows("select systemkas from Parameter WHERE systemkas = 'Akrual'").Tables[0].Rows.Count > 0)
                {
                    cbonorekDb.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Debit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis in('12') and sts='2' and nokegiatan='0') a");
                    cbonorekDb.DataValueField = "id";
                    cbonorekDb.DataTextField = "name";
                    cbonorekDb.DataBind();
                }
            }
            else
            {
                cbonorekDb.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Debit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis in('1','2') and sts='2' and nokegiatan='0') a");
                cbonorekDb.DataValueField = "id";
                cbonorekDb.DataTextField = "name";
                cbonorekDb.DataBind();
            }


            cbonorekKd.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Kredit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis='12' and sts='2' and nokegiatan='0') a");
            cbonorekKd.DataValueField = "id";
            cbonorekKd.DataTextField = "name";
            cbonorekKd.DataBind();

            cbojnskegiatan.DataSource = ObjDb.GetRows("select a.* from (SELECT '0' id, '---Pilih Jenis Kegiatan---' name union SELECT distinct nomasterkegiatan id,kegiatan name FROM mmasterkegiatan  where sts='1' ) a");
            cbojnskegiatan.DataValueField = "id";
            cbojnskegiatan.DataTextField = "name";
            cbojnskegiatan.DataBind();

            cbomTransaksi.DataSource = ObjDb.GetRows("select a.* from (SELECT '0' id, '---Pilih Jenis Transaksi---' name union SELECT distinct jenistransaski id,jenistransaski name FROM mMasterjenistransaksi where sts='1' and nokegiatan=0) a");
            cbomTransaksi.DataValueField = "id";
            cbomTransaksi.DataTextField = "name";
            cbomTransaksi.DataBind();

        }
        protected void cboPosbln_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboposbln.Text == "2")
            {
                divjeniskegiatan.Visible = true;
            }
            else
            {
                divjeniskegiatan.Visible = false;
            }
        }
        
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;


         if (ObjSys.GetKategori_Usaha == "Sekolah")
          {
                    if (cbomTransaksi.Text == "0")
                    {
                        message += ObjSys.CreateMessage("Jenis Transaksi harus dipilih.");
                        valid = false;
                    }

                    if (cboposbln.Text == "")
                    {
                        message += ObjSys.CreateMessage("Kategori Transaksi harus dipilih.");
                        valid = false;
                    }
                    if (cboposbln.Text == "1")
                    {
                        if (ObjDb.GetRows("SELECT * FROM mJenistransaksi WHERE posbln = '1' and noCabang = '" + ObjSys.GetCabangId + "' ").Tables[0].Rows.Count > 0)
                        {
                            message += ObjSys.CreateMessage("Kategori Transaksi SPP sudah ada dan hanya untuk 1x input.");
                            valid = false;
                        }
                    }
                    if (cbonorekDb.Text == "0")
                    {
                        message += ObjSys.CreateMessage("COA Debit harus dipilih.");
                        valid = false;
                    }
                    if (cbonorekKd.Text == "0")
                    {
                        message += ObjSys.CreateMessage("COA Kredit tidak boleh kosong.");
                        valid = false;
                    }
                    if (txtdenda.Text == "")
                    {
                        message += ObjSys.CreateMessage("Nilai denda tidak boleh kosong.");
                        valid = false;
                    }
                    if (kodeva.Text == "")
                    {
                        message += ObjSys.CreateMessage("Kode Virtual Account tidak boleh kosong.");
                        valid = false;
                    }
                    if (txtUrutan.Text == "")
                    {
                        message += ObjSys.CreateMessage("Urutan tidak boleh kosong.");
                        valid = false;
                    }
                  
                    //if (ObjDb.GetRows("SELECT * FROM mJenistransaksi WHERE kodeVA = '" + kodeva.Text + "' and noCabang = '" + ObjSys.GetCabangId + "' ").Tables[0].Rows.Count > 0)
                    //{
                    //    if (ObjDb.GetRows("SELECT * FROM mJenistransaksi WHERE urutan = '" + txtUrutan.Text + "' and noCabang = '" + ObjSys.GetCabangId + "' ").Tables[0].Rows.Count > 0)
                    //    {
                    //        message += ObjSys.CreateMessage("Urutan sudah ada.");
                    //        valid = false;
                    //    }
                    //}

                    if (cbopelunasan.Text == "")
                    {
                        message += ObjSys.CreateMessage("Level Pelunasan harus dipilih.");
                        valid = false;
                    }
                    if (cbobank.Text == "")
                    {
                        message += ObjSys.CreateMessage("Bank harus dipilih.");
                        valid = false;
                    }
            }
            else
            {
                if (cbomTransaksi.Text == "0")
                {
                    message += ObjSys.CreateMessage("Jenis Transaksi harus dipilih.");
                    valid = false;
                }
                if (cbonorekDb.Text == "0")
                {
                    message += ObjSys.CreateMessage("COA Debit harus dipilih.");
                    valid = false;
                }
                if (cbonorekKd.Text == "0")
                {
                    message += ObjSys.CreateMessage("COA Kredit tidak boleh kosong.");
                    valid = false;
                }
            }

            if (valid == true)

            {
                try
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("jenisTransaksi", cbomTransaksi.Text);
                    ObjGlobal.Param.Add("posbln", cboposbln.Text);
                    ObjGlobal.Param.Add("posbln1", cboposbln1.Text);
                    ObjGlobal.Param.Add("norekDb", cbonorekDb.Text);
                    ObjGlobal.Param.Add("norekKd", cbonorekKd.Text);
                    ObjGlobal.Param.Add("kodeva", kodeva.Text);
                    ObjGlobal.Param.Add("denda", Convert.ToDecimal(txtdenda.Text).ToString());
                    ObjGlobal.Param.Add("urutan", txtUrutan.Text);
                    ObjGlobal.Param.Add("pelunasan", cbopelunasan.Text);
                    ObjGlobal.Param.Add("nobank", cbobank.Text);
                    ObjGlobal.Param.Add("nomhs", nomhs.Text);
                    ObjGlobal.Param.Add("noMkegiatan", cbojnskegiatan.Text);
                    ObjGlobal.Param.Add("sts", "1");
                    ObjGlobal.Param.Add("closeopen", closeopen.Text);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPmJenistransaksi", ObjGlobal.Param);

                    ClearData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
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
            jenisTransaksi.Text = "";
            kodeva.Text = "";
            txtdenda.Text = "0.00";
            txtUrutan.Text = "";
            //cbonorekKd.Text = "";
            //cbonorekDb.Text = "";
            CloseMessage();
        }

        protected void cbopelunasan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbopelunasan.Text == "2")
            {
                cbobank.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih Bank---' name union SELECT distinct norek id,ket name FROM mRekening where jenis='2' and sts='2' and nocabang=" + ObjSys.GetCabangId + " union SELECT distinct norek id,ket name FROM mRekening where jenis='1' and sts='2' ) a");
                cbobank.DataValueField = "id";
                cbobank.DataTextField = "name";
                cbobank.DataBind();
            }
            else if (cbopelunasan.Text == "3")
            {
                cbobank.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih Bank---' name union SELECT distinct a1.norek id, a1.ket name FROM mRekening a1 inner join mCabang b1 on a1.nocabang = b1.noCabang where a1.jenis='2' and a1.sts='2' and b1.parent=" + ObjSys.GetParentCabang + ") a");
                cbobank.DataValueField = "id";
                cbobank.DataTextField = "name";
                cbobank.DataBind();
            }
            else if (cbopelunasan.Text == "4")
            {
                cbobank.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih Bank---' name union SELECT distinct norek id, ket name FROM mRekening where jenis='2' and sts='2') a");
                cbobank.DataValueField = "id";
                cbobank.DataTextField = "name";
                cbobank.DataBind();
            }
            else if (cbopelunasan.Text == "")
            {
                cbobank.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih Bank---' name) a");
                cbobank.DataValueField = "id";
                cbobank.DataTextField = "name";
                cbobank.DataBind();
            }
        }

        protected void cbojnstrskegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbomTransaksi.DataSource = ObjDb.GetRows("select a.* from (SELECT '0' id, '---Pilih Jenis Transaksi---' name union SELECT distinct jenistransaski id,jenistransaski name FROM mMasterjenistransaksi where sts='1' and nokegiatan=" + cbojnskegiatan.Text + ") a");
            cbomTransaksi.DataValueField = "id";
            cbomTransaksi.DataTextField = "name";
            cbomTransaksi.DataBind();


            if (ObjDb.GetRows("select systemkas from Parameter WHERE systemkas = 'Cash'").Tables[0].Rows.Count > 0)
            {
                cbonorekDb.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Debit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis in('11') and sts='2' and nokegiatan=" + cbojnskegiatan.Text + " union SELECT distinct norek id,ket name FROM mRekening where jenis in('11') and sts='2' and nokegiatan='99' ) a");
                cbonorekDb.DataValueField = "id";
                cbonorekDb.DataTextField = "name";
                cbonorekDb.DataBind();
            }
            if (ObjDb.GetRows("select systemkas from Parameter WHERE systemkas = 'Akrual'").Tables[0].Rows.Count > 0)
            {
                cbonorekDb.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Debit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis in('12') and sts='2' and nokegiatan=" + cbojnskegiatan.Text + " union SELECT distinct norek id,ket name FROM mRekening where jenis in('12') and sts='2' and nokegiatan='99') a");
                cbonorekDb.DataValueField = "id";
                cbonorekDb.DataTextField = "name";
                cbonorekDb.DataBind();
            }

            cbonorekKd.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Kredit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis='12' and sts='2' and nokegiatan=" + cbojnskegiatan.Text + " union SELECT distinct norek id,ket name FROM mRekening where jenis='12' and sts='2' and nokegiatan='99') a");
            cbonorekKd.DataValueField = "id";
            cbonorekKd.DataTextField = "name";
            cbonorekKd.DataBind();

            //cbonorekDb.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Debit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis in('11') and sts='2' and nokegiatan=" + cbojnskegiatan.Text + " union SELECT distinct norek id,ket name FROM mRekening where jenis='11' and sts='2' and nokegiatan='99') a");
            //cbonorekDb.DataValueField = "id";
            //cbonorekDb.DataTextField = "name";
            //cbonorekDb.DataBind();


        }
    }
}