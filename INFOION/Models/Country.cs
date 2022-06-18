namespace INFOION.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<Publisher> Publishers { get; set; }
    }
}
