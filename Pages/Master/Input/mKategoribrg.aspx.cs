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
    public partial class mKategoribrg : System.Web.UI.Page
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
            dt.Columns.Add(new DataColumn("cbnorek", typeof(string)));
            dt.Columns.Add(new DataColumn("cborekkd", typeof(string)));
            dt.Columns.Add(new DataColumn("cborekdb", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["cbnorek"] = "0";
            dr["cborekkd"] = "0";
            dr["cborekdb"] = "0";

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdKategoriBrg.DataSource = dt;
            grdKategoriBrg.DataBind();

            SetPreviousData();
        }
        private void SetInitialRow2()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtInstansi2", typeof(string)));
            dt.Columns.Add(new DataColumn("cboPendapatan", typeof(string)));
            dt.Columns.Add(new DataColumn("cboPiutang", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["txtInstansi2"] = string.Empty;
            dr["cboPendapatan"] = "0";
            dr["cboPiutang"] = "0";

            dt.Rows.Add(dr);
            ViewState["CurrentTable2"] = dt;
            grdKategoriBrg2.DataSource = dt;
            grdKategoriBrg2.DataBind();

            SetPreviousData2();
        }
        private void SetInitialRow3()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("cboBiaya", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column3"] = string.Empty;
            dr["cboBiaya"] = "0";

            dt.Rows.Add(dr);
            ViewState["CurrentTable3"] = dt;
            grdKategoriBrg3.DataSource = dt;
            grdKategoriBrg3.DataBind();

            SetPreviousData3();
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
                        TextBox txtInstansi = (TextBox)grdKategoriBrg.Rows[i].FindControl("txtInstansi");
                        DropDownList cbnorek = (DropDownList)grdKategoriBrg.Rows[i].FindControl("cbnorek");
                        DropDownList cborekkd = (DropDownList)grdKategoriBrg.Rows[i].FindControl("cborekkd");
                        DropDownList cborekdb = (DropDownList)grdKategoriBrg.Rows[i].FindControl("cborekdb");

                        txtInstansi.Text = dt.Rows[i]["Column1"].ToString();
                        cbnorek.Text = dt.Rows[i]["cbnorek"].ToString();
                        cborekkd.Text = dt.Rows[i]["cborekkd"].ToString();
                        cborekdb.Text = dt.Rows[i]["cborekdb"].ToString();

                    }
                }
            }
        }
        private void SetPreviousData2()
        {
            if (ViewState["CurrentTable2"] != null)
            {
                DataTable dt2 = (DataTable)ViewState["CurrentTable2"];
                if (dt2.Rows.Count > 0)
                {
                    for (int ii = 0; ii < dt2.Rows.Count; ii++)
                    {
                        TextBox txtInstansi2 = (TextBox)grdKategoriBrg2.Rows[ii].FindControl("txtInstansi2");
                        DropDownList cboPendapatan = (DropDownList)grdKategoriBrg2.Rows[ii].FindControl("cboPendapatan");
                        DropDownList cboPiutang = (DropDownList)grdKategoriBrg2.Rows[ii].FindControl("cboPiutang");

                        txtInstansi2.Text = dt2.Rows[ii]["txtInstansi2"].ToString();
                        cboPendapatan.Text = dt2.Rows[ii]["cboPendapatan"].ToString();
                        cboPiutang.Text = dt2.Rows[ii]["cboPiutang"].ToString();
                        
                    }
                }
            }
        }
        private void SetPreviousData3()
        {
            if (ViewState["CurrentTable3"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable3"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtInstansi3 = (TextBox)grdKategoriBrg3.Rows[i].FindControl("txtInstansi3");
                        DropDownList cboBiaya = (DropDownList)grdKategoriBrg3.Rows[i].FindControl("cboBiaya");

                        txtInstansi3.Text = dt.Rows[i]["Column3"].ToString();
                        cboBiaya.Text = dt.Rows[i]["cboBiaya"].ToString();

                    }
                }
            }
        }
        protected void LoadRekDetil(string jsnBarang = "")
        {
            for (int i = 0; i < grdKategoriBrg.Rows.Count; i++)
            {
                DropDownList cbnorek = (DropDownList)grdKategoriBrg.Rows[i].FindControl("cbnorek");
                DropDownList cborekdb = (DropDownList)grdKategoriBrg.Rows[i].FindControl("cborekdb");
                DropDownList cborekkd = (DropDownList)grdKategoriBrg.Rows[i].FindControl("cborekkd");

                cborekdb.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih COA (Debit)---' name union all SELECT distinct norek id,ket  name FROM mRekening where jenis=8) a");
                cborekdb.DataValueField = "id";
                cborekdb.DataTextField = "name";
                cborekdb.DataBind();

                cborekkd.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih COA (Kredit)---' name union all SELECT distinct norek id,ket  name FROM mRekening where jenis=7 ) a");
                cborekkd.DataValueField = "id";
                cborekkd.DataTextField = "name";
                cborekkd.DataBind();

                if (jsnBarang == "1" || jsnBarang == "4")
                {
                    cbnorek.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih COA--' name union all SELECT distinct norek as id, ket as name FROM mrekening where Grup='PosisiKeuangan' and pos='1' and jenis='9'");
                    cbnorek.DataValueField = "id";
                    cbnorek.DataTextField = "name";
                    cbnorek.DataBind();
                }
                else if (jsnBarang == "2")
                {
                    cbnorek.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih COA--' name union all SELECT distinct norek as id, ket as name FROM mrekening where grup = 'Aktivitas' and pos='1' and sts='2' and Kelompok='PERUBAHAN ASSET NETTO TIDAK TERIKAT'");
                    cbnorek.DataValueField = "id";
                    cbnorek.DataTextField = "name";
                    cbnorek.DataBind();
                }
                else if (jsnBarang == "3")
                {
                    cbnorek.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih COA--' name union all SELECT distinct norek as id, ket as name FROM mrekening where grup = 'Aktivitas' and pos='1' and sts='2' and jenis='20' and Kelompok='PERUBAHAN ASSET NETTO TIDAK TERIKAT'");
                    cbnorek.DataValueField = "id";
                    cbnorek.DataTextField = "name";
                    cbnorek.DataBind();
                }
                else if (jsnBarang == "5")
                {
                    cbnorek.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih COA--' name union all SELECT distinct norek as id, ket as name FROM mrekening where Grup='PosisiKeuangan' and pos='1' and jenis='30'");
                    cbnorek.DataValueField = "id";
                    cbnorek.DataTextField = "name";
                    cbnorek.DataBind();
                }
                else
                {
                    cbnorek.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih COA--' name ");
                    cbnorek.DataValueField = "id";
                    cbnorek.DataTextField = "name";
                    cbnorek.DataBind();
                }
            }
            //sales
            for (int ii = 0; ii < grdKategoriBrg2.Rows.Count; ii++)
            {
                DropDownList cboPendapatan = (DropDownList)grdKategoriBrg2.Rows[ii].FindControl("cboPendapatan");
                DropDownList cboPiutang = (DropDownList)grdKategoriBrg2.Rows[ii].FindControl("cboPiutang");

                cboPendapatan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih COA (Debit)---' name union all SELECT distinct norek id,ket  name FROM mRekening) a");
                cboPendapatan.DataValueField = "id";
                cboPendapatan.DataTextField = "name";
                cboPendapatan.DataBind();

                cboPiutang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih COA (Kredit)---' name union all SELECT distinct norek id,ket  name FROM mRekening) a");
                cboPiutang.DataValueField = "id";
                cboPiutang.DataTextField = "name";
                cboPiutang.DataBind();

               
            }
            for (int iii = 0; iii< grdKategoriBrg3.Rows.Count; iii++)
            {
                DropDownList cboBiaya = (DropDownList)grdKategoriBrg3.Rows[iii].FindControl("cboBiaya");

                cboBiaya.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih COA (Debit)---' name union all SELECT distinct norek id,ket  name FROM mRekening) a");
                cboBiaya.DataValueField = "id";
                cboBiaya.DataTextField = "name";
                cboBiaya.DataBind();

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
                        TextBox txtInstansi = (TextBox)grdKategoriBrg.Rows[i].FindControl("txtInstansi");
                        DropDownList cbnorek = (DropDownList)grdKategoriBrg.Rows[i].FindControl("cbnorek");
                        DropDownList cborekkd = (DropDownList)grdKategoriBrg.Rows[i].FindControl("cborekkd");
                        DropDownList cborekdb = (DropDownList)grdKategoriBrg.Rows[i].FindControl("cborekdb");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtInstansi.Text;
                        dtCurrentTable.Rows[i]["cbnorek"] = cbnorek.Text;
                        dtCurrentTable.Rows[i]["cborekkd"] = cborekkd.Text;
                        dtCurrentTable.Rows[i]["cborekdb"] = cborekdb.Text;

                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdKategoriBrg.DataSource = dtCurrentTable;
                    grdKategoriBrg.DataBind();
                }
            }
            SetPreviousData();
        }
        private void AddNewRow2()
        {
            if (ViewState["CurrentTable2"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable2"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtInstansi2 = (TextBox)grdKategoriBrg2.Rows[i].FindControl("txtInstansi2");
                        DropDownList cboPendapatan = (DropDownList)grdKategoriBrg2.Rows[i].FindControl("cboPendapatan");
                        DropDownList cboPiutang = (DropDownList)grdKategoriBrg2.Rows[i].FindControl("cboPiutang");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["txtInstansi2"] = txtInstansi2.Text;
                        dtCurrentTable.Rows[i]["cboPendapatan"] = cboPendapatan.Text;
                        dtCurrentTable.Rows[i]["cboPiutang"] = cboPiutang.Text;

                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable2"] = dtCurrentTable;
                    grdKategoriBrg2.DataSource = dtCurrentTable;
                    grdKategoriBrg2.DataBind();
                }
            }
            SetPreviousData2();
        }
        private void AddNewRow3()
        {
            if (ViewState["CurrentTable3"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable3"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtInstansi3 = (TextBox)grdKategoriBrg3.Rows[i].FindControl("txtInstansi3");
                        DropDownList cboBiaya = (DropDownList)grdKategoriBrg3.Rows[i].FindControl("cboBiaya");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column3"] = txtInstansi3.Text;
                        dtCurrentTable.Rows[i]["cboBiaya"] = cboBiaya.Text;

                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable3"] = dtCurrentTable;
                    grdKategoriBrg3.DataSource = dtCurrentTable;
                    grdKategoriBrg3.DataBind();
                }
            }
            SetPreviousData3();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("mKategoribrg.aspx");
                SetInitialRow();
                SetInitialRow2();
                SetInitialRow3();
                for (int i = 1; i < 5; i++)
                {
                    AddNewRow();
                }
                for (int i = 1; i < 5; i++)
                {
                    AddNewRow2();
                }
                for (int i = 1; i < 5; i++)
                {
                    AddNewRow3();
                }

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
            AddNewRow2();
            AddNewRow3();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();
            int count = 0, count2 = 0;

            if (cbojnsBarang.Text == "0")
            {
                message += ObjSys.CreateMessage("Kategori tidak boleh kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                    if(cbojnsBarang.SelectedValue == "1")
                    {
                        for (int i = 0; i < grdKategoriBrg.Rows.Count; i++)
                        {
                            TextBox txtInstansi = (TextBox)grdKategoriBrg.Rows[i].FindControl("txtInstansi");
                            DropDownList cbnorek = (DropDownList)grdKategoriBrg.Rows[i].FindControl("cbnorek");
                            DropDownList cborekkd = (DropDownList)grdKategoriBrg.Rows[i].FindControl("cborekkd");
                            DropDownList cborekdb = (DropDownList)grdKategoriBrg.Rows[i].FindControl("cborekdb");

                            if (ObjDb.GetRows("SELECT * FROM mKategori WHERE Kategori = '" + txtInstansi.Text + "'").Tables[0].Rows.Count > 0)
                            {
                                message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + txtInstansi.Text + "</b> tidak tersimpan karena nama Kategori sudah terdaftar sebelumnya.");
                                count2 += 1;
                            }
                            else
                            {

                                if (txtInstansi.Text != "")
                                {
                                    ObjDb.Data.Clear();
                                    ObjDb.Data.Add("jns", cbojnsBarang.Text);
                                    ObjDb.Data.Add("Kategori", txtInstansi.Text);
                                    ObjDb.Data.Add("sts", "1");
                                    ObjDb.Data.Add("norek", cbnorek.Text);
                                    ObjDb.Data.Add("norekkd", cborekkd.Text);
                                    ObjDb.Data.Add("norekdb", cborekdb.Text);
                                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                    ObjDb.Insert("mKategori", ObjDb.Data);
                                    message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + txtInstansi.Text + "</b> data tersimpan.");
                                    count += 1;

                                }
                            }

                        }
                    }else if (cbojnsBarang.SelectedValue == "5")
                    {
                        for (int ii = 0; ii < grdKategoriBrg2.Rows.Count; ii++)
                        {
                            TextBox txtInstansi2 = (TextBox)grdKategoriBrg2.Rows[ii].FindControl("txtInstansi2");
                            DropDownList cboPendapatan = (DropDownList)grdKategoriBrg2.Rows[ii].FindControl("cboPendapatan");
                            DropDownList cboPiutang = (DropDownList)grdKategoriBrg2.Rows[ii].FindControl("cboPiutang");

                            if (ObjDb.GetRows("SELECT * FROM mKategori WHERE Kategori = '" + txtInstansi2.Text + "'").Tables[0].Rows.Count > 0)
                            {
                                message += ObjSys.CreateMessage("Baris " + (ii + 1) + " <b>" + txtInstansi2.Text + "</b> tidak tersimpan karena nama Kategori sudah terdaftar sebelumnya.");
                                count2 += 1;
                            }
                            else
                            {

                                if (txtInstansi2.Text != "")
                                {
                                    ObjDb.Data.Clear();
                                    ObjDb.Data.Add("jns", cbojnsBarang.Text);
                                    ObjDb.Data.Add("Kategori", txtInstansi2.Text);
                                    ObjDb.Data.Add("sts", "1");
                                    ObjDb.Data.Add("norek", cboPendapatan.Text);
                                    ObjDb.Data.Add("norekdb", cboPiutang.Text);
                                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                    ObjDb.Insert("mKategori", ObjDb.Data);
                                    message += ObjSys.CreateMessage("Baris " + (ii + 1) + " <b>" + txtInstansi2.Text + "</b> data tersimpan.");
                                    count += 1;

                                }
                            }

                        }
                    }else
                    {
                        for (int x = 0; x < grdKategoriBrg3.Rows.Count; x++)
                        {
                            TextBox txtInstansi3 = (TextBox)grdKategoriBrg3.Rows[x].FindControl("txtInstansi3");
                            DropDownList cboBiaya = (DropDownList)grdKategoriBrg3.Rows[x].FindControl("cboBiaya");

                            if (ObjDb.GetRows("SELECT * FROM mKategori WHERE Kategori = '" + txtInstansi3.Text + "'").Tables[0].Rows.Count > 0)
                            {
                                message += ObjSys.CreateMessage("Baris " + (x + 1) + " <b>" + txtInstansi3.Text + "</b> tidak tersimpan karena nama Kategori sudah terdaftar sebelumnya.");
                                count2 += 1;
                            }
                            else
                            {

                                if (txtInstansi3.Text != "")
                                {
                                    ObjDb.Data.Clear();
                                    ObjDb.Data.Add("jns", cbojnsBarang.Text);
                                    ObjDb.Data.Add("Kategori", txtInstansi3.Text);
                                    ObjDb.Data.Add("sts", "1");
                                    ObjDb.Data.Add("norek", cboBiaya.Text);
                                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                    ObjDb.Insert("mKategori", ObjDb.Data);
                                    message += ObjSys.CreateMessage("Baris " + (x + 1) + " <b>" + txtInstansi3.Text + "</b> data tersimpan.");
                                    count += 1;

                                }
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
            cbojnsBarang.Text = "0";
            SetInitialRow();
            SetInitialRow2();
            SetInitialRow3();
            for (int i = 1; i < 5; i++)
            {
                AddNewRow();
            }
            for (int i = 1; i < 5; i++)
            {
                AddNewRow2();
            }
            for (int i = 1; i < 5; i++)
            {
                AddNewRow3();
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearData();
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


        protected void cbojnsBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbojnsBarang.SelectedValue == "0")
            {
                kategoriBrg1.Visible = false;
                kategoriBrg2.Visible = false;
                kategoriBrg3.Visible = false;
            }else if (cbojnsBarang.SelectedValue == "1")
            {
                kategoriBrg1.Visible = true;
                kategoriBrg2.Visible = false;
                kategoriBrg3.Visible = false;
            }
            else if (cbojnsBarang.SelectedValue == "5")
            {
                kategoriBrg1.Visible = false;
                kategoriBrg2.Visible = true;
                kategoriBrg3.Visible = false;
            }else
            {
                kategoriBrg1.Visible = false;
                kategoriBrg2.Visible = false;
                kategoriBrg3.Visible = true;
            }
            CloseMessage();
            LoadRekDetil(cbojnsBarang.Text);
        }
    }

}

