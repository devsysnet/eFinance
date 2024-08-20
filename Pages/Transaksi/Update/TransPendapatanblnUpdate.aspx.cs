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


namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransPendapatanblnUpdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;

        private Database ObjDb = new Database();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadIndexYayasan();
                LoadDataCombo();
                button.Visible = false;
                btnDelete.Visible = false;
                //loadDataFirst(cboPerwakilan.Text, cboUnit.Text);
            }

        }
        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
             int counta = 0;
                for (int i = 0; i < grdKelasSiswa.Rows.Count; i++)
                {
                    HiddenField hdnnogajibln = (HiddenField)grdKelasSiswa.Rows[i].FindControl("hdnnogajibln");
                    CheckBox chkCheck = (CheckBox)grdKelasSiswa.Rows[i].FindControl("chkCheck");

                    if (chkCheck.Checked == true)
                    {
                        counta++;

                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("nogajibln", hdnnogajibln.Value);
                        ObjDb.Delete("TGajibulanan", ObjDb.Where);

                    }
                }
                if (counta == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data harus dipilih.");
                }
                else
                {
                    loadDataFirst(cboPerwakilan.Text, cboCabang.Text);

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus.");
                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Invalid transaction data.");
            }
        }
        protected void LoadIndexYayasan()
        {
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
            DataRow myRow = mySet.Tables[0].Rows[0];
            hdnnoYysn.Value = myRow["noCabang"].ToString();
        }

        protected void loadDataFirst(string perwakilan = "0", string unit = "0")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("tgl", dtPO.Text);
            ObjGlobal.Param.Add("jenis", cboStatus.Text);
            ObjGlobal.Param.Add("noCabang", unit);
            ObjGlobal.Param.Add("perwakilan", perwakilan);
            grdKelasSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDatapendapatanbulanan", ObjGlobal.Param);
            grdKelasSiswa.DataBind();
          
               


        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataFirst(cboPerwakilan.Text, cboCabang.Text);
            button.Visible = true;
            btnDelete.Visible = true;

        }

        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataUnit();
            //loadDataFirst(cboPerwakilan.Text, cboUnit.Text);
           
        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataFirst(cboPerwakilan.Text, cboCabang.Text);

        }


        protected void LoadDataCombo()
        {

            cboStatus.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct nokomponengaji id, komponengaji name FROM mstkomponenGaji where jenis = '0' and kategori='1') a");
            cboStatus.DataValueField = "id";
            cboStatus.DataTextField = "name";
            cboStatus.DataBind();
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Perwakilan' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and noCabang=" + ObjSys.GetCabangId + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where noCabang=" + ObjSys.GetParentCabang + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "3")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where noCabang=" + ObjSys.GetParentCabang + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where  stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }

            loadDataUnit(cboPerwakilan.Text);
        }

        protected void loadDataUnit(string perwakilan = "0")
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", perwakilan);
            cboCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitPerwakilan1", ObjGlobal.Param);
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();


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
            loadDataFirst(cboPerwakilan.Text, cboCabang.Text);

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                for (int i = 0; i < grdKelasSiswa.Rows.Count; i++)
                {
                    HiddenField hdnnokaryawan = (HiddenField)grdKelasSiswa.Rows[i].FindControl("hdnnokaryawan");
                    HiddenField hdnnocabang = (HiddenField)grdKelasSiswa.Rows[i].FindControl("hdnnocabang");
                    HiddenField hdnnogajibln = (HiddenField)grdKelasSiswa.Rows[i].FindControl("hdnnogajibln");
                    CheckBox chkCheck = (CheckBox)grdKelasSiswa.Rows[i].FindControl("chkCheck");
                    TextBox txtnilai = (TextBox)grdKelasSiswa.Rows[i].FindControl("nilai");

                    if (chkCheck.Checked == true)
                    {
                        count++;

                        ObjDb.Data.Clear();
                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("nogajibln", hdnnogajibln.Value);
                        ObjDb.Data.Add("nilai", Convert.ToDecimal(txtnilai.Text).ToString());
                        ObjDb.Update("TGajibulanan", ObjDb.Data, ObjDb.Where);

                    }
                }
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data harus dipilih.");
                }
                else
                {
                    loadDataFirst(cboPerwakilan.Text, cboCabang.Text);

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

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadValueDetil(cboStatus.Text);
        }

    }
}
