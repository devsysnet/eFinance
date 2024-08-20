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
    public partial class Transbagitampunganupd : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                LoadDataCombo();
                loadData();
            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            grdGajiKar.DataSource = ObjGlobal.GetDataProcedure("SPLoadTtampunganpengeluaranupd", ObjGlobal.Param);
            grdGajiKar.DataBind();
        }

        protected void cmdMode_Click(object sender, EventArgs e)
        {

        }

        protected void grdSaldoGL_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void LoadDataCombo()
        {

            //if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            //{
            //    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where stscabang=2) a");
            //    cboCabang.DataValueField = "id";
            //    cboCabang.DataTextField = "name";
            //    cboCabang.DataBind();
            //}
        }

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
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

        protected void btnCari_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }
        private void clearData()
        {

        }

        protected void btnhapus_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("notampungand", hdnotampungan.Value);
            ObjGlobal.ExecuteProcedure("SPdeletetampD", ObjGlobal.Param);

            loadData();
            this.ShowHideGridAndForm(true, false, false);
            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            ShowMessage("success", "Data berhasil Dihapus");


        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;

                if (valid == true)
                {
                    if (Convert.ToDecimal(txtTotalDebit.Text) > Convert.ToDecimal(txtsaldo.Text))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Pengeluaran Lebih Besar dari Saldo");
                    }

                    else
                        for (int i = 0; i < grdSaldoGL.Rows.Count; i++)
                        {
                            HiddenField hdnnotkasdetil = (HiddenField)grdSaldoGL.Rows[i].FindControl("hdnnotkasdetil");
                            TextBox txtDebet = (TextBox)grdSaldoGL.Rows[i].FindControl("txtDebit");
                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("notkasdetil", hdnnotkasdetil.Value);
                            ObjGlobal.Param.Add("nilai", Convert.ToDecimal(txtDebet.Text).ToString());
                            ObjGlobal.ExecuteProcedure("SPupdatetkasdetiltamp", ObjGlobal.Param);

                        }

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("notampungand", hdnotampungan.Value);
                        ObjGlobal.Param.Add("nilailama", Convert.ToDecimal(hdnilailama.Value).ToString());
                        ObjGlobal.ExecuteProcedure("SPupdatetampD", ObjGlobal.Param);

                        loadData();
                        this.ShowHideGridAndForm(true, false, false);
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil diUpdate");
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


        protected void grdGajiKar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdGajiKar.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false, false);
            clearData();
            CloseMessage();
        }

        protected void grdGajiKar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdGajiKar.SelectedRow.RowIndex;
                string notampungand = grdGajiKar.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = notampungand ;

                DataSet MySet = ObjDb.GetRows("select d.notampungand,d.kdtran,d.tglalokasi,c.jenisTampungan,b.nomorKode,CAST(CONVERT(varchar, CAST(a.saldo AS Money), 1) AS varchar) as saldo,CAST(CONVERT(varchar, CAST(d.nilai AS Money), 1) AS varchar) as nilai,CAST(CONVERT(varchar, CAST(a.saldo AS Money), 1) AS varchar) as saldo,b.uraian,e.namacabang from Ttampunganpengeluaran a inner join tKas b on a.nokas=b.noKas inner join mTampPengeluaran_H c on a.nomtampungan=c.nomtampungan inner join Ttampunganpengeluaran_D d on a.notampungan=d.notampungan inner join mCabang e on d.nocabang=e.noCabang where d.notampunganD = '" + notampungand + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow myRow = MySet.Tables[0].Rows[0];

                    txtjenis.Text = myRow["jenisTampungan"].ToString();
                    txtNomorkode.Text = myRow["nomorkode"].ToString();
                    txtkdtran.Text = myRow["kdtran"].ToString();
                    txtRemark.Text = myRow["uraian"].ToString();
                    txtUnit.Text = myRow["namacabang"].ToString();
                    hdnotampungan.Value = myRow["notampungand"].ToString();
                    hdnilailama.Value = myRow["nilai"].ToString();
                    txtsaldo.Text = myRow["saldo"].ToString();
                    dtmasuk.Text = Convert.ToDateTime(myRow["tglalokasi"]).ToString("dd-MMM-yyyy");
                    txtNilai.Text = myRow["nilai"].ToString();
                   

                    this.ShowHideGridAndForm(false, true, false);
                    loadDetil(myRow["notampungand"].ToString());
                    CloseMessage();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Tidak ada.");
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

       
        protected void loadDetil(string notampungand = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("notampungand", notampungand);
            grdSaldoGL.DataSource = ObjGlobal.GetDataProcedure("SPLoadTampunganDetilUpdate", ObjGlobal.Param);
            grdSaldoGL.DataBind();
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }
    }
}