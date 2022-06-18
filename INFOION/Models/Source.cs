namespace INFOION.Models
{
    public class Source
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
    
}
