using System;
using System.Globalization;

namespace Employees.Website.Services
{
    public interface IFormatService
    {
        DateTime TryParse(string text);
    }

    public class FormatService : IFormatService
    {
        private readonly string[] formats = {
                "M/d/yyyy", "MM/dd/yyyy",
                "d/M/yyyy", "dd/MM/yyyy",
                "yyyy/M/d", "yyyy/MM/dd",
                "M-d-yyyy", "MM-dd-yyyy",
                "d-M-yyyy", "dd-MM-yyyy",
                "yyyy-M-d", "yyyy-MM-dd",
                "M.d.yyyy", "MM.dd.yyyy",
                "d.M.yyyy", "dd.MM.yyyy",
                "yyyy.M.d", "yyyy.MM.dd",
                "M,d,yyyy", "MM,dd,yyyy",
                "d,M,yyyy", "dd,MM,yyyy",
                "yyyy,M,d", "yyyy,MM,dd",
                "M d yyyy", "MM dd yyyy",
                "d M yyyy", "dd MM yyyy",
                "yyyy M d", "yyyy MM dd"
            };

        public DateTime TryParse(string text)
        {
            DateTime date;

            if (DateTime.TryParseExact(text, formats, null, DateTimeStyles.None, out date))
            {
                return date;
            }

            return DateTime.Now;
        }
    }
}
