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
    public partial class PostingARtsiswabaru : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
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
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            cboMonth.DataSource = ObjGlobal.GetDataProcedure("SPLoaddatamulaithnajaran", ObjGlobal.Param);
            cboMonth.DataValueField = "mulaithnajaran";
            cboMonth.DataTextField = "mulaithnajaran";
            cboMonth.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            cboYear.DataSource = ObjGlobal.GetDataProcedure("SPLoaddataakhirthnajaran", ObjGlobal.Param);
            cboYear.DataValueField = "akhirthnajaran";
            cboYear.DataTextField = "akhirthnajaran";
            cboYear.DataBind();
        }


        protected void btnPosting_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (valid == true)
            {

                //if (ObjDb.GetRows("select distinct YEAR(tgl) from TransPiutang a inner join mJenisTransaksi b on a.noTransaksi=b.noTransaksi where b.posbln=1 and b.sts=1 and a.nocabang='" + ObjSys.GetCabangId + "' and Year(a.tgl)='" + cboYear.Text + "'").Tables[0].Rows.Count > 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //    ShowMessage("error", "Tahun Ajaran Ini Sudah Pernah DiPosting");
                //}

                //else


                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.ExecuteProcedure("SPProsesARTahunansiswa", ObjGlobal.Param);

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
