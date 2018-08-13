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
    public class UserCatalog
    {
        private bool _isError;
        private string _errorMessage;
        private User _user;

        public bool IsError
        {
            get { return _isError; }
            set { _isError = value; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public User UserAuthentication(string username, string password, string hostname, string hostip)
        {
            SqlConnection conn = DatabaseSql.GetConnectionMaster();
            SqlCommand cmd = DatabaseSql.GetCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "EXEC bioumum.sp_AUTHENTICATION_LOGIN @username, @pass, @applicationCode, @hostname, @hostip;";

                cmd.Parameters.Add("@username", SqlDbType.NVarChar, 100);
                cmd.Parameters["@username"].Direction = ParameterDirection.Input;

                cmd.Parameters.Add("@pass", SqlDbType.NVarChar, 100);
                cmd.Parameters["@pass"].Direction = ParameterDirection.Input;

                cmd.Parameters.Add("@applicationCode", SqlDbType.NVarChar, 100);
                cmd.Parameters["@applicationCode"].Direction = ParameterDirection.Input;

                cmd.Parameters.Add("@hostname", SqlDbType.NVarChar, 100);
                cmd.Parameters["@hostname"].Direction = ParameterDirection.Input;

                cmd.Parameters.Add("@hostip", SqlDbType.NVarChar, 100);
                cmd.Parameters["@hostip"].Direction = ParameterDirection.Input;

                cmd.Parameters["@username"].Value = username;
                cmd.Parameters["@pass"].Value = password;
                cmd.Parameters["@applicationCode"].Value = ConfigurationManager.AppSettings["ApplicationCode"];
                cmd.Parameters["@hostname"].Value = hostname;
                cmd.Parameters["@hostip"].Value = hostip;

                SqlDataReader reader = DatabaseSql.GetDataReader(cmd);
                {
                    while (reader.Read())
                    {
                        string nik = Convert.ToString(reader["userid"]);  // PERNR
                        string userName = Convert.ToString(reader["username"]);// CNAME
                        string posid = Convert.ToString(reader["posid"]);   // POSID
                        string posName = Convert.ToString(reader["posname"]); // PRPOS
                        string unitCode = Convert.ToString(reader["unitCode"]);// COCTR
                        string unitName = Convert.ToString(reader["unitname"]);// PRORG
                        string roleId = Convert.ToString(reader["roleid"]);  // ROLID
                        string roleName = Convert.ToString(reader["rolename"]);// ROLNM
                        string grade = Convert.ToString(reader["grade"]);   // PSGRP
                        string email = Convert.ToString(reader["usermail"]);
                        string organizationCode = Convert.ToString(reader["organizationcode"]); //ORGCD

                        _user = new User(nik, posid, userName, posName, unitCode, unitName, roleId, roleName, grade, hostname, "hostip", email, organizationCode);
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = ex.Message;
                throw;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }
            return _user;
        }

        public static Boolean SignatureAuthentication(string username, string password)
        {
            SqlConnection conn = DatabaseSql.GetConnectionMaster();
            SqlCommand cmd = DatabaseSql.GetCommand();
            Boolean result = false;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = @"bioumum.sp_AUTHENTICATION_SIGNATURE";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@pUSRNM", SqlDbType.VarChar, 15).Value = username;
                cmd.Parameters.Add("@pPASWD", SqlDbType.VarChar, 50).Value = password;
                cmd.Parameters.Add("@pUSRDT", SqlDbType.VarChar, 50).Value = "K815";//HttpContext.Current.Session["biofarma_userid"];
                cmd.Parameters.Add("@pRESULT", SqlDbType.Bit).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToBoolean(cmd.Parameters["@pRESULT"].Value);

            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }

            return result;
        }

        public User SingleSignOnUserAuthentication(string personalNumber, string hostname, string hostip)
        {
            SqlConnection conn = DatabaseSql.GetConnectionMaster();
            SqlCommand cmd = DatabaseSql.GetCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "EXEC bioumum.sp_AUTHENTICATION_LOGIN_CTI @userNik, @applicationCode;";

                cmd.Parameters.Add("@userNik", SqlDbType.NVarChar, 100);
                cmd.Parameters["@userNik"].Direction = ParameterDirection.Input;

                cmd.Parameters.Add("@applicationCode", SqlDbType.NVarChar, 100);
                cmd.Parameters["@applicationCode"].Direction = ParameterDirection.Input;

                cmd.Parameters["@userNik"].Value = personalNumber;
                cmd.Parameters["@applicationCode"].Value = ConfigurationManager.AppSettings["ApplicationCode"];

                SqlDataReader reader = DatabaseSql.GetDataReader(cmd);
                {
                    while (reader.Read())
                    {
                        string nik = Convert.ToString(reader["userid"]);  // PERNR
                        string userName = Convert.ToString(reader["username"]);// CNAME
                        string posid = Convert.ToString(reader["posid"]);   // POSID
                        string posName = Convert.ToString(reader["posname"]); // PRPOS
                        string unitCode = Convert.ToString(reader["unitCode"]);// COCTR
                        string unitName = Convert.ToString(reader["unitname"]);// PRORG
                        string roleId = Convert.ToString(reader["roleid"]);  // ROLID
                        string roleName = Convert.ToString(reader["rolename"]);// ROLNM
                        string grade = Convert.ToString(reader["grade"]);   // PSGRP
                        string email = Convert.ToString(reader["usermail"]);
                        string organizationCode = Convert.ToString(reader["organizationcode"]); //ORGCD

                        _user = new User(nik, posid, userName, posName, unitCode, unitName, roleId, roleName, grade, hostname, hostip,email,organizationCode);
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = ex.Message;
                throw;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }
            return _user;
        }

        public void SetAuditTrailApplicationLogin(string personalNumber, string userName, string loginStatus)
        {
            SqlConnection conn = DatabaseSql.GetConnectionMaster();
            SqlCommand cmd = DatabaseSql.GetCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = @"bioumum.usp_Set_AuditTrailApplicationLogin";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@pPERNR", SqlDbType.VarChar, 15).Value = personalNumber;
                cmd.Parameters.Add("@pUSRNM", SqlDbType.VarChar, 30).Value = userName;
                cmd.Parameters.Add("@pAPPCD", SqlDbType.VarChar, 5).Value = ConfigurationManager.AppSettings["ApplicationCode"];
                cmd.Parameters.Add("@pAPPST", SqlDbType.VarChar, 51).Value = loginStatus;


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