using System.Threading.Tasks;
using Babylon.Networking.Brokers;
using Babylon.Networking.Interfaces.Brokers;
using Xunit;

namespace Babylon.Networking.Tests.Brokers
{
    public class FinancialsBrokerTests
    {
        private readonly IFinancialsBroker _financialsBroker;

        public FinancialsBrokerTests()
        {
            _financialsBroker = new FinancialsBroker();
        }

        [Fact]
        public async Task GetYearly_Result_OK()
        {
            // Arrange


            // Act
            var results = await _financialsBroker.GetByTicker("AAPL");

            // Assert
            Assert.NotNull(results);
        }
    }
}
