using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using BioTemplate.Model.Dictionary;

namespace BioTemplate.Controller.Function
{
    public class DateTimeReferences
    {
        static DateTime _baseDate = DateTime.Today;

        public static DateTime GetTodayDate()
        {
            return (_baseDate);
        }

        public static DateTime GetMaxDate()
        {
            return (DateTime.MaxValue);
        }

        public static String GetDay(string date)
        {
            return (Convert.ToDateTime(date).Day.ToString());
        }

        public static String GetMonth(string date)
        {
            return (Convert.ToDateTime(date).Month.ToString());
        }

        public static String GetYear(string date)
        {
            return (Convert.ToDateTime(date).Year.ToString());
        }

        #region :: GET CUSTOM DATE FORMAT DISPLAY ::

        public static String GetShortDateFormat(string date)
        {
            return Convert.ToDateTime(date).ToString("d");
        } 

        public static String GetMonthRomawiFormat(string date)
        {
            return NumToEnum<BulanRomawi>(Convert.ToInt16(GetMonth(date))).ToString();
        }

        public static String GetMonthFromEnum(string month)
        {
            switch (month.ToLower())
            {
                case "januari":
                    {
                        return "01";
                    }
                case "februari":
                    {
                        return "02";
                    }
                case "maret":
                    {
                        return "03";
                    }
                case "april":
                    {
                        return "04";
                    }
                case "mei":
                    {
                        return "05";
                    }
                case "juni":
                    {
                        return "06";
                    }
                case "juli":
                    {
                        return "07";
                    }
                case "agustus":
                    {
                        return "08";
                    }
                case "september":
                    {
                        return "09";
                    }
                case "oktober":
                    {
                        return "10";
                    }
                case "november":
                    {
                        return "11";
                    }
                case "desember":
                    {
                        return "12";
                    }
                default: { return "01"; }
            }
        }

        public static T1 NumToEnum<T1>(int number)
        {
            return (T1)Enum.ToObject(typeof(T1), number);
        }

        public static String GetDateFormatToNumber(string date)
        {
            return GetMonthFromEnum(date.Split(' ')[1].ToLower()) + "/" + date.Split(' ')[0] + "/" + date.Split(' ')[2];
        }

        public static String GetDateFormat(string date)
        {
            return GetDay(date) + " " + NumToEnum<Bulan>(Convert.ToInt16(GetMonth(date))) + " " + GetYear(date);
        }

        public static String GetDateFormatMonth(string date)
        {
            return NumToEnum<Bulan>(Convert.ToInt16(GetMonth(date))).ToString();
        }

        #endregion

        #region :: GET DAY START & END ::

        public static DateTime GetYesterday()
        {
            return (_baseDate.AddDays(-1));
        }
        public static DateTime GetThisWeekStart()
        {
            return (_baseDate.AddDays(-(int)_baseDate.DayOfWeek));
        }
        public static DateTime GetThisWeekEnd()
        {
            return (GetThisWeekStart().AddDays(7).AddSeconds(-1));
        }
        public static DateTime GetLastWeekStart()
        {
            return (GetThisWeekStart().AddDays(-7));
        }
        public static DateTime GetLastWeekEnd()
        {
            return (GetThisWeekStart().AddSeconds(-1));
        }

        public static DateTime GetThisMonthStart()
        {
            return (_baseDate.AddDays(1 - _baseDate.Day));
        }
        public static DateTime GetThisMonthEnd()
        {
            return (GetThisMonthStart().AddMonths(1).AddSeconds(-1));
        }
        public static DateTime GetLastMonthStart()
        {
            return (GetThisMonthStart().AddMonths(-1));
        }
        public static DateTime GetLastMonthEnd()
        {
            return (GetThisMonthStart().AddSeconds(-1));
        }

        #endregion

        #region :: GET CUSTOM DAY START & END ::
        public static DateTime GetYesterday(string date)
        {
            return (Convert.ToDateTime(date).AddDays(-1));
        }
        public static DateTime GetThisWeekStart(string date)
        {
            return (Convert.ToDateTime(date).AddDays(-(int)Convert.ToDateTime(date).DayOfWeek));
        }
        public static DateTime GetThisWeekEnd(string date)
        {
            return (GetThisWeekStart(date).AddDays(7).AddSeconds(-1));
        }
        public static DateTime GetLastWeekStart(string date)
        {
            return (GetThisWeekStart(date).AddDays(-7));
        }
        public static DateTime GetLastWeekEnd(string date)
        {
            return (GetThisWeekStart(date).AddSeconds(-1));
        }

        public static DateTime GetThisMonthStart(string date)
        {
            return (Convert.ToDateTime(date).AddDays(1 - Convert.ToDateTime(date).Day));
        }
        public static DateTime GetThisMonthEnd(string date)
        {
            return (GetThisMonthStart(date).AddMonths(1).AddSeconds(-1));
        }
        public static DateTime GetLastMonthStart(string date)
        {
            return (GetThisMonthStart(date).AddMonths(-1));
        }
        public static DateTime GetLastMonthEnd(string date)
        {
            return (GetThisMonthStart(date).AddSeconds(-1));
        }

        #endregion

        #region :: GET DATE DIFFERENCES ::

        public static int BusinessDaysUntil(DateTime firstDay, DateTime lastDay, params DateTime[] bankHolidays)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            if (firstDay > lastDay)
                throw new ArgumentException("Incorrect last day " + lastDay);

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceeding the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                var firstDayOfWeek = (int)firstDay.DayOfWeek;
                var lastDayOfWeek = (int)lastDay.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            // subtract the number of bank holidays during the time interval
            foreach (DateTime bankHoliday in bankHolidays)
            {
                DateTime bh = bankHoliday.Date;
                if (firstDay <= bh && bh <= lastDay)
                    --businessDays;
            }

            return businessDays;
        }

        #endregion
    }

    public class TimeReferences
    {
        public static string GetTimeDifference(string t1, string t2)
        {
            DateTime time1 = Convert.ToDateTime(t1);
            DateTime time2 = Convert.ToDateTime(t2);

            TimeSpan timeSpan = time1.Subtract(time2);

            return timeSpan.ToString();
        }

        public static double GetTimeDifferenceTotalHours(string t1, string t2)
        {
            DateTime time1 = Convert.ToDateTime(t1);
            DateTime time2 = Convert.ToDateTime(t2);

            return time1.Subtract(time2).TotalHours;
        }

        public static double GetTimeDifferenceTotalMinutes(string t1, string t2)
        {
            DateTime time1 = Convert.ToDateTime(t1);
            DateTime time2 = Convert.ToDateTime(t2);

            return time1.Subtract(time2).TotalMinutes;
        }

        public static double GetTimeDifferenceTotalSeconds(string t1, string t2)
        {
            DateTime time1 = Convert.ToDateTime(t1);
            DateTime time2 = Convert.ToDateTime(t2);

            return time1.Subtract(time2).TotalSeconds;
        }

    }

}