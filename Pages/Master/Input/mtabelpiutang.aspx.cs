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
    public partial class mtabelpiutang : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("mtabelpiutang.aspx");
                LoadData();
                SetInitialRow();
                for (int i = 1; i <= 4; i++)
                {
                    AddNewRow();

                }

            }
        }
        protected void LoadData()
        {

        }



        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtkomponengaji", typeof(string)));
            dt.Columns.Add(new DataColumn("cbnorek", typeof(string)));
            dt.Columns.Add(new DataColumn("cbnorek1", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["txtkomponengaji"] = string.Empty;
            dr["cbnorek"] = "0";
            dr["cbnorek1"] = "0";


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
                        DropDownList cbnorek = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorek");
                        DropDownList cbnorek1 = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorek1");

                        cbnorek.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih COA--' name union all SELECT distinct norek as id, ket as name FROM mrekening where grup = 'PosisiKeuangan' and pos='1' and sts='2' and jenis='11' ");
                        cbnorek.DataValueField = "id";
                        cbnorek.DataTextField = "name";
                        cbnorek.DataBind();

                        cbnorek1.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih COA--' name union all SELECT distinct norek as id, ket as name FROM mrekening where grup = 'Aktivitas' and pos='2' and sts='2'");
                        cbnorek1.DataValueField = "id";
                        cbnorek1.DataTextField = "name";
                        cbnorek1.DataBind();

                        txtkomponengaji.Text = dt.Rows[i]["txtkomponengaji"].ToString();
                        cbnorek.Text = dt.Rows[i]["cbnorek"].ToString();
                        cbnorek1.Text = dt.Rows[i]["cbnorek1"].ToString();



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
                        DropDownList cbnorek = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorek");
                        DropDownList cbnorek1 = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorek1");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["txtkomponengaji"] = txtkomponengaji.Text;
                        dtCurrentTable.Rows[i]["cbnorek"] = cbnorek.Text;
                        dtCurrentTable.Rows[i]["cbnorek1"] = cbnorek.Text;

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
                    DropDownList cbnorek = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorek");
                    DropDownList cbnorek1 = (DropDownList)grdkomponengaji.Rows[i].FindControl("cbnorek1");
                    DataSet mySet = ObjDb.GetRows("Select * from mTabelhutang where hutang= '" + txtkomponengaji.Text + "'");
                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + txtkomponengaji.Text + "</b> gagal disimpan,  Hutang sudah terdaftar sebelumnya.");
                        count2 += 1;
                    }
                    else
                    {
                        if (txtkomponengaji.Text != "")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("hutang", txtkomponengaji.Text);
                            ObjDb.Data.Add("norek", cbnorek.Text);
                            ObjDb.Data.Add("norekkredit", cbnorek1.Text);
                            ObjDb.Data.Add("sts", "1");
                            ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                            ObjDb.Insert("mTabelhutang", ObjDb.Data);
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