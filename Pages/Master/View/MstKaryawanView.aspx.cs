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


namespace eFinance.Pages.Master.View
{
    public partial class MstKaryawanView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();
        protected string execBind = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadIndexYayasan();
                loadDataCombo();
                LoadData(cboPerwakilan.Text, cboUnit.Text, txtSearch.Text);
            }
        }

        protected void LoadData(string perwakilan = "0", string unit = "0", string search = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("perwakilan", perwakilan);
            ObjGlobal.Param.Add("unit", unit);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("Search", search);
            grdKaryawan.DataSource = ObjGlobal.GetDataProcedure("SPreportDatakaryawn", ObjGlobal.Param);
            grdKaryawan.DataBind();

        }

        protected void LoadIndexYayasan()
        {
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
            DataRow myRow = mySet.Tables[0].Rows[0];
            hdnnoYysn.Value = myRow["noCabang"].ToString();
        }
        protected void loadDataCombo()
        {

            //if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            //{
            //    cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Perwakilan' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and stspusat=0 and stscabang=1) a");
            //    cboPerwakilan.DataValueField = "id";
            //    cboPerwakilan.DataTextField = "name";
            //    cboPerwakilan.DataBind();
            //}
            //else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            //{
            //    cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and noCabang=" + ObjSys.GetCabangId + " and stspusat=0 and stscabang=1) a");
            //    cboPerwakilan.DataValueField = "id";
            //    cboPerwakilan.DataTextField = "name";
            //    cboPerwakilan.DataBind();
            //}
            //else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            //{
            //    cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where noCabang=" + ObjSys.GetParentCabang + " and stspusat=0 and stscabang=1) a");
            //    cboPerwakilan.DataValueField = "id";
            //    cboPerwakilan.DataTextField = "name";
            //    cboPerwakilan.DataBind();
            //}

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("yayasan", hdnnoYysn.Value);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", ObjSys.GetParentCabang);
            cboPerwakilan.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataWilayah", ObjGlobal.Param);
            cboPerwakilan.DataValueField = "id";
            cboPerwakilan.DataTextField = "name";
            cboPerwakilan.DataBind();

            LoadDataCombo2();
        }

        protected void LoadDataCombo2()
        {
            //if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            //{
            //    cboUnit.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + ObjSys.GetParentCabang + " and nocabang=" + ObjSys.GetCabangId + " and stspusat=0 and stscabang=2) a");
            //    cboUnit.DataValueField = "id";
            //    cboUnit.DataTextField = "name";
            //    cboUnit.DataBind();
            //}
            //else
            //{
            //    cboUnit.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Unit' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + cboPerwakilan.Text + " and stspusat=0 and stscabang=2) a");
            //    cboUnit.DataValueField = "id";
            //    cboUnit.DataTextField = "name";
            //    cboUnit.DataBind();
            //}

            if (ObjSys.GetstsPusat == "3")
            {
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
                ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
                ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
                ObjGlobal.Param.Add("perwakilan", cboPerwakilan.Text);
                cboUnit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitPerwakilan", ObjGlobal.Param);
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();
            }
            else
            {
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
                ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
                ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
                ObjGlobal.Param.Add("perwakilan", cboPerwakilan.Text);
                cboUnit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnit", ObjGlobal.Param);
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();
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

        protected void LoadDataCombo()
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

            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang
            //pusat
          
                cboCabangInput.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang) a ");
                cboCabangInput.DataValueField = "id";
                cboCabangInput.DataTextField = "name";
                cboCabangInput.DataBind();
            

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

        protected void grdKaryawan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdKaryawan.PageIndex = e.NewPageIndex;
            LoadData(cboPerwakilan.Text, cboUnit.Text, txtSearch.Text);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            LoadData(cboPerwakilan.Text, cboUnit.Text, txtSearch.Text);
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

                DataSet MySet = ObjDb.GetRows("select a.*,case when a.golongandarah='' then '-' else a.golongandarah end as golongandarahx, " +
                    "isnull(a.tglKepesertaansehat,getDate()) as tglKepesertaansehatx, isnull(a.tglKepesertaankerja,getDate()) as tglKepesertaankerjax, " +
                    "isnull(a.tmtKWI,getDate()) as tmtKWIx, isnull(a.tglDiangkat,getDate()) as tglDiangkatx, isnull(a.tgllahir,getDate()) as tgllahirx, " +
                    "isnull(a.tglmasuk,getDate()) as tglmasukx, isnull(a.statusPeg,0) as statusPegx, isnull(a.tanggungan,0) as tanggunganx, " +
                    "isnull(a.jabatan,0) as jabatanx,a.tglTetapTMT,a.tglCapegTMT from  mstKaryawan a where a.noKaryawan = '" + noKaryawan + "'");


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
                    statuspegawai.Text = myRow["statusPegx"].ToString();
                    cboJabatan.Text = myRow["jabatanx"].ToString();
                    cboCabangInput.Text = myRow["noCabang"].ToString();
                    cboAktif.Text = myRow["sts"].ToString();
                    cboAkta.Text= myRow["akta4"].ToString();

                    SetInitialRowPendidikan(noKaryawan);
                    SetInitialRowDocument(noKaryawan);
                    SetInitialRowContact(noKaryawan);
                    SetInitialRowkeluarga(noKaryawan);
                    SetInitialRowIuran(noKaryawan);

                    grdContact.Enabled = false;
                    grdPendidikan.Enabled = false;
                    GridKeluarga.Enabled = false;
                    GridPengalamnkerja.Enabled = false;
                    grdIuran.Enabled = false;

                    LoadDataCombo();

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

        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataCombo2();
            LoadData(cboPerwakilan.Text, cboUnit.Text, txtSearch.Text);
        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(cboPerwakilan.Text, cboUnit.Text, txtSearch.Text);
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(cboPerwakilan.Text, cboUnit.Text, txtSearch.Text);
        }
    }
}