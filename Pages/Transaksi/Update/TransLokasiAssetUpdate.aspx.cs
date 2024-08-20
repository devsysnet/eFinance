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
    public partial class TransLokasiAssetUpdate : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataFirstCombo();
                loadDataFirst(ObjSys.GetCabangId, cboLokasi.Text, cboSubLokasi.Text, txtSearch.Text);
            }
            
        }

        protected void loadDataFirst(string cabang = "0", string lokasi = "0", string sublokasi = "0", string cari = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Cabang", cabang);
            ObjGlobal.Param.Add("noLokasi", lokasi);
            ObjGlobal.Param.Add("noSublokasi", sublokasi);
            ObjGlobal.Param.Add("cari", cari);
            grdLokasiAsset.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPenerimaanAsset", ObjGlobal.Param);
            grdLokasiAsset.DataBind();

            if (grdLokasiAsset.Rows.Count > 0)
                button.Visible = true;
            else
                button.Visible = false;

            loadDataCombo();
        }

        protected void loadDataFirstCombo()
        {
            cboLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Semua Lokasi---' name union all SELECT distinct noLokasi id, Lokasi name FROM mLokasi where nocabang='" + ObjSys.GetCabangId + "' ) a");
            cboLokasi.DataValueField = "id";
            cboLokasi.DataTextField = "name";
            cboLokasi.DataBind();

            cboSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Semua Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi where noLokasi = '" + cboLokasi.Text + "') a");
            cboSubLokasi.DataValueField = "id";
            cboSubLokasi.DataTextField = "name";
            cboSubLokasi.DataBind();

            loadDataFirst(ObjSys.GetCabangId, cboLokasi.Text, cboSubLokasi.Text, txtSearch.Text);

        }

        protected void loadDataCombo()
        {
            
            for (int i = 0; i < grdLokasiAsset.Rows.Count; i++)
            {
                DropDownList cboLokasiBaru = (DropDownList)grdLokasiAsset.Rows[i].FindControl("cboLokasiBaru");
                DropDownList cboSubLokasiBaru = (DropDownList)grdLokasiAsset.Rows[i].FindControl("cboSubLokasiBaru");
                DropDownList cborekdb = (DropDownList)grdLokasiAsset.Rows[i].FindControl("cborekdb");
                DropDownList cborekkd = (DropDownList)grdLokasiAsset.Rows[i].FindControl("cborekkd");

                HiddenField hdnnoLokasi = (HiddenField)grdLokasiAsset.Rows[i].FindControl("hdnnoLokasi");
                HiddenField hdnnoSubLokasi = (HiddenField)grdLokasiAsset.Rows[i].FindControl("hdnnoSubLokasi");
                HiddenField hdnrekDb = (HiddenField)grdLokasiAsset.Rows[i].FindControl("hdnrekDb");
                HiddenField hdnrekKd = (HiddenField)grdLokasiAsset.Rows[i].FindControl("hdnrekKd");

                cboLokasiBaru.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Lokasi---' name union all SELECT distinct noLokasi id, Lokasi name FROM mLokasi where nocabang='" + ObjSys.GetCabangId + "' ) a");
                cboLokasiBaru.DataValueField = "id";
                cboLokasiBaru.DataTextField = "name";
                cboLokasiBaru.DataBind();

                cboLokasiBaru.SelectedValue = hdnnoLokasi.Value;

                cboSubLokasiBaru.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi where noLokasi = '" + hdnnoLokasi.Value + "') a");
                cboSubLokasiBaru.DataValueField = "id";
                cboSubLokasiBaru.DataTextField = "name";
                cboSubLokasiBaru.DataBind();

                cboSubLokasiBaru.SelectedValue = hdnnoSubLokasi.Value;

                //cborekdb.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih COA (Debit)---' name union all SELECT distinct norek id,ket  name FROM mRekening where jenis=8) a");
                //cborekdb.DataValueField = "id";
                //cborekdb.DataTextField = "name";
                //cborekdb.DataBind();

                //cborekdb.SelectedValue = hdnrekDb.Value;

                //cborekkd.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih COA (Kredit)---' name union all SELECT distinct norek id,ket  name FROM mRekening where jenis=7 ) a");
                //cborekkd.DataValueField = "id";
                //cborekkd.DataTextField = "name";
                //cborekkd.DataBind();

                //cborekkd.SelectedValue = hdnrekKd.Value;
            }
                
        }

        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {

            CloseMessage();

            loadDataFirst(ObjSys.GetCabangId, cboLokasi.Text, cboSubLokasi.Text, txtSearch.Text);

            cboSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Semua Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi where noLokasi = '" + cboLokasi.Text + "') a");
            cboSubLokasi.DataValueField = "id";
            cboSubLokasi.DataTextField = "name";
            cboSubLokasi.DataBind();


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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            int count = 0;
            for (int i = 0; i < grdLokasiAsset.Rows.Count; i++)
            {
                CheckBox chkCheck = (CheckBox)grdLokasiAsset.Rows[i].FindControl("chkCheck");
                if (chkCheck.Checked == true)
                    count++;
            }

            if (count == 0)
            {
                message += ObjSys.CreateMessage("Data harus dipilih.");
                valid = false;
            }
            if (valid == true && count > 0)
            {
                try
                {
                    for (int i = 0; i < grdLokasiAsset.Rows.Count; i++)
                    {

                        HiddenField hdnNoAsset = (HiddenField)grdLokasiAsset.Rows[i].FindControl("hdnNoAsset");
                        HiddenField hdntglAsset = (HiddenField)grdLokasiAsset.Rows[i].FindControl("hdntglAsset");
                        HiddenField hdnnoCabang = (HiddenField)grdLokasiAsset.Rows[i].FindControl("hdnnoCabang");
                        HiddenField hdnnoBarang = (HiddenField)grdLokasiAsset.Rows[i].FindControl("hdnnoBarang");
                        HiddenField hdnDanaBOS = (HiddenField)grdLokasiAsset.Rows[i].FindControl("hdnDanaBOS");

                        TextBox txtNamaAsset = (TextBox)grdLokasiAsset.Rows[i].FindControl("txtNamaAsset");
                        DropDownList cboLokasiBaru = (DropDownList)grdLokasiAsset.Rows[i].FindControl("cboLokasiBaru");
                        DropDownList cboSubLokasiBaru = (DropDownList)grdLokasiAsset.Rows[i].FindControl("cboSubLokasiBaru");
                        //DropDownList cborekdb = (DropDownList)grdLokasiAsset.Rows[i].FindControl("cborekdb");
                        //DropDownList cborekkd = (DropDownList)grdLokasiAsset.Rows[i].FindControl("cborekkd");
                        CheckBox chkCheck = (CheckBox)grdLokasiAsset.Rows[i].FindControl("chkCheck");


                        if (chkCheck.Checked == true)
                        {
                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("noAset", hdnNoAsset.Value);
                            ObjGlobal.Param.Add("tglAsset", Convert.ToDateTime(hdntglAsset.Value).ToString("dd-MMM-yyyy"));
                            ObjGlobal.Param.Add("noCabang", hdnnoCabang.Value);
                            ObjGlobal.Param.Add("noBarangAsset", hdnnoBarang.Value);
                            ObjGlobal.Param.Add("namaAsset", txtNamaAsset.Text);
                            ObjGlobal.Param.Add("nolokasi", cboLokasiBaru.Text);
                            ObjGlobal.Param.Add("noSubLokasi", cboSubLokasiBaru.Text);
                            //ObjGlobal.Param.Add("norekdb", cborekdb.Text);
                            //ObjGlobal.Param.Add("norekkd", cborekkd.Text);
                            ObjGlobal.Param.Add("danaBOS", hdnDanaBOS.Value);
                            ObjGlobal.Param.Add("modiBy", ObjSys.GetUserId);
                            ObjGlobal.GetDataProcedure("SPUpdateTransAssetMulti", ObjGlobal.Param);
                        }
                    }
                    loadDataFirst();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                    clearData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.Message);
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
            CloseMessage();
            clearData();
        }

        protected void clearData()
        {
            cboLokasi.Text = "0";
            cboSubLokasi.Text = "0";
            txtSearch.Text = "";
            loadDataFirst(ObjSys.GetCabangId, cboLokasi.Text, cboSubLokasi.Text, txtSearch.Text);
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadDataFirst(ObjSys.GetCabangId, cboLokasi.Text, cboSubLokasi.Text, txtSearch.Text);
        }

        protected void cboSubLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataFirst(ObjSys.GetCabangId, cboLokasi.Text, cboSubLokasi.Text, txtSearch.Text);

        }

        protected void cboLokasiBaru_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboLokasiBaru = (DropDownList)sender;
            var row = (GridViewRow)cboLokasiBaru.NamingContainer;

            DropDownList cboSubLokasiBaru = (DropDownList)row.FindControl("cboSubLokasiBaru");
            cboSubLokasiBaru.DataSource = ObjDb.GetRows("select a.* from (select '0' id, '---Pilih Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi where noLokasi = '" + cboLokasiBaru.Text + "') a");
            cboSubLokasiBaru.DataValueField = "id";
            cboSubLokasiBaru.DataTextField = "name";
            cboSubLokasiBaru.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            int count = 0;
            for (int i = 0; i < grdLokasiAsset.Rows.Count; i++)
            {
                CheckBox chkCheck = (CheckBox)grdLokasiAsset.Rows[i].FindControl("chkCheck");
                if (chkCheck.Checked == true)
                    count++;
            }

            if (count == 0)
            {
                message += ObjSys.CreateMessage("Data harus dipilih.");
                valid = false;
            }
            if (valid == true && count > 0)
            {
                try
                {
                    for (int i = 0; i < grdLokasiAsset.Rows.Count; i++)
                    {

                        HiddenField hdnNoAsset = (HiddenField)grdLokasiAsset.Rows[i].FindControl("hdnNoAsset");
                        CheckBox chkCheck = (CheckBox)grdLokasiAsset.Rows[i].FindControl("chkCheck");


                        if (chkCheck.Checked == true)
                        {
                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("noAset", hdnNoAsset.Value);
                            ObjGlobal.Param.Add("deletedBy", ObjSys.GetUserId);
                            ObjGlobal.GetDataProcedure("SPDeleteTransAssetMulti", ObjGlobal.Param);
                        }
                    }
                    loadDataFirst();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus.");
                    clearData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.Message);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }
    }
}