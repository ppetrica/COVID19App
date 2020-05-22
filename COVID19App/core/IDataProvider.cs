/*************************************************************************
 *                                                                        *
 *  File:        IDataProvider.cs                                         *
 *  Copyright:   (c) 2020, Petrica Petru                                  *
 *  E-mail:      petru.petrica@student.tuiasi.ro                          *
 *  Description: This interface is implemented by the different providers *
 *  of covid information (network / database).                            *
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


namespace core
{
    /// <summary>
    /// Interface implemented by any data provider in program.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataProvider<out T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Statistics about COVID-19 organized by country as a read only list of core.CountryInfo.</returns>
        IReadOnlyList<T> GetCountryData();
    }
}
