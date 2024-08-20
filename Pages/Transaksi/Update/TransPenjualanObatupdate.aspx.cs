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
    public partial class TransPenjualanObatupdate : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                dtMulai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                dtSampai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                loadData();

            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            grdReceiveUpdate.DataSource = ObjGlobal.GetDataProcedure("SPUpdateTransPenjualanObat", ObjGlobal.Param);
            grdReceiveUpdate.DataBind();

        }
      
        protected void loadDataCombo()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdBank.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataProductObat", ObjGlobal.Param);
            grdBank.DataBind();
        }

        private void SetInitialRow(string Id = "")
        {

            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("lblDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQTY", typeof(string)));
            dt.Columns.Add(new DataColumn("txtUnit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtDebit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKredit", typeof(string)));
            

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPTransPenjualanObatDUpdate", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnAccount"] = myRow["noBarang"].ToString();
                dr["txtAccount"] = myRow["kodeBarang"].ToString();
                dr["lblDescription"] = myRow["namaBarang"].ToString();
                dr["txtUnit"] = myRow["satuan"].ToString();
                dr["txtQTY"] = ObjSys.IsFormatNumber(myRow["qty"].ToString());
                dr["txtDebit"] = myRow["harga"].ToString();
              
                dr["txtKredit"] = myRow["totalharga"].ToString();

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnAccount"] = string.Empty;
                dr["txtAccount"] = string.Empty;
                dr["lblDescription"] = string.Empty;
                dr["txtUnit"] = string.Empty;
                dr["txtQty"] = string.Empty;
                dr["txtDebit"] = string.Empty;
                dr["txtKredit"] = string.Empty;
               

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdReceive.DataSource = dt;
            grdReceive.DataBind();

            SetPreviousData();
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
              
                        HiddenField hdnAccount = (HiddenField)grdReceive.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdReceive.Rows[i].FindControl("txtAccount");
                        TextBox txtQTY = (TextBox)grdReceive.Rows[i].FindControl("txtQTY");
                        TextBox lblDescription = (TextBox)grdReceive.Rows[i].FindControl("lblDescription");
                        TextBox txtUnit = (TextBox)grdReceive.Rows[i].FindControl("txtUnit");
                        TextBox txtDebit = (TextBox)grdReceive.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdReceive.Rows[i].FindControl("txtKredit");


                        txtAccount.Text = dt.Rows[i]["txtAccount"].ToString();
                        hdnAccount.Value = dt.Rows[i]["hdnAccount"].ToString();
                        lblDescription.Text = dt.Rows[i]["lblDescription"].ToString();
                        txtQTY.Text = dt.Rows[i]["txtQTY"].ToString();
                        txtUnit.Text = dt.Rows[i]["txtUnit"].ToString();
                        txtDebit.Text = dt.Rows[i]["txtDebit"].ToString();
                        txtKredit.Text = dt.Rows[i]["txtKredit"].ToString();
                    
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void grdReceiveUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdReceiveUpdate.PageIndex = e.NewPageIndex;
            loadData();

        }
        protected void grdBank_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdBank.PageIndex = e.NewPageIndex;
            loadData();
            dlgBank.Show();
        }

        protected void grdBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;

                int rowIndexHdn = Convert.ToInt32(hdnParameterProd.Value);
                int rowIndex = grdBank.SelectedRow.RowIndex;

                string kdRek = (grdBank.SelectedRow.FindControl("lblKdRek") as Label).Text;
                string Ket = (grdBank.SelectedRow.FindControl("lblKet") as Label).Text;
                string noRek = (grdBank.SelectedRow.FindControl("hdnNoRek") as HiddenField).Value;
                string unit = (grdBank.SelectedRow.FindControl("lblSatuan") as Label).Text;
                string harga = (grdBank.SelectedRow.FindControl("lblHarga") as Label).Text;


                HiddenField hdnAccount = (HiddenField)grdReceive.Rows[rowIndexHdn].FindControl("hdnAccount");
                TextBox txtAccount = (TextBox)grdReceive.Rows[rowIndexHdn].FindControl("txtAccount");
                TextBox lblDescription = (TextBox)grdReceive.Rows[rowIndexHdn].FindControl("lblDescription");
                TextBox txtQTY = (TextBox)grdReceive.Rows[rowIndexHdn].FindControl("txtQTY");
                TextBox txtUnit = (TextBox)grdReceive.Rows[rowIndexHdn].FindControl("txtUnit");
                TextBox txtDebit = (TextBox)grdReceive.Rows[rowIndexHdn].FindControl("txtDebit");

                //Account Boleh Sama 07092020
                //for (int i = 0; i < grdKasBank.Rows.Count; i++)
                //{
                //    TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].Cells[1].FindControl("txtAccount");
                //    if (kdRek == kdRekBank.Text)
                //    {
                //        message += ObjSys.CreateMessage("Akun : " + kdRek + " sudah terpilih.");
                //        valid = false;
                //    }
                //}

                if (valid == true)
                {
                    hdnAccount.Value = noRek;
                    txtAccount.Text = kdRek;
                    lblDescription.Text = Ket;
                    txtUnit.Text = unit;
                    txtDebit.Text = harga;


                    txtSearch.Text = "";
                    loadData();
                    dlgBank.Hide();

                    lblMessageError.Visible = false;
                }
                else
                {
                    dlgBank.Show();
                    lblMessageError.Text = ObjSys.GetMessage("error", message);
                    lblMessageError.Visible = true;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
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

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            showHideFormKas(true, false);
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";

            if (dtPO.Text == "")
            {
                message = ObjSys.CreateMessage("Tanggal Dijual harus di isi.");
                alert = "error";
                valid = false;
            }
            
          

            try
            {
                if (valid == true)
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noPenjualan", hdnId.Value);
                    ObjGlobal.Param.Add("tgljual", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("uraian", txtKeterangan.Text);
                    ObjGlobal.GetDataProcedure("SPUpdateTransPenjualanObat_H", ObjGlobal.Param);

                    for (int i = 0; i < grdReceive.Rows.Count; i++)
                    {
                        
                        HiddenField hdnAccount = (HiddenField)grdReceive.Rows[i].FindControl("hdnAccount");
                        TextBox txtQTY = (TextBox)grdReceive.Rows[i].FindControl("txtQTY");
                        TextBox txtUnit = (TextBox)grdReceive.Rows[i].FindControl("txtUnit");
                        TextBox txtDebit = (TextBox)grdReceive.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdReceive.Rows[i].FindControl("txtKredit");
                  
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noPenjualan", hdnId.Value);
                        ObjGlobal.Param.Add("noBarang", hdnAccount.Value);

                        ObjGlobal.Param.Add("satuan", txtUnit.Text);
                        ObjGlobal.Param.Add("harga", Convert.ToDecimal(txtDebit.Text).ToString());
                        ObjGlobal.Param.Add("total", Convert.ToDecimal(txtKredit.Text).ToString());
                        ObjGlobal.Param.Add("qty", Convert.ToDecimal(txtQTY.Text).ToString());
                        ObjGlobal.GetDataProcedure("SPUpdateTransPenjualanObat_D", ObjGlobal.Param);

                    }


                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data Berhasil Disimpan");
                    showHideFormKas(true, false);
                    loadData();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage(alert, message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void grdReceive_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void grdReceiveUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SelectEdit")
                {
                    CloseMessage();
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string id = grdReceiveUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = id;

                    DataSet myData = ObjDb.GetRows("select a.tgljual, a.uraian from TransPenjualanObat_h a inner join TransPenjualanObat_d b on a.noPenjualan=b.noPenjualan " +
                        "where a.noPenjualan = '" + id + "'");
                    DataRow myRow = myData.Tables[0].Rows[0];

                    DataSet myData1 = ObjDb.GetRows("select  sum(b.totalharga) as total from TransPenjualanObat_h a inner join TransPenjualanObat_d b on a.noPenjualan=b.noPenjualan " +
                       "where a.noPenjualan = '" + id + "'");
                    DataRow myRow1 = myData1.Tables[0].Rows[0];

                    dtPO.Text = Convert.ToDateTime(myRow["tgljual"]).ToString("dd-MMM-yyyy");
                  
                    txtKeterangan.Text = myRow["uraian"].ToString();
                    txtTotalKredit.Text = myRow1["total"].ToString();
                    SetInitialRow(hdnId.Value);
                    showHideFormKas(false, true);
           
                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string id = grdReceiveUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = id;

                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noPenjualan", hdnId.Value);
                   
                    ObjDb.Delete("TransPenjualanObat_h", ObjDb.Where);
                    ObjDb.Delete("TransPenjualanObat_d", ObjDb.Where);

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Success Deleted");
                    showHideFormKas(true, false);
                    loadData();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.ToString());
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
            }
        }
        protected void grdReceive_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                hdnParameterProd.Value = rowIndex.ToString();
                if (e.CommandName == "Select")
                {
                    CloseMessage();
                    loadDataCombo();
                    //string value = (grdKasBank.SelectedRow.FindControl("hdnParameter") as HiddenField).Value;
                    //hdnParameterProd.Value = value;
                    dlgBank.Show();
                }
                else if (e.CommandName == "Clear")
                {
                    TextBox txtAccount = (TextBox)grdReceive.Rows[rowIndex].FindControl("txtAccount");
                    HiddenField hdnAccount = (HiddenField)grdReceive.Rows[rowIndex].FindControl("hdnAccount");
                    TextBox lblDescription = (TextBox)grdReceive.Rows[rowIndex].FindControl("lblDescription");
                    TextBox txtQTY = (TextBox)grdReceive.Rows[rowIndex].FindControl("txtQTY");
                    TextBox txtUnit = (TextBox)grdReceive.Rows[rowIndex].FindControl("txtUnit");
                    TextBox txtDebit = (TextBox)grdReceive.Rows[rowIndex].FindControl("txtDebit");
                    TextBox txtKredit = (TextBox)grdReceive.Rows[rowIndex].FindControl("txtKredit");

                    txtAccount.Text = "";
                    hdnAccount.Value = "";
                    lblDescription.Text = "";
                    txtQTY.Text = "";
                    txtUnit.Text = "";
                    txtDebit.Text = "";
                    txtKredit.Text = "";

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();CalculateDiscount();", true);

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