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

namespace eFinance.Pages.Master.Update
{
    public partial class MstAccountUpdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                LoadDataGrid();
            }
        }

        #region 
        protected void LoadDataGrid()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearch.Text);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAccountDetail", ObjGlobal.Param);
            grdAccount.DataBind();

            IndexPakai();
        }


        protected void IndexPakai()
        {
            for (int i = 0; i < grdAccount.Rows.Count; i++)
            {
                string itemId = grdAccount.DataKeys[i].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdAccount.Rows[i].FindControl("chkCheck");


                DataSet mySet1 = ObjDb.GetRows("Select distinct norek from tkas Where norek = '" + itemId + "'");
                if (mySet1.Tables[0].Rows.Count > 0)
                    chkCheck.Visible = false;
                else
                    chkCheck.Visible = true;

            }

        }
        protected void loadDataCombo()
        {

            cboJnsTipe.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nourut id, tipe name FROM mTipeCOA) a order by name");
            cboJnsTipe.DataValueField = "id";
            cboJnsTipe.DataTextField = "name";
            cboJnsTipe.DataBind();

            cboCabang.DataSource = ObjDb.GetRows("select a.* from(select 0 as id,'---Pilih Cabang---' as name ,0 as urutan union SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang in(2,3,4)) a order by urutan");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();

        }
        protected void LoadData()
        {
            //DataSet mySet = ObjDb.GetRows("select '--------'+'^'+PosisiKeuangan+'^'+Aktivitas as GroupParameter from Parameter");
            //DataRow myRow = mySet.Tables[0].Rows[0];
            //string[] Group = myRow["GroupParameter"].ToString().Split('^');

            //foreach (string Value in Group)
            //cboGroup.Items.Add(Value);
            

        }

        protected void LoadDataParent()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearchPop.Text);
            grdRekening.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAccount", ObjGlobal.Param);
            grdRekening.DataBind();

        }
        #endregion

        #region Search
        protected void btnCari_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataGrid();
        }

        protected void btnCariRek_Click(object sender, EventArgs e)
        {
            LoadDataParent();
            dlgParentAccount.Show();
        }
        #endregion

        #region POPUP
        protected void imgButtonProduct_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataParent();
            dlgParentAccount.Show();
        }
        #endregion

        #region Update
        protected void btnSimpan_Click(object sender, EventArgs e)
        {

            string message = "";
            bool valid = true;

            if (cboGroup.Text == "")
            {
                message += ObjSys.CreateMessage("Group harus dipilih.");
                valid = false;
            }
            if (txtAccountCode.Text == "")
            {
                message += ObjSys.CreateMessage("Kode COA harus diisi");
                valid = false;
            }
            if (txtAccountCode.Text != "")
            {
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noRek", hdnId.Value);
                ObjGlobal.Param.Add("kdRek", txtAccountCode.Text);
                if (ObjGlobal.GetDataProcedure("SPLoadDataAccountDUpdate", ObjGlobal.Param).Tables[0].Rows.Count > 0)
                {
                    message += ObjSys.CreateMessage("Nomor Rekening sudah ada.");
                    valid = false;
                }
            }

            if (txtRemark.Text == "")
            {
                message += ObjSys.CreateMessage("Keterangan COA harus diisi.");
                valid = false;
            }
            if (cboPosition.Text == "")
            {
                message += ObjSys.CreateMessage("Posisi harus dipilih");
                valid = false;
            }
            if (cboType.Text == "")
            {
                message += ObjSys.CreateMessage("Kategori harus dipilih.");
                valid = false;
            }

            try
            {
                if (valid == true)
                {
                    ObjDb.Where.Clear();
                    ObjDb.Data.Clear();
                    ObjDb.Where.Add("noRek", hdnId.Value);
                    ObjDb.Data.Add("kdRek", txtAccountCode.Text);
                    ObjDb.Data.Add("Ket", txtRemark.Text);
                    string parent = "0";
                    if (txtParent.Text != "")
                    {
                        parent = hdnParent.Value;
                    }
   
                    ObjDb.Data.Add("parent", parent);
                    if (parent != "0")
                    {
                        DataSet mySet_CekTingkat = ObjDb.GetRows("select * from mRekening where noRek = '" + parent + "'");
                        DataRow myRow_CekTingkat = mySet_CekTingkat.Tables[0].Rows[0];
                        if (Convert.ToInt32(myRow_CekTingkat["tingkat"]) > 0)
                        {
                            int Tingkat = Convert.ToInt32(myRow_CekTingkat["tingkat"]) + 1;
                            ObjDb.Data.Add("tingkat", Tingkat.ToString());
                        }
                        else
                        {
                            ObjDb.Data.Add("tingkat", "1");
                        }
                    }
                    else
                    {
                        ObjDb.Data.Add("tingkat", "1");
                    }

                    ObjDb.Data.Add("pos", cboPosition.Text);
                    ObjDb.Data.Add("Grup", hdnKelompok.Value);
                    ObjDb.Data.Add("sts", cboType.Text);
                    //ObjDb.Data.Add("Kelompok", cboGroup.Text);
                    ObjDb.Data.Add("userEntryRek", ObjSys.GetUserId);
                    ObjDb.Data.Add("jenis", cboJnsTipe.Text);
                    ObjDb.Data.Add("kelaktivitas", cboRekMon.Text);
                    ObjDb.Data.Add("nocabang", cboCabang.Text);
                    ObjDb.Data.Add("stsaktif", cboAktif.Text);

                    ObjDb.Update("mRekening", ObjDb.Data, ObjDb.Where);
                    ObjGlobal.ExecuteProcedure("SPIinsertviewRekening");
                    ObjGlobal.ExecuteProcedure("SPIinsertviewRekeningRL");

                    ClearData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }
        #endregion

        #region PageSelectAndPagging
        protected void grdRekening_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRekening.PageIndex = e.NewPageIndex;
            LoadDataParent();
            dlgParentAccount.Show();
        }

        protected void grdRekening_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = grdRekening.SelectedRow.RowIndex;
                string Id = grdRekening.DataKeys[rowIndex].Values[0].ToString();
                string kdRek = grdRekening.SelectedRow.Cells[1].Text;

                hdnParent.Value = Id;
                txtParent.Text = kdRek;
                LoadDataParent();
                dlgParentAccount.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void grdAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdAccount.PageIndex = e.NewPageIndex;
            LoadDataGrid();
        }

        protected void grdAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                int rowIndex = grdAccount.SelectedRow.RowIndex;
                string Id = grdAccount.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = Id;
                DataSet mySet = ObjDb.GetRows("select * from mRekening  where noRek = '" + hdnId.Value + "'");

                if (mySet.Tables[0].Rows.Count > 0)
                {
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtAccountCode.Text = myRow["kdRek"].ToString();
                    txtRemark.Text = myRow["Ket"].ToString();
                    hdnKelompok.Value = myRow["Grup"].ToString();
                    cboJnsTipe.Text = myRow["jenis"].ToString();
                    cboPosition.Text = myRow["pos"].ToString();
                    cboRekMon.Text = myRow["kelaktivitas"].ToString();
                    cboType.Text = myRow["sts"].ToString();
                    cboCabang.Text = myRow["nocabang"].ToString();
                    cboGroup.Text = myRow["kelompok"].ToString();
                    hdnParent.Value = myRow["parent"].ToString();

                    DataSet mysetParent = ObjDb.GetRows("select * from mRekening where noRek = '" + hdnParent.Value + "'");
                    if (mysetParent.Tables[0].Rows.Count > 0)
                    {
                        DataRow myRowParent = mysetParent.Tables[0].Rows[0];
                        txtParent.Text = myRowParent["kdRek"].ToString();
                    }
                    loadDataCombo();
                    LoadDataParent();
                    showHideForm(false, true);
                    
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Tidak ada.");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }
        #endregion

        #region Delete
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
                            ObjDb.Where.Add("noRek", itemRow);
                            ObjDb.Delete("mRekening", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdAccount.Rows)
                    {
                        string itemId = grdAccount.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdAccount.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noRek", itemId);
                            ObjDb.Delete("mRekening", ObjDb.Where);
                        }
                    }
                }
                /*END DELETE ALL SELECTED*/
                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil dihapus.");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        #endregion

        #region Other
        protected void ClearData()
        {
            cboPosition.Text = "0";
            //cboGroup.Text = "--------";
            cboRekMon.Text = "0";
            cboType.Text = "0";
            txtAccountCode.Text = "";
            txtParent.Text = "";
            txtRemark.Text = "";
            hdnParent.Value = "";
            LoadData();
            LoadDataParent();
            LoadDataGrid();
            showHideForm(true, false);
        }

        protected void showHideForm(bool DivGrid, bool DivForm)
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

        protected void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cboGroup.SelectedItem.ToString() == "PosisiKeuangan" || cboGroup.SelectedItem.ToString() == "Aktivitas")
            //{
            //    hdnKelompok.Value = "ASSET";
            //}
            //else
            //{
            //    hdnKelompok.Value = "PERUBAHAN ASSET NETTO TIDAK TERIKAT";
            //}
            CloseMessage();
        }

        protected void btnResetRek_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        #endregion

        protected void btnCariPopUp_Click(object sender, EventArgs e)
        {
            LoadDataParent();
            dlgParentAccount.Show();
        }
    }
}