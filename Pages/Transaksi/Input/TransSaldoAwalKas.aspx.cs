using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransSaldoAwalKas : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                txtTanggal.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            }
        }
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdSaldoGL.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataSaldoAwal", ObjGlobal.Param);
            grdSaldoGL.DataBind();

            if (grdSaldoGL.Rows.Count > 0)
            {
                btnSimpan.Visible = true;
                btnReset.Visible = true;
                for (int i = 0; i < grdSaldoGL.Rows.Count; i++)
                {
                    DropDownList cboMataUang = (DropDownList)grdSaldoGL.Rows[i].FindControl("cboMataUang");

                    cboMataUang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noMataUang id, namaMataUang name FROM mMataUang ) a");
                    cboMataUang.DataValueField = "id";
                    cboMataUang.DataTextField = "name";
                    cboMataUang.DataBind();

                    cboMataUang.SelectedValue = "20";
                } 
            }
            else
            {
                btnSimpan.Visible = false;
                btnReset.Visible = false;
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
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;
                TextBox txtTotalDebet = (TextBox)grdSaldoGL.FooterRow.FindControl("txtTotalDebet");
                TextBox txtTotalKredit = (TextBox)grdSaldoGL.FooterRow.FindControl("txtTotalKredit");

                if (valid == true)
                {
                    for (int i = 0; i < grdSaldoGL.Rows.Count; i++)
                    {
                        HiddenField hdnNoRek = (HiddenField)grdSaldoGL.Rows[i].FindControl("hdnNoRek");
                        TextBox txtNilai = (TextBox)grdSaldoGL.Rows[i].FindControl("txtNilai");
                        DropDownList cboMataUang = (DropDownList)grdSaldoGL.Rows[i].FindControl("cboMataUang");

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("norek", hdnNoRek.Value);
                        ObjGlobal.Param.Add("kdRek", grdSaldoGL.Rows[i].Cells[1].Text);
                        ObjGlobal.Param.Add("Tgl", txtTanggal.Text);
                        ObjGlobal.Param.Add("noMataUang", cboMataUang.Text);
                        ObjGlobal.Param.Add("Nilai", Convert.ToDecimal(txtNilai.Text).ToString());
                        ObjGlobal.Param.Add("sts", "1");
                        ObjGlobal.Param.Add("jns", "1");
                        ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                        ObjGlobal.ExecuteProcedure("SPInsertGLSaldoAwalKas", ObjGlobal.Param);
                    }
                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }
    }
}