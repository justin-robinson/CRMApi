using System;

using Amazon.DynamoDBv2.DataModel;

namespace CRMApi.Models {
    [DynamoDBTable("Posts")]
    public class Post {
        [DynamoDBHashKey] public string PostId { get; set; }
        [DynamoDBProperty] public string Content { get; set; }
        [DynamoDBProperty] public DateTime CreateTime { get; set; }
        [DynamoDBProperty] public DateTime UpdateTime { get; set; }
        [DynamoDBProperty] public DateTime DeleteTime { get; set; }
    }
}
