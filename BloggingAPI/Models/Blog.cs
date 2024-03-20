using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingAPI.Models
{
    [Table("Blogs")]
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new();
    }
}
