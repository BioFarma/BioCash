using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using BioTemplate.Model.Database;
using BioTemplate.Model.Object;

namespace BioTemplate.Controller.Database
{
    public class MenuCatalog
    {
        List<Menu> listMenu = new List<Menu>();

        public List<Menu> GetMenuFromDb()
        {
            string roleId = HttpContext.Current.Session["biofarma_roleid"].ToString();
            string applicationCode = ConfigurationManager.AppSettings["ApplicationCode"];

            SqlConnection conn = DatabaseSql.GetConnectionMaster();
            SqlCommand cmd = DatabaseSql.GetCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "EXEC bioumum.sp_ROLE_MODUL_GET @appid, @roleid;";

                cmd.Parameters.AddWithValue("@roleid", roleId);
                cmd.Parameters.AddWithValue("@appid", applicationCode);

                //cmd.Parameters.Add("@roleid", SqlDbType.NVarChar, 30);
                //cmd.Parameters["@roleid"].Direction = ParameterDirection.Input;
                //cmd.Parameters["@roleid"].Value = "00";

                SqlDataReader reader = DatabaseSql.GetDataReader(cmd);
                while (reader.Read())
                {
                    Menu m = new Menu();
                    m.Id = Convert.ToInt16(reader["MODID"]);
                    m.MenuName = Convert.ToString(reader["MODUL"]);
                    //m.NavUrl  = HttpContext.Current.Server.MapPath(Convert.ToString(reader["NVURL"])); //Converting server path (~) into computer physical path (H:\)
                    m.NavUrl = VirtualPathUtility.ToAbsolute(Convert.ToString(reader["NVURL"])); //Converting server path (~) into URL path (localhost/Default.aspx)
                    m.IconClass = Convert.ToString(reader["ICONM"]);
                    //If the Parent ID [PARID] in database == Null, then it was first level Menu (root)
                    if (reader["PARID"] != DBNull.Value)
                    {
                        m.Parent = new Menu();
                        m.Parent.Id = Convert.ToInt16(reader["PARID"]);
                    }
                    listMenu.Add(m);
                }
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }
            return listMenu;
        }
    }
}