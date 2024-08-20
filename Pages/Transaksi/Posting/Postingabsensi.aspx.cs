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

namespace eFinance.Pages.Transaksi.Posting
{
    public partial class Postingabsensi : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                LoadDataabsen();
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

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            cboMonth.DataSource = ObjGlobal.GetDataProcedure("SPLoaddataposAbsen", ObjGlobal.Param);
            cboMonth.DataValueField = "bln";
            cboMonth.DataTextField = "bulan";
            cboMonth.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            cboYear.DataSource = ObjGlobal.GetDataProcedure("SPLoaddataposAbsenThn", ObjGlobal.Param);
            cboYear.DataValueField = "thn";
            cboYear.DataTextField = "thn";
            cboYear.DataBind();
        }

        protected void LoadDataabsen()
        {
            string absen = "0", absenpotong="0" ;
            string sql = "select absenkegaji,absenpotonggaji from Parametercabang where nocabang='" + ObjSys.GetCabangId + "' ";
            DataSet mySet = ObjDb.GetRows(sql);
            if (mySet.Tables[0].Rows.Count > 0)
            {
                absen = mySet.Tables[0].Rows[0]["absenkegaji"].ToString();
                absenpotong= mySet.Tables[0].Rows[0]["absenpotonggaji"].ToString();

                hdnabsendtg.Value = absen;
                hdnabsenptg.Value = absenpotong;
            }
        }


        protected void btnPosting_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";


            if (hdnabsendtg.Value == "1")
            {
                if (ObjDb.GetRows("select noAbsensi from tAbsensi where nocabang='" + ObjSys.GetCabangId + "' and month(tgl)='" + cboMonth.Text + "' and Year(tgl)='" + cboYear.Text + "'").Tables[0].Rows.Count == 0)
                {
                    message = ObjSys.CreateMessage("Masih Belum Masukan Data Absensi.");
                    alert = "error";
                    valid = false;
                }
            }

            if (hdnabsenptg.Value == "1")
            {
                if (ObjDb.GetRows("select noAbsensi from tAbsensi where nocabang='" + ObjSys.GetCabangId + "' and month(tgl)='" + cboMonth.Text + "' and Year(tgl)='" + cboYear.Text + "'").Tables[0].Rows.Count == 0)
                {
                    message = ObjSys.CreateMessage("Masih Belum Masukan Data Absensi.");
                    alert = "error";
                    valid = false;
                }
            }

                
            if (ObjDb.GetRows("select distinct month(bln) as bln from TGajibulanan a inner join mstkomponengaji b on a.nomaster=b.nokomponengaji where a.stsapv<>1 and a.nocabang='" + ObjSys.GetCabangId + "' and month(a.bln)='" + cboMonth.Text + "' and Year(a.bln)='" + cboYear.Text + "'").Tables[0].Rows.Count > 0)
            {
                message = ObjSys.CreateMessage("Masih ada Pendapatan dan Potongan Gaji yang Belum Disetujui.");
                alert = "error";
                valid = false;
            }

                
            if (ObjDb.GetRows("select distinct month(bln) as bln from TGajibulanan a inner join mstkomponengaji b on a.nomaster=b.nokomponengaji where b.jenis=1 and a.nocabang='" + ObjSys.GetCabangId + "' and month(a.bln)='" + cboMonth.Text + "' and Year(a.bln)='" + cboYear.Text + "'").Tables[0].Rows.Count > 0)
            {
                message = ObjSys.CreateMessage("Bulan Ini Sudah Pernah DiPosting.");
                alert = "error";
                valid = false;
            }

                
            if (ObjDb.GetRows("select distinct nokaryawan from mstKaryawan where nocabang='" + ObjSys.GetCabangId + "' and stsrubahgaji='0'").Tables[0].Rows.Count > 0)
            {
                message = ObjSys.CreateMessage("Masih Ada Data Karyawan yang Harus dirubah Karena Kenaikan Gaji.");
                alert = "error";
                valid = false;
            }

            try
            {
                if (valid == true)
                {
                    

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("blnkirim", cboMonth.Text);
                    ObjGlobal.Param.Add("thn", cboYear.Text);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.ExecuteProcedure("SPProsesBulananGaji", ObjGlobal.Param);
                    ObjGlobal.ExecuteProcedure("SPInserttabelgaji", ObjGlobal.Param);
                    ObjGlobal.ExecuteProcedure("SPProsesJurnalGaji", ObjGlobal.Param);

                    showHideForm(true, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data Berhasil Di Posting");
                    LoadData();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage(alert, message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void showHideForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

    }
}