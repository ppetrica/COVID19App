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
        private int _timeout;

        /// <summary>
        /// The period, in milliseconds, until the web request times out.
        /// Also the value Infinite (-1) can be used to indicate that the
        /// web request doesn't time out.
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        public WebClientEx(int timeout)
        {
            _timeout = timeout;
        }

        /// <summary>
        /// This method is overriden to customize the request timeout.
        /// </summary>
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest webRequest = base.GetWebRequest(address);
            webRequest.Timeout = _timeout;
            return webRequest;
        }
    }
}
