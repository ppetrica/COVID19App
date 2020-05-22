/*************************************************************************
 *                                                                        *
 *  File:        DateJsonConverter.cs                                     *
 *  Copyright:   (c) 2020, Moisii Marin                                   *
 *  E-mail:      marin.moisii@student.tuiasi.ro                           *
 *  Description: This class is used to convert date objects to and from   *
 *  JSON format.                                                          *
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
using Newtonsoft.Json;
using System;


namespace Network
{
    /// <summary>
    /// This class is used to deserialize date value from json to Core.Date structure.
    /// </summary>
    class DateJsonConverter : JsonConverter<Date>
    {
        public override Date ReadJson(JsonReader reader, Type objectType, Date existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Date.Parse((string)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, Date value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
