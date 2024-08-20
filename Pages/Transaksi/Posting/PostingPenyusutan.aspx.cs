using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Posting
{
    public partial class PostingPenyusutan : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();
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

        }
        #region 

        protected void btnPosting_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (valid == true)
            {
                if (ObjDb.GetRows("select * from tkasdetil where month(tgl)='"+ cboMonth.Text + "' and year(tgl)='" + cboYear.Text  + "' and jenistran='Akumulasi Penyusutan'  and nocabang='" + ObjSys.GetCabangId + "'").Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Sudah Diposting");
                }
                else
                {
                    var dateString = cboMonth.Text + '/' + "1" + '/' + cboYear.Text ;
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("thn", cboYear.Text);
                    ObjGlobal.Param.Add("bln", cboMonth.Text);
                    ObjGlobal.Param.Add("tgl", dateString.ToString());
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.ExecuteProcedure("SPProsesPenyusutanGL", ObjGlobal.Param);
                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diposting");
                }
            }
        }
    
        #endregion


    }
}
