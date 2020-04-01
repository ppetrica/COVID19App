using System;


namespace core
{
    public struct Date
    {
        public Date(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        /// <summary>
        /// Parses a date from a string of format yyyy-mm-dd,
        /// where y is used to denote the year, m for the month
        /// and d for day.
        /// </summary>
        /// <param name="format">String to parse.</param>
        /// <returns>The corresponding date structure.</returns>
        public static Date Parse(string format)
        {
            string[] parts = format.Split('-');

            if (parts.Length != 3)
                throw new FormatException();

            int year = Int32.Parse(parts[0]);
            int month = Int32.Parse(parts[1]);
            int day = Int32.Parse(parts[2]);

            if (year < 0 || month < 1 || month > 12 || day < 1 || day > 31)
                throw new FormatException();

            return new Date(year, month, day);
        }

        public override bool Equals(object obj)
        {
            return this == (Date)obj;
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

        public readonly int Year;
        public readonly int Month;
        public readonly int Day;
    }
}
