using ContentAPI.Context;
using ContentAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly ContentContext _context;

        public VideosController(ContentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var videos = _context.Videos.ToList();
            return Ok(videos);
        }

        [HttpPost]
        public IActionResult Post(Video video)
        {
            _context.Videos.Add(video);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = video.VideoId }, video);
        }

        [HttpPut]
        public IActionResult Put(Video video)
        {
            _context.Entry(video).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var video = _context.Videos.FirstOrDefault(x => x.VideoId == id);

            if (video == null) return NotFound();

            _context.Videos.Remove(video);
            _context.SaveChanges();

            return NoContent();
        }
    }

}
