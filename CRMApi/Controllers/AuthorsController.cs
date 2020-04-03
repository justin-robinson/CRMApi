using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CRMApi.AWS.DynamoDB;
using CRMApi.Models;
using CRMApi.Utils;

namespace CRMApi.Controllers {
    [Route("api/[controller]/")]
    public class AuthorsController : Controller {

        [HttpGet]
        public JsonResult Get() {
            return Json(Author.All.ToList());
        }

        [HttpGet("{authorId}")]
        public JsonResult Get(Guid authorId) {
            return Json(Author.All
                .Where(a => a.authorId == authorId)
                .First());
        }

        [HttpGet("{authorId}/posts")]
        public JsonResult GetPosts(Guid authorId) {
            return Json(Models.Post.All
                .Where(a => a.authorId == authorId)
                .ToList());
        }

        [HttpPost]
        public async Task<Guid> Post(Author author) {
            do {
                author.authorId = Guid.NewGuid();
            } while (Get(author.authorId).Value != null);
            author.salt = Crypto.GenerateRandomCryptographicKey(128);
            var password = new Password(author.password, author.salt);
            author.password = password.Hash();
            author.createTime = DateTime.UtcNow;
            author.updateTime = author.createTime;
            await Client.GetContext().SaveAsync(author);
            return author.authorId;
        }

        [HttpPut("{authorId}")]
        public async Task<Guid> Put(Guid authorId, Author author) {
            author.authorId = authorId;
            author.updateTime = DateTime.UtcNow;
            await Client.GetContext().SaveAsync(author);
            return author.authorId;
        }

        [HttpDelete("{authorId}")]
        public async void Delete(Guid authorId) {
            var context = Client.GetContext();
            var author = Author.All
                .Where(a => a.authorId == authorId)
                .First();
            await context.SaveAsync(author);
        }
    }
}
