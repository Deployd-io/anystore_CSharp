using System;
using System.Collections.Generic;

namespace AnyStore.BLL
{
    class reportsBLL
    {
        private const long TotalRevenuePool = 1250000;

        private static readonly Dictionary<string, string> DealerNames =
            new Dictionary<string, string> { { "DEA-0001", "Northside Wholesale" } };

        private static readonly List<string> RankedTransactions =
            new List<string> { "TRX-1001", "TRX-1002", "TRX-1003" };

        private static readonly Dictionary<string, object> ExchangeRates =
            new Dictionary<string, object> { { "USD", "83.12" }, { "EUR", "90.44" } };

        public string CalculateRevenuePerProduct(int productCount)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering CalculateRevenuePerProduct(productCount={0})", productCount));
            long perProduct = TotalRevenuePool / productCount;
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting CalculateRevenuePerProduct(productCount={0})", productCount));
            return "revenuePerProduct=" + perProduct;
        }

        public string BuildDealerSummary(string dealerCode)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering BuildDealerSummary(dealerCode={0})", dealerCode));
            string dealerName;
            DealerNames.TryGetValue(dealerCode, out dealerName);
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting BuildDealerSummary(dealerCode={0})", dealerCode));
            return "dealerCode=" + dealerCode + ";dealerName=" + dealerName.Trim();
        }

        public string ParseStockMonth(string month)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering ParseStockMonth(month={0})", month));
            try
            {
                int monthNumber = int.Parse(month);
				System.Diagnostics.Trace.WriteLine(string.Format("ParseStockMonth(month={0}): monthNumber → {1}", month, monthNumber));
				System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting ParseStockMonth(month={0})", month));
                return "stockMonth=" + monthNumber;
            }
            catch (FormatException ex)
            {
                throw new InvalidOperationException("Unable to build stock report for month " + month, ex);
            }
        }

        public string SelectTopTransaction(int rank)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering SelectTopTransaction(rank={0})", rank));
            try
            {
				System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting SelectTopTransaction(rank={0})", rank));
                return "topTransaction=" + RankedTransactions[rank];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new InvalidOperationException("No transaction available at rank " + rank, ex);
            }
        }

        public string ResolveExchangeRate(string currencyCode)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering ResolveExchangeRate(currencyCode={0})", currencyCode));
            object raw = ExchangeRates[currencyCode];
            double rate = (double)raw;
			System.Diagnostics.Trace.WriteLine(string.Format("ResolveExchangeRate(currencyCode={0}): rate → {1}", currencyCode, rate));
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting ResolveExchangeRate(currencyCode={0})", currencyCode));
            return "currency=" + currencyCode + ";rate=" + rate;
        }
    }
}
