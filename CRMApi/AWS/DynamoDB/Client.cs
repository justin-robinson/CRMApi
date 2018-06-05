using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using Amazon.Runtime.CredentialManagement;

namespace CRMApi.AWS.DynamoDB {

    public static class Client {
        private static AmazonDynamoDBClient Instance { get; set; }

        public static void CreateClient() {
            Instance = new AmazonDynamoDBClient();
        }

        public static void CreateClient(string profileName, string regionEndpointName) {
			var chain = new CredentialProfileStoreChain ();
            if (!chain.TryGetAWSCredentials(profileName, out var crmApiCredentials)) return;
            try{
                Instance = new AmazonDynamoDBClient (crmApiCredentials, RegionEndpoint.GetBySystemName (regionEndpointName));
            } catch (Exception e) {
                LambdaLogger.Log($"Error: failed to create a DynamoDB client: {e.Message}");
            }
        }

        public static void CreateClient(string serviceUrl) {
            try {
                Instance = new AmazonDynamoDBClient(new AmazonDynamoDBConfig {
                    ServiceURL = serviceUrl
                });
            } catch (Exception e) {
                LambdaLogger.Log($"Error: failed to create a DynamoDB client: {e.Message}");
            }
		}

        public static DynamoDBContext GetContext() {
            return new DynamoDBContext(Instance);
        }
    }
}
