using Amazon.DynamoDBv2.DocumentModel;
namespace CRMApi.Models
{
    public class Post
    {
        public string postId { get; set; }
        public string content { get; set; }
        public long createTime { get; set; }
        public long updateTime { get; set; }

        public Post() { }

        public Post(Document document)
        {
            this.content = document["content"];
            this.postId = document["postId"];
            this.createTime = (long)document["createTime"];
            this.updateTime = (long)document["updateTime"];
        }

        public Document ToDocument() {
            Document document = new Document();
            document["postId"] = this.postId;
            document["content"] = this.content;
            document["createTime"] = this.createTime;
            document["updateTime"] = this.updateTime;
            return document;
        }
    }
}
