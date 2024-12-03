using Blog_Management_API.Model;
using System.Text.Json;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly Repository _repository;
        public BlogController(Repository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllBlogs()
        {
            var contacts = await _repository.GetAllBlogsAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBlogById(int id)
        {
            var blog = await _repository.GetBlogsByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        [HttpPost]
        public async Task<ActionResult> AddBlog([FromBody] Blogs blog)
        {
            if (blog == null)
            {
                return BadRequest("blog is null");
            }

            await _repository.AddBlogsAsync(blog);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBlogs(int id, [FromBody] Blogs blog)
        {
            await _repository.UpdateBlogsAsync(id, blog);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deleteblog(int id)
        {
            await _repository.DeleteBlogAsync(id);
            return Ok();
        }
    }
}
