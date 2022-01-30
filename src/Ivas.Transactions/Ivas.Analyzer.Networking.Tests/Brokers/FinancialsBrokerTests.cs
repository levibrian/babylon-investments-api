using System.Threading.Tasks;
using Ivas.Analyzer.Networking.Brokers;
using Ivas.Analyzer.Networking.Interfaces.Brokers;
using Xunit;

namespace Ivas.Analyzer.Networking.Tests.Brokers
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
            var results = await _financialsBroker.GetYearlyByTicker("AAPL");

            // Assert
            Assert.NotNull(results);
        }
    }
}
