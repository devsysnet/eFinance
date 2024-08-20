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
    public partial class LokasiGudangUpdate : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected string execBind = string.Empty;

        protected void CreateViewStateProduk(string index)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("noProduct")
            });
            if (ViewState["ProdukState" + index] == null)
            {
                ArrayList userdetails = new ArrayList();
                ViewState["CHECKED_ITEMS" + index] = userdetails;
                ViewState["ProdukState" + index] = dt;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                //ObjSys.SessionCheck("LokasiGudangUpdate.aspx");
                LoadData();
                LoadDataCombo();
                LoadDataView();
            }
        }
        protected void LoadData()
        {
            //DataSet MySet = ObjDb.GetRows("select * from product");
            //if (MySet.Tables[0].Rows.Count > 0)
            //    lblJumlahData.Text = MySet.Tables[0].Rows.Count.ToString();
        }
        protected void LoadDataView()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdLokGudang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUpdateLokGudang", ObjGlobal.Param);
            grdLokGudang.DataBind();
        }
        protected void LoadDataCombo()
        {
            cboGudang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-------' name union all SELECT noGudang id, kdGudang +'-'+ namaGudang name FROM Gudang where stsGudang not in ('Inspeksi') and noCabang='" + ObjSys.GetCabangId + "') a");
            cboGudang.DataValueField = "id";
            cboGudang.DataTextField = "name";
            cboGudang.DataBind();

            cboBrand.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-------' name union all SELECT brand id, brand name FROM mbrand) a");
            cboBrand.DataValueField = "id";
            cboBrand.DataTextField = "name";
            cboBrand.DataBind();
        }
        protected void LoadDataProduk()
        {
            if (ViewState["ProdukState"] == null)
            {
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noLokasiGudang", hdnId.Value);
                ViewState["ProdukState"] = ObjGlobal.GetDataProcedureDataTable("SPLoadDataViewProductLokGudang", ObjGlobal.Param);
            }
            DataTable myData = (DataTable)ViewState["ProdukState"];
            myData.DefaultView.RowFilter = "stsPilih=1";
            grdAddProduk.DataSource = myData;
            grdAddProduk.DataBind();
        }

        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdLokGudang.Rows)
                {
                    string index = grdLokGudang.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdLokGudang.Rows)
            {
                string index = grdLokGudang.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdLokGudang.Rows[gvrow.RowIndex].FindControl("chkCheck");
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
                            ObjDb.Where.Add("noLokasiGudang", itemRow);
                            ObjDb.Delete("LokasiGudang", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdLokGudang.Rows)
                    {
                        string itemId = grdLokGudang.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdLokGudang.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noLokasiGudang", itemId);
                            ObjDb.Delete("LokasiGudang", ObjDb.Where);
                        }
                    }
                }
                /*END DELETE ALL SELECTED*/
                LoadData();
                this.ShowHideGridAndForm(true, false);
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil dihapus.");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        protected void ClearData()
        {

            CloseMessage();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDataView();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ShowHideGridAndForm(true, false);
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;


            if (txtnama.Text == "")
            {
                message += ObjSys.CreateMessage("Nama tidak boleh kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                    DataSet MySet = ObjDb.GetRows("Select * from Gudang where noGudang='" + cboGudang.Text + "'");
                    var kodeGudang = MySet.Tables[0].Rows[0]["kdGudang"].ToString();

                    string Kode = ObjSys.GetCodeAutoNumber("36", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));

                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noLokasiGudang", hdnId.Value);
                    ObjDb.Data.Add("kdLokGud", Kode);
                    ObjDb.Data.Add("NamaLokGud", txtnama.Text);
                    ObjDb.Data.Add("ketLokGud", txtKet.Text);
                    ObjDb.Data.Add("suhuDari", txtSuhuDari.Text);
                    ObjDb.Data.Add("suhuKe", txtSuhuKe.Text);
                    ObjDb.Data.Add("tglEntryLokGud", ObjSys.GetDate);
                    ObjDb.Data.Add("userEntryLokGud", ObjSys.GetUserId);
                    ObjDb.Data.Add("noGudang", cboGudang.Text);
                    ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                    ObjDb.Data.Add("modifiedBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modifiedDate", ObjSys.GetNow);
                    ObjDb.Update("LokasiGudang", ObjDb.Data, ObjDb.Where);

                    //if (ViewState["ProdukState"] != null)
                    //{
                    //    ObjDb.ExecQuery("delete from mLokasiBarang where noLokasiGudang = '" + hdnId.Value + "'");
                    //    DataTable myData = (DataTable)ViewState["ProdukState"];
                    //    foreach (DataRow MyRow in myData.Select("stsPilih=1"))
                    //    {
                    //        ObjDb.Data.Clear();
                    //        ObjDb.Data.Add("noProduct", MyRow["noproduct"].ToString());
                    //        ObjDb.Data.Add("noLokasiGudang", hdnId.Value);
                    //        ObjDb.Data.Add("noGudang", cboGudang.Text);
                    //        ObjDb.Data.Add("kapasitas", "");
                    //        ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                    //        ObjDb.Data.Add("minSafety", "");
                    //        ObjDb.Data.Add("stsDefaultLokBar", "");
                    //        ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    //        ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                    //        ObjDb.Insert("mLokasiBarang", ObjDb.Data);
                    //    }
                    //}
                    ObjSys.UpdateAutoNumberCode("36", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));

                    ClearData();
                    LoadDataView();
                    ShowMessage("success", "Data berhasil diupdate.");
                    ShowHideGridAndForm(true, false);
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
        protected void btnSubmitProduk_Click(object sender, EventArgs e)
        {
            DataTable myData = (DataTable)ViewState["ProdukState"];
            for (int i = 0; i < grdAddProduk.Rows.Count; i++)
            {
                CheckBox chkCheck = (CheckBox)grdAddProduk.Rows[i].FindControl("chkCheck");
                DataRow[] rowCek = myData.Select("noproduct='" + grdAddProduk.DataKeys[i].Value.ToString() + "'");
                if (chkCheck.Checked == true)
                    rowCek[0]["stsPilih"] = "1";
                else
                    rowCek[0]["stsPilih"] = "0";
                myData.AcceptChanges();
                myData.Rows[i].SetModified();

            }

            ViewState["ProdukState"] = myData;
            DataRow[] MyRow = myData.Select("stsPilih=1");
            //lblJumlahPilih.Text = MyRow.Length.ToString();
        }
        protected void btnPilihProduk_Click(object sender, EventArgs e)
        {
            LoadDataProduk();
            dlgAddProduk.Show();
        }
        protected void btnPilihSemuaProduk_Click(object sender, EventArgs e)
        {
            if (ViewState["ProdukState"] == null)
            {
                ViewState["ProdukState"] = ObjDb.GetRowsDataTable("select noproduct,prodno,prodnm,groups,manufactur,brand,1 stsPilih from product");
            }
            DataTable myData = (DataTable)ViewState["ProdukState"];
            ViewState["ProdukState"] = myData;
            DataRow[] MyRow = myData.Select("stsPilih=1");
            //lblJumlahPilih.Text = MyRow.Length.ToString();
        }

        protected void grdAddProduk_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnStsPilih = (HiddenField)e.Row.FindControl("hdnStsPilih");
                CheckBox chkCheck = (CheckBox)e.Row.FindControl("chkCheck");

                if (hdnStsPilih.Value == "1")
                    chkCheck.Checked = true;
                else
                    chkCheck.Checked = false;
            }
        }
        protected void grdLokGudang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLokGudang.PageIndex = e.NewPageIndex;
            LoadDataView();
        }
        protected void grdLokGudang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdLokGudang.SelectedRow.RowIndex;
                string noLokGudang = grdLokGudang.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noLokGudang;
                DataSet MySet = ObjDb.GetRows("select * from LokasiGudang where noLokasiGudang='" + noLokGudang + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0];

                    txtnama.Text = MyRow["namaLokGud"].ToString();
                    txtKet.Text = MyRow["ketLokGud"].ToString();
                    cboGudang.Text = MyRow["noGudang"].ToString();
                    txtSuhuDari.Text = MyRow["suhuDari"].ToString();
                    txtSuhuKe.Text = MyRow["suhuKe"].ToString();
                    //LoadDataProduk();
                    ShowHideGridAndForm(false, true);
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



    }
}