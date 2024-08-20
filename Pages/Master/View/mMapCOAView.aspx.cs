using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Collections;

namespace eFinance.Pages.Master.View
{
    public partial class mMapCOAView : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                loadData();
            }
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;

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

                        txtAgama.Text = dt.Rows[i]["Column1"].ToString();
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
                        TextBox txtAgama = (TextBox)grdInstansi.Rows[i].FindControl("txtAgama");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtAgama.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdInstansi.DataSource = dtCurrentTable;
                    grdInstansi.DataBind();
                }
            }
            SetPreviousData();
        }
        protected void loadDataAktivitas()
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

        private void loadData()
        {
            grdMapCOA.DataSource = ObjDb.GetRows("select a.noCOAMap,b.noRek,c.noRek as noRekBos,b.Ket,c.Ket as ketBos from mMapCOA a left join mRekening b on a.noRek = b.noRek left Join mRekening c on a.noRekBos = c.noRek WHERE b.Ket LIKE '%" + txtSearch.Text + "%'");
            grdMapCOA.DataBind();
        }
        protected void grdMapCOA_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMapCOA.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
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

        protected void grdMapCOA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdMapCOA.SelectedRow.RowIndex;
                string noCOAMap = grdMapCOA.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noCOAMap;
                CloseMessage();


                grdInstansi.DataSource = ObjDb.GetRows("select a.noCOAMap,b.noRek,c.noRek as noRekBos,b.Ket,c.Ket as ketBos from mMapCOA a left join mRekening b on a.noRek = b.noRek left Join mRekening c on a.noRekBos = c.noRek WHERE noCOAMap ='" + hdnId.Value + "'");
                grdInstansi.DataBind();

                for (int i = 1; i < 1; i++)
                {
                    AddNewRow();
                }
                loadDataDanaBOS();
                loadDataAktivitas();
                this.ShowHideGridAndForm(false, true);
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

        protected void grdBank_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdBank.PageIndex = e.NewPageIndex;
            loadDataAktivitas();
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

                HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[rowIndexHdn].FindControl("hdnAccount");
                TextBox txtAccount = (TextBox)grdInstansi.Rows[rowIndexHdn].FindControl("txtAccount");
                TextBox txtRemarkGrid = (TextBox)grdInstansi.Rows[rowIndexHdn].FindControl("txtRemarkDetil");

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

                    txtSearchBank.Text = "";
                    loadDataAktivitas();
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

                HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[rowIndexHdn].FindControl("hdnAccountDanaBOS");
                TextBox txtAccount = (TextBox)grdInstansi.Rows[rowIndexHdn].FindControl("txtAccountDanaBOS");


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
        protected void grdInstansi_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                hdnParameterProd.Value = rowIndex.ToString();
                if (e.CommandName == "Select")
                {
                    CloseMessage();
                    loadDataAktivitas();
                    //string value = (grdKasBank.SelectedRow.FindControl("hdnParameter") as HiddenField).Value;
                    //hdnParameterProd.Value = value;
                    dlgBank.Show();
                }
                else if (e.CommandName == "Clear")
                {
                    TextBox txtAccount = (TextBox)grdInstansi.Rows[rowIndex].FindControl("txtAccount");
                    HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[rowIndex].FindControl("hdnAccount");


                    txtAccount.Text = "";
                    hdnAccount.Value = "";


                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();CalculateDiscount();", true);

                }
                hdnParameterProdDanaBOS.Value = rowIndex.ToString();
                if (e.CommandName == "SelectDanaBOS")
                {
                    loadDataDanaBOS();
                    //string value = (grdKasBank.SelectedRow.FindControl("hdnParameter") as HiddenField).Value;
                    //hdnParameterProd.Value = value;
                    dlgDanaBOS.Show();
                }
                else if (e.CommandName == "ClearDanaBOS")
                {
                    TextBox txtAccount = (TextBox)grdInstansi.Rows[rowIndex].FindControl("txtAccountDanaBOS");
                    HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[rowIndex].FindControl("hdnAccountDanaBOS");


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
        protected void btnSearchBank_Click(object sender, EventArgs e)
        {
            loadDataAktivitas();
            dlgBank.Show();

        }

        protected void btnSearchDanaBOS_Click(object sender, EventArgs e)
        {
            loadDataDanaBOS();
            dlgDanaBOS.Show();

        }
        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnMode.Value.ToLower() == "deleteall")
                {
                    /*DELETE ALL SELECTED*/

                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noCOAMap", itemRow);
                            ObjDb.Delete("mMapCOA", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdMapCOA.Rows)
                    {
                        string itemId = grdMapCOA.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdMapCOA.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noCOAMap", itemId);
                            ObjDb.Delete("mMapCOA", ObjDb.Where);
                        }
                    }
                }
                /*END DELETE ALL SELECTED*/
                loadData();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil dihapus.");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdMapCOA.Rows)
                {
                    string index = grdMapCOA.DataKeys[gvrow.RowIndex].Value.ToString();
                    if (userdetails.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)gvrow.FindControl("chkCheck");
                        myCheckBox.Checked = true;
                    }
                }
            }
        }
        private void SaveCheckedValues()
        {
            ArrayList userdetails = new ArrayList();
            foreach (GridViewRow gvrow in grdMapCOA.Rows)
            {
                string index = grdMapCOA.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdMapCOA.Rows[gvrow.RowIndex].FindControl("chkCheck");
                bool result = chkCheck.Checked;
                // Check in the Session
                if (ViewState["CHECKED_ITEMS"] != null)
                    userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
                if (result)
                {
                    if (!userdetails.Contains(index))
                        userdetails.Add(index);
                }
                else
                    userdetails.Remove(index);
            }
            if (userdetails != null && userdetails.Count > 0)
                ViewState["CHECKED_ITEMS"] = userdetails;
        }

        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    string message = "";
        //    bool valid = true;
        //    ObjDb.Data.Clear();
        //    ObjDb.Where.Clear();

        //    if (valid == true)
        //    {
        //        try
        //        {
        //            for (int i = 0; i < grdInstansi.Rows.Count; i++)
        //            {
        //                HiddenField hdnNoCOAMap = (HiddenField)grdInstansi.Rows[i].FindControl("hdnNoCOAMap");
        //                TextBox txtAccount = (TextBox)grdInstansi.Rows[i].FindControl("txtAccount");
        //                HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[i].FindControl("hdnAccount");
        //                TextBox txtAccountDanaBOS = (TextBox)grdInstansi.Rows[i].FindControl("txtAccountDanaBOS");
        //                HiddenField hdnAccountDanaBOS = (HiddenField)grdInstansi.Rows[i].FindControl("hdnAccountDanaBOS");
        //                if (txtAccount.Text != "" || txtAccountDanaBOS.Text != "")
        //                {
        //                    ObjDb.Data.Clear();
        //                    ObjDb.Where.Clear();
        //                    ObjDb.Where.Add("noCOAMap", hdnNoCOAMap.Value);
        //                    ObjDb.Data.Add("noRek", hdnAccount.Value);
        //                    ObjDb.Data.Add("noRekBos", hdnAccountDanaBOS.Value);
        //                    ObjDb.Update("mMapCOA", ObjDb.Data, ObjDb.Where);

        //                }
        //            }
        //            this.ShowHideGridAndForm(true, false);
        //            ShowMessage("success", "Data berhasil diupdate.");
        //            clearData();
        //            loadData();
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //            ShowMessage("error", ex.ToString());
        //        }

        //    }
        //}
        private void clearData()
        {
            for (int i = 0; i < grdInstansi.Rows.Count; i++)
            {
                TextBox txtAccount = (TextBox)grdInstansi.Rows[i].FindControl("txtAccount");
                HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[i].FindControl("hdnAccount");
                TextBox txtAccountDanaBOS = (TextBox)grdInstansi.Rows[i].FindControl("txtAccountDanaBOS");
                HiddenField hdnAccountDanaBOS = (HiddenField)grdInstansi.Rows[i].FindControl("hdnAccountDanaBOS");

                txtAccount.Text = "";
                txtAccountDanaBOS.Text = "";
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false);
        }
    }
}