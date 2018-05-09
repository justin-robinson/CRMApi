using Amazon.DynamoDBv2.DataModel;

namespace CRMApi.Models {
    [DynamoDBTable("Posts")]
    public class Post {
        [DynamoDBHashKey] public string PostId { get; set; }
        [DynamoDBProperty] public string Content { get; set; }
        [DynamoDBProperty] public long CreateTime { get; set; }
        [DynamoDBProperty] public long UpdateTime { get; set; }
        [DynamoDBProperty] public long DeleteTime { get; set; }
    }
}
