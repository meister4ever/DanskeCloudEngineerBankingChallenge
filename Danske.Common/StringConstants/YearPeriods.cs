using System.Collections.Generic;

namespace Danske.Common.StringConstants
{
    /// <summary>
    /// Amount of periods to divide the year interest rate.
    /// </summary>
    public static class YearPeriods
    {
        public static int GetYearPeriods(string periodName)
        {
            var periods = new Dictionary<string, int>
            {
                { "Monthly", 12 }
            };
            return periods[periodName];
        }
    }
}
