using System;
using Amazon.DynamoDBv2.DataModel;

namespace CRMApi.Models {
    [DynamoDBTable("CRM-API-post")]
    public class Post : AbstractModel<Post> {
        [DynamoDBHashKey] public Guid postId { get; set; }
        [DynamoDBProperty] public Guid authorId { get; set; }
        [DynamoDBProperty] public string content { get; set; }
        [DynamoDBProperty] public string title { get; set; }
        [DynamoDBProperty] public Uri heroImageUri{ get; set; }
        [DynamoDBProperty] public DateTime createTime { get; set; }
        [DynamoDBProperty] public DateTime updateTime { get; set; }

        public Post() { }

        public Post(Guid postId, Guid authorId, string content, string title, Uri heroImageUri, DateTime createTime, DateTime updateTime) {
            this.postId = postId;
            this.authorId = authorId;
            this.content = content;
            this.title = title;
            this.heroImageUri = heroImageUri;
            this.createTime = createTime;
            this.updateTime = updateTime;
        }
    }
}
