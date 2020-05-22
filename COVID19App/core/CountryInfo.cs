/*************************************************************************
 *                                                                        *
 *  File:        CountryInfo.cs                                           *
 *  Copyright:   (c) 2020, Petrica Petru                                  *
 *  E-mail:      petru.petrica@student.tuiasi.ro                          *
 *  Description: This class is used to represent country data             *
 *  as received from the network.                                         *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System.Collections.Generic;
using System.Linq;

/// <summary>
/// This is the core module. It defines structures, snippets and others basic functionality.
/// </summary>
namespace core
{
    /// <summary>
    /// This structure will hold information about the status of
    /// COVID-19 in a particular period of time.
    /// </summary>
    public struct CountryInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the country</param>
        /// <param name="daysInfo">List of information corresponding to country</param>
        public CountryInfo(string name, IReadOnlyList<DayInfo> daysInfo)
        {
            Name = name;
            DaysInfo = daysInfo;
        }

        public readonly string Name;
        public readonly IReadOnlyList<DayInfo> DaysInfo;

        /// <param name="country">CountryInfo object</param>
        /// <param name="mostRecentDay">The date from which we need to include data</param>
        /// <returns>CountryInfo object which contains dayInfo starting from the day parameter(exclusive)</returns>
        public static CountryInfo FilterCountryInfoDates(CountryInfo country, Date mostRecentDay)
        {
            var daysInfoTemp = country.DaysInfo.Where(dayInfo => dayInfo.Date > mostRecentDay).ToList();
            return new CountryInfo(country.Name, daysInfoTemp);
        }
    }
}
