using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.DataAccess.CRM
{
  public  class CRMAccessDB
    {

        public CRMAccessDB()
        {

        }
        public static string connectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            }


        }
        public static DataSet SelectQ(string sql, List<Tuple<string, SqlDbType, object>> parameters)
        {
            SqlCommand cmd = new SqlCommand(sql);
            foreach (Tuple<string, SqlDbType, object> item in parameters)
            {
                //cmd.Parameters.AddWithValue(item.Item1, item.Item3.ToString());
                cmd.Parameters.Add(item.Item1, item.Item2);
                cmd.Parameters[item.Item1].Value = item.Item3;
            }
            cmd.Connection = new SqlConnection(connectionString);

            System.Data.IDbDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataAdapter.Fill(dataSet);
            return dataSet;

        }
    }
}
