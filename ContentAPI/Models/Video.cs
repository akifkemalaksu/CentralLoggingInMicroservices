using System.ComponentModel.DataAnnotations.Schema;

namespace ContentAPI.Models
{
    [Table("Videos")]
    public class Video
    {
        public int VideoId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public int Length { get; set; }
        public int Size { get; set; }
    }
}
