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
    public partial class TransApprovecuti : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loaddatacombo();
                //loaddata(cboYear.Text, cboCabang.Text);
            }
        }
        protected void loaddatacombo()
        {
            //cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPpilihanthn1");
            //cboYear.DataValueField = "id";
            //cboYear.DataTextField = "name";
            //cboYear.DataBind();

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
            ObjGlobal.Param.Add("noParameter", "12");
            grdBudget.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataApproveCuti", ObjGlobal.Param);
            grdBudget.DataBind();
 
 
            if (grdBudget.Rows.Count > 0)
                showhidebutton.Visible = true;
            else
                showhidebutton.Visible = false;

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
                                HiddenField hdnNoRek = (HiddenField)grdBudget.Rows[i].FindControl("hdnNoRek");
                                HiddenField hdnnocuti = (HiddenField)grdBudget.Rows[i].FindControl("hdnnocuti");
                                CheckBox chkCheck = (CheckBox)grdBudget.Rows[i].FindControl("chkCheck");
                                    if (chkCheck.Checked == true)
                                    {
                                        ObjGlobal.Param.Clear();
                                        ObjGlobal.Param.Add("nouser", ObjSys.GetUserId);
                                        ObjGlobal.Param.Add("nocuti", hdnnocuti.Value);
                                        ObjGlobal.Param.Add("nokaryawan", hdnNoRek.Value);
                                        ObjGlobal.Param.Add("noCabang", cboCabang.Text);
                                        ObjGlobal.GetDataProcedure("SPUpdateTcuti", ObjGlobal.Param);
                                    }

                                }



                            //}

                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    //loaddata(cboYear.Text, cboCabang.Text);
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
                    string rjke = "0";
                    DataSet mySet = ObjDb.GetRows("select isnull(count(*),0) + 1 as rjke from tBudget_Jenjangreject where noCabang = '" + cboCabang.Text + "'");
                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        DataRow myRow = mySet.Tables[0].Rows[0];
                        rjke = myRow["rjke"].ToString();
                    }

                    ObjDb.Data.Clear();
                    //ObjDb.Data.Add("tahun", cboYear.Text);
                    ObjDb.Data.Add("nouser", ObjSys.GetUserId);
                    ObjDb.Data.Add("apprKe", rjke);
                    ObjDb.Data.Add("stsAppr", "1");
                    ObjDb.Data.Add("noCabang", cboCabang.Text);
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                    ObjDb.Insert("tCuti_jenjangReject", ObjDb.Data);

                    string sql = "delete tCuti_jenjangReject where nocabang = '" + cboCabang.Text + "'";
                    ObjDb.ExecQuery(sql);

                    //DataSet mySet4 = ObjDb.GetRows("select systembudget from parameter ");
                    //DataRow myRow4 = mySet4.Tables[0].Rows[0];
                    //string systembudget = myRow4["systembudget"].ToString();

                    
                    //    for (int i = 0; i < grdBudget.Rows.Count; i++)
                    //    {
                    //        HiddenField hdnNoRek = (HiddenField)grdBudget.Rows[i].FindControl("hdnNoRek");
                    //        ObjDb.Data.Clear();
                    //        ObjDb.Where.Clear();
                    //        ObjDb.Where.Add("noRek", hdnNoRek.Value);
                    //        //ObjDb.Where.Add("thn", cboYear.Text);
                    //        ObjDb.Where.Add("nocabang", cboCabang.Text);
                    //        ObjDb.Data.Add("stsApprv", "2");
                    //        ObjDb.Data.Add("keteranganreject", txtCatatanReject.Text);
                    //        ObjDb.Update("tBudget_H ", ObjDb.Data, ObjDb.Where);
                    //    }
                    



                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    loaddata();
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
            //loaddata(cboYear.Text, cboCabang.Text);
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            //loaddata(cboYear.Text, cboCabang.Text);
        }
    }
}