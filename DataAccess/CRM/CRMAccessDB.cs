using HourlySectorLib.ViewModels.Custom;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
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


        public static System.Data.DataSet SelectQ(System.Data.IDbCommand cmd)
        {
            //Response.Write(queryString);
            //string connectionString = "server=\'(local)\'; trusted_connection=true; database=\'warehouse\'";

            System.Data.IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.IDbCommand dbCommand = cmd;
            // dbCommand.CommandText = queryString;
            dbCommand.Connection = dbConnection;

            System.Data.IDbDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            dataAdapter.SelectCommand = dbCommand;
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataAdapter.Fill(dataSet);

            return dataSet;
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


        public static System.Data.DataSet SelectQ(string queryString)
        {
            //Response.Write(queryString);
            //string connectionString = "server=\'(local)\'; trusted_connection=true; database=\'warehouse\'";
            //

            System.Data.IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.IDbCommand dbCommand = new System.Data.SqlClient.SqlCommand();
            dbCommand.CommandText = queryString;
            dbCommand.Connection = dbConnection;
            dbCommand.CommandTimeout = 10000;

            System.Data.IDbDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            dataAdapter.SelectCommand = dbCommand;
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataAdapter.Fill(dataSet);

            return dataSet;
        }

        public static BulkEntitiesResult UpdateBulkEntiteies<T>(List<T> ListOfEntities) where T : Entity
        {
            List<UpdateRequest> updateRequests = new List<UpdateRequest>();
            var multipleRequest = new ExecuteMultipleRequest()
            {
                // Assign settings that define execution behavior: continue on error, return responses.
                Settings = new ExecuteMultipleSettings()
                {
                    ContinueOnError = true,
                    ReturnResponses = true
                },
                // Create an empty organization request collection.
                Requests = new OrganizationRequestCollection()
            };

            int counts = 0;

            for (int i = 0; i < ListOfEntities.Count; i++)
            {
                updateRequests.Add(new UpdateRequest() { Target = ListOfEntities[i] });
            }
            var lstlstEntity = SplitUpdateList(updateRequests, 1000);
            var BulkEntitiesResultreturn = new BulkEntitiesResult { UpdatedEntityId = new List<string>(), NotUpdatedEntityId = new List<string>() };
            foreach (var lstEntity in lstlstEntity)
            {

                multipleRequest.Requests.Clear();
                //times += "Count=" + counts + "Start:" + DateTime.Now.Minute + ":" + DateTime.Now.Second;

                multipleRequest.Requests.AddRange(lstEntity);
                var _service = CRMService.Service;
                ExecuteMultipleResponse multipleResponse = (ExecuteMultipleResponse)_service.Execute(multipleRequest);
                if (multipleResponse.IsFaulted)
                {
                    //LogError.Error(new Exception(multipleResponse.Responses[0].Fault.Message, new Exception(multipleResponse.Responses[0].Fault.InnerFault.Message)), System.Reflection.MethodBase.GetCurrentMethod().Name, ("ListOfEntities", multipleResponse.Responses.Where(a => a.Fault != null)));
                    var faultedIndex = multipleResponse.Responses.Where(a => a.Response != null).Select(a => a.RequestIndex).ToList();
                    faultedIndex.ForEach(a =>
                    {
                        BulkEntitiesResultreturn.NotUpdatedEntityId.Add(lstEntity[a].RequestId.ToString());
                    });
                }
                else
                {
                    BulkEntitiesResultreturn.UpdatedEntityId.AddRange(lstEntity.Select(a => a.RequestId.ToString()));
                }

            }
            return BulkEntitiesResultreturn;
        }
        public static List<List<UpdateRequest>> SplitUpdateList(List<UpdateRequest> locations, int nSize = 30)
        {
            var list = new List<List<UpdateRequest>>();
            for (int i = 0; i < locations.Count; i += nSize)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
            }
            return list;
        }


        public static int ExecuteNonQuery(string queryString)
        {

            System.Data.IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(connectionString);
            //string queryString = "UPDATE [Customer] SET NAME=@Name,ProjName=@ProjName WHERE ID=@ID";
            System.Data.IDbCommand dbCommand = new System.Data.SqlClient.SqlCommand();
            dbCommand.CommandText = queryString;
            dbCommand.Connection = dbConnection;
            int rowsAffected = 0;
            dbConnection.Open();
            try
            {
                rowsAffected = dbCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new ArgumentException(queryString + e.ToString());
            }
            finally
            {
                dbConnection.Close();
            }

            return rowsAffected;
        }


    }
}


