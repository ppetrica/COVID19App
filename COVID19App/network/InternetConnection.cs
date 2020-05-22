/*************************************************************************
 *                                                                        *
 *  File:        CovidDataProvider.cs                                     *
 *  Copyright:   (c) 2020, Moisii Marin                                   *
 *  E-mail:      marin.moisii@student.tuiasi.ro                           *
 *  Description: This module is used to test whether the client has an    *
 *  Internet connection available.                                        *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

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
