using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Text.RegularExpressions;
using System.Web.Configuration;
public class Func
{
    #region general Function
    public static string BaseUrl
    {
        get
        {
            Uri url = HttpContext.Current.Request.Url;
            string root = url.Scheme + "://" + url.Authority + HttpContext.Current.Request.ApplicationPath;
            if (!root.EndsWith("/"))
                root += "/";

            return root;
        }
    }

    public static string TempFolder
    {
        get
        {
            return HttpContext.Current.Server.MapPath("~/assets/document/");
        }
    }
    #endregion
}