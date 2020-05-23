/*************************************************************************
 *                                                                        *
 *  File:        MockDataProviderEx.cs                                    *
 *  Copyright:   (c) 2020, Petrica Petru                                  *
 *  E-mail:      petru.petrica@student.tuiasi.ro                          *
 *  Description: This class provides mock data from the database.         *
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
using System.Collections.Generic;


namespace TestCore
{
    public class MockDataProviderEx : IDataProvider<CountryInfoEx>
    {
        public IReadOnlyList<CountryInfoEx> GetCountryData()
        {
            return new List<CountryInfoEx> {
                new CountryInfoEx(
                    new CountryInfo("",
                        new List<DayInfo> {
                            new DayInfo(new Date(2020, 1, 22), 0, 0, 0),
                            new DayInfo(new Date(2020, 1, 23), 5, 1, 0),
                            new DayInfo(new Date(2020, 1, 24), 25, 3, 1)
                        }
                    ),"MX", "America",120_000_000),
                new CountryInfoEx(
                    new CountryInfo("",
                        new List<DayInfo>() {
                            new DayInfo(new Date(2020, 1, 22), 1, 0, 1),
                            new DayInfo(new Date(2020, 1, 23), 7, 2, 1),
                            new DayInfo(new Date(2020, 1, 24), 18, 4, 0)
                        }),
                    "CA", "America", 250_000_000),
                new CountryInfoEx(
                    new CountryInfo("", new List<DayInfo> {
                        new DayInfo(new Date(2020, 1, 22), 0, 0, 0),
                        new DayInfo(new Date(2020, 1, 23), 1, 0, 0),
                        new DayInfo(new Date(2020, 1, 24), 2, 0, 1)
                        }),
                    "RU", "Europe",360_000_000),
                new CountryInfoEx(
                    new CountryInfo("", new List<DayInfo> {
                        new DayInfo(new Date(2020, 1, 22), 0, 0, 0),
                        new DayInfo(new Date(2020, 1, 23), 20, 1, 3),
                        new DayInfo(new Date(2020, 1, 24), 40, 10, 5)
                        }),
                    "IT", "Europe",60_000_000)
            };
        }
    }
}
