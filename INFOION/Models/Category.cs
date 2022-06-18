namespace INFOION.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Article> Articles { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
