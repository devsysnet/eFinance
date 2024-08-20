using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.Configuration;

namespace eFinance.Report
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        ReportDocument crystalReport = new ReportDocument();
        protected void Page_Init(object sender, EventArgs e)
        {

            string reportName = "";
            if (Session["REPORTTITLE"] != null)
                reportName = Session["REPORTTITLE"].ToString();
            else
            {

                reportName = Session["REPORTNAME"].ToString();
                reportName = reportName.Remove(0, 3);
                reportName = reportName.Substring(0, reportName.Length - 4);
            }
            reportName = reportName.Replace(' ', '_') + "_" + DateTime.Now.ToString("dMyyyy");
            CRViewerTras.ID = reportName;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (HttpContext.Current.Session["ParamReport"] != null)
            {
                Dictionary<string, string> Param = HttpContext.Current.Session["ParamReport"] as Dictionary<string, string>;

                string reportName = Session["REPORTNAME"].ToString();
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(Server.MapPath("~/App_Code") + "/dsReport.xsd");
                ds.EnforceConstraints = false;
                crystalReport.Load(Server.MapPath(reportName));
                for (int i = 0; i < crystalReport.Database.Tables.Count; i++)
                {
                    DataTable dt = ObjGlobal.GetDataProcedureDataTable(crystalReport.Database.Tables[i].Name, Param);
                    //VerifyDatabase For Report
                    ds.Tables[crystalReport.Database.Tables[i].Name].Merge(dt, true, MissingSchemaAction.Ignore);
                    crystalReport.SetDataSource(ds);
                    crystalReport.Refresh();
                    crystalReport.VerifyDatabase();
                    //crystalReport.PrintToPrinter(1, true, 0, 0);
                    //crystalReport.PrintOptions.PaperSize = PaperSize.PaperA4

                    //UNTUK VERIFY SUB REPORT
                    SubreportObject subreport = null;
                    foreach (Section section in crystalReport.ReportDefinition.Sections)
                    {
                        foreach (object item in section.ReportObjects)
                        {
                            subreport = item as SubreportObject;
                            if (subreport != null)
                            {


                                dt = new DataTable();
                                String tblname = ds.Tables[crystalReport.Subreports[subreport.SubreportName].Database.Tables[0].Name].TableName;
                                dt.TableName = subreport.Name;
                                dt = ObjGlobal.GetDataProcedureDataTable(tblname);
                                ds.Tables[tblname].Merge(dt);
                                crystalReport.Subreports[subreport.SubreportName].SetDataSource(ds);
                                crystalReport.Subreports[subreport.SubreportName].VerifyDatabase();
                            }
                        }
                    }
                }



                CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinitions crParameterdef;
                crParameterdef = crystalReport.DataDefinition.ParameterFields;
                foreach (var _param in Param)
                {
                    crystalReport.SetParameterValue(_param.Key, _param.Value);
                }
                Session["rptDoc"] = crystalReport;
                CRViewerTras.ReportSource = crystalReport;
            }
            else
            {
                string reportName = Session["REPORTNAME"].ToString();
                crystalReport.Load(Server.MapPath(reportName));
                CRViewerTras.ReportSource = crystalReport;
            }

            //crystalReport.Close();
            //crystalReport.Dispose();
            //GC.Collect();
        }

        protected override void OnUnload(EventArgs e)
        {
            if (crystalReport != null)
            {
                crystalReport.Close();
                crystalReport.Dispose();
            }

            base.OnUnload(e);
        }
    }
}