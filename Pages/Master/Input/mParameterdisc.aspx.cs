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
    public partial class mParameterdisc : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("mParameterdisc.aspx");
                loadDataCombo();
            }
        }

        private void loadDataCombo()
        {
            cbojnstransaksi.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih Jenis Transaksi---' name union select noTransaksi as id, jenisTransaksi as name from mJenisTransaksi where nocabang='" + ObjSys.GetCabangId + "') a");
            cbojnstransaksi.DataValueField = "id";
            cbojnstransaksi.DataTextField = "name";
            cbojnstransaksi.DataBind();

            cbonorek.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Debit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis='23' and sts='2') a");
            cbonorek.DataValueField = "id";
            cbonorek.DataTextField = "name";
            cbonorek.DataBind();

        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (namaDiskon.Text == "")
            {
                message += ObjSys.CreateMessage("Nama Discount tidak boleh kosong.");
                valid = false;
            }

            if (cbojnstransaksi.Text == "")
            {
                message += ObjSys.CreateMessage("Kategori Transaksi harus dipilih.");
                valid = false;
            }
            if (dtsistem.Text == "")
            {
               
                message += ObjSys.CreateMessage("Tanggal Mulai System Harus Diisi");
                valid = false;
               
            }
            if (dtsistem1.Text == " ")
            {
                message += ObjSys.CreateMessage("Tanggal Selesai System Harus Diisi");
                valid = false;
            }
           
            if (cboStatus.Text == "-")
            {
                message += ObjSys.CreateMessage("Jenis Discount Harus Diisi");
                valid = false;
            }
            if (txtnilai.Text == "")
            {
                message += ObjSys.CreateMessage("Nilai tidak boleh kosong.");
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


            if (valid == true)

            {
                try
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("namaDiskon", namaDiskon.Text);
                    ObjGlobal.Param.Add("noTransaksi", cbojnstransaksi.Text);
                    ObjGlobal.Param.Add("drtgl", Convert.ToDateTime(dtsistem.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("sdtgl", Convert.ToDateTime(dtsistem1.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("nilai", Convert.ToDecimal(txtnilai.Text).ToString());
                    ObjGlobal.Param.Add("jns", cboStatus.Text);
                    ObjGlobal.Param.Add("norek", cbonorek.Text);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPmparameterdisc", ObjGlobal.Param);

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
            namaDiskon.Text = "";
            CloseMessage();
        }
    }
}