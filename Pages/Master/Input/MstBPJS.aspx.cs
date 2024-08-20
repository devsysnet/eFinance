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
    public partial class MstBPJS : System.Web.UI.Page
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
                ObjSys.SessionCheck("MstBPJS.aspx");
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
            dt.Columns.Add(new DataColumn("txtjenisIuran", typeof(string)));
            dt.Columns.Add(new DataColumn("txtpersenperusahaan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtpersenkaryawan", typeof(string)));
            dt.Columns.Add(new DataColumn("cbopph21", typeof(string)));
            dt.Columns.Add(new DataColumn("cbokategori", typeof(string)));
            dt.Columns.Add(new DataColumn("cbokomponengaji", typeof(string)));
            dt.Columns.Add(new DataColumn("cbnorek", typeof(string)));
            dt.Columns.Add(new DataColumn("cbnorekkd", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["txtjenisIuran"] = string.Empty;
            dr["txtpersenperusahaan"] = string.Empty;
            dr["txtpersenkaryawan"] = string.Empty;
            dr["cbopph21"] = string.Empty;
            dr["cbokategori"] = string.Empty;
            dr["cbokomponengaji"] = "0";
            dr["cbnorek"] = "0";
            dr["cbnorekkd"] = "0";

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

                        TextBox txtjenisIuran = (TextBox)grdkomponengaji.Rows[i].FindControl("txtjenisIuran");
                        TextBox txtpersenperusahaan = (TextBox)grdkomponengaji.Rows[i].FindControl("txtpersenperusahaan");
                        TextBox txtpersenkaryawan = (TextBox)grdkomponengaji.Rows[i].FindControl("txtpersenkaryawan");
                        DropDownList cbopph21 = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbopph21");
                        DropDownList cboKategori = (DropDownList)grdkomponengaji.Rows[i].FindControl("cboKategori");
                        DropDownList cbokomponengaji = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbokomponengaji");
                        DropDownList cbnorek = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorek");
                        DropDownList cbnorekkd = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorekkd");

                        cbnorek.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih COA--' name union all SELECT distinct norek as id, ket as name FROM mrekening where jenis in(36)");
                        cbnorek.DataValueField = "id";
                        cbnorek.DataTextField = "name";
                        cbnorek.DataBind();

                        cbnorekkd.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih COA--' name union all SELECT distinct norek as id, ket as name FROM mrekening where jenis in(35)");
                        cbnorekkd.DataValueField = "id";
                        cbnorekkd.DataTextField = "name";
                        cbnorekkd.DataBind();

                        cbokomponengaji.DataSource = ObjDb.GetRows("SELECT distinct nokategori id, kategori name FROM MstkategorikomponenGaji");
                        cbokomponengaji.DataValueField = "id";
                        cbokomponengaji.DataTextField = "name";
                        cbokomponengaji.DataBind();

                        txtjenisIuran.Text = dt.Rows[i]["txtjenisIuran"].ToString();
                        txtpersenperusahaan.Text = dt.Rows[i]["txtpersenperusahaan"].ToString();
                        txtpersenkaryawan.Text = dt.Rows[i]["txtpersenkaryawan"].ToString();
                        cbopph21.Text = dt.Rows[i]["cbopph21"].ToString();
                        cboKategori.Text = dt.Rows[i]["cboKategori"].ToString();
                        cbokomponengaji.Text = dt.Rows[i]["cbokomponengaji"].ToString();
                        cbnorek.Text = dt.Rows[i]["cbnorek"].ToString();
                        cbnorekkd.Text = dt.Rows[i]["cbnorekkd"].ToString();



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

                        TextBox txtjenisIuran = (TextBox)grdkomponengaji.Rows[i].FindControl("txtjenisIuran");
                        TextBox txtpersenperusahaan = (TextBox)grdkomponengaji.Rows[i].FindControl("txtpersenperusahaan");
                        TextBox txtpersenkaryawan = (TextBox)grdkomponengaji.Rows[i].FindControl("txtpersenkaryawan");
                        DropDownList cbopph21 = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbopph21");
                        DropDownList cboKategori = (DropDownList)grdkomponengaji.Rows[i].FindControl("cboKategori");
                        DropDownList cbokomponengaji = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbokomponengaji");
                        DropDownList cbnorek = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorek");
                        DropDownList cbnorekkd = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorekkd");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["txtjenisIuran"] = txtjenisIuran.Text;
                        dtCurrentTable.Rows[i]["txtpersenperusahaan"] = txtpersenperusahaan.Text;
                        dtCurrentTable.Rows[i]["txtpersenkaryawan"] = txtpersenkaryawan.Text;
                        dtCurrentTable.Rows[i]["cbopph21"] = cbopph21.Text;
                        dtCurrentTable.Rows[i]["cboKategori"] = cboKategori.Text;
                        dtCurrentTable.Rows[i]["cbokomponengaji"] = cbokomponengaji.Text;
                        dtCurrentTable.Rows[i]["cbnorek"] = cbnorek.Text;
                        dtCurrentTable.Rows[i]["cbnorekkd"] = cbnorekkd.Text;

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
                    TextBox txtjenisIuran = (TextBox)grdkomponengaji.Rows[i].FindControl("txtjenisIuran");
                    TextBox txtpersenperusahaan = (TextBox)grdkomponengaji.Rows[i].FindControl("txtpersenperusahaan");
                    TextBox txtpersenkaryawan = (TextBox)grdkomponengaji.Rows[i].FindControl("txtpersenkaryawan");
                    DropDownList cbopph21 = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbopph21");
                    DropDownList cboKategori = (DropDownList)grdkomponengaji.Rows[i].FindControl("cboKategori");
                    DropDownList cbokomponengaji = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbokomponengaji");
                    DropDownList cbnorek = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorek");
                    DropDownList cbnorekkd = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorekkd");

                    DataSet mySet = ObjDb.GetRows("Select * from MstBPJS where komponengaji= '" + txtjenisIuran.Text + "'");
                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + txtjenisIuran.Text + "</b> gagal disimpan,  komponengaji sudah terdaftar sebelumnya.");
                        count2 += 1;
                    }
                    else
                    {
                        if (txtjenisIuran.Text != "")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("BPJS", txtjenisIuran.Text);
                            ObjDb.Data.Add("persenperusahan", Convert.ToDecimal(txtpersenperusahaan.Text).ToString());
                            ObjDb.Data.Add("persenkaryawan", Convert.ToDecimal(txtpersenkaryawan.Text).ToString());
                            ObjDb.Data.Add("pph21", cbopph21.Text);
                            ObjDb.Data.Add("kategori", cboKategori.Text);
                            ObjDb.Data.Add("nokomponengaji", cbokomponengaji.Text);
                            ObjDb.Data.Add("norek", cbnorek.Text);
                            ObjDb.Data.Add("norekkd", cbnorekkd.Text);
                            ObjDb.Data.Add("sts", "1");
                            ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                            ObjDb.Insert("MstBPJS", ObjDb.Data);
                            message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + txtjenisIuran.Text + "</b> data tersimpan.");
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