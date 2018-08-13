using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using BioTemplate.Controller.Database;
using BioTemplate.Model.Object;

namespace BioTemplate.Model.Database
{
    public class GeneralDataCatalog : DatabaseSql
    {
        public static DataTable GetDropDownList(string type)
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = GetCommand();
            DataTable ds = new DataTable();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = @"bioumum.usp_GetParameterDropDownList";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pPRMTY", type);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }
            return ds;
        }
        public static DataTable GetListBank(string group)
        {
            SqlConnection conn = GetConnectionMaster();
            SqlCommand cmd = GetCommand();
            DataTable ds = new DataTable();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = @"bioumum.usp_GetListBank";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pBANGR", group);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }
            return ds;
        }
        public static DataSet GetHistoryApproval(string supplierIdentity)
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = GetCommand();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = @"bioPro.usp_GetListHistoryCommentApproval";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pSUPID", Convert.ToInt32(supplierIdentity));

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }
            return ds;
        }
        public static DataTable GetAppAuditTrail(string startDate, string endDate)
        {
            SqlConnection conn = GetConnectionMaster();
            SqlCommand cmd = GetCommand();
            DataTable dt = new DataTable();

            

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = @"bioumum.usp_GetAuditTrail";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pBUSID", ConfigurationManager.AppSettings["BussinessId"]);
                cmd.Parameters.AddWithValue("@pAPPCD", ConfigurationManager.AppSettings["ApplicationCode"]);
                cmd.Parameters.AddWithValue("@pSTART", startDate);
                cmd.Parameters.AddWithValue("@pENDDT", endDate);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }
            return dt;
        }
    }
}