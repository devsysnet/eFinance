using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.Input
{
    public partial class mstStatusjenisPeg1 : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dr["Column4"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdInstansi.DataSource = dt;
            grdInstansi.DataBind();
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
                        TextBox txtAgama = (TextBox)grdInstansi.Rows[i].FindControl("txtAgama");
                        HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdInstansi.Rows[i].FindControl("txtAccount");
                        Label lblDescription = (Label)grdInstansi.Rows[i].FindControl("lblDescription");


                        txtAgama.Text = dt.Rows[i]["Column1"].ToString();
                        hdnAccount.Value = dt.Rows[i]["Column2"].ToString();
                        txtAccount.Text = dt.Rows[i]["Column3"].ToString();
                        lblDescription.Text = dt.Rows[i]["Column4"].ToString();
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
                        HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdInstansi.Rows[i].FindControl("txtAccount");
                        TextBox txtRemarkDetil = (TextBox)grdInstansi.Rows[i].FindControl("txtRemarkDetil");
                        Label lblDescription = (Label)grdInstansi.Rows[i].FindControl("lblDescription");
                        TextBox txtAgama = (TextBox)grdInstansi.Rows[i].FindControl("txtAgama");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtAgama.Text;
                        dtCurrentTable.Rows[i]["Column2"] = txtAgama.Text;
                        dtCurrentTable.Rows[i]["Column3"] = txtAgama.Text;
                        dtCurrentTable.Rows[i]["Column4"] = txtAgama.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdInstansi.DataSource = dtCurrentTable;
                    grdInstansi.DataBind();
                }
            }
            SetPreviousData();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetInitialRow();
                for (int i = 1; i < 5; i++)
                {
                    AddNewRow();
                }

           
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
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
            string message = "";
            int count = 0, count2 = 0;

                try
                {
                    for (int i = 0; i < grdInstansi.Rows.Count; i++)
                    {
                        TextBox txtAgama = (TextBox)grdInstansi.Rows[i].FindControl("txtAgama");
                        HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[i].FindControl("hdnAccount");

                      
                            if (txtAgama.Text != "")

                            {
                                ObjDb.Data.Clear();
                                ObjDb.Data.Add("statuspegawai", txtAgama.Text);
                                ObjDb.Data.Add("sts", "1");
                                ObjDb.Data.Add("norek", hdnAccount.Value);
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Insert("Mmststatuspegawai", ObjDb.Data);
                                message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + txtAgama.Text + "</b> data tersimpan.");
                                count += 1;
                            }
                       
                      
                       
                    }
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil disimpan.");
                clearData();
            }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }

            
        }
        private void clearData()
        {
            for (int i = 0; i < grdInstansi.Rows.Count; i++)
            {
                TextBox txtAgama = (TextBox)grdInstansi.Rows[i].FindControl("txtAgama");

                txtAgama.Text = "";

                SetInitialRow();
                for (int g = 1; g < 5; g++)
                {
                    AddNewRow();
                }
            }
        }
        protected void LoadDataBank(string kdRek = "")
        {
            ObjGlobal.Param.Clear();
            //ObjGlobal.Param.Add("noRekInduk", cboAccount.Text);
            ObjGlobal.Param.Add("Search", kdRek);
           
                ObjGlobal.Param.Add("jenisRekening", "0");
          
            grdBank.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekeningnew", ObjGlobal.Param);
            grdBank.DataBind();
        }

        protected void txtAccount_TextChanged(object sender, EventArgs e)
        {
            autoComplete();
        }
        protected void autoComplete()
        {
            for (int i = 0; i < grdInstansi.Rows.Count; i++)
            {
                HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[i].FindControl("hdnAccount");
                TextBox kdRekBank = (TextBox)grdInstansi.Rows[i].FindControl("txtAccount");
                Label lblDescription = (Label)grdInstansi.Rows[i].FindControl("lblDescription");
              
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("kdRek", kdRekBank.Text.Replace(" ", ""));
                DataSet mySet = ObjGlobal.GetDataProcedure("SPmrekening", ObjGlobal.Param);

                foreach (DataRow myRow in mySet.Tables[0].Rows)
                {
                    kdRekBank.Text = myRow["kdRek"].ToString();
                    hdnAccount.Value = myRow["noRek"].ToString();
                    lblDescription.Text = myRow["Ket"].ToString();
                   
                }
            }
        }
        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            //ObjGlobal.Param.Add("noRekInduk", cboAccount.Text);
            ObjGlobal.Param.Add("Search", txtSearch.Text);

            ObjGlobal.Param.Add("jenisRekening", "0");

            grdBank.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekeningnew", ObjGlobal.Param);
            grdBank.DataBind();
        }
        protected void grdBank_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdBank.PageIndex = e.NewPageIndex;
            loadData();
            dlgBank.Show();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
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

                HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[rowIndexHdn].FindControl("hdnAccount");
                TextBox txtAccount = (TextBox)grdInstansi.Rows[rowIndexHdn].FindControl("txtAccount");
                Label lblDescription = (Label)grdInstansi.Rows[rowIndexHdn].FindControl("lblDescription");
             

               

                if (valid == true)
                {
                    hdnAccount.Value = noRek;
                    txtAccount.Text = kdRek;
                    lblDescription.Text = Ket;
                    

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

        protected void grdInstansi_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                hdnParameterProd.Value = rowIndex.ToString();
                if (e.CommandName == "Select")
                {
                    TextBox txtAccount = (TextBox)grdInstansi.Rows[rowIndex].FindControl("txtAccount");
                    if (txtAccount.Text != "")
                    {
                        CloseMessage();
                        LoadDataBank(txtAccount.Text);
                        dlgBank.Show();
                    }
                    if (txtAccount.Text == "")
                    {
                        CloseMessage();
                        loadData();
                        //string value = (grdKasBank.SelectedRow.FindControl("hdnParameter") as HiddenField).Value;
                        //hdnParameterProd.Value = value;
                        dlgBank.Show();
                    }


                }
                else if (e.CommandName == "Clear")
                {
                    TextBox txtAccount = (TextBox)grdInstansi.Rows[rowIndex].FindControl("txtAccount");
                    HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[rowIndex].FindControl("hdnAccount");
                    Label lblDescription = (Label)grdInstansi.Rows[rowIndex].FindControl("lblDescription");
                   
                    txtAccount.Text = "";
                    hdnAccount.Value = "";
                    lblDescription.Text = "";
                 
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
            clearData();
        }
    }
}