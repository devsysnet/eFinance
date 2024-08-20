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
    public partial class TransPRView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        //buatcetakan
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCombo();
                dtMulai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                dtSampai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                loadData();

            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            grdPRView.DataSource = ObjGlobal.GetDataProcedure("SPViewPR", ObjGlobal.Param);
            grdPRView.DataBind();

        }

        protected void loadCombo()
        {
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang in(2,3,4)) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang where stscabang in(2,3,4)) a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
        }

          protected void loadDataComboKegiatan()
        {
            cboKegiatan.DataSource = ObjDb.GetRows("select '0' as no, '--Pilih Kegiatan--' name union all select noMkegiatan as no, namaKegiatan as name from mJenisKegiatan where sts = '1' and nocabang='" + ObjSys.GetCabangId + "'");
            cboKegiatan.DataValueField = "no";
            cboKegiatan.DataTextField = "name";
            cboKegiatan.DataBind();
        }


        protected void showHideKatDana(bool DivKatdana)
        {
            showKatDana.Visible = DivKatdana;
        }

        #region setInitial & AddRow
        private void SetInitialRow(string id = "", string dari = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKode", typeof(string)));
            dt.Columns.Add(new DataColumn("lblNama", typeof(string)));
            dt.Columns.Add(new DataColumn("txtNilai", typeof(string)));
            dt.Columns.Add(new DataColumn("cboSatuan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtBudgetPR", typeof(string)));
            dt.Columns.Add(new DataColumn("Keterangan", typeof(string)));
            dt.Columns.Add(new DataColumn("pilihan", typeof(string)));
            dt.Columns.Add(new DataColumn("cboDana", typeof(string)));
            dt.Columns.Add(new DataColumn("txtHargaStn", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", id);
            ObjGlobal.Param.Add("dari", dari);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPUpdatePR", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtKode"] = myRow["kode"].ToString();
                dr["lblNama"] = myRow["nama"].ToString();
                dr["txtNilai"] = ObjSys.IsFormatNumber(myRow["qty"].ToString());
                dr["cboSatuan"] = myRow["satuan"].ToString();
                dr["txtBudgetPR"] = ObjSys.IsFormatNumber(myRow["nilai"].ToString());
                dr["Keterangan"] = myRow["keterangan"].ToString();
                dr["pilihan"] = myRow["pilihan"].ToString();
                dr["cboDana"] = myRow["danaBOS"].ToString();
                dr["txtHargaStn"] = ObjSys.IsFormatNumber(myRow["hargaSatuanPR"].ToString());
                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdDetail.DataSource = dt;
            grdDetail.DataBind();

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
                        TextBox txtKode = (TextBox)grdDetail.Rows[i].FindControl("txtKode");
                        Label lblNama = (Label)grdDetail.Rows[i].FindControl("lblNama");
                        TextBox txtNilai = (TextBox)grdDetail.Rows[i].FindControl("txtNilai");
                        DropDownList cboSatuan = (DropDownList)grdDetail.Rows[i].FindControl("cboSatuan");
                        TextBox txtBudgetPR = (TextBox)grdDetail.Rows[i].FindControl("txtBudgetPR");
                        TextBox Keterangan = (TextBox)grdDetail.Rows[i].FindControl("Keterangan");
                        TextBox pilihan = (TextBox)grdDetail.Rows[i].FindControl("pilihan");
                        DropDownList cboDana = (DropDownList)grdDetail.Rows[i].FindControl("cboDana");
                        TextBox txtHargaStn = (TextBox)grdDetail.Rows[i].FindControl("txtHargaStn");

                        txtKode.Text = dt.Rows[i]["txtKode"].ToString();
                        lblNama.Text = dt.Rows[i]["lblNama"].ToString();
                        txtNilai.Text = dt.Rows[i]["txtNilai"].ToString();
                        cboSatuan.Text = dt.Rows[i]["cboSatuan"].ToString();
                        txtBudgetPR.Text = dt.Rows[i]["txtBudgetPR"].ToString();
                        Keterangan.Text = dt.Rows[i]["Keterangan"].ToString();
                        pilihan.Text = dt.Rows[i]["pilihan"].ToString();
                        cboDana.Text = dt.Rows[i]["cboDana"].ToString();
                        txtHargaStn.Text = dt.Rows[i]["txtHargaStn"].ToString();

                        //loadDataCombo(cboSatuan);
                    }
                }
            }
        }

        #endregion

        //private void loadDataCombo(DropDownList cboSatuan)
        //{
        //    cboSatuan.DataSource = ObjDb.GetRows("select '' mesurement union all select mesurement from mMesurement");
        //    cboSatuan.DataValueField = "mesurement";
        //    cboSatuan.DataTextField = "mesurement";
        //    cboSatuan.DataBind();

        //}

        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void grdPRView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPRView.PageIndex = e.NewPageIndex;
            loadData();
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

        protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4" || cboTransaction.SelectedValue == "5")
                {
                    e.Row.Cells[1].Text = "Kode Barang";
                    e.Row.Cells[2].Text = "Nama Barang";
                    e.Row.Cells[3].Text = "Qty";
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[5].Visible = true;
                    e.Row.Cells[6].Visible = true;
                    e.Row.Cells[7].Text = "Spesifikasi";
                }
                else if (cboTransaction.SelectedValue == "2")
                {
                    e.Row.Cells[1].Text = "Akun";
                    e.Row.Cells[2].Text = "Nama Akun";
                    e.Row.Cells[3].Text = "Nominal";
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Text = "Keterangan";
                }
                else if (cboTransaction.SelectedValue == "3")
                {
                    e.Row.Cells[1].Text = "Kode Jasa";
                    e.Row.Cells[2].Text = "Nama Jasa";
                    e.Row.Cells[3].Text = "Biaya";
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Text = "Keterangan";
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4" || cboTransaction.SelectedValue == "5")
                {
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[5].Visible = true;
                    e.Row.Cells[6].Visible = true;
                }
                else if (cboTransaction.SelectedValue == "2")
                {
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                }
                else if (cboTransaction.SelectedValue == "3")
                {
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                }
            }
        }

        protected void grdPRView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Lblsts = (Label)e.Row.FindControl("Lblsts");
                HiddenField hdnstsappr = (HiddenField)e.Row.FindControl("hdnstsappr");
                Button btnClose = (Button)e.Row.FindControl("btnClose");

                if (hdnstsappr.Value == "0")
                {
                    Lblsts.Text = "<span class='label label-warning ticket-label'>" + Lblsts.Text + "</span>";
                    btnClose.Enabled = false;
                }
                else if (hdnstsappr.Value == "1")
                {
                    Lblsts.Text = "<span class='label label-success ticket-label'>" + Lblsts.Text + "</span>";
                    btnClose.Enabled = true;
                }
                else if (hdnstsappr.Value == "2")
                {
                    Lblsts.Text = "<span class='label label-danger ticket-label'>" + Lblsts.Text + "</span>";
                    btnClose.Enabled = false;
                }
                else if (hdnstsappr.Value == "3")
                {
                    Lblsts.Text = "<span class='label label-default ticket-label'>" + Lblsts.Text + "</span>";
                    btnClose.Enabled = false;
                }
                else if (hdnstsappr.Value == "4")
                {
                    Lblsts.Text = "<span class='label label-primary ticket-label'>" + Lblsts.Text + "</span>";
                    btnClose.Enabled = false;
                }
            }
        }

        protected void grdPRView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectDetil")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noPR = grdPRView.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noPR;

                    DataSet mySet = ObjDb.GetRows("select a.tglpakai,a.noPR,a.kodePR,a.tglPR,a.subtotal,a.jenis,a.peminta,a.uraian,isnull(kategoriRutin,'') as kategoriRutin, " +
                     "b.noUser, isnull(a.noKegiatan,0) as noKegiatan, isnull(a.noProject,0) as noProject, isNULL(a.danaBOS,x.danaBOS) as danaBOS from TransPR_H a left join muser b on a.peminta = b.namaUser " +
                    "left join (select distinct top 1 noPR, danaBOS from TransPR_D where noPR = '" + hdnId.Value + "') x on x.noPR = a.noPR " +
                    "where a.noPR = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtKdPR.Text = myRow["kodePR"].ToString();
                    dtDate.Text = Convert.ToDateTime(myRow["tglPR"]).ToString("dd-MMM-yyyy");
                    dtDate1.Text = Convert.ToDateTime(myRow["tglpakai"]).ToString("dd-MMM-yyyy");
                    cboTransaction.Text = myRow["jenis"].ToString();
                    txtNamaPeminta.Text = myRow["peminta"].ToString();
                    hdnNoUser.Value = myRow["noUser"].ToString();
                    txtKeterangan.Text = myRow["uraian"].ToString();
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

                    cboDanaH.Text = myRow["danaBOS"].ToString();

                    DataSet mySet2 = ObjDb.GetRows("select top 1 reasson from tPR_jenjangReject "+
                        "where notPR = '" + hdnId.Value + "' order by noPRjenjangR desc");
                    if (mySet2.Tables[0].Rows.Count > 0)
                    {
                        DataRow myRow2 = mySet2.Tables[0].Rows[0];
                        txtReject.Text = myRow2["reasson"].ToString();
                        ketreject.Visible = true;
                    }
                    else
                        ketreject.Visible = false;

                    SetInitialRow(hdnId.Value, cboTransaction.Text);

                    grdDetail.Enabled = false;
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
                    showHideFormKas(false, true);
                }
                else if (e.CommandName == "SelectClose")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noPR = grdPRView.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noPR;

                    DataSet mySetH2 = ObjDb.GetRows("Select * from TransPR_D Where noPR = '" + noPR + "' and noPO is null");
                    if (mySetH2.Tables[0].Rows.Count > 0)
                    {
                        //DataRow myRowH2 = mySetH2.Tables[0].Rows[0];
                        ObjDb.Where.Clear();
                        ObjDb.Data.Clear();
                        ObjDb.Where.Add("noPR", hdnId.Value);
                        ObjDb.Data.Add("stsApv", "3");
                        ObjDb.Update("transPR_H", ObjDb.Data, ObjDb.Where);

                        loadData();
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil diclose");
                        this.showHideFormKas(true, false);
                    }
                    else
                    {

                        DataSet mySet = ObjDb.GetRows("select a.noPR,a.kodePR,a.tglPR,a.jenis,a.peminta,a.uraian,isnull(a.kategoriRutin,'') as kategoriRutin, " +
                           "isnull(subTotal,0) as subTotal,b.noUser, isnull(a.noProject,0) as noProject, isNULL(a.danaBOS,x.danaBOS) as danaBOS from TransPR_H a left join muser b on a.peminta = b.namaUser " +
                           "left join (select distinct top 1 noPR, danaBOS from TransPR_D where noPR = '" + hdnId.Value + "') x on x.noPR = a.noPR " +
                            "where a.noPR = '" + hdnId.Value + "'");
                        DataRow myRow = mySet.Tables[0].Rows[0];

                        txtKdPR.Text = myRow["kodePR"].ToString();
                        dtDate.Text = Convert.ToDateTime(myRow["tglPR"]).ToString("dd-MMM-yyyy");
                        cboTransaction.Text = myRow["jenis"].ToString();
                        txtNamaPeminta.Text = myRow["peminta"].ToString();
                        hdnNoUser.Value = myRow["noUser"].ToString();
                        txtKeterangan.Text = myRow["uraian"].ToString();
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
                            loadDataComboProject();
                        }
                        else
                            showHideKegiatan(false);

                        txtSubTotal.Text = ObjSys.IsFormatNumber(myRow["subTotal"].ToString());

                        SetInitialRow(hdnId.Value, cboTransaction.Text);

                        grdDetail.Enabled = false;
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
      
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Data permintaan sudah di pembelian");
                        this.showHideFormKas(false, true);
                    }
                    
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void showHideProject(bool DivProject)
        {
            showProject.Visible = DivProject;
        }

        protected void showHideKegiatan(bool DivProject)
        {
            showKegiatan.Visible = DivProject;
        }

        protected void loadDataComboProject()
        {
            cboProject.DataSource = ObjDb.GetRows("select '0' as no, '--Pilih Project--' name union all select noproject as no, noKontrak+' - '+Project as name from mProject where stsProject = '1' ");
            cboProject.DataValueField = "no";
            cboProject.DataTextField = "name";
            cboProject.DataBind();
        }
    }
}