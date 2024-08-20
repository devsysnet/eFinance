using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace eFinance.GlobalApp
{
    public static class ViewHelper
    {
        public static void DynamicDataBind(GridView grdView, DataSet ds)
        {
            // Clear and add initial columns
            grdView.Columns.Clear();

            DataTable headerTable = ds.Tables[0];
            // Add new columns
            foreach (DataColumn column in headerTable.Columns)
            {
                BoundField bfield = new BoundField();
                bfield.HeaderText = column.ColumnName;
                bfield.DataField = column.ColumnName;
                grdView.Columns.Add(bfield);
            }

            grdView.DataSource = ds;
            grdView.DataBind();
        }

        public static void DownloadExcel(HttpResponse Response, String fileName, DataTable dt)
        {
            string attachment = "attachment; filename=" + fileName;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
    }
}