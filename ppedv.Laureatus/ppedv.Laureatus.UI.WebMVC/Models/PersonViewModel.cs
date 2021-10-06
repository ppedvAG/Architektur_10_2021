using ppedv.Laureatus.Model;

namespace ppedv.Laureatus.UI.WebMVC.Models
{
    public class PersonViewModel
    {
        public Person Person { get; }

        public PersonViewModel(Person person)
        {
            Person = person;
        }

        public string Name { get => Person.Name; set => Person.Name = value; }
        public DateTime GebDatum { get => Person.BirthDate; set => Person.BirthDate = value; }

        public int Alter { get => 99999; }

    }
}
