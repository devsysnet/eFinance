using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using eFinance.GlobalApp;

namespace eFinance.Pages.Transaksi.View
{
    public partial class RRekaptransaksibyrall : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCombo();
                LoadData();
                this.ShowHideGridAndForm(true, false);
            }
        }

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            //ObjGlobal.Param.Add("notransaksi", cbotransaksi.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            grdRekap.DataSource = ObjGlobal.GetDataProcedure("SPLoadDatarekapjnsbayar1", ObjGlobal.Param);
            grdRekap.DataBind();

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";
            try
            {
                if (valid == true)
                {
                    using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                    {

                        SqlCommand cmd = new SqlCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        DataTable dt = new DataTable();
                        try
                        {
                            cmd = new SqlCommand("SPLoadDatarekapjnsbayardetail1", con);
                            cmd.Parameters.Add(new SqlParameter("@notransaksi", hdnnotransaksi.Value));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", hdnnocabangdet.Value));
                            cmd.Parameters.Add(new SqlParameter("@tgl", hdntgldet.Value));
                            cmd.Parameters.Add(new SqlParameter("@norek", hdnnorekdet.Value));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "DetailDatapelunasan_" + ObjSys.GetNow + ".xls";
                            ViewHelper.DownloadExcel(Response, fileName, dt);
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            //ShowMessage("error", ex.ToString());
                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    //ShowMessage(alert, message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //ShowMessage("error", ex.ToString());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdRekap_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdRekap.PageIndex = e.NewPageIndex;
            LoadData();
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false);
            LoadData();
        }


        protected void grdRekap_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = int.Parse(e.CommandArgument.ToString());

                if (e.CommandName == "SelectEdit")
                {
                    HiddenField hdnId = (HiddenField)grdRekap.Rows[rowIndex].FindControl("hdnId");
                    HiddenField hdntgl = (HiddenField)grdRekap.Rows[rowIndex].FindControl("hdntgl");
                    HiddenField hdnnorek = (HiddenField)grdRekap.Rows[rowIndex].FindControl("hdnnorek");

                    ObjGlobal.Param.Clear();
                    hdnnotransaksi.Value = hdnId.Value;
                    hdnnorekdet.Value = hdnnorek.Value;
                    hdntgldet.Value = hdntgl.Value;
                    hdnnocabangdet.Value = cboCabang.Text;
                    ObjGlobal.Param.Add("noTransaksi", hdnId.Value);
                    ObjGlobal.Param.Add("norek", hdnnorek.Value);
                    ObjGlobal.Param.Add("nocabang", cboCabang.Text);
                    ObjGlobal.Param.Add("tgl", hdntgl.Value);
                    GridView1.DataSource = ObjGlobal.GetDataProcedure("SPLoadDatarekapjnsbayardetail", ObjGlobal.Param);
                    GridView1.DataBind();

                    dlgDetilKas.Show();

                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void loadCombo()
        {
            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang
            //pusat
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang=2) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct '0' id,'--Semua Cabang--' name union SELECT distinct nocabang id, namaCabang name FROM vCabang where stscabang=2) a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //kepala perwakilan
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                //cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                //        "union " +
                //        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct '0' id,'--Semua Cabang--' name union SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //admin perwakilan
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct '0' id,'--Semua Cabang--' name union SELECT distinct nocabang id, namaCabang name FROM vCabang b inner join (select parent from mcabang where nocabang = '" + ObjSys.GetCabangId + "') c on b.parent=c.parent and stscabang=2) a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //unit
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "' and stscabang=2) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

            LoadDataAccount(cboCabang.Text);
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataAccount(cboCabang.Text);
        }


        protected void LoadDataAccount(string cabang = "")
        {
            //cbotransaksi.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'--Semua Transaksi--' as name UNION SELECT distinct noTransaksi id, jenisTransaksi name FROM mJenisTransaksi where sts='1' and nocabang = '" + cabang + "') a order by a.id");
            //cbotransaksi.DataValueField = "id";
            //cbotransaksi.DataTextField = "name";
            //cbotransaksi.DataBind();
        }
    }
}