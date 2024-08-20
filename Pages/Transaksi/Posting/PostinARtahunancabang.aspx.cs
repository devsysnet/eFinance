using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using eFinance.GlobalApp;


namespace eFinance.Pages.Transaksi.Posting
{
    public partial class PostinARtahunancabang : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadData();
                LoadCombo(cboCabang.Text);

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
        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCombo(cboCabang.Text);

        }
        protected void LoadCombo(string cabang = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            cboMonth.DataSource = ObjGlobal.GetDataProcedure("SPLoaddatamulaithnajaran", ObjGlobal.Param);
            cboMonth.DataValueField = "mulaithnajaran1";
            cboMonth.DataTextField = "mulaithnajaran";
            cboMonth.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            cboYear.DataSource = ObjGlobal.GetDataProcedure("SPLoaddataakhirthnajaran", ObjGlobal.Param);
            cboYear.DataValueField = "akhirthnajaran1";
            cboYear.DataTextField = "akhirthnajaran";
            cboYear.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            cboYearmulai.DataSource = ObjGlobal.GetDataProcedure("SPLoaddatthnmulai", ObjGlobal.Param);
            cboYearmulai.DataValueField = "mulaithn";
            cboYearmulai.DataTextField = "mulaithn";
            cboYearmulai.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            cboYearakhir.DataSource = ObjGlobal.GetDataProcedure("SPLoaddatthnakhir", ObjGlobal.Param);
            cboYearakhir.DataValueField = "akhirthn";
            cboYearakhir.DataTextField = "akhirthn";
            cboYearakhir.DataBind();
        }
        protected void LoadData()
        {
           

            cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where parent='" + ObjSys.GetParentCabang + "') a");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();

        }

             

        protected void btnPosting_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (valid == true)
            {

                if (ObjDb.GetRows("select distinct YEAR(tgl) from TransPiutang a inner join mJenisTransaksi b on a.noTransaksi=b.noTransaksi where b.posbln in(1,5) and b.sts=1 and a.nocabang='" + ObjSys.GetCabangId + "' and month(a.tgl)='" + cboMonth.Text + "' and year(a.tgl)='" + cboYearmulai.Text + "'").Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Tahun Ajaran Ini Sudah Pernah DiPosting");
                }

                else
                
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("nocabang", cboCabang.Text);
                    ObjGlobal.ExecuteProcedure("SPProsesARTahunan", ObjGlobal.Param);

                    showHideForm(true, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data Berhasil Di Posting");
                    LoadData();

                }
            }
        }

        protected void showHideForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

    }
}
