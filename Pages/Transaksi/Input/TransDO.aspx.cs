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
    public partial class TransDO : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearch.Text);
            grdPOLocal.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataDO", ObjGlobal.Param);
            grdPOLocal.DataBind();
        }
        protected void LoadDataSupplier()
        {
            grdSupplier.DataSource = ObjDb.GetRowsDataTable("select noSupplier,kodeSupplier,namaSupplier,alamat from mSupplier where (kodeSupplier like '%" + txtSearchSupplier.Text + "%' or namaSupplier like '%" + txtSearchSupplier.Text + "%')");
            grdSupplier.DataBind();
        }
        protected void LoadDataCustomer()
        {
            grdCustomer.DataSource = ObjDb.GetRowsDataTable("select noCust,kdCust,namaCust,namaAlias,alamatCust from mCustomer");
            grdCustomer.DataBind();
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
            txtCatatan.Text = "";
            txtCustomerCode.Text = "";
            txtKodeSupplier.Text = "";
            txtNomorPolisi.Text = "";
            TxtSopir.Text = "";
            lblCustomerAddress.Text = "";
            lblCustomerName.Text = "";
            lblSupplierAddress.Text = "";
            lblSupplierName.Text = "";
            ViewState.Clear();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;

                if (dtDO.Text == "")
                {
                    message += ObjSys.CreateMessage("Tanggal tidak boleh kosong.");
                    valid = false;
                }
                if (valid == true)
                {
                    string Kode = ObjSys.GetCodeAutoNumberNew("32", Convert.ToDateTime(dtDO.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kdDO", Kode);
                    ObjGlobal.Param.Add("tglDO", dtDO.Text);
                    ObjGlobal.Param.Add("noPicking", hdnId.Value);
                    ObjGlobal.Param.Add("noCust", hdnNoCust.Value);
                    ObjGlobal.Param.Add("kepada", lblCustomerName.Text);
                    ObjGlobal.Param.Add("alamatPengiriman", lblCustomerAddress.Text);
                    ObjGlobal.Param.Add("noTelpon", "");
                    ObjGlobal.Param.Add("noSup", hdnNoSupplier.Value);
                    ObjGlobal.Param.Add("noPolisi", txtNomorPolisi.Text);
                    if (TxtSopir.Text != "")
                        ObjGlobal.Param.Add("sopir", TxtSopir.Text);
                    ObjGlobal.Param.Add("keterangan", txtCatatan.Text);
                    ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                    //if (ckbCoo.Checked == true)
                    //    ObjGlobal.Param.Add("cetaksuhu", "1");
                    //else
                    //    ObjGlobal.Param.Add("cetaksuhu", "0");
                    ObjGlobal.Param.Add("stsDO", "0");
                    ObjGlobal.Param.Add("stsInv", "0");
                    ObjGlobal.Param.Add("asal", "DO");
                    ObjGlobal.Param.Add("namaDriver", "");
                    ObjGlobal.Param.Add("noSupAs", hdnNoSup.Value);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);

                    ObjGlobal.ExecuteProcedure("SPInsertDO", ObjGlobal.Param);

                    ObjSys.UpdateAutoNumberCodeNew("32", Convert.ToDateTime(dtDO.Text).ToString("yyyy-MM-dd"));
                    //DataRow myRow = ObjDb.GetRows("select * from tDO_H where kdDO = '" + Kode + "'").Tables[0].Rows[0];
                    //for (int i = 0; i < grdDODetail.Rows.Count; i++)
                    //{
                    //    HiddenField hdnNoPickingD = (HiddenField)grdDODetail.Rows[i].FindControl("hdnNoPicking");
                    //    HiddenField hdnNoProduct = (HiddenField)grdDODetail.Rows[i].FindControl("hdnNoProduct");
                    //    HiddenField Hiddennowadah = (HiddenField)grdDODetail.Rows[i].FindControl("Hiddennowadah");
                    //    Label lblQtyPicking = (Label)grdDODetail.Rows[i].FindControl("lblQtyPicking");
                    //    TextBox txtKetLain = (TextBox)grdDODetail.Rows[i].FindControl("txtKetLain");

                    //    if (hdnNoPickingD.Value != "")
                    //    {
                    //        ObjGlobal.Param.Clear();
                    //        ObjGlobal.Param.Add("noDO", myRow["noDO"].ToString());
                    //        ObjGlobal.Param.Add("noPickingD", hdnNoPickingD.Value);
                    //        ObjGlobal.Param.Add("noProduct", hdnNoProduct.Value);
                    //        ObjGlobal.Param.Add("lotno", Hiddennowadah.Value);
                    //        ObjGlobal.Param.Add("qtyDO", Convert.ToDecimal(lblQtyPicking.Text).ToString());
                    //        ObjGlobal.Param.Add("sisaqtyDO", Convert.ToDecimal(lblQtyPicking.Text).ToString());
                    //        ObjGlobal.Param.Add("keterangan", txtKetLain.Text);
                    //        ObjGlobal.ExecuteProcedure("SPInsertDODetail", ObjGlobal.Param);
                    //    }
                    //}


                    //HttpContext.Current.Session["ParamReport"] = null;
                    //Session["REPORTNAME"] = null;
                    //Session["REPORTTITLE"] = null;
                    //Param.Clear();
                    //Param.Add("noDO", myRow["noDO"].ToString());
                    //HttpContext.Current.Session.Add("ParamReport", Param);
                    //Session["REPORTNAME"] = "RptDeliveryOrder.rpt";
                    //Session["REPORTTILE"] = "Report Delivery Order";
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);

                    LoadData();
                    ClearData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);

                    ShowMessage("success", "Data berhasil disimpan");
                    this.ShowHideGridAndForm(true, false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }


        protected void btnImgSelectSupplier_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataSupplier();
            dlgSupplier.Show();
        }

        protected void btnImgSelectCustomer_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataCustomer();
            dlgCustomer.Show();
        }

        protected void btnSearchSupplier_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataSupplier();
            dlgSupplier.Show();
        }

        protected void grdSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSupplier.PageIndex = e.NewPageIndex;
            LoadDataSupplier();
            dlgSupplier.Show();
        }

        protected void grdSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = grdSupplier.SelectedRow.RowIndex;

            txtKodeSupplier.Text = grdSupplier.SelectedRow.Cells[1].Text;
            lblSupplierName.Text = grdSupplier.SelectedRow.Cells[2].Text;
            lblSupplierAddress.Text = grdSupplier.SelectedRow.Cells[3].Text;
            hdnNoSupplier.Value = grdSupplier.DataKeys[rowIndex].Value.ToString();

            dlgSupplier.Hide();
        }

        protected void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataCustomer();
            dlgCustomer.Show();
        }

        protected void grdCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCustomer.PageIndex = e.NewPageIndex;
            LoadDataCustomer();
            dlgCustomer.Show();
        }

        protected void grdCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = grdCustomer.SelectedRow.RowIndex;

            txtCustomerCode.Text = grdCustomer.SelectedRow.Cells[1].Text;
            lblCustomerName.Text = grdCustomer.SelectedRow.Cells[2].Text;
            lblCustomerAddress.Text = grdCustomer.SelectedRow.Cells[3].Text;
            hdnNoCust.Value = grdCustomer.DataKeys[rowIndex].Value.ToString();

            dlgSupplier.Hide();
        }
        protected void grdDODetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                hdnRowIndex.Value = rowIndex.ToString();
                if (e.CommandName == "Browse")
                {
                    lblMessage.Text = "";
                    CloseMessage();
                    LoadDataPicking();
                    dlgPicking.Show();
                }
                else if (e.CommandName == "Empty")
                {
                    TextBox txtKodePicking = (TextBox)grdDODetail.Rows[rowIndex].FindControl("txtKodePicking");
                    HiddenField hdnNoPicking = (HiddenField)grdDODetail.Rows[rowIndex].FindControl("hdnNoPicking");
                    txtKodePicking.Text = "";
                    hdnNoPicking.Value = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void LoadDataPicking()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearchPicking.Text);
            ObjGlobal.Param.Add("noCust", hdnNoCust.Value);
            grdPicking.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadDataPickingDO", ObjGlobal.Param);
            grdPicking.DataBind();
        }
        protected void btnSearchPicking_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataPicking();
            dlgPicking.Show();
        }

        protected void grdPicking_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPicking.PageIndex = e.NewPageIndex;
            CloseMessage();
            LoadDataPicking();
            dlgPicking.Show();
        }

        protected void grdPicking_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool valid = true;
                string message = "";
                int rowIndex = Convert.ToInt32(hdnRowIndex.Value);
                HiddenField hdnNoPicking = (HiddenField)grdDODetail.Rows[rowIndex].FindControl("hdnNoPicking");
                HiddenField hdnNoProduct = (HiddenField)grdDODetail.Rows[rowIndex].FindControl("hdnNoProduct");
                TextBox txtKodePicking = (TextBox)grdDODetail.Rows[rowIndex].FindControl("txtKodePicking");
                Label lblQtyPicking = (Label)grdDODetail.Rows[rowIndex].FindControl("lblQtyPicking");
                Label lblTglPicking = (Label)grdDODetail.Rows[rowIndex].FindControl("lblTglPicking");
                Label lblProduct = (Label)grdDODetail.Rows[rowIndex].FindControl("lblProduct");
                TextBox txtQtyDO = (TextBox)grdPicking.Rows[grdPicking.SelectedRow.RowIndex].FindControl("txtQtyDO");
                if (Convert.ToInt32(txtQtyDO.Text) == 0)
                {
                    valid = false;
                    message += "Qty tidak boleh 0";
                }
                if (Convert.ToInt32(txtQtyDO.Text) > Convert.ToInt32(grdPicking.Rows[grdPicking.SelectedRow.RowIndex].Cells[10].Text))
                {
                    valid = false;
                    message += "Qty tidak boleh lebih dari Qty Picking";
                }
                if (valid == true)
                {
                    HiddenField hdnNoProductGrid = (HiddenField)grdPicking.Rows[grdPicking.SelectedRow.RowIndex].FindControl("hdnNoProduct");
                    hdnNoPicking.Value = grdPicking.DataKeys[grdPicking.SelectedRow.RowIndex].Value.ToString();
                    txtKodePicking.Text = grdPicking.Rows[grdPicking.SelectedRow.RowIndex].Cells[1].Text;
                    hdnNoProduct.Value = hdnNoProductGrid.Value;
                    lblQtyPicking.Text = txtQtyDO.Text;
                    lblTglPicking.Text = grdPicking.Rows[grdPicking.SelectedRow.RowIndex].Cells[2].Text;
                    lblProduct.Text = grdPicking.Rows[grdPicking.SelectedRow.RowIndex].Cells[6].Text;
                    dlgPicking.Hide();
                }
                else
                {
                    dlgPicking.Show();
                    lblMessage.Text = ObjSys.GetMessage("error", message);
                }
            }
            catch (Exception ex)
            {
                dlgPicking.Show();
                lblMessage.Text = ObjSys.GetMessage("error", ex.ToString());
            }
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void grdPOLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdPOLocal.SelectedRow.RowIndex;
                string noPicking = grdPOLocal.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noPicking;

                DataSet MySet = ObjDb.GetRows("select * from tPicking_H a inner join hSO b on a.noSO = b.noSO inner join mCustomer c on a.noCust = c.noCust Where a.noPicking = '" + noPicking + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0];

                    txtCustomerCode.Text = MyRow["kdCust"].ToString();
                    hdnNoCust.Value = MyRow["noCust"].ToString();
                    lblCustomerName.Text = MyRow["namaCust"].ToString();
                    lblCustomerAddress.Text = MyRow["alamatCust"].ToString();

                    grdDODetail.DataSource = ObjDb.GetRows("select a.*,d.*,b.namaBarang as prodnm,b.kodeBarang as manufactur,e.qtySatuanKecil from tPicking_D a " +
                        "inner join mBarang b on a.noProduct = b.noBarang " +
                        "inner join tPicking_H d on a.noPicking = d.noPicking " +
                        "inner join dSO e on e.noDetSO = a.noDetSO " +
                        "Where a.noPicking = '" + hdnId.Value + "'");
                    grdDODetail.DataBind();

                    //loadCombo();


                    CloseMessage();
                    this.ShowHideGridAndForm(false, true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Tidak ada.");
                }

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

        protected void grdPOLocal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPOLocal.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ShowHideGridAndForm(true, false);
            CloseMessage();
        }

        protected void cboJenis_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboKode.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct kddo id, kddo name FROM TDeletekodedo where sts = '" + cboJenis.SelectedValue + "' ) a");
            cboKode.DataValueField = "id";
            cboKode.DataTextField = "name";
            cboKode.DataBind();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataSupplierAs();
            dlgSupplierAs.Show();
        }
        protected void LoadDataSupplierAs()
        {
            grdSuppAsu.DataSource = ObjDb.GetRowsDataTable("select noSupplier,kodeSupplier,namaSupplier,alamat from mSupplier where (kodeSupplier like '%" + txtSearcAsu.Text + "%' or namaSupplier like '%" + txtSearcAsu.Text + "%')");
            grdSuppAsu.DataBind();
        }

        protected void btnAsu_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataSupplierAs();
            dlgSupplierAs.Show();
        }

        protected void grdSuppAsu_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdSuppAsu.PageIndex = e.NewPageIndex;
            LoadDataSupplierAs();
            dlgSupplierAs.Show();
        }

        protected void grdSuppAsu_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = grdSuppAsu.SelectedRow.RowIndex;

            txtNamaSup.Text = grdSuppAsu.SelectedRow.Cells[2].Text;
            hdnNoSup.Value = grdSuppAsu.DataKeys[rowIndex].Value.ToString();

            dlgSupplierAs.Hide();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }
    }
}