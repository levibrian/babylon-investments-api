using System.Text.Json.Serialization;

namespace Babylon.Transactions.Domain.Abstractions.Brokers
{
    public class FinancialsYearly
    {
        [JsonPropertyName("ticker")]
        public string Ticker { get; set; }

        [JsonPropertyName("period")]
        public string Period { get; set; }

        [JsonPropertyName("calendarDate")]
        public string CalendarDate { get; set; }

        [JsonPropertyName("reportPeriod")]
        public string ReportPeriod { get; set; }

        [JsonPropertyName("updated")]
        public string Updated { get; set; }

        [JsonPropertyName("dateKey")]
        public string DateKey { get; set; }

        [JsonPropertyName("accumulatedOtherComprehensiveIncome")]
        public long AccumulatedOtherComprehensiveIncome { get; set; }

        [JsonPropertyName("assets")]
        public long Assets { get; set; }

        [JsonPropertyName("assetsAverage")]
        public long AssetsAverage { get; set; }

        [JsonPropertyName("assetsCurrent")]
        public long AssetsCurrent { get; set; }

        [JsonPropertyName("assetsNonCurrent")]
        public long AssetsNonCurrent { get; set; }

        [JsonPropertyName("assetTurnover")]
        public double AssetTurnover { get; set; }

        [JsonPropertyName("bookValuePerShare")]
        public double BookValuePerShare { get; set; }

        [JsonPropertyName("capitalExpenditure")]
        public long CapitalExpenditure { get; set; }

        [JsonPropertyName("cashAndEquivalents")]
        public long CashAndEquivalents { get; set; }

        [JsonPropertyName("cashAndEquivalentsUSD")]
        public long CashAndEquivalentsUSD { get; set; }

        [JsonPropertyName("costOfRevenue")]
        public long CostOfRevenue { get; set; }

        [JsonPropertyName("consolidatedIncome")]
        public long ConsolidatedIncome { get; set; }

        [JsonPropertyName("currentRatio")]
        public double CurrentRatio { get; set; }

        [JsonPropertyName("debtToEquityRatio")]
        public double DebtToEquityRatio { get; set; }

        [JsonPropertyName("debt")]
        public long Debt { get; set; }

        [JsonPropertyName("debtCurrent")]
        public long DebtCurrent { get; set; }

        [JsonPropertyName("debtNonCurrent")]
        public long DebtNonCurrent { get; set; }

        [JsonPropertyName("debtUSD")]
        public long DebtUSD { get; set; }

        [JsonPropertyName("deferredRevenue")]
        public long DeferredRevenue { get; set; }

        [JsonPropertyName("depreciationAmortizationAndAccretion")]
        public long DepreciationAmortizationAndAccretion { get; set; }

        [JsonPropertyName("deposits")]
        public long Deposits { get; set; }

        [JsonPropertyName("dividendYield")]
        public double DividendYield { get; set; }

        [JsonPropertyName("dividendsPerBasicCommonShare")]
        public double DividendsPerBasicCommonShare { get; set; }

        [JsonPropertyName("earningBeforelongerestTaxes")]
        public long EarningBeforelongerestTaxes { get; set; }

        [JsonPropertyName("earningsBeforelongerestTaxesDepreciationAmortization")]
        public long EarningsBeforelongerestTaxesDepreciationAmortization { get; set; }

        [JsonPropertyName("EBITDAMargin")]
        public double EBITDAMargin { get; set; }

        [JsonPropertyName("earningsBeforelongerestTaxesDepreciationAmortizationUSD")]
        public long EarningsBeforelongerestTaxesDepreciationAmortizationUSD { get; set; }

        [JsonPropertyName("earningBeforelongerestTaxesUSD")]
        public long EarningBeforelongerestTaxesUSD { get; set; }

        [JsonPropertyName("earningsBeforeTax")]
        public long EarningsBeforeTax { get; set; }

        [JsonPropertyName("earningsPerBasicShare")]
        public double EarningsPerBasicShare { get; set; }

        [JsonPropertyName("earningsPerDilutedShare")]
        public double EarningsPerDilutedShare { get; set; }

        [JsonPropertyName("earningsPerBasicShareUSD")]
        public double EarningsPerBasicShareUSD { get; set; }

        [JsonPropertyName("shareholdersEquity")]
        public long ShareholdersEquity { get; set; }

        [JsonPropertyName("averageEquity")]
        public long AverageEquity { get; set; }

        [JsonPropertyName("shareholdersEquityUSD")]
        public long ShareholdersEquityUSD { get; set; }

        [JsonPropertyName("enterpriseValue")]
        public long EnterpriseValue { get; set; }

        [JsonPropertyName("enterpriseValueOverEBIT")]
        public long EnterpriseValueOverEBIT { get; set; }

        [JsonPropertyName("enterpriseValueOverEBITDA")]
        public double EnterpriseValueOverEBITDA { get; set; }

        [JsonPropertyName("freeCashFlow")]
        public long FreeCashFlow { get; set; }

        [JsonPropertyName("freeCashFlowPerShare")]
        public double FreeCashFlowPerShare { get; set; }

        [JsonPropertyName("foreignCurrencyUSDExchangeRate")]
        public long ForeignCurrencyUSDExchangeRate { get; set; }

        [JsonPropertyName("grossProfit")]
        public long GrossProfit { get; set; }

        [JsonPropertyName("grossMargin")]
        public double GrossMargin { get; set; }

        [JsonPropertyName("goodwillAndlongangibleAssets")]
        public long GoodwillAndlongangibleAssets { get; set; }

        [JsonPropertyName("longerestExpense")]
        public long longerestExpense { get; set; }

        [JsonPropertyName("investedCapital")]
        public long InvestedCapital { get; set; }

        [JsonPropertyName("investedCapitalAverage")]
        public long InvestedCapitalAverage { get; set; }

        [JsonPropertyName("inventory")]
        public long Inventory { get; set; }

        [JsonPropertyName("investments")]
        public long Investments { get; set; }

        [JsonPropertyName("investmentsCurrent")]
        public long InvestmentsCurrent { get; set; }

        [JsonPropertyName("investmentsNonCurrent")]
        public long InvestmentsNonCurrent { get; set; }

        [JsonPropertyName("totalLiabilities")]
        public long TotalLiabilities { get; set; }

        [JsonPropertyName("currentLiabilities")]
        public long CurrentLiabilities { get; set; }

        [JsonPropertyName("liabilitiesNonCurrent")]
        public long LiabilitiesNonCurrent { get; set; }

        [JsonPropertyName("marketCapitalization")]
        public long MarketCapitalization { get; set; }

        [JsonPropertyName("netCashFlow")]
        public long NetCashFlow { get; set; }

        [JsonPropertyName("netCashFlowBusinessAcquisitionsDisposals")]
        public long NetCashFlowBusinessAcquisitionsDisposals { get; set; }

        [JsonPropertyName("issuanceEquityShares")]
        public long IssuanceEquityShares { get; set; }

        [JsonPropertyName("issuanceDebtSecurities")]
        public long IssuanceDebtSecurities { get; set; }

        [JsonPropertyName("paymentDividendsOtherCashDistributions")]
        public long PaymentDividendsOtherCashDistributions { get; set; }

        [JsonPropertyName("netCashFlowFromFinancing")]
        public long NetCashFlowFromFinancing { get; set; }

        [JsonPropertyName("netCashFlowFromInvesting")]
        public long NetCashFlowFromInvesting { get; set; }

        [JsonPropertyName("netCashFlowInvestmentAcquisitionsDisposals")]
        public long NetCashFlowInvestmentAcquisitionsDisposals { get; set; }

        [JsonPropertyName("netCashFlowFromOperations")]
        public long NetCashFlowFromOperations { get; set; }

        [JsonPropertyName("effectOfExchangeRateChangesOnCash")]
        public long EffectOfExchangeRateChangesOnCash { get; set; }

        [JsonPropertyName("netIncome")]
        public long NetIncome { get; set; }

        [JsonPropertyName("netIncomeCommonStock")]
        public long NetIncomeCommonStock { get; set; }

        [JsonPropertyName("netIncomeCommonStockUSD")]
        public long NetIncomeCommonStockUSD { get; set; }

        [JsonPropertyName("netLossIncomeFromDiscontinuedOperations")]
        public long NetLossIncomeFromDiscontinuedOperations { get; set; }

        [JsonPropertyName("netIncomeToNonControllinglongerests")]
        public long NetIncomeToNonControllinglongerests { get; set; }

        [JsonPropertyName("profitMargin")]
        public double ProfitMargin { get; set; }

        [JsonPropertyName("operatingExpenses")]
        public long OperatingExpenses { get; set; }

        [JsonPropertyName("operatingIncome")]
        public long OperatingIncome { get; set; }

        [JsonPropertyName("tradeAndNonTradePayables")]
        public long TradeAndNonTradePayables { get; set; }

        [JsonPropertyName("payoutRatio")]
        public double PayoutRatio { get; set; }

        [JsonPropertyName("priceToBookValue")]
        public double PriceToBookValue { get; set; }

        [JsonPropertyName("priceEarnings")]
        public double PriceEarnings { get; set; }

        [JsonPropertyName("priceToEarningsRatio")]
        public double PriceToEarningsRatio { get; set; }

        [JsonPropertyName("propertyPlantEquipmentNet")]
        public long PropertyPlantEquipmentNet { get; set; }

        [JsonPropertyName("preferredDividendsIncomeStatementImpact")]
        public long PreferredDividendsIncomeStatementImpact { get; set; }

        [JsonPropertyName("sharePriceAdjustedClose")]
        public double SharePriceAdjustedClose { get; set; }

        [JsonPropertyName("priceSales")]
        public double PriceSales { get; set; }

        [JsonPropertyName("priceToSalesRatio")]
        public double PriceToSalesRatio { get; set; }

        [JsonPropertyName("tradeAndNonTradeReceivables")]
        public long TradeAndNonTradeReceivables { get; set; }

        [JsonPropertyName("accumulatedRetainedEarningsDeficit")]
        public long AccumulatedRetainedEarningsDeficit { get; set; }

        [JsonPropertyName("revenues")]
        public long Revenues { get; set; }

        [JsonPropertyName("revenuesUSD")]
        public long RevenuesUSD { get; set; }

        [JsonPropertyName("researchAndDevelopmentExpense")]
        public long ResearchAndDevelopmentExpense { get; set; }

        [JsonPropertyName("returnOnAverageAssets")]
        public double ReturnOnAverageAssets { get; set; }

        [JsonPropertyName("returnOnAverageEquity")]
        public double ReturnOnAverageEquity { get; set; }

        [JsonPropertyName("returnOnInvestedCapital")]
        public double ReturnOnInvestedCapital { get; set; }

        [JsonPropertyName("returnOnSales")]
        public double ReturnOnSales { get; set; }

        [JsonPropertyName("shareBasedCompensation")]
        public long ShareBasedCompensation { get; set; }

        [JsonPropertyName("sellingGeneralAndAdministrativeExpense")]
        public long SellingGeneralAndAdministrativeExpense { get; set; }

        [JsonPropertyName("shareFactor")]
        public long ShareFactor { get; set; }

        [JsonPropertyName("shares")]
        public long Shares { get; set; }

        [JsonPropertyName("weightedAverageShares")]
        public long WeightedAverageShares { get; set; }

        [JsonPropertyName("weightedAverageSharesDiluted")]
        public long WeightedAverageSharesDiluted { get; set; }

        [JsonPropertyName("salesPerShare")]
        public double SalesPerShare { get; set; }

        [JsonPropertyName("tangibleAssetValue")]
        public long TangibleAssetValue { get; set; }

        [JsonPropertyName("taxAssets")]
        public long TaxAssets { get; set; }

        [JsonPropertyName("incomeTaxExpense")]
        public long IncomeTaxExpense { get; set; }

        [JsonPropertyName("taxLiabilities")]
        public long TaxLiabilities { get; set; }

        [JsonPropertyName("tangibleAssetsBookValuePerShare")]
        public double TangibleAssetsBookValuePerShare { get; set; }

        [JsonPropertyName("workingCapital")]
        public long WorkingCapital { get; set; }
    }
}