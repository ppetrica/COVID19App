/*************************************************************************
 *                                                                        *
 *  File:        MockDatabaseProvider.cs                                  *
 *  Copyright:   (c) 2020, Enachi Vasile                                  *
 *  E-mail:      vasile.enachi@student.tuiasi.ro                          *
 *  Description: This class is used to mock a Database for the Cache      *
 *  module                                                                *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using Core;
using Database;
using System.Collections.Generic;
using System.Data;


namespace TestCache
{
    class MockDatabaseProvider : IDatabase, IDataProvider<CountryInfoEx>
    {
        public Date GetTheMostRecentDateOfData()
        {
            if (rawDaysInfoList.Count == 0)
            {
                throw new ObjectNotFoundException();
            }

            var date = new Date(1, 1, 1);
            foreach (var countryInfo in rawDaysInfoList)
            {
                foreach (var dayInfo in countryInfo.DaysInfo)
                {
                    if (date < dayInfo.Date)
                    {
                        date = dayInfo.Date;
                    }
                }
            }
            return date;
        }

        public void InsertCountryData(IReadOnlyList<CountryInfo> countryInfoList)
        {
            rawDaysInfoList.AddRange(countryInfoList);
        }

        public IReadOnlyList<CountryInfoEx> GetCountryData()
        {
            var countryData = new List<CountryInfoEx>();
            foreach (var countryInfo in rawDaysInfoList)
            {
                countryData.Add(new CountryInfoEx(countryInfo, "whatever", "nevermind", 0));
            }
            return countryData;
        }

        List<CountryInfo> rawDaysInfoList = new List<CountryInfo>();
    }
}
