namespace INFOION.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public List<Country> Countries { get; set; }
        public override string ToString()
        {
            return Name + " " + LastName;
        }
    }

}
