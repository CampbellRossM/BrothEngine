using System;
using System.Collections.Generic;
using System.Text.Json;
using Broth.Engine.Util;

namespace Broth.Engine.IO
{
    /// <summary>
    /// A class to help find subclasses for json deserialization.
    /// </summary>
    /// <typeparam name="TBaseObject"> The base type that all objects deserialized must inherit. </typeparam>
    public class JsonPolymorphicDeserializer<TBaseObject>
    {
        private readonly Dictionary<string, Func<string, TBaseObject>> deserializerLookup = new Dictionary<string, Func<string, TBaseObject>>();

        /// <summary>
        /// The JSON property that will be used to identify the subclass.
        /// </summary>
        public string TypeProperty { get; set; } = "Type";

        /// <summary>
        /// Add a deserializer to the lookup dictionary.
        /// </summary>
        /// <param name="typeString"> The value that the Type Property in JSON should be to use this deserializer. </param>
        /// <param name="deserializer"> A function that takes a json string and returns the deserialized object. </param>
        /// <returns></returns>
        public bool RegisterType(string typeString, Func<string, TBaseObject> deserializer)
        {
            if (deserializerLookup.TryAdd(typeString, deserializer))
                return true;

            Debug.Warning("Failed to add type \"" + typeString + "\" to lookup.");
            return false;
        }

        /// <summary>
        /// Look for the deserializer for this json string in the lookup dictionary, and return the object.
        /// </summary>
        /// <param name="jsonString"> A json string with a Type property inside the root object </param>
        /// <returns> The deserialized object </returns>
        public TBaseObject Deserialize(string jsonString)
        {
            JsonDocument jDoc = JsonManager.GetJsonDocument(jsonString);

            string type = jDoc.RootElement.GetProperty(TypeProperty).GetString();

            jDoc.Dispose();

            deserializerLookup.TryGetValue(type, out Func<string, TBaseObject> deserializer);

            return deserializer(jsonString);
        }

    }
}
