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
    public partial class MstRekeningTransaksiKas : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdInstansi.DataSource = dt;
            grdInstansi.DataBind();
        }
        private void SetPreviousData()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList cboRekening = (DropDownList)grdInstansi.Rows[i].FindControl("cboRekening");

                        cboRekening.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih Rekening--' name union all SELECT distinct noRek as id, Ket as name FROM mRekening");
                        cboRekening.DataValueField = "id";
                        cboRekening.DataTextField = "name";
                        cboRekening.DataBind();
                        

                        cboRekening.Text = dt.Rows[i]["Column1"].ToString();
                    }
                }
            }
        }
        private void AddNewRow()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList cboRekening = (DropDownList)grdInstansi.Rows[i].FindControl("cboRekening");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = cboRekening.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdInstansi.DataSource = dtCurrentTable;
                    grdInstansi.DataBind();
                }
            }
            SetPreviousData();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstRekeningTransaksiKas.aspx");
                LoadDataSearch();
                SetInitialRow();
                for (int i = 1; i < 5; i++)
                {
                    AddNewRow();
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();
            int count = 0, count2 = 0;

            if (cboLokasi.Text == "0")
            {
                message += ObjSys.CreateMessage("Jenis transaksi tidak boleh kosong.");
                valid = false;
            }
           
            if (valid == true)
            {
                try
                {
                    for (int i = 0; i < grdInstansi.Rows.Count; i++)
                    {
                        DropDownList cboRekening = (DropDownList)grdInstansi.Rows[i].FindControl("cboRekening");
                        if (cboRekening.Text != "0"){
                                ObjDb.Data.Clear();
                        ObjDb.Data.Add("noTransKas", cboLokasi.Text);
                        ObjDb.Data.Add("norek", cboRekening.Text);
                        ObjDb.Insert("mRekeningtranskasikas", ObjDb.Data);
                        message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + cboRekening.Text + "</b> data tersimpan.");
                        count += 1;
                    }
                            
                        
                    }

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    if (count == 0)
                    {
                        ShowMessage("error", "Data gagal disimpan." + message);
                    }
                    else if (count2 > 0)
                    {
                        ShowMessage("warning", "Data berhasil disimpan." + message);
                    }
                    else
                    {
                        ShowMessage("success", "Data berhasil disimpan.");
                        clearData();
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
        private void clearData()
        {
            for (int i = 0; i < grdInstansi.Rows.Count; i++)
            {
                DropDownList cboRekening = (DropDownList)grdInstansi.Rows[i].FindControl("cboRekening");

                cboRekening.Text = "0";
                cboLokasi.Text = "0";
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearData();
        }
        #region brand
       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();


        
            if (valid == true)
            {
                try
                {
                 

                        ObjDb.Data.Clear();
                        ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                        ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                        ObjDb.Insert("mLokasi", ObjDb.Data);

                    
                      
                        LoadDataSearch();
                    
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

       
       
        protected void LoadDataSearch()
        {
           
            cboLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noTransKas id, JnsTransKas name FROM mJenisTransaksiKas) a");
            cboLokasi.DataValueField = "id";
            cboLokasi.DataTextField = "name";
            cboLokasi.DataBind();

         


        }
        //protected void LoadDataRekening()
        //{

        //    cboRekening.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noTransKas id, JnsTransKas name FROM mJenisTransaksiKas) a");
        //    cboRekening.DataValueField = "id";
        //    cboRekening.DataTextField = "name";
        //    cboRekening.DataBind();


        //}
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
          
            LoadDataSearch();
        }
    }
}