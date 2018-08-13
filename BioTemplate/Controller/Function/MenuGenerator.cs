using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using BioTemplate.Model.Object;

namespace BioTemplate.Controller.Function
{
    public class MenuGenerator
    {
        protected StringBuilder _listMenu = new StringBuilder();

        public StringBuilder ListMenu
        {
            get { return _listMenu; }
            set { _listMenu = value; }
        }

        public void GenerateMenu(string name,string position)
        {
            BioTemplate.Controller.Database.MenuCatalog getMenu = new Controller.Database.MenuCatalog();
            IList<Menu> topLevelMenus = Controller.Helper.TreeHelper.ConvertToForest(getMenu.GetMenuFromDb());
            ListMenu.Append("<ul class='nav metismenu' id='side-menu'>");
                ListMenu.Append("<li class='nav-header'>");
                    ListMenu.Append("<div class='dropdown profile-element'>");
                       ListMenu.Append("<span><img alt='image' class='img-circle' style='max-height: 48px; background-color: floralwhite;' src='" + VirtualPathUtility.ToAbsolute("~/Images/Login/ca-bio-logo-small.png") + "'/></span>");
                           ListMenu.Append("<a data-toggle='dropdown' class='dropdown-toggle' href='#'>");
                               ListMenu.Append("<span class='clear'>");
                                    ListMenu.Append("<span class='block m-t-sm'>");
                                    ListMenu.Append("<asp:Label ID='lblName' runat='server' class='font-bold' Style='color: white;'>" + name + "</asp:Label>");
                                    ListMenu.Append("</span>");
                                    ListMenu.Append("<span class='text-muted m-t-xs block'>"+position+"<b class='caret'></b>");
                                    ListMenu.Append("</span>");
                               ListMenu.Append("</span>");
                           ListMenu.Append("</a>");
                           ListMenu.Append("<ul class='dropdown-menu animated fadeInRight m-t-xs'>");
                                ListMenu.Append("<li><a href='profile.html'>Profile</a></li>");
                                ListMenu.Append("<li><a href='contacts.html'>Contacts</a></li>");
                                ListMenu.Append("<li><a href='mailbox.html'>Mailbox</a></li>");
                                ListMenu.Append("<li class='divider'></li>");
                                ListMenu.Append("<li><a href='login.html'>Logout</a></li>");
                            ListMenu.Append("</ul>");
                    ListMenu.Append("</div>");
                    ListMenu.Append("<div class='logo-element'>BIO+</div>");
                ListMenu.Append("</li>");
            foreach (Menu topLevelMenu in topLevelMenus)
            {
                RenderMenuItems(topLevelMenu);
            }
            ListMenu.Append("</ul>");
        }

        public void RenderMenuItems(Menu menuItem)
        {
            string menuName      = menuItem.MenuName.ToString();
            string navigationUrl = menuItem.NavUrl.ToString();
            string iconClass     = menuItem.IconClass.ToString();

            if ((menuItem.Parent == null) && (menuItem.Children.Count == 0)) {
                GenerateMenuListStructure(menuName, navigationUrl, iconClass, "1");
            }
            else if (menuItem.Children.Count > 0)
            {
                if (menuItem.Parent == null) {
                    GenerateMenuListStructure(menuName, navigationUrl, iconClass, "2");
                }
                else if (menuItem.Parent != null) {
                    GenerateMenuListStructure(menuName, navigationUrl, iconClass, "3");
                }
                foreach (Menu child in menuItem.Children) {
                    if (child.Children.Count > 0) {
                        RenderMenuItemsChild(child);
                    }
                    else {
                        GenerateMenuListStructure(child.MenuName.ToString(), child.NavUrl.ToString(), child.IconClass.ToString(), "4");
                    }
                }
                //ListMenu.Append("</ul>");
                //ListMenu.Append("</li>");
            }
        }

        public void RenderMenuItemsChild(Menu menuItem)
        {
            string menuName = menuItem.MenuName.ToString();
            string navigationUrl = menuItem.NavUrl.ToString();
            string iconClass = menuItem.IconClass.ToString();

            if ((menuItem.Parent == null) && (menuItem.Children.Count == 0))
            {
                GenerateMenuListStructure(menuName, navigationUrl, iconClass, "1");
            }
            else if (menuItem.Children.Count > 0)
            {
                if (menuItem.Parent == null)
                {
                    GenerateMenuListStructure(menuName, navigationUrl, iconClass, "2");
                }
                else if (menuItem.Parent != null)
                {
                    GenerateMenuListStructure(menuName, navigationUrl, iconClass, "3");
                }
                foreach (Menu child in menuItem.Children)
                {
                    if (child.Children.Count > 0)
                    {
                        RenderMenuItems(child);
                    }
                    else
                    {
                        GenerateMenuListStructure(child.MenuName.ToString(), child.NavUrl.ToString(), child.IconClass.ToString(), "4");
                    }
                }
                ListMenu.Append("</ul>");
                ListMenu.Append("</li>");
            }
        }

        protected void GenerateMenuListStructure(string menuName, string navUrl, string navIcon, string type)
        {
            if (type == "1")
            {
                ListMenu.Append("<li>");
			ListMenu.Append("<a href = '" + navUrl + "'>");
			    ListMenu.Append("<i class = '" + navIcon + "'></i>");
			    ListMenu.Append("<span>" + menuName + "</span>");
			ListMenu.Append("</a>");
                ListMenu.Append("</li>");
            }
            if (type == "2")
            {
                ListMenu.Append("<li class='special_link'>");
                ListMenu.Append("<div class='static_li'><span class='nav-label'>" + menuName + "</span></div></li>");
            }
            if (type == "3")
            {
                ListMenu.Append("<li>");
                ListMenu.Append("<a href = '#'>");
                ListMenu.Append("<i class = '" + navIcon + "'></i>");
            ListMenu.Append("<span class='nav-label'>" + menuName + "</span><span class='fa arrow'></span></a>");
            ListMenu.Append("<ul class = 'nav nav-second-level collapse'>");
            }
            if (type == "4")
            {
                ListMenu.Append("<li><a href = '" + navUrl + "'>" + menuName + "</a></li>");
            }
        }
    }
}