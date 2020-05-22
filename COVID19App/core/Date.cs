/*************************************************************************
 *                                                                        *
 *  File:        Date.cs                                                  *
 *  Copyright:   (c) 2020, Petrica Petru                                  *
 *  E-mail:      petru.petrica@student.tuiasi.ro                          *
 *  Description: This struct is used to represent a date.                 *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System;

namespace Core
{
    /// <summary>
    /// User defined lightweight date class.
    /// </summary>
    public struct Date
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public Date(int year, int month, int day)
        {
            if (!Utils.IsValid(year, month, day))
                throw new ArgumentException();
            Year = year;
            Month = month;
            Day = day;
        }

        /// <summary>
        /// Parses a date from a string of format yyyy-mm-dd,
        /// where y is used to denote the year, m for the month
        /// and d for day.
        /// Throw FormatException if the input string to parse is not valid
        /// Throw ArgumentException Error if the year, month, day are not valid
        /// </summary>
        /// <param name="format">String to parse.</param>
        /// <returns>The corresponding date structure.</returns>
        public static Date Parse(string format)
        {
            var parts = format.Split('-');

            if (parts.Length != 3)
                throw new FormatException();

            var year = int.Parse(parts[0]);
            var month = int.Parse(parts[1]);
            var day = int.Parse(parts[2]);

            if (!Utils.IsValid(year, month, day))
                throw new ArgumentException();

            return new Date(year, month, day);
        }

        public override bool Equals(object obj)
        {
            return obj != null && this == (Date)obj;
        }

        public override int GetHashCode()
        {
            return Year ^ Month ^ Day;
        }

        public static bool operator==(Date d1, Date d2)
        {
            return d1.Year == d2.Year && d1.Month == d2.Month && d1.Day == d2.Day;
        }
        
        public static bool operator!=(Date d1, Date d2)
        {
            return !(d1 == d2);
        }

        public static bool operator<(Date d1, Date d2)
        {
            if (d1.Year != d2.Year)
                return d1.Year < d2.Year;

            if (d1.Month != d2.Month)
                return d1.Month < d2.Month;

            return d1.Day < d2.Day;
        }
        
        public static bool operator>(Date d1, Date d2)
        {
            if (d1.Year != d2.Year)
                return d1.Year > d2.Year;

            if (d1.Month != d2.Month)
                return d1.Month > d2.Month;

            return d1.Day > d2.Day;
        }

        public override string ToString()
        {
            return Year + "-" + Month.ToString().PadLeft(2, '0') + "-" + Day.ToString().PadLeft(2, '0');
        }

        public DateTime ToDateTime()
        {
            return new DateTime(Year, Month, Day);
        }

        public readonly int Year;
        public readonly int Month;
        public readonly int Day;
    }
}
