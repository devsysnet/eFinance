using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance
{
    public partial class Home1 : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgHome.ImageUrl = Func.BaseUrl+ "assets/images/home.jpeg" + "?r=" + DateTime.Now.Ticks.ToString();
                DataSet mySet = ObjDb.GetRows("select a.*, b.namaTask, b.linkFile, b.queryAlert, b.param1, b.param2, b.param3 from MstTaskAlert_D a left join MstTaskAlert b on a.noTaskAlert = b.noTaskAlert where a.nouser = '" + ObjSys.GetUserId + "'");
                if (mySet.Tables[0].Rows.Count > 0)
                {
                    string alert = "";
                    foreach (DataRow myRow in mySet.Tables[0].Rows)
                    {
                        int countAlert = 0;
                        if (myRow["queryAlert"].ToString() != "")
                        {
                            string param1 = "", param2 = "";
                            if (myRow["param1"].ToString().ToLower() == "nocabang" && myRow["param2"].ToString().ToLower() == "nouser")
                            {
                                param1 = ObjSys.GetCabangId;
                                param2 = ObjSys.GetUserId;
                            }
                            else if (myRow["param1"].ToString().ToLower() == "nocabang")
                                param1 = ObjSys.GetCabangId;
                            else if (myRow["param2"].ToString().ToLower() == "nouser")
                                param2 = ObjSys.GetUserId;

                            if (myRow["param1"].ToString() != "" && myRow["param2"].ToString() != "")
                            {
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add(myRow["param1"].ToString(), param1);
                                ObjGlobal.Param.Add(myRow["param2"].ToString(), param2);
                                countAlert = ObjGlobal.GetDataProcedure(myRow["queryAlert"].ToString(), ObjGlobal.Param).Tables[0].Rows.Count;
                            }
                            else if (myRow["param1"].ToString() != "" && myRow["param2"].ToString() == "")
                            {
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add(myRow["param1"].ToString(), param1);
                                countAlert = ObjGlobal.GetDataProcedure(myRow["queryAlert"].ToString(), ObjGlobal.Param).Tables[0].Rows.Count;
                            }
                            else if (myRow["param1"].ToString() == "" && myRow["param2"].ToString() != "")
                            {
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add(myRow["param2"].ToString(), param2);
                                countAlert = ObjGlobal.GetDataProcedure(myRow["queryAlert"].ToString(), ObjGlobal.Param).Tables[0].Rows.Count;
                            }
                            else
                            {
                                countAlert = ObjGlobal.GetDataProcedure(myRow["queryAlert"].ToString()).Tables[0].Rows.Count;
                            }
                        }
                        if (countAlert != 0)
                            alert += "<a href='" + Func.BaseUrl + myRow["linkFile"] + "'><li class='list-group-item no-border-hr padding-xs-hr no-bg'>" + myRow["namaTask"] + " <span class='label label-danger pull-right'> " + countAlert + " </span></li></a>";
                        else
                            alert += "<a href='" + Func.BaseUrl + myRow["linkFile"] + "'><li class='list-group-item no-border-hr padding-xs-hr no-bg'>" + myRow["namaTask"] + " <span class='label label-primary pull-right'> " + countAlert + " </span></li></a>";
                    }

                    lblAlertList.Text = alert;
                }

            }
        }
    }
}