using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

namespace eFinance.Pages.Master.Input
{
    public partial class MstProductLocation : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstProductLocation.aspx");
                loadData();
            }
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdProdLoc.DataSource = dt;
            grdProdLoc.DataBind();
        }
        private void SetPreviousData()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList cboGudang = (DropDownList)grdProdLoc.Rows[i].FindControl("cboGudang");
                        DropDownList box4 = (DropDownList)grdProdLoc.Rows[i].FindControl("cboLokasiGudang");

                        cboGudang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noGudang id, namaGudang name FROM Gudang ) a");
                        cboGudang.DataValueField = "id";
                        cboGudang.DataTextField = "name";
                        cboGudang.DataBind();

                        cboGudang.Text = dt.Rows[i]["Column1"].ToString();
                        box4.Text = dt.Rows[i]["Column2"].ToString();
                    }
                }
            }
        }
        private void AddNewRow()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList box1 = (DropDownList)grdProdLoc.Rows[i].FindControl("cboGudang");
                        DropDownList box4 = (DropDownList)grdProdLoc.Rows[i].FindControl("cboLokasiGudang");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = box1.Text;
                        dtCurrentTable.Rows[i]["Column2"] = box4.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdProdLoc.DataSource = dtCurrentTable;
                    grdProdLoc.DataBind();
                }
            }
            SetPreviousData();
        }
        private void loadData()
        {
            grdVoucher.DataSource = ObjDb.GetRows("SELECT * from mBarang where sts = '1' AND (kodebarang LIKE '%" + txtSearch.Text + "%' or namabarang LIKE '%" + txtSearch.Text + "%') ");
            grdVoucher.DataBind();
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
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

        protected void grdVoucher_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdVoucher.SelectedRow.RowIndex;
                string noBarang = grdVoucher.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noBarang;
                CloseMessage();
                DataSet MySet = ObjDb.GetRows("select * from mbarang where noBarang ='" + noBarang + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0];
                    hdnnobrg.Value = MyRow["noBarang"].ToString();
                    lblKodeBarang.Text = MyRow["kodebarang"].ToString();
                    lblNamaBarang.Text = MyRow["namabarang"].ToString();
                    //lblJenis.Text = MyRow["groups"].ToString();
                    //hdnSuhuLink.Value = MyRow["suhuLink"].ToString();
                    //hdnNolay.Value = MyRow["layer_plt"].ToString();
                    //hdnNolayy.Value = MyRow["qty_layer"].ToString();

                    SetInitialRow();
                    for (int i = 1; i < 5; i++)
                    {
                        AddNewRow();
                    }


                    this.ShowHideGridAndForm(false, true, false);
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

        protected void btnCari_Click(object sender, EventArgs e)
        {
            loadData();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false, false);
            ClearData();
            loadData();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (txtMinStok.Text == "")
            {
                message += ObjSys.CreateMessage("Stok tidak boleh kosong.");
                valid = false;
            }
            if (txtKapasitas.Text == "")
            {
                message += ObjSys.CreateMessage("Kapasitas tidak boleh kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                    int count = 0;
                    for (int i = 0; i < grdProdLoc.Rows.Count; i++)
                    {
                        DropDownList cboGudang = (DropDownList)grdProdLoc.Rows[i].FindControl("cboGudang");
                        DropDownList cboLokasiGudang = (DropDownList)grdProdLoc.Rows[i].FindControl("cboLokasiGudang");
                        HiddenField hdnsuhu = (HiddenField)grdProdLoc.Rows[i].FindControl("hdnsuhu");
                        HiddenField hdnSuhuKe = (HiddenField)grdProdLoc.Rows[i].FindControl("hdnSuhuKe");

                        //if (Convert.ToDecimal(hdnSuhuLink.Value) >= Convert.ToDecimal(hdnsuhu.Value) && Convert.ToDecimal(hdnSuhuLink.Value) <= Convert.ToDecimal(hdnSuhuKe.Value))
                        //{
                        //    count += 1;
                        //}
                    }
                    //if (count == 0)
                    //{
                    //    message += ObjSys.CreateMessage("Suhu tidak sesuai");
                    //    valid = false;
                    //    string error = Convert.ToInt32(DateTime.Now.ToString()).ToString();
                    //}
                    //else
                    //{
                        for (int i = 0; i < grdProdLoc.Rows.Count; i++)
                        {
                            DropDownList cboGudang = (DropDownList)grdProdLoc.Rows[i].FindControl("cboGudang");
                            DropDownList cboLokasiGudang = (DropDownList)grdProdLoc.Rows[i].FindControl("cboLokasiGudang");
                            HiddenField hdnsuhu = (HiddenField)grdProdLoc.Rows[i].FindControl("hdnsuhu");

                            if (cboLokasiGudang.SelectedValue != "")
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("noBarang", hdnnobrg.Value);
                                ObjDb.Data.Add("noLokasiGudang", cboLokasiGudang.SelectedValue);
                                ObjDb.Data.Add("noGudang", cboGudang.SelectedValue);
                                ObjDb.Data.Add("minSafety", txtMinStok.Text);
                                ObjDb.Data.Add("kapasitas", txtKapasitas.Text);
                                ObjDb.Data.Add("noCabang", "1");
                                ObjDb.Data.Add("stsDefaultLokBar", "1");
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Insert("mLokasiBarang", ObjDb.Data);

                            }

                        }

                        ShowMessage("success", "Data berhasil disimpan.");
                        this.ShowHideGridAndForm(true, false, false);
                        ClearData();
                        loadData();
                    //}
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
        private void ClearData()
        {
            txtMinStok.Text = "";
            txtKapasitas.Text = "";
        }
        protected void cboGudang_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboGudang = (DropDownList)sender;
            var row = (GridViewRow)cboGudang.NamingContainer;

            DropDownList Gudang = (DropDownList)row.FindControl("cboGudang");
            DropDownList cboLokasiGudang = (DropDownList)row.FindControl("cboLokasiGudang");

            cboLokasiGudang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noLokasiGudang id, namaLokGud name FROM LokasiGudang where noGudang = '" + Gudang.SelectedValue + "' ) a");
            cboLokasiGudang.DataValueField = "id";
            cboLokasiGudang.DataTextField = "name";
            cboLokasiGudang.DataBind();

        }

        protected void cboLokasiGudang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                DropDownList cboLokasiGudang = (DropDownList)sender;
                var row = (GridViewRow)cboLokasiGudang.NamingContainer;

                DropDownList cboGudang = (DropDownList)row.FindControl("cboGudang");
                DropDownList LokasiGudang = (DropDownList)row.FindControl("cboLokasiGudang");

                for (int i = 0; i < grdProdLoc.Rows.Count; i++)
                {
                    DropDownList GudanGrid = (DropDownList)grdProdLoc.Rows[i].FindControl("cboGudang");
                    DropDownList LokasiGudangGrid = (DropDownList)grdProdLoc.Rows[i].FindControl("cboLokasiGudang");
                    DataSet MySet = ObjDb.GetRows("select * from mLokasiBarang where noBarang = " + hdnnobrg.Value + " and noLokasiGudang ='" + LokasiGudangGrid.SelectedValue + "'");
                    if (MySet.Tables[0].Rows.Count > 0)
                    {
                        message += ObjSys.CreateMessage("Lokasi tidak boleh sama.");
                        valid = false;
                        string error = Convert.ToInt32(DateTime.Now.ToString()).ToString();
                        btnSimpan.Enabled = false;
                    }
                    else
                    {

                        CloseMessage();
                        btnSimpan.Enabled = true;

                    }

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

    }
}