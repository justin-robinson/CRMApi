using System;
using System.Collections.Generic;

using Amazon.DynamoDBv2.DataModel;

using CRMApi.Models;

namespace CRMApi.AWS.DynamoDB.Linq {
    public static class ServiceHelper {

        internal static ICollection<Post> GetPosts(ICollection<ScanCondition> scanConditions) {
            var posts = new List<Post>();
            var query = Client.GetContext().ScanAsync<Post>(scanConditions);
            while (!query.IsDone) {
                query.GetNextSetAsync().GetAwaiter().GetResult().ForEach(p => posts.Add(p));
            }
            return posts;
        }
    }
}
