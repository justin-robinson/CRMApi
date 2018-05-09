using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;

namespace CRMApi.Models {
    [DynamoDBTable(TABLE)]
    public class Post {
        public const string TABLE = "Posts";
        public const string HASH_KEY = "PostId";
        [DynamoDBHashKey] public string PostId { get; set; }
        [DynamoDBProperty] public string Content { get; set; }
        [DynamoDBProperty] public DateTime CreateTime { get; set; }
        [DynamoDBProperty] public DateTime UpdateTime { get; set; }
        [DynamoDBProperty] public DateTime DeleteTime { get; set; }

        public static CreateTableRequest TableRequest() {
            return new CreateTableRequest {
                TableName = TABLE,
                AttributeDefinitions = new List<AttributeDefinition>(){
                    new AttributeDefinition {
                        AttributeName = HASH_KEY,
                        AttributeType = "S"
                    }
                },
                KeySchema = new List<KeySchemaElement>() {
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
