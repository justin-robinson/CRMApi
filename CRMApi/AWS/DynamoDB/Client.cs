using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
namespace CRMApi.AWS.DynamoDB {

    public static class Client {

		public static AmazonDynamoDBClient client;

		public static void CreateClient(string profileName, string regionEndpointName) {
			var chain = new CredentialProfileStoreChain ();
            AWSCredentials crmApiCredentials;
			if (chain.TryGetAWSCredentials(profileName, out crmApiCredentials)) {
                try{
                    client = new AmazonDynamoDBClient (crmApiCredentials, RegionEndpoint.GetBySystemName (regionEndpointName));    
                } catch (Exception e) {
                    Console.WriteLine ("\n Error: failed to create a DynamoDB client; " + e.Message);
                }
				
            }
        }

		public static void CreateClient(string serviceURL) {
            try {
                client = new AmazonDynamoDBClient(new AmazonDynamoDBConfig {
                    ServiceURL = serviceURL
                });
            } catch (Exception e) {
                Console.WriteLine("\n Error: failed to create a DynamoDB client; " + e.Message);
            }
		}

        public static DynamoDBContext GetContext() {
            return new DynamoDBContext(client);
        }
    }
}
