using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using eFinance.GlobalApp;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransInputNilaiSoalKPI : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                LoadDataSoal();
                LoadDataKaryawan(cboUnit.Text);

            }
        }

      
        #region LoadData
        protected void LoadDataGrid(string nosoal = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nosoal", nosoal);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPviewSoalKPI", ObjGlobal.Param);
            grdAccount.DataBind();


        }

        protected void jenis_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string noProduk = "0.00";
            //DataSet mySetH = ObjDb.GetRows("select TOP 1 nilaiKursPajak from tKursPajak where noMatauang = '" + cboCurrency.SelectedValue + "'");
            //if (mySetH.Tables[0].Rows.Count > 0)
            //{
            //    DataRow myRowH = mySetH.Tables[0].Rows[0];
            //    noProduk = myRowH["nilaiKursPajak"].ToString();
            //}

            //if (cboCurrency.Text == "20")
            //{
            //    showhidekurs.Visible = false;
            //    txtKurs.Text = "1.00";
            //}
            //else
            //{
            //    showhidekurs.Visible = true;
            //    txtKurs.Text = ObjSys.IsFormatNumber(noProduk).ToString();
            //}

            //ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
        }

        protected void LoadDataSoal()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nosoal", "");
            cboSoal.DataSource = ObjGlobal.GetDataProcedure("SPloadDataSoalKPIinput", ObjGlobal.Param);
            cboSoal.DataValueField = "id";
            cboSoal.DataTextField = "name";
            cboSoal.DataBind();

      
        }
        protected void cbosoal_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataGrid(cboSoal.Text);

        }
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            cboUnit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitNew", ObjGlobal.Param);
            cboUnit.DataValueField = "id";
            cboUnit.DataTextField = "name";
            cboUnit.DataBind();

            LoadDataKaryawan(cboUnit.Text);
        }
        protected void LoadDataKaryawan(string unit="")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("perwakilan", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("cabang", unit);
            cboKaryawan.DataSource = ObjGlobal.GetDataProcedure("SPloadDataKaryawanUnit", ObjGlobal.Param);
            cboKaryawan.DataValueField = "id";
            cboKaryawan.DataTextField = "name";
            cboKaryawan.DataBind();
        }
        #endregion
        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataKaryawan(cboUnit.Text);

        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
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
            for (int i = 0; i < grdAccount.Rows.Count; i++)
            {
                TextBox txtNilai = (TextBox)grdAccount.Rows[i].FindControl("txtNilai");
                txtNilai.Text = "0";
            }
            txtKeterangan.Text = "";
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;


            if (cboKaryawan.Text == "")
            {
                message += ObjSys.CreateMessage("Karyawan harus diisi");
                valid = false;
            }
            

            try
            {
                if (valid == true)
                {
                            
                    int totnilai = 0;
                    for (int i = 0; i < grdAccount.Rows.Count; i++)
                    {
                        TextBox txtNilai = (TextBox)grdAccount.Rows[i].FindControl("txtNilai");
                        totnilai += Int32.Parse(txtNilai.Text);
                    }
                    string Kode = ObjSys.GetCodeAutoNumberNew("38", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kodeUjian", Kode);
                    ObjGlobal.Param.Add("tgl", dttgl.Text);
                    ObjGlobal.Param.Add("nocabang", cboUnit.Text);
                    ObjGlobal.Param.Add("totalNilai", totnilai.ToString());
                    ObjGlobal.Param.Add("nokaryawan", cboKaryawan.Text);
                    ObjGlobal.Param.Add("nosoalParent", cboSoal.Text);
                    ObjGlobal.Param.Add("catatan", txtKeterangan.Text);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.ExecuteProcedure("SPInsertSoalKPIH", ObjGlobal.Param);
                    ObjSys.UpdateAutoNumberCode("38", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));

                    DataSet mySetH = ObjDb.GetRows("Select * from TranssoalKPI_H Where kodUjian = '" + Kode + "'");
                    if (mySetH.Tables[0].Rows.Count > 0)
                    {
                        DataRow myRowH = mySetH.Tables[0].Rows[0];
                        string noUjian = myRowH["noUjian"].ToString();
                        for (int i = 0; i < grdAccount.Rows.Count; i++)
                        {
                            TextBox txtNilai = (TextBox)grdAccount.Rows[i].FindControl("txtNilai");
                            HiddenField hdnNoRek = (HiddenField)grdAccount.Rows[i].FindControl("hdnNoRek");

                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("nilai", txtNilai.Text);
                            ObjGlobal.Param.Add("nosoal", hdnNoRek.Value);
                            ObjGlobal.Param.Add("noUjian", noUjian);
  

                            ObjGlobal.ExecuteProcedure("SPInsertSoalKPID", ObjGlobal.Param);
                        }
                    }


                 
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
    }
}





