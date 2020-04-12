using System;
using System.Net;


namespace network
{
    [System.ComponentModel.DesignerCategory("")]
    /// <summary>
    /// This class is an extension on System.Net.WebClient that
    /// provides customizable timeout for web requests.
    /// </summary>
    class WebClientEx : WebClient
    {
        public WebClientEx(int timeout)
        {
            _timeout = timeout;
        }

        /// <summary>
        /// This method is overriden to customize the request timeout.
        /// </summary>
        protected override WebRequest GetWebRequest(Uri address)
        {
            var webRequest = base.GetWebRequest(address);
            webRequest.Timeout = _timeout;
            return webRequest;
        }

        /// <summary>
        /// The period, in milliseconds, until the web request times out.
        /// Also the value Infinite (-1) can be used to indicate that the
        /// web request doesn't time out.
        /// </summary>
        public int Timeout
        {
            get => _timeout;
            set => _timeout = value;
        }

        private int _timeout;
    }
}
