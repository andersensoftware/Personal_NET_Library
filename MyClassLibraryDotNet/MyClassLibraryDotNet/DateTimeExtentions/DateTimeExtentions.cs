using System;
using System.Collections.Generic;
using System.Linq;

namespace Dte
{
    public static class DateTimeExtentions
    {
        /// <summary>
        /// Converts DateTime to UnixTimeStamp : Total seconds since 1st Jan 1970 12:00 AM
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>Total Seconds passed since 1st Jan 1970 12:00 AM</returns>
        public static long ToUnixTimeStamp(this DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }

        /// <summary>
        /// Adds Unix Time Stamp to dateTime
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="unixTimeStamp"></param>
        /// <returns>DateTime</returns>
        public static DateTime AddUnixTimeStamp(this DateTime dateTime, long unixTimeStamp)
        {
            return dateTime.AddSeconds(unixTimeStamp);
        }

        /// <summary>
        /// Checks datetime's Year for Leap Year
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns>bool</returns>
        public static bool IsLeapYear(this DateTime datetime)
        {
            if (datetime.Year % 4 != 0)
                return false;
            if (datetime.Year % 100 != 0)
                return true;
            if (datetime.Year % 400 != 0)
                return false;
            return true;
        }

        /// <summary>
        /// Get all the Days of the week supplied in days parameter. If days empty then return all the days.
        /// Search is inclusive of fromDate and toDate
        /// </summary>
        /// <param name="fromDate">Start date</param>
        /// <param name="toDate">To date</param>
        /// <param name="days">DayOfWeek</param>
        /// <returns>List of DateTime</returns>
        public static List<DateTime> GetDaysOfWeek(this DateTime fromDate, DateTime toDate, params DayOfWeek[] days)
        {
            if (fromDate >= toDate) return new List<DateTime>();
            var result = new List<DateTime>();

            while (fromDate <= toDate)
            {
                if (days != null && days.Any())
                {
                    if (days.Contains(fromDate.DayOfWeek))
                        result.Add(fromDate);
                }
                else
                {
                    result.Add(fromDate);
                }
                fromDate = fromDate.AddDays(1);
            }
            return result;
        }

        /// <summary>
        /// Returns only Monday to Friday from fromDate till toDate
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns>List of DateTime</returns>
        public static List<DateTime> GetWeekdays(this DateTime fromDate, DateTime toDate)
        {
            return fromDate.GetDaysOfWeek(toDate, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                DayOfWeek.Thursday, DayOfWeek.Friday);
        }

        /// <summary>
        /// Returns only Saturday and Sunday from fromDate till toDate
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns>List of DateTime</returns>
        public static List<DateTime> GetWeekends(this DateTime fromDate, DateTime toDate)
        {
            return fromDate.GetDaysOfWeek(toDate, DayOfWeek.Saturday, DayOfWeek.Sunday);
        }
    }
}
