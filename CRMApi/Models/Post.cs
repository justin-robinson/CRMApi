using System;
using Amazon.DynamoDBv2.DataModel;

namespace CRMApi.Models {
    [DynamoDBTable("Posts")]
    public class Post : AbstractModel<Post> {
        [DynamoDBHashKey] public Guid PostId { get; set; }
        [DynamoDBProperty] public string Content { get; set; }
        [DynamoDBProperty] public string Title { get; set; }
        [DynamoDBProperty] public DateTime CreateTime { get; set; }
        [DynamoDBProperty] public DateTime UpdateTime { get; set; }
        [DynamoDBProperty] public DateTime DeleteTime { get; set; }

        public Post() { }

        public Post(Guid postId, string content, DateTime createTime, DateTime updateTime, DateTime deleteTime) {
            PostId = postId;
            Content = content;
            CreateTime = createTime;
            UpdateTime = updateTime;
            DeleteTime = deleteTime;
        }
    }
}
