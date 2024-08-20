using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class FormDokterGigi : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowHideGridAndForm(true, false);
                loadAwal();
            }
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        protected void loadGrid()
        {
            grd1.DataSource = ObjGlobal.GetDataProcedure("SPLoadDiagramGigi");
            grd1.DataBind();
        }

        protected void grdRegister_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRegister.PageIndex = e.NewPageIndex;
            loadAwal();
        }
        protected void loadAwal()
        {
            ObjGlobal.Param.Add("nokaryawan", ObjSys.GetUserId);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdRegister.DataSource = ObjGlobal.GetDataProcedure("SPLoadRegisterGigi", ObjGlobal.Param);
            grdRegister.DataBind();
        }

        protected void grdRegister_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectDetil")
            {

                int rowIndex = int.Parse(e.CommandArgument.ToString());
                string noMedik = grdRegister.DataKeys[rowIndex].Value.ToString();
                hdnId.Value = noMedik;

                DataSet mySet = ObjDb.GetRows("select * from Transmedik_h a inner join mPasien b on a.noPasien=b.noPasien where a.noMedik = '" + hdnId.Value + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];

                hdnReg.Value = myRow["noPasien"].ToString();
                txtNoReg.Text = myRow["noRegister"].ToString();
                txtNamaReg.Text = myRow["namaPasien"].ToString();
                txtAlamatReg.Text = myRow["alamat"].ToString();
                txtThn.Text = myRow["umurth"].ToString();
                txtBln.Text = myRow["umurbln"].ToString();
                dtPeriksa.Text = Convert.ToDateTime(myRow["tglmedik"]).ToString("dd-MMM-yyyy");

                ShowHideGridAndForm(false, true);
                loadGrid();
                CloseMessage();
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

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "";

            try
            {

                if (valid == true)
                {
                    ObjDb.Data.Clear();
                    ObjDb.Data.Add("noMedik", hdnId.Value);

                    ObjDb.Data.Add("cbo1", cbo1.Text);
                    ObjDb.Data.Add("cbo2", cbo2.Text);
                    ObjDb.Data.Add("cbo3", cbo3.Text);
                    ObjDb.Data.Add("cbo4", cbo4.Text);
                    ObjDb.Data.Add("cbo5", cbo5.Text);
                    ObjDb.Data.Add("cbo6", cbo6.Text);
                    ObjDb.Data.Add("cbo7", cbo7.Text);
                    ObjDb.Data.Add("cbo8", cbo8.Text);
                    ObjDb.Data.Add("cbo9", cbo9.Text);
                    ObjDb.Data.Add("cbo10", cbo10.Text);
                    ObjDb.Data.Add("cbo11", cbo11.Text);

                    ObjDb.Data.Add("txtLT1", txtLT1.Text);
                    ObjDb.Data.Add("txtLT2", txtLT2.Text);
                    ObjDb.Data.Add("txtLT3", txtLT3.Text);
                    ObjDb.Data.Add("txtLT4", txtLT4.Text);
                    ObjDb.Data.Add("txtLT5", txtLT5.Text);
                    ObjDb.Data.Add("txtLT6", txtLT6.Text);
                    ObjDb.Data.Add("txtLT7", txtLT7.Text);

                    ObjDb.Data.Add("txtLB1", txtLB1.Text);
                    ObjDb.Data.Add("txtLB2", txtLB2.Text);
                    ObjDb.Data.Add("txtLB3", txtLB3.Text);
                    ObjDb.Data.Add("txtLB4", txtLB4.Text);
                    ObjDb.Data.Add("txtLB5", txtLB5.Text);
                    ObjDb.Data.Add("txtLB6", txtLB6.Text);
                    ObjDb.Data.Add("txtLB7", txtLB7.Text);

                    ObjDb.Data.Add("txtRT1", txtRT1.Text);
                    ObjDb.Data.Add("txtRT2", txtRT2.Text);
                    ObjDb.Data.Add("txtRT3", txtRT3.Text);
                    ObjDb.Data.Add("txtRT4", txtRT4.Text);
                    ObjDb.Data.Add("txtRT5", txtRT5.Text);
                    ObjDb.Data.Add("txtRT6", txtRT6.Text);
                    ObjDb.Data.Add("txtRT7", txtRT7.Text);

                    ObjDb.Data.Add("txtRB1", txtRB1.Text);
                    ObjDb.Data.Add("txtRB2", txtRB2.Text);
                    ObjDb.Data.Add("txtRB3", txtRB3.Text);
                    ObjDb.Data.Add("txtRB4", txtRB4.Text);
                    ObjDb.Data.Add("txtRB5", txtRB5.Text);
                    ObjDb.Data.Add("txtRB6", txtRB6.Text);
                    ObjDb.Data.Add("txtRB7", txtRB7.Text);

                    ObjDb.Data.Add("txtLokasi", txtLokasi.Text);
                    
                    ObjDb.Insert("TransGigi", ObjDb.Data);

                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noMedik", hdnId.Value);
                    ObjDb.Data.Add("tglKontrol", Convert.ToDateTime(dtPeriksa.Text).ToString("yyyy-MM-dd"));
                    ObjDb.Data.Add("sts", "1");
                    ObjDb.Data.Add("modiBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modidate", ObjSys.GetNow);

                    ObjDb.Update("Transmedik_h", ObjDb.Data, ObjDb.Where);

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan");
                    ClearData();
                    ShowHideGridAndForm(true, false);
                    loadAwal();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
            
            hdnId.Value = "0";
            cbo1.Text = "Tidak";
            cbo2.Text = "Tidak";
            cbo3.Text = "Tidak";
            cbo4.Text = "Tidak";
            cbo5.Text = "Tidak";
            cbo6.Text = "Tidak";
            cbo7.Text = "Tidak";
            cbo8.Text = "Tidak";
            cbo9.Text = "Tidak";
            cbo10.Text = "Tidak";
            cbo11.Text = "Tidak";

            txtLT1.Text = "";
            txtLT2.Text = "";
            txtLT3.Text = "";
            txtLT4.Text = "";
            txtLT5.Text = "";
            txtLT6.Text = "";
            txtLT7.Text = "";

            txtLB1.Text = "";
            txtLB2.Text = "";
            txtLB3.Text = "";
            txtLB4.Text = "";
            txtLB5.Text = "";
            txtLB6.Text = "";
            txtLB7.Text = "";

            txtRT1.Text = "";
            txtRT2.Text = "";
            txtRT3.Text = "";
            txtRT4.Text = "";
            txtRT5.Text = "";
            txtRT6.Text = "";
            txtRT7.Text = "";

            txtRB1.Text = "";
            txtRB2.Text = "";
            txtRB3.Text = "";
            txtRB4.Text = "";
            txtRB5.Text = "";
            txtRB6.Text = "";
            txtRB7.Text = "";

            txtLokasi.Text = "";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

            ShowHideGridAndForm(true, false);
            ClearData();
        }

        protected void ClearData()
        {
            hdnId.Value = "0";
            cbo1.Text = "Tidak";
            cbo2.Text = "Tidak";
            cbo3.Text = "Tidak";
            cbo4.Text = "Tidak";
            cbo5.Text = "Tidak";
            cbo6.Text = "Tidak";
            cbo7.Text = "Tidak";
            cbo8.Text = "Tidak";
            cbo9.Text = "Tidak";
            cbo10.Text = "Tidak";
            cbo11.Text = "Tidak";

            txtLT1.Text = "";
            txtLT2.Text = "";
            txtLT3.Text = "";
            txtLT4.Text = "";
            txtLT5.Text = "";
            txtLT6.Text = "";
            txtLT7.Text = "";

            txtLB1.Text = "";
            txtLB2.Text = "";
            txtLB3.Text = "";
            txtLB4.Text = "";
            txtLB5.Text = "";
            txtLB6.Text = "";
            txtLB7.Text = "";

            txtRT1.Text = "";
            txtRT2.Text = "";
            txtRT3.Text = "";
            txtRT4.Text = "";
            txtRT5.Text = "";
            txtRT6.Text = "";
            txtRT7.Text = "";

            txtRB1.Text = "";
            txtRB2.Text = "";
            txtRB3.Text = "";
            txtRB4.Text = "";
            txtRB5.Text = "";
            txtRB6.Text = "";
            txtRB7.Text = "";

        }
    }
}