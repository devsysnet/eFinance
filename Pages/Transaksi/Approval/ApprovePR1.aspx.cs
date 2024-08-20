using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Approval
{
    public partial class ApprovePR1 : System.Web.UI.Page
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
            ObjGlobal.Param.Add("noUser", ObjSys.GetUserId);
            ObjGlobal.Param.Add("noParameter", "1");
            grdPRView.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataApprovePR", ObjGlobal.Param);
            grdPRView.DataBind();
        }

        #region setInitial & AddRow
        private void SetInitialRow(string Id = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnnoPR", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnnoPRD", typeof(string)));
            dt.Columns.Add(new DataColumn("txtkodebrg", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("lblnamaBarang", typeof(string)));
            dt.Columns.Add(new DataColumn("txtSisaBgt", typeof(string)));
            dt.Columns.Add(new DataColumn("txtNilaiBesar", typeof(string)));
            dt.Columns.Add(new DataColumn("lblsatuanBesar", typeof(string)));
            dt.Columns.Add(new DataColumn("txtQty", typeof(string)));
            dt.Columns.Add(new DataColumn("txtsatuan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKeterangan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtNilaiD", typeof(string)));
            dt.Columns.Add(new DataColumn("cboselect", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPViewPRAppD", ObjGlobal.Param);
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnnoPR"] = myRow["noPR"].ToString();
                dr["hdnnoPRD"] = myRow["noPRD"].ToString();
                dr["txtkodebrg"] = myRow["kodeBarang"].ToString();
                dr["hdnAccount"] = myRow["noPR"].ToString();
                dr["lblnamaBarang"] = myRow["namaBarang"].ToString();
                dr["txtSisaBgt"] = ObjSys.IsFormatNumber(myRow["sisabgt"].ToString());
                dr["lblsatuanBesar"] = myRow["satbesar"].ToString();
                dr["txtNilaiBesar"] = ObjSys.IsFormatNumber(myRow["qtybesar"].ToString());
                dr["txtsatuan"] = myRow["satuan"].ToString();
                dr["txtQty"] = ObjSys.IsFormatNumber(myRow["qty"].ToString());
                dr["txtKeterangan"] = myRow["keterangan"].ToString();
                dr["txtNilaiD"] = ObjSys.IsFormatNumber(myRow["nilai"].ToString());
                dr["cboselect"] = myRow["pilihan"].ToString();
                dt.Rows.Add(dr);

            }

            ViewState["CurrentTable"] = dt;
            grdPRViewDetil.DataSource = dt;
            grdPRViewDetil.DataBind();

            SetPreviousData();
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
                        HiddenField hdnnoPR = (HiddenField)grdPRViewDetil.Rows[i].FindControl("hdnnoPR");
                        HiddenField hdnnoPRD = (HiddenField)grdPRViewDetil.Rows[i].FindControl("hdnnoPRD");
                        HiddenField hdnAccount = (HiddenField)grdPRViewDetil.Rows[i].FindControl("hdnAccount");
                        TextBox txtkodebrg = (TextBox)grdPRViewDetil.Rows[i].FindControl("txtkodebrg");
                        TextBox txtNilaiBesar = (TextBox)grdPRViewDetil.Rows[i].FindControl("txtNilaiBesar");
                        Label lblsatuanBesar = (Label)grdPRViewDetil.Rows[i].FindControl("lblsatuanBesar");
                        TextBox txtQty = (TextBox)grdPRViewDetil.Rows[i].FindControl("txtQty");
                        Label lblnamaBarang = (Label)grdPRViewDetil.Rows[i].FindControl("lblnamaBarang");
                        TextBox txtsatuan = (TextBox)grdPRViewDetil.Rows[i].FindControl("txtsatuan");
                        TextBox txtKeterangan = (TextBox)grdPRViewDetil.Rows[i].FindControl("txtKeterangan");
                        TextBox txtNilaiD = (TextBox)grdPRViewDetil.Rows[i].FindControl("txtNilaiD");
                        TextBox txtSisaBgt = (TextBox)grdPRViewDetil.Rows[i].FindControl("txtSisaBgt");
                        DropDownList cboselect = (DropDownList)grdPRViewDetil.Rows[i].FindControl("cboselect");

                        hdnnoPRD.Value = dt.Rows[i]["hdnnoPRD"].ToString();
                        txtkodebrg.Text = dt.Rows[i]["txtkodebrg"].ToString();
                        hdnAccount.Value = dt.Rows[i]["hdnAccount"].ToString();
                        lblnamaBarang.Text = dt.Rows[i]["lblnamaBarang"].ToString();
                        txtNilaiBesar.Text = dt.Rows[i]["txtNilaiBesar"].ToString();
                        lblsatuanBesar.Text = dt.Rows[i]["lblsatuanBesar"].ToString();
                        txtQty.Text = dt.Rows[i]["txtQty"].ToString();
                        txtsatuan.Text = dt.Rows[i]["txtsatuan"].ToString();
                        txtKeterangan.Text = dt.Rows[i]["txtKeterangan"].ToString();
                        txtNilaiD.Text = dt.Rows[i]["txtNilaiD"].ToString();
                        txtSisaBgt.Text = dt.Rows[i]["txtSisaBgt"].ToString();
                        cboselect.Text = dt.Rows[i]["cboselect"].ToString();
                    }
                }
            }
        }
        #endregion

        protected void grdPRView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            loadData();
            grdPRView.PageIndex = e.NewPageIndex;
        }

        protected void grdPRView_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            try
            {
                int rowIndex = grdPRView.SelectedRow.RowIndex;
                string id = grdPRView.DataKeys[rowIndex].Values[0].ToString();
                HiddenField hdnlvlApprove = (HiddenField)grdPRView.Rows[rowIndex].FindControl("hdnlvlApprove");

                hdnId.Value = id;
                DataSet mySet = ObjDb.GetRows("select a.tglpakai,a.nocabang,a.noPR,a.kodePR,a.tglPR,a.jenis,a.peminta,a.subtotal,a.uraian,isnull(kategoriRutin,'') as kategoriRutin, " +
                     "b.noUser, isnull(a.noKegiatan,0) as noKegiatan, isnull(a.noProject,0) as noProject, isNULL(a.danaBOS,x.danaBOS) as danaBOS from TransPR_H a left join muser b on a.peminta = b.namaUser " +
                    "left join (select distinct top 1 noPR, danaBOS from TransPR_D where noPR = '" + hdnId.Value + "') x on x.noPR = a.noPR " +
                    "where a.noPR = '" + hdnId.Value + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];

                cboTransaction.Text = myRow["jenis"].ToString();
                hdnJns.Value = myRow["jenis"].ToString();
                txtkodePR.Text = myRow["kodePR"].ToString();
                txtpeminta.Text = myRow["peminta"].ToString();
                dtKas.Text = Convert.ToDateTime(myRow["tglPR"]).ToString("dd-MMM-yyyy");
                dtKas1.Text = Convert.ToDateTime(myRow["tglpakai"]).ToString("dd-MMM-yyyy");
                txturaian.Text = myRow["uraian"].ToString();
                if (cboTransaction.Text == "2")
                {
                    showHideKatDana(true);
                    cboKategoriDana.Text = myRow["kategoriRutin"].ToString();
                }
                else
                    showHideKatDana(false);

                if (cboTransaction.Text == "2" && cboKategoriDana.Text == "Project")
                {
                    showHideProject(true);
                    cboProject.Text = myRow["noProject"].ToString();
                    loadDataComboProject();
                }
                else
                    showHideProject(false);

                if (cboTransaction.Text == "2" && cboKategoriDana.Text == "Kegiatan")
                {
                    showHideKegiatan(true);
                    cboKegiatan.Text = myRow["noKegiatan"].ToString();
                    loadDataComboKegiatan();
                }
                else
                    showHideKegiatan(false);

                txtSubTotal.Text = ObjSys.IsFormatNumber(myRow["subTotal"].ToString());
                hdnlvlApproveH.Value = hdnlvlApprove.Value;
                hdnnoCabangPRH.Value = myRow["noCabang"].ToString();

                for (int i = 0; i < grdPRViewDetil.Rows.Count; i++)
                {
                    if (hdnJns.Value == "1" || hdnJns.Value == "4" || hdnJns.Value == "5")
                    {
                        grdPRViewDetil.Columns[4].Visible = true;
                        grdPRViewDetil.Columns[5].Visible = true;
                        grdPRViewDetil.Columns[7].Visible = true;
                    }
                    else if (hdnJns.Value == "2")
                    {
                        grdPRViewDetil.Columns[4].Visible = false;
                        grdPRViewDetil.Columns[5].Visible = false; 
                        grdPRViewDetil.Columns[7].Visible = false;
                    }
                    else
                    {
                        grdPRViewDetil.Columns[4].Visible = false;
                        grdPRViewDetil.Columns[5].Visible = false; 
                        grdPRViewDetil.Columns[7].Visible = false;
                    }

                }

                //string param = "";
                //if (cboTransaction.Text == "1")
                //    param = "1";
                //else
                //    param = "3";

                //btnSimpan.Enabled = true;
                //btnReject.Enabled = true;
                //DataSet mySetH = ObjDb.GetRows("SELECT * FROM MstApproveNilai WHERE noParameterApprove = '" + param + "' and noCabang = '" + hdnnoCabangPRH.Value + "' ");
                //if (mySetH.Tables[0].Rows.Count == 0)
                //{
                //    btnSimpan.Enabled = false;
                //    btnReject.Enabled = false;
                //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //    ShowMessage("error", "Jenis Permintaan belum di set Master Approval Harga");
                //}

                SetInitialRow(hdnId.Value);
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
                showHideFormKas(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void showHideKatDana(bool DivKatdana)
        {
            showKatDana.Visible = DivKatdana;
        }

        protected void showHideProject(bool DivProject)
        {
            showProject.Visible = DivProject;
        }

        protected void showHideKegiatan(bool DivKegiatan)
        {
            showKegiatan.Visible = DivKegiatan;
        }

        protected void loadDataComboProject()
        {
            cboProject.DataSource = ObjDb.GetRows("select '0' as no, '--Pilih Project--' name union all select noproject as no, noKontrak+' - '+Project as name from mProject where stsProject = '1' ");
            cboProject.DataValueField = "no";
            cboProject.DataTextField = "name";
            cboProject.DataBind();
        }

        protected void loadDataComboKegiatan()
        {
            cboKegiatan.DataSource = ObjDb.GetRows("select '0' as no, '--Pilih Kegiatan--' name union all select noMkegiatan as no, namaKegiatan as name from mJenisKegiatan where sts = '1'");
            cboKegiatan.DataValueField = "no";
            cboKegiatan.DataTextField = "name";
            cboKegiatan.DataBind();
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

            int cekData = 0;
            for (int i = 0; i < grdPRViewDetil.Rows.Count; i++)
            {
                TextBox txtSisaBgt = (TextBox)grdPRViewDetil.Rows[i].FindControl("txtSisaBgt");
                TextBox txtNilaiD = (TextBox)grdPRViewDetil.Rows[i].FindControl("txtNilaiD");
                Label lblnamaBarang = (Label)grdPRViewDetil.Rows[i].FindControl("lblnamaBarang");
                DropDownList cboselect = (DropDownList)grdPRViewDetil.Rows[i].FindControl("cboselect");

                string nilai = "0";
                if (txtNilaiD.Text != "0.00")
                    nilai = Convert.ToDecimal(txtNilaiD.Text).ToString();

                if (nilai == "0")
                {
                    message = ObjSys.CreateMessage("Nilai Input untuk barang " + lblnamaBarang.Text + " harus > 0.");
                    alert = "error";
                    valid = false;
                }

                if (cboselect.Text != "")
                {
                    cekData++;
                }

            }

            if (cekData == 0)
            {
                message = ObjSys.CreateMessage("Pembuat PO harus dipilih.");
                alert = "error";
                valid = false;
            }

            try
            {
                if (valid == true)
                {
                    for (int i = 0; i < grdPRViewDetil.Rows.Count; i++)
                    {
                        HiddenField hdnnoPRD = (HiddenField)grdPRViewDetil.Rows[i].FindControl("hdnnoPRD");
                        TextBox txtNilaiD = (TextBox)grdPRViewDetil.Rows[i].FindControl("txtNilaiD");
                        DropDownList cboselect = (DropDownList)grdPRViewDetil.Rows[i].FindControl("cboselect");

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noPRD", hdnnoPRD.Value);
                        ObjGlobal.Param.Add("budget", Convert.ToDecimal(txtNilaiD.Text).ToString());
                        ObjGlobal.Param.Add("pilihan", cboselect.Text);
                        ObjGlobal.GetDataProcedure("SPUpdatePRD", ObjGlobal.Param);

                        string param = "";
                        if (cboTransaction.Text == "1")
                            param = "1";
                        else
                            param = "3";

                        string levelAppv = "";

                        DataSet mySet = ObjDb.GetRows("select TOP 1 b.lvlUser from TransPR_H a " +
                              "inner join MstApproveNilai b on b.noParameterApprove = '" + param + "' where " +
                              "b.drNilai <= a.subtotal and a.nocabang = b.nocabang " +
                              "and b.peruntukan in ('Unit','Perwakilan','Yayasan') and a.noPR='" + hdnId.Value + "' ORDER BY b.lvlUser DESC");

                        DataRow myRow = mySet.Tables[0].Rows[0];
                        levelAppv = myRow["lvlUser"].ToString();
                        if (levelAppv == hdnlvlApproveH.Value)
                        {

                            ObjDb.Data.Clear();
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noPR", hdnId.Value);
                            ObjDb.Data.Add("stsD", "1");
                            ObjDb.Update("TransPR_D", ObjDb.Data, ObjDb.Where);

                            ObjDb.Data.Clear();
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noPR", hdnId.Value);
                            ObjDb.Data.Add("stsApv", "1");
                            ObjDb.Update("TransPR_H", ObjDb.Data, ObjDb.Where);
                        }

                    }

                    ObjDb.Data.Clear();
                    ObjDb.Data.Add("notPR", hdnId.Value);
                    ObjDb.Data.Add("nouser", ObjSys.GetUserId);
                    ObjDb.Data.Add("apprKe", hdnlvlApproveH.Value);
                    ObjDb.Data.Add("stsAppr", "1");
                    ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                    ObjDb.Insert("tPR_jenjang", ObjDb.Data);

                    loadData();
                    showHideFormKas(true, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diapprove.");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage(alert, message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            dlgReject.Show();
        }

        protected void btnBatal_Click(object sender, EventArgs e)
        {
            dlgReject.Hide();
            showHideFormKas(false, true);
        }

        protected void btnRejectData_Click(object sender, EventArgs e)
        {
            try
            {
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("Id", hdnId.Value);
                ObjGlobal.Param.Add("noUser", ObjSys.GetUserId);
                ObjGlobal.Param.Add("nocabang", hdnnoCabangPRH.Value);
                ObjGlobal.Param.Add("catatan", txtCatatanReject.Text);
                ObjGlobal.GetDataProcedure("SPVrejectPR", ObjGlobal.Param);
                loadData();
                showHideFormKas(true, false);
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil di reject.");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Invalid transaction data.");
            }
        }

        protected void grdPRViewDetil_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4" || cboTransaction.SelectedValue == "5")
                {
                    e.Row.Cells[1].Text = "Kode Barang";
                    e.Row.Cells[2].Text = "Nama Barang";
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[5].Visible = true;
                    e.Row.Cells[6].Text = "Qty Kecil";
                    e.Row.Cells[7].Visible = true;
                }
                else if (cboTransaction.SelectedValue == "2")
                {
                    e.Row.Cells[1].Text = "Akun";
                    e.Row.Cells[2].Text = "Nama Akun";
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Text = "Nominal";
                    e.Row.Cells[7].Visible = false;
                }
                else if (cboTransaction.SelectedValue == "3")
                {
                    e.Row.Cells[1].Text = "Kode Jasa";
                    e.Row.Cells[2].Text = "Nama Jasa";
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Text = "Biaya";
                    e.Row.Cells[7].Visible = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4" || cboTransaction.SelectedValue == "5")
                {
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[5].Visible = true;
                    e.Row.Cells[7].Visible = true;
                }
                else if (cboTransaction.SelectedValue == "2")
                {
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[7].Visible = false;
                }
                else if (cboTransaction.SelectedValue == "3")
                {
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[7].Visible = false;
                }

                if (ObjDb.GetRows("SELECT * FROM TransPR_D WHERE noPR = '" + hdnId.Value + "' and noPR in (select notPR from tPR_jenjang)").Tables[0].Rows.Count > 0)
                {
                    ((DropDownList)e.Row.Cells[9].FindControl("cboSelect")).Enabled = false;
                }
                else
                {
                    ((DropDownList)e.Row.Cells[9].FindControl("cboSelect")).Enabled = true;
                }

            }
        }

        protected void grdPRView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnDetail = (Button)e.Row.FindControl("btnDetail");
                Label lblAlert = (Label)e.Row.FindControl("lblAlert");

                if (lblAlert.Text == "Available")
                    btnDetail.Enabled = true;
                else
                    btnDetail.Enabled = false;
            }

        }

        protected void cboSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < grdPRViewDetil.Rows.Count; i++)
            {
                DropDownList cboselect = (DropDownList)grdPRViewDetil.Rows[i].FindControl("cboselect");

                if (cboselect.Text != "")
                {
                    for (int j = 0; j < grdPRViewDetil.Rows.Count; j++)
                    {
                        DropDownList cboselectx = (DropDownList)grdPRViewDetil.Rows[j].FindControl("cboselect");

                        cboselectx.Text = cboselect.Text;
                    }
                }
            }
        }

        protected void grdPRViewDetil_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = grdPRViewDetil.SelectedRow.RowIndex;
            TextBox txtkodebrg = (TextBox)grdPRViewDetil.Rows[rowIndex].FindControl("txtkodebrg");

            hdnIdD.Value = txtkodebrg.Text;
            loadDataHistoryPR(hdnIdD.Value);
            dlgHistory.Show();

        }

        protected void loadDataHistoryPR(string kdbarang = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("kdbarang", kdbarang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parent", ObjSys.GetParentCabang); 
            grdHistoryPR.DataSource = ObjGlobal.GetDataProcedure("SPLoadHistoryPR", ObjGlobal.Param);
            grdHistoryPR.DataBind();
        }
    }
}