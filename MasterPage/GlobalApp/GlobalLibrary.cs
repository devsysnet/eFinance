using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace eFinance
{
    public class GlobalLibrary
    {
        private Database ObjDb = new Database();

        public Dictionary<string, string> Param = new Dictionary<string, string>();
        public void ExecuteProcedure(string spName = "", Dictionary<string, string> _param = null)
        {
            SqlConnection sqlConnection = new SqlConnection(ObjDb.ConDb());
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 7200;
            if (_param != null)
            {
                foreach (var param in _param)
                {
                    sqlCommand.Parameters.AddWithValue("@" + param.Key, param.Value);
                }
            }
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public DataSet GetDataProcedure(string spName = "", Dictionary<string, string> _param = null)
        {
            DataSet mySet = new DataSet();
            SqlConnection sqlConnection = new SqlConnection(ObjDb.ConDb());
            SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 7200;
            if (_param != null)
            {
                foreach (var param in _param)
                {
                    sqlCommand.Parameters.AddWithValue("@" + param.Key, param.Value);
                }
            }
            SqlDataAdapter data = new SqlDataAdapter(sqlCommand);
            data.Fill(mySet);
            return mySet;
        }

        public DataTable GetDataProcedureDataTable(string spName = "", Dictionary<string, string> _param = null)
        {
            DataTable mySet = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ObjDb.ConDb());
            SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 7200;
            if (_param != null)
            {
                foreach (var param in _param)
                {
                    sqlCommand.Parameters.AddWithValue("@" + param.Key, param.Value);
                }
            }
            SqlDataAdapter data = new SqlDataAdapter(sqlCommand);
            data.Fill(mySet);
            return mySet;
        }
    }
}