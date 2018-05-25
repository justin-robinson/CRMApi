using System;
using System.Collections.Generic;

using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

using CRMApi.Models;

namespace CRMApi.AWS.DynamoDB.Linq {
    public static class ServiceHelper {
        internal static Post[] GetPostsByPostId(List<Guid> postIds) {
            var scanConditions = new LinkedList<ScanCondition>();
            postIds.ForEach(postId => {
                scanConditions.AddLast(new ScanCondition("PostId", ScanOperator.Equal, postId));
            });
            return GetPosts(scanConditions).ToArray();
        }

        internal static Post[] GetPosts() {
            var scanConditions = new LinkedList<ScanCondition>();
            scanConditions.AddLast(new ScanCondition("DeleteTime", ScanOperator.Equal, DateTime.MinValue));
            return GetPosts(scanConditions).ToArray();
        }

        private static List<Post> GetPosts(IEnumerable<ScanCondition> scanConditions) {
            var posts = new List<Post>();
            var query = Client.GetContext().ScanAsync<Post>(scanConditions);
            while (!query.IsDone) {
                query.GetNextSetAsync().GetAwaiter().GetResult().ForEach(p => posts.Add(p));
            }
            return posts;
        }
    }
}
