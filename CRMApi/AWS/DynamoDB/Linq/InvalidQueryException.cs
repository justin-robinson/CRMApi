using System;
namespace CRMApi.AWS.DynamoDB.Linq {
    class InvalidQueryException : Exception {
        private string message;

        public InvalidQueryException(string message) {
            this.message = message;
        }

        public override string Message => "The client query is invalid: " + message;
    }
}
