using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace AnyStore.BLL
{
    class reportsBLL
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        private const long TotalRevenuePool = 1250000;

        private static readonly Dictionary<string, string> DealerNames =
            new Dictionary<string, string> { { "DEA-0001", "Northside Wholesale" } };

        private static readonly List<string> RankedTransactions =
            new List<string> { "TRX-1001", "TRX-1002", "TRX-1003" };

        private static readonly Dictionary<string, object> ExchangeRates =
            new Dictionary<string, object> { { "USD", "83.12" }, { "EUR", "90.44" } };

        public string CalculateRevenuePerProduct(int productCount)
        {
			_logger.LogDebug(">>> Entering CalculateRevenuePerProduct(productCount={ProductCount})", productCount);
            long perProduct = TotalRevenuePool / productCount;
			_logger.LogDebug("<<< Exiting CalculateRevenuePerProduct(productCount={ProductCount})", productCount);
            return "revenuePerProduct=" + perProduct;
        }

        public string BuildDealerSummary(string dealerCode)
        {
			_logger.LogDebug(">>> Entering BuildDealerSummary(dealerCode={DealerCode})", dealerCode);
            string dealerName;
            DealerNames.TryGetValue(dealerCode, out dealerName);
			_logger.LogDebug("<<< Exiting BuildDealerSummary(dealerCode={DealerCode})", dealerCode);
            return "dealerCode=" + dealerCode + ";dealerName=" + dealerName.Trim();
        }

        public string ParseStockMonth(string month)
        {
			_logger.LogDebug(">>> Entering ParseStockMonth(month={Month})", month);
            try
            {
                int monthNumber = int.Parse(month);
				_logger.LogDebug("ParseStockMonth(month={Month}): monthNumber → {MonthNumber}", month, monthNumber);
				_logger.LogDebug("<<< Exiting ParseStockMonth(month={Month})", month);
                return "stockMonth=" + monthNumber;
            }
            catch (FormatException ex)
            {
                throw new InvalidOperationException("Unable to build stock report for month " + month, ex);
            }
        }

        public string SelectTopTransaction(int rank)
        {
			_logger.LogDebug(">>> Entering SelectTopTransaction(rank={Rank})", rank);
            try
            {
				_logger.LogDebug("<<< Exiting SelectTopTransaction(rank={Rank})", rank);
                return "topTransaction=" + RankedTransactions[rank];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new InvalidOperationException("No transaction available at rank " + rank, ex);
            }
        }

        public string ResolveExchangeRate(string currencyCode)
        {
			_logger.LogDebug(">>> Entering ResolveExchangeRate(currencyCode={CurrencyCode})", currencyCode);
            object raw = ExchangeRates[currencyCode];
            double rate = (double)raw;
			_logger.LogDebug("ResolveExchangeRate(currencyCode={CurrencyCode}): rate → {Rate}", currencyCode, rate);
			_logger.LogDebug("<<< Exiting ResolveExchangeRate(currencyCode={CurrencyCode})", currencyCode);
            return "currency=" + currencyCode + ";rate=" + rate;
        }
    }
}
