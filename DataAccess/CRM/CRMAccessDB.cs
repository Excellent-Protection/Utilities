using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.Extensions.Options;

namespace Utilities.DataAccess.CRM
{
    /// <summary>
    /// Summary description for AccessDB
    /// </summary>
    public class CRMAccessDB
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

        public static System.Data.DataSet SelectQ(string queryString)
        {
            //Response.Write(queryString);
            //string connectionString = "server=\'(local)\'; trusted_connection=true; database=\'warehouse\'";
            //
            System.Data.IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.IDbCommand dbCommand = new System.Data.SqlClient.SqlCommand();
            dbCommand.CommandText = queryString;
            dbCommand.Connection = dbConnection;

            System.Data.IDbDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            dataAdapter.SelectCommand = dbCommand;
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataAdapter.Fill(dataSet);

            return dataSet;
        }


    }

}