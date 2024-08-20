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
using System.IO;


namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransConfirmDO : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ObjSys.SessionCheck("TransConfirmDO.aspx");
                LoadData();
                //dtPIB.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtCariData.Text);
            grdDOView.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataDOConfirm", ObjGlobal.Param);
            grdDOView.DataBind();
        }
        protected void LoadDataDetail()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noDO", hdnNoDO.Value);
            grdDOConfirmDetail.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataDOConfirmDetail", ObjGlobal.Param);
            grdDOConfirmDetail.DataBind();
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
        protected void ClearData()
        {
            hdnNoDO.Value = "";
            CloseMessage();
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (dtDOConfirmDate.Text == "")
            {
                message += ObjSys.CreateMessage("Kurs tidak boleh kosong.");
                valid = false;
            }

            int cek = 0;
            for (int i = 0; i < grdDOConfirmDetail.Rows.Count; i++)
            {
                TextBox txtQtyConfrim = (TextBox)grdDOConfirmDetail.Rows[i].FindControl("txtQtyConfrim");

                if (txtQtyConfrim.Text != "")
                {
                    cek++;
                }
            }

            if (cek == 0)
            {
                message += ObjSys.CreateMessage("Detil tidak boleh kosong.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                    string Kode = ObjSys.GetCodeAutoNumberNew("33", Convert.ToDateTime(dtDOConfirmDate.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noDO", hdnNoDO.Value);
                    ObjGlobal.Param.Add("kdConf", Kode);
                    ObjGlobal.Param.Add("tglConf", Convert.ToDateTime(dtDOConfirmDate.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("noCust", hdnNoCust.Value);
                    ObjGlobal.Param.Add("noCabang", hdnNoCabang.Value);
                    ObjGlobal.Param.Add("sts", "1");
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPInsertConfDO_H", ObjGlobal.Param);
                    DataSet mySettt = ObjDb.GetRows("select * from tConfDO_H where kdConf = '" + Kode + "'");
                    DataRow myRowtt = mySettt.Tables[0].Rows[0];
                    string noConf = myRowtt["noConf"].ToString();
                    ObjSys.UpdateAutoNumberCode("33", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));

                    for (int a = 0; a < grdDOConfirmDetail.Rows.Count; a++)
                    {
                        HiddenField hdnNoProduk = (HiddenField)grdDOConfirmDetail.Rows[a].FindControl("hdnNoProduk");
                        HiddenField hdnNoSA = (HiddenField)grdDOConfirmDetail.Rows[a].FindControl("hdnNoSA");
                        HiddenField hdnNoDOD = (HiddenField)grdDOConfirmDetail.Rows[a].FindControl("hdnNoDOD");
                        TextBox txtQtyConfrim = (TextBox)grdDOConfirmDetail.Rows[a].FindControl("txtQtyConfrim");
                        TextBox txtQtySatuanBesarConfrim = (TextBox)grdDOConfirmDetail.Rows[a].FindControl("txtQtySatuanBesarConfrim");
                        TextBox txtQtySatuanBesar1Confrim = (TextBox)grdDOConfirmDetail.Rows[a].FindControl("txtQtySatuanBesar1Confrim");

                        if (txtQtyConfrim.Text != "")
                        {
                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("noConf", noConf);
                            ObjGlobal.Param.Add("noDOD", hdnNoDOD.Value);
                            ObjGlobal.Param.Add("noProduct", hdnNoProduk.Value);
                            ObjGlobal.Param.Add("qtyConf", txtQtyConfrim.Text);
                            ObjGlobal.Param.Add("qtySatuanBesarConf", txtQtySatuanBesarConfrim.Text);
                            ObjGlobal.Param.Add("qtySatuanBesar1Conf", txtQtySatuanBesar1Confrim.Text);
                            ObjGlobal.GetDataProcedure("SPInsertConfDO_D", ObjGlobal.Param);
                        }


                    }

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noDO", hdnNoDO.Value);
                    ObjGlobal.Param.Add("sts", "1");
                    ObjGlobal.GetDataProcedure("SPUpdateDO_H", ObjGlobal.Param);


                   
                    ClearData();
                    LoadData();
                    ShowHideGridAndForm(true, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                }
                catch (Exception ex)
                {
                    if (valid == false)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", message);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", ex.ToString());
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideGridAndForm(true, false);
                ClearData();

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());

            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();

        }

        protected void grdDOView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDOView.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void grdDOView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = grdDOView.SelectedRow.RowIndex;
                hdnRowIndexShipment.Value = rowIndex.ToString();
                string itemID = grdDOView.DataKeys[rowIndex].Values[0].ToString();
                //HiddenField hdnNoMataUang = (HiddenField)grdDOView.Rows[rowIndex].FindControl("hdnNoMataUang");
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noDO", itemID);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataConfirmDO", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                hdnNoDO.Value = itemID;
                lblDONumber.Text = myRow["kdDO"].ToString();
                lblDoDate.Text = Convert.ToDateTime(myRow["tglDO"]).ToString("dd-MMM-yyyy");
                lblCustomerCode.Text = myRow["kdCust"].ToString();
                lblCustomer.Text = myRow["namaCust"].ToString();
                dtDOConfirmDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                hdnNoCust.Value = myRow["noCust"].ToString();
                hdnNoCabang.Value = myRow["noCabang"].ToString();
                LoadDataDetail();
                ShowHideGridAndForm(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
    }
}