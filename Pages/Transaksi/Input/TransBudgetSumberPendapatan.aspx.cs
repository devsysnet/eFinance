using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransBudgetSumberPendapatan : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loaddata();
            }
        }
        protected void loaddata()
        {
            cboTA.DataSource = ObjDb.GetRows("select distinct tahunAjaran as id, tahunAjaran as name from Parameter");
            cboTA.DataValueField = "id";
            cboTA.DataTextField = "name";
            cboTA.DataBind();

            ObjGlobal.Param.Clear();
            grdBudget.DataSource = ObjGlobal.GetDataProcedure("SPViewInsertBudgetSumberPendapatan", ObjGlobal.Param);
            grdBudget.DataBind();




        }
        protected void GridView31_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //GridView HeaderGrid = (GridView)sender;
                //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //TableCell HeaderCell = new TableCell();
                //HeaderCell.Text = "PENDAPATAN";
                //HeaderCell.ColumnSpan = 3;
                //HeaderGridRow.Cells.Add(HeaderCell);

                //HeaderCell = new TableCell();
                //HeaderCell.Text = "RINCIAN PERHITUNGAN";
                //HeaderCell.ColumnSpan = 6;
                //HeaderGridRow.Cells.Add(HeaderCell);

                //HeaderCell = new TableCell();
                //HeaderCell.Text = "KETR.";
                //HeaderCell.RowSpan = 2;
                //HeaderGridRow.Cells.Add(HeaderCell);

                //grdBudget.Controls[0].Controls.AddAt(0, HeaderGridRow);
                //e.Row.Cells[9].Visible = false;

            }


        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();
            DataSet mySetx = ObjDb.GetRows("select systembudget from parameter ");
            DataRow myRowx = mySetx.Tables[0].Rows[0];
            string systembudgetx = myRowx["systembudget"].ToString();
            //int countTBudget = 0;

            //    countTBudget = ObjDb.GetRows("select * from tBudget_H where keterangan = 'tahunan' and nocabang = '" + ObjSys.GetCabangId + "' and thn='" + cboTA.Text + "'").Tables[0].Rows.Count;

            //if (countTBudget > 0)
            //{

            //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            //    ShowMessage("error", "Budget Sudah Diinput");
            //}
            //else
            //{

                if (valid == true)
                {
                    try
                    {


                        for (int i = 0; i < grdBudget.Rows.Count; i++)
                        {
                            HiddenField hdnNoRek = (HiddenField)grdBudget.Rows[i].FindControl("hdnNoRek");
                            Label txtkdRek = (Label)grdBudget.Rows[i].FindControl("txtkdRek");
                            Label txtKet = (Label)grdBudget.Rows[i].FindControl("txtKet");
                            TextBox txtVol = (TextBox)grdBudget.Rows[i].FindControl("txtVol");
                            TextBox txtSat = (TextBox)grdBudget.Rows[i].FindControl("txtSat");
                            TextBox txtHargaSat = (TextBox)grdBudget.Rows[i].FindControl("txtHargaSat");
                            TextBox txtJumlah = (TextBox)grdBudget.Rows[i].FindControl("txtJumlah");
                            TextBox txtSubJumlah = (TextBox)grdBudget.Rows[i].FindControl("txtSubJumlah");
                            TextBox txtTotal = (TextBox)grdBudget.Rows[i].FindControl("txtTotal");
                            TextBox txtKeterangan = (TextBox)grdBudget.Rows[i].FindControl("txtKeterangan");

                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("noRek", hdnNoRek.Value);
                            ObjGlobal.Param.Add("kdRek", txtkdRek.Text);
                            ObjGlobal.Param.Add("tahunAjaran", cboTA.Text);
                            ObjGlobal.Param.Add("namaAkun", txtKet.Text);
                            ObjGlobal.Param.Add("volume", txtVol.Text);
                            ObjGlobal.Param.Add("satuan", txtSat.Text);
                            ObjGlobal.Param.Add("hargaSatuan", txtHargaSat.Text);
                            ObjGlobal.Param.Add("jumlah", txtJumlah.Text);
                            ObjGlobal.Param.Add("subJumlah", txtSubJumlah.Text);
                            ObjGlobal.Param.Add("total", txtTotal.Text);
                            ObjGlobal.Param.Add("keterangan", txtKeterangan.Text);

                            ObjGlobal.Param.Add("jenis", "pendapatan");
                            ObjGlobal.GetDataProcedure("SPInsertTransBudgetSumberPendapatan", ObjGlobal.Param);

                        //DataSet mySet2 = ObjDb.GetRows("select * from tBudget_H where norek = '" + hdnNoRek.Value + "' and nocabang='" + ObjSys.GetCabangId + "' and thn='" + cboTA.Text + "'");
                        //DataRow myRow2 = mySet2.Tables[0].Rows[0];
                        //string noPO = myRow2["noBudget"].ToString();


                        //ObjGlobal.Param.Clear();
                        //ObjGlobal.Param.Add("noBudget", noPO);

                        //ObjGlobal.GetDataProcedure("SPInsertBudgetD", ObjGlobal.Param);
                    }
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil disimpan.");
                        clearData();
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
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", message);
                }
           // }
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
        protected void clearData()
        {
            for (int i = 0; i < grdBudget.Rows.Count; i++)
            {
                TextBox txtVol = (TextBox)grdBudget.Rows[i].FindControl("txtVol");
                TextBox txtSat = (TextBox)grdBudget.Rows[i].FindControl("txtSat");
                TextBox txtHargaSat = (TextBox)grdBudget.Rows[i].FindControl("txtHargaSat");
                TextBox txtJumlah = (TextBox)grdBudget.Rows[i].FindControl("txtJumlah");
                TextBox txtSubJumlah = (TextBox)grdBudget.Rows[i].FindControl("txtSubJumlah");
                TextBox txtTotal = (TextBox)grdBudget.Rows[i].FindControl("txtTotal");
                TextBox txtKeterangan = (TextBox)grdBudget.Rows[i].FindControl("txtKeterangan");

                txtKeterangan.Text = "";
                txtVol.Text = "0.00";
                txtSat.Text = "0.00";
                txtSubJumlah.Text = "0.00";

                txtHargaSat.Text = "0.00";
                txtJumlah.Text = "0.00";
                txtTotal.Text = "0.00";
            }
           
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
    }

}
