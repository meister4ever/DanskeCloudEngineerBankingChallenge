using System.Globalization;

namespace Danske.Common.Extensions
{
    public static class StringExtensions
    {
        public static string AsCurrencyString(this double amount, string cultureInfo)
        {
            // Display string representations of numbers for the loans culture
            var ci = new CultureInfo(cultureInfo);
            return amount.ToString("C", ci);
        }

        public static string AsPercentageString(this double amount, string cultureInfo)
        {
            var ci = new CultureInfo(cultureInfo);
            return amount.ToString("P", ci);
        }
    }
}
