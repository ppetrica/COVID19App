using core;
using Newtonsoft.Json;
using System;


namespace network
{
    /// <summary>
    /// This class is used to deserialize date value from json to core.Date structure.
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
