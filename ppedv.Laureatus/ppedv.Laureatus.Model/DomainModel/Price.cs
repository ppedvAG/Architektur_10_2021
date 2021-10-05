namespace ppedv.Laureatus.Model
{
    public class Price : Entity
    {
        public int Year { get; set; }
        public virtual ICollection<Laureate> Laureates { get; set; } = new HashSet<Laureate>();
    }
}