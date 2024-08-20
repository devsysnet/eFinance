using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;


namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransPelunasanARUpdate1 : System.Web.UI.Page
    {

        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
            }
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
                cboCabang.DataBind();
            }


            cboAccount.DataSource = ObjDb.GetRows("select a.* from (select '0' id,' ---Pilih Akun--- ' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='2' and nocabang = '" + ObjSys.GetCabangId + "' union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis in(1,22)) a order by a.id");
            cboAccount.DataValueField = "id";
            cboAccount.DataTextField = "name";
            cboAccount.DataBind();
        }


        #region rows
    
        #endregion
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("noCabang", cboCabang.Text);
            grdMemoJurnalD.DataSource = ObjGlobal.GetDataProcedure("SPpelunasanupdate", ObjGlobal.Param);
            grdMemoJurnalD.DataBind();
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
        protected void grdMemoJurnalD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMemoJurnalD.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void grdMemoJurnalD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {

                int rowIndex = grdMemoJurnalD.SelectedRow.RowIndex;
                string noPO = grdMemoJurnalD.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noPO;
                DataSet MySet = ObjDb.GetRows("select distinct nomorkode, tgl,norek from tkas a where nomorkode = '" + noPO + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0]; 
                    txtKode.Text = MyRow["nomorkode"].ToString();
                    dtDate.Text = Convert.ToDateTime(MyRow["Tgl"]).ToString("dd-MMM-yyyy");
                    cboAccount.Text = MyRow["norek"].ToString();

                    CloseMessage();
                    this.ShowHideGridAndForm(false, true);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Tidak ada.");
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
     

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

           
            DataSet protekpos = ObjDb.GetRows("SELECT protekpos as protekpos FROM mcabang WHERE nocabang = '" + ObjSys.GetCabangId + "' and protekpos=1");
            if (protekpos.Tables[0].Rows.Count > 0)
            {
                DataSet dataSaldobln1 = ObjDb.GetRows("select distinct month(tgl) as bln from tsaldobln where noCabang = '" + ObjSys.GetCabangId + "' and sts=0 and year(tgl)='" + Convert.ToDateTime(dtDate.Text).Year + "'");
                if (dataSaldobln1.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSK = dataSaldobln1.Tables[0].Rows[0];
                    int blnDb = int.Parse(myRowSK["bln"].ToString());

                    if (Convert.ToDateTime(dtDate.Text).Month != blnDb)
                    {
                        message += ObjSys.CreateMessage("Sudah Posting Bulanan GL");
                        valid = false;
                    }

                }

                DataSet dataSaldobln = ObjDb.GetRows("select distinct year(tgl) as thn from btsaldobln where noCabang = '" + ObjSys.GetCabangId + "' and sts=0 and year(tgl)='" + Convert.ToDateTime(dtDate.Text).Year + "'");
                if (dataSaldobln.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSK = dataSaldobln.Tables[0].Rows[0];
                    int thnDb = int.Parse(myRowSK["thn"].ToString());

                    if (Convert.ToDateTime(dtDate.Text).Year == thnDb)
                    {
                        message += ObjSys.CreateMessage("Sudah Posting Tahunan GL");
                        valid = false;
                    }

                }
            }

           
            if (valid == true)
            {
                try
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("nomorkode", hdnId.Value);
                    ObjGlobal.Param.Add("norek", cboAccount.Text);
                    ObjGlobal.Param.Add("tgl", dtDate.Text);
                    ObjGlobal.Param.Add("modiby", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPUpdatetglpelunasan", ObjGlobal.Param);


                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diubah.");
                    //clearData();
                    this.ShowHideGridAndForm(true, false);
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


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false);
        }
       
        protected void btnReset_Click(object sender, EventArgs e)
        {
            //clearData();
            LoadData();
            this.ShowHideGridAndForm(true, false);
            CloseMessage();
        }

      

        private void LoadDataPanel()
        {
            //grdProduct.DataSource = ObjDb.GetRows("select * from mRekening where  (kdRek like '%" + TextBox1.Text + "%' and sts = '2') or (Ket like '%" + TextBox1.Text + "%' and sts='2')");
            //grdProduct.DataBind();
        }
        protected void btnCariProduct_Click(object sender, EventArgs e)
        {
            LoadDataPanel();
            mpe.Show();
        }

        protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
          
            LoadDataPanel();
            mpe.Show();
        }

    
    }
}