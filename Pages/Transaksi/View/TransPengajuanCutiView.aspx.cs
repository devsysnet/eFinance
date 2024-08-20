using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.View
{
    public partial class TransPengajuanCutiView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                this.ShowHideGridAndForm(true, false);
            }
        }

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdBarang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataCuti", ObjGlobal.Param);
            grdBarang.DataBind();

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
                        ObjGlobal.Param.Add("nocuti", hdnId.Value);
                        ObjGlobal.Param.Add("tglpengajuan", dttgl.Text);
                        ObjGlobal.Param.Add("tglmulaicuti", dttgldr.Text);
                        ObjGlobal.Param.Add("tglselesaicuti", dttglsd.Text);
                        ObjGlobal.Param.Add("saldocuti", txtsaldo.Text);
                        ObjGlobal.Param.Add("uraian", txtUraian.Text);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.GetDataProcedure("SPUpdatecuti", ObjGlobal.Param);

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

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("nocuti", hdnId.Value);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadcutiDetil", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtKode.Text = myRow["kodecuti"].ToString();
                    txtnama.Text = myRow["nama"].ToString();
                    txtsaldo.Text = myRow["saldo"].ToString();
                    dttgl.Text = myRow["tglpengajuan"].ToString();
                    dttgldr.Text = myRow["tglmulaicuti"].ToString();
                    dttglsd.Text = myRow["tglselesaicuti"].ToString();
                    txtUraian.Text = myRow["Uraian"].ToString();

                    this.ShowHideGridAndForm(false, true);
                }
                else
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nobarang = grdBarang.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nobarang;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("nocuti", hdnId.Value);
                    ObjDb.Delete("transcuti", ObjDb.Where);

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