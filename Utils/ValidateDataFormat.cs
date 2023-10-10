using Microsoft.Extensions.Primitives;
using System.Globalization;

namespace eventz.Utils
{
    public class ValidateDataFormat
    {

        public static DateTime ConvertToCustomFormat(string inputDate)
        {
            if (string.IsNullOrEmpty(inputDate))
            {
                return DateTime.Now;
            }

            DateTime date = DateTime.ParseExact(inputDate, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);


            return date;
        }

    }

}
