using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CRMApi.AWS.DynamoDB;
using CRMApi.Models;

namespace CRMApi.Controllers {
    [Route("api/[controller]")]
    public class PostsController : Controller {
        private const string TABLE = "crm_posts";
        // GET api/posts
        [HttpGet]
        public async Task<JsonResult> Get() {
            Post post = new Post();
            List<DynamoDBModel> posts = await post.GetAll();
            return Json(posts);
        }

        // GET api/posts/5
        [HttpGet("{postId}")]
        public async Task<JsonResult> Get(string postId) {
            Post post = new Post();
            await post.Get(postId);
            return Json(post);
        }

        // POST api/posts
        [HttpPost]
        public async Task<string> Post(Post post) {
            post.PostId = Guid.NewGuid().ToString();
            post.CreateTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            post.UpdateTime = post.CreateTime;
            await post.Save();
            return post.PostId;
        }

        // PUT api/posts/5
        [HttpPut("{postId}")]
        public async Task<string> Put(int postId, Post post) {
            post.UpdateTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            await post.Save();
            return post.PostId;
        }

        // DELETE api/posts/5
        [HttpDelete("{postId}")]
        public async void Delete(string postId) {
            Post post = new Post();
            await post.Get(postId);
            post.DeleteTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            await post.Save();
        }
    }
}
