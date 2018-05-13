using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;

namespace CRMApi.AWS.DynamoDB {

    public static class Client {

        public static AmazonDynamoDBClient AmazonDynamoDBClient { get; set; }

        public static void CreateClient() {
            AmazonDynamoDBClient = new AmazonDynamoDBClient();
        }

        public static void CreateClient(string profileName, string regionEndpointName) {
			var chain = new CredentialProfileStoreChain ();
            AWSCredentials crmApiCredentials;
			if (chain.TryGetAWSCredentials(profileName, out crmApiCredentials)) {
                try{
                    AmazonDynamoDBClient = new AmazonDynamoDBClient (crmApiCredentials, RegionEndpoint.GetBySystemName (regionEndpointName));    
                } catch (Exception e) {
                    LambdaLogger.Log($"Error: failed to create a DynamoDB client: {e.Message}");
                }
				
            }
        }

        public static void CreateClient(string serviceURL) {
            try {
                AmazonDynamoDBClient = new AmazonDynamoDBClient(new AmazonDynamoDBConfig {
                    ServiceURL = serviceURL
                });
            } catch (Exception e) {
                LambdaLogger.Log($"Error: failed to create a DynamoDB clien: {e.Message}");
            }
		}

        public static DynamoDBContext GetContext() {
            return new DynamoDBContext(AmazonDynamoDBClient);
        }
    }
}
