namespace INFOION.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public Article Article { get; set; }
    }
}
