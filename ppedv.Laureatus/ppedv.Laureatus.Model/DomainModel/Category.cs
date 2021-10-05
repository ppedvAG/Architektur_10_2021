using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ppedv.Laureatus.Model
{
    public class Category : Entity
    {
        //[Column("DerName")] 
        //[MaxLength(199)]
        public string? Name { get; set; }
        public virtual ICollection<Laureate> Laureates { get; set; } = new HashSet<Laureate>();
    }
}