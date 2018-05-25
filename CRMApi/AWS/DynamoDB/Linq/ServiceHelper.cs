using System.Collections.Generic;

using Amazon.DynamoDBv2.DataModel;

namespace CRMApi.AWS.DynamoDB.Linq {
    public static class ServiceHelper {

        internal static ICollection<T> GetItems<T>(ICollection<ScanCondition> scanConditions) {
            var items = new List<T>();
            var query = Client.GetContext().ScanAsync<T>(scanConditions);
            while (!query.IsDone) {
                query.GetNextSetAsync().GetAwaiter().GetResult().ForEach(p => items.Add(p));
            }
            return items;
        }
    }
}
