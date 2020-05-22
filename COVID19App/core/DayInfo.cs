/*************************************************************************
 *                                                                        *
 *  File:        Date.cs                                                  *
 *  Copyright:   (c) 2020, Petrica Petru                                  *
 *  E-mail:      petru.petrica@student.tuiasi.ro                          *
 *  Description: This struct is used to represent information             *
 *  about a particular day.                                               *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

namespace Core
{
    /// <summary>
    /// This structure will be used to hold information about the status
    /// for COVID-19 in a country on a particular date.
    /// </summary>
    public struct DayInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="date">User defined date object</param>
        /// <param name="confirmed">Number of confirmed cases</param>
        /// <param name="deaths">Number of deaths</param>
        /// <param name="recovered">Numver of recovered people</param>
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
