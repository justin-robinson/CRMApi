using CRMApi.AWS.DynamoDB.Linq;

namespace CRMApi.Models {
    public abstract class AbstractModel<T> {
        public static QueryableServerData<T> Queryable => new QueryableServerData<T>();
    }
}