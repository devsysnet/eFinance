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
    public partial class Transketeranganabsen : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtAbsen.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                btnSimpan.Visible = false;
            }
        }
        protected void LoadData()
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("dtAbsen", dtAbsen.Text);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdKetAbsen.DataSource = ObjGlobal.GetDataProcedure("SPLoaddataabsen", ObjGlobal.Param);
            grdKetAbsen.DataBind();

            if (grdKetAbsen.Rows.Count > 0)
                btnSimpan.Visible = true;
            else
                btnSimpan.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
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

                if (valid == true)
                {
                    for (int i = 0; i < grdKetAbsen.Rows.Count; i++)
                    {
                        HiddenField hdnNoKaryawan = (HiddenField)grdKetAbsen.Rows[i].FindControl("hdnNoKaryawan");
                        DropDownList cboIjin = (DropDownList)grdKetAbsen.Rows[i].FindControl("cboIjin");
                        TextBox txtKeterangan = (TextBox)grdKetAbsen.Rows[i].FindControl("txtKeterangan");

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noKaryawan", hdnNoKaryawan.Value);
                        ObjGlobal.Param.Add("tgl", Convert.ToDateTime(dtAbsen.Text).ToString("dd-MMM-yyyy"));
                        ObjGlobal.Param.Add("alasan", cboIjin.Text);
                        ObjGlobal.Param.Add("keterangan", txtKeterangan.Text);
                        ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);

                        ObjGlobal.ExecuteProcedure("SPInsertijinabsen", ObjGlobal.Param);

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


    }
}