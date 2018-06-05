﻿using System;
using Amazon.DynamoDBv2.DataModel;

namespace CRMApi.Models {
    [DynamoDBTable("Authors")]
    public class Author : AbstractModel<Author> {
        [DynamoDBHashKey] public Guid AuthorId { get; set; }
        [DynamoDBProperty] public string Username { get; set; }
        [DynamoDBProperty] public string EmailAddress { get; set; }
        [DynamoDBProperty] public string FirstName { get; set; }
        [DynamoDBProperty] public string LastName { get; set; }
        [DynamoDBProperty] public DateTime CreateTime { get; set; }
        [DynamoDBProperty] public DateTime UpdateTime { get; set; }
        [DynamoDBProperty] public DateTime DeleteTime { get; set; }

        public Author() { }

        public Author(Guid authorId, string username, string emailAddress, string firstName, string lastName, DateTime createTime, DateTime updateTime, DateTime deleteTime) {
            AuthorId = authorId;
            Username = username;
            EmailAddress = emailAddress;
            FirstName = firstName;
            LastName = lastName;
            CreateTime = createTime;
            UpdateTime = updateTime;
            DeleteTime = deleteTime;
        }
    }
}