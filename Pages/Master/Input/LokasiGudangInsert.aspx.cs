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

namespace eFinance.Pages.Master.Input
{
    public partial class LokasiGudangInsert : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();
        //protected void CreateViewStateProduk(string index)
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.AddRange(new DataColumn[] 
        //    { 
        //        new DataColumn("noProduct")
        //    });
        //    if (ViewState["ProdukState" + index] == null)
        //    {
        //        ArrayList userdetails = new ArrayList();
        //        ViewState["CHECKED_ITEMS" + index] = userdetails;
        //        ViewState["ProdukState" + index] = dt;
        //    }

        //}
        //private void PopulateCheckedValues()
        //{
        //    ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
        //    if (userdetails != null && userdetails.Count > 0)
        //    {
        //        foreach (GridViewRow gvrow in grdAddProduk.Rows)
        //        {
        //            string index = grdAddProduk.DataKeys[gvrow.RowIndex].Value.ToString();
        //            if (userdetails.Contains(index))
        //            {
        //                CheckBox myCheckBox = (CheckBox)gvrow.FindControl("chkCheck");
        //                myCheckBox.Checked = true;
        //            }
        //        }
        //    }
        //}
        //private void SaveCheckedValues()
        //{
        //    ArrayList userdetails = new ArrayList();
        //    foreach (GridViewRow gvrow in grdAddProduk.Rows)
        //    {
        //        string index = grdAddProduk.DataKeys[gvrow.RowIndex].Value.ToString();
        //        CheckBox chkCheck = (CheckBox)grdAddProduk.Rows[gvrow.RowIndex].FindControl("chkCheck");
        //        bool result = chkCheck.Checked;
        //        // Check in the Session
        //        if (ViewState["CHECKED_ITEMS"] != null)
        //            userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
        //        if (result)
        //        {
        //            if (!userdetails.Contains(index))
        //                userdetails.Add(index);
        //        }
        //        else
        //            userdetails.Remove(index);
        //    }
        //    if (userdetails != null && userdetails.Count > 0)
        //        ViewState["CHECKED_ITEMS"] = userdetails;
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("LokasiGudangInsert.aspx");
                LoadDataCombo();
                LoadData();
            }
        }
        protected void LoadData()
        {
            //DataSet MySet = ObjDb.GetRows("select * from product");
            //if (MySet.Tables[0].Rows.Count > 0)
            //    lblJumlahData.Text = MySet.Tables[0].Rows.Count.ToString();
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
                ViewState["ProdukState"] = ObjDb.GetRowsDataTable("select noproduct,prodno,prodnm,groups,manufactur,brand,0 stsPilih from product where brand='" + cboBrand.Text + "'");
            }
            DataTable myData = (DataTable)ViewState["ProdukState"];
            myData.DefaultView.RowFilter = "stsPilih=1";
            grdAddProduk.DataSource = myData;
            grdAddProduk.DataBind();
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
        protected void ClearData()
        {
            txtKet.Text = "";
            txtnama.Text = "";
            txtSuhuDari.Text = "";
            txtSuhuKe.Text = "";
            cboGudang.Text = "0";
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
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (txtnama.Text == "")
            {
                message += ObjSys.CreateMessage("Nama tidak boleh kosong.");
                valid = false;
            }
            if (cboGudang.Text == "0")
            {
                message += ObjSys.CreateMessage("Gudang Harus dipilih.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {

                    //if (ObjDb.GetRows("select * from LokasiGudang where NamaLokGud = '" + txtnama.Text + "'").Tables[0].Rows.Count > 0)
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    //    ShowMessage("error", "Nama Lokasi Gudang sudah ada.");
                    //}
                    //else
                    //{
                    DataSet MySet = ObjDb.GetRows("Select * from Gudang where noGudang='" + cboGudang.Text + "'");
                    var kodeGudang = MySet.Tables[0].Rows[0]["kdGudang"].ToString();

                    //string Kode = ObjSys.GetCodeAutoNumberMasterLokasiGudang("35", kodeGudang, cboGudang.Text);
                    //ObjDb.Data.Add("kdLokGud", Kode);
                    ObjDb.Data.Add("NamaLokGud", txtnama.Text);
                    ObjDb.Data.Add("ketLokGud", txtKet.Text);
                    ObjDb.Data.Add("suhuDari", txtSuhuDari.Text);
                    ObjDb.Data.Add("suhuKe", txtSuhuKe.Text);
                    ObjDb.Data.Add("tglEntryLokGud", ObjSys.GetDate);
                    ObjDb.Data.Add("userEntryLokGud", ObjSys.GetUserId);
                    ObjDb.Data.Add("noGudang", cboGudang.Text);
                    ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                    ObjDb.Data.Add("stsTransfer", "0");
                    ObjDb.Data.Add("stsgud", "A");
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                    ObjDb.Data.Add("jnsGudang", DropDownList1.Text);
                    ObjDb.Insert("LokasiGudang", ObjDb.Data);

                    //DataSet MySet = ObjDb.GetRows("Select * from LokasiGudang where kdLokGud='" + Kode + "'");
                    //var noLokGudang = MySet.Tables[0].Rows[0]["noLokasiGudang"].ToString();

                    //DataTable myData = (DataTable)ViewState["ProdukState"];
                    //foreach (DataRow MyRow in myData.Select("stsPilih=1"))
                    //{
                    //    ObjDb.Data.Clear();
                    //    ObjDb.Data.Add("noProduct", MyRow["noproduct"].ToString());
                    //    ObjDb.Data.Add("noLokasiGudang", noLokGudang);
                    //    ObjDb.Data.Add("noGudang", cboGudang.Text);
                    //    ObjDb.Data.Add("kapasitas", "");
                    //    ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                    //    ObjDb.Data.Add("minSafety", "");
                    //    ObjDb.Data.Add("stsDefaultLokBar", "");
                    //    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    //    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                    //    ObjDb.Insert("mLokasiBarang", ObjDb.Data);
                    //}


                    ObjSys.UpdateAutoNumberCode("35", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                    //}
                    ClearData();
                    ShowMessage("success", "Data berhasil disimpan.");
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
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ViewState.Clear();
            ClearData();
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
    }
}