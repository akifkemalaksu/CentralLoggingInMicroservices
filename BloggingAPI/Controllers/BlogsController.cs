using BloggingAPI.Context;
using BloggingAPI.Dtos;
using BloggingAPI.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloggingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BloggingContext _context;

        public BlogsController(BloggingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var blogs = _context.Blogs.ToList();
            return Ok(blogs);
        }

        [HttpPost]
        public IActionResult Post(BlogDto blog)
        {
            var newBlog = blog.Adapt<Blog>();
            _context.Blogs.Add(newBlog);
            _context.SaveChanges();

            return Ok(newBlog);
        }

        [HttpPut]
        public IActionResult Put(BlogDto blog)
        {
            var updatedBlog = blog.Adapt<Blog>();
            _context.Entry(updatedBlog).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok(updatedBlog);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog == null) return NotFound();

            _context.Blogs.Remove(blog);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
