/*************************************************************************
 *                                                                        *
 *  File:        IMapObserver.cs                                          *
 *  Copyright:   (c) 2020, Petrica Petru                                  *
 *  E-mail:      petru.petrica@student.tuiasi.ro                          *
 *  Description: This class represents a listener for user click          *
 *  events on the map                                                     *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using core;


namespace view
{
    /// <summary>
    /// Interface implemented by those who want to receive
    /// click events from the map.
    /// </summary>
    public interface IMapObserver
    {
        /// <summary>
        /// This method is called when the user clicks on a specific
        /// country
        /// </summary>
        /// <param name="country">
        /// The country info specific to the country 
        /// the user clicked
        /// </param>
        void OnClick(CountryInfoEx country);
    }
}
