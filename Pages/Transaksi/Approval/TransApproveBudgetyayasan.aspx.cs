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
    public partial class TransApproveBudgetyayasan : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboYear.DataSource = ObjDb.GetRows("select a.* from (select distinct thn id, thn name from tBudget_H a inner join MstApprove b on a.nocabang=b.noCabang where stsApprv in(0) and  nouser='" + ObjSys.GetUserId + "' and '" + ObjSys.GetUserId + "' not in(select nouser from tBudget_jenjang dd where dd.nocabang=a.nocabang and dd.tahun=a.thn)) a order by name");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();
                loaddatacombo(cboYear.Text);
                showhidebutton.Visible = false;

            }
        }
        protected void loaddatacombo(string tahun = "")
        {


            string lvlappr = "";
            DataSet mySetx = ObjDb.GetRows("select distinct levelApprove from mstApprove where nouser = '" + ObjSys.GetUserId + "' and noParameterApprove = 2 and peruntukan = 'Unit' ");

            DataRow myRowx = mySetx.Tables[0].Rows[0];
            lvlappr = myRowx["levelApprove"].ToString();

            if (lvlappr == "1")
            {

                //pengurusyayasan
                if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
                {
                    cboCabang.DataSource = ObjDb.GetRows("select a.* from (select distinct a.nocabang id, namaCabang name, noUrut as urutan FROM tbudget_H aa inner join MstApprove a on aa.nocabang=a.nocabang join mCabang b on a.noCabang=b.nocabang where aa.stsApprv=0 and nouser='" + ObjSys.GetUserId + "' and '" + ObjSys.GetUserId + "' not in(select nouser from tBudget_jenjang dd where dd.nocabang=aa.nocabang and dd.tahun=aa.thn)) a order by name");
                    cboCabang.DataValueField = "id";
                    cboCabang.DataTextField = "name";
                    cboCabang.DataBind();
                }
                //kepalacabang/perwakilan
                else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
                {
                    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct a.nocabang id, namaCabang name FROM tBudget_H a inner join mCabang b on a.nocabang=b.noCabang WHERE b.stsPusat = 0 and b.stsCabang in(2,3,4) and a.stsApprv=0 and b.parent = '" + ObjSys.GetCabangId + "' and '" + ObjSys.GetUserId + "' not in(select nouser from tBudget_jenjang dd where dd.nocabang=a.nocabang and dd.tahun=a.thn)) a order by name");
                    cboCabang.DataValueField = "id";
                    cboCabang.DataTextField = "name";
                    cboCabang.DataBind();


                }
                else if (ObjSys.GetstsPusat == "3")
                {
                    //DataSet mySet1 = ObjDb.GetRows("select parent from mcabang where noCabang = '" + ObjSys.GetCabangId + "' ");
                    //DataRow myRow1 = mySet1.Tables[0].Rows[0];
                    //string parent = myRow1["parent"].ToString();
                    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct a.nocabang id, namaCabang name FROM tBudget_H a inner join mCabang b on a.nocabang=b.noCabang WHERE b.stsPusat = 0 and b.stsCabang in(2,3,4) and a.stsApprv=0 and b.parent = '" + ObjSys.GetParentCabang + "' and '" + ObjSys.GetUserId + "' not in(select nouser from tBudget_jenjang dd where dd.nocabang=a.nocabang and dd.tahun=a.thn)) a order by name");
                    cboCabang.DataValueField = "id";
                    cboCabang.DataTextField = "name";
                    cboCabang.DataBind();
                }
                else if (ObjSys.GetstsPusat == "2")
                {
                    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct a.nocabang id, namaCabang name FROM tBudget_H a inner join mCabang b on a.nocabang=b.noCabang WHERE b.stsPusat = 0 and b.stsCabang in(2) and a.stsApprv=0 and b.nocabang = '" + ObjSys.GetCabangId + "' and '" + ObjSys.GetUserId + "' not in(select nouser from tBudget_jenjang dd where dd.nocabang=a.nocabang and dd.tahun=a.thn)) a order by name");
                    cboCabang.DataValueField = "id";
                    cboCabang.DataTextField = "name";
                    cboCabang.DataBind();
                }

            }
            else
            {
                //pengurusyayasan
                if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
                {
                    cboCabang.DataSource = ObjDb.GetRows("select a.* from (select distinct a.nocabang id, namaCabang name, noUrut as urutan FROM tbudget_H aa inner join MstApprove a on aa.nocabang=a.nocabang join mCabang b on a.noCabang=b.nocabang where b.nocabang in (select nocabang from tBudget_jenjang where noCabang in (select nocabang from mstApprove where nouser='" + ObjSys.GetUserId + "') and apprKe = 1 and tahun = '" + tahun + "') ) a order by name");
                    cboCabang.DataValueField = "id";
                    cboCabang.DataTextField = "name";
                    cboCabang.DataBind();
                }
                //kepalacabang/perwakilan
                else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
                {
                    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct a.nocabang id, namaCabang name FROM tBudget_H a inner join mCabang b on a.nocabang=b.noCabang WHERE b.nocabang in (select nocabang from tBudget_jenjang where noCabang in (select nocabang from mstApprove where nouser='" + ObjSys.GetUserId + "') and apprKe = 1 and tahun = '" + tahun + "') ) a order by name");
                    cboCabang.DataValueField = "id";
                    cboCabang.DataTextField = "name";
                    cboCabang.DataBind();


                }
                else if (ObjSys.GetstsPusat == "3")
                {
                    //DataSet mySet1 = ObjDb.GetRows("select parent from mcabang where noCabang = '" + ObjSys.GetCa
                    //DataRow myRow1 = mySet1.Tables[0].Rows[0];
                    //string parent = myRow1["parent"].ToString();
                    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct a.nocabang id, namaCabang name FROM tBudget_H a inner join mCabang b on a.nocabang=b.noCabang WHERE b.nocabang in (select nocabang from tBudget_jenjang where noCabang in (select nocabang from mstApprove where nouser='" + ObjSys.GetUserId + "') and apprKe = 1 and tahun = '" + tahun + "') ) a order by name");
                    cboCabang.DataValueField = "id";
                    cboCabang.DataTextField = "name";
                    cboCabang.DataBind();
                }
                else if (ObjSys.GetstsPusat == "2")
                {
                    cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct a.nocabang id, namaCabang name FROM tBudget_H a inner join mCabang b on a.nocabang=b.noCabang WHERE b.nocabang in (select nocabang from tBudget_jenjang where noCabang in (select nocabang from mstApprove where nouser='" + ObjSys.GetUserId + "') and apprKe = 1 and tahun = '" + tahun + "') ) a order by name");
                    cboCabang.DataValueField = "id";
                    cboCabang.DataTextField = "name";
                    cboCabang.DataBind();
                }
            }
        }
        protected void loaddata(string tahun = "", string cabang = "",string jenis = "")
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            ObjGlobal.Param.Add("noUser", ObjSys.GetUserId);
            ObjGlobal.Param.Add("tahun", tahun);
            ObjGlobal.Param.Add("jenis", jenis);
            ObjGlobal.Param.Add("noParameter", "2");
            grdBudget.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataApproveBudgetyayasan", ObjGlobal.Param);
            grdBudget.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            ObjGlobal.Param.Add("noUser", ObjSys.GetUserId);
            ObjGlobal.Param.Add("tahun", tahun);
            ObjGlobal.Param.Add("noParameter", "2");
            ObjGlobal.Param.Add("jenis", jenis);

            GridView1.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataApproveBudgetyayasan", ObjGlobal.Param);
            GridView1.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            ObjGlobal.Param.Add("noUser", ObjSys.GetUserId);
            ObjGlobal.Param.Add("tahun", tahun);
            ObjGlobal.Param.Add("jenis", jenis);

            ObjGlobal.Param.Add("noParameter", "2");
            GridView2.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataApproveBudgetyayasan", ObjGlobal.Param);
            GridView2.DataBind();

            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataApproveBudgetyayasan", ObjGlobal.Param);
            if (mySet.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(mySet.Tables[0].Rows[0]["posisiApprove"]).ToString() == "0")
                {
                    btnSimpan.Text = "Approve Level ke-1 ";
                    btnReject.Text = "Reject Level ke-1";
                }
                else
                {
                    btnSimpan.Text = "Approve Level ke-" + Convert.ToInt32(mySet.Tables[0].Rows[0]["posisiApprove"]).ToString();
                    btnReject.Text = "Reject Level ke-" + Convert.ToInt32(mySet.Tables[0].Rows[0]["posisiApprove"]).ToString();
                }

            }

            if (grdBudget.Rows.Count > 0)
                showhidebutton.Visible = true;
            else
                showhidebutton.Visible = false;

            if (GridView1.Rows.Count > 0)
                showhidebutton.Visible = true;
            else
                showhidebutton.Visible = false;

            if (GridView2.Rows.Count > 0)
                showhidebutton.Visible = true;
            else
                showhidebutton.Visible = false;

            DataSet mySet1 = ObjDb.GetRows("select systembudget from parameter ");
            DataRow myRow1 = mySet1.Tables[0].Rows[0];
            string systembudget = myRow1["systembudget"].ToString();

            if (systembudget == "Tahun Ajaran")
            {
                pajak.Visible = false;
                tahunAjaran.Visible = true;
                tahunan.Visible = false;

            }
            else if (systembudget == "Tahunan")
            {
                pajak.Visible = false;
                tahunAjaran.Visible = false;
                tahunan.Visible = true;
            }
            else
            {
                pajak.Visible = true;
                tahunAjaran.Visible = false;
                tahunan.Visible = false;
            }
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;

                if (valid == true)
                {

                    string appke = "0";
                    string lvlappr = "1";
                    DataSet mySet = ObjDb.GetRows("select isnull(count(*),0) + 1 as appke from tBudget_Jenjang where tahun = '" + cboYear.Text + "' and noCabang = '" + cboCabang.Text + "'");
                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        DataRow myRow = mySet.Tables[0].Rows[0];

                        appke = myRow["appke"].ToString();
                    }

                    DataSet mySetx = ObjDb.GetRows("select levelApprove from mstApprove where nouser = '" + ObjSys.GetUserId + "' and nocabang = '" + cboCabang.Text + "'");

                    DataRow myRowx = mySetx.Tables[0].Rows[0];
                    lvlappr = myRowx["levelApprove"].ToString();
                    if (appke == lvlappr)
                    {

                        ObjDb.Data.Clear();
                        ObjDb.Data.Add("tahun", cboYear.Text);
                        ObjDb.Data.Add("nouser", ObjSys.GetUserId);
                        ObjDb.Data.Add("apprKe", appke);
                        ObjDb.Data.Add("stsAppr", "1");
                        ObjDb.Data.Add("noCabang", cboCabang.Text);
                        ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                        ObjDb.Insert("tBudget_Jenjang", ObjDb.Data);

                        string jmllevelapp = "0";
                        DataSet mySet2 = ObjDb.GetRows("select TOP 1 levelApprove as jmllevelapp from MstApprove " +
                            "where noParameterApprove = '2' and noCabang = '" + cboCabang.Text + "' " +
                            "ORDER BY levelApprove DESC ");
                        DataRow myRow2 = mySet2.Tables[0].Rows[0];
                        jmllevelapp = myRow2["jmllevelapp"].ToString();


                        if (appke == jmllevelapp)
                        {
                            DataSet mySet4 = ObjDb.GetRows("select systembudget from parameter ");
                            DataRow myRow4 = mySet4.Tables[0].Rows[0];
                            string systembudget = myRow4["systembudget"].ToString();

                            if (systembudget == "Tahun Ajaran")
                            {
                                //for (int i = 0; i < GridView1.Rows.Count; i++)
                                //{
                                //    HiddenField hdnNoRek = (HiddenField)GridView1.Rows[i].FindControl("hdnNoRek");

                                //    ObjDb.Data.Clear();
                                //    ObjDb.Where.Clear();
                                //    ObjDb.Where.Add("noRek", hdnNoRek.Value);
                                //    ObjDb.Where.Add("thn", cboYear.Text);
                                //    ObjDb.Where.Add("nocabang", cboCabang.Text);
                                //    ObjDb.Data.Add("stsApprv", "1");
                                //    ObjDb.Update("tBudget_H ", ObjDb.Data, ObjDb.Where);
                                //}
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("thn", cboYear.Text);
                                ObjGlobal.Param.Add("noCabang", cboCabang.Text);
                                ObjGlobal.GetDataProcedure("SPInsertAppBudget1", ObjGlobal.Param);

                                ObjDb.Data.Clear();
                                ObjDb.Where.Clear();
                                ObjDb.Where.Add("thn", cboYear.Text);
                                ObjDb.Where.Add("nocabang", cboCabang.Text);
                                ObjDb.Where.Add("jenis", cbojenis.Text);
                                ObjDb.Data.Add("stsApprv", "1");
                                ObjDb.Update("tBudget_H ", ObjDb.Data, ObjDb.Where);
                            }
                            else if (systembudget == "Tahunan")
                            {
                                //for (int i = 0; i < GridView2.Rows.Count; i++)
                                //{
                                //    HiddenField hdnNoRek = (HiddenField)GridView2.Rows[i].FindControl("hdnNoRek");

                                //    ObjDb.Data.Clear();
                                //    ObjDb.Where.Clear();
                                //    ObjDb.Where.Add("noRek", hdnNoRek.Value);
                                //    ObjDb.Where.Add("thn", cboYear.Text);
                                //    ObjDb.Where.Add("nocabang", cboCabang.Text);
                                //    ObjDb.Data.Add("stsApprv", "1");
                                //    ObjDb.Update("tBudget_H ", ObjDb.Data, ObjDb.Where);
                                //}
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("thn", cboYear.Text);
                                ObjGlobal.Param.Add("noCabang", cboCabang.Text);
                                ObjGlobal.GetDataProcedure("SPInsertAppBudget1", ObjGlobal.Param);

                                ObjDb.Data.Clear();
                                ObjDb.Where.Clear();
                                ObjDb.Where.Add("thn", cboYear.Text);
                                ObjDb.Where.Add("nocabang", cboCabang.Text);
                                ObjDb.Where.Add("jenis", cbojenis.Text);

                                ObjDb.Data.Add("stsApprv", "1");

                                ObjDb.Update("tBudget_H ", ObjDb.Data, ObjDb.Where);

                            }
                            else
                            {
                                //for (int i = 0; i < grdBudget.Rows.Count; i++)
                                //{
                                //    HiddenField hdnNoRek = (HiddenField)grdBudget.Rows[i].FindControl("hdnNoRek");

                                //    ObjDb.Data.Clear();
                                //    ObjDb.Where.Clear();
                                //    ObjDb.Where.Add("noRek", hdnNoRek.Value);
                                //    ObjDb.Where.Add("thn", cboYear.Text);
                                //    ObjDb.Where.Add("nocabang", cboCabang.Text);
                                //    ObjDb.Data.Add("stsApprv", "1");
                                //    ObjDb.Update("tBudget_H ", ObjDb.Data, ObjDb.Where);
                                //}
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("thn", cboYear.Text);
                                ObjGlobal.Param.Add("noCabang", cboCabang.Text);
                                ObjGlobal.GetDataProcedure("SPInsertAppBudget", ObjGlobal.Param);

                                ObjDb.Data.Clear();
                                ObjDb.Where.Clear();
                                ObjDb.Where.Add("thn", cboYear.Text);
                                ObjDb.Where.Add("nocabang", cboCabang.Text);
                                ObjDb.Where.Add("jenis", cbojenis.Text);

                                ObjDb.Data.Add("stsApprv", "1");
                                ObjDb.Update("tBudget_H ", ObjDb.Data, ObjDb.Where);
                            }



                        }

                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        loaddata();
                        ShowMessage("success", "Data berhasil di approve.");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Harus Approve dilevel " + appke + " terlebih dahulu.");
                    }

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
                    DataSet mySet = ObjDb.GetRows("select isnull(count(*),0) + 1 as rjke from tBudget_Jenjangreject where tahun = '" + cboYear.Text + "' and noCabang = '" + cboCabang.Text + "'");
                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        DataRow myRow = mySet.Tables[0].Rows[0];
                        rjke = myRow["rjke"].ToString();
                    }

                    ObjDb.Data.Clear();
                    ObjDb.Data.Add("tahun", cboYear.Text);
                    ObjDb.Data.Add("nouser", ObjSys.GetUserId);
                    ObjDb.Data.Add("apprKe", rjke);
                    ObjDb.Data.Add("stsAppr", "1");
                    ObjDb.Data.Add("noCabang", cboCabang.Text);
                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                    ObjDb.Insert("tBudget_Jenjangreject", ObjDb.Data);

                    string sql = "delete tBudget_Jenjang where nocabang = '" + cboCabang.Text + "' and tahun = '" + cboYear.Text + "'";
                    ObjDb.ExecQuery(sql);

                    DataSet mySet4 = ObjDb.GetRows("select systembudget from parameter ");
                    DataRow myRow4 = mySet4.Tables[0].Rows[0];
                    string systembudget = myRow4["systembudget"].ToString();

                    if (systembudget == "Tahun Ajaran")
                    {
                        //for (int i = 0; i < GridView1.Rows.Count; i++)
                        //    {
                        //        HiddenField hdnNoRek = (HiddenField)GridView1.Rows[i].FindControl("hdnNoRek");
                        ObjDb.Data.Clear();
                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("thn", cboYear.Text);
                        ObjDb.Where.Add("nocabang", cboCabang.Text);
                        ObjDb.Where.Add("jenis", cbojenis.Text);
                        ObjDb.Data.Add("stsApprv", "2");
                        ObjDb.Data.Add("keteranganreject", txtCatatanReject.Text);
                        ObjDb.Update("tBudget_H ", ObjDb.Data, ObjDb.Where);
                        //}
                    }
                    else if (systembudget == "Tahunan")
                    {
                        //for (int i = 0; i < GridView2.Rows.Count; i++)
                        //{
                        //    HiddenField hdnNoRek = (HiddenField)GridView2.Rows[i].FindControl("hdnNoRek");
                        ObjDb.Data.Clear();
                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("thn", cboYear.Text);
                        ObjDb.Where.Add("nocabang", cboCabang.Text);
                        ObjDb.Where.Add("jenis", cbojenis.Text);

                        ObjDb.Data.Add("stsApprv", "2");
                        ObjDb.Data.Add("keteranganreject", txtCatatanReject.Text);
                        ObjDb.Update("tBudget_H ", ObjDb.Data, ObjDb.Where);
                        //}
                    }
                    else
                    {
                        //for (int i = 0; i < grdBudget.Rows.Count; i++)
                        //{
                        //    HiddenField hdnNoRek = (HiddenField)grdBudget.Rows[i].FindControl("hdnNoRek");
                        ObjDb.Data.Clear();
                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("thn", cboYear.Text);
                        ObjDb.Where.Add("nocabang", cboCabang.Text);
                        ObjDb.Where.Add("jenis", cbojenis.Text);

                        ObjDb.Data.Add("stsApprv", "2");
                        ObjDb.Data.Add("keteranganreject", txtCatatanReject.Text);
                        ObjDb.Update("tBudget_H ", ObjDb.Data, ObjDb.Where);
                        //}
                    }



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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loaddata(cboYear.Text, cboCabang.Text, cbojenis.Text);
            showhidebutton.Visible = true;

        }

    }
}