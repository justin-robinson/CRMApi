using CRMApi.AWS.DynamoDB.Linq;

namespace CRMApi.Models {
    public abstract class AbstractModel<T> {
        public static QueryableServerData<T> Select () {
            return new QueryableServerData<T>();
        }

        // for yoda style linq queries
        public static QueryableServerData<T> Source => Select();
    }
}