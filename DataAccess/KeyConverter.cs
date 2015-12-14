using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Custom JsonConverter for the Key class.
    /// </summary>
    public class KeyConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType != null && objectType == typeof(Key)) return true;

            return false;
        }

        /// <summary>
        /// Deserializes Json into a Key instance.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // TODO: null checks?

            Key key = new Key((string)reader.Value);

            return key;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // TODO: null checks?

            Key key = (Key)value;

            writer.WriteValue(key.Value);
        }
    }
}
