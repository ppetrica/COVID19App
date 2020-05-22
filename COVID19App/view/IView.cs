/*************************************************************************
 *                                                                        *
 *  File:        IView.cs                                                 *
 *  Copyright:   (c) 2020, Petrica Petru                                  *
 *  E-mail:      petru.petrica@student.tuiasi.ro                          *
 *  Description: This interface is implemented by each View               * 
 *  in the application                                                    *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System.Windows.Forms;


namespace View
{
    /// <summary>
    /// Interface for uniform access to MapView, GlobalView, CountryView.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// This method will return the control specific to a View, for now
        /// it should return a TabPage control to be added to our central
        /// TabControl
        /// </summary>
        /// <returns>The control to be inserted in the main tab control</returns>
        TabPage GetPage();
    }
}
