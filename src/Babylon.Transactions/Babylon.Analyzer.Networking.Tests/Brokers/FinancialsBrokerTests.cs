using System.Threading.Tasks;
using Babylon.Transactions.Networking.Brokers;
using Babylon.Transactions.Networking.Interfaces.Brokers;
using Xunit;

namespace Babylon.Analyzer.Networking.Tests.Brokers
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
