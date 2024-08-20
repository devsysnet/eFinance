using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Posting
{
    public partial class PostingHarianGLnew : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtMulai.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                dtSampai.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadData();
                loadDataCombo();
            }
        }
        protected void LoadData()
        {
            CloseMessage();
            grdHarianGL.DataSource = ObjDb.GetRowsDataTable("SELECT a.kdtran, a.tgl, SUM(a.debet) AS debet, SUM(a.kredit) AS kredit FROM dbo.tkasdetil a inner join mrekening b on a.norek=b.norek where a.tgl BETWEEN '" + dtMulai.Text + "' and '" + dtSampai.Text + "' AND (a.sts='' or a.sts is null or a.sts='0')  and a.nocabang='"+ cboCabang.Text  + "' and b.grup<>'Danabos' GROUP BY a.kdtran, a.tgl");
            grdHarianGL.DataBind();
            if (grdHarianGL.Rows.Count > 0)
                btnPosting.Visible = true;
            else
                btnPosting.Visible = false;

        }

        protected void loadDataCombo()
        {
            //Perwakilan Mardi Yuana Bogor GetstsPusat=1
            if (ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4) and parent = " + ObjSys.GetCabangId + ") a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //unit
            else if (ObjSys.GetstsPusat == "2")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //admin Kantor Perwakilan Bogor GetstsPusat=3
            else if (ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4) and parent = " + ObjSys.GetParentCabang + ") a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4)) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4)) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();            }

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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bool valid = true;

            DataSet mySet2 = ObjDb.GetRows("select month('" + dtMulai.Text + "') as dari, month('" + dtSampai.Text + "') as sampai");
            DataRow myRow2 = mySet2.Tables[0].Rows[0];
            int dari = Convert.ToInt32(myRow2["dari"].ToString());
            int sampai = Convert.ToInt32(myRow2["sampai"].ToString());
           

            if (dari != sampai)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Bulan transaksi harus sama");
                valid = false;
            }
            else
            {
                if (ObjDb.GetRows("select distinct sts from tkasdetil where sts=1 and nocabang='" + cboCabang.Text + "' and month(tgl)=month('" + dtMulai.Text + "') and year(tgl)=year('" + dtMulai.Text + "')").Tables[0].Rows.Count > 0)
                {
                    LoadData();
                    loadDataCombo();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Sudah Diposting");
                    valid = false;
                    
                }
                else
                if (ObjDb.GetRows("select distinct sts from tsaldobln where sts=1 and nocabang='" + cboCabang.Text + "' and month(tgl)=month('" + dtMulai.Text + "')").Tables[0].Rows.Count <= 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Belum Posting Akhir Bulan Lalu");
                    valid = false;
                }
                else
                {
                LoadData();
                CloseMessage();
                valid = true;
                }
            }
        }

        protected void grdHarianGL_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdHarianGL.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void btnPosting_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", StrKode = "", alert = "", nocabang1 = "";
           
            int cekData = 0;
            for (int i = 0; i < grdHarianGL.Rows.Count; i++)
            {
                CheckBox chkCheck = (CheckBox)grdHarianGL.Rows[i].FindControl("chkCheck");
                string itemId = grdHarianGL.DataKeys[i].Value.ToString();
                if (chkCheck.Checked == true)

                {
                    cekData++;
                    StrKode += itemId.ToString() + ",";
                }
            }

            if (cekData == 0)
            {
                message = ObjSys.CreateMessage("Tidak ada data yang dipilih.");
                alert = "error";
                valid = false;
            }

            try
            {
               
                if (valid == true)
                {
                   if (cekData > 0)
                    {

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("kodeTransaksi", StrKode);
                        ObjGlobal.Param.Add("nocabang", cboCabang.Text);
                        ObjGlobal.ExecuteProcedure("SPProsesPostingHarianGL", ObjGlobal.Param);
                        ObjGlobal.ExecuteProcedure("SPProsesupdatestsGL", ObjGlobal.Param);

                        LoadData();
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil diposting");
                    }
                   
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
    }
}