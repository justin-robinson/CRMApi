using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Amazon.DynamoDBv2.DocumentModel;

using CRMApi.AWS;
using CRMApi.Models;

namespace CRMApi.Controllers {
    [Route("api/[controller]")]
    public class PostsController : Controller {
        private const string TABLE = "crm_posts";
        // GET api/posts
        [HttpGet]
        public async Task<JsonResult> Get() {
            List<Post> posts = new List<Post>();
            (await DynamoDBHelper.GetTableItems(TABLE)).ForEach(d => posts.Add(new Post(d)));
            return Json(posts);
        }

        // GET api/posts/5
        [HttpGet("{postId}")]
        public async Task<JsonResult> Get(string postId) {
            return Json(new Post((await DynamoDBHelper.GetTableItemAsync(TABLE, postId))));
        }

        // POST api/posts
        [HttpPost]
        public async Task<string> Post(Post post) {
            post.PostId = Guid.NewGuid().ToString();
            post.CreateTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            post.UpdateTime = post.CreateTime;
            await DynamoDBHelper.PutTableItemAsync(TABLE, post.ToDocument());
            return post.PostId;
        }

        // PUT api/posts/5
        [HttpPut("{postId}")]
        public async Task<string> Put(int postId, Post post) {
            post.UpdateTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            await DynamoDBHelper.PutTableItemAsync(TABLE, post.ToDocument());
            return post.PostId;
        }

        // DELETE api/posts/5
        [HttpDelete("{postId}")]
        public async void Delete(string postId) {
            Post post = new Post(await DynamoDBHelper.GetTableItemAsync(TABLE, postId));
            post.DeleteTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            await DynamoDBHelper.PutTableItemAsync(TABLE, post.ToDocument());
        }
    }
}
