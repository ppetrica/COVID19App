/**************************************************************************
 *                                                                        *
 *  File:        AbstractDatabaseCache.cs                                 *
 *  Copyright:   (c) 2020, Enachi Vasile                                  *
 *  E-mail:      vasile.enachi@student.tuiasi.ro                          *
 *  Description: An abstract representation of a Database Cache.          *
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
using System.Data;
using Core;
using Database;

/// <summary>
/// This module manages relational Database.
/// </summary>
namespace Cache
{
    /// <summary>
    /// The DatabaseCache class will take a look if there are any new data to insert in the Database.
    /// </summary>
    public abstract class AbstractDatabaseCache
    {
        private List<IDatabase> _providers = new List<IDatabase>();
        protected List<CountryInfo> _countryInfoList = new List<CountryInfo>();

        public void Attach(IDatabase provider)
        {
            _providers.Add(provider);
        }

        public void Detach(IDatabase provider)
        {
            _providers.Remove(provider);
        }

        public void Notify()
        {
            foreach (var provider in _providers)
            {
                provider.InsertCountryData(_countryInfoList);
            }
        }

        /// <returns>Return the most recent date of data of the existing providers
        /// If not recentDate was found throw the ObjectNotFoundException exception</returns>
        protected Date getTheMostRecentDateFromProviders()
        {
            var dateList = new List<Date>();
            foreach(var provider in _providers)
            {
                try
                {
                    dateList.Add(provider.GetTheMostRecentDateOfData());
                }
                catch (ObjectNotFoundException)
                {

                }
            }

            if (dateList.Count == 0)
            {
                throw new ObjectNotFoundException();
            }
            return Utils.MaxElement(dateList, (date1, date2) => date1 > date2);
        }
    }
}
