namespace ppedv.Laureatus.Model
{
    public class Laureate : Entity
    {
        public virtual Category? Category { get; set; }
        public virtual Price? Price { get; set; }
        public virtual Person? Person { get; set; }
    }
}