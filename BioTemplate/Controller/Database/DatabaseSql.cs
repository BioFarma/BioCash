using System.Configuration;
using System.Data.SqlClient;

namespace BioTemplate.Controller.Database
{
    public class DatabaseSql
    {
        public static string GetDbConnectionStringMaster()
        {
            return ConfigurationManager.ConnectionStrings["BioFarmaConnectionString"].ConnectionString;
        }

        public static string GetDbConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["BioPROConnectionString"].ConnectionString;
        }

        public static string GetDbConnectionStringEproc()
        {
            return ConfigurationManager.ConnectionStrings["EProcurementConnectionString"].ConnectionString;
        }

        public static SqlConnection GetConnectionMaster()
        {
            return new SqlConnection(GetDbConnectionStringMaster());
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(GetDbConnectionString());
        }

        public static SqlConnection GetConnectionEproc()
        {
            return new SqlConnection(GetDbConnectionStringEproc());
        }

        public static SqlCommand GetCommand()
        {
            return new SqlCommand();
        }

        public static SqlCommand GetCommand(SqlConnection con, string sqlCommand)
        {
            return new SqlCommand(sqlCommand, (SqlConnection)con);
        }

        public static SqlDataReader GetDataReader(SqlCommand cmd)
        {
            return cmd.ExecuteReader();
        }

        public static SqlParameter GetParameter(string parameterName, object parameterValue)
        {
            return new SqlParameter(parameterName, parameterValue);
        }
    }
}