using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BioTemplate.Controller.Database;
using System.Data.SqlClient;
using System.Data;
using BioTemplate.Model.Database;
using BioTemplate.Model.Object;

namespace BioTemplate.Controller.Database
{
    public class OrganizationCatalog
    {
        List<Organization> listOrganization = new List<Organization>();

        public List<Organization> GetOrganizationFromDb()
        {
            SqlConnection conn = DatabaseSql.GetConnection();
            SqlCommand cmd = DatabaseSql.GetCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM bioumum.V_ORGANIZATION_STRUCTURE;";

                SqlDataReader reader = DatabaseSql.GetDataReader(cmd);
                while (reader.Read())
                {
                    Organization m = new Organization();
                    m.Id = Convert.ToInt16(reader["ORGID"]);
                    m.OrganizationName = Convert.ToString(reader["ORGNM"]);
                    //If the Parent ID [PRRID] in database == Null, then it was the first level (root)
                    if (reader["PRTID"] != DBNull.Value)
                    {
                        m.Parent = new Organization();
                        m.Parent.Id = Convert.ToInt16(reader["PRTID"]);
                    }
                    listOrganization.Add(m);
                }
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }
            return listOrganization;
        }
    }
}