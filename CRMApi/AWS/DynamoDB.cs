using System;
using System.Threading.Tasks;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;

using CRMApi.Models;

namespace CRMApi.AWS
{
    public class DynamoDB
    {
        private static AmazonDynamoDBClient client = null;
        private const String PROFILE_NAME = "crmapi";

        public static AmazonDynamoDBClient GetClient() {
            if (client == null) {
                var chain = new CredentialProfileStoreChain();
                AWSCredentials crmApiCredentials;
                if(chain.TryGetAWSCredentials(PROFILE_NAME, out crmApiCredentials)) {
                    client = new AmazonDynamoDBClient(crmApiCredentials, RegionEndpoint.USEast1);
                }
            }

            return client;
        }

        public static Table GetTable(string tableName) {
            Table table = null;
            try
            {
                table = Table.LoadTable(GetClient(), tableName);
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n Error: failed to load the {tableName} table; " + e.Message);
            }

            return table;
        }

        public async static Task<string> getTableItemAsync(string tableName, string id){
            Table table = GetTable(tableName);
            Document document = await table.GetItemAsync(id);
            return document.ToJson();
        }

        //public async static Task<string> getTableItemsAsync(string tableName){
        //    Table table = DynamoDB.GetTable(tableName);
        //    table.CreateBatchGet
        //}

        public async static Task<Document> putTableItemAsync(string tableName, Document document){
            Table table = GetTable(tableName);
            Document result = await table.PutItemAsync(document);
            return result;
        }
    }
}
