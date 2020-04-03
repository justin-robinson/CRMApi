using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CRMApi.AWS.DynamoDB;
using CRMApi.Models;

namespace CRMApi.Controllers {
    [Route("api/[controller]")]
    public class PostsController : Controller {

        // GET api/posts
        [HttpGet]
        public JsonResult Get() {

            return Json(Models.Post.All.ToList());
        }

        // GET api/posts/5
        [HttpGet("{postId}")]
        public JsonResult Get(Guid postId) {
            return Json(Models.Post.All
                .Where(p => p.postId == postId)
                .First());
        }

        // POST api/posts
        [HttpPost]
        public async Task<Guid> Post(Post post) {
            post.postId = Guid.NewGuid();
            post.createTime = DateTime.UtcNow;
            post.updateTime = post.createTime;
            await Client.GetContext().SaveAsync(post);
            return post.postId;
        }

        // PUT api/posts/5
        [HttpPut("{postId}")]
        public async Task<Guid> Put(Guid postId, Post post) {
            post.postId = postId;
            post.updateTime = DateTime.UtcNow;
            await Client.GetContext().SaveAsync(post);
            return post.postId;
        }

        // DELETE api/posts/5
        [HttpDelete("{postId}")]
        public async void Delete(Guid postId) {
            var context = Client.GetContext();
            var post = Models.Post.All
                .Where(p => p.postId == postId)
                .First();
            await context.SaveAsync(post);
        }
    }
}
