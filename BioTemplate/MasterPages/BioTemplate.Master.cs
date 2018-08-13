using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BioTemplate.Controller.Function;

namespace BioTemplate.MasterPages
{
    public partial class BioProMaster : System.Web.UI.MasterPage
    {
        MenuGenerator Menu = new MenuGenerator();
        DataView dvApproval = new DataView();
        DataTable dtApproval = new DataTable();
        DataView dvCITO = new DataView();
        DataTable dtCITO = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["biofarma_userid"] != null)
            {
                Menu.GenerateMenu(Session["biofarma_username"].ToString(), Session["biofarma_positionname"].ToString());
                sideMenu.InnerHtml = Menu.ListMenu.ToString();
                //lblName.Text = Session["biofarma_username"].ToString();
                //getNotificationApproval();
                //getNotificationCITO();
            }
            // use to show wheter it have notification.
            // Example of function :
            // toastr.error
            // toastr.success
            // toastr.warning
            // toastr.primary
            // Info is the content of Info itself
            if (!string.IsNullOrEmpty(Session["function"] as string))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Session["function"].ToString(), Session["function"].ToString() + "('" + Session["info"].ToString() + "','Notifikasi');", true);
                Session["function"] = "";
                Session["info"] = "";
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            // use to show wheter it have notification.
            // Example of function :
            // toastr.error
            // toastr.success
            // toastr.warning
            // toastr.info
            // Info is the content of Info itself
            if (!string.IsNullOrEmpty(Session["function"] as string))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Session["function"].ToString(), Session["function"].ToString() + "('" + Session["info"].ToString() + "','Notifikasi');", true);
                Session["function"] = "";
                Session["info"] = "";
            }
        }
        
       
    }
}