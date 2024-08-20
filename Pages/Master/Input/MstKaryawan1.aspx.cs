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

namespace eFinance.Pages.Master.Input
{
    public partial class MstKaryawan1 : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstKaryawan1.aspx");
                cboStsPegawai.Text = "4";
                SetInitialRowPendidikan();
                for (int i = 1; i <= 4; i++)
                {
                    AddNewRowPendidikan();

                }
                LoadDataCombo();


                SetInitialRowContact();
                for (int i = 1; i <= 4; i++)
                {
                    AddNewRowContact();

                }

                SetInitialRowDocument();
                for (int i = 1; i <= 4; i++)
                {
                    AddNewRowDocument();

                }

                SetInitialRowkeluarga();
                for (int i = 1; i <= 4; i++)
                {
                    AddNewRowkeluarga();

                }

                SetInitialRowIuran();
                for (int i = 1; i <= 4; i++)
                {
                    AddNewRowIuran();

                }
            }
        }

        private void SetInitialRowDocument()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtnamaperusahaan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtjabatan", typeof(string)));
            dt.Columns.Add(new DataColumn("dtAwal", typeof(string)));
            dt.Columns.Add(new DataColumn("dtAkhir", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["txtnamaperusahaan"] = string.Empty;
            dr["txtjabatan"] = string.Empty;
            dr["dtAwal"] = string.Empty;
            dr["dtAkhir"] = string.Empty;

            dt.Rows.Add(dr);
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



        private void SetInitialRowkeluarga()
        {
            DataTable dt2 = new DataTable();
            DataRow dr3 = null;
            dt2.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt2.Columns.Add(new DataColumn("cbokeluarga", typeof(string)));
            dt2.Columns.Add(new DataColumn("txtnamakel", typeof(string)));
            dt2.Columns.Add(new DataColumn("txttelpkel", typeof(string)));
            dt2.Columns.Add(new DataColumn("cbopendindikankel", typeof(string)));
            dt2.Columns.Add(new DataColumn("dtlahirkel", typeof(string)));
            dr3 = dt2.NewRow();
            dr3["RowNumber"] = 1;
            dr3["cbokeluarga"] = string.Empty;
            dr3["txtnamakel"] = string.Empty;
            dr3["txttelpkel"] = string.Empty;
            dr3["cbopendindikankel"] = string.Empty;
            dr3["dtlahirkel"] = string.Empty;

            dt2.Rows.Add(dr3);
            ViewState["CurrentTableKeluarga"] = dt2;
            GridKeluarga.DataSource = dt2;
            GridKeluarga.DataBind();

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




        private void SetInitialRowPendidikan()
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

                        cbopendidikan.SelectedValue = dt.Rows[i]["cbopendidikan"].ToString();
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
                        dtCurrentTable.Rows[i]["cbopendidikan"] = cbopendidikan.SelectedValue;
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




        private void SetInitialRowContact()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("cboidentitas", typeof(string)));
            dt.Columns.Add(new DataColumn("txtnoIdentitas", typeof(string)));
            dt.Columns.Add(new DataColumn("dtexpdatekartu", typeof(string)));
            dt.Columns.Add(new DataColumn("dtBerlaku", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["cboidentitas"] = string.Empty;
            dr["txtnoIdentitas"] = string.Empty;
            dr["dtexpdatekartu"] = string.Empty;
            dr["dtBerlaku"] = string.Empty;

            dt.Rows.Add(dr);
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

        //mulai iuran

        private void SetInitialRowIuran()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("cboIuran", typeof(string)));


            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["cboIuran"] = string.Empty;
            dt.Rows.Add(dr);
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

            cbostatuspegawai.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct nostatuspeg id, statuspegawai name FROM Mstjenispegawai where sts = '1') a");
            cbostatuspegawai.DataValueField = "id";
            cbostatuspegawai.DataTextField = "name";
            cbostatuspegawai.DataBind();

            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang
            //pusat
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang in(2,3,4) a order by urutan");
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
            //perwakilan
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE  stscabang in(2,3,4) and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsCabang in(2,3,4) and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //unit
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
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
        private void clearData()
        {
            txtNUPTK.Text = "";
            txtNama.Text = "";
            txttempatlahir.Text = "";
            dtlahir.Text = "";
            dtAngkat.Text = "";
            dtcapegTMT.Text = "";
            cboStatus.Text = "-";
            cboKewarganegaraan.Text = "-";
            cboPerkawinan.Text = "-";
            TextnoSK.Text = "";
            cboGolongandarah.Text = "-";
            dtMasuk.Text = "";
            txttinggibadan.Text = "";
            txttempatlahir.Text = "";
            txttinggibadan.Text = "";
            txtberatbadan.Text = "";
            txtalamat.Text = "";
            txttelp.Text = "";
            txthp.Text = "";
            txtEmail.Text = "";
            txtnorek.Text = "";
            txnamaRekening.Text = "";
            cboStsPegawai.Text = "5";
            cbostatuspegawai.Text = "0";

            SetInitialRowPendidikan();
            for (int i = 1; i <= 4; i++)
            {
                AddNewRowPendidikan();

            }
            LoadDataCombo();


            SetInitialRowContact();
            for (int i = 1; i <= 4; i++)
            {
                AddNewRowContact();

            }

            SetInitialRowDocument();
            for (int i = 1; i <= 4; i++)
            {
                AddNewRowDocument();

            }

            SetInitialRowkeluarga();
            for (int i = 1; i <= 4; i++)
            {
                AddNewRowkeluarga();

            }

            SetInitialRowIuran();
            for (int i = 1; i <= 4; i++)
            {
                AddNewRowIuran();

            }
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            
            if (txtNama.Text == "")
            {
                message += ObjSys.CreateMessage("Nama tidak boleh kosong.");
                valid = false;
            }

            for (int i = 0; i < grdContact.Rows.Count; i++)
            {
                DropDownList cboidentitas = (DropDownList)grdContact.Rows[i].FindControl("cboidentitas");
                TextBox txtnoIdentitas = (TextBox)grdContact.Rows[i].FindControl("txtnoIdentitas");

                if (cboidentitas.Text == "1" || cboidentitas.Text == "8")
                {
                    DataSet mySet = ObjDb.GetRows("select * from MstKaryawan where nik = '" + txtnoIdentitas.Text + "' or idPeg = '" + txtnoIdentitas.Text + "'");
                    if (mySet.Tables[0].Rows.Count > 1)
                    {
                        message += ObjSys.CreateMessage("NIK / ID Pegawai sudah ada.");
                        valid = false;
                    }
                }
            } 

            if (valid == true)
            {
                try
                {

                    ObjDb.Data.Clear();
                    ObjDb.Data.Add("nama", txtNama.Text);
                    ObjDb.Data.Add("tempatlahir", txttempatlahir.Text);
                    ObjDb.Data.Add("tgllahir", dtlahir.Text);
                    ObjDb.Data.Add("jeniskelamin", cboStatus.Text);
                    ObjDb.Data.Add("agama", cboAgama.SelectedValue);
                    ObjDb.Data.Add("kewarganegraan", cboKewarganegaraan.Text);
                    ObjDb.Data.Add("golongandarah", cboGolongandarah.SelectedValue);
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
                    ObjDb.Data.Add("beratbadan", "0");
                    ObjDb.Data.Add("tinggibadan", "0");
                    ObjDb.Data.Add("bank", cboBank.SelectedValue);
                    ObjDb.Data.Add("norek", txtnorek.Text);
                    ObjDb.Data.Add("namarek", txnamaRekening.Text);
                    ObjDb.Data.Add("noSK", TextnoSK.Text);
                    ObjDb.Data.Add("tglCapegTMT", dtcapegTMT.Text);
                    ObjDb.Data.Add("tglTetapTMT", dtAngkat.Text);
                    ObjDb.Data.Add("statusPeg", cboStsPegawai.Text);
                    //if (cboStsPegawai.Text == "1")
                    //    ObjDb.Data.Add("status", "1");
                    //else
                    //    ObjDb.Data.Add("status", "0");
                    ObjDb.Data.Add("sts", "1");
                    ObjDb.Data.Add("stsgaji", "0");
                    ObjDb.Data.Add("akta4", cboAkta.Text);
                    ObjDb.Data.Add("pph21", ph21.Text);
                    ObjDb.Data.Add("jenispegawai", cbostatuspegawai.Text);
                    ObjDb.Data.Add("nocabang", cboCabang.Text);
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("createdate", ObjSys.GetNow);
                    ObjDb.Insert("MstKaryawan", ObjDb.Data);

                    DataSet mySetH = ObjDb.GetRows("select * from MstKaryawan where nama = '" + txtNama.Text + "'");
                    DataRow myRowH = mySetH.Tables[0].Rows[0];
                    string noKaryawan = myRowH["noKaryawan"].ToString();


                    for (int i = 0; i < grdPendidikan.Rows.Count; i++)
                    {
                        DropDownList cbopendidikan = (DropDownList)grdPendidikan.Rows[i].FindControl("cbopendidikan");
                        TextBox txtnmSekolah = (TextBox)grdPendidikan.Rows[i].FindControl("txtnmSekolah");
                        TextBox dtdrthn = (TextBox)grdPendidikan.Rows[i].FindControl("dtdrthn");
                        TextBox dtspthn = (TextBox)grdPendidikan.Rows[i].FindControl("dtspthn");
                        TextBox txtNilai = (TextBox)grdPendidikan.Rows[i].FindControl("txtNilai");
                        TextBox txtJurusan = (TextBox)grdPendidikan.Rows[i].FindControl("txtJurusan");
                        TextBox txtThnLulus = (TextBox)grdPendidikan.Rows[i].FindControl("txtThnLulus");
                        if (cbopendidikan.Text != "-" && txtnmSekolah.Text != "")
                        {

                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noKaryawan", noKaryawan);
                            ObjDb.Data.Add("noPendindikan", cbopendidikan.SelectedValue);
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
                            ObjDb.Data.Add("noKaryawan", noKaryawan);
                            ObjDb.Data.Add("noIdentitas", cboidentitas.SelectedValue);
                            ObjDb.Data.Add("nomor", txtnoIdentitas.Text);
                            ObjDb.Data.Add("akhirberlaku", dtexpdatekartu.Text);
                            ObjDb.Data.Add("tglBerlaku", dtBerlaku.Text);
                            ObjDb.Insert("TIdentitaskaryawan", ObjDb.Data);

                            if (cboidentitas.Text == "1")
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Where.Clear();
                                ObjDb.Where.Add("noKaryawan", noKaryawan);
                                ObjDb.Data.Add("nik", txtnoIdentitas.Text);
                                ObjDb.Update("MstKaryawan", ObjDb.Data, ObjDb.Where);

                                ObjDb.Where.Clear();
                                ObjDb.Data.Clear();
                                ObjDb.Where.Add("noKaryawan", noKaryawan);
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
                                ObjDb.Where.Add("noKaryawan", noKaryawan);
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


                        if (cbokeluarga.Text != "-" && txtnamakel.Text != "")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noKaryawan", noKaryawan);
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
                            ObjDb.Data.Add("nokaryawan", noKaryawan);
                            ObjDb.Data.Add("namaPerusahaan", txtnamaperusahaan.Text);
                            ObjDb.Data.Add("jabatan", txtjabatan.Text);
                            ObjDb.Data.Add("drtgl", dtAwal.Text);
                            ObjDb.Data.Add("sdtgl", dtAwal.Text);
                            ObjDb.Insert("TPengalamankaryawan", ObjDb.Data);
                        }
                    }


                    //mulai iuran

                    for (int i = 0; i < grdIuran.Rows.Count; i++)
                    {
                        DropDownList cboIuran = (DropDownList)grdIuran.Rows[i].FindControl("cboIuran");


                        if (cboIuran.Text != "0")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noKaryawan", noKaryawan);
                            ObjDb.Data.Add("noBpjs", cboIuran.SelectedValue);
                            ObjDb.Insert("mIurankaryawan", ObjDb.Data);
                        }
                    }

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                    clearData();

                    
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
            clearData();
        }

        protected void btnAddContact_Click(object sender, EventArgs e)
        {
            AddNewRowContact();
        }

        protected void btnAddkeluarga_Click(object sender, EventArgs e)
        {
            AddNewRowkeluarga();
        }

        protected void btnPengalamnkerja_Click(object sender, EventArgs e)
        {
            AddNewRowDocument();
        }

        protected void btnPendidikan_Click(object sender, EventArgs e)
        {
            AddNewRowPendidikan();
        }

        protected void btnAddIuran_Click(object sender, EventArgs e)
        {
            AddNewRowIuran();
        }
    }
}