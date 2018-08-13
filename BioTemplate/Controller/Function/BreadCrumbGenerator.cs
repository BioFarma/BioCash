using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using BioTemplate.Model.Object;

namespace BioTemplate.Controller.Function
{
    public class BreadCrumbGenerator
    {
        protected Menu _menu = new Menu();
        protected StringBuilder _listBreadCrumb = new StringBuilder();

        public StringBuilder ListBreadCrumb
        {
            get { return _listBreadCrumb; }
            set { _listBreadCrumb = value; }
        }

        public void GenerateBreadCrumbs(int currentMenuId, string currentMenu)
        {
            BioTemplate.Controller.Database.MenuCatalog getMenu = new Controller.Database.MenuCatalog();
            IList<Menu> listMenu                           = Controller.Helper.TreeHelper.ConvertToForest(getMenu.GetMenuFromDb());
            int i = 1;

            RenderBreadCrumbsRoot();
            foreach (Menu menu in BioTemplate.Controller.Helper.TreeHelper.FromRootToNode(GetCurrentMenu(listMenu,currentMenu, currentMenuId)))
            {
                if ( i != BioTemplate.Controller.Helper.TreeHelper.FromRootToNode(GetCurrentMenu(listMenu,currentMenu, currentMenuId)).Count ) {
                    RenderBreadCrumbs(menu.MenuName, menu.NavUrl.ToString());
                    i++;
                } else {
                    RenderBreadCrumbsLeaf(menu.MenuName);
                }
            }
        }

        public Menu GetCurrentMenu(IList<Menu> listMenu, string currentMenu, int currentMenuId)
        {
            //int currentMenuId = 101;

            //foreach (Menu menu in listMenu)
            //{
            //    if (menu.MenuName == currentMenu)
            //    {
            //        currentMenuId = menu.Id;
            //break;
            //    }
            //}
            return BioTemplate.Controller.Helper.TreeHelper.FindTreeNode(listMenu, currentMenuId);
        }

        public void RenderBreadCrumbsRoot()
        {
            string navUrl = VirtualPathUtility.ToAbsolute("~/Default.aspx");
            ListBreadCrumb.Append("<li><a href = '" + navUrl + "'><i class = 'icon-home'></i> Home </a></li>");
        }

	    public void RenderBreadCrumbs(string menuName, string navUrl)
            {
	        ListBreadCrumb.Append("<li><a href = 'javascript:;'>" + menuName + "</a></li>");
            }

	    public void RenderBreadCrumbsLeaf(string menuName)
	    {
	        ListBreadCrumb.Append("<li class = 'active'>" + menuName + "</li>");
	    }

	

    }
}