using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransThrbonus : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataFirst();
                LoadDataCombo();
            }

        }

        protected void loadDataFirst()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("tgl", dtPO.Text);
            ObjGlobal.Param.Add("jenis", cboStatus.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdKelasSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataThrbonus", ObjGlobal.Param);
            grdKelasSiswa.DataBind();
            if (grdKelasSiswa.Rows.Count > 0)
                button.Visible = true;
            else
                button.Visible = false;


        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataFirst();
        }

        protected void LoadDataCombo()
        {

            cboStatus.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct nokomponengaji id, komponengaji name FROM mstkomponenGaji where jenis = '0' and kategori='1') a");
            cboStatus.DataValueField = "id";
            cboStatus.DataTextField = "name";
            cboStatus.DataBind();

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

        protected void grdKelasSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdKelasSiswa.PageIndex = e.NewPageIndex;
            loadDataFirst();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (dtPO.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal harus diisi.");
                valid = false;
            }
            if (valid == true)
            {
                try
            {
                int count = 0;
                for (int i = 0; i < grdKelasSiswa.Rows.Count; i++)
                {

                    HiddenField hdnnokaryawan = (HiddenField)grdKelasSiswa.Rows[i].FindControl("hdnnokaryawan");
                    CheckBox chkCheck = (CheckBox)grdKelasSiswa.Rows[i].FindControl("chkCheck");
                    //TextBox txtcatatan = (TextBox)grdKelasSiswa.Rows[i].FindControl("txtcatatan");
                    TextBox txtnilai = (TextBox)grdKelasSiswa.Rows[i].FindControl("nilai");
                    //TextBox dtPO = (TextBox)grdKelasSiswa.Rows[i].FindControl("dtPO");
                    //DropDownList cboStatus = (DropDownList)grdKelasSiswa.Rows[i].FindControl("cboStatus");

                    if (chkCheck.Checked == true)
                    {
                        string Kode = "";
                        count++;

                        Kode = ObjSys.GetCodeAutoNumberNew("24", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("kdTran", Kode);
                        ObjGlobal.Param.Add("nokaryawan", hdnnokaryawan.Value);
                        ObjGlobal.Param.Add("jenis", cboStatus.Text);
                        ObjGlobal.Param.Add("tgl", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("nilai", Convert.ToDecimal(txtnilai.Text).ToString());
                        ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("createddate", ObjSys.GetDate);
                        ObjGlobal.GetDataProcedure("SPInsertThrbonus", ObjGlobal.Param);

                        ObjSys.UpdateAutoNumberCode("24", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));

                    }
                }
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data harus dipilih.");
                }
                else
                {
                    loadDataFirst();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadValueDetil(cboStatus.Text);
        }

        protected void loadValueDetil(string cboStatus)
        {
            for (int i = 0; i < grdKelasSiswa.Rows.Count; i++)
            {
                HiddenField hdnnokaryawan = (HiddenField)grdKelasSiswa.Rows[i].FindControl("hdnnokaryawan");
                CheckBox chkCheck = (CheckBox)grdKelasSiswa.Rows[i].FindControl("chkCheck");
                TextBox txtnilai = (TextBox)grdKelasSiswa.Rows[i].FindControl("nilai");

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("id", hdnnokaryawan.Value);
                ObjGlobal.Param.Add("select", cboStatus);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPGetValueTHRnBonus", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];
                if (myRow["Nilai"].ToString() != "0")
                    chkCheck.Checked = true;
                else
                    chkCheck.Checked = false;

                txtnilai.Text = ObjSys.IsFormatNumber(myRow["Nilai"].ToString());

            }
        }
    }
}