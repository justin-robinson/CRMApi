using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

using CRMApi.AWS.DynamoDB;
using CRMApi.AWS.DynamoDB.Linq;
using CRMApi.Models;

namespace CRMApi.Controllers {
    [Route("api/[controller]")]
    public class PostsController : Controller {

        // GET api/posts
        [HttpGet]
        public JsonResult Get() {
            QueryableServerData<Post> posts = new QueryableServerData<Post>();
            var q =
                from p in posts
                where p.DeleteTime == DateTime.MinValue
                select p;
            return Json(q.ToList());
        }

        // GET api/posts/5
        [HttpGet("{postId}")]
        public JsonResult Get(string postId) {
            QueryableServerData<Post> posts = new QueryableServerData<Post>();
            var q =
                (from p in posts
                 where p.PostId == postId
                 select p).Take(1);
            Post post = q.First();
            return Json(post);
        }

        // POST api/posts
        [HttpPost]
        public async Task<string> Post(Post post) {
            post.PostId = Guid.NewGuid().ToString();
            post.CreateTime = DateTime.Now;
            post.UpdateTime = post.CreateTime;
            await Client.GetContext().SaveAsync(post);
            return post.PostId;
        }

        // PUT api/posts/5
        [HttpPut("{postId}")]
        public async Task<string> Put(string postId, Post post) {
            post.PostId = postId;
            post.UpdateTime = DateTime.Now;
            await Client.GetContext().SaveAsync(post);
            return post.PostId;
        }

        // DELETE api/posts/5
        [HttpDelete("{postId}")]
        public async void Delete(string postId) {
            var context = Client.GetContext();
            QueryableServerData<Post> posts = new QueryableServerData<Post>();
            var q =
                (from p in posts
                 where p.PostId == postId
                 select p).Take(1);
            Post post = q.First();
            post.DeleteTime = DateTime.Now;
            await context.SaveAsync(post);
        }
    }
}
