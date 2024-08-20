using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance
{
    public partial class Setting : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            DataSet MySet = ObjDb.GetRows("select * from mUser where noUser='" + ObjSys.GetUserId + "'");
            if (MySet.Tables[0].Rows.Count > 0)
            {
                DataRow MyRow = MySet.Tables[0].Rows[0];
                hdnId.Value = MyRow["noUser"].ToString();
                cboCabang.Text = MyRow["noCabang"].ToString();
                txtUserID.Text = MyRow["userID"].ToString();
                txtFullName.Text = MyRow["namaUser"].ToString();
                txtEmail.Text = MyRow["emailUser"].ToString();
                txtNoTelp.Text = MyRow["telp"].ToString();

                imgItem.ImageUrl = "~/assets/images/user/" + MyRow["fotouser"].ToString();
                hdnFileImage.Value = MyRow["fotouser"].ToString();

                if (hdnFileImage.Value == "")
                {
                    imgItem.ImageUrl = "~/assets/images/empty.jpg";
                    flUpload.Enabled = true;
                    cekGnt.Visible = false;
                    lblec.Visible = false;
                }
                else
                {
                    imgItem.ImageUrl = "~/assets/images/user/" + MyRow["fotouser"].ToString();
                    flUpload.Enabled = false;
                    cekGnt.Visible = true;
                    lblec.Visible = true;
                }

                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select '' id,'-------' name union all select noCabang id, '( ' + kdCabang + ' ) ' + namaCabang name from mCabang) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();

                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Data Tidak ada.");
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (chkPassword.Checked == true)
            {
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    message += ObjSys.CreateMessage("Konfirmasi password harus sama dengan password pertama.");
                    valid = false;
                }
            }

            if (valid == true)
            {
                try
                {
                    string fileName = "";
                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noUser", hdnId.Value);
                    if (chkPassword.Checked == true)
                        ObjDb.Data.Add("passwordUser", ObjSys.Sha1(txtPassword.Text));
                    ObjDb.Data.Add("emailUser", txtEmail.Text);
                    ObjDb.Data.Add("telp", txtNoTelp.Text);
                    string fullPath = "";
                    if (cekGnt.Checked == true)
                    {
                        string logo = hdnFileImage.Value;

                        fullPath = Server.MapPath("~/assets/images/user/") + logo;
                        if (System.IO.File.Exists(fullPath))
                            System.IO.File.Delete(fullPath);

                        if (flUpload.HasFile)
                        {
                            ObjDb.Data.Add("fotoUser", flUpload.FileName);
                            fileName = flUpload.FileName;
                            flUpload.SaveAs(Server.MapPath("~/assets/images/user/" + flUpload.FileName));
                        }
                    }
                    else
                    {
                        if (flUpload.HasFile)
                        {
                            ObjDb.Data.Add("fotoUser", flUpload.FileName);
                            fileName = flUpload.FileName;
                            flUpload.SaveAs(Server.MapPath("~/assets/images/user/" + flUpload.FileName));
                        }
                    }


                    ObjDb.Update("mUser", ObjDb.Data, ObjDb.Where);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diubah, silahkan Log Out untuk melihat perubahan!");
                    //Response.Redirect("Setting.aspx");
                    LoadData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }

        protected void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPassword.Checked == true)
            {
                txtPassword.Enabled = true;
                txtConfirmPassword.Enabled = true;
            }
            else
            {
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            LoadData();
            CloseMessage();
        }

        protected void cekGnt_CheckedChanged(object sender, EventArgs e)
        {
            if (cekGnt.Checked == true)
                flUpload.Enabled = true;
            else
                flUpload.Enabled = false;
        }
    }
}