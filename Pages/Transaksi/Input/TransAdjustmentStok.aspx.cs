using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransAdjustmentStok : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        #region rows
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string)));
            dt.Columns.Add(new DataColumn("Column5", typeof(string)));
            dt.Columns.Add(new DataColumn("Column6", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dr["Column4"] = string.Empty;
            dr["Column5"] = string.Empty;
            dr["Column6"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdAdj.DataSource = dt;
            grdAdj.DataBind();
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
                        TextBox txtBatch = (TextBox)grdAdj.Rows[i].FindControl("txtBatch");
                        HiddenField hdnProduct = (HiddenField)grdAdj.Rows[i].FindControl("hdnProduct");
                        Label txtProductName = (Label)grdAdj.Rows[i].FindControl("txtProductName");
                        Label lblonHand = (Label)grdAdj.Rows[i].FindControl("lblonHand");
                        TextBox lblQty = (TextBox)grdAdj.Rows[i].FindControl("txtQty");
                        Label lblonHold = (Label)grdAdj.Rows[i].FindControl("lblonHold");

                        txtBatch.Text = dt.Rows[i]["Column1"].ToString();
                        hdnProduct.Value = dt.Rows[i]["Column2"].ToString();
                        txtProductName.Text = dt.Rows[i]["Column3"].ToString();
                        lblQty.Text = dt.Rows[i]["Column4"].ToString();
                        lblonHand.Text = dt.Rows[i]["Column5"].ToString();
                        lblonHold.Text = dt.Rows[i]["Column6"].ToString();
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
                        TextBox txtBatch = (TextBox)grdAdj.Rows[i].FindControl("txtBatch");
                        HiddenField hdnProduct = (HiddenField)grdAdj.Rows[i].FindControl("hdnProduct");
                        Label txtProductName = (Label)grdAdj.Rows[i].FindControl("txtProductName");
                        Label lblonHand = (Label)grdAdj.Rows[i].FindControl("lblonHand");
                        TextBox lblQty = (TextBox)grdAdj.Rows[i].FindControl("txtQty");
                        Label lblonHold = (Label)grdAdj.Rows[i].FindControl("lblonHold");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtBatch.Text;
                        dtCurrentTable.Rows[i]["Column2"] = hdnProduct.Value;
                        dtCurrentTable.Rows[i]["Column3"] = txtProductName.Text;
                        dtCurrentTable.Rows[i]["Column4"] = lblQty.Text;
                        dtCurrentTable.Rows[i]["Column5"] = lblonHand.Text;
                        dtCurrentTable.Rows[i]["Column6"] = lblonHold.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdAdj.DataSource = dtCurrentTable;
                    grdAdj.DataBind();
                }
            }
            SetPreviousData();
        }
        #endregion

        #region PopUp
        private void LoadDataPanel()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nogudang", cboGudang.Text);
            ObjGlobal.Param.Add("jnsTrans", cboJenis.Text);
            ObjGlobal.Param.Add("search", txtSearch.Text);
            grdProduct.DataSource = ObjGlobal.GetDataProcedure("SPLoadDetilAdjustStok", ObjGlobal.Param);
            grdProduct.DataBind();

            LoadColumn();
        }

        protected void btnCariProduct_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataPanel();
            mpe.Show();
        }

        protected void grdProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(txtHdnPopup.Value);
            bool valid = true;
            string message = "";
            string prodnm = "", noprod = "", nobatch = "", onHold = "", onHand = "", SupCust, unit="", unitBesar="", unitBesar1="", konfersiBesar="", konfersiBesar1="";
            prodnm = (grdProduct.SelectedRow.FindControl("lblNama") as Label).Text;
            noprod = (grdProduct.SelectedRow.FindControl("hdnnoProduk") as HiddenField).Value;
            nobatch = (grdProduct.SelectedRow.FindControl("lblBatchNo") as Label).Text;
            onHold = (grdProduct.SelectedRow.FindControl("hdnOnHold") as HiddenField).Value;
            onHand = (grdProduct.SelectedRow.FindControl("hdnOnHand") as HiddenField).Value;
            unit = (grdProduct.SelectedRow.FindControl("hdnSatuan") as HiddenField).Value;
            unitBesar = (grdProduct.SelectedRow.FindControl("hdnSatuanBesar") as HiddenField).Value;
            unitBesar1 = (grdProduct.SelectedRow.FindControl("hdnSatuanBesar1") as HiddenField).Value;
            konfersiBesar = (grdProduct.SelectedRow.FindControl("hdnkonfersiBesar") as HiddenField).Value;
            konfersiBesar1 = (grdProduct.SelectedRow.FindControl("hdnkonfersiBesar1") as HiddenField).Value;

            TextBox txtBatch = (TextBox)grdAdj.Rows[rowIndex - 1].FindControl("txtBatch");
            Label txtProductName = (Label)grdAdj.Rows[rowIndex - 1].FindControl("txtProductName");
            HiddenField hdnProduct = (HiddenField)grdAdj.Rows[rowIndex - 1].FindControl("hdnProduct");
            Label lblonHand = (Label)grdAdj.Rows[rowIndex - 1].FindControl("lblonHand");
            Label lblonHold = (Label)grdAdj.Rows[rowIndex - 1].FindControl("lblonHold");
            TextBox lblUnit = (TextBox)grdAdj.Rows[rowIndex - 1].FindControl("lblUnit");
            TextBox lblUnitSatuanBesar = (TextBox)grdAdj.Rows[rowIndex - 1].FindControl("lblUnitSatuanBesar");
            TextBox lblUnitSatuanBesar1 = (TextBox)grdAdj.Rows[rowIndex - 1].FindControl("lblUnitSatuanBesar1");
            HiddenField tkonfersiBesar1 = (HiddenField)grdAdj.Rows[rowIndex - 1].FindControl("konfersiBesar1");
            HiddenField tkonfersiBesar = (HiddenField)grdAdj.Rows[rowIndex - 1].FindControl("konfersiBesar");
            TextBox txtQtySatuanBesar1 = (TextBox)grdAdj.Rows[rowIndex - 1].FindControl("txtQtySatuanBesar1");
            TextBox txtQtySatuanBesar = (TextBox)grdAdj.Rows[rowIndex - 1].FindControl("txtQtySatuanBesar");

            for (int i = 0; i < grdAdj.Rows.Count; i++)
            {
                TextBox Batch = (TextBox)grdAdj.Rows[i].Cells[1].FindControl("txtBatch");
                if (Batch.Text == nobatch)
                {
                    message += ObjSys.CreateMessage("Batch : " + txtBatch + " sudah terpilih.");
                    valid = false;
                }
            }

            if (valid == true)
            {
                if (konfersiBesar1 == "0")
                {
                    txtQtySatuanBesar1.Enabled = false;
                }
                else
                {
                    txtQtySatuanBesar1.Enabled = true;
                }
                if (konfersiBesar == "0")
                {
                    txtQtySatuanBesar.Enabled = false;
                }
                else
                {
                    txtQtySatuanBesar.Enabled = true;
                }
                txtBatch.Text = nobatch;
                txtProductName.Text = prodnm;
                hdnProduct.Value = noprod;
                lblonHand.Text = onHand;
                lblonHold.Text = onHold;
                lblUnit.Text = unit;
                lblUnitSatuanBesar.Text = unitBesar;
                lblUnitSatuanBesar1.Text = unitBesar1;
                tkonfersiBesar1.Value = konfersiBesar1;
                tkonfersiBesar.Value = konfersiBesar;
                lblMessageError.Visible = false;
                mpe.Hide();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "", "", true);

            }
            else
            {
                mpe.Show();
                lblMessageError.Text = ObjSys.GetMessage("error", message);
                lblMessageError.Visible = true;
            }


        }

        protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdProduct.PageIndex = e.NewPageIndex;
            LoadDataPanel();
            mpe.Show();
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetInitialRow();
                for (int i = 1; i < 5; i++)
                {
                    AddNewRow();
                }
                loadCombo();
                showhideklasifikasiIn.Visible = false;
                showhideklasifikasiOut.Visible = false;
                showhideLain.Visible = false;
            }
        }
        private void loadCombo()
        {
            cboGudang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Gudang---' name union all SELECT distinct noGudang id, namaGudang name FROM Gudang ) a");
            cboGudang.DataValueField = "id";
            cboGudang.DataTextField = "name";
            cboGudang.DataBind();

            cboKlasifikasiOut.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Klasifikasi Keluar---' name union all SELECT distinct namatransaksi id, namatransaksi name FROM mJenistransaksiadj where jenistransaksi = 2 ) a");
            cboKlasifikasiOut.DataValueField = "id";
            cboKlasifikasiOut.DataTextField = "name";
            cboKlasifikasiOut.DataBind();

            cboKlasifikasiIn.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Klasifikasi Masuk---' name union all SELECT distinct namatransaksi id, namatransaksi name FROM mJenistransaksiadj where jenistransaksi = 1 ) a");
            cboKlasifikasiIn.DataValueField = "id";
            cboKlasifikasiIn.DataTextField = "name";
            cboKlasifikasiIn.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (cboGudang.Text == "0")
            {
                message += ObjSys.CreateMessage("Gudang tidak boleh kosong.");
                valid = false;
            }
            if (dtAdjustment.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal tidak boleh kosong.");
                valid = false;
            }
            if (cboJenis.Text == "")
            {
                message += ObjSys.CreateMessage("Jenis harus dipilih.");
                valid = false;
            }
            if (cboJenis.Text == "0")
            {
                if (cboKlasifikasiOut.Text == "")
                {
                    message += ObjSys.CreateMessage("Klasifikasi keluar harus dipilih.");
                    valid = false;
                }
            }
            if (cboJenis.Text == "1")
            {
                if (cboKlasifikasiIn.Text == "")
                {
                    message += ObjSys.CreateMessage("Klasifikasi masuk harus dipilih.");
                    valid = false;
                }
            }


            if (valid == true)
            {
                try
                {

                    ObjDb.Data.Clear();
                    string Kode = "";

                    Kode = ObjSys.GetCodeAutoNumber("1", Convert.ToDateTime(dtAdjustment.Text).ToString("yyyy-MM-dd"));
                    ObjDb.Data.Add("kdAdjsMent",Kode);
                    ObjDb.Data.Add("tglAdjsment", Convert.ToDateTime(dtAdjustment.Text).ToString("yyyy-MM-dd"));
                    ObjDb.Data.Add("noGudang", cboGudang.Text);
                    ObjDb.Data.Add("jenis", cboJenis.Text);
                    if(cboJenis.Text == "0")
                    {
                        ObjDb.Data.Add("klasifikasi", cboKlasifikasiOut.Text);
                    }
                    else
                    {
                        ObjDb.Data.Add("klasifikasi", cboKlasifikasiIn.Text);
                    }


                    ObjDb.Data.Add("catatanAdjsment", txtUraian.Text);
                    ObjDb.Data.Add("stsAdjsment", "0");
                    ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
          
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                    ObjDb.Insert("tAdjsment_h", ObjDb.Data);
                   
                    DataSet mySet = ObjDb.GetRows("select * from tAdjsment_h where kdAdjsMent = '" + Kode + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    string Id = myRow["noAdjsMent"].ToString();
                    ObjSys.UpdateAutoNumberCode("1", Convert.ToDateTime(dtAdjustment.Text).ToString("yyyy-MM-dd"));

                    for (int i = 0; i < grdAdj.Rows.Count; i++)
                    {
                        HiddenField hdnProduct = (HiddenField)grdAdj.Rows[i].FindControl("hdnProduct");
                        TextBox txtBatch = (TextBox)grdAdj.Rows[i].FindControl("txtBatch");
                        Label txtProductName = (Label)grdAdj.Rows[i].FindControl("txtProductName");
                        TextBox txtQty = (TextBox)grdAdj.Rows[i].FindControl("txtQty");
                        Label lblonHand = (Label)grdAdj.Rows[i].FindControl("lblonHand");
                        Label lblonHold = (Label)grdAdj.Rows[i].FindControl("lblonHold");
                        TextBox txtQtySatuan = (TextBox)grdAdj.Rows[i].FindControl("txtQtySatuan");
                        TextBox lblUnit = (TextBox)grdAdj.Rows[i].FindControl("lblUnit");
                        TextBox txtQtySatuanBesar = (TextBox)grdAdj.Rows[i].FindControl("txtQtySatuanBesar");
                        TextBox lblUnitSatuanBesar = (TextBox)grdAdj.Rows[i].FindControl("lblUnitSatuanBesar");
                        TextBox txtQtySatuanBesar1 = (TextBox)grdAdj.Rows[i].FindControl("txtQtySatuanBesar1");
                        TextBox lblUnitSatuanBesar1 = (TextBox)grdAdj.Rows[i].FindControl("lblUnitSatuanBesar1");
                        if (txtBatch.Text != "")
                        {
                                ObjDb.Data.Clear();
                            ObjDb.Data.Add("noAdjsment", Id);
                            ObjDb.Data.Add("noBarang", hdnProduct.Value);
                            ObjDb.Data.Add("qty", Convert.ToDecimal(txtQtySatuan.Text).ToString());
                            ObjDb.Data.Add("qty1", Convert.ToDecimal(txtQtySatuanBesar.Text).ToString());
                            ObjDb.Data.Add("qty2", Convert.ToDecimal(txtQtySatuanBesar1.Text).ToString());
                            ObjDb.Data.Add("total", Convert.ToDecimal(txtQty.Text).ToString());
                            ObjDb.Insert("tAdjsment_d", ObjDb.Data);

                        }
                    }


                    //ObjSys.UpdateAutoNumber("1", Convert.ToDateTime(dtAdjustment.Text).ToString("yyyy-MM-dd"));
                    ShowMessage("success", "Data berhasil disimpan.");
                    clearData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
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

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void grdAdj_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataPanel();
            mpe.Show();
            string value = (grdAdj.SelectedRow.FindControl("txtHdnValue") as HiddenField).Value;
            txtHdnPopup.Value = value;

            LoadColumn();
        }

        protected void grdAdj_RowCommand(object sender, GridViewCommandEventArgs e)

        {

            if (e.CommandName == "Clear")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                HiddenField hdnProduct = (HiddenField)grdAdj.Rows[rowIndex].FindControl("hdnProduct");
                HiddenField konfersiBesar1 = (HiddenField)grdAdj.Rows[rowIndex].FindControl("konfersiBesar1");
                HiddenField konfersiBesar = (HiddenField)grdAdj.Rows[rowIndex].FindControl("konfersiBesar");

                TextBox txtBatch = (TextBox)grdAdj.Rows[rowIndex].FindControl("txtBatch");
                Label txtProductName = (Label)grdAdj.Rows[rowIndex].FindControl("txtProductName");

                TextBox txtQtySatuan = (TextBox)grdAdj.Rows[rowIndex].FindControl("txtQtySatuan");
                TextBox lblUnit = (TextBox)grdAdj.Rows[rowIndex].FindControl("lblUnit");
                TextBox txtQtySatuanBesar = (TextBox)grdAdj.Rows[rowIndex].FindControl("txtQtySatuanBesar");
                TextBox lblUnitSatuanBesar = (TextBox)grdAdj.Rows[rowIndex].FindControl("lblUnitSatuanBesar");
                TextBox txtQtySatuanBesar1 = (TextBox)grdAdj.Rows[rowIndex].FindControl("txtQtySatuanBesar1");
                TextBox lblUnitSatuanBesar1 = (TextBox)grdAdj.Rows[rowIndex].FindControl("lblUnitSatuanBesar1");
                Label lblonHand = (Label)grdAdj.Rows[rowIndex].FindControl("lblonHand");
                Label txtTolblonHoldtal = (Label)grdAdj.Rows[rowIndex].FindControl("lblonHold");
                TextBox txtQty = (TextBox)grdAdj.Rows[rowIndex].FindControl("txtQty");

                hdnProduct.Value = "";
                konfersiBesar.Value = "";
                konfersiBesar1.Value = "";
                txtQtySatuan.Text = "0";
                txtQtySatuanBesar.Text = "0";
                txtQtySatuanBesar1.Text = "0";
                lblUnit.Text = "";
                lblUnitSatuanBesar.Text = "";
                lblUnitSatuanBesar1.Text = "";
                txtBatch.Text = "";
                txtProductName.Text = "";
                lblonHand.Text = "";
                txtTolblonHoldtal.Text = "";
                txtQty.Text = "0";
            }
        }
        protected void cboJenis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboJenis.Text == "0")
            {
                showhideklasifikasiIn.Visible = false;
                showhideklasifikasiOut.Visible = true;
                showhideLain.Visible = false;
               
            }
            else if (cboJenis.Text == "1")
            {
                showhideklasifikasiIn.Visible = true;
                showhideklasifikasiOut.Visible = false;
                showhideLain.Visible = false;

            }
            else
            {
                showhideklasifikasiIn.Visible = false;
                showhideklasifikasiOut.Visible = false;
                showhideLain.Visible = false;
            }

            SetInitialRow();
            for (int i = 1; i < 5; i++)
            {
                AddNewRow();
            }

            LoadColumn();
        }

        protected void cboKlasifikasiIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadketLainIn(cboKlasifikasiIn.Text);

            SetInitialRow();
            for (int i = 1; i < 5; i++)
            {
                AddNewRow();
            }

            LoadColumn();
        }

        protected void cboKlasifikasiOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadketLainOut(cboKlasifikasiOut.Text);

            SetInitialRow();
            for (int i = 1; i < 5; i++)
            {
                AddNewRow();
            }

            LoadColumn();
        }

        protected void loadketLainIn(string In = "")
        {
            if (In == "0")
                showhideLain.Visible = true;
            else if (In != "0")
                showhideLain.Visible = false;
        }

        protected void loadketLainOut(string Out = "")
        {
            if (Out == "0")
                showhideLain.Visible = true;
            else
                showhideLain.Visible = false;
        }
        private void clearData()
        {
            dtAdjustment.Text = "";
            cboJenis.Text = "";
            txtUraian.Text = "";
           
            txtSearch.Text = "";
            SetInitialRow();
            for (int i = 1; i < 5; i++)
            {
                AddNewRow();
            }

            //SetInitialRowx();
            //for (int i = 1; i < 1; i++)
            //{
            //    AddNewRowx();
            //}

            //txtAmount2.Text = "0.00";
            showhideklasifikasiIn.Visible = false;
            showhideklasifikasiOut.Visible = false;

        }
        protected void LoadColumn()
        {

            for (int i = 0; i < grdProduct.Rows.Count; i++)
            {
                if (cboJenis.SelectedValue == "0" && cboKlasifikasiOut.SelectedValue == "1")
                {
                    grdProduct.Columns[1].HeaderText = "Batch No";
                    grdProduct.Columns[2].HeaderText = "Product Name";
                    grdProduct.Columns[3].HeaderText = "Suplier";
                    grdProduct.Columns[3].Visible = true;
                }
                else if (cboJenis.SelectedValue == "1" && cboKlasifikasiIn.SelectedValue == "1")
                {
                    grdProduct.Columns[1].HeaderText = "Batch No";
                    grdProduct.Columns[2].HeaderText = "Product Name";
                    grdProduct.Columns[3].HeaderText = "Customer";
                    grdProduct.Columns[3].Visible = true;
                }
                else
                {
                    grdProduct.Columns[1].HeaderText = "Batch No";
                    grdProduct.Columns[2].HeaderText = "Product Name";
                    grdProduct.Columns[3].Visible = false;

                }
            }
        }

    }
}