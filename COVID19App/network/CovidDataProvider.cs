using System.Collections.Generic;
using core;
using System;
using Newtonsoft.Json;


namespace network
{
    /// <summary>
    /// This class is used to get information of each country about
    /// effects of COVID-19, using an API request.
    /// </summary>
    public class CovidDataProvider : DataProvider<CountryInfo>
    {
        /// <param name="timeout">The period, in milliseconds, until the web request times out.
        /// Also the value Infinite (-1) can be used to indicate that the web request doesn't time out.</param>
        /// <exception cref="ArgumentException">Thrown when parameter value is lower than -1.</exception>
        public CovidDataProvider(int timeout = InternetConnection.DefaultTimeout)
        {
            if (timeout < -1)
                throw new ArgumentException();

            _webClient = new WebClientEx(timeout);
        }

        /// <returns>Statistics about COVID-19 organized by country as a read only list of core.CountryInfo.</returns>
        /// <exception cref="System.Net.WebException">Thrown when api request time out or fails.</exception>
        public IReadOnlyList<CountryInfo> GetCountryData()
        {
            List<CountryInfo> countryData = new List<CountryInfo>();
            string responseJson = _webClient.DownloadString(Url);

            // covid info api provides data as a dictionary of (country name : array of daily statistics)
            Dictionary<string, List<DayInfo>> covidInfo = JsonConvert.DeserializeObject<Dictionary<string, List<DayInfo>>>(responseJson,
                new DateJsonConverter());

            foreach (KeyValuePair<string, List<DayInfo>> info in covidInfo)
            {
                countryData.Add(new CountryInfo(info.Key, info.Value));
            }

            return countryData.AsReadOnly();
        }

        /// <summary>
        /// The period, in milliseconds, until the web request times out.
        /// Also the value Infinite (-1) can be used to indicate that the web request doesn't time out.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown by setter when value is lower than -1.</exception>
        public int Timeout
        {
            get
            {
                return _webClient.Timeout;
            }
            set
            {
                if (value < -1)
                    throw new ArgumentException();

                _webClient.Timeout = value;
            }
        }
        
        public const string Url = "https://pomber.github.io/covid19/timeseries.json?fbclid=IwAR2FznKc4nXVzWdyZMKc7X58psda0y3DzTMet9u_FU8BtEfkB6n3H9uxhDA";

        private WebClientEx _webClient;
    }
}
