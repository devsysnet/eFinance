using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace eFinance.Pages.Transaksi.View
{
    public partial class TranskenaikanGajiView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        //buatcetakan
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCombo();
                loadData();

            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            grdKas.DataSource = ObjGlobal.GetDataProcedure("SPViewnaikgolongan", ObjGlobal.Param);
            grdKas.DataBind();

            for (int i = 0; i < grdKas.Rows.Count; i++)
            {
                HiddenField hdnIdPrint = (HiddenField)grdKas.Rows[i].FindControl("hdnIdPrint");
                Button btnPrint = (Button)grdKas.Rows[i].FindControl("btnPrint");

                DataSet mySet = ObjDb.GetRows("select stsApp from TransKenaikanGaji where stsApp=0 and nonaikgaji = '" + hdnIdPrint.Value + "'");
                if (mySet.Tables[0].Rows.Count > 0)
                {
                    btnPrint.Enabled = true;
                }
                else
                {
                    btnPrint.Enabled = false;
                }
            }
        }

        protected void loadCombo()
        {
            cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang= '" + ObjSys.GetCabangId + "') a");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();
        }

            

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        protected void grdKas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdKas.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void loadDataCombo()
        {
            //cboAccount.DataSource = ObjDb.GetRows("select a.* from (select '0' id,' --------- ' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis in(1,2)) a");
            //cboAccount.DataValueField = "id";
            //cboAccount.DataTextField = "name";
            //cboAccount.DataBind();

            //cboCurrency.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noMataUang id, namaMataUang name FROM mMataUang where stsMataUang = '1' ) a");
            //cboCurrency.DataValueField = "id";
            //cboCurrency.DataTextField = "name";
            //cboCurrency.DataBind();
        }

        protected void grdKas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = grdKas.SelectedRow.RowIndex;
                string id = grdKas.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = id;
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("nonaikgaji", hdnId.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPViewnaikgolongandet", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                txtkdtran.Text = myRow["kdtran"].ToString();
                dttgltrs.Text = Convert.ToDateTime(myRow["Tgl"]).ToString("dd-MMM-yyyy");
                txtnama.Text = myRow["nama"].ToString();
                txttempatlahir.Text = myRow["txttempatlahir"].ToString();
                txtJabatan.Text = myRow["Jabatan"].ToString();
                dttgldiangkat.Text = Convert.ToDateTime(myRow["tglDiangkat"]).ToString("dd-MMM-yyyy");
                txtgollama.Text = myRow["gollama"].ToString();
                txtgolbaru.Text = myRow["golbaru"].ToString();
                txtgajilama.Text = ObjSys.IsFormatNumber(myRow["gajilama"].ToString());
                txtgajibaru.Text = ObjSys.IsFormatNumber(myRow["gajibaru"].ToString());
                dttglmulai.Text = Convert.ToDateTime(myRow["tglmulai"]).ToString("dd-MMM-yyyy");

                showHideFormKas(false, true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void grdKasView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detail")
            {
                try
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    HiddenField hdnIdPrint = (HiddenField)grdKas.Rows[rowIndex].FindControl("hdnIdPrint");
                    HttpContext.Current.Session["ParamReport"] = null;
                    Session["REPORTNAME"] = null;
                    Session["REPORTTITLE"] = null;
                    Param.Clear();
                    Param.Add("nonaikgaji", hdnIdPrint.Value);
                    HttpContext.Current.Session.Add("ParamReport", Param);
                    Session["REPORTNAME"] = "Rptsuratgajinaikgol.rpt";
                    Session["REPORTTILE"] = "Surat Permintaan Kenaikan Gaji";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);

                }
                catch (Exception ex)
                {
                    Response.Write("Error:" + ex.ToString());
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
                }
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

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }



        protected void btnReset_Click(object sender, EventArgs e)
        {
            showHideFormKas(true, false);
        }


    }
}