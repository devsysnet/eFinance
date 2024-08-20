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
    public partial class trantagihanupdate1 : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataComboFirst();
                loadDataCombo();
                ShowHideGridAndForm(true, true);
                button.Visible = false;
            }
        }

        private void loadDataComboFirst()
        {
            if (ObjSys.GetstsPusat == "3")
            {
                cboPerwakilanUnit.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE stsCabang = 2 and parent = '" + ObjSys.GetParentCabang + "') a order by a.noUrut");
                cboPerwakilanUnit.DataValueField = "id";
                cboPerwakilanUnit.DataTextField = "name";
                cboPerwakilanUnit.DataBind();
            }
            else if (ObjSys.GetstsPusat == "2")
            {
                cboPerwakilanUnit.DataSource = ObjDb.GetRows("SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsCabang = 2 and noCabang = '" + ObjSys.GetCabangId + "'");
                cboPerwakilanUnit.DataValueField = "id";
                cboPerwakilanUnit.DataTextField = "name";
                cboPerwakilanUnit.DataBind();
            }
            else
            {
                cboPerwakilanUnit.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE stsCabang = 2) a order by a.noUrut");
                cboPerwakilanUnit.DataValueField = "id";
                cboPerwakilanUnit.DataTextField = "name";
                cboPerwakilanUnit.DataBind();
            }
             loadDataKelas(cboPerwakilanUnit.Text);
            //loadDataJnsTransaksi(cboPerwakilanUnit.Text, cboJnsDiskon.Text);
        }

        protected void cboPerwakilanUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataKelas(cboPerwakilanUnit.Text);
            //loadDataJnsTransaksi(cboPerwakilanUnit.Text, cboJnsDiskon.Text);
        }

       
        protected void loadDataKelas(string PerwakilanUnit = "")
        {
            if (PerwakilanUnit == "0")
            {
                cboKelas.DataSource = ObjDb.GetRows("select * from (select '0' as id, '-Pilih Kelas-' as name)x");
                cboKelas.DataValueField = "id";
                cboKelas.DataTextField = "name";
                cboKelas.DataBind();

                cboTahun.DataSource = ObjDb.GetRows("select * from (select '0' as id, '-Pilih Tahun-' as name)x");
                cboTahun.DataValueField = "id";
                cboTahun.DataTextField = "name";
                cboTahun.DataBind();
            }
            else
            {
                cboKelas.DataSource = ObjDb.GetRows("select '-Pilih Kelas-' as id, '-Pilih Kelas-' as name union all select distinct kelas as id, kelas as name from TransKelas where nocabang = '" + PerwakilanUnit + "' order by name");
                cboKelas.DataValueField = "id";
                cboKelas.DataTextField = "name";
                cboKelas.DataBind();

                cboTahun.DataSource = ObjDb.GetRows("select * from (select '0' as id, '-Pilih Tahun-' as name union select distinct year(tgl) as id, CONVERT(varchar,year(tgl)) as name from TransPiutang where nocabang = '" + PerwakilanUnit + "')x");
                cboTahun.DataValueField = "id";
                cboTahun.DataTextField = "name";
                cboTahun.DataBind();
            }

        }


        private void loadDataCombo()
        {
            cboBulan.DataSource = ObjDb.GetRows("select * from (select '0' as id, '-Pilih Bulan-' as name union select distinct month(tgl) as id, DATENAME(mm, tgl) as name from TransPiutang)x");
            cboBulan.DataValueField = "id";
            cboBulan.DataTextField = "name";
            cboBulan.DataBind();
        }

        protected void loadDataFirst()
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Bulan", cboBulan.SelectedValue);
            ObjGlobal.Param.Add("Tahun", cboTahun.SelectedValue);
            ObjGlobal.Param.Add("Kelas", cboKelas.SelectedValue);
            ObjGlobal.Param.Add("Cabang", cboPerwakilanUnit.Text);
            ObjGlobal.Param.Add("Nama", txtNamaSiswa.Text);
            grdARSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPelunasanAR1update", ObjGlobal.Param);
            grdARSiswa.DataBind();
            if (grdARSiswa.Rows.Count > 0)
                button.Visible = true;
            else
            {
                button.Visible = false;
                //txtTotal.Text = "";
            }

        }

        protected void grdARSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdARSiswa.PageIndex = e.NewPageIndex;
            //loadDataFirst();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            //loadDataFirst();
            ShowHideGridAndForm(true, true);
            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate()", "Calculate();", true);

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

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            int count = 0, selisih = 0;
            for (int i = 0; i < grdARSiswa.Rows.Count; i++)
            {
                CheckBox chkCheck = (CheckBox)grdARSiswa.Rows[i].FindControl("chkCheck");
                HiddenField hdnSaldo = (HiddenField)grdARSiswa.Rows[i].FindControl("hdnSaldo");
                TextBox txtbayar = (TextBox)grdARSiswa.Rows[i].FindControl("txtbayar");
               if (chkCheck.Checked == true && txtbayar.Text != "")
                {
                    count++;
                }
                //if ((Convert.ToDecimal(txtbayar.Text) + Convert.ToDecimal(txtDiskonD.Text)) > Convert.ToDecimal(hdnSaldo.Value))
                //    selisih++;
            }

           if (count == 0)
            {
                message += ObjSys.CreateMessage("Data harus dipilih.");
                valid = false;
            }
            if (selisih > 0)
            {
                message += ObjSys.CreateMessage("Nilai bayar + diskon harus <= nilai saldo.");
                valid = false;
            }


            if (valid == true && count != 0 && selisih == 0)
            {
                try
                {
                    decimal totalbayar = 0, totaldisc = 0;
                    string cek = "";
                    for (int i = 0; i < grdARSiswa.Rows.Count; i++)
                    {
                        HiddenField hdnNoPiut = (HiddenField)grdARSiswa.Rows[i].FindControl("hdnNoPiut");
                        HiddenField hdnNoSiswa = (HiddenField)grdARSiswa.Rows[i].FindControl("hdnNoSiswa");
                        HiddenField hdnNIK = (HiddenField)grdARSiswa.Rows[i].FindControl("hdnNIK");
                        CheckBox chkCheck = (CheckBox)grdARSiswa.Rows[i].FindControl("chkCheck");
                        TextBox txtbayar = (TextBox)grdARSiswa.Rows[i].FindControl("txtbayar");
                        HiddenField hdnTglJt = (HiddenField)grdARSiswa.Rows[i].FindControl("hdnTglJt");
                        HiddenField hdnThAj = (HiddenField)grdARSiswa.Rows[i].FindControl("hdnThAj");
                        HiddenField hdnnoTrans = (HiddenField)grdARSiswa.Rows[i].FindControl("hdnnoTrans");
                        TextBox txtDiskonD = (TextBox)grdARSiswa.Rows[i].FindControl("txtDiskonD");

                        if (chkCheck.Checked == true)
                        {
                            cek += hdnNoPiut.Value + ",";
                            decimal saldo = 0, sisasaldo = 0, diskon = 0, bayar = 0, nilaibayar = 0;
                            DataSet dataSNx = ObjDb.GetRows("SELECT nilaibayar, saldo, isnull(diskon,0) as diskon FROM TransPiutang WHERE noPiutang = '" + hdnNoPiut.Value + "'");
                            if (dataSNx.Tables[0].Rows.Count > 0)
                            {
                                DataRow myRowSnx = dataSNx.Tables[0].Rows[0];
                                bayar = Convert.ToDecimal(myRowSnx["nilaibayar"]);
                                saldo = Convert.ToDecimal(myRowSnx["saldo"]);
                                diskon = Convert.ToDecimal(myRowSnx["diskon"]);
                            }

                            nilaibayar = (Convert.ToDecimal(txtbayar.Text));
                           
                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("noPiutang", hdnNoPiut.Value);
                            ObjGlobal.Param.Add("nilaibayar", Convert.ToDecimal(nilaibayar).ToString());
                             ObjGlobal.GetDataProcedure("SPUpdatetranspiutang2", ObjGlobal.Param);                         
                            }

                            totalbayar += Convert.ToDecimal(txtbayar.Text);

                        }
          

                    {

                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "alertMessage", "alert('Data berhasil disimpan.'); window.location.reload();", true);
                    }
                    loadDataFirst();




                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.Message);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (cboBulan.Text == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Bulan harus dipilih.");
            }
            else if (cboTahun.Text == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Tahun harus dipilih.");
            }
            loadDataFirst();
        }

        protected void cboJnsDiskon_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            //loadDataJnsTransaksi(cboPerwakilanUnit.Text, cboJnsDiskon.Text);
        }
    }
}