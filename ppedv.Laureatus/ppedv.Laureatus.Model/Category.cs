namespace ppedv.Laureatus.Model
{
    public class Category : Entity
    {
        public string? Name { get; set; }
        public virtual ICollection<Laureate> Laureates { get; set; } = new HashSet<Laureate>();
    }
}