using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace CRMApi.TableDefinitions {
    public class Post {
        public static CreateTableRequest TableRequest() {
            return new CreateTableRequest {
                TableName = "Posts",
                AttributeDefinitions = new List<AttributeDefinition>{
                    new AttributeDefinition {
                        AttributeName = "PostId",
                        AttributeType = "S"
                    }
                },
                KeySchema = new List<KeySchemaElement> {
                    new KeySchemaElement {
                        AttributeName = "PostId",
                        KeyType = "HASH"
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                }
            };
        }
    }
}