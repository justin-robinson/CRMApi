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

        public Post() { }

        public Post(string postId, string content, DateTime createTime, DateTime updateTime, DateTime deleteTime) {
            PostId = postId;
            Content = content;
            CreateTime = createTime;
            UpdateTime = updateTime;
            DeleteTime = deleteTime;
        }
    }
}
