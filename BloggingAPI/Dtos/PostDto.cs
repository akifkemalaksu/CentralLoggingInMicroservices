namespace BloggingAPI.Dtos
{
    public class PostDto
    {
        public int PostId { get; set; }
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
