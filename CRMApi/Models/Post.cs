using Amazon.DynamoDBv2.DocumentModel;

using CRMApi.AWS;
namespace CRMApi.Models {
    public class Post : DynamoDBModel {
        public string PostId { get; set; }
        public string Content { get; set; }
        public long CreateTime { get; set; }
        public long UpdateTime { get; set; }
        public long DeleteTime { get; set; }

        public Post() : base() {}

        public Post(Document document) : base(document) {}
    }
}
