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
using System.IO;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransSuratTugas : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearData();
                LoadDataCombo();
            }
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            //if (txtNama.Text == "")
            //{
            //    message += ObjSys.CreateMessage("Nama cabang tidak boleh kosong.");
            //    valid = false;
            //}

            //if (ObjDb.GetRows("SELECT * FROM mCabang WHERE namaCabang = '" + txtNama.Text + "'").Tables[0].Rows.Count > 0)
            //{
            //    message += ObjSys.CreateMessage("Nama Cabang sudah ada.");
            //    valid = false;
            //}

            if (valid == true)
            {

                try
                {
                    string Kode = ObjSys.GetCodeAutoNumberNew("36", Convert.ToDateTime(date.Text).ToString("yyyy-MM-dd"));
                    ObjDb.Data.Clear();
                    ObjDb.Data.Add("kodetugas", Kode);
                    ObjDb.Data.Add("tgl", date.Text);
                    ObjDb.Data.Add("drtgl", fromDate.Text);
                    ObjDb.Data.Add("sptgl", toDate.Text);
                    ObjDb.Data.Add("noJenisTugas", cboUnit.Text);
                    ObjDb.Data.Add("noatasan", cboParent.Text);
                    ObjDb.Data.Add("noKaryawan", mhs.Text);
                    ObjDb.Data.Add("diskripsi", deskripsi.Text);
                    ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                    ObjDb.Insert("TransTugaskaryawan", ObjDb.Data);
                    ObjSys.UpdateAutoNumberCodeNew("36", Convert.ToDateTime(date.Text).ToString("yyyy-MM-dd"));

                    ClearData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
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
            CloseMessage();
            ClearData();
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
            deskripsi.Text = "";
            toDate.Text = "";
            fromDate.Text = "";
            date.Text = "";
            cboUnit.Text = "0";
            cboParent.Text = "0";
            mhs.Text = "0";
           
        }


        protected void LoadDataCombo()
        {
            cboParent.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'--Pilih--' name union all SELECT distinct noKaryawan id, nama name FROM Mstkaryawan where jabatan = '1' ) a");
            cboParent.DataValueField = "id";
            cboParent.DataTextField = "name";
            cboParent.DataBind();

            mhs.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'--Pilih--' name union all SELECT distinct noKaryawan id, nama name FROM Mstkaryawan where jabatan != '1' ) a");
            mhs.DataValueField = "id";
            mhs.DataTextField = "name";
            mhs.DataBind();

            cboUnit.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'--Pilih--' name union all SELECT distinct noJenisTugas id, JenisTugas name FROM MstJenistugas ) a");
            cboUnit.DataValueField = "id";
            cboUnit.DataTextField = "name";
            cboUnit.DataBind();

        }
    }
    
}