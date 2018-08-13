using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using BioTemplate.Model.Object;

namespace BioTemplate.Controller.Function
{
    public class OrganizationGenerator
    {
        protected Organization _organization = new Organization();
        protected StringBuilder _listOrganization = new StringBuilder();

        public StringBuilder ListOrganization
        {
            get { return _listOrganization; }
            set { _listOrganization = value; }
        }

        public void GenerateOrganization()
        {
            BioTemplate.Controller.Database.OrganizationCatalog getOrganization = new Controller.Database.OrganizationCatalog();
            IList<Organization> topLevelOrganizations = Controller.Helper.TreeHelper.ConvertToForest(getOrganization.GetOrganizationFromDb());

            foreach (Organization topLevelOrganization in topLevelOrganizations) {
                RenderOrganizationItems(topLevelOrganization);
            }
        }

        public void RenderOrganizationItems(Organization organizationItem)
        {
            string organizationName = organizationItem.OrganizationName.ToString();

            if ((organizationItem.Parent == null) && (organizationItem.Children.Count == 0)) {
                GenerateOrganizationListStructure(organizationName, "1");
            }
            else if (organizationItem.Children.Count > 0)
            {
                GenerateOrganizationListStructure(organizationName, "2");
                foreach (Organization child in organizationItem.Children)
                {
                    if (child.Children.Count > 0) {
                        RenderOrganizationItems(child);
                    }
                    else {
                        GenerateOrganizationListStructure(child.OrganizationName.ToString(), "1");
                    }
                }
			ListOrganization.Append("</ol>");
                ListOrganization.Append("</li>");
            }
        }

        protected void GenerateOrganizationListStructure(string organizationName, string type)
        {
            if (type == "1")
            {
                ListOrganization.Append("<li class = 'dd-item'>");
			ListOrganization.Append("<div class = 'dd-handle'>" + organizationName + "</div>");
                ListOrganization.Append("</li>");
            }
            if (type == "2")
            {
                ListOrganization.Append("<li class = 'dd-item'>");
			ListOrganization.Append("<div class = 'dd-handle'>" + organizationName + "</div>");
			ListOrganization.Append("<ol class = 'dd-list'>");
                /*Generate <li> element for sub organization*/
			//ListOrganization.Append("</ol>");
                //ListOrganization.Append("</li>");
            }
        }
    }
}