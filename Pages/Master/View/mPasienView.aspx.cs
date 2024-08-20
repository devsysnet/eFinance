using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.View
{
    public partial class mPasienView : System.Web.UI.Page
    {

        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                LoadData();
                ShowHideGridAndForm(true, false);
            }
        }

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("noCabang", cboCabang.Text);
            grdSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPasienNew", ObjGlobal.Param);
            grdSiswa.DataBind();

        }

        protected void loadDataCombo()
        {

            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang
            //pusat
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang=2) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang where stscabang=2) a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //perwakilan
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //unit
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdSiswa.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void grdSiswa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = grdSiswa.SelectedRow.RowIndex;
                string noSiswa = grdSiswa.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noSiswa;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("nopasien", hdnId.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataPasien_Detail", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                txtNama.Text = myRow["namaPasien"].ToString();
                txtNIK.Text = myRow["nik"].ToString();
                txtnoRegister.Text = myRow["noregister"].ToString();
                txtgender.Text = myRow["gender"].ToString();
                txtAgama.Text = myRow["nmAgama"].ToString();
                txtAlamat.Text = myRow["alamat"].ToString();
                txtTglLahir.Text = Convert.ToDateTime(myRow["tgllahir"]).ToString("dd-MMM-yyyy");
                txtTglregister.Text = Convert.ToDateTime(myRow["tglRegiister"]).ToString("dd-MMM-yyyy");
                //txtnohp.Text = myRow["noHp"].ToString();
                txtTelp.Text = myRow["noTelp"].ToString();
                txtKota.Text = myRow["nokota"].ToString();
                txtKotaLhr.Text = myRow["tempatlahir"].ToString();
                txtstsBpjs.Text = myRow["stsBpjs"].ToString();
                txtnoBPJS.Text = myRow["noBpjs"].ToString();
                txtGoldarah.Text = myRow["goldarah"].ToString();
                txtUraian.Text = myRow["uraian"].ToString();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noPasien", hdnId.Value);
                grdPiutSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadRegisterHistory", ObjGlobal.Param);
                grdPiutSiswa.DataBind();

                this.ShowHideGridAndForm(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false);
            LoadData();
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}