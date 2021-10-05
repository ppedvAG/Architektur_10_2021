using Xunit;

namespace ppedv.Laureatus.Logic.Tests
{
    public class CoreTests
    {
        [Fact]
        public void GetPersonWithMostWins_empty_db_should_return_null()
        {
            Assert.True(false, "Todo für morgen aber mit Moq4");
        }

        [Fact]
        public void GetPersonWithMostWins_two_persons_fred_wins()
        {
            var core = new Core(new TestRepo());

            var result = core.GetPersonWithMostWins();

            Assert.Equal("Fred", result.Name);
        }
    }
}