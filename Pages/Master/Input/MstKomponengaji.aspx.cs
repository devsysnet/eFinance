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
using System.IO;

namespace eFinance.Pages.Master.Input
{
    public partial class MstKomponengaji : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //loadDataCombo();
                ObjSys.SessionCheck("MstKomponengaji.aspx");
                SetInitialRow();
                for (int i = 1; i <= 4; i++)
                {
                    AddNewRow();
                 
                }
               
            }
        }

       
           

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtkomponengaji", typeof(string)));
            dt.Columns.Add(new DataColumn("cboKategori", typeof(string)));
            dt.Columns.Add(new DataColumn("cboJenis", typeof(string)));
            dt.Columns.Add(new DataColumn("cbopph21", typeof(string)));
            dt.Columns.Add(new DataColumn("cbopengurang", typeof(string)));
            dt.Columns.Add(new DataColumn("cbobpjs", typeof(string)));
            dt.Columns.Add(new DataColumn("cbothnbln", typeof(string)));
            dt.Columns.Add(new DataColumn("cbnorek", typeof(string)));
            dt.Columns.Add(new DataColumn("cbojeniskegiatan", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["txtkomponengaji"] = string.Empty;
            dr["cboKategori"] = string.Empty;
            dr["cboJenis"] = string.Empty;
            dr["cbopph21"] = string.Empty;
            dr["cbopengurang"] = string.Empty;
            dr["cbobpjs"] = string.Empty;
            dr["cbothnbln"] = string.Empty;
            dr["cbnorek"] = "0";
            dr["cbojeniskegiatan"] = "0";


            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdkomponengaji.DataSource = dt;
            grdkomponengaji.DataBind();
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

                        TextBox txtkomponengaji = (TextBox)grdkomponengaji.Rows[i].FindControl("txtkomponengaji");
                        DropDownList cboKategori = (DropDownList)grdkomponengaji.Rows[i].FindControl("cboKategori");
                        DropDownList cboJenis = (DropDownList)grdkomponengaji.Rows[i].FindControl("cboJenis");
                        DropDownList cbopph21 = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbopph21");
                        DropDownList cbopengurang = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbopengurang");
                        DropDownList cbobpjs = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbobpjs");
                        DropDownList cbothnbln = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbothnbln");
                        DropDownList cbnorek = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorek");
                        DropDownList cbojeniskegiatan = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbojeniskegiatan");

                        cbnorek.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih COA--' name union all SELECT distinct norek as id, ket as name FROM mrekening where sts='2' and jenis in(16,36,44,47)");
                        cbnorek.DataValueField = "id";
                        cbnorek.DataTextField = "name";
                        cbnorek.DataBind();

                        cboKategori.DataSource = ObjDb.GetRows("SELECT distinct nokategori id, kategori name FROM MstkategorikomponenGaji");
                        cboKategori.DataValueField = "id";
                        cboKategori.DataTextField = "name";
                        cboKategori.DataBind();

                        cbojeniskegiatan.DataSource = ObjDb.GetRows("SELECT distinct namakegiatan id, namakegiatan name FROM mJenisKegiatan");
                        cbojeniskegiatan.DataValueField = "id";
                        cbojeniskegiatan.DataTextField = "name";
                        cbojeniskegiatan.DataBind();

                        txtkomponengaji.Text = dt.Rows[i]["txtkomponengaji"].ToString();
                        cboKategori.Text = dt.Rows[i]["cboKategori"].ToString();
                        cboJenis.Text = dt.Rows[i]["cboJenis"].ToString();
                        cbopph21.Text = dt.Rows[i]["cbopph21"].ToString();
                        cbopengurang.Text = dt.Rows[i]["cbopengurang"].ToString();
                        cbobpjs.Text = dt.Rows[i]["cbobpjs"].ToString();
                        cbothnbln.Text = dt.Rows[i]["cbothnbln"].ToString();
                        cbnorek.Text = dt.Rows[i]["cbnorek"].ToString();
                        cbojeniskegiatan.Text = dt.Rows[i]["cbojeniskegiatan"].ToString();



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

                        TextBox txtkomponengaji = (TextBox)grdkomponengaji.Rows[i].FindControl("txtkomponengaji");
                        DropDownList cboKategori = (DropDownList)grdkomponengaji.Rows[i].FindControl("cboKategori");                      
                        DropDownList cboJenis = (DropDownList)grdkomponengaji.Rows[i].FindControl("cboJenis");
                        DropDownList cbopph21 = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbopph21");
                        DropDownList cbopengurang = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbopengurang");
                        DropDownList cbobpjs = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbobpjs");
                        DropDownList cbothnbln = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbothnbln");
                        DropDownList cbnorek = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorek");
                        DropDownList cbojeniskegiatan = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbojeniskegiatan");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["txtkomponengaji"] = txtkomponengaji.Text;
                        dtCurrentTable.Rows[i]["cboKategori"] = cboKategori.Text;
                        dtCurrentTable.Rows[i]["cboJenis"] = cboJenis.Text;
                        dtCurrentTable.Rows[i]["cbopph21"] = cbopph21.Text;
                        dtCurrentTable.Rows[i]["cbopengurang"] = cbopengurang.Text;
                        dtCurrentTable.Rows[i]["cbobpjs"] = cbobpjs.Text;
                        dtCurrentTable.Rows[i]["cbothnbln"] = cbothnbln.Text;
                        dtCurrentTable.Rows[i]["cbnorek"] = cbnorek.Text;
                        dtCurrentTable.Rows[i]["cbojeniskegiatan"] = cbojeniskegiatan.Text;

                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grdkomponengaji.DataSource = dtCurrentTable;
                    grdkomponengaji.DataBind();
                }
            }
            SetPreviousData();
        }
        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            int count = 0, count2 = 0;
            try
            {
                for (int i = 0; i < grdkomponengaji.Rows.Count; i++)
                {
                    TextBox txtkomponengaji = (TextBox)grdkomponengaji.Rows[i].FindControl("txtkomponengaji");
                    DropDownList cboKategori = (DropDownList)grdkomponengaji.Rows[i].FindControl("cboKategori");
                    DropDownList cboJenis = (DropDownList)grdkomponengaji.Rows[i].FindControl("cboJenis");
                    DropDownList cbopph21 = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbopph21");
                    DropDownList cbopengurang = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbopengurang");
                    DropDownList cbobpjs = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbobpjs");
                    DropDownList cbothnbln = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbothnbln");
                    DropDownList cbnorek = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorek");
                    DropDownList cbojeniskegiatan = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbojeniskegiatan");

                    DataSet mySet = ObjDb.GetRows("Select * from Mstkomponengaji where komponengaji= '" + txtkomponengaji.Text + "'");
                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + txtkomponengaji.Text + "</b> gagal disimpan,  komponengaji sudah terdaftar sebelumnya.");
                        count2 += 1;
                    }
                    else
                    {
                        if (txtkomponengaji.Text != "")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("komponengaji", txtkomponengaji.Text);
                            ObjDb.Data.Add("kategori", cboKategori.Text);
                            ObjDb.Data.Add("jenis", cboJenis.Text);
                            ObjDb.Data.Add("pph21", cbopph21.Text);
                            ObjDb.Data.Add("bpjs", cbobpjs.Text);
                            ObjDb.Data.Add("bulanan", cbothnbln.Text);
                            ObjDb.Data.Add("absensi", cbopengurang.Text);
                            ObjDb.Data.Add("norek", cbnorek.Text);
                            ObjDb.Data.Add("jeniskegiatan", cbojeniskegiatan.Text);
                            ObjDb.Data.Add("sts", "1");
                            ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                            ObjDb.Insert("Mstkomponengaji", ObjDb.Data);
                            message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + txtkomponengaji.Text + "</b> data tersimpan.");
                            count += 1;
                        }
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
                    ClearData();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Invalid transaction data to database.");
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

        protected void ClearData()
        {
            SetInitialRow();
            for (int i = 1; i <= 4; i++)
            {
                AddNewRow();
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
            CloseMessage();
        }
    }
}