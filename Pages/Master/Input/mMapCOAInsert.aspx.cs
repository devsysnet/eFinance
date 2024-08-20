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
    public partial class mMapCOAInsert : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("mMapCOAInser.aspx");
                SetInitialRowDanaBOS();
                SetInitialRow();
                for (int i = 1; i < 3; i++)
                {
                    AddNewRow();
                    AddNewRowDanaBOS();
                }

            }
        }

       
        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);

            ObjGlobal.Param.Add("GetstsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("GetstsPusat", ObjSys.GetstsPusat);
            grdBank.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekeningKasBankAktivitas", ObjGlobal.Param);
            grdBank.DataBind();

            


        }
        protected void loadDataDanaBOS()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchDanaBOS.Text);

            ObjGlobal.Param.Add("GetstsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("GetstsPusat", ObjSys.GetstsPusat);
            grdAkunDanaBOS.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekeningKasBankDanaBOS", ObjGlobal.Param);
            grdAkunDanaBOS.DataBind();
        }

    


        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["txtAccount"] = string.Empty;
            dr["hdnAccount"] = string.Empty;


            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdKasBank.DataSource = dt;
            grdKasBank.DataBind();

 

        }

        private void SetInitialRowDanaBOS()
        {
          
            DataTable dx = new DataTable();
            DataRow dz = null;
            dx.Columns.Add(new DataColumn("RowNumberDanaBOS", typeof(string)));
            dx.Columns.Add(new DataColumn("txtAccountDanaBOS", typeof(string)));
            dx.Columns.Add(new DataColumn("hdnAccountDanaBOS", typeof(string)));

            dz = dx.NewRow();
            dz["RowNumberDanaBOS"] = 1;
            dz["txtAccountDanaBOS"] = string.Empty;
            dz["hdnAccountDanaBOS"] = string.Empty;


            dx.Rows.Add(dz);
            ViewState["CurrentTableDanaBOS"] = dx;
            grdDanaBOS.DataSource = dx;
            grdDanaBOS.DataBind();

        }

        private void SetInitialRowOto(string noTransKas = "0")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));
          

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noTransKas", noTransKas);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPmRekeningtranskasikas", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtAccount"] = myRow["Ket"].ToString();
                dr["hdnAccount"] = myRow["noRek"].ToString();
      
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grdKasBank.DataSource = dt;
            grdKasBank.DataBind();

            DataTable dx = new DataTable();
            DataRow dz = null;
            dx.Columns.Add(new DataColumn("RowNumberDanaBOS", typeof(string)));
            dx.Columns.Add(new DataColumn("txtAccountDanaBOS", typeof(string)));
            dx.Columns.Add(new DataColumn("hdnAccountDanaBOS", typeof(string)));

            foreach (DataRow myRowDanaBos in mySet.Tables[0].Rows)
            {
                dz = dx.NewRow();
                dz["RowNumberDanaBOS"] = 1;
                dz["txtAccountDanaBOS"] = myRowDanaBos["Ket"].ToString();
                dz["hdnAccountDanaBOS"] = myRowDanaBos["noRek"].ToString();

                dx.Rows.Add(dz);
            }

            ViewState["CurrentTableDanaBOS"] = dx;
            grdDanaBOS.DataSource = dx;
            grdDanaBOS.DataBind();

            SetPreviousData();
        }

        private void SetInitialRowTamp(string nomTampungan = "0", decimal nilai = 0)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));
           
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nomTampungan", nomTampungan);
            ObjGlobal.Param.Add("nilai", Convert.ToDecimal(nilai).ToString());
            DataSet mySet = ObjGlobal.GetDataProcedure("SPDetilTampungan", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtAccount"] = myRow["Ket"].ToString();
                dr["hdnAccount"] = myRow["noRek"].ToString();
               
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grdKasBank.DataSource = dt;
            grdKasBank.DataBind();

            DataTable dx = new DataTable();
            DataRow dz = null;
            dx.Columns.Add(new DataColumn("RowNumberDanaBOS", typeof(string)));
            dx.Columns.Add(new DataColumn("txtAccountDanaBOS", typeof(string)));
            dx.Columns.Add(new DataColumn("hdnAccountDanaBOS", typeof(string)));

            foreach (DataRow myRowDanaBos in mySet.Tables[0].Rows)
            {
                dz = dx.NewRow();
                dz["RowNumberDanaBOS"] = 1;
                dz["txtAccountDanaBOS"] = myRowDanaBos["Ket"].ToString();
                dz["hdnAccountDanaBOS"] = myRowDanaBos["noRek"].ToString();

                dx.Rows.Add(dz);
            }

            ViewState["CurrentTableDanaBOS"] = dx;
            grdDanaBOS.DataSource = dx;
            grdDanaBOS.DataBind();

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
                        HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdKasBank.Rows[i].FindControl("txtAccount");
                       
                        txtAccount.Text = dt.Rows[i]["txtAccount"].ToString();
                        hdnAccount.Value = dt.Rows[i]["hdnAccount"].ToString();
                      
                    }
                }

                
            }
            
        }
        private void SetPreviousDataDanaBOS()
        {
            if (ViewState["CurrentTableDanaBOS"] != null)
            {

                DataTable dx = (DataTable)ViewState["CurrentTableDanaBOS"];
                if (dx.Rows.Count > 0)
                {
                    for (int x = 0; x < dx.Rows.Count; x++)
                    {
                        HiddenField hdnAccountDanaBOS = (HiddenField)grdDanaBOS.Rows[x].FindControl("hdnAccountDanaBOS");
                        TextBox txtAccountDanaBOS = (TextBox)grdDanaBOS.Rows[x].FindControl("txtAccountDanaBOS");

                        txtAccountDanaBOS.Text = dx.Rows[x]["txtAccountDanaBOS"].ToString();
                        hdnAccountDanaBOS.Value = dx.Rows[x]["hdnAccountDanaBOS"].ToString();

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
                        HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdKasBank.Rows[i].FindControl("txtAccount");
                       
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["txtAccount"] = txtAccount.Text;
                        dtCurrentTable.Rows[i]["hdnAccount"] = hdnAccount.Value;
           
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdKasBank.DataSource = dtCurrentTable;
                    grdKasBank.DataBind();
                }

            
            }

            SetPreviousData();
        }

        private void AddNewRowDanaBOS()
        {
            if (ViewState["CurrentTableDanaBOS"] != null)
            {
      
                DataTable dxCurrentTable = (DataTable)ViewState["CurrentTableDanaBOS"];
                DataRow dzCurrentRow = null;
                if (dxCurrentTable.Rows.Count > 0)
                {
                    for (int x = 0; x < dxCurrentTable.Rows.Count; x++)
                    {
                        HiddenField hdnAccountDanaBOS = (HiddenField)grdDanaBOS.Rows[x].FindControl("hdnAccountDanaBOS");
                        TextBox txtAccountDanaBOS = (TextBox)grdDanaBOS.Rows[x].FindControl("txtAccountDanaBOS");

                        dzCurrentRow = dxCurrentTable.NewRow();
                        dxCurrentTable.Rows[x]["txtAccountDanaBOS"] = txtAccountDanaBOS.Text;
                        dxCurrentTable.Rows[x]["hdnAccountDanaBOS"] = hdnAccountDanaBOS.Value;

                    }
                    dxCurrentTable.Rows.Add(dzCurrentRow);
                    ViewState["CurrentTableDanaBOS"] = dxCurrentTable;
                    grdDanaBOS.DataSource = dxCurrentTable;
                    grdDanaBOS.DataBind();
                }
            }

            SetPreviousDataDanaBOS();
        }


        protected void grdBank_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdBank.PageIndex = e.NewPageIndex;
            loadData();
            dlgBank.Show();
        }

        protected void grdAkunDanaBOS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdAkunDanaBOS.PageIndex = e.NewPageIndex;
            loadDataDanaBOS();
            dlgDanaBOS.Show();
        }

        protected void grdBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;

                int rowIndexHdn = Convert.ToInt32(hdnParameterProd.Value);
                int rowIndex = grdBank.SelectedRow.RowIndex;

                string kdRek = (grdBank.SelectedRow.FindControl("lblKet") as Label).Text;
                string noRek = (grdBank.SelectedRow.FindControl("hdnNoRek") as HiddenField).Value;

                HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[rowIndexHdn].FindControl("hdnAccount");
                TextBox txtAccount = (TextBox)grdKasBank.Rows[rowIndexHdn].FindControl("txtAccount");
                TextBox txtRemarkGrid = (TextBox)grdKasBank.Rows[rowIndexHdn].FindControl("txtRemarkDetil");

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

                //Jika login pusat tetapi akun yang dipilih bukan pusat maka muncul proteksi
                //if (ObjSys.GetstsPusat == "4")
                //{
                //    for (int i = 0; i < grdKasBank.Rows.Count; i++)
                //    {
                //        TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].Cells[1].FindControl("txtAccount");

                //        DataSet mySet = ObjDb.GetRows("select * from mRekening where noCabang = "+ ObjSys.GetstsPusat + " and noRek = "+ noRek  + "");
                //        if (mySet.Tables[0].Rows.Count == 0)
                //        {
                //            //DataRow myRow = mySet.Tables[0].Rows[0];
                //            //string Id = myRow["noRek"].ToString();

                //            message += ObjSys.CreateMessage("Akun anya untuk Admin Kantor Yayasan.");
                //            valid = false;
                //        }
                //        else
                //            valid = true;
                //    }

                //}

                if (valid == true)
                {
                    hdnAccount.Value = noRek;
                    txtAccount.Text = kdRek;

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

        protected void grdAkunDanaBOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;

                int rowIndexHdn = Convert.ToInt32(hdnParameterProdDanaBOS.Value);
                int rowIndex = grdAkunDanaBOS.SelectedRow.RowIndex;

                string kdRek = (grdAkunDanaBOS.SelectedRow.FindControl("lblKetDanaBOS") as Label).Text;
                string noRek = (grdAkunDanaBOS.SelectedRow.FindControl("hdnNoRekDanaBOS") as HiddenField).Value;

                HiddenField hdnAccount = (HiddenField)grdDanaBOS.Rows[rowIndexHdn].FindControl("hdnAccountDanaBOS");
                TextBox txtAccount = (TextBox)grdDanaBOS.Rows[rowIndexHdn].FindControl("txtAccountDanaBOS");


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

                //Jika login pusat tetapi akun yang dipilih bukan pusat maka muncul proteksi
                //if (ObjSys.GetstsPusat == "4")
                //{
                //    for (int i = 0; i < grdKasBank.Rows.Count; i++)
                //    {
                //        TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].Cells[1].FindControl("txtAccount");

                //        DataSet mySet = ObjDb.GetRows("select * from mRekening where noCabang = "+ ObjSys.GetstsPusat + " and noRek = "+ noRek  + "");
                //        if (mySet.Tables[0].Rows.Count == 0)
                //        {
                //            //DataRow myRow = mySet.Tables[0].Rows[0];
                //            //string Id = myRow["noRek"].ToString();

                //            message += ObjSys.CreateMessage("Akun anya untuk Admin Kantor Yayasan.");
                //            valid = false;
                //        }
                //        else
                //            valid = true;
                //    }

                //}

                if (valid == true)
                {
                    hdnAccount.Value = noRek;
                    txtAccount.Text = kdRek;

                    txtSearchDanaBOS.Text = "";
                    loadDataDanaBOS();
                    dlgDanaBOS.Hide();

                  
                }
                else
                {
                    dlgDanaBOS.Show();
           
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }


        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            AddNewRow();
            AddNewRowDanaBOS();
        }


        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            int cekData = 0;
            for (int i = 0; i < grdKasBank.Rows.Count; i++)
            {
                TextBox txtAccount = (TextBox)grdKasBank.Rows[i].FindControl("txtAccount");
                if (txtAccount.Text != "")
                {
                    cekData++;
                }
            }

            // cek sudah input / belum saldo awalnya, jika belum lepas protek post saldo kas terakhir


            if (valid == true)
            {
                try
                {
                    string kurs = "1";


                    for (int i = 0; i < grdKasBank.Rows.Count; i++)
                    {
                        HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdKasBank.Rows[i].FindControl("txtAccount");

                        HiddenField hdnAccountDanaBOS = (HiddenField)grdDanaBOS.Rows[i].FindControl("hdnAccountDanaBOS");
                        TextBox txtAccountDanaBOS = (TextBox)grdDanaBOS.Rows[i].FindControl("txtAccountDanaBOS");

                        if (txtAccount.Text != "" || txtAccountDanaBOS.Text != "")
                        {
                            //    string Debit = "0.00", Kredit = "0.00";
                            //    if (txtDebit.Text != "")
                            //    {
                            //        Debit = Convert.ToDecimal(txtDebit.Text).ToString();
                            //    }
                            //    if (txtKredit.Text != "")
                            //    {
                            //        Kredit = Convert.ToDecimal(txtKredit.Text).ToString();
                            //    }
                            //    decimal a = Convert.ToDecimal(kurs);
                            //    decimal b = Convert.ToDecimal(Debit);
                            //    decimal nilairpdebet = (a * b);

                            //    decimal c = Convert.ToDecimal(kurs);
                            //    decimal d = Convert.ToDecimal(Kredit);
                            //    decimal nilairpkredit = (c * d);

                            ObjDb.Data.Clear();

                            ObjDb.Data.Add("noRek", hdnAccount.Value);
                            ObjDb.Data.Add("noRekBos", hdnAccountDanaBOS.Value);
                            ObjDb.Insert("mMapCOA", ObjDb.Data);
                        }

                    }


                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                    ClearData();


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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
            dlgBank.Show();

        }

        protected void btnSearchDanaBOS_Click(object sender, EventArgs e)
        {
            loadDataDanaBOS();
            dlgDanaBOS.Show();

        }




        #region Other
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

            txtSearch.Text = "";
            txtSearchDanaBOS.Text = "";
            hdnParameterProd.Value = "";
            hdnParameterProdDanaBOS.Value = "";
            SetInitialRow();
            SetInitialRowDanaBOS();
            for (int i = 1; i < 3; i++)
            {
                AddNewRow();
                AddNewRowDanaBOS();
            }

        }



        #endregion

        protected void loadDataReimbersment()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtKodeReimbersment.Text);
            grdReimbersment.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataReimbersment", ObjGlobal.Param);
            grdReimbersment.DataBind();
        }

        protected void btnReimbersment_Click(object sender, ImageClickEventArgs e)
        {
            loadDataReimbersment();
            dlgReimbersment.Show();
        }

        protected void btnCariReimbersment_Click(object sender, EventArgs e)
        {
            loadDataReimbersment();
            dlgReimbersment.Show();
        }

        protected void grdReimbersment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdReimbersment.PageIndex = e.NewPageIndex;
            loadDataReimbersment();
            dlgReimbersment.Show();
        }

        protected void grdReimbersment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                CloseMessage();

                int rowIndex = grdReimbersment.SelectedRow.RowIndex;
                string Id = grdReimbersment.DataKeys[rowIndex].Values[0].ToString();
                string Kd = grdReimbersment.SelectedRow.Cells[1].Text;
                string nilai = grdReimbersment.SelectedRow.Cells[3].Text;
                string ket = grdReimbersment.SelectedRow.Cells[4].Text;




                SetInitialRowReim(Id);

                dlgReimbersment.Hide();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        private void SetInitialRowReim(string Id = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("lblDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("txtRemarkDetil", typeof(string)));
            dt.Columns.Add(new DataColumn("txtDebit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKredit", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadReimbursmentKasBank", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtAccount"] = myRow["kdRek"].ToString();
                dr["hdnAccount"] = myRow["noRek"].ToString();
                dr["lblDescription"] = myRow["Ket"].ToString();
                dr["txtRemarkDetil"] = string.Empty;
                dr["txtDebit"] = ObjSys.IsFormatNumber(myRow["value"].ToString());
                dr["txtKredit"] = string.Empty;
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grdKasBank.DataSource = dt;
            grdKasBank.DataBind();

            SetPreviousData();
        }

        protected void btnPO_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgPO.Show();
        }

        protected void btnSearchPO_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgPO.Show();
        }

        protected void grdPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdPO.PageIndex = e.NewPageIndex;
            loadData();
            dlgPO.Show();
        }

        protected void grdPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdPO.SelectedRow.RowIndex;
                string Id = grdPO.DataKeys[rowIndex].Values[0].ToString();
                string kodePO = grdPO.SelectedRow.Cells[1].Text;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noPO", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankPO", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["hargasatuan"].ToString());

                    //txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["hargasatuan"].ToString());
                }

                DataSet mySet = ObjDb.GetRows("select distinct a.noPO, c.noRek, c.kdRek, c.Ket, d.nSaldoHutang as hargasatuan " +
                    "from TransPO_D a inner join mBarang b on a.noBarang = b.noBarang inner join mRekening " +
                    "c on c.noRek = b.norek inner join thutang d on d.nopo = a.nopo where d.nohutang = '" + Id + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];
                string dpp = myRow["hargasatuan"].ToString();


                loadData();
                dlgPO.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnPR_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgPR.Show();
        }

        protected void btnSearchPR_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgPR.Show();
        }

        protected void grdPR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdPR.PageIndex = e.NewPageIndex;
            loadData();
            dlgPR.Show();
        }

        protected void grdPR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdPR.SelectedRow.RowIndex;
                string Id = grdPR.DataKeys[rowIndex].Values[0].ToString();
                string kodePR = grdPR.SelectedRow.Cells[1].Text;
                string uraian = grdPR.SelectedRow.Cells[4].Text;


                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noPR", Id);
                ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankPR", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    TextBox txtRemarkDetil = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtRemarkDetil");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtRemarkDetil.Text = uraian;
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["hargasatuan"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["hargasatuan"].ToString());
                }

                loadData();
                dlgPR.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnGaji_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgGaji.Show();
        }

        protected void btnIuran_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgIuran.Show();
        }

        protected void btnKasBon_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgKasBon.Show();
        }

        protected void btnTHR_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgTHR.Show();
        }

        protected void btnSearchGaji_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgGaji.Show();
        }

        protected void grdGaji_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdGaji.PageIndex = e.NewPageIndex;
            loadData();
            dlgGaji.Show();
        }

        protected void grdGaji_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdGaji.SelectedRow.RowIndex;
                string Id = grdGaji.DataKeys[rowIndex].Values[0].ToString();
                string kodegaji = grdGaji.SelectedRow.Cells[1].Text;


                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noGaji", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankGaji", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                }


                loadData();
                dlgGaji.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnSearchIuran_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgIuran.Show();
        }

        protected void grdIuran_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdGaji.PageIndex = e.NewPageIndex;
            loadData();
            dlgIuran.Show();
        }

        protected void grdIuran_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdIuran.SelectedRow.RowIndex;
                string Id = grdIuran.DataKeys[rowIndex].Values[0].ToString();
                string kodeIuran = grdIuran.SelectedRow.Cells[1].Text;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noIuran", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankIuran", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                }

                loadData();
                dlgIuran.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnSearchKasBon_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgKasBon.Show();
        }

        protected void grdKasBon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdKasBon.PageIndex = e.NewPageIndex;
            loadData();
            dlgKasBon.Show();
        }

        protected void grdKasBon_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdKasBon.SelectedRow.RowIndex;
                string Id = grdKasBon.DataKeys[rowIndex].Values[0].ToString();
                string kode = grdKasBon.SelectedRow.Cells[1].Text;


                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noKasbon", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankKasBon", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();


                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(txtDebit.Text);
                }

                loadData();
                dlgKasBon.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnSearchTHR_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgTHR.Show();
        }

        protected void grdTHR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdTHR.PageIndex = e.NewPageIndex;
            loadData();
            dlgTHR.Show();
        }

        protected void grdTHR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdTHR.SelectedRow.RowIndex;
                string Id = grdTHR.DataKeys[rowIndex].Values[0].ToString();
                string kodeTHR = grdTHR.SelectedRow.Cells[1].Text;


                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noTHR", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankTHR", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                }


                loadData();
                dlgTHR.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void grdKasBank_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                hdnParameterProd.Value = rowIndex.ToString();
                if (e.CommandName == "Select")
                {
                    CloseMessage();
                    loadData();
                    //string value = (grdKasBank.SelectedRow.FindControl("hdnParameter") as HiddenField).Value;
                    //hdnParameterProd.Value = value;
                    dlgBank.Show();
                }
                else if (e.CommandName == "Clear")
                {
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[rowIndex].FindControl("txtAccount");
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[rowIndex].FindControl("hdnAccount");
                   

                    txtAccount.Text = "";
                    hdnAccount.Value = "";
                   

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();CalculateDiscount();", true);

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void grdDanaBOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                hdnParameterProdDanaBOS.Value = rowIndex.ToString();
                if (e.CommandName == "Select")
                {
                    loadDataDanaBOS();
                    //string value = (grdKasBank.SelectedRow.FindControl("hdnParameter") as HiddenField).Value;
                    //hdnParameterProd.Value = value;
                    dlgDanaBOS.Show();
                }
                else if (e.CommandName == "Clear")
                {
                    TextBox txtAccount = (TextBox)grdDanaBOS.Rows[rowIndex].FindControl("txtAccountDanaBOS");
                    HiddenField hdnAccount = (HiddenField)grdDanaBOS.Rows[rowIndex].FindControl("hdnAccountDanaBOS");


                    txtAccount.Text = "";
                    hdnAccount.Value = "";
      

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();CalculateDiscount();", true);

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void btnSearchAbsensi_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgAbsensi.Show();
        }

        protected void grdAbsensi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdAbsensi.PageIndex = e.NewPageIndex;
            loadData();
            dlgAbsensi.Show();
        }

        protected void grdAbsensi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdAbsensi.SelectedRow.RowIndex;
                string Id = grdAbsensi.DataKeys[rowIndex].Values[0].ToString();
                string kodeAbsensi = grdAbsensi.SelectedRow.Cells[1].Text;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noAbsensi", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankAbsensi", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                }


                loadData();
                dlgAbsensi.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnAbsen_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgAbsensi.Show();
        }

        protected void btnPRKas_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgPRKas.Show();
        }

        protected void btnSearchPRKas_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgPRKas.Show();
        }

        protected void grdPRKas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdPRKas.PageIndex = e.NewPageIndex;
            loadData();
            dlgPRKas.Show();
        }

        protected void grdPRKas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdPRKas.SelectedRow.RowIndex;
                string Id = grdPRKas.DataKeys[rowIndex].Values[0].ToString();
                string kodeKas = grdPRKas.SelectedRow.Cells[1].Text;


                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noKasPR", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankPRKas", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                }


                loadData();
                dlgPRKas.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void txtDebit_TextChanged(object sender, EventArgs e)
        {
            TextBox txtDebit = (TextBox)sender;
            var row = (GridViewRow)txtDebit.NamingContainer;

            TextBox txtKredit = (TextBox)row.FindControl("txtKredit");
            if (txtDebit.Text != "0.00" && txtDebit.Text != "0")
                txtKredit.Enabled = false;
            else
                txtKredit.Enabled = true;
        }

        protected void txtKredit_TextChanged(object sender, EventArgs e)
        {
            TextBox txtKredit = (TextBox)sender;
            var row = (GridViewRow)txtKredit.NamingContainer;

            TextBox txtDebit = (TextBox)row.FindControl("txtDebit");
            if (txtKredit.Text != "0.00" && txtKredit.Text != "0")
                txtDebit.Enabled = false;
            else
                txtDebit.Enabled = true;
        }


        protected void btnPengembalian_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgPengembalian.Show();
        }

        protected void btnSearchPengembelian_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgPengembalian.Show();
        }

        protected void grdPengembalian_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdPengembalian.PageIndex = e.NewPageIndex;
            loadData();
            dlgPengembalian.Show();
        }

        protected void grdPengembalian_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdPengembalian.SelectedRow.RowIndex;
                string Id = grdPengembalian.DataKeys[rowIndex].Values[0].ToString();
                string kodePO = grdPengembalian.SelectedRow.Cells[1].Text;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);

                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankPengembalian", ObjGlobal.Param);
                int i = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();

                }

                loadData();
                dlgPengembalian.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void txtAccount_TextChanged(object sender, EventArgs e)
        {
            autoComplete();
        }

        protected void txtAccountDanaBOS_TextChanged(object sender, EventArgs e)
        {
            autoCompleteDanaBOS();
        }

        protected void autoComplete()
        {
            for (int i = 0; i < grdKasBank.Rows.Count; i++)
            {
                HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i].FindControl("hdnAccount");
                TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].FindControl("txtAccount");
                Label lblDescription = (Label)grdKasBank.Rows[i].FindControl("lblDescription");

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

        protected void autoCompleteDanaBOS()
        {
            for (int i = 0; i < grdDanaBOS.Rows.Count; i++)
            {
                HiddenField hdnAccountDanaBOS = (HiddenField)grdDanaBOS.Rows[i].FindControl("hdnAccountDanaBOS");
                TextBox kdRekBankDanaBOS = (TextBox)grdDanaBOS.Rows[i].FindControl("txtAccountDanaBOS");
               
                
            }
        }


    }
}
