using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;

namespace eFinance.Pages.Master.Update
{
    public partial class MstKaryawanUpdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                loadDataFirst();
                loadData();
                
            }
        }

        //protek index yang dipakai transaksi
        protected void IndexPakai()
        {
            for (int i = 0; i < grdKaryawan.Rows.Count; i++)
            {
                string itemId = grdKaryawan.DataKeys[i].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdKaryawan.Rows[i].FindControl("chkCheck");
                
                DataSet mySet = ObjDb.GetRows("Select top 1 noKaryawan from MstGaji_H Where noKaryawan = '" + itemId + "'");
                if (mySet.Tables[0].Rows.Count > 0)
                    chkCheck.Visible = false;
                else
                    chkCheck.Visible = true;
            }

        }
        protected void loadDataFirst()
        {
            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang
            //pusat
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
             
                cbocabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang) a order by urutan");
                cbocabang.DataValueField = "id";
                cbocabang.DataTextField = "name";
                cbocabang.DataBind();
            }
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cbocabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang) a ");
                cbocabang.DataValueField = "id";
                cbocabang.DataTextField = "name";
                cbocabang.DataBind();
            }
            //perwakilan
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "3")
            {
               

                cbocabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct a.nocabang id, a.namaCabang name FROM vCabang a inner join mcabang b on a.parent=b.parent WHERE a.stsPusat = 0 and a.stsCabang = 2 and b.nocabang = '" + ObjSys.GetCabangId + "') a ");
                cbocabang.DataValueField = "id";
                cbocabang.DataTextField = "name";
                cbocabang.DataBind();
            }
            //perwakilan
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
               

                cbocabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                       "union " +
                       "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
                cbocabang.DataValueField = "id";
                cbocabang.DataTextField = "name";
                cbocabang.DataBind();
            }
            //unit
            else
            {
              
                cbocabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cbocabang.DataValueField = "id";
                cbocabang.DataTextField = "name";
                cbocabang.DataBind();
            }
        }
        private void SetInitialRowDocument(string noKaryawan = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtnamaperusahaan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtjabatan", typeof(string)));
            dt.Columns.Add(new DataColumn("dtAwal", typeof(string)));
            dt.Columns.Add(new DataColumn("dtAkhir", typeof(string)));
            DataSet mySet = ObjDb.GetRows("select * from TPengalamankaryawan WHERE noKaryawan = '" + noKaryawan + "'");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {

                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtnamaperusahaan"] = myRow["namaPerusahaan"].ToString();
                dr["txtjabatan"] = myRow["jabatan"].ToString();
                dr["dtAwal"] = Convert.ToDateTime(myRow["drtgl"]).ToString("dd-MMM-yyyy");
                dr["dtAkhir"] = Convert.ToDateTime(myRow["sdtgl"]).ToString("dd-MMM-yyyy");

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtnamaperusahaan"] = string.Empty;
                dr["txtjabatan"] = string.Empty;
                dr["dtAwal"] = string.Empty;
                dr["dtAkhir"] = string.Empty;

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTableKerja"] = dt;
            GridPengalamnkerja.DataSource = dt;
            GridPengalamnkerja.DataBind();

            SetPreviousDataDocument();
        }
        private void SetPreviousDataDocument()
        {
            if (ViewState["CurrentTableKerja"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTableKerja"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtnamaperusahaan = (TextBox)GridPengalamnkerja.Rows[i].FindControl("txtnamaperusahaan");
                        TextBox txtjabatan = (TextBox)GridPengalamnkerja.Rows[i].FindControl("txtjabatan");
                        TextBox dtAwal = (TextBox)GridPengalamnkerja.Rows[i].FindControl("dtAwal");
                        TextBox dtAkhir = (TextBox)GridPengalamnkerja.Rows[i].FindControl("dtAkhir");


                        txtnamaperusahaan.Text = dt.Rows[i]["txtnamaperusahaan"].ToString();
                        txtjabatan.Text = dt.Rows[i]["txtjabatan"].ToString();
                        dtAwal.Text = dt.Rows[i]["dtAwal"].ToString();
                        dtAkhir.Text = dt.Rows[i]["dtAkhir"].ToString();

                    }
                }
            }
        }
        private void AddNewRowDocument()
        {
            if (ViewState["CurrentTableKerja"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableKerja"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtnamaperusahaan = (TextBox)GridPengalamnkerja.Rows[i].FindControl("txtnamaperusahaan");
                        TextBox txtjabatan = (TextBox)GridPengalamnkerja.Rows[i].FindControl("txtjabatan");
                        TextBox dtAwal = (TextBox)GridPengalamnkerja.Rows[i].FindControl("dtAwal");
                        TextBox dtAkhir = (TextBox)GridPengalamnkerja.Rows[i].FindControl("dtAkhir");


                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["txtnamaperusahaan"] = txtnamaperusahaan.Text;
                        dtCurrentTable.Rows[i]["txtjabatan"] = txtjabatan.Text;
                        dtCurrentTable.Rows[i]["dtAwal"] = dtAwal.Text;
                        dtCurrentTable.Rows[i]["dtAkhir"] = dtAkhir.Text;

                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTableKerja"] = dtCurrentTable;
                    GridPengalamnkerja.DataSource = dtCurrentTable;
                    GridPengalamnkerja.DataBind();
                }
            }
            SetPreviousDataDocument();
        }

        private void SetInitialRowPendidikan(string noKaryawan = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("cbopendidikan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtnmSekolah", typeof(string)));
            dt.Columns.Add(new DataColumn("dtdrthn", typeof(string)));
            dt.Columns.Add(new DataColumn("dtspthn", typeof(string)));
            dt.Columns.Add(new DataColumn("txtNilai", typeof(string)));
            dt.Columns.Add(new DataColumn("txtJurusan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtThnLulus", typeof(string)));
            DataSet mySet = ObjDb.GetRows("select * from TPendindikanKaryawan WHERE noKaryawan = '" + noKaryawan + "'");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {

                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["cbopendidikan"] = myRow["noPendindikan"].ToString();
                dr["txtnmSekolah"] = myRow["namaSekolah"].ToString();
                dr["dtdrthn"] = Convert.ToDateTime(myRow["drthn"]).ToString("dd-MMM-yyyy");
                dr["dtspthn"] = Convert.ToDateTime(myRow["sdthn"]).ToString("dd-MMM-yyyy");
                dr["txtNilai"] = myRow["nilai"].ToString();
                dr["txtJurusan"] = myRow["Jurusan"].ToString();
                dr["txtThnLulus"] = myRow["thnlulus"].ToString();
                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["cbopendidikan"] = string.Empty;
                dr["txtnmSekolah"] = string.Empty;
                dr["dtdrthn"] = string.Empty;
                dr["dtspthn"] = string.Empty;
                dr["txtNilai"] = string.Empty;
                dr["txtJurusan"] = string.Empty;
                dr["txtThnLulus"] = string.Empty;

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTablePendidikan"] = dt;
            grdPendidikan.DataSource = dt;
            grdPendidikan.DataBind();

            SetPreviousDataPendidikan();
        }
        private void SetPreviousDataPendidikan()
        {
            if (ViewState["CurrentTablePendidikan"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTablePendidikan"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList cbopendidikan = (DropDownList)grdPendidikan.Rows[i].FindControl("cbopendidikan");
                        TextBox txtnmSekolah = (TextBox)grdPendidikan.Rows[i].FindControl("txtnmSekolah");
                        TextBox dtdrthn = (TextBox)grdPendidikan.Rows[i].FindControl("dtdrthn");
                        TextBox dtspthn = (TextBox)grdPendidikan.Rows[i].FindControl("dtspthn");
                        TextBox txtNilai = (TextBox)grdPendidikan.Rows[i].FindControl("txtNilai");
                        TextBox txtJurusan = (TextBox)grdPendidikan.Rows[i].FindControl("txtJurusan");
                        TextBox txtThnLulus = (TextBox)grdPendidikan.Rows[i].FindControl("txtThnLulus");

                        cbopendidikan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noPendindikan id, pendindikan name FROM Mstpendindikan where sts = '1') a");
                        cbopendidikan.DataValueField = "id";
                        cbopendidikan.DataTextField = "name";
                        cbopendidikan.DataBind();

                        cbopendidikan.Text = dt.Rows[i]["cbopendidikan"].ToString();
                        txtnmSekolah.Text = dt.Rows[i]["txtnmSekolah"].ToString();
                        dtdrthn.Text = dt.Rows[i]["dtdrthn"].ToString();
                        dtspthn.Text = dt.Rows[i]["dtspthn"].ToString();
                        txtNilai.Text = dt.Rows[i]["txtNilai"].ToString();
                        txtJurusan.Text = dt.Rows[i]["txtJurusan"].ToString();
                        txtThnLulus.Text = dt.Rows[i]["txtThnLulus"].ToString();
                    }
                }
            }
        }
        private void AddNewRowPendidikan()
        {
            if (ViewState["CurrentTablePendidikan"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTablePendidikan"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList cbopendidikan = (DropDownList)grdPendidikan.Rows[i].FindControl("cbopendidikan");
                        TextBox txtnmSekolah = (TextBox)grdPendidikan.Rows[i].FindControl("txtnmSekolah");
                        TextBox dtdrthn = (TextBox)grdPendidikan.Rows[i].FindControl("dtdrthn");
                        TextBox dtspthn = (TextBox)grdPendidikan.Rows[i].FindControl("dtspthn");
                        TextBox txtNilai = (TextBox)grdPendidikan.Rows[i].FindControl("txtNilai");
                        TextBox txtJurusan = (TextBox)grdPendidikan.Rows[i].FindControl("txtJurusan");
                        TextBox txtThnLulus = (TextBox)grdPendidikan.Rows[i].FindControl("txtThnLulus");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["cbopendidikan"] = cbopendidikan.Text;
                        dtCurrentTable.Rows[i]["txtnmSekolah"] = txtnmSekolah.Text;
                        dtCurrentTable.Rows[i]["dtdrthn"] = dtdrthn.Text;
                        dtCurrentTable.Rows[i]["dtspthn"] = dtspthn.Text;
                        dtCurrentTable.Rows[i]["txtNilai"] = txtNilai.Text;
                        dtCurrentTable.Rows[i]["txtJurusan"] = txtJurusan.Text;
                        dtCurrentTable.Rows[i]["txtThnLulus"] = txtThnLulus.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTablePendidikan"] = dtCurrentTable;
                    grdPendidikan.DataSource = dtCurrentTable;
                    grdPendidikan.DataBind();
                }
            }
            SetPreviousDataPendidikan();
        }

        private void SetInitialRowContact(string noKaryawan = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("cboidentitas", typeof(string)));
            dt.Columns.Add(new DataColumn("txtnoIdentitas", typeof(string)));
            dt.Columns.Add(new DataColumn("dtexpdatekartu", typeof(string)));
            dt.Columns.Add(new DataColumn("dtBerlaku", typeof(string)));
            DataSet mySet = ObjDb.GetRows("select * from TIdentitaskaryawan where noKaryawan = '" + noKaryawan + "'");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["cboidentitas"] = myRow["noidentitas"].ToString();
                dr["txtnoIdentitas"] = myRow["nomor"].ToString();
                dr["dtexpdatekartu"] = Convert.ToDateTime(myRow["akhirberlaku"]).ToString("dd-MMM-yyyy");
                dr["dtBerlaku"] = Convert.ToDateTime(myRow["tglBerlaku"]).ToString("dd-MMM-yyyy");
                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["cboidentitas"] = string.Empty;
                dr["txtnoIdentitas"] = string.Empty;
                dr["dtexpdatekartu"] = string.Empty;
                dr["dtBerlaku"] = string.Empty;
                dt.Rows.Add(dr);
            }
            ViewState["CurrentTableContact"] = dt;
            grdContact.DataSource = dt;
            grdContact.DataBind();

            SetPreviousDataContact();
        }

        private void SetPreviousDataContact()
        {
            if (ViewState["CurrentTableContact"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTableContact"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList cboidentitas = (DropDownList)grdContact.Rows[i].FindControl("cboidentitas");
                        TextBox txtnoIdentitas = (TextBox)grdContact.Rows[i].FindControl("txtnoIdentitas");
                        TextBox dtexpdatekartu = (TextBox)grdContact.Rows[i].FindControl("dtexpdatekartu");
                        TextBox dtBerlaku = (TextBox)grdContact.Rows[i].FindControl("dtBerlaku");

                        cboidentitas.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noIdentitas id, Identitas name FROM mIdentitas where sts = '1') a");
                        cboidentitas.DataValueField = "id";
                        cboidentitas.DataTextField = "name";
                        cboidentitas.DataBind();

                        cboidentitas.SelectedValue = dt.Rows[i]["cboidentitas"].ToString();
                        txtnoIdentitas.Text = dt.Rows[i]["txtnoIdentitas"].ToString();
                        dtexpdatekartu.Text = dt.Rows[i]["dtexpdatekartu"].ToString();
                        dtBerlaku.Text = dt.Rows[i]["dtBerlaku"].ToString();

                    }
                }
            }
        }

        private void AddNewRowContact()
        {
            if (ViewState["CurrentTableContact"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableContact"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList cboidentitas = (DropDownList)grdContact.Rows[i].FindControl("cboidentitas");
                        TextBox txtnoIdentitas = (TextBox)grdContact.Rows[i].FindControl("txtnoIdentitas");
                        TextBox dtexpdatekartu = (TextBox)grdContact.Rows[i].FindControl("dtexpdatekartu");
                        TextBox dtBerlaku = (TextBox)grdContact.Rows[i].FindControl("dtBerlaku");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["cboidentitas"] = cboidentitas.Text;
                        dtCurrentTable.Rows[i]["txtnoIdentitas"] = txtnoIdentitas.Text;
                        dtCurrentTable.Rows[i]["dtexpdatekartu"] = dtexpdatekartu.Text;
                        dtCurrentTable.Rows[i]["dtBerlaku"] = dtBerlaku.Text;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTableContact"] = dtCurrentTable;

                    grdContact.DataSource = dtCurrentTable;
                    grdContact.DataBind();
                }
            }
            SetPreviousDataContact();
        }

        private void AddNewRowkeluarga()
        {
            if (ViewState["CurrentTableKeluarga"] != null)
            {
                DataTable dt2 = (DataTable)ViewState["CurrentTableKeluarga"];
                DataRow dr3 = null;
                if (dt2.Rows.Count > 0)
                {
                    for (int ii = 0; ii < dt2.Rows.Count; ii++)
                    {

                        DropDownList cbokeluarga = (DropDownList)GridKeluarga.Rows[ii].FindControl("cbokeluarga");
                        TextBox txtnamakel = (TextBox)GridKeluarga.Rows[ii].FindControl("txtnamakel");
                        TextBox txttelpkel = (TextBox)GridKeluarga.Rows[ii].FindControl("txttelpkel");
                        DropDownList cbopendindikankel = (DropDownList)GridKeluarga.Rows[ii].FindControl("cbopendindikankel");
                        TextBox dtlahirkel = (TextBox)GridKeluarga.Rows[ii].FindControl("dtlahirkel");

                        dr3 = dt2.NewRow();
                        dt2.Rows[ii]["cbokeluarga"] = cbokeluarga.SelectedValue;
                        dt2.Rows[ii]["txtnamakel"] = txtnamakel.Text;
                        dt2.Rows[ii]["txttelpkel"] = txttelpkel.Text;
                        dt2.Rows[ii]["cbopendindikankel"] = cbopendindikankel.SelectedValue;
                        dt2.Rows[ii]["dtlahirkel"] = dtlahirkel.Text;

                    }
                    dt2.Rows.Add(dr3);
                    ViewState["CurrentTableKeluarga"] = dt2;
                    GridKeluarga.DataSource = dt2;
                    GridKeluarga.DataBind();
                }
            }
            SetPreviousDatakeluarga();
        }

        private void SetPreviousDatakeluarga()
        {
            if (ViewState["CurrentTableKeluarga"] != null)
            {
                DataTable dt2 = (DataTable)ViewState["CurrentTableKeluarga"];
                if (dt2.Rows.Count > 0)
                {
                    for (int ii = 0; ii < dt2.Rows.Count; ii++)
                    {
                        DropDownList cbokeluarga = (DropDownList)GridKeluarga.Rows[ii].FindControl("cbokeluarga");
                        TextBox txtnamakel = (TextBox)GridKeluarga.Rows[ii].FindControl("txtnamakel");
                        TextBox txttelpkel = (TextBox)GridKeluarga.Rows[ii].FindControl("txttelpkel");
                        DropDownList cbopendindikankel = (DropDownList)GridKeluarga.Rows[ii].FindControl("cbopendindikankel");
                        TextBox dtlahirkel = (TextBox)GridKeluarga.Rows[ii].FindControl("dtlahirkel");

                        cbokeluarga.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noDatakeluarga id, data name FROM mDatakeluarga where sts = '1') a");
                        cbokeluarga.DataValueField = "id";
                        cbokeluarga.DataTextField = "name";
                        cbokeluarga.DataBind();

                        cbopendindikankel.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noPendindikan id, pendindikan name FROM Mstpendindikan where sts = '1') a");
                        cbopendindikankel.DataValueField = "id";
                        cbopendindikankel.DataTextField = "name";
                        cbopendindikankel.DataBind();

                        cbokeluarga.SelectedValue = dt2.Rows[ii]["cbokeluarga"].ToString();
                        txtnamakel.Text = dt2.Rows[ii]["txtnamakel"].ToString();
                        txttelpkel.Text = dt2.Rows[ii]["txttelpkel"].ToString();
                        cbopendindikankel.SelectedValue = dt2.Rows[ii]["cbopendindikankel"].ToString();
                        dtlahirkel.Text = dt2.Rows[ii]["dtlahirkel"].ToString();


                    }
                }
            }
        }

        private void SetInitialRowkeluarga(string noKaryawan = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("cbokeluarga", typeof(string)));
            dt.Columns.Add(new DataColumn("txtnamakel", typeof(string)));
            dt.Columns.Add(new DataColumn("txttelpkel", typeof(string)));
            dt.Columns.Add(new DataColumn("cbopendindikankel", typeof(string)));
            dt.Columns.Add(new DataColumn("dtlahirkel", typeof(string)));
            DataSet mySet = ObjDb.GetRows("select * from TDaftarkeluarga where noKaryawan = '" + noKaryawan + "'");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["cbokeluarga"] = myRow["status"].ToString();
                dr["txtnamakel"] = myRow["nama"].ToString();
                dr["txttelpkel"] = myRow["telp"].ToString();
                dr["cbopendindikankel"] = myRow["nopendindikan"].ToString();
                dr["dtlahirkel"] = Convert.ToDateTime(myRow["tgllahir"]).ToString("dd-MMM-yyyy");
                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["cbokeluarga"] = string.Empty;
                dr["txtnamakel"] = string.Empty;
                dr["txttelpkel"] = string.Empty;
                dr["cbopendindikankel"] = string.Empty;
                dr["dtlahirkel"] = string.Empty;

                dt.Rows.Add(dr);
            }

            ViewState["CurrentTableKeluarga"] = dt;
            GridKeluarga.DataSource = dt;
            GridKeluarga.DataBind();

            SetPreviousDatakeluarga();
        }

        private void SetInitialRowIuran(string noKaryawan = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("cboIuran", typeof(string)));
            DataSet mySet = ObjDb.GetRows("select * from mIurankaryawan WHERE noKaryawan = '" + noKaryawan + "'");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {

                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["cboIuran"] = myRow["noBpjs"].ToString();
                
                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["cboIuran"] = string.Empty;
                
                dt.Rows.Add(dr);
            }
            ViewState["CurrentTableIuran"] = dt;
            grdIuran.DataSource = dt;
            grdIuran.DataBind();

            SetPreviousDataIuran();
        }
        private void SetPreviousDataIuran()
        {
            if (ViewState["CurrentTableIuran"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTableIuran"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList cboIuran = (DropDownList)grdIuran.Rows[i].FindControl("cboIuran");

                        cboIuran.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noBPJS id, BPJS name FROM MstBPJS where sts = '1') a");
                        cboIuran.DataValueField = "id";
                        cboIuran.DataTextField = "name";
                        cboIuran.DataBind();

                        cboIuran.SelectedValue = dt.Rows[i]["cboIuran"].ToString();
                        
                    }
                }
            }
        }
        private void AddNewRowIuran()
        {
            if (ViewState["CurrentTableIuran"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableIuran"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList cboIuran = (DropDownList)grdIuran.Rows[i].FindControl("cboIuran");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["cboIuran"] = cboIuran.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTableIuran"] = dtCurrentTable;
                    grdIuran.DataSource = dtCurrentTable;
                    grdIuran.DataBind();
                }
            }
            SetPreviousDataIuran();
        }

        protected void loadData()
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cbocabang.Text);
            ObjGlobal.Param.Add("cari", txtSearch.Text);
            grdKaryawan.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataKaryawanUpdate", ObjGlobal.Param);
            grdKaryawan.DataBind();

            IndexPakai();
            CloseMessage();
        }

        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnMode.Value.ToLower() == "deleteall")
                {
                    /*DELETE ALL SELECTED*/
                    int cek = 0;
                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noKaryawan", itemRow);
                            ObjDb.Delete("mstKaryawan", ObjDb.Where);
                            ObjDb.Delete("TPendindikanKaryawan", ObjDb.Where);
                            ObjDb.Delete("TIdentitaskaryawan", ObjDb.Where);
                            ObjDb.Delete("TDaftarkeluarga", ObjDb.Where);
                            ObjDb.Delete("TPengalamankaryawan", ObjDb.Where);
                            ObjDb.Delete("mIurankaryawan", ObjDb.Where);
                        }
                    }
                    for (int i = 0; i < grdKaryawan.Rows.Count; i++)
                    {
                        string itemId = grdKaryawan.DataKeys[i].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdKaryawan.Rows[i].FindControl("chkCheck");

                        if (chkCheck.Checked == true)
                        {
                            cek++;
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noKaryawan", itemId);
                            ObjDb.Delete("mstKaryawan", ObjDb.Where);
                            ObjDb.Delete("TPendindikanKaryawan", ObjDb.Where);
                            ObjDb.Delete("TIdentitaskaryawan", ObjDb.Where);
                            ObjDb.Delete("TDaftarkeluarga", ObjDb.Where);
                            ObjDb.Delete("TPengalamankaryawan", ObjDb.Where);
                            ObjDb.Delete("mIurankaryawan", ObjDb.Where);
                        }
                    }

                    if (cek > 0)
                    {
                        ShowMessage("success", "Data yang dipilih berhasil dihapus.");
                    }
                    else
                    {
                        ShowMessage("error", "Tidak ada data yang dipilih.");
                    }

                    /*END DELETE ALL SELECTED*/
                    loadData();
                    ShowHideGridAndForm(true, false, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void LoadDataCombo(string cabang = "")
        {

            cboAgama.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noAgama id, Agama name FROM mAgama where sts = '1') a");
            cboAgama.DataValueField = "id";
            cboAgama.DataTextField = "name";
            cboAgama.DataBind();

            cbostatusPTK.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noPTKP id, PTKP name FROM mstPTKP where sts = '1') a");
            cbostatusPTK.DataValueField = "id";
            cbostatusPTK.DataTextField = "name";
            cbostatusPTK.DataBind();

            cboGolPegawai.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noGolongan id, Golongan+' '+ruang name FROM MstGolongan where sts = '1') a");
            cboGolPegawai.DataValueField = "id";
            cboGolPegawai.DataTextField = "name";
            cboGolPegawai.DataBind();

            cboJabatan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct nojabatan id, Jabatan name FROM MstJabatan where sts = '1') a");
            cboJabatan.DataValueField = "id";
            cboJabatan.DataTextField = "name";
            cboJabatan.DataBind();

            cboDepartemen.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noDepartemen id, departemen name FROM Mstdepartemen where sts = '1') a");
            cboDepartemen.DataValueField = "id";
            cboDepartemen.DataTextField = "name";
            cboDepartemen.DataBind();

            cboBank.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noBank id, bankname name FROM mbank where sts = '1') a");
            cboBank.DataValueField = "id";
            cboBank.DataTextField = "name";
            cboBank.DataBind();

            cboStsPegawai.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct nostatuspeg id, statuspegawai name FROM Mmststatuspegawai where sts = '1') a");
            cboStsPegawai.DataValueField = "id";
            cboStsPegawai.DataTextField = "name";
            cboStsPegawai.DataBind();

            statuspegawai.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct nostatuspeg id, statuspegawai name FROM Mstjenispegawai where sts = '1') a");
            statuspegawai.DataValueField = "id";
            statuspegawai.DataTextField = "name";
            statuspegawai.DataBind();

            DataSet mySetHx = ObjDb.GetRows("select * from mcabang where nocabang = '" + cabang + "'");
            DataRow myRowHx = mySetHx.Tables[0].Rows[0];
            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang

            //pusat
            if (myRowHx["stscabang"].ToString() == "0")
            {
                cboCabangInput.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang ) a order by urutan");
                cboCabangInput.DataValueField = "id";
                cboCabangInput.DataTextField = "name";
                cboCabangInput.DataBind();
            }

            //perwakilan
            else if (myRowHx["stscabang"].ToString() == "3")
            {
                cboCabangInput.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE   noCabang = '" + cabang + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE parent = '" + myRowHx["parent"].ToString() + "') a ");
                cboCabangInput.DataValueField = "id";
                cboCabangInput.DataTextField = "name";
                cboCabangInput.DataBind();
            }
            else if (myRowHx["stscabang"].ToString() == "4" || myRowHx["stscabang"].ToString() == "1")
            {
                cboCabangInput.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE  stscabang in(2,3,4)     " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsCabang in(2,3,4) and parent = '" + cabang + "') a ");
                cboCabangInput.DataValueField = "id";
                cboCabangInput.DataTextField = "name";
                cboCabangInput.DataBind();
            }
            //unit
            else
            {
                cboCabangInput.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + cabang + "') a");
                cboCabangInput.DataValueField = "id";
                cboCabangInput.DataTextField = "name";
                cboCabangInput.DataBind();
            }


        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
        }

        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdKaryawan.Rows)
                {
                    string index = grdKaryawan.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdKaryawan.Rows)
            {
                string index = grdKaryawan.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdKaryawan.Rows[gvrow.RowIndex].FindControl("chkCheck");
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

        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataCombo(cbocabang.Text);
            CloseMessage();
            loadData();

        }
        
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            CloseMessage();
            bool valid = true;

            if (txtNama.Text == "")
            {
                message += ObjSys.CreateMessage("Nama tidak boleh kosong.");
                valid = false;
            }
            if (cboAktif.Text == "")
            {
                message += ObjSys.CreateMessage("Status Aktif/Tidak harus dipilih.");
                valid = false;
            }
            for (int i = 0; i < grdContact.Rows.Count; i++)
            {
                DropDownList cboidentitas = (DropDownList)grdContact.Rows[i].FindControl("cboidentitas");
                TextBox txtnoIdentitas = (TextBox)grdContact.Rows[i].FindControl("txtnoIdentitas");

                if (cboidentitas.Text == "1" || cboidentitas.Text == "8")
                {
                    //DataSet mySet = ObjDb.GetRows("select * from MstKaryawan where (nik = '" + txtnoIdentitas.Text + "' or idPeg = '" + txtnoIdentitas.Text + "') and noKaryawan <> '" + hdnId.Value + "'");
                    //if (mySet.Tables[0].Rows.Count > 1)
                    //{
                    //    message += ObjSys.CreateMessage("NIK / ID Pegawai sudah ada.");
                    //    valid = false;
                    //}
                }
            }

            if (valid == true)
            {
                try
                {
                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noKaryawan", hdnId.Value);
                    ObjDb.Data.Add("tglCapegTMT", dtCapeg.Text);
                    ObjDb.Data.Add("nama", txtNama.Text);
                    ObjDb.Data.Add("tempatlahir", txttempatlahir.Text);
                    ObjDb.Data.Add("tgllahir", dtlahir.Text);
                    ObjDb.Data.Add("jeniskelamin", cboStatus.Text);
                    ObjDb.Data.Add("agama", cboAgama.SelectedValue);
                    ObjDb.Data.Add("kewarganegraan", cboKewarganegaraan.Text);
                    ObjDb.Data.Add("golongandarah", cboGolongandarah.SelectedValue);
                    ObjDb.Data.Add("tinggibadan", "0");
                    ObjDb.Data.Add("beratbadan", "0");
                    ObjDb.Data.Add("tanggungan", cbostatusPTK.SelectedValue);
                    ObjDb.Data.Add("perkawinan", cboPerkawinan.SelectedValue);
                    ObjDb.Data.Add("tglmasuk", dtMasuk.Text);
                    ObjDb.Data.Add("jabatan", cboJabatan.SelectedValue);
                    ObjDb.Data.Add("golongan", cboGolPegawai.SelectedValue);
                    ObjDb.Data.Add("dept", cboDepartemen.SelectedValue);
                    ObjDb.Data.Add("alamat", txtalamat.Text);
                    ObjDb.Data.Add("telp", txttelp.Text);
                    ObjDb.Data.Add("hp", txthp.Text);
                    ObjDb.Data.Add("email", txtEmail.Text);
                    ObjDb.Data.Add("bank", cboBank.SelectedValue);
                    ObjDb.Data.Add("norek", txtnorek.Text);
                    ObjDb.Data.Add("namarek", txnamaRekening.Text);
                    ObjDb.Data.Add("noSK", TextnoSK.Text);
                    ObjDb.Data.Add("noBpjsKesehatan", txtnoBPJSSehat.Text);
                    ObjDb.Data.Add("tglKepesertaansehat", dtBPJSSehat.Text);
                    ObjDb.Data.Add("noBpjsTenagaKerja", txtnoBPJSKerja.Text);
                    ObjDb.Data.Add("tglKepesertaankerja", dtnoBPJSKerja.Text);
                    ObjDb.Data.Add("noKWI", txtnoKWI.Text);
                    ObjDb.Data.Add("tglTetapTMT", dtAngkat.Text);
                    ObjDb.Data.Add("tmtKWI", dtTMTKWI.Text);
                    ObjDb.Data.Add("jenispegawai", cboStsPegawai.Text);
                    //if (cboStsPegawai.Text == "1")
                    //    ObjDb.Data.Add("status", "1");
                    //else
                    //    ObjDb.Data.Add("status", "0");

                    ObjDb.Data.Add("sts", cboAktif.Text);
                    //ObjDb.Data.Add("stsgaji", "0");
                    ObjDb.Data.Add("akta4", cboAkta.Text);
                    ObjDb.Data.Add("pph21", pph21.Text);
                    ObjDb.Data.Add("noCabang", cboCabangInput.Text); 
                    ObjDb.Data.Add("modiBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modidate", ObjSys.GetNow);
                    ObjDb.Update("MstKaryawan", ObjDb.Data, ObjDb.Where);

                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noKaryawan", hdnId.Value);
                    ObjDb.Delete("TPendindikanKaryawan", ObjDb.Where);
                    ObjDb.Delete("TIdentitaskaryawan", ObjDb.Where);
                    ObjDb.Delete("TDaftarkeluarga", ObjDb.Where);
                    ObjDb.Delete("TPengalamankaryawan", ObjDb.Where);
                    ObjDb.Delete("mIurankaryawan", ObjDb.Where);

                    for (int i = 0; i < grdPendidikan.Rows.Count; i++)
                    {
                        DropDownList cbopendidikan = (DropDownList)grdPendidikan.Rows[i].FindControl("cbopendidikan");
                        TextBox txtnmSekolah = (TextBox)grdPendidikan.Rows[i].FindControl("txtnmSekolah");
                        TextBox dtdrthn = (TextBox)grdPendidikan.Rows[i].FindControl("dtdrthn");
                        TextBox dtspthn = (TextBox)grdPendidikan.Rows[i].FindControl("dtspthn");
                        TextBox txtNilai = (TextBox)grdPendidikan.Rows[i].FindControl("txtNilai");
                        TextBox txtJurusan = (TextBox)grdPendidikan.Rows[i].FindControl("txtJurusan");
                        TextBox txtThnLulus = (TextBox)grdPendidikan.Rows[i].FindControl("txtThnLulus");
                        if (cbopendidikan.Text != "-")
                        {

                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noKaryawan", hdnId.Value);
                            ObjDb.Data.Add("noPendindikan", cbopendidikan.Text);
                            ObjDb.Data.Add("namasekolah", txtnmSekolah.Text);
                            ObjDb.Data.Add("drthn", dtdrthn.Text);
                            ObjDb.Data.Add("sdthn", dtspthn.Text);
                            ObjDb.Data.Add("nilai", txtNilai.Text);
                            ObjDb.Data.Add("Jurusan", txtJurusan.Text);
                            ObjDb.Data.Add("thnlulus", txtThnLulus.Text);
                            ObjDb.Insert("TPendindikanKaryawan", ObjDb.Data);

                        }
                    }



                    for (int i = 0; i < grdContact.Rows.Count; i++)
                    {
                        DropDownList cboidentitas = (DropDownList)grdContact.Rows[i].FindControl("cboidentitas");
                        TextBox txtnoIdentitas = (TextBox)grdContact.Rows[i].FindControl("txtnoIdentitas");
                        TextBox dtexpdatekartu = (TextBox)grdContact.Rows[i].FindControl("dtexpdatekartu");
                        TextBox dtBerlaku = (TextBox)grdContact.Rows[i].FindControl("dtBerlaku");

                        if (cboidentitas.Text != "-")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noKaryawan", hdnId.Value);
                            ObjDb.Data.Add("noIdentitas", cboidentitas.SelectedValue);
                            ObjDb.Data.Add("nomor", txtnoIdentitas.Text);
                            ObjDb.Data.Add("akhirberlaku", dtexpdatekartu.Text);
                            ObjDb.Data.Add("tglBerlaku", dtBerlaku.Text);
                            ObjDb.Insert("TIdentitaskaryawan", ObjDb.Data);

                            if (cboidentitas.Text == "1")
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Where.Clear();
                                ObjDb.Where.Add("noKaryawan", hdnId.Value);
                                ObjDb.Data.Add("nik", txtnoIdentitas.Text);
                                ObjDb.Update("MstKaryawan", ObjDb.Data, ObjDb.Where);

                                ObjDb.Where.Clear();
                                ObjDb.Data.Clear();
                                ObjDb.Where.Add("noKaryawan", hdnId.Value);
                                ObjDb.Data.Add("noIdentitas", cboidentitas.SelectedValue);
                                ObjDb.Data.Add("nomor", txtnoIdentitas.Text);
                                ObjDb.Data.Add("akhirberlaku", dtexpdatekartu.Text);
                                ObjDb.Data.Add("tglBerlaku", dtBerlaku.Text);
                                ObjDb.Update("TIdentitaskaryawan", ObjDb.Data, ObjDb.Where);
                            }

                            if (cboidentitas.Text == "8")
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Where.Clear();
                                ObjDb.Where.Add("noKaryawan", hdnId.Value);
                                ObjDb.Data.Add("idPeg", txtnoIdentitas.Text);
                                ObjDb.Update("MstKaryawan", ObjDb.Data, ObjDb.Where);

                            }
                        }
                    }


                    for (int ii = 0; ii < GridKeluarga.Rows.Count; ii++)
                    {
                        DropDownList cbokeluarga = (DropDownList)GridKeluarga.Rows[ii].FindControl("cbokeluarga");
                        TextBox txtnamakel = (TextBox)GridKeluarga.Rows[ii].FindControl("txtnamakel");
                        TextBox txttelpkel = (TextBox)GridKeluarga.Rows[ii].FindControl("txttelpkel");
                        DropDownList cbopendindikankel = (DropDownList)GridKeluarga.Rows[ii].FindControl("cbopendindikankel");
                        TextBox dtlahirkel = (TextBox)GridKeluarga.Rows[ii].FindControl("dtlahirkel");


                        if (cbokeluarga.SelectedValue != "-")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noKaryawan", hdnId.Value);
                            ObjDb.Data.Add("status", cbokeluarga.SelectedValue);
                            ObjDb.Data.Add("nama", txtnamakel.Text);
                            ObjDb.Data.Add("telp", txttelpkel.Text);
                            ObjDb.Data.Add("nopendindikan", cbopendindikankel.SelectedValue);
                            ObjDb.Data.Add("tgllahir", dtlahirkel.Text);
                            ObjDb.Insert("TDaftarkeluarga", ObjDb.Data);
                        }
                    }


                    for (int i = 0; i < GridPengalamnkerja.Rows.Count; i++)
                    {
                        TextBox txtnamaperusahaan = (TextBox)GridPengalamnkerja.Rows[i].FindControl("txtnamaperusahaan");
                        TextBox txtjabatan = (TextBox)GridPengalamnkerja.Rows[i].FindControl("txtjabatan");
                        TextBox dtAwal = (TextBox)GridPengalamnkerja.Rows[i].FindControl("dtAwal");
                        TextBox dtAkhir = (TextBox)GridPengalamnkerja.Rows[i].FindControl("dtAkhir");


                        if (txtnamaperusahaan.Text != "")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("nokaryawan", hdnId.Value);
                            ObjDb.Data.Add("namaPerusahaan", txtnamaperusahaan.Text);
                            ObjDb.Data.Add("jabatan", txtjabatan.Text);
                            ObjDb.Data.Add("drtgl", dtAwal.Text);
                            ObjDb.Data.Add("sdtgl", dtAwal.Text);
                            ObjDb.Insert("TPengalamankaryawan", ObjDb.Data);
                        }
                    }

                    for (int i = 0; i < grdIuran.Rows.Count; i++)
                    {
                        DropDownList cboIuran = (DropDownList)grdIuran.Rows[i].FindControl("cboIuran");
                        
                        if (cboIuran.Text != "0")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noKaryawan", hdnId.Value);
                            ObjDb.Data.Add("noBpjs", cboIuran.SelectedValue);
                            ObjDb.Insert("mIurankaryawan", ObjDb.Data);
                        }
                    }

                    ShowHideGridAndForm(true, false, false);
                    loadData();
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

        protected void grdKaryawan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdKaryawan.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ShowHideGridAndForm(true, false, false);
            CloseMessage();
        }

        protected void grdKaryawan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdKaryawan.SelectedRow.RowIndex;
                string noKaryawan = grdKaryawan.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noKaryawan;

                DataSet MySet = ObjDb.GetRows("select a.*,case when a.golongandarah='' then '-' else a.golongandarah end as golongandarahx, "+
                    "isnull(a.tglKepesertaansehat,getDate()) as tglKepesertaansehatx, isnull(a.tglKepesertaankerja,getDate()) as tglKepesertaankerjax, " +
                    "isnull(a.tmtKWI,getDate()) as tmtKWIx, isnull(a.tglDiangkat,getDate()) as tglDiangkatx, isnull(a.tgllahir,getDate()) as tgllahirx, " +
                    "isnull(a.tglmasuk,getDate()) as tglmasukx, isnull(a.jenispegawai,0) as statusPegx, isnull(a.tanggungan,0) as tanggunganx, "+
                    "isnull(a.jabatan,0) as jabatanx,a.tglCapegTMT,a.tglTetapTMT from  mstKaryawan a where a.noKaryawan = '" + noKaryawan + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow myRow = MySet.Tables[0].Rows[0];

                    txtNama.Text = myRow["nama"].ToString();
                    txtNUPTK.Text = myRow["NUPTK"].ToString();
                    hdnnoKaryawan.Value = myRow["noKaryawan"].ToString();
                    txttempatlahir.Text = myRow["tempatlahir"].ToString();
                    dtlahir.Text = Convert.ToDateTime(myRow["tgllahirx"]).ToString("dd-MMM-yyyy");
                    cboStatus.Text = myRow["jeniskelamin"].ToString();
                    cboAgama.Text = myRow["agama"].ToString();
                    cboKewarganegaraan.Text = myRow["kewarganegraan"].ToString();
                    cboGolongandarah.Text = myRow["golongandarahx"].ToString();
                    txttinggibadan.Text = Convert.ToDecimal(myRow["tinggibadan"]).ToString();
                    txtberatbadan.Text = Convert.ToDecimal(myRow["beratbadan"]).ToString();
                    cbostatusPTK.Text = myRow["tanggunganx"].ToString();
                    cboPerkawinan.Text = myRow["perkawinan"].ToString();
                    dtMasuk.Text = Convert.ToDateTime(myRow["tglmasukx"]).ToString("dd-MMM-yyyy");
                    cboGolPegawai.Text = myRow["golongan"].ToString();
                    cboDepartemen.Text = myRow["dept"].ToString();
                    txtalamat.Text = myRow["alamat"].ToString();
                    txttelp.Text = myRow["telp"].ToString();
                    txthp.Text = myRow["hp"].ToString();
                    txtEmail.Text = myRow["email"].ToString();
                    cboBank.Text = myRow["bank"].ToString();
                    txtnorek.Text = myRow["norek"].ToString();
                    txnamaRekening.Text = myRow["namarek"].ToString();
                    txtnoBPJSSehat.Text = myRow["noBpjsKesehatan"].ToString();
                    dtBPJSSehat.Text = Convert.ToDateTime(myRow["tglKepesertaansehatx"]).ToString("dd-MMM-yyyy");
                    txtnoBPJSKerja.Text = myRow["noBpjsTenagaKerja"].ToString();
                    dtnoBPJSKerja.Text = Convert.ToDateTime(myRow["tglKepesertaankerjax"]).ToString("dd-MMM-yyyy");
                    txtnoKWI.Text = myRow["noKWI"].ToString();
                    dtAngkat.Text = Convert.ToDateTime(myRow["tglTetapTMT"]).ToString("dd-MMM-yyyy");
                    dtCapeg.Text = Convert.ToDateTime(myRow["tglCapegTMT"]).ToString("dd-MMM-yyyy");
                    dtTMTKWI.Text = Convert.ToDateTime(myRow["tmtKWIx"]).ToString("dd-MMM-yyyy");
                    TextnoSK.Text = myRow["noSK"].ToString();
                    cboStsPegawai.Text = myRow["statusPegx"].ToString();
                    //statuspegawai.Text = myRow["statusPegx"].ToString();
                    cboJabatan.Text = myRow["jabatanx"].ToString();
                    cboCabangInput.Text = myRow["noCabang"].ToString();
                    cboAktif.Text = myRow["sts"].ToString();

                    SetInitialRowPendidikan(noKaryawan);
                    SetInitialRowDocument(noKaryawan);
                    SetInitialRowContact(noKaryawan);
                    SetInitialRowkeluarga(noKaryawan);
                    SetInitialRowIuran(noKaryawan);

                    LoadDataCombo(cbocabang.Text);

                    ShowHideGridAndForm(false, true, false);
                    CloseMessage();
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

        protected void btnAddContact_Click(object sender, EventArgs e)
        {
            AddNewRowContact();
        }

        protected void btnPendidikan_Click(object sender, EventArgs e)
        {
            AddNewRowPendidikan();
        }

        protected void btnPengalamnkerja_Click(object sender, EventArgs e)
        {
            AddNewRowDocument();
        }

        protected void btnAddkeluarga_Click(object sender, EventArgs e)
        {
            AddNewRowkeluarga();
        }

        protected void cboCabangInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void btnIuran_Click(object sender, EventArgs e)
        {
            AddNewRowIuran();
        }
    }
}