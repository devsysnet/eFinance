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
    public class Database
    {
        public string ConDb(string con = "")
        {
            string ret = "";
            switch (con)
            {
                case "0":
                default: ret = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
                    break;

            }
            return ret;
        }
        public Dictionary<string, string> Data = new Dictionary<string, string>();
        public Dictionary<string, string> Where = new Dictionary<string, string>();
        public SqlConnection DbConString(string con)
        {
            SqlConnection ret = new SqlConnection(ConDb(con));
            return ret;
        }
        public DataSet GetRows(string sql, SqlConnection DbConString = null)
        {
            if (DbConString == null)
            {
                DbConString = this.DbConString("0");
            }
            DataSet result = new DataSet();
            try
            {
                DbConString.Open();
                SqlDataAdapter data = new SqlDataAdapter(sql, DbConString);
                data.Fill(result);
                data.Dispose();
            }
            catch (Exception)
            {
                result = null;
            }
            finally
            {
                DbConString.Close();
            }
            return result;
        }
        public DataTable GetRowsDataTable(string sql, SqlConnection DbConString = null)
        {
            if (DbConString == null)
            {
                DbConString = this.DbConString("0");
            }
            DataTable result = new DataTable();
            try
            {
                DbConString.Open();
                SqlDataAdapter data = new SqlDataAdapter(sql, DbConString);
                data.Fill(result);
                data.Dispose();
            }
            catch (Exception)
            {
                result = null;
            }
            finally
            {
                DbConString.Close();
            }
            return result;
        }

        public DataSet Select(string table, string column = "*", Dictionary<string, string> _where = null, string order = "", SqlConnection DbConString = null)
        {
            if (DbConString == null)
            {
                DbConString = this.DbConString("0");
            }
            string sql = "select " + column + " from " + table + " where 1=1";
            if (_where != null)
            {
                foreach (var data in _where)
                {
                    sql += " and " + data.Key + "=" + "'" + data.Value + "'";
                }
            }
            if (order != "")
            {
                sql += " order by " + order;
            }
            DataSet result = new DataSet();
            try
            {
                DbConString.Open();
                SqlDataAdapter data = new SqlDataAdapter(sql, DbConString);
                data.Fill(result);
                data.Dispose();
            }
            catch (Exception)
            {
                result = null;
            }
            finally
            {
                DbConString.Close();
            }
            return result;
        }

        public string SelectString(string table, string column = "*", Dictionary<string, string> _where = null, string order = "")
        {
            string sql = "select " + column + " from " + table + " where 1=1";

            if (_where != null)
            {
                foreach (var data in _where)
                {
                    sql += " and " + data.Key + "=" + "'" + data.Value + "'";
                }
            }

            if (order != "")
            {
                sql += " order by " + order;
            }

            return sql;
        }

        public void Update(string table, Dictionary<string, string> _data, Dictionary<string, string> _where = null, SqlConnection dbConString = null)
        {
            ExecQuery(UpdateString(table, _data, _where), dbConString);
        }

        public string UpdateString(string table, Dictionary<string, string> _data, Dictionary<string, string> _where = null)
        {
            string value = "";
            string where = "";

            foreach (var data in _data)
            {
                if (data.Value != null)
                {
                    value += data.Key + "=" + "'" + data.Value + "',";
                }
                else
                {
                    value += data.Key + "=NULL,";
                }
            }

            if (_where != null)
            {
                foreach (var data in _where)
                {
                    where += " and " + data.Key + "=" + "'" + data.Value + "'";
                }
            }

            string sql = "update " + table + " set " + value + " where 1=1 " + where;
            return sql.Replace("', w", "' w");
        }

        public void Delete(string table, Dictionary<string, string> _where, SqlConnection dbConString = null)
        {
            ExecQuery(DeleteString(table, _where), dbConString);
        }

        public string DeleteString(string table, Dictionary<string, string> _where)
        {
            string sql = "delete from " + table + " where 1=1 ";

            foreach (var data in _where)
            {
                sql += " and " + data.Key + "=" + "'" + data.Value + "'";
            }

            return sql;
        }

        public void Insert(string table, Dictionary<string, string> _data, SqlConnection dbConString = null)
        {
            ExecQuery(InsertString(table, _data), dbConString);
        }

        public string InsertString(string table, Dictionary<string, string> _data)
        {
            string sValCol = "";
            string value = "";

            foreach (var data in _data)
            {
                sValCol += data.Key + ",";
                if (data.Value != null)
                {
                    value += "'" + data.Value + "',";
                }
                else
                {
                    value += "NULL,";
                }
            }

            string sql = "insert into " + table + " (" + sValCol + ") values (" + value + "')";
            sql = sql.Replace(",')", ")");
            sql = sql.Replace(",)", ")");

            return sql;
        }

        public void ExecQuery(string sql, SqlConnection dbConString = null)
        {
            if (dbConString == null)
            {
                dbConString = DbConString("0");
            }
            dbConString.Close();
            dbConString.Open();
            SqlCommand cmd = new SqlCommand(sql, dbConString);
            cmd.ExecuteNonQuery();
            dbConString.Close();
        }

        public bool IsExist(string table, string column = "*", Dictionary<string, string> _where = null, SqlConnection dbConString = null)
        {
            bool ret = false;
            string sql = "select " + column + " from " + table + " where 1=1";

            if (_where != null)
            {
                foreach (var data in _where)
                {
                    sql += " and " + data.Key + "=" + "'" + data.Value + "'";
                }
            }

            Result = GetRows(sql, dbConString);

            if (Result.Tables[0].Rows.Count > 0)
            {
                ret = true;
            }

            return ret;
        }

        public Dictionary<string, string> GetData(DataSet dData)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            if (dData.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < dData.Tables[0].Columns.Count; j++)
                {
                    data.Add(dData.Tables[0].Columns[j].ToString(), dData.Tables[0].Rows[0][j].ToString());
                }
            }
            return data;
        }

        public DataSet Result = new DataSet();
        public DataRow Row;
    }
}