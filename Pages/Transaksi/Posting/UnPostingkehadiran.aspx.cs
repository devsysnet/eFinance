using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Posting
{
    public partial class UnPostingkehadiran : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
                //cboYear.DataValueField = "id";
                //cboYear.DataTextField = "name";
                //cboYear.DataBind();
                LoadData();

            }
        }

        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            cboMonth.DataSource = ObjGlobal.GetDataProcedure("SPLoaddataunposkehadiran", ObjGlobal.Param);
            cboMonth.DataValueField = "bln";
            cboMonth.DataTextField = "bulan";
            cboMonth.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            cboYear.DataSource = ObjGlobal.GetDataProcedure("SPLoaddataunposkehadiranThn", ObjGlobal.Param);
            cboYear.DataValueField = "thn";
            cboYear.DataTextField = "thn";
            cboYear.DataBind();
        }
        #region 

        protected void btnPosting_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", cboYear.Text);
            ObjGlobal.Param.Add("bln", cboMonth.Text);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.ExecuteProcedure("SPUnpostkehadiran", ObjGlobal.Param);
            LoadData();
            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            ShowMessage("success", "Data berhasil diposting");

        }


        #endregion


    }
}