using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BioTemplate.Controller
{
    public class BasePage : System.Web.UI.Page
    {
        //protected ConfigControl config = new ConfigControl();
        protected Int32 ModulId = -1;
        protected string UserId;
        protected string RoleId;
        protected string UnitJabId;
        protected string UserName;
        protected string RoleName;
        protected string HostName;

        protected string HostIp;

        private void Page_PreInit(object sender, System.EventArgs e)
        {
            dynamic userHostName = Request.UserHostName;
            dynamic userHostAddress = Request.UserHostAddress;
            bool isAuthorized = false;

            if (Session["biofarma_userid"] != null)
            {
                UserId = Convert.ToString(Session["biofarma_userid"]);
                RoleId = Convert.ToString(Session["biofarma_roleid"]);
                UserName = Convert.ToString(Session["biofarma_username"]);
                isAuthorized = true;
                //if (config.checkAuthorization(userId, roleId, modulId, userHostName, userHostAddress) | modulId == 0)
                //{
                //    isAuthorized = true;
                //}
            }

            if (!isAuthorized)
            {
                Response.StatusCode = 201;
                if (Page.IsCallback)
                {
                    Response.RedirectLocation = ResolveUrl("~/Login.aspx");
                    Response.End();
                }
                else
                {
                    Response.Redirect("~/Login.aspx", true);
                }
            }
            else
            {
                //hostAddress
                HostName = userHostName;
                HostIp = userHostAddress;
                Session["biofarma_hostname"] = HostName;
                Session["biofarma_hostip"] = HostIp;
            }
        }

        private void Page_PreRender(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Title) || this.Title.Equals("Untitled Page", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("Page title cannot be \"Untitled Page\" or an empty string.");
            }

        }
        public BasePage()
        {
            PreInit += Page_PreInit;
            PreRender += Page_PreRender;
        }
    }
}