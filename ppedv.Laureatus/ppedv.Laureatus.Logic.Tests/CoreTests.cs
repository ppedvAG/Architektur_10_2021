using FluentAssertions;
using Moq;
using ppedv.Laureatus.Model;
using ppedv.Laureatus.Model.Contracts;
using System.Linq;
using Xunit;

namespace ppedv.Laureatus.Logic.Tests
{
    public class CoreTests
    {
        [Fact]
        public void GetPersonWithMostWins_empty_db_should_return_null()
        {
            var mock = new Mock<IRepository>();
            var core = new Core(mock.Object);

            var result = core.GetPersonWithMostWins();

            result.Should().BeNull();
        }

        [Fact]
        public void GetPersonWithMostWins_two_persons_fred_wins_MOQ()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.Query<Person>())
                .Returns(() =>
                  {
                      var p1 = new Person() { Name = "Barney" };
                      p1.Laureates.Add(new Laureate());

                      var p2 = new Person() { Name = "Fred" };
                      p2.Laureates.Add(new Laureate());
                      p2.Laureates.Add(new Laureate());

                      return new[] { p1, p2 }.AsQueryable();
                  });
            var core = new Core(mock.Object);

            var result = core.GetPersonWithMostWins();

            result.Name.Should().Be("Fred");
        }

        [Fact]
        public void GetPersonWithMostWins_two_persons_same_amount_of_prices_barney_wins()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.Query<Person>())
                .Returns(() =>
                {
                    var p1 = new Person() { Name = "Barney" };
                    p1.Laureates.Add(new Laureate());
                    p1.Laureates.Add(new Laureate());

                    var p2 = new Person() { Name = "Fred" };
                    p2.Laureates.Add(new Laureate());
                    p2.Laureates.Add(new Laureate());

                    var p3 = new Person() { Name = "Alf" }; //damit orderby statt thenby fehlschlägt
                    p3.Laureates.Add(new Laureate());

                    return new[] { p2, p1, p3 }.AsQueryable(); //zuerst p2, damit firstOrDefault immer fehlschlägt
                });
            var core = new Core(mock.Object);

            var result = core.GetPersonWithMostWins();

            result.Name.Should().Be("Barney");

            mock.Verify(x => x.Query<Person>(), Times.Exactly(1));
            
            mock.Verify(x => x.Save(), Times.Never);
        }

        [Fact]
        public void GetPersonWithMostWins_two_persons_fred_wins()
        {
            var core = new Core(new TestRepo());

            var result = core.GetPersonWithMostWins();

            //Assert.Equal("Fred", result.Name);
            result.Name.Should().Be("Fred");

        }
    }
}