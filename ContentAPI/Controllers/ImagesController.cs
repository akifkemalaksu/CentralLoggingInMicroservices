using ContentAPI.Context;
using ContentAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ContentContext _context;

        public ImagesController(ContentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var images = _context.Images.ToList();
            return Ok(images);
        }

        [HttpPost]
        public IActionResult Post(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = image.ImageId }, image);
        }

        [HttpPut]
        public IActionResult Put(Image image)
        {
            _context.Entry(image).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var image = _context.Images.FirstOrDefault(x => x.ImageId == id);

            if (image == null) return NotFound();

            _context.Images.Remove(image);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
