using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace CRMApi.TableDefinitions {
    public class Author {
        private const string TABLE = "Authors";
        private const string HASH_KEY = "AuthorId";
        public static CreateTableRequest TableRequest() {
            return new CreateTableRequest {
                TableName = TABLE,
                AttributeDefinitions = new List<AttributeDefinition>{
                    new AttributeDefinition {
                        AttributeName = HASH_KEY,
                        AttributeType = "S"
                    },
                    new AttributeDefinition {
                        AttributeName = "EmailAddress",
                        AttributeType = "S"
                    },
                    new AttributeDefinition {
                        AttributeName = "Password",
                        AttributeType = "S"
                    },
                    new AttributeDefinition {
                        AttributeName = "Salt",
                        AttributeType = "S"
                    },
                    new AttributeDefinition {
                        AttributeName = "FirstName",
                        AttributeType = "S"
                    },
                    new AttributeDefinition {
                        AttributeName = "LastName",
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