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
    public partial class TransThrbonusupdate : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //LoadData();
                loadDataCombo();
                this.ShowHideGridAndForm(true, false);
                
            }
        }

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("jenis", cboStatus.SelectedValue);
            ObjGlobal.Param.Add("tgl", dtPO.Text);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            grdBarang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataupdpendapatan", ObjGlobal.Param);
            grdBarang.DataBind();
        }


        private void loadDataCombo()
        {
            cboStatus.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct nokomponengaji id, komponengaji name FROM mstkomponenGaji where kategori='1') a");
            cboStatus.DataValueField = "id";
            cboStatus.DataTextField = "name";
            cboStatus.DataBind();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdBarang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdBarang.PageIndex = e.NewPageIndex;
            LoadData();
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false);
            LoadData();
        }



        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (txtnama.Text == "")
            {
                message += ObjSys.CreateMessage("Nama Karyawan Tidak Boleh Kosong.");
                valid = false;
            }


            if (valid == true)

            {
                //if (ObjDb.GetRows("SELECT * FROM TransPinjamankaryawan WHERE namaBarang = '" + txtnama.Text + "' and nobarang <> '" + hdnId.Value + "'").Tables[0].Rows.Count > 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //    ShowMessage("error", "Data Siswa sudah ada.");
                //}
                //else
                {

                    try
                    {
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("notrs", hdnId.Value);
                        ObjGlobal.Param.Add("nokomponengaji", hdnkomponenD.Value);
                        ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
                        ObjGlobal.Param.Add("tgl", Convert.ToDateTime(dttgl.Text).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("nilai", Convert.ToDecimal(txtPinjaman.Text).ToString());
                        ObjGlobal.Param.Add("modiBy", ObjSys.GetUserId);
                        ObjGlobal.GetDataProcedure("SPUpdatependapatanbln", ObjGlobal.Param);

                        LoadData();
                        this.ShowHideGridAndForm(true, false);
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil diubah.");
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", ex.ToString());
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }

        protected void grdBarang_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectEdit")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nobarang = grdBarang.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nobarang;
                    HiddenField nokomponengajix = (HiddenField)grdBarang.Rows[rowIndex].FindControl("hdnkomponen");
                    hdnkomponenD.Value = nokomponengajix.Value;

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("notrs", hdnId.Value);
                    ObjGlobal.Param.Add("nokomponengaji", hdnkomponenD.Value);
                    ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataupdpendapatanDetail", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtKode.Text = myRow["nik"].ToString();
                    txtnama.Text = myRow["nama"].ToString();
                    txtjabatan.Text = myRow["Jabatan"].ToString();
                    txtGolongan.Text = myRow["Jabatan"].ToString();
                    dttgl.Text = myRow["tgl"].ToString();
                    txtPinjaman.Text = myRow["nilai"].ToString();
                    this.ShowHideGridAndForm(false, true);

                }
                else
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nobarang = grdBarang.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nobarang;

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("notrs", hdnId.Value);
                    ObjGlobal.Param.Add("nokomponengaji", hdnkomponenD.Value);
                    ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
                    ObjGlobal.GetDataProcedure("SPDeletependapatanbln", ObjGlobal.Param);

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Success Deleted");
                    this.ShowHideGridAndForm(true, false);
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