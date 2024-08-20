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
    public partial class TranskenaikanGaji : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdKas.DataSource = ObjGlobal.GetDataProcedure("SPKenaikanGolongan", ObjGlobal.Param);
            grdKas.DataBind();
        }

        #region setInitial & AddRow
   
       
        #endregion

        protected void grdKas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            loadData();
            grdKas.PageIndex = e.NewPageIndex;
        }

        protected void grdKas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = grdKas.SelectedRow.RowIndex;
                string id = grdKas.DataKeys[rowIndex].Values[0].ToString();
                HiddenField hdnnokaryawan = (HiddenField)grdKas.Rows[rowIndex].FindControl("hdnnokaryawan");
                HiddenField hdjenis = (HiddenField)grdKas.Rows[rowIndex].FindControl("hdjenis");

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("nokaryawan", hdnnokaryawan.Value);
                ObjGlobal.Param.Add("hdjenis", hdjenis.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPKenaikanGolongandet", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];
                Txtnama.Text = myRow["nama"].ToString();
                Txttempatlahir.Text = myRow["txttempatlahir"].ToString();
                Txtnik.Text = myRow["nik"].ToString();
                TxtJabatan.Text = myRow["Jabatan"].ToString();
                Txtpangkat.Text = myRow["pangkat"].ToString();
                txttgldiangkat.Text = myRow["tgldiangkat"].ToString();
                txtnoSK.Text = myRow["noSK"].ToString();
                hdnjns.Value= myRow["jns"].ToString();
                hdnnokaryawan1.Value = myRow["nokaryawan"].ToString();
                txtlamakerja.Text = myRow["lamakerja"].ToString();
                txtgapoklm.Text = ObjSys.IsFormatNumber(myRow["Nilai"].ToString());
                txtgolbaru.Text = myRow["golbaru"].ToString();
                txtgajibru.Text = ObjSys.IsFormatNumber(myRow["gajibru"].ToString());
                txttglmulai.Text = Convert.ToDateTime(myRow["tglmulai"]).ToString("dd-MMM-yyyy");
                if (hdjenis.Value == "Kenaikan Golongan")
                    showhidegol.Visible = true;
                else
                    showhidegol.Visible = false;
                showHideFormKas(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
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
            CloseMessage();
            showHideFormKas(true, false);
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";

            if (dtPO.Text == "")
            {
                message = ObjSys.CreateMessage("Tgl Terima harus di isi.");
                alert = "error";
                valid = false;
            }
                      
            try
            {
                if (valid == true)
                {
                    string Kode = "";

                    Kode = ObjSys.GetCodeAutoNumberNew("23", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kdTran", Kode);
                    ObjGlobal.Param.Add("nokaryawan", hdnnokaryawan1.Value);
                    ObjGlobal.Param.Add("jns", hdnjns.Value);
                    ObjGlobal.Param.Add("tgl", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("createddate", ObjSys.GetDate);
                    ObjGlobal.GetDataProcedure("SPInsertkenaikanGol", ObjGlobal.Param);

                    ObjSys.UpdateAutoNumberCode("23", Convert.ToDateTime(dtPO.Text).ToString("yyyy-MM-dd"));

                 
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data Berhasil Disimpan");
                    showHideFormKas(true, false);
                    loadData();
                }
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //    ShowMessage(alert, message);
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void ClearData()
        {
            CloseMessage();

            //dtPO.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            //txtKeterangan.Text = "";
            //txtPenerima.Text = "";

        }

        protected void grdKasBank_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }
    }
}