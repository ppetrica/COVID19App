/*************************************************************************
 *                                                                        *
 *  File:        WebClientEx.cs                                           *
 *  Copyright:   (c) 2020, Moisii Marin                                   *
 *  E-mail:      marin.moisii@student.tuiasi.ro                           *
 *  Description: This class is responsible for communicating with the     *
 *  the Internet.                                                         *
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
using System.Net;


namespace Network
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
            Timeout = timeout;
        }

        /// <summary>
        /// This method is overriden to customize the request timeout.
        /// </summary>
        protected override WebRequest GetWebRequest(Uri address)
        {
            var webRequest = base.GetWebRequest(address);
            webRequest.Timeout = Timeout;
            return webRequest;
        }

        /// <summary>
        /// The period, in milliseconds, until the web request times out.
        /// Also the value Infinite (-1) can be used to indicate that the
        /// web request doesn't time out.
        /// </summary>
        public int Timeout { get; set; }
    }
}
