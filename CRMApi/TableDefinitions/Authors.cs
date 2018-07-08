using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace CRMApi.TableDefinitions {
    public class Author {
        public static CreateTableRequest TableRequest() {
            return new CreateTableRequest {
                TableName = "Authors",
                AttributeDefinitions = new List<AttributeDefinition>{
                    new AttributeDefinition {
                        AttributeName = "AuthorId",
                        AttributeType = "S"
                    }
                },
                KeySchema = new List<KeySchemaElement> {
                    new KeySchemaElement {
                        AttributeName = "AuthorId",
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