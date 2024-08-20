using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Update
{
    public partial class Cancelrequest : System.Web.UI.Page
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
            }
        }

        protected void LoadDataPelunasanAR()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("KdBayar", txtKdBayar.Text);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdARSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPelunasanARUpdate", ObjGlobal.Param);
            grdARSiswa.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtKdBayar.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Kode Bayar harus diisi.");
            }



            else
            {
                CloseMessage();
                LoadDataPelunasanAR();
            }
        }


        protected void btndelete_Click(object sender, EventArgs e)
        {
            if (txtKdBayar.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Kode Bayar harus diisi.");
            }

            else
            {
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("KdBayar", txtKdBayar.Text);
                ObjGlobal.ExecuteProcedure("SPDeletepelunasanAR", ObjGlobal.Param);
                LoadDataPelunasanAR();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil dihapus.");

            }
        }


        protected void grdARSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdARSiswa.PageIndex = e.NewPageIndex;
            LoadDataPelunasanAR();
        }

        protected void grdARSiswa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectEdit")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nopiutang = grdARSiswa.DataKeys[rowIndex].Value.ToString();
                    hdnNoSiswa.Value = nopiutang;

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("nopiutang", hdnNoSiswa.Value);
                    ObjGlobal.Param.Add("KdBayar", txtKdBayar.Text);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataPelunasanARUpdateDetail", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    hdnNoPiut.Value = myRow["noPiutang"].ToString();
                    hdnkdBayar.Value = myRow["kdBayar"].ToString();
                    txtKelas.Text = myRow["kelas"].ToString();
                    txtNamaSiswa.Text = myRow["namaSiswa"].ToString();
                    txtPiutang.Text = ObjSys.IsFormatNumber(myRow["piutang"].ToString());
                    txtBayar.Text = ObjSys.IsFormatNumber(myRow["nilaiBayar"].ToString());
                    txtTglBayar.Text = Convert.ToDateTime(myRow["tgl"]).ToString("dd-MMM-yyyy");
                    txtKodeBayar.Text = myRow["kdBayar"].ToString();

                    this.ShowHideGridAndForm(false, true);
                }



            }
            catch (Exception ex)
            {

                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }

        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false);
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noPiutang", hdnNoPiut.Value);
            ObjGlobal.Param.Add("KdBayar", txtKdBayar.Text);
            ObjGlobal.Param.Add("Nilai", Convert.ToDecimal(txtBayar.Text).ToString());
            ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
            ObjGlobal.ExecuteProcedure("SPUpdatePelunasanAR", ObjGlobal.Param);

            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            ShowMessage("success", "Data berhasil dihapus.");
            LoadDataPelunasanAR();
            this.ShowHideGridAndForm(true, false);
        }
        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnMode.Value.ToLower() == "delete")
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noSiswa", hdnNoSiswa.Value);
                    ObjGlobal.Param.Add("noPiutang", hdnNoPiut.Value);
                    ObjGlobal.Param.Add("kdBayar", hdnkdBayar.Value);
                    ObjGlobal.Param.Add("Nilai", Convert.ToDecimal(txtBayar.Text).ToString());
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.ExecuteProcedure("SPUpdatePelunasanAR", ObjGlobal.Param);


                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus.");
                    LoadDataPelunasanAR();
                    this.ShowHideGridAndForm(true, false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Invalid transaction data.");
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

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
    }
}