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
    public partial class TransStatusKrywn : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
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

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdStsKrywn.DataSource = ObjGlobal.GetDataProcedure("SPStatusKaryawan", ObjGlobal.Param);
            grdStsKrywn.DataBind();
        
        }

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        
        protected void btnSimpan_Click(object sender, EventArgs e)
        {

            bool valid = true;
            string message = "", alert = "";

            if (dtInput.Text == "")
            {
                message = ObjSys.CreateMessage("Tgl harus di isi.");
                alert = "error";
                valid = false;
            }
            try
            {
                if (valid == true)
                {
                    string Kode = "";

                    if (hdnStatusH.Value == "Masa Percobaan")
                        Kode = ObjSys.GetCodeAutoNumberNew("28", Convert.ToDateTime(dtInput.Text).ToString("yyyy-MM-dd"));
                    else if (hdnStatusH.Value == "Calon Pegawai")
                        Kode = ObjSys.GetCodeAutoNumberNew("29", Convert.ToDateTime(dtInput.Text).ToString("yyyy-MM-dd"));

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kode", Kode);
                    ObjGlobal.Param.Add("tgl", Convert.ToDateTime(dtInput.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("keterangan", txtKeterangan.Text);
                    ObjGlobal.Param.Add("nokaryawan", hdnnokaryawanH.Value);
                    ObjGlobal.Param.Add("stsPgwAwal", cboStsPegawai.Text);
                    ObjGlobal.Param.Add("stsPgwAkhir", cboStsPegawaiAkhir.Text);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPInsertStsKrywn", ObjGlobal.Param);

                    if (hdnStatusH.Value == "Masa Percobaan")
                        ObjSys.UpdateAutoNumberCode("28", Convert.ToDateTime(dtInput.Text).ToString("yyyy-MM-dd"));
                    else if (hdnStatusH.Value == "Calon Pegawai")
                        ObjSys.UpdateAutoNumberCode("29", Convert.ToDateTime(dtInput.Text).ToString("yyyy-MM-dd"));

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data has been saved.");
                    showHideFormKas(true, false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Invalid transaction data.");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CloseMessage();
            showHideFormKas(true, false);
        }

        protected void grdStsKrywn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = grdStsKrywn.SelectedRow.RowIndex;
                string id = grdStsKrywn.DataKeys[rowIndex].Values[0].ToString();
                HiddenField hdnnoKrywn = (HiddenField)grdStsKrywn.Rows[rowIndex].FindControl("hdnnoKrywn");
                HiddenField hdnStatus = (HiddenField)grdStsKrywn.Rows[rowIndex].FindControl("hdnStatus");

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("nokaryawan", hdnnoKrywn.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDetilKrywn", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];
                txtID.Text = myRow["idPeg"].ToString();
                Txtnama.Text = myRow["nama"].ToString();
                Txtttlahir.Text = myRow["ttl"].ToString();
                Txtnik.Text = myRow["nik"].ToString();
                dtMasuk.Text = Convert.ToDateTime(myRow["tglmasuk"]).ToString("dd-MMM-yyyy");
                cboStsPegawai.Text = myRow["statusPeg"].ToString();
                if (hdnStatus.Value == "Masa Percobaan")
                    cboStsPegawaiAkhir.Text = "2";
                else if (hdnStatus.Value == "Calon Pegawai")
                    cboStsPegawaiAkhir.Text = "1";

                hdnnokaryawanH.Value = myRow["noKaryawan"].ToString();
                hdnStatusH.Value = hdnStatus.Value;

                loadCombo();
                showHideFormKas(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void loadCombo()
        {
            cboStsPegawai.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct nostatuspeg id, statuspegawai name FROM Mmststatuspegawai where sts = '1') a");
            cboStsPegawai.DataValueField = "id";
            cboStsPegawai.DataTextField = "name";
            cboStsPegawai.DataBind();

            cboStsPegawaiAkhir.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct nostatuspeg id, statuspegawai name FROM Mmststatuspegawai where sts = '1') a");
            cboStsPegawaiAkhir.DataValueField = "id";
            cboStsPegawaiAkhir.DataTextField = "name";
            cboStsPegawaiAkhir.DataBind();
        }
        protected void grdStsKrywn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            LoadData();
            grdStsKrywn.PageIndex = e.NewPageIndex;
        }
    }
}