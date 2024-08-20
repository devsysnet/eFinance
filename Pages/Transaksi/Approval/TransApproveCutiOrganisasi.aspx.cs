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
    public partial class TransApproveCutiOrganisasi : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loaddatacombo();
                loaddata(cboYear.Text, cboCabang.Text);
            }
        }
        protected void loaddatacombo()
        {
            cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPpilihanthn1");
            cboYear.DataValueField = "id";
            cboYear.DataTextField = "name";
            cboYear.DataBind();

            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang in(2,3,4)) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang in (2,3) and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();


            }
            else if (ObjSys.GetstsPusat == "3")
            {
                DataSet mySet1 = ObjDb.GetRows("select parent from mcabang where noCabang = '" + ObjSys.GetCabangId + "' ");
                DataRow myRow1 = mySet1.Tables[0].Rows[0];
                string parent = myRow1["parent"].ToString();
                cboCabang.DataSource = ObjDb.GetRows(
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE   stsCabang in (2,3) and parent = '" + parent + "' ");
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
        protected void loaddata(string tahun = "", string cabang = "")
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            ObjGlobal.Param.Add("noUser", ObjSys.GetUserId);
            ObjGlobal.Param.Add("tahun", tahun);
            grdBudget.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataApprCutiOrganisasi", ObjGlobal.Param);
            grdBudget.DataBind();


         

        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;

                if (valid == true)
                {
                    for (int i = 0; i < grdBudget.Rows.Count; i++)
                    {
                        CheckBox chkCheck = (CheckBox)grdBudget.Rows[i].FindControl("chkCheck");
                        HiddenField hdnNoRek = (HiddenField)grdBudget.Rows[i].FindControl("hdnNoRek");
                        HiddenField hdnnocuti = (HiddenField)grdBudget.Rows[i].FindControl("hdnnocuti");
                        Label txtJanuari = (Label)grdBudget.Rows[i].FindControl("txtJanuari");
                        Label txtFebuari = (Label)grdBudget.Rows[i].FindControl("txtFebuari");
                        Label txtMaret = (Label)grdBudget.Rows[i].FindControl("txtMaret");
                        Label txtApril = (Label)grdBudget.Rows[i].FindControl("txtApril");
                        Label txtApril1 = (Label)grdBudget.Rows[i].FindControl("txtApril1");

                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("nokaryawan", hdnNoRek.Value);
                            ObjDb.Data.Add("nocabang", cboCabang.Text);
                            ObjDb.Data.Add("nocuti", hdnnocuti.Value);
                            ObjDb.Data.Add("tglpengajuan", Convert.ToDateTime(txtJanuari.Text).ToString("yyyy-MM-dd"));
                            ObjDb.Data.Add("tglmulai", Convert.ToDateTime(txtFebuari.Text).ToString("yyyy-MM-dd"));
                            ObjDb.Data.Add("tglselesai", Convert.ToDateTime(txtMaret.Text).ToString("yyyy-MM-dd"));
                            ObjDb.Data.Add("uraian", txtApril.Text);
                            ObjDb.Data.Add("totalcuti", Convert.ToDecimal(txtApril1.Text).ToString());
                            ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                            ObjDb.Insert("tApproveCuti", ObjDb.Data);

                            ObjDb.Data.Clear();
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("nocuti", hdnnocuti.Value);                        
                            ObjDb.Data.Add("sts", "1");
                            ObjDb.Data.Add("modifiedby", ObjSys.GetUserId);
                            ObjDb.Data.Add("modifieddate", ObjSys.GetNow);
                            ObjDb.Update("TransCuti", ObjDb.Data, ObjDb.Where);

                        }
                    }

                   

                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        loaddata(cboYear.Text,cboCabang.Text);
                        ShowMessage("success", "Data berhasil di approve.");
                    

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
        protected void btnReject_Click(object sender, EventArgs e)
        {
            dlgReject.Show();

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnBatal_Click(object sender, EventArgs e)
        {
            dlgReject.Hide();
        }

        protected void btnRejectData_Click(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;

                if (valid == true)
                {
                    for (int i = 0; i < grdBudget.Rows.Count; i++)
                    {
                        CheckBox chkCheck = (CheckBox)grdBudget.Rows[i].FindControl("chkCheck");
                        HiddenField hdnNoRek = (HiddenField)grdBudget.Rows[i].FindControl("hdnNoRek");
                        HiddenField hdnnocuti = (HiddenField)grdBudget.Rows[i].FindControl("hdnnocuti");
                        Label txtJanuari = (Label)grdBudget.Rows[i].FindControl("txtJanuari");
                        Label txtFebuari = (Label)grdBudget.Rows[i].FindControl("txtFebuari");
                        Label txtMaret = (Label)grdBudget.Rows[i].FindControl("txtMaret");
                        Label txtApril = (Label)grdBudget.Rows[i].FindControl("txtApril");
                        Label txtApril1 = (Label)grdBudget.Rows[i].FindControl("txtApril1");

                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("nokaryawan", hdnNoRek.Value);
                            ObjDb.Data.Add("nocabang", cboCabang.Text);
                            ObjDb.Data.Add("nocuti", hdnnocuti.Value);
                            ObjDb.Data.Add("tglpengajuan", Convert.ToDateTime(txtJanuari.Text).ToString("yyyy-MM-dd"));
                            ObjDb.Data.Add("tglmulai", Convert.ToDateTime(txtFebuari.Text).ToString("yyyy-MM-dd"));
                            ObjDb.Data.Add("tglselesai", Convert.ToDateTime(txtMaret.Text).ToString("yyyy-MM-dd"));
                            ObjDb.Data.Add("uraian", txtApril.Text);
                            ObjDb.Data.Add("totalcuti", Convert.ToDecimal(txtApril1.Text).ToString());
                            ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                            ObjDb.Insert("tRejectCuti", ObjDb.Data);

                            ObjDb.Data.Clear();
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("nocuti", hdnnocuti.Value);
                            ObjDb.Delete("transcuti", ObjDb.Where);

                        }
                    }



                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    loaddata(cboYear.Text,cboCabang.Text);
                    ShowMessage("success", "Data berhasil direject");

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

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loaddata(cboYear.Text, cboCabang.Text);
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loaddata(cboYear.Text, cboCabang.Text);
        }
    }
}