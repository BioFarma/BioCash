using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Linq;


namespace BioTemplate.Controller.Database
{
    public class MailCatalog : DatabaseSql
    {
        public static void GenerateQueueMailToBeSend(int documentId, string documentCode, int approvalAction, int templateId = 0)
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = GetCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "bioPro.usp_ConstructWorkflowNotificationMail";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 200;

                cmd.Parameters.Add("@pDOCID", SqlDbType.Int);
                cmd.Parameters.Add("@pDOCCD", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@pVALAPP", SqlDbType.Int);
                cmd.Parameters.Add("@pTMPID", SqlDbType.Int);

                cmd.Parameters["@pDOCID"].Value = documentId;
                cmd.Parameters["@pDOCCD"].Value = documentCode;
                cmd.Parameters["@pVALAPP"].Value = approvalAction;
                cmd.Parameters["@pTMPID"].Value = templateId;

                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }

        }

        public static void GenerateQueueMailToBeSend1(int documentId, string documentCode, int approvalAction, int workflowId, int templateId = 0)
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = GetCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "bioPro.usp_ConstructWorkflowNotificationMail";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 200;

                cmd.Parameters.Add("@pDOCID", SqlDbType.Int);
                cmd.Parameters.Add("@pDOCCD", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@pWRFID", SqlDbType.Int);
                cmd.Parameters.Add("@pVALAPP", SqlDbType.Int);
                cmd.Parameters.Add("@pTMPID", SqlDbType.Int);

                cmd.Parameters["@pDOCID"].Value = documentId;
                cmd.Parameters["@pDOCCD"].Value = documentCode;
                cmd.Parameters["@pWRFID"].Value = workflowId;
                cmd.Parameters["@pVALAPP"].Value = approvalAction;
                cmd.Parameters["@pTMPID"].Value = templateId;

                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }

        }
        public static void GenerateQueueMailToBeSendExpired(int documentId,string documentCode)
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = GetCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "bioPro.usp_ConstructExpiredNotificationMail";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 200;

                cmd.Parameters.Add("@pDOCID", SqlDbType.Int);
                cmd.Parameters.Add("@pDOCCD", SqlDbType.VarChar, 30);
                //cmd.Parameters.Add("@pVALAPP", SqlDbType.Int);
                //cmd.Parameters.Add("@pTMPID", SqlDbType.Int);

                cmd.Parameters["@pDOCID"].Value = documentId;
                cmd.Parameters["@pDOCCD"].Value = documentCode;
                //cmd.Parameters["@pVALAPP"].Value = approvalAction;
                //cmd.Parameters["@pTMPID"].Value = templateId;

                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }

        }
        public static DataTable GetQueueMailToBeSend()
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = GetCommand();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dtOvertime = new DataTable();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "bioPro.usp_GetListQueueMail";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 200;

                cmd.Parameters.AddWithValue("@pDOCCD", ConfigurationManager.AppSettings["ApplicationCode"]);

                adapter.SelectCommand = cmd;
                adapter.Fill(dtOvertime);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }
            return dtOvertime;
        }

        public static DataTable GetQueueMailContent()
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = GetCommand();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dtOvertime = new DataTable();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "bioPro.usp_GetListQueueMail";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 200;



                adapter.SelectCommand = cmd;
                adapter.Fill(dtOvertime);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }
            return dtOvertime;
        }

        public static void DequequeMailList(XDocument xmlDataDocument)
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = GetCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "bioPro.usp_SetListQueueMailFlag";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;

                cmd.Parameters.Add("@pQUEUEMAIL", SqlDbType.Xml);

                cmd.Parameters["@pQUEUEMAIL"].Value = xmlDataDocument.ToString();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }

        }
    }
}