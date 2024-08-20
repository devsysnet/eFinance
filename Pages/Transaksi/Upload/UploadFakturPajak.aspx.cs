using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.IO.Compression;
using ClosedXML.Excel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Upload
{
    public partial class UploadFakturPajak : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtMulai.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                dtSampai.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadData();
            }
        }
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("tglMulai", dtMulai.Text);
            ObjGlobal.Param.Add("tglSampai", dtSampai.Text);
            grdUploadFakturPajak.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUploadFakturPajak", ObjGlobal.Param);
            grdUploadFakturPajak.DataBind();
            if (grdUploadFakturPajak.Rows.Count > 0)
                btnExport.Enabled = true;
            else
                btnExport.Enabled = false;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();

        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                XLWorkbook wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("FAKTUR" + DateTime.Now.ToString("yyyy"));

                #region Header
                ws.Cell("A1").Value = "FK";
                ws.Cell("A2").Value = "LT";
                ws.Cell("A3").Value = "OF";

                ws.Cell("B1").Value = "KD_JENIS_TRANSAKSI";
                ws.Cell("B2").Value = "NPWP";
                ws.Cell("B3").Value = "KODE_OBJEK";

                ws.Cell("C1").Value = "FG_PENGGANTI";
                ws.Cell("C2").Value = "NAMA";
                ws.Cell("C3").Value = "NAMA";

                ws.Cell("D1").Value = "NOMOR_FAKTUR";
                ws.Cell("D2").Value = "JALAN";
                ws.Cell("D3").Value = "HARGA_SATUAN";

                ws.Cell("E1").Value = "MASA_PAJAK";
                ws.Cell("E2").Value = "BLOK";
                ws.Cell("E3").Value = "JUMLAH_BARANG";

                ws.Cell("F1").Value = "TAHUN_PAJAK";
                ws.Cell("F2").Value = "NOMOR";
                ws.Cell("F3").Value = "JUMLAH_BARANG";

                ws.Cell("G1").Value = "TANGGAL_FAKTUR";
                ws.Cell("G2").Value = "RT";
                ws.Cell("G3").Value = "DISKON";

                ws.Cell("H1").Value = "NPWP";
                ws.Cell("H2").Value = "RW";
                ws.Cell("H3").Value = "DPP";

                ws.Cell("I1").Value = "NAMA";
                ws.Cell("I2").Value = "KECAMATAN";
                ws.Cell("I3").Value = "PPN";

                ws.Cell("J1").Value = "ALAMAT_LENGKAP";
                ws.Cell("J2").Value = "KELURAHAN";
                ws.Cell("J3").Value = "TARIF_PPNBM";

                ws.Cell("K1").Value = "JUMLAH_DPP";
                ws.Cell("K2").Value = "KELURAHAN";
                ws.Cell("K3").Value = "KABUPATEN";

                ws.Cell("L1").Value = "JUMLAH_PPN";
                ws.Cell("L2").Value = "PROPINSI";

                ws.Cell("M1").Value = "JUMLAH_PPNBM";
                ws.Cell("M2").Value = "KODE_POS";

                ws.Cell("N1").Value = "ID_KETERANGAN_TAMBAHAN";
                ws.Cell("N2").Value = "NOMOR_TELEPON";
                ws.Cell("O1").Value = "FG_UANG_MUKA";
                ws.Cell("P1").Value = "UANG_MUKA_DPP";
                ws.Cell("Q1").Value = "UANG_MUKA_PPN";
                ws.Cell("R1").Value = "UANG_MUKA_PPNBM";
                ws.Cell("S1").Value = "REFERENSI";
                #endregion

                #region data
                int mulai = 5;
                int mulai2 = 6;
                int mulai3 = 7;
                for (int i = 0; i < grdUploadFakturPajak.Rows.Count; i++)
                {
                    CheckBox chkCheck = (CheckBox)grdUploadFakturPajak.Rows[i].FindControl("chkCheck");

                    if (chkCheck.Checked == true)
                    {
                        string itemId = grdUploadFakturPajak.DataKeys[i].Value.ToString();
                        HiddenField hdntipeInv = (HiddenField)grdUploadFakturPajak.Rows[i].FindControl("hdntipeInv");

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noInvoice", itemId);
                        ObjGlobal.Param.Add("tipeInv", hdntipeInv.Value);
                        DataSet mySet = ObjGlobal.GetDataProcedure("SPDataUploadFakturPajak", ObjGlobal.Param);
                        DataRow myRow = mySet.Tables[0].Rows[0];

                        ws.Cell("A" + mulai).Value = "FK";
                        ws.Cell("B" + mulai).Value = myRow["kodePajak"].ToString();
                        ws.Cell("C" + mulai).Value = 0;
                        ws.Cell("D" + mulai).Value = myRow["nomorPajak"].ToString();
                        ws.Cell("E" + mulai).Value = myRow["month"].ToString();
                        ws.Cell("F" + mulai).Value = myRow["year"].ToString();
                        ws.Cell("G" + mulai).Value = myRow["tglPajak"].ToString();
                        ws.Cell("H" + mulai).Value = myRow["NPWP"].ToString();
                        ws.Cell("I" + mulai).Value = myRow["namaCust"].ToString();
                        ws.Cell("J" + mulai).Value = myRow["alamatTax"].ToString();
                        ws.Cell("K" + mulai).Value = myRow["nilaiDPP"].ToString();
                        ws.Cell("L" + mulai).Value = myRow["nilaiPPN"].ToString();
                        ws.Cell("M" + mulai).Value = 0;
                        ws.Cell("N" + mulai).Value = 0;
                        ws.Cell("O" + mulai).Value = 0;
                        ws.Cell("P" + mulai).Value = 0;
                        ws.Cell("Q" + mulai).Value = 0;
                        ws.Cell("R" + mulai).Value = 0;
                        ws.Cell("S" + mulai).Value = "";

                        ws.Cell("A" + mulai2).Value = "F" + Convert.ToDateTime(myRow["tglPajak"].ToString()).ToString("MMM").ToUpper();
                        ws.Cell("B" + mulai2).Value = myRow["namaPT"].ToString();
                        ws.Cell("C" + mulai2).Value = myRow["alamatPT"].ToString();
                        ws.Cell("D" + mulai2).Value = myRow["penandatangan"].ToString();
                        ws.Cell("E" + mulai2).Value = myRow["AgamaPT"].ToString();
                        ws.Cell("F" + mulai2).Value = myRow["noNPWP"].ToString();
                        ws.Cell("G" + mulai2).Value = "";
                        ws.Cell("H" + mulai2).Value = "";
                        ws.Cell("I" + mulai2).Value = "";
                        ws.Cell("J" + mulai2).Value = "";
                        ws.Cell("K" + mulai2).Value = "";
                        ws.Cell("L" + mulai2).Value = "";
                        ws.Cell("M" + mulai2).Value = "";
                        ws.Cell("N" + mulai2).Value = "";
                        ws.Cell("O" + mulai2).Value = "";
                        ws.Cell("P" + mulai2).Value = "";
                        ws.Cell("Q" + mulai2).Value = "";
                        ws.Cell("R" + mulai2).Value = "";
                        ws.Cell("S" + mulai2).Value = "";

                        ws.Cell("A" + mulai3).Value = "OF";
                        ws.Cell("B" + mulai3).Value = "";
                        ws.Cell("C" + mulai3).Value = myRow["jenis"].ToString();
                        ws.Cell("D" + mulai3).Value = myRow["nilaiDPP"].ToString();
                        ws.Cell("E" + mulai3).Value = 1;
                        ws.Cell("F" + mulai3).Value = Convert.ToDecimal(myRow["nilaiDPP"].ToString()) * 1;
                        ws.Cell("G" + mulai3).Value = 0;
                        ws.Cell("H" + mulai3).Value = Convert.ToDecimal(myRow["nilaiDPP"].ToString()) * 1;
                        ws.Cell("I" + mulai3).Value = Convert.ToDecimal(myRow["nilaiPPN"].ToString()) * 1;
                        ws.Cell("J" + mulai3).Value = 0;
                        ws.Cell("K" + mulai3).Value = 0;
                        ws.Cell("L" + mulai3).Value = 0;
                        ws.Cell("M" + mulai3).Value = 0;
                        ws.Cell("N" + mulai3).Value = 0;
                        ws.Cell("O" + mulai3).Value = 0;
                        ws.Cell("P" + mulai3).Value = 0;
                        ws.Cell("Q" + mulai3).Value = 0;
                        ws.Cell("R" + mulai3).Value = 0;
                        ws.Cell("S" + mulai3).Value = "";

                        mulai += 3;
                        mulai2 += 3;
                        mulai3 += 3;

                        if (hdntipeInv.Value == "Sales Order")
                            ObjDb.ExecQuery("update tInvoiceSO_H set noKD_JENIS_TRANSAKSI = '1', noFG_PENGGANTI = '1' where noInv = '" + itemId + "'");
                        else
                            ObjDb.ExecQuery("update tInvoiceGeneral_H set noKD_JENIS_TRANSAKSI = '1', noFG_PENGGANTI = '1' where noInvoiceGen = '" + itemId + "'");

                    }
                }

                #endregion

                ws.Column("A").Width = 8;
                ws.Column("B").Width = 23;
                ws.Column("C").Width = 23;
                ws.Column("D").Width = 23;
                ws.Column("E").Width = 23;
                ws.Column("F").Width = 23;
                ws.Column("G").Width = 23;
                ws.Column("H").Width = 23;
                ws.Column("I").Width = 23;
                ws.Column("J").Width = 23;
                ws.Column("K").Width = 23;
                ws.Column("L").Width = 23;
                ws.Column("M").Width = 23;
                ws.Column("N").Width = 23;
                ws.Column("O").Width = 23;
                ws.Column("P").Width = 23;
                ws.Column("Q").Width = 23;
                ws.Column("R").Width = 23;
                ws.Column("S").Width = 23;

                Random rnd = new Random();
                int codeRnd = rnd.Next(100, 9999);

                wb.SaveAs(Func.TempFolder + "/" + "Faktur" + DateTime.Now.ToString("yyyy") + codeRnd.ToString() + "." + cboType.Text + "");

                LoadData();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "File has been saved in assets/document/Faktur" + DateTime.Now.ToString("yyyy") + codeRnd.ToString() + "." + cboType.Text + "" + ".");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Invalid export data.");
            }
        }

        protected void grdUploadFakturPajak_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUploadFakturPajak.PageIndex = e.NewPageIndex;
            LoadData();
        }

    }
}