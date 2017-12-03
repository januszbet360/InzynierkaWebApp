using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDownloader
{
    public static class SeasonHelper
    {
        public static string GetCurrentSeason(DateTime date)
        {
            StringBuilder sb = new StringBuilder();

            if (date.Month >= 7)
            {
                sb.Append(date.Year);
                sb.Append('/');
                sb.Append(date.Year + 1);
            }
            else
            {
                sb.Append(date.Year - 1);
                sb.Append('/');
                sb.Append(date.Year);
            }
            return sb.ToString();
        }

    }
}
