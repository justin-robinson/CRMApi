using Amazon.DynamoDBv2.DocumentModel;
namespace CRMApi.Models {
    public class Post {
        public string PostId { get; set; }
        public string Content { get; set; }
        public long CreateTime { get; set; }
        public long UpdateTime { get; set; }

        public Post() { }

        public Post(Document document) {
            this.Content = document["Content"];
            this.PostId = document["PostId"];
            this.CreateTime = (long)document["CreateTime"];
            this.UpdateTime = (long)document["UpdateTime"];
        }

        public Document ToDocument() {
            Document document = new Document();
            document["PostId"] = this.PostId;
            document["Content"] = this.Content;
            document["CreateTime"] = this.CreateTime;
            document["UpdateTime"] = this.UpdateTime;
            return document;
        }
    }
}
