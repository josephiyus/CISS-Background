using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace CISS_Background.id.co.cdp.util
{
    public static class DateTimeUtil
    {
        public static string getFormattedDateTime(string date, string time) 
        {
            foreach (var item in new int[] { 4, 7 }) date = date.Insert(item, "-");
            foreach (var item in new int[] { 2, 5 }) time = time.Insert(item, ":");
            return date + " " + time;
        }

        public static DateTime parseDateTime(string dateTimeStr) 
        {
            //return DateTime.ParseExact(dateTimeStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dDate;
            DateTime.TryParse(dateTimeStr, out dDate);
            return dDate;
        }

        public static DateTime parseDateTimeddMMyyyy(string dateTimeStr)
        {
            return DateTime.ParseExact(dateTimeStr, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
