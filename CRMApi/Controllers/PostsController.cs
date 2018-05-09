using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

using CRMApi.AWS.DynamoDB;
using CRMApi.Models;

namespace CRMApi.Controllers {
    [Route("api/[controller]")]
    public class PostsController : Controller {
        // GET api/posts
        [HttpGet]
        public async Task<JsonResult> Get() {
            LinkedList<Post> posts = new LinkedList<Post>();
            var query = Client.GetContext().ScanAsync<Post>(new List<ScanCondition> {
                new ScanCondition("DeleteTime", ScanOperator.Equal, DateTime.MinValue)
            });
            while (!query.IsDone) {
                (await query.GetNextSetAsync()).ForEach(p => posts.AddLast(p));
            }
            return Json(posts);
        }

        // GET api/posts/5
        [HttpGet("{postId}")]
        public async Task<JsonResult> Get(string postId) {
            Post post = await Client.GetContext().LoadAsync<Post>(postId);
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
            Post post = await context.LoadAsync<Post>(postId);
            post.DeleteTime = DateTime.Now;
            await context.SaveAsync(post);
        }
    }
}
