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
    public partial class TransPemusnahanAsetView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtMulai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                dtSampai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                loadData();
            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            ObjGlobal.Param.Add("search", txtCariAset.Text); 
            grdAsetUpdate.DataSource = ObjGlobal.GetDataProcedure("SPLoadUpdateViewMusnahAset", ObjGlobal.Param);
            grdAsetUpdate.DataBind();
        }

        protected void LoadCombo()
        {
            cboLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Lokasi---' name union all SELECT distinct noLokasi id, Lokasi name FROM mLokasi ) a");
            cboLokasi.DataValueField = "id";
            cboLokasi.DataTextField = "name";
            cboLokasi.DataBind();

            cboSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi ) a");
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

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            showHideFormKas(true, false);
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void grdAsetUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdAsetUpdate.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void grdAsetUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SelectEdit")
                {
                    CloseMessage();
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string id = grdAsetUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = id;

                    DataSet myData = ObjDb.GetRows("select * from Tasset where noAset = '" + id + "'");
                    DataRow myRow = myData.Tables[0].Rows[0];

                    hdnBarangAset.Value = myRow["noAset"].ToString();
                    txtNamaBarangAset.Text = myRow["namaAsset"].ToString();
                    txtKodeAset.Text = myRow["kodeAsset"].ToString();
                    cboLokasi.Text = myRow["noLokasi"].ToString();
                    cboSubLokasi.Text = myRow["noSubLokasi"].ToString();
                    dtMusnah.Text = Convert.ToDateTime(myRow["tglMusnah"]).ToString("dd-MMM-yyyy");
                    txtAlasan.Text = myRow["alasanMusnah"].ToString();
                    LoadCombo();
                    showHideFormKas(false, true);

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