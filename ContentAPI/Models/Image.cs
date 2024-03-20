using System.ComponentModel.DataAnnotations.Schema;

namespace ContentAPI.Models
{
    [Table("Images")]
    public class Image
    {
        public int ImageId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
    }
}
