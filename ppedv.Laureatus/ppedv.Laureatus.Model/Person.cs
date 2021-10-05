namespace ppedv.Laureatus.Model
{
    public class Person : Entity
    {
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }

        public string? Job { get; set; }
        public string? Nationality { get; set; }
        public virtual ICollection<Laureate> Laureates { get; set; } = new HashSet<Laureate>();
    }
}