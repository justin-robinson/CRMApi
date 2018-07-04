using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace CRMApi.TableDefinitions {
    public class Post {
        private const string TABLE = "Posts";
        private const string HASH_KEY = "PostId";
        public static CreateTableRequest TableRequest() {
            return new CreateTableRequest {
                TableName = TABLE,
                AttributeDefinitions = new List<AttributeDefinition>{
                    new AttributeDefinition {
                        AttributeName = HASH_KEY,
                        AttributeType = "S"
                    },
                    new AttributeDefinition {
                        AttributeName = "AuthorId",
                        AttributeType = "S"
                    },
                    new AttributeDefinition {
                        AttributeName = "Content",
                        AttributeType = "S"
                    },
                    new AttributeDefinition {
                        AttributeName = "Title",
                        AttributeType = "S"
                    },
                    new AttributeDefinition {
                        AttributeName = "CreateTime",
                        AttributeType = "S"
                    },
                    new AttributeDefinition {
                        AttributeName = "UpdateTime",
                        AttributeType = "S"
                    },
                    new AttributeDefinition {
                        AttributeName = "DeleteTime",
                        AttributeType = "S"
                    }
                },
                KeySchema = new List<KeySchemaElement> {
                    new KeySchemaElement {
                        AttributeName = HASH_KEY,
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