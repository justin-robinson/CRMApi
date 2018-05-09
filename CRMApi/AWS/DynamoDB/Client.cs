using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
namespace CRMApi.AWS.DynamoDB {

    public class Client {

		public static AmazonDynamoDBClient client;

		public static void CreateClient(string profileName, string regionEndpointName) {
			var chain = new CredentialProfileStoreChain ();
            AWSCredentials crmApiCredentials;
			if (chain.TryGetAWSCredentials(profileName, out crmApiCredentials)) {
				client = new AmazonDynamoDBClient(crmApiCredentials, RegionEndpoint.GetBySystemName (regionEndpointName));
            }
        }

		public static void CreateClient(string serviceURL) {
            try {
                client = new AmazonDynamoDBClient(new AmazonDynamoDBConfig {
                    ServiceURL = serviceURL
                });
            } catch (Exception ex) {
                Console.WriteLine("\n Error: failed to create a DynamoDB client; " + ex.Message);
            }
		}
    }
}
