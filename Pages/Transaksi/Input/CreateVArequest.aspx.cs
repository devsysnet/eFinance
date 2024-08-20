using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Http;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class CreateVArequest : System.Web.UI.Page
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
            }
            else
            {
                cboKelas.DataSource = ObjDb.GetRows("select '-Pilih Kelas-' as id, '-Pilih Kelas-' as name union all select distinct kelas as id, kelas as name from TransKelas where nocabang = '" + PerwakilanUnit + "' order by name");
                cboKelas.DataValueField = "id";
                cboKelas.DataTextField = "name";
                cboKelas.DataBind();
            }

        }

        protected void loadDataFirst()
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Kelas", cboKelas.SelectedValue);
            ObjGlobal.Param.Add("Cabang", cboPerwakilanUnit.Text);
            ObjGlobal.Param.Add("jenisva", cbojenis.Text);
            ObjGlobal.Param.Add("Nama", txtNamaSiswa.Text);
            grdARSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDatagenerateVA", ObjGlobal.Param);
            grdARSiswa.DataBind();
            if (grdARSiswa.Rows.Count > 0)
                button.Visible = true;
            else
            {
                button.Visible = false;
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

        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "";


            int count = 0;
            for (int i = 0; i < grdARSiswa.Rows.Count; i++)
            {
                CheckBox chkCheck = (CheckBox)grdARSiswa.Rows[i].FindControl("chkCheck");

                if (chkCheck.Checked == true)
                    count++;

            }


            if (count == 0)
            {
                message += ObjSys.CreateMessage("Data harus dipilih.");
                valid = false;
            }



            if (valid == true && count != 0)
            {
                try
                {
                    List<string> paymentTransactionIds = new List<string>();

                     string cek = "", StrKode = "";
                    for (int i = 0; i < grdARSiswa.Rows.Count; i++)
                    {
                        HiddenField hdnNoPiut = (HiddenField)grdARSiswa.Rows[i].FindControl("hdnNoPiut");
                        HiddenField hdnNoSiswa = (HiddenField)grdARSiswa.Rows[i].FindControl("hdnNoSiswa");
                        CheckBox chkCheck = (CheckBox)grdARSiswa.Rows[i].FindControl("chkCheck");


                        string itemId = grdARSiswa.DataKeys[i].Value.ToString();
                        if (chkCheck.Checked == true)
                        {
                            paymentTransactionIds.Add(hdnNoPiut.Value);
                            //cekData++;
                            //StrKode += itemId.ToString() + ",";
                            //cek += hdnNoPiut.Value + ",";

                        }
                    }

                    //ObjGlobal.Param.Clear();
                    //ObjGlobal.Param.Add("kodeTransaksi", StrKode);
                    //ObjGlobal.GetDataProcedure("SPUpdatePaymenttransaksion", ObjGlobal.Param);

                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://kanisius-api.efinac.co.id/api/paymenttransaction/recreateVA");
                    var content = new StringContent("[" + string.Join(",", paymentTransactionIds) + "]", null, "application/json");
                    
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    var res = response.EnsureSuccessStatusCode();
                    
                    loadDataFirst();

                    if (!res.IsSuccessStatusCode)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Penggabungan data tidak berhasil, silahkan dicoba kembali.");
                    }
                    else
                    {

                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", string.Join(",", paymentTransactionIds));
                    }
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

            CloseMessage();
            loadDataFirst();
            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);

        }

        protected void cboJnsDiskon_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            //loadDataJnsTransaksi(cboPerwakilanUnit.Text, cboJnsDiskon.Text);
        }
    }
}