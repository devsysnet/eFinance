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
using System.Net.Mail;
using System.Net;
using System.Runtime.InteropServices;

namespace eFinance.Pages.Transaksi.View
{
    public partial class ReportSlipGajiKaryawan : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                loadData(cboCabang.Text, cbobln.Text, cbothn.Text);

            }
        }
        protected void tabelDinamis(DataSet ds)
        {
            // Initialize columns
            List<String> datacolumns = new List<String> {
                //"No",
               
                "Nama_Pegawai",

            };

            // Clear and add initial columns
            grdAccount.Columns.Clear();

            foreach (String datacolumn in datacolumns)
            {
                BoundField bfield = new BoundField();
                bfield.HeaderText = datacolumn;
                bfield.DataField = datacolumn;
                grdAccount.Columns.Add(bfield);
            }

            // Add new columns
            DataTable headerTable = ds.Tables[0];
            int headerLength = headerTable.Rows.Count;
            for (int i = 0; i < headerLength; i++)
            {
                String jenis = headerTable.Rows[i]["jenis"].ToString();
                BoundField bfield = new BoundField();
                bfield.HeaderText = jenis;
                bfield.DataField = jenis;
                bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdAccount.Columns.Add(bfield);
            }


            // Fill data if exists
            if (ds.Tables.Count > 1)
            {
                DataTable dataTable = ds.Tables[1];
                grdAccount.DataSource = dataTable;
            }
            grdAccount.DataBind();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                HttpContext.Current.Session["ParamReport"] = null;
                Session["REPORTNAME"] = null;
                Session["REPORTTITLE"] = null;
                Param.Clear();
                Param.Add("bln", cbobln.Text);
                Param.Add("thn", cbothn.Text);
                Param.Add("nocabang", cboCabang.Text);
                HttpContext.Current.Session.Add("ParamReport", Param);
                Session["REPORTNAME"] = "RPrintSlipGaji1.rpt";
                Session["REPORTTILE"] = "Report Slip Gaji";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);

            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.ToString());
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
            }
        }


        protected void btnemail_Click(object sender, EventArgs e)
        {
            try
            {
                HttpContext.Current.Session["ParamReport"] = null;
                Session["REPORTNAME"] = null;
                Session["REPORTTITLE"] = null;
                Param.Clear();
                Param.Add("thn", cbothn.Text);
                Param.Add("bln", cbobln.Text);
                Param.Add("nocabang", cboCabang.Text);

                DataSet mySet = ObjGlobal.GetDataProcedure("SPSPreportDaftargajislipemail", Param);

                if (mySet.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                string emailBuffer = null;
                List<DataRow> rowBuffer = new List<DataRow>();

                foreach (DataRow row in mySet.Tables[0].Rows)
                {
                    string curEmail = row["email"].ToString();

                    if (string.IsNullOrWhiteSpace(curEmail))
                    {
                        rowBuffer.Add(row);
                        continue;
                    }

                    if (emailBuffer != null)
                    {
                        SendEmail(new string[] { emailBuffer }, ComposeBody(rowBuffer));
                    }

                    try
                    {
                        // Check valid mail address
                        new MailAddress(curEmail);
                        emailBuffer = curEmail;
                    }
                    catch
                    {
                        emailBuffer = null;
                    }

                    rowBuffer = new List<DataRow>();
                }

                // Send Last Buffer
                if (emailBuffer != null)
                {
                    SendEmail(new string[] { emailBuffer }, ComposeBody(rowBuffer));
                }

                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Success send email');", true);
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.ToString());
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Error send email');", true);
            }
        }

        private string ComposeBody(List<DataRow> rows)
        {
            string tableBody = string.Empty;

            foreach (DataRow row in rows)
            {
                string ket = row["ket"].ToString();

                if (string.IsNullOrWhiteSpace(ket))
                {
                    continue;
                }

                tableBody += ComposeRow(
                    ComposeColumnData(ket) +
                    ComposeColumnData(row["nilai"].ToString()) +
                    ComposeColumnData(row["total"].ToString())
                );
            }

            return ComposeTable(tableBody);
        }

        private string ComposeTable(string str)
        {
            return $@"<table border='1'>{str}</table>";
        }

        private string ComposeRow(string str)
        {
            return $@"<tr>{str}</tr>";
        }

        private string ComposeColumnHeader(string str)
        {
            return $@"<th>{str}</th>";
        }

        private string ComposeColumnData(string str)
        {
            return $@"<td>{str}</td>";
        }

        private SmtpClient getEmailClient(string senderEmail, string senderPassword)
        {
            DataSet mySet = ObjDb.GetRows("select top 1 * from parameter ");
            DataRow myRow = mySet.Tables[0].Rows[0];

            string smtphost = myRow["smtphost"].ToString();
            //string smtpHost = "127.0.0.1";
            int smtpPort = 25;

            return new SmtpClient(smtphost, smtpPort)
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
                //EnableSsl = false,
            };
        }

        private void SendEmail(string[] penerimaEmails, string emailBody)
        {

            DataSet mySet = ObjDb.GetRows("select top 1 * from parameterCabang ");
            DataRow myRow = mySet.Tables[0].Rows[0];


            string senderEmail = myRow["emailsender"].ToString();
            string senderPassword = myRow["passwordsender"].ToString();
 


            SmtpClient client = getEmailClient(senderEmail, senderPassword);

            DataSet mySeta = ObjDb.GetRows("select * from parameterCabang where nocabang = '" + ObjSys.GetCabangId + "' ");
            DataRow myRowa = mySeta.Tables[0].Rows[0];
            string emailcc = myRow["emailpenerimacc"].ToString();

            string[] penerimaCC = { emailcc };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = "Slip Gaji Karyawan",
                Body = emailBody,
                IsBodyHtml = true
            };

            foreach (string penerima in penerimaEmails)
            {
                mailMessage.To.Add(penerima);
            }

            foreach (string cc in penerimaCC)
            {
                mailMessage.CC.Add(cc);
            }

            client.Send(mailMessage);
        }


        protected void loadData(string unit = "",string bln = "", string thn = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", unit);
            ObjGlobal.Param.Add("bln", bln);
            ObjGlobal.Param.Add("thn", thn);

            tabelDinamis(ObjGlobal.GetDataProcedure("SPreportDaftargajiKaryawan", ObjGlobal.Param));
        }

        protected void loadDataCombo()
        {
            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang
            //pusat
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang) a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang where parent='" + ObjSys.GetParentCabang + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //perwakilan
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //unit
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

            cbothn.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
            cbothn.DataValueField = "id";
            cbothn.DataTextField = "name";
            cbothn.DataBind();

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
                            cmd = new SqlCommand("SPreportDaftargajiKaryawan", con);
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
                            cmd.Parameters.Add(new SqlParameter("@bln", cbobln.Text));
                            cmd.Parameters.Add(new SqlParameter("@thn", cbothn.Text));


                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "daftargajikaryawan" + ObjSys.GetNow + ".xls";
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
        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(cboCabang.Text,cbobln.Text,cbothn.Text);
        }
        protected void cbobln_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(cboCabang.Text, cbobln.Text, cbothn.Text);

        }
        protected void cbothn_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(cboCabang.Text, cbobln.Text, cbothn.Text);

        }

        protected void Click(object sender, EventArgs e)
        {
            loadData(cboCabang.Text, cbobln.Text, cbothn.Text);

        }

    }
}