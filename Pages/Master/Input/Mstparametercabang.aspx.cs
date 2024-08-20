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
    public partial class Mstparametercabang : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ObjSys.SessionCheck("MProject.aspx");
                loadCombo();
                showhidekode.Visible = false;
            }
        }

        protected void loadCombo()
        {

            cboTA.DataSource = ObjDb.GetRows("select distinct tahunAjaran as id, tahunAjaran as name from Parameter");
            cboTA.DataValueField = "id";
            cboTA.DataTextField = "name";
            cboTA.DataBind();
        }

        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbojnsbyr.Text == "PaymentGateWay")
                   showhidekode.Visible = true;
              
            else
                showhidekode.Visible = false;
            



            cbobank.DataSource = ObjDb.GetRows("select a.* from (SELECT '0' id, '---Pilih Bank---' name union SELECT distinct norek id,ket name FROM mRekening where jenis='2' and sts='2' and nocabang=" + ObjSys.GetCabangId + ") a");
            cbobank.DataValueField = "id";
            cbobank.DataTextField = "name";
            cbobank.DataBind();

        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (valid == true)
            {

                if (ObjDb.GetRows("SELECT * FROM Parametercabang WHERE nocabang = '" + ObjSys.GetCabangId + "'").Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Sudah Setting Parameter Cabang");
                }
                else
                {
                    try
                    {
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("tahunajaran", cboTA.Text);
                        ObjGlobal.Param.Add("mulaithnajaran", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("akhirthnajaran", Convert.ToDateTime(dtKas1.Text).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("absenkegaji", cboitunggaji.Text);
                        ObjGlobal.Param.Add("dtsampai", Convert.ToDateTime(dtsampai.Text).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("dtdari", Convert.ToDateTime(dtdari.Text).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("absenpotonggaji", cboPotonggaji.Text);
                        ObjGlobal.Param.Add("jammasuk", jammasuk.Text);
                        ObjGlobal.Param.Add("jamkeluar", jamkeluar.Text);
                        ObjGlobal.Param.Add("upahminimum", Convert.ToDecimal(txtNilai.Text).ToString());
                        ObjGlobal.Param.Add("biayaadmbank", Convert.ToDecimal(biayaadmbank.Text).ToString());
                        ObjGlobal.Param.Add("kepalasekolah", kepalasekolah.Text);
                        ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                        ObjGlobal.Param.Add("penggajian", cbogaji.Text);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("jenisbyr", cbojnsbyr.Text);
                        ObjGlobal.Param.Add("bankpayment", cbobank.Text);
                        ObjGlobal.Param.Add("ipcabang", ipcabang.Text);
                        ObjGlobal.Param.Add("kdbank", kdbank.Text);
                        ObjGlobal.Param.Add("jenisbank", cbojnsbank.Text);
                        ObjGlobal.Param.Add("cuti", cboscuti.Text);
                        ObjGlobal.GetDataProcedure("SPInsertparametercabang", ObjGlobal.Param);

                        ClearData();
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil disimpan.");
                    }
                    catch (Exception ex)
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
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

            dtKas.Text = "";
            dtKas1.Text = "";
            dtKas.Text = "";
            txtNilai.Text = "";
           CloseMessage();
        }
    }
}