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
    public partial class TransPicking : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                loadDataCombo();
            }
        }

        private void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearch.Text);
            grdPOLocal.DataSource = ObjGlobal.GetDataProcedure("SPLoadPicking", ObjGlobal.Param);
            grdPOLocal.DataBind();

            for (int i = 0; i < grdPOLocal.Rows.Count; i++)
            {
                DropDownList cboSlcGudang = (DropDownList)grdPOLocal.Rows[i].FindControl("cboSlcGudang");
                HiddenField hdnTipe = (HiddenField)grdPOLocal.Rows[i].FindControl("hdnTipe");

                string sql = "";
                    sql = "select distinct a.noGudang as no , a.namaGudang as name from Gudang a WHERE a.noGudang IN (Select distinct noGudang FROM tSaldoAging) ";
             
                cboSlcGudang.DataSource = ObjDb.GetRows(sql);
                cboSlcGudang.DataValueField = "no";
                cboSlcGudang.DataTextField = "name";
                cboSlcGudang.DataBind();

            }
        }

        private void loadDataCombo()
        {
            cboGudang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct a.noUser id, a.namauser name from mUser a inner join tAkses b on a.noUser=b.noUser inner join mAkses c on b.noAkses=c.noAkses where c.noAkses=13) a");
            cboGudang.DataValueField = "id";
            cboGudang.DataTextField = "name";
            cboGudang.DataBind();


            cboGudang1.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct a.noUser id, a.namauser name from mUser a inner join tAkses b on a.noUser=b.noUser inner join mAkses c on b.noAkses=c.noAkses where c.noAkses=13) a");
            cboGudang1.DataValueField = "id";
            cboGudang1.DataTextField = "name";
            cboGudang1.DataBind();
        }
        protected void grdPOLocal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdPOLocal.PageIndex = e.NewPageIndex;
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
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivFormm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabFormm.Visible = DivFormm;
        }
        protected void grdPOLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdPOLocal.SelectedRow.RowIndex;
                string noSO = grdPOLocal.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noSO;

                DropDownList cboSlcGudang = (DropDownList)grdPOLocal.Rows[rowIndex].FindControl("cboSlcGudang");

                DataSet MySet = ObjDb.GetRows(
                        "select a.kdSO, a.tglSO, c.kdCust, c.namaCust, a.noPo, a.noCust " +
                        "from hSO a inner join mCustomer c on a.noCust = c.noCust " +
                        "where a.noSO = '" + noSO + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0];
                    lblTglSO.Text = Convert.ToDateTime(MyRow["tglSO"]).ToString("dd-MMM-yyyy");
                    lblNoSO.Text = MyRow["kdSO"].ToString();
                    lblKdCust.Text = MyRow["kdCust"].ToString();
                    lblNamaCust.Text = MyRow["namaCust"].ToString();
                    lblPOCust.Text = MyRow["noPo"].ToString();
                    hdnNoCust.Value = MyRow["noCust"].ToString();
                    dtPicking.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noSO", noSO);
                    ObjGlobal.Param.Add("noGudang", cboSlcGudang.Text);
                    grdPO.DataSource = ObjGlobal.GetDataProcedure("SPLoadPickingDetil", ObjGlobal.Param);
                    grdPO.DataBind();
                    
                    for (int i = 0; i < grdPO.Rows.Count; i++)
                    {
                        HiddenField hdnKonfersiBesar = (HiddenField)grdPO.Rows[i].FindControl("hdnKonfersiBesar");
                        TextBox lblQtySO = (TextBox)grdPO.Rows[i].FindControl("lblQtySO");
                        TextBox lblqtySatuanBesar1 = (TextBox)grdPO.Rows[i].FindControl("lblqtySatuanBesar1");
                        HiddenField hdnKonfersi1 = (HiddenField)grdPO.Rows[i].FindControl("hdnKonfersi1");
                        if (hdnKonfersiBesar.Value == "0")
                        {
                            lblQtySO.Enabled = false;
                        }else
                        {
                            lblQtySO.Enabled = true;
                        }

                        if (hdnKonfersi1.Value == "0")
                        {
                            lblqtySatuanBesar1.Enabled = false;
                        }
                        else
                        {
                            lblqtySatuanBesar1.Enabled = true;
                        }
                    }
                        

                        CloseMessage();
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
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false, false);
        }
        protected void btnDetail_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(false, false, true);
        }
        protected void grdPO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "detail")
            {
                hdnRowIndex.Value = rowIndex.ToString();

                string itemId = grdPO.DataKeys[rowIndex].Values[0].ToString();
                string nodetSO = grdPO.DataKeys[rowIndex].Values[1].ToString();

                TextBox txtQtyPick = (TextBox)grdPO.Rows[rowIndex].FindControl("txtQtyPick");
                HiddenField hdnNoGudang = (HiddenField)grdPO.Rows[rowIndex].FindControl("hdnNoGudang");

                DataSet MySet = ObjDb.GetRows("select *, (CONVERT(varchar(10), a.packing) +' '+ a.unitPacking +'/'+ a.ketPacking) as kemasan from Product a " +
                    "inner join mLokasiBarang d on a.noproduct = d.noproduct " +
                    "inner join LokasiGudang e on d.nogudang = e.noGudang and e.noLokasiGudang = d.noLokasiGudang " +
                    "inner join Gudang c on d.noGudang = c.noGudang WHERE a.noproduct = '" + itemId + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    if (txtQtyPick.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Tolong isi qty pick.");
                        return;
                    }
                    else
                    {
                        dlgDetil.Show();
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noSO", hdnId.Value);
                        ObjGlobal.Param.Add("nodetSO", nodetSO);
                        ObjGlobal.Param.Add("noProductParam", itemId);
                        DataSet QtyCheck = ObjGlobal.GetDataProcedure("SPLoadDataPicking_U_Check", ObjGlobal.Param);
                        string stock = QtyCheck.Tables[0].Rows[0]["totalSisaSASMTx"].ToString();
                        if (Convert.ToDecimal(stock) < Convert.ToDecimal(txtQtyPick.Text))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            ShowMessage("error", "Produk Kurang dari Permintaan Picking. Stok sementara = " + stock + ".");
                            return;
                        }

                        DataRow MyRow = MySet.Tables[0].Rows[0];
                        lblGudang.Text = MyRow["kdGudang"].ToString() + "-" + MyRow["namaGudang"].ToString();
                        lblManufature1.Text = MyRow["manufactur"].ToString();
                        lblkemasan.Text = MyRow["kemasan"].ToString();
                        lblProd.Text = "[" + MyRow["prodno"].ToString() + "]" + MyRow["prodnm"].ToString();

                        LoadDataDetil(nodetSO, itemId, txtQtyPick.Text, hdnNoGudang.Value);
                        CloseMessage();
                        this.ShowHideGridAndForm(false, true, true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Produk Sedang Kosong.");
                }

            }
        }

        protected void LoadDataDetil(string noDetSO = "", string itemId = "", string QtyPick = "", string Gudang = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noSO", hdnId.Value);
            ObjGlobal.Param.Add("noProductParam", itemId);
            ObjGlobal.Param.Add("nodetSO", noDetSO);
            ObjGlobal.Param.Add("qtyPick", QtyPick);
            ObjGlobal.Param.Add("noGudang", Gudang);
            grdPicking1.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPicking_U", ObjGlobal.Param);
            grdPicking1.DataBind();
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            bool valid = true;
            ObjDb.Data.Clear();

            try
            {

               
                if (valid == true)
                {
                    string Kode = ObjSys.GetCodeAutoNumberNew("32", Convert.ToDateTime(dtPicking.Text).ToString("yyyy-MM-dd"));

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kdPicking", Kode);
                    ObjGlobal.Param.Add("tglPicking", dtPicking.Text);
                    ObjGlobal.Param.Add("noSO", hdnId.Value);
                    ObjGlobal.Param.Add("noCust", hdnNoCust.Value);
                    ObjGlobal.Param.Add("keterangan", txtKeterangan.Text);
                    ObjGlobal.Param.Add("nosales", cboGudang.Text);
                    ObjGlobal.Param.Add("nosales1", cboGudang1.Text);
                    ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("stsPicking", "1");
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("createdDate", ObjSys.GetNow);
                    ObjGlobal.ExecuteProcedure("SPInserttPickingh", ObjGlobal.Param);
                   

                    //--------------
                    DataSet mySetH = ObjDb.GetRows("select * from tPicking_H where kdPicking = '" + Kode + "'");
                    DataRow myRowH = mySetH.Tables[0].Rows[0];
                    string noPicking = myRowH["noPicking"].ToString();

                    ObjSys.UpdateAutoNumberCode("32", Convert.ToDateTime(dtPicking.Text).ToString("yyyy-MM-dd"));
                    for (int i = 0; i < grdPO.Rows.Count; i++)
                    {
                        HiddenField hdnDetSO = (HiddenField)grdPO.Rows[i].FindControl("hdnDetSO");


                        HiddenField hdnNoProduct = (HiddenField)grdPO.Rows[i].FindControl("hdnNoProduct");
                        TextBox lblManufacture = (TextBox)grdPO.Rows[i].FindControl("lblManufacture");
                        TextBox lblqtySatuanBesar1 = (TextBox)grdPO.Rows[i].FindControl("lblqtySatuanBesar1");
                        TextBox lblQtySO = (TextBox)grdPO.Rows[i].FindControl("lblQtySO");
                        Label lblSisaQty = (Label)grdPO.Rows[i].FindControl("lblSisaQty");
                        HiddenField hdnNoGudang = (HiddenField)grdPO.Rows[i].FindControl("hdnNoGudang");
                        HiddenField hdnNoLok = (HiddenField)grdPO.Rows[i].FindControl("hdnNoLok");
                        TextBox txtQtyPick = (TextBox)grdPO.Rows[i].FindControl("txtQtyPick");
                        HiddenField hdnNoSAH = (HiddenField)grdPO.Rows[i].FindControl("hdnNoSAH");

                        HiddenField hdnKonfersiBesar = (HiddenField)grdPO.Rows[i].FindControl("hdnKonfersiBesar");
                        HiddenField hdnKonfersi1 = (HiddenField)grdPO.Rows[i].FindControl("hdnKonfersi1");


                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noPicking", noPicking);
                            ObjDb.Data.Add("noDetSO", hdnDetSO.Value);
                            ObjDb.Data.Add("noproduct", hdnNoProduct.Value);
                            ObjDb.Data.Add("qty", Convert.ToDecimal(lblManufacture.Text).ToString());
                            if(hdnKonfersiBesar.Value == "0")
                            {
                                ObjDb.Data.Add("qtySatuanBesar", "0");
                            }else
                            {
                                ObjDb.Data.Add("qtySatuanBesar", Convert.ToDecimal(lblQtySO.Text).ToString());
                            }

                            if (hdnKonfersi1.Value == "0")
                            {
                                ObjDb.Data.Add("qtySatuanBesar1", "0");
                            }
                            else
                            {
                                ObjDb.Data.Add("qtySatuanBesar1", Convert.ToDecimal(lblqtySatuanBesar1.Text).ToString());
                            }

                        
                            ObjDb.Data.Add("noGudang", hdnNoGudang.Value);
                            ObjDb.Data.Add("nolokasigudang", hdnNoLok.Value);
                            //ObjDb.Data.Add("sisaqtypicking", Convert.ToDecimal(lblSisaQty.Text).ToString());
                            ObjDb.Insert("tPicking_D", ObjDb.Data);

                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("noPicking", noPicking);
                            ObjGlobal.ExecuteProcedure("SPInserttPickingsa", ObjGlobal.Param);





                    }
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil di simpan.");
                    LoadData();
                    this.ShowHideGridAndForm(true, false, false);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdPO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnNoProduct = (HiddenField)e.Row.FindControl("hdnNoProduct");
                HiddenField hdnDetSO = (HiddenField)e.Row.FindControl("hdnDetSO");
                TextBox txtQtyPick = (TextBox)e.Row.FindControl("txtQtyPick");

                txtQtyPick.Enabled = false;
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noSO", hdnId.Value);
                ObjGlobal.Param.Add("nodetSO", hdnDetSO.Value);
                ObjGlobal.Param.Add("noProductParam", hdnNoProduct.Value);
                DataSet QtyCheck = ObjGlobal.GetDataProcedure("SPLoadDataPicking_U_Check", ObjGlobal.Param);
                if (QtyCheck.Tables[0].Rows.Count > 0)
                {
                    txtQtyPick.Enabled = true;
                }


            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            int cek = 0; string noSA = "";
            for (int i = 0; i < grdPicking1.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)grdPicking1.Rows[i].FindControl("chkSelect");
                HiddenField hdnNoSA = (HiddenField)grdPicking1.Rows[i].FindControl("hdnNoSA");

                if (chkSelect.Checked == true)
                {
                    cek++;
                    noSA += hdnNoSA.Value + ",";
                }
            }

            int rowIndexHdn = Convert.ToInt32(hdnRowIndex.Value);
            CheckBox chkSelectH = (CheckBox)grdPO.Rows[rowIndexHdn].FindControl("chkSelectH");
            HiddenField hdnNoSAH = (HiddenField)grdPO.Rows[rowIndexHdn].FindControl("hdnNoSAH");
            if (cek != 0)
            {
                chkSelectH.Checked = true;
                hdnNoSAH.Value = noSA;
            }
            else
            {
                chkSelectH.Checked = false;
                hdnNoSAH.Value = "";
            }
            this.ShowHideGridAndForm(false, true, false);
        }

        protected void btnKembali_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(false, true, false);
        }

        //protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        //{
        //    decimal totQty = 0;
        //    for (int i = 0; i < grdPicking1.Rows.Count; i++)
        //    {
        //        CheckBox chkSelect = (CheckBox)grdPicking1.Rows[i].Cells[0].FindControl("chkSelect");
        //        TextBox txtQtyPick = (TextBox)grdPicking1.Rows[i].Cells[0].FindControl("txtQtyPick");

        //        if (chkSelect.Checked == true)
        //        {
        //            totQty += Convert.ToDecimal(txtQtyPick.Text);
        //        }
        //    }
        //    txtTotalQty.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totQty).ToString());
        //    dlgDetil.Show();

        //}
    }
}