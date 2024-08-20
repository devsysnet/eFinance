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
    public partial class TransAbsensiUpdate : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtAbsen.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                LoadData(dtAbsen.Text);
            }
        }

        protected void LoadBindTime()
        {
            List<string> hours = new List<string>();
            List<string> minutes = new List<string>();
            //List<string> seconds = new List<string>();

            for (int i = 0; i <= 59; i++)
            {
                if (i < 24)
                {
                    hours.Add(i.ToString("00"));
                }
                minutes.Add(i.ToString("00"));
                //seconds.Add(i.ToString("00"));
            }

            for (int i = 0; i < grdAbsensiUpdate.Rows.Count; i++)
            {
                DropDownList cbojammasuk = (DropDownList)grdAbsensiUpdate.Rows[i].FindControl("cbojammasuk");
                DropDownList cbomenitmasuk = (DropDownList)grdAbsensiUpdate.Rows[i].FindControl("cbomenitmasuk");
                DropDownList cbojamkeluar = (DropDownList)grdAbsensiUpdate.Rows[i].FindControl("cbojamkeluar");
                DropDownList cbomenitkeluar = (DropDownList)grdAbsensiUpdate.Rows[i].FindControl("cbomenitkeluar");

                cbojammasuk.DataSource = hours;
                cbojammasuk.DataBind();

                cbomenitmasuk.DataSource = minutes;
                cbomenitmasuk.DataBind();

                cbojamkeluar.DataSource = hours;
                cbojamkeluar.DataBind();

                cbomenitkeluar.DataSource = minutes;
                cbomenitkeluar.DataBind();

                //ddlSeconds.DataSource = seconds;
                //ddlSeconds.DataBind();
            }
        }
        protected void LoadData(string tgl = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("tgl", Convert.ToDateTime(tgl).ToString("yyyy-MM-dd"));
            grdAbsensiUpdate.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAbsensiKaryawanUpdate", ObjGlobal.Param);
            grdAbsensiUpdate.DataBind();
            if (grdAbsensiUpdate.Rows.Count > 0)
                if (grdAbsensiUpdate.Enabled == false)
                {
                    btnHapus.Visible = false;
                    btnSimpan.Visible = false;
                }
                else
                {
                    btnHapus.Visible = true;
                    btnSimpan.Visible = true;
                }
            else
            {
                btnHapus.Visible = false;
                btnSimpan.Visible = false;
            }
                

            LoadBindTime();

            for (int i = 0; i < grdAbsensiUpdate.Rows.Count; i++)
            {
                HiddenField hdnSts = (HiddenField)grdAbsensiUpdate.Rows[i].FindControl("hdnSts");
                HiddenField hdnjammasuk = (HiddenField)grdAbsensiUpdate.Rows[i].FindControl("hdnjammasuk");
                HiddenField hdnmenitmasuk = (HiddenField)grdAbsensiUpdate.Rows[i].FindControl("hdnmenitmasuk");
                HiddenField hdnjamkeluar = (HiddenField)grdAbsensiUpdate.Rows[i].FindControl("hdnjamkeluar");
                HiddenField hdnmenitkeluar = (HiddenField)grdAbsensiUpdate.Rows[i].FindControl("hdnmenitkeluar");
                DropDownList cbojammasuk = (DropDownList)grdAbsensiUpdate.Rows[i].FindControl("cbojammasuk");
                DropDownList cbomenitmasuk = (DropDownList)grdAbsensiUpdate.Rows[i].FindControl("cbomenitmasuk");
                DropDownList cbojamkeluar = (DropDownList)grdAbsensiUpdate.Rows[i].FindControl("cbojamkeluar");
                DropDownList cbomenitkeluar = (DropDownList)grdAbsensiUpdate.Rows[i].FindControl("cbomenitkeluar");

                if (hdnSts.Value == "0")
                {
                    cbojammasuk.SelectedValue = (System.DateTime.Now.Hour).ToString();
                    cbomenitmasuk.SelectedValue = (System.DateTime.Now.Minute).ToString();
                    cbojamkeluar.SelectedValue = (System.DateTime.Now.Hour).ToString();
                    cbomenitkeluar.SelectedValue = (System.DateTime.Now.Minute).ToString();
                }
                else
                {
                    cbojammasuk.SelectedValue = hdnjammasuk.Value;
                    cbomenitmasuk.SelectedValue = hdnmenitmasuk.Value;
                    cbojamkeluar.SelectedValue = hdnjamkeluar.Value;
                    cbomenitkeluar.SelectedValue = hdnmenitkeluar.Value;
                }
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
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            int adaDataPilih = 0;
            for (int i = 0; i < grdAbsensiUpdate.Rows.Count; i++)
            {
                CheckBox chkCheck = (CheckBox)grdAbsensiUpdate.Rows[i].FindControl("chkCheck");

                if (chkCheck.Checked == true)
                    adaDataPilih++;
            }

            if (adaDataPilih == 0)
            {
                message += ObjSys.CreateMessage("Karyawan belum dipilih.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                    for (int i = 0; i < grdAbsensiUpdate.Rows.Count; i++)
                    {
                        HiddenField hdnNoKaryawan = (HiddenField)grdAbsensiUpdate.Rows[i].FindControl("hdnNoKaryawan");
                        DropDownList cbojammasuk = (DropDownList)grdAbsensiUpdate.Rows[i].FindControl("cbojammasuk");
                        DropDownList cbomenitmasuk = (DropDownList)grdAbsensiUpdate.Rows[i].FindControl("cbomenitmasuk");
                        DropDownList cbojamkeluar = (DropDownList)grdAbsensiUpdate.Rows[i].FindControl("cbojamkeluar");
                        DropDownList cbomenitkeluar = (DropDownList)grdAbsensiUpdate.Rows[i].FindControl("cbomenitkeluar");
                        CheckBox chkCheck = (CheckBox)grdAbsensiUpdate.Rows[i].FindControl("chkCheck");

                        if (chkCheck.Checked == true)
                        {
                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("noKaryawan", hdnNoKaryawan.Value);
                            ObjGlobal.Param.Add("tgl", Convert.ToDateTime(dtAbsen.Text).ToString("dd-MMM-yyyy"));
                            ObjGlobal.Param.Add("jammasuk", Convert.ToString(cbojammasuk.Text) + ":" + Convert.ToString(cbomenitmasuk.Text) + ":00");
                            ObjGlobal.Param.Add("jamkeluar", Convert.ToString(cbojamkeluar.Text) + ":" + Convert.ToString(cbomenitkeluar.Text) + ":00");
                            ObjGlobal.Param.Add("sts", "1");
                            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                            ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);

                            ObjGlobal.ExecuteProcedure("SPUpdateAbsenkaryawan", ObjGlobal.Param);

                        }

                    }
                    LoadData(dtAbsen.Text);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan");
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
            dtAbsen.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            LoadData(dtAbsen.Text);
            CloseMessage();
        }

        protected void TglAbsen_SelectionChanged(object sender, EventArgs e)
        {
            dtAbsen.Text = TglAbsen.SelectedDate.ToString("dd-MMM-yyyy");
            TglAbsen.Visible = false;
            DateTime tanggalAkhir = DateTime.ParseExact(dtAbsen.Text, "dd-MMM-yyyy", null);
            LoadData(dtAbsen.Text);
            CloseMessage();
        }

        protected void lnkPickDate_Click(object sender, EventArgs e)
        {
            TglAbsen.Visible = true;
        }

        protected void grdAbsensiUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkCheck = (CheckBox)e.Row.FindControl("chkCheck");
                CheckBox chkCheck2 = (CheckBox)e.Row.FindControl("chkCheck2");
                HiddenField hdnSts = (HiddenField)e.Row.FindControl("hdnSts");

                if (hdnSts.Value == "0")
                {
                    chkCheck.Checked = false;
                    chkCheck2.Enabled = false;
                }
                else
                {
                    chkCheck.Checked = true;
                    chkCheck2.Enabled = true;
                }
            }
        }

        protected void TglAbsen_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date > DateTime.Today)
            {
                e.Day.IsSelectable = false;
            }
        }

        protected void btnHapus_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            int adaDataPilih = 0;
            for (int i = 0; i < grdAbsensiUpdate.Rows.Count; i++)
            {
                CheckBox chkCheck2 = (CheckBox)grdAbsensiUpdate.Rows[i].FindControl("chkCheck2");

                if (chkCheck2.Checked == true)
                    adaDataPilih++;
            }

            if (adaDataPilih == 0)
            {
                message += ObjSys.CreateMessage("Karyawan belum dipilih.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                    for (int i = 0; i < grdAbsensiUpdate.Rows.Count; i++)
                    {
                        HiddenField hdnNoKaryawan = (HiddenField)grdAbsensiUpdate.Rows[i].FindControl("hdnNoKaryawan");
                        CheckBox chkCheck2 = (CheckBox)grdAbsensiUpdate.Rows[i].FindControl("chkCheck2");

                        if (chkCheck2.Checked == true)
                        {
                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("noKaryawan", hdnNoKaryawan.Value);
                            ObjGlobal.Param.Add("tgl", Convert.ToDateTime(dtAbsen.Text).ToString("dd-MMM-yyyy"));
                            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);

                            ObjGlobal.ExecuteProcedure("SPDeleteAbsenkaryawan", ObjGlobal.Param);

                        }

                    }
                    LoadData(dtAbsen.Text);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus");
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
    }
}