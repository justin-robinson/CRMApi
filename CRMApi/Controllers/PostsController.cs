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
            var posts = new QueryableServerData<Post>();
            var q =
                from p in posts
                where p.DeleteTime > DateTime.Now
                select p;
            return Json(q.ToList());
        }

        // GET api/posts/5
        [HttpGet("{postId}")]
        public JsonResult Get(Guid postId) {
            var posts = new QueryableServerData<Post>();
            var q =
                (from p in posts
                 where p.PostId == postId
                 select p).Take(1);
            var post = q.First();
            return Json(post);
        }

        // POST api/posts
        [HttpPost]
        public async Task<Guid> Post(Post post) {
            post.PostId = Guid.NewGuid();
            post.CreateTime = DateTime.Now;
            post.UpdateTime = post.CreateTime;
            post.DeleteTime = DateTime.MaxValue;
            await Client.GetContext().SaveAsync(post);
            return post.PostId;
        }

        // PUT api/posts/5
        [HttpPut("{postId}")]
        public async Task<Guid> Put(Guid postId, Post post) {
            post.PostId = postId;
            post.UpdateTime = DateTime.Now;
            await Client.GetContext().SaveAsync(post);
            return post.PostId;
        }

        // DELETE api/posts/5
        [HttpDelete("{postId}")]
        public async void Delete(Guid postId) {
            var context = Client.GetContext();
            var posts = new QueryableServerData<Post>();
            var q =
                (from p in posts
                 where p.PostId == postId
                 select p).Take(1);
            var post = q.First();
            post.DeleteTime = DateTime.Now;
            await context.SaveAsync(post);
        }
    }
}
