using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace CRMApi.TableDefinitions {
    public class Post {
        public static CreateTableRequest TableRequest() {
            return new CreateTableRequest {
                TableName = "CRM-API-post",
                AttributeDefinitions = new List<AttributeDefinition>{
                    new AttributeDefinition {
                        AttributeName = "postId",
                        AttributeType = "S"
                    }
                },
                KeySchema = new List<KeySchemaElement> {
                    new KeySchemaElement {
                        AttributeName = "postId",
                        KeyType = "HASH"
                    }
                },
//                GlobalSecondaryIndexes = new List<GlobalSecondaryIndex> {
//                    new GlobalSecondaryIndex {
//                        IndexName = "authorId-index-createTime-index",
//                        KeySchema = new List<KeySchemaElement> {
//                            new KeySchemaElement {
//                                AttributeName = "authorId",
//                                KeyType = "HASH"
//                            },
//                            new KeySchemaElement {
//                                AttributeName = "createTime",
//                                KeyType = "RANGE"
//                            }
//                        }
//                    }
//                },
                ProvisionedThroughput = new ProvisionedThroughput {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                }
            };
        }
    }
}