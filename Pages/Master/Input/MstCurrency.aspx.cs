using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

namespace eFinance.Pages.Master.Input
{
    public partial class MstCurrency : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstCurrency.aspx");
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

        protected void ClearData()
        {
            txtCurrencyName.Text = "";
            txtCode.Text = "";
            txtCountry.Text = "";
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (txtCurrencyName.Text == "")
            {
                message += ObjSys.CreateMessage("Currency Name Tidak Boleh Kosong.");
                valid = false;
            }
            if (txtCode.Text == "")
            {
                message += ObjSys.CreateMessage("Code Tidak Boleh Kosong.");
                valid = false;
            }
            if (txtCountry.Text == "")
            {
                message += ObjSys.CreateMessage("Country Tidak Boleh Kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {

                    DataSet mySet = ObjDb.GetRows("Select * from mMataUang where kodeMataUang = '" + txtCode.Text + "'");
                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        ShowMessage("error", "Code sudah ada.");

                    }
                    else
                    {
                        ObjDb.Data.Add("namaMataUang", txtCurrencyName.Text);
                        ObjDb.Data.Add("kodeMataUang", txtCode.Text);
                        ObjDb.Data.Add("Negara", txtCountry.Text);
                        ObjDb.Data.Add("stsMataUang", cboStatus.Text);
                        ObjDb.Data.Add("stsDefault", cboSet.Text);
                        ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                        ObjDb.Insert("mMataUang", ObjDb.Data);

                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil disimpan.");
                        ClearData();
                    }

                }

                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Invalid transaction data to database.");
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
            ClearData();
            CloseMessage();
        }

    }
}