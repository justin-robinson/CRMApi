using System;
using Amazon.DynamoDBv2.DataModel;

namespace CRMApi.Models {
    [DynamoDBTable("CRM-API-author")]
    public class Author : AbstractModel<Author> {
        [DynamoDBHashKey] public Guid authorId { get; set; }
        [DynamoDBProperty] public string username { get; set; }
        [DynamoDBProperty] public string emailAddress { get; set; }
        [DynamoDBProperty] public string password { get; set; }
        [DynamoDBProperty] public string salt { get; set; }
        [DynamoDBProperty] public string firstName { get; set; }
        [DynamoDBProperty] public string lastName { get; set; }
        [DynamoDBProperty] public Uri avatarUri { get; set; }
        [DynamoDBProperty] public DateTime createTime { get; set; }
        [DynamoDBProperty] public DateTime updateTime { get; set; }

        public Author() { }

        public Author(Guid authorId, string username, string emailAddress, string firstName, string lastName, Uri avatarUri, DateTime createTime, DateTime updateTime) {
            this.authorId = authorId;
            this.username = username;
            this.emailAddress = emailAddress;
            this.firstName = firstName;
            this.lastName = lastName;
            this.avatarUri = avatarUri;
            this.createTime = createTime;
            this.updateTime = updateTime;
        }
    }
}
