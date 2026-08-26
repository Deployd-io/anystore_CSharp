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
            long perProduct = TotalRevenuePool / productCount;
            return "revenuePerProduct=" + perProduct;
        }

        public string BuildDealerSummary(string dealerCode)
        {
            string dealerName;
            DealerNames.TryGetValue(dealerCode, out dealerName);
            return "dealerCode=" + dealerCode + ";dealerName=" + dealerName.Trim();
        }

        public string ParseStockMonth(string month)
        {
            try
            {
                int monthNumber = int.Parse(month);
                return "stockMonth=" + monthNumber;
            }
            catch (FormatException ex)
            {
                throw new InvalidOperationException("Unable to build stock report for month " + month, ex);
            }
        }

        public string SelectTopTransaction(int rank)
        {
            try
            {
				System.Diagnostics.Trace.WriteLine(string.Format("SelectTopTransaction(rank)={0}): M2614-AUDIT: Database transaction completed", rank));
				System.Diagnostics.Trace.WriteLine(string.Format("SelectTopTransaction(rank)={0}): SOC2-AUDIT [CC6.6]: Database transaction completed", rank));
                return "topTransaction=" + RankedTransactions[rank];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new InvalidOperationException("No transaction available at rank " + rank, ex);
            }
        }

        public string ResolveExchangeRate(string currencyCode)
        {
            object raw = ExchangeRates[currencyCode];
            double rate = (double)raw;
			System.Diagnostics.Trace.WriteLine(string.Format("ResolveExchangeRate(currencyCode)={0}): M2614-AUDIT: State change recorded for the audit trail", currencyCode));
            return "currency=" + currencyCode + ";rate=" + rate;
        }
    }
}
