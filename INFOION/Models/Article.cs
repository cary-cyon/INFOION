namespace INFOION.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        [ForeignKey("Source")]
        public int SourceId { get; set; }
        public Source Source { get; set; }
        public List<Comment> Comments { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
