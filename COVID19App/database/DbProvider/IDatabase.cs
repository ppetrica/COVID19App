/**************************************************************************
 *                                                                        *
 *  File:        IDatabase.cs                                             *
 *  Copyright:   (c) 2020, Enachi Vasile                                  *
 *  E-mail:      vasile.enachi@student.tuiasi.ro                          *
 *  Description: An abstract representation of a database subscribed to   *
 *  a cache for new data.                                                 *
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
using core;

/// <summary>
/// This module is a middleware (proxy) beetween database and highlevel module view.
/// </summary>
namespace database
{
    public interface IDatabase
    {
        /// <summary>
        /// Insert the list of countryInfo to the database, transferring raw data to IDbManager
        /// </summary>
        /// <param name="countryInfoList">List of Country Info to be inserted in the database</param>
        void InsertCountryData(IReadOnlyList<CountryInfo> countryInfoList);

        /// <returns>The most recent Date of the data from the database</returns>
        Date GetTheMostRecentDateOfData();
    }
}
