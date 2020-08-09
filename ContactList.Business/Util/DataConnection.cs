using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ContactList.Business
{
    /// <summary>
    /// Simple SQL data connection object for queries
    /// </summary>
    public static class DataConnection
    {
        /// <summary>
        /// Executes a query that expects to return a DataSet of values
        /// </summary>
        /// <param name="query">inline string of query</param>
        /// <param name="parameters">List of SqlParameter (can be empty)</param>
        /// <returns>DataSet of return table or empty table if error</returns>
        public static DataTable ExecuteQuery(string query, List<SqlParameter> parameters)
        {
            SqlConnection conn = null;

            try
            {
                DataSet ds = new DataSet();
                conn = new SqlConnection(AppSettings.ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                if (parameters.Any())
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                if(ds.Tables.Count>0)
                {
                    return ds.Tables[0];
                }
            } catch(Exception ex)
            {
                ErrorLog.LogError(ex);
            } finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return new DataTable();
        }

        /// <summary>
        /// Executes a query that expects to return a single integer
        /// </summary>
        /// <param name="query">inline string of query</param>
        /// <param name="parameters">List of SqlParameter (can be empty)</param>
        /// <returns>number; defaults to 0</returns>
        public static int ExecuteScalarInt(string query, List<SqlParameter> parameters)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(AppSettings.ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                if (parameters.Any())
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }
                return (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return 0;
        }

        /// <summary>
        /// Executes a NonQuery
        /// </summary>
        /// <param name="query">inline string of query</param>
        /// <param name="parameters">List of SqlParameter (can be empty)</param>
        /// <returns>Is Successful bool</returns>
        public static bool ExecuteNonQuery(string query, List<SqlParameter> parameters)
        {
            SqlConnection conn = null;

            try
            {
                DataSet ds = new DataSet();
                conn = new SqlConnection(AppSettings.ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                if (parameters.Any())
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            } finally
            {
                if(conn!=null)
                {
                    conn.Close();
                }
            }

            return false;
        }
    }
}
