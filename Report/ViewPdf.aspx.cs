using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace eTrading.Pages
{
    public partial class ViewPdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string fileName = Request["file"].ToString();

                if (!System.IO.Path.GetExtension(fileName).Equals(".pdf"))
                {
                    Label1.Text = "File must be pdf! Please check the attach file format!";
                    return;
                }

                string filePath = Server.MapPath("~/Assets/DocScreening/" + fileName);
                WebClient user = new WebClient();
                Byte[] fileBuffer = user.DownloadData(filePath);
                if (fileBuffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", fileBuffer.Length.ToString());
                    Response.BinaryWrite(fileBuffer);
                }
                else
                {
                    RedirectHome();
                }
            }
            catch (Exception)
            {
                RedirectHome();
            }
        }

        private void RedirectHome()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenWindow", "window.open('/', '_self')", true);
        }
    }
}