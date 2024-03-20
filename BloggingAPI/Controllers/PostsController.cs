using BloggingAPI.Context;
using BloggingAPI.Dtos;
using BloggingAPI.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace BloggingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly BloggingContext _context;

        public PostsController(BloggingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var posts = _context.Posts.ToList();
            return Ok(posts);
        }

        [HttpPost]
        public IActionResult Post(PostDto post)
        {
            var newPost = post.Adapt<Post>();
            _context.Posts.Add(newPost);
            _context.SaveChanges();

            return Ok(newPost);
        }

        [HttpPut]
        public IActionResult Put(PostDto post)
        {
            var updatedPost = post.Adapt<Post>();
            _context.Entry(updatedPost).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok(updatedPost);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var post = _context.Posts.FirstOrDefault(x => x.PostId == id);
            if (post != null) return NotFound();
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
