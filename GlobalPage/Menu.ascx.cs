using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Web.Configuration;

namespace eFinance.GlobalPage
{
    public partial class Menu : System.Web.UI.UserControl
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string ipaddress = "";
                //ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                //if (ipaddress == "" || ipaddress == null)
                //    ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                //string myIP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
                imgUser.ImageUrl = "../assets/images/user/" + ObjSys.GetUserImage;
                lblNameUser.Text = ObjSys.GetFullName;
                LoadData();
                lblCabang.Text = "Branch   : " + ObjSys.GetCabangName;
                //lblIp.Text = "Your IP : " + ipaddress;
                //SessionTimeOut();
            }

        }
        //protected void SessionTimeOut()
        //{
        //    Session["Reset"] = true;
        //    Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
        //    SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
        //    int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
        //}
        protected void LoadData()
        {
            string StatusMenu = "", StatusMenu2 = "", lBody = "", Link = "";
            lBody += "<ul class='navigation'>";
            sql = "select distinct x.noUser, d.* from ";
            sql += "( ";
            sql += "select a.*, c.noMenu from tAkses a  ";
            sql += "inner join mAkses b on a.noAkses = b.noAkses  ";
            sql += "inner join tHakMenu c on a.noAkses = c.noAkses  ";
            sql += "where a.noUser = '" + ObjSys.GetUserId + "' ";
            sql += ") x inner join menu d on x.noMenu = d.noMenu ";
            sql += "where d.parentMenu = '0' and d.tingkatMenu = '1' or d.tingkatMenu = '2' order by noUrut asc";
            if (ObjDb.GetRows(sql).Tables[0].Rows.Count > 0)
            {
                DataSet mySet = ObjDb.GetRows(sql);
                foreach (DataRow myRow in mySet.Tables[0].Rows)
                {
                    if (myRow["tingkatMenu"].ToString() == "1")
                    {
                        StatusMenu = "";
                        Link = myRow["namaFileMenu"].ToString();
                    }
                    else
                    {
                        StatusMenu = "class='mm-dropdown'";
                        //Link = "javascript:void()";
                        Link = "#";
                    }

                    lBody += "<li  " + StatusMenu + ">";
                    lBody += "<a href='" + Func.BaseUrl + Link + "'><i class='" + myRow["iconMenu"].ToString() + "'></i><span class='mm-text'>" + myRow["namaMenu"].ToString() + "</span></a>";

                    sql = "select distinct x.noUser, d.* from ";
                    sql += "( ";
                    sql += "select a.*, c.noMenu from tAkses a  ";
                    sql += "inner join mAkses b on a.noAkses = b.noAkses  ";
                    sql += "inner join tHakMenu c on a.noAkses = c.noAkses ";
                    sql += "where a.noUser = '" + ObjSys.GetUserId + "' ";
                    sql += ") x inner join menu d on x.noMenu = d.noMenu ";
                    sql += "where d.parentMenu = '" + myRow["noMenu"].ToString() + "' and d.tingkatMenu = '3' order by noUrut asc";

                    if (ObjDb.GetRows(sql).Tables[0].Rows.Count > 0)
                    {
                        lBody += "<ul>";
                        DataSet mySet2 = ObjDb.GetRows(sql);
                        foreach (DataRow myRow2 in mySet2.Tables[0].Rows)
                        {
                            //sql = "select a.GroupId, b.* from MstGroupMenu a left join MstMenu b on a.MenuId = b.MenuId where b.ParentId = '" + myRow2["MenuId"] + "' and b.IsPublic = '4' and GroupId = '" + ObjSys.GetGroup + "' order by b.Sort";
                            sql = "select distinct x.noUser, d.* from ";
                            sql += "( ";
                            sql += "select a.*, c.noMenu from tAkses a  ";
                            sql += "inner join mAkses b on a.noAkses = b.noAkses  ";
                            sql += "inner join tHakMenu c on a.noAkses = c.noAkses ";
                            sql += "where a.noUser = '" + ObjSys.GetUserId + "' ";
                            sql += ") x inner join menu d on x.noMenu = d.noMenu ";
                            sql += "where d.parentMenu = '" + myRow2["noMenu"].ToString() + "' and d.tingkatMenu = '4' order by noUrut asc";

                            DataSet mySet3 = ObjDb.GetRows(sql);
                            if (mySet3.Tables[0].Rows.Count > 0)
                                StatusMenu2 = "class='mm-dropdown'";
                            else
                                StatusMenu2 = "";

                            lBody += "<li  " + StatusMenu2 + ">";
                            lBody += "<a href='" + Func.BaseUrl + myRow2["namaFileMenu"].ToString() + "'><i class='" + myRow2["iconMenu"].ToString() + "'></i><span class='mm-text'>" + myRow2["namaMenu"].ToString() + "</span></a>";
                            if (mySet3.Tables[0].Rows.Count > 0)
                            {
                                lBody += "<ul>";
                                foreach (DataRow myRow3 in mySet3.Tables[0].Rows)
                                {

                                    lBody += "<li>";
                                    lBody += "<a href='" + Func.BaseUrl + myRow3["namaFileMenu"].ToString() + "'><i class='" + myRow3["iconMenu"].ToString() + "'></i><span class='mm-text'>" + myRow3["namaMenu"].ToString() + "</span></a>";
                                    lBody += "</li>";

                                }
                                lBody += "</ul>";
                            }
                            lBody += "</li>";
                        }
                        lBody += "</ul>";

                    }
                    lBody += "</li>";
                }
            }
            lBody += "</ul>";
            lblMenu.Text = lBody;
        }
    }
}