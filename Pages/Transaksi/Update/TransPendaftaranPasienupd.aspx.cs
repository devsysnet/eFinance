
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransPendaftaranPasienupd : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                dtMulai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                //dtSampai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                loadData();
                loadDataCombo1();

            }
        }
        protected void btnBrowsePasien_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataPasien();
            dlgAddData.Show();
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("tgl", dtMulai.Text);
            grdReceiveUpdate.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPendaftaranPasienUpdate", ObjGlobal.Param);
            grdReceiveUpdate.DataBind();

        }

        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {

            cbodokter.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Dokter---' name union all SELECT distinct nokaryawan id, nama name FROM MstKaryawan where dept = '" + cboDept.Text + "') a");
            cbodokter.DataValueField = "id";
            cbodokter.DataTextField = "name";
            cbodokter.DataBind();

        }

        protected void LoadDataPasien()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearchMinta.Text);

            grdDataPasien.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPasien", ObjGlobal.Param);
            grdDataPasien.DataBind();
        }

        private void loadDataCombo()
        {
            cboDept.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Klinik---' name union all SELECT distinct noDepartemen id, departemen name FROM Mstdepartemen where transaksi ='1') a");
            cboDept.DataValueField = "id";
            cboDept.DataTextField = "name";
            cboDept.DataBind();
        }


        protected void loadDataCombo1()
        {

            cbodokter.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Dokter---' name union all SELECT distinct nokaryawan id, nama name FROM MstKaryawan ) a");
            cbodokter.DataValueField = "id";
            cbodokter.DataTextField = "name";
            cbodokter.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void grdReceiveUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdReceiveUpdate.PageIndex = e.NewPageIndex;
            loadData();
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

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            showHideFormKas(true, false);
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";
            try
            {
                if (valid == true)
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noMedik", hdnId.Value);
                    ObjGlobal.Param.Add("tglPeriksa",dtDate.Text);
                    ObjGlobal.Param.Add("noPasien", hdnNopasien.Value);
                    ObjGlobal.Param.Add("klinik", cboDept.Text);
                    ObjGlobal.Param.Add("dokter", cbodokter.Text);
                    ObjGlobal.Param.Add("uraian", txtKeterangan.Text);
                    ObjGlobal.Param.Add("updatedBy", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPUpdateTransPendaftaranPasien", ObjGlobal.Param);



                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data Berhasil Disimpan");
                    showHideFormKas(true, false);
                    loadData();
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


        protected void btnMinta_Click(object sender, EventArgs e)
        {
            LoadDataPasien();
            dlgAddData.Show();
        }
        protected void grdDataPasien_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = grdDataPasien.SelectedRow.RowIndex;
            string ID = grdDataPasien.DataKeys[rowIndex].Values[0].ToString();
            string Nama = grdDataPasien.SelectedRow.Cells[2].Text;
            txtNamaPasien.Text = Nama;
            hdnNopasien.Value = ID;
            dlgAddData.Hide();
        }
        protected void grdReceiveUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SelectEdit")
                {
                    
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string id = grdReceiveUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = id;

                    DataSet myData = ObjDb.GetRows("select a.tglmedik, a.klinik, a.noMedik, a.noPasien, b.namaPasien, a.noKaryawan, a.uraian from  Transmedik_h a inner join " +
                        "mPasien b on a.noPasien = b.noPasien inner join MstKaryawan c on c.nokaryawan = a.noKaryawan " +
                        "where a.noMedik = '" + id + "'");
                    DataRow myRow = myData.Tables[0].Rows[0];

                    hdnNoMedik.Value = myRow["noMedik"].ToString();
                    hdnNopasien.Value = myRow["noPasien"].ToString();
                    dtDate.Text = Convert.ToDateTime(myRow["tglmedik"]).ToString("dd-MMM-yyyy");
                    txtNamaPasien.Text = myRow["namaPasien"].ToString();
                    cboDept.Text = myRow["klinik"].ToString();
                    cbodokter.Text = myRow["noKaryawan"].ToString();
                    txtKeterangan.Text = myRow["uraian"].ToString();
                    CloseMessage();
                    tabForm.Visible = true;
                    tabGrid.Visible = false;



                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string id = grdReceiveUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = id;

                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noMedik", hdnId.Value);
                   
                    ObjDb.Delete("Transmedik_h", ObjDb.Where);

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Success Deleted");
                    showHideFormKas(true, false);
                    loadData();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.ToString());
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
            }
        }
    }
}