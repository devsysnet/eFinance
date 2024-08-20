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

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransLokasiAsset : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataFirstCombo();
                loadDataFirst();
            }
            
        }

        protected void loadDataFirst()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Cabang", ObjSys.GetCabangId);
            grdLokasiAsset.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAsset", ObjGlobal.Param);
            grdLokasiAsset.DataBind();
            if (grdLokasiAsset.Rows.Count > 0)
                button.Visible = true;
            else
                button.Visible = false;

            loadDataCombo();
        }

        protected void loadDataFirstCombo()
        {
            cboLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Lokasi---' name union all SELECT distinct noLokasi id, Lokasi name FROM mLokasi where nocabang='" + ObjSys.GetCabangId + "' ) a");
            cboLokasi.DataValueField = "id";
            cboLokasi.DataTextField = "name";
            cboLokasi.DataBind();

            cboSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi where noLokasi = '" + cboLokasi.Text + "') a");
            cboSubLokasi.DataValueField = "id";
            cboSubLokasi.DataTextField = "name";
            cboSubLokasi.DataBind();
        }

        protected void loadDataCombo()
        {
            
            for (int i = 0; i < grdLokasiAsset.Rows.Count; i++)
            {
                //DropDownList cborekdb = (DropDownList)grdLokasiAsset.Rows[i].FindControl("cborekdb");
                //DropDownList cborekkd = (DropDownList)grdLokasiAsset.Rows[i].FindControl("cborekkd");

                //cborekdb.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih COA (Debit)---' name union all SELECT distinct norek id,ket  name FROM mRekening where jenis=8) a");
                //cborekdb.DataValueField = "id";
                //cborekdb.DataTextField = "name";
                //cborekdb.DataBind();

                //cborekkd.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih COA (Kredit)---' name union all SELECT distinct norek id,ket  name FROM mRekening where jenis=7 ) a");
                //cborekkd.DataValueField = "id";
                //cborekkd.DataTextField = "name";
                //cborekkd.DataBind();
            }
                
        }

        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi where noLokasi = '" + cboLokasi.Text + "') a");
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

            if (cboLokasi.Text == "0")
            {
                message += ObjSys.CreateMessage("Lokasi harus dipilih.");
                valid = false;
            }
            if (cboSubLokasi.Text == "0")
            {
                message += ObjSys.CreateMessage("SubLokasi harus dipilih.");
                valid = false;
            }
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
                        DropDownList cborekdb = (DropDownList)grdLokasiAsset.Rows[i].FindControl("cborekdb");
                        DropDownList cborekkd = (DropDownList)grdLokasiAsset.Rows[i].FindControl("cborekkd");
                        CheckBox chkCheck = (CheckBox)grdLokasiAsset.Rows[i].FindControl("chkCheck");


                        if (chkCheck.Checked == true)
                        {

                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("noAset", hdnNoAsset.Value);
                            ObjGlobal.Param.Add("tglAsset", Convert.ToDateTime(hdntglAsset.Value).ToString("dd-MMM-yyyy"));
                            ObjGlobal.Param.Add("noCabang", hdnnoCabang.Value);
                            ObjGlobal.Param.Add("noBarangAsset", hdnnoBarang.Value);
                            ObjGlobal.Param.Add("namaAsset", txtNamaAsset.Text);
                            ObjGlobal.Param.Add("nolokasi", cboLokasi.Text);
                            ObjGlobal.Param.Add("noSubLokasi", cboSubLokasi.Text);
                            ObjGlobal.Param.Add("danaBOS", hdnDanaBOS.Value);
                            ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                            ObjGlobal.GetDataProcedure("SPInsertTransAssetMulti", ObjGlobal.Param);
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
            loadDataFirst();
        }
    }
}