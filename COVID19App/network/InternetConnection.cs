namespace network
{
    /// <summary>
    /// This class check in there is Internet connection.
    /// </summary>
    public class InternetConnection
    {
        /// <summary>
        /// Check if an internet connection is available.
        /// </summary>
        /// <param name="timeout">Length of time, in milliseconds, until the web request time out.</param>
        /// <param name="testUrl">Valid url to requested resource over Internet.</param>
        /// <returns>True if application has access to Internet, false otherwise.</returns>
        public static bool isConnectionAvailable(int timeout = DefaultTimeout, 
                                                 string testUrl = "http://google.com/generate_204")
        {
            var webClient = new WebClientEx(timeout);
            try
            {
                webClient.OpenRead(testUrl);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public const int DefaultTimeout = 5000;
    }
}
