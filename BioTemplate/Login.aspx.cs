using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BioHR.Controller.Function;
using UserManagement.Controller.Database;
using UserManagement.Controller.Function;
using UserManagement.Model.Object;

namespace UserManagement
{
    public partial class Login : System.Web.UI.Page
    {
        public UserCatalog DbUser = new UserCatalog();
        private User _objUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            ClearCache();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string hostName = Environment.MachineName + "\\" + Environment.UserName;
            Boolean isError = false;
            var errorMessage = "";
            string nik;

             try {
                // If configuration settings for ActiveDirectoryVerification is set to true then verify user login using Active Directory Username and Password, call respective function and sp_AUTHENTICATION_LOGIN_CTI
                if (ConfigurationManager.AppSettings["ActiveDirectoryVerification"].ToLower() == "true") {
                    if (ActiveDirectoryManager.ValidateUser(tbUsername.Text, tbPassword.Text))
                    {
                        //get IP Address dari PC yang melakukan akses pada aplikasi CTI
                        string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                        //email dapat digunakan untuk melakukan pengambilan data pada database BIOFARMA terkait info karyawan (in this case get NIK)
                        nik = ActiveDirectoryManager.GetUserNumberByEmail(tbUsername.Text + "@biofarma.co.id");
                        _objUser = DbUser.SingleSignOnUserAuthentication(nik, hostName, ipAddress);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Username atau Password anda salah');", true);
                    }
                } else {
                    // else , verified it using stored Procedure sp_AUTHENTICATION_LOGIN, with username = nik and password is value within table USER_PASSWORD
                    _objUser = DbUser.UserAuthentication(tbUsername.Text, tbPassword.Text, hostName, "");                    
                }
             }
             catch (Exception ex)
             {
                 isError = true;
                 errorMessage = ex.Message;
             }

             if ((!isError) && (_objUser != null) && (_objUser.UserId != null && _objUser.UserId != "-1"))
             {
                 Session.Timeout = 60;
                 Session["biofarma_userid"] = _objUser.UserId;
                 Session["biofarma_username"] = _objUser.Nama;
                 Session["biofarma_positionid"] = _objUser.Posid;
                 Session["biofarma_positionname"] = _objUser.PositionName;
                 Session["biofarma_unitcode"] = _objUser.UnitCode;
                 Session["biofarma_unitname"] = _objUser.UnitName;
                 Session["biofarma_roleid"] = _objUser.RoleId;
                 Session["biofarma_rolename"] = _objUser.RoleName;
                 Session["biofarma_grade"] = _objUser.Grade;

                 // store audit trail log for user success login
                 DbUser.SetAuditTrailApplicationLogin(_objUser.UserId, tbUsername.Text, "1");
                 //Server.Transfer("~/Default.aspx");
                 Response.Redirect("Pages/Default.aspx", true);
             }
             else
             {
                 // get user nik from its mail address
                 nik = ActiveDirectoryManager.GetUserNumberByEmail(tbUsername.Text + "@biofarma.co.id");
                 // store audit trail log for user failed login attempt
                 DbUser.SetAuditTrailApplicationLogin(nik ?? "", tbUsername.Text, "2");
                 if (_objUser != null && (string.IsNullOrEmpty(_objUser.UserId) || _objUser.UserId == "-1"))
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Username atau Password anda salah');", true);
                 }
                 else
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('" + errorMessage + "');", true);
                 }
             }
        }

        protected void ClearCache()
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();
        }
    }
}