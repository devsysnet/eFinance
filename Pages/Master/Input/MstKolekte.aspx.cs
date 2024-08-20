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
    public partial class MstKolekte : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtkursrate.Text = "1,00";
                SetInitialRow();
                for (int i = 1; i < 5; i++)
                {
                    AddNewRow();
                }
                cboCurrencyTrans.DataSource = ObjDb.GetRows("select a.* from ( SELECT distinct noMataUang id, namaMataUang name FROM mMataUang) a");
                cboCurrencyTrans.DataValueField = "id";
                cboCurrencyTrans.DataTextField = "name";
                cboCurrencyTrans.DataBind();
            }
        }

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
            grdMemoJurnal.DataSource = dt;
            grdMemoJurnal.DataBind();
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
                        TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                        HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                        TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");
                        TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtRemark");
                        TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtKredit");

                        txtAccount.Text = dt.Rows[i]["Column1"].ToString();
                        hdnAccount.Value = dt.Rows[i]["Column2"].ToString();
                        txtDescription.Text = dt.Rows[i]["Column3"].ToString();
                        txtRemark.Text = dt.Rows[i]["Column4"].ToString();
                        if (dt.Rows[i]["Column5"].ToString() == "")
                            txtDebit.Text = "0.00";
                        else
                            txtDebit.Text = dt.Rows[i]["Column5"].ToString();
                        if (dt.Rows[i]["Column6"].ToString() == "")
                            txtKredit.Text = "0.00";
                        else
                            txtKredit.Text = dt.Rows[i]["Column6"].ToString();
                    }
                }
            }
        }
        protected void grdMemoJurnal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                txtHdnPopup.Value = rowIndex.ToString();
                if (e.CommandName == "Clear")
                {
                    TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtAccount");
                    HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[rowIndex].FindControl("hdnAccount");
                    TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtDescription");
                    TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtRemark");
                    TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtDebit");
                    TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtKredit");

                    txtAccount.Text = "";
                    hdnAccount.Value = "";
                    txtDescription.Text = "";
                    txtRemark.Text = "";
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
                        TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                        HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                        TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");
                        TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtRemark");
                        TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtKredit");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtAccount.Text;
                        dtCurrentTable.Rows[i]["Column2"] = hdnAccount.Value;
                        dtCurrentTable.Rows[i]["Column3"] = txtDescription.Text;
                        dtCurrentTable.Rows[i]["Column4"] = txtRemark.Text;
                        dtCurrentTable.Rows[i]["Column5"] = txtDebit.Text;
                        dtCurrentTable.Rows[i]["Column6"] = txtKredit.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdMemoJurnal.DataSource = dtCurrentTable;
                    grdMemoJurnal.DataBind();
                }
            }
            SetPreviousData();
        }
        #endregion
        protected void CloseMessage()
        {
            ((Label)Master.FindControl("lblMessage")).Text = "";
            ((Label)Master.FindControl("lblMessage")).Visible = false;
        }
        protected void cboCurrencyTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nilai = "0.00";
            DataSet mySetH = ObjDb.GetRows("select TOP 1 nilaiKursPajak from tKursPajak where noMatauang = '" + cboCurrencyTrans.SelectedValue + "'");
            DataSet mySetHx = ObjDb.GetRows("select stsDefault from mmatauang where noMatauang = '" + cboCurrencyTrans.SelectedValue + "'");
            DataRow myRowHx = mySetHx.Tables[0].Rows[0];


            //if (myRowHx["stsDefault"].ToString() == "1")
            //{
            //    nilai = myRowHx["stsDefault"].ToString();
            //}
            //else
            //{
            //    DataRow myRowH = mySetH.Tables[0].Rows[0];
            //    if (mySetH.Tables[0].Rows.Count > 0)
            //    {
            //        nilai = myRowH["nilaiKursPajak"].ToString();
            //    }
            //    else
            //    {
            //        nilai = "0.00";
            //    }


            //}

            if (myRowHx["stsDefault"].ToString() == "1")
            {
                nilai = myRowHx["stsDefault"].ToString();
            }
            else
            {
                nilai = "0.00";
            }

            txtkursrate.Visible = true;
            Kurstrs2.Visible = true;
            txtkursrate.Text = ObjSys.IsFormatNumber(nilai);
        }
        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }
        protected void grdMemoJurnal_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kdRek = (grdMemoJurnal.SelectedRow.FindControl("txtAccount") as TextBox).Text;
            if (kdRek != "")
            {
                string kodeRek = (grdMemoJurnal.SelectedRow.FindControl("txtAccount") as TextBox).Text;
                LoadDataPanelkdRek(kodeRek);
                mpe.Show();
                string value = (grdMemoJurnal.SelectedRow.FindControl("txtHdnValue") as HiddenField).Value;
                txtHdnPopup.Value = value;
                txtSearch.Text = "";
            }
            if (kdRek == "")
            {
                LoadDataPanel();
                mpe.Show();
                string value = (grdMemoJurnal.SelectedRow.FindControl("txtHdnValue") as HiddenField).Value;
                txtHdnPopup.Value = value;
                txtSearch.Text = "";
            }

        }
        private void LoadDataPanelkdRek(string kodeRek = "")
        {
            grdProduct.DataSource = ObjDb.GetRows("select * from mRekening where (kdRek like '%" + kodeRek + "%' and sts = '2') or (Ket like '%" + kodeRek + "%' and sts='2')");
            grdProduct.DataBind();
        }
        #region PopUpProduct
        private void LoadDataPanel()
        {
            grdProduct.DataSource = ObjDb.GetRows("select * from mRekening where  (kdRek like '%" + txtSearch.Text + "%' and sts = '2') or (Ket like '%" + txtSearch.Text + "%' and sts='2')");
            grdProduct.DataBind();
        }
        protected void btnCariProduct_Click(object sender, EventArgs e)
        {
            LoadDataPanel();
            mpe.Show();
        }

        protected void grdProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(txtHdnPopup.Value);
            string prodno = "", prodnm = "", noprod = "";
            prodno = (grdProduct.SelectedRow.FindControl("lblKodeReagent") as Label).Text;
            prodnm = (grdProduct.SelectedRow.FindControl("lblNamaReagent") as Label).Text;
            noprod = (grdProduct.SelectedRow.FindControl("hidNoReagent") as HiddenField).Value;

            TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[rowIndex - 1].FindControl("txtAccount");
            TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[rowIndex - 1].FindControl("txtDescription");
            HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[rowIndex - 1].FindControl("hdnAccount");

            int noCek = 0;
            for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
            {
                TextBox txtAccountt = (TextBox)grdMemoJurnal.Rows[i].Cells[1].FindControl("txtAccount");
                if (txtAccountt.Text == prodno)
                {
                    noCek += 1;
                }
            }
            //if (noCek > 0)
            //{
            //    Response.Write("<script>alert('Account yang dipilih tidak boleh sama !');</script>");
            //    mpe.Show();
            //}
            //else
            {
                txtAccount.Text = prodno;
                txtDescription.Text = prodnm;
                hdnAccount.Value = noprod;
            }

            mpe.Hide();
        }

        protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProduct.PageIndex = e.NewPageIndex;
            LoadDataPanel();
            mpe.Show();
        }
        #endregion
        protected void txtAccount_TextChanged(object sender, EventArgs e)
        {
            autoComplete();
        }

        protected void autoComplete()
        {
            for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
            {
                HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("kdRek", txtAccount.Text.Replace(" ", ""));
                DataSet mySet = ObjGlobal.GetDataProcedure("SPmrekening", ObjGlobal.Param);

                foreach (DataRow myRow in mySet.Tables[0].Rows)
                {
                    txtAccount.Text = myRow["kdRek"].ToString();
                    hdnAccount.Value = myRow["noRek"].ToString();
                    txtDescription.Text = myRow["Ket"].ToString();
                }
            }
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

            if (dtDate.Text == "0")
            {
                message += ObjSys.CreateMessage("Jenis Kolekte harus dipilih.");
                valid = false;
            }
 

 
            if (valid == true)
            {
                try
                {


                    ObjDb.Data.Clear();
                    ObjDb.Data.Add("jenisKolekte", dtDate.Text);
                    ObjDb.Data.Add("sts", "1");
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("createddate", ObjSys.GetNow);
                    ObjDb.Insert("MstJenisKolekte_h", ObjDb.Data);

                    for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
                    {
                        HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                        DropDownList cbojns = (DropDownList)grdMemoJurnal.Rows[i].FindControl("cbojns");
                        TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                      

                        if (txtAccount.Text != "")
                        {
                            DataSet mySetHx = ObjDb.GetRows("select top 1 nokolekte from MstJenisKolekte_h order by nokolekte desc");
                            DataRow myRowHx = mySetHx.Tables[0].Rows[0];


                           
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("jenis", cbojns.Text);
                            ObjDb.Data.Add("pos", "0");
                            ObjDb.Data.Add("nokolekte", myRowHx["nokolekte"].ToString());
                            ObjDb.Data.Add("norek", hdnAccount.Value);
                            ObjDb.Insert("MstJenisKolekte_d", ObjDb.Data);
                        }

                    }

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
        private void clearData()
        {
            dtDate.Text = "";

            for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
            {
                HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                DropDownList cbojns = (DropDownList)grdMemoJurnal.Rows[i].FindControl("cbojns");
                TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtRemark");
                TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDebit");
                TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtKredit");

                hdnAccount.Value = "";
                txtAccount.Text = "";
                cbojns.Text = "1";
                txtRemark.Text = "";
                txtDebitTotal.Text = "";
                txtKreditTotal.Text = "";
                SetPreviousData();
                cboType.Text = "0";
                dtDate.Text = "";
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            clearData();
        }

    }
}