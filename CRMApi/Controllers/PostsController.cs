using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Amazon.DynamoDBv2.DocumentModel;

using CRMApi.AWS;
using CRMApi.Models;

namespace CRMApi.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        // GET api/posts
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/posts/5
        [HttpGet("{id}")]
        public async Task<string> Get(string id) {
            return await DynamoDB.getTableItemAsync("crm_posts", id);
        }

        // POST api/posts
        [HttpPost]
        public async Task<string> Post(string content) {
            Post p = new Post();
            p.content = content;
            p.postId = Guid.NewGuid().ToString();
            p.createTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            p.updateTime = p.createTime;
            await DynamoDB.putTableItemAsync("crm_posts", p.ToDocument());
            return p.postId;
        }

        // PUT api/posts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/posts/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
