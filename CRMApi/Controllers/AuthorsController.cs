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
            return Json(Author.All
                .Where(p => p.DeleteTime > DateTime.UtcNow)
                .ToList());
        }

        [HttpGet("{authorId}")]
        public JsonResult Get(Guid authorId) {
            return Json(Author.All
                .Where(a => a.AuthorId == authorId)
                .First());
        }

        [HttpGet("{authorId}/posts")]
        public JsonResult GetPosts(Guid authorId) {
            return Json(Models.Post.All
                .Where(a => a.AuthorId == authorId)
                .ToList());
        }

        [HttpPost]
        public async Task<Guid> Post(Author author) {
            do {
                author.AuthorId = Guid.NewGuid();
            } while (Get(author.AuthorId).Value != null);
            author.Salt = Crypto.GenerateRandomCryptographicKey(128);
            var password = new Password(author.Password, author.Salt);
            author.Password = password.Hash();
            author.CreateTime = DateTime.UtcNow;
            author.UpdateTime = author.CreateTime;
            author.DeleteTime = DateTime.MaxValue;
            await Client.GetContext().SaveAsync(author);
            return author.AuthorId;
        }

        [HttpPut("{authorId}")]
        public async Task<Guid> Put(Guid authorId, Author author) {
            author.AuthorId = authorId;
            author.UpdateTime = DateTime.UtcNow;
            await Client.GetContext().SaveAsync(author);
            return author.AuthorId;
        }

        [HttpDelete("{authorId}")]
        public async void Delete(Guid authorId) {
            var context = Client.GetContext();
            var author = Author.All
                .Where(a => a.AuthorId == authorId)
                .First();
            author.DeleteTime = DateTime.UtcNow;
            await context.SaveAsync(author);
        }
    }
}
