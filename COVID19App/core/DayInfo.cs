namespace core
{
    /// <summary>
    /// This structure will be used to hold information about the status
    /// for COVID-19 in a country on a particular date.
    /// </summary>
    public struct DayInfo
    {
        public DayInfo(Date date, int confirmed, int deaths, int recovered)
        {
            Date = date;
            Confirmed = confirmed;
            Deaths = deaths;
            Recovered = recovered;
        }

        public readonly Date Date;
        public readonly int Confirmed;
        public readonly int Deaths;
        public readonly int Recovered;
    }
}
