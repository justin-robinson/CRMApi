﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;

using CRMApi.Models;

namespace CRMApi.AWS {
    public class DynamoDB {
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

        public async static Task<Document> GetTableItemAsync(string tableName, string id) {
            Table table = GetTable(tableName);
            return await table.GetItemAsync(id);
        }

        public async static Task<List<Document>> GetTableItems(string tableName){
            Table table = DynamoDB.GetTable(tableName);
            ScanFilter notDeleted = new ScanFilter();
            notDeleted.AddCondition("DeleteTime", ScanOperator.Equal, 0);
            Search results = table.Scan(notDeleted);
            List<Document> output = new List<Document>();
            List<Document> set;
            do {
                set = await results.GetNextSetAsync();
                set.ForEach(d =>  output.Add(d) );
            } while (!results.IsDone);

            return output;
        }

        public async static Task<Document> PutTableItemAsync(string tableName, Document document) {
            Table table = GetTable(tableName);
            return await table.PutItemAsync(document);
        }

        public async static Task<Document> DeleteTableItemAsync(string tableName, Document document) {
            Table table = GetTable(tableName);
            return await table.DeleteItemAsync(document);
        }
    }
}
