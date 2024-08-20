using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.Input
{
    public partial class MstAccount : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstAccount.aspx");
                LoadData();
                LoadDataParent();
                loadDataCombo();
            }
        }

        protected void LoadDataParent()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearch.Text);
            grdRekening.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAccount", ObjGlobal.Param);
            grdRekening.DataBind();
        }

        protected void LoadData()
        {
            DataSet mySet = ObjDb.GetRows("select '---Pilih Group---'+'^'+PosisiKeuangan+'^'+Aktivitas+'^'+danabos as GroupParameter from Parameter");
            DataRow myRow = mySet.Tables[0].Rows[0];
            string[] Group = myRow["GroupParameter"].ToString().Split('^');

            foreach (string Value in Group)
                cboGroup.Items.Add(Value);

          
        }

        protected void loadDataCombo()
        {



                cboJnsTipe.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nourut id, tipe name FROM mTipeCOA) a order by id");
                cboJnsTipe.DataValueField = "id";
                cboJnsTipe.DataTextField = "name";
                cboJnsTipe.DataBind();

                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name ,0 as urutan union SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang in(2,3,4)) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();

               

        }
        protected void ClearData()
        {
            cboPosition.Text = "0";
            cboGroup.Text = "---Pilih Group---";
            cboRekMon.Text = "0";
            cboType.Text = "0";
            txtAccountCode.Text = "";
            txtParent.Text = "";
            txtRemark.Text = "";
            hdnID.Value = "";
            LoadDataParent();
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
                ObjGlobal.Param.Add("kdRek", txtAccountCode.Text);
                if (ObjGlobal.GetDataProcedure("SPLoadDataAccountD", ObjGlobal.Param).Tables[0].Rows.Count > 1)
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
                    
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kdRek", txtAccountCode.Text);
                    ObjGlobal.Param.Add("Ket", txtRemark.Text);
                    if (txtParent.Text != "")
                    {
                        ObjGlobal.Param.Add("parent", hdnID.Value);
                    }
                    else
                    {
                        ObjGlobal.Param.Add("parent", "0");
                    }
                    if (hdnID.Value != "")
                    {
                        DataSet mySet_CekTingkat = ObjDb.GetRows("select * from mRekening where noRek = '" + hdnID.Value + "'");
                        DataRow myRow_CekTingkat = mySet_CekTingkat.Tables[0].Rows[0];
                        if (Convert.ToInt32(myRow_CekTingkat["tingkat"]) > 0)
                        {
                            int Tingkat = Convert.ToInt32(myRow_CekTingkat["tingkat"]) + 1;
                            ObjGlobal.Param.Add("tingkat", Tingkat.ToString());
                        }
                    }
                    else
                    {
                        ObjGlobal.Param.Add("tingkat", "1");
                    }

                    ObjGlobal.Param.Add("pos", cboPosition.Text);
                    ObjGlobal.Param.Add("group", hdnKelompok.Value);
                    ObjGlobal.Param.Add("sts", cboType.Text);
                    ObjGlobal.Param.Add("kelompok", cboGroup.Text.Replace(" ", ""));
                    ObjGlobal.Param.Add("nocabang", cboCabang.Text);
                    ObjGlobal.Param.Add("userEntryRek", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("jenis", cboJnsTipe.Text.Replace(" ", ""));
                    ObjGlobal.Param.Add("moneter", cboRekMon.Text);

                    ObjGlobal.ExecuteProcedure("SPInsertRekening", ObjGlobal.Param);
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

        protected void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGroup.SelectedItem.ToString() == "ASSET" || cboGroup.SelectedItem.ToString() == "LIABILITAS dan ASSET NETO")
            {
                hdnKelompok.Value = "PosisiKeuangan";
            }
            if (cboGroup.SelectedItem.ToString() == "COA BOS") 
            {
                hdnKelompok.Value = "Danabos";
            }
            if (cboGroup.SelectedItem.ToString() == "PERUBAHAN ASSET NETTO TIDAK TERIKAT" || cboGroup.SelectedItem.ToString() == "PERUBAHAN ASSET NETTO TERIKAT TEMPORER" || cboGroup.SelectedItem.ToString() == "PERUBAHAN ASSET NETTO TERIKAT PERMANEN")
            {
                hdnKelompok.Value = "Aktivitas";
            }

            if (cboGroup.SelectedItem.ToString() == "AKTIVA" || cboGroup.SelectedItem.ToString() == "KEWAJIBAN Dan EKUITAS")
            {
                hdnKelompok.Value = "Neraca";
            }
            if (cboGroup.SelectedItem.ToString() == "BIAYA OPERASI" || cboGroup.SelectedItem.ToString() == "BIAYA PAJAK" || cboGroup.SelectedItem.ToString() == "BIAYA PENYUSUTAN" || cboGroup.SelectedItem.ToString() == "BIAYA UMUM DAN ADMINISTRASI" || cboGroup.SelectedItem.ToString() == "HARGA POKOK PENJUALAN" || cboGroup.SelectedItem.ToString() == "PENDAPATAN" || cboGroup.SelectedItem.ToString() == "PENDAPATAN DAN BIAYA LAIN")
            {
                hdnKelompok.Value = "RugiLaba";
            }
            CloseMessage();
        }

        protected void imgButtonProduct_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataParent();
            dlgParentAccount.Show();
        }

        protected void grdRekening_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
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

                hdnID.Value = Id;
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

        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataParent();
            dlgParentAccount.Show();
        }

        protected void btnResetRek_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}