using System;
using System.Collections.Generic;
using System.Text.Json;
using Broth.GameData.Resources;
using Broth.GameData.Resources.GraphicsResources;
using Broth.Util;

namespace Broth.IO
{
    public static class ResourceFactory
    {

        private static readonly Dictionary<string, Func<string, Resource>> resourceDeserializerLookup = new Dictionary<string, Func<string, Resource>>();

        public static bool RegisterResourceType(string typeString, Func<string, Resource> deserializer)
        {
            if (resourceDeserializerLookup.TryAdd(typeString, deserializer))
                return true;
            
            Debug.Warning("Failed to add resource type \"" + typeString + "\" to lookup.");
            return false;
        }

        internal static void RegisterCommonResourceTypes()
        {
            RegisterResourceType("image", (string jString) => { return JsonManager.DeserializeObject<ImageResource>(jString); });
            RegisterResourceType("font", (string jString) => { return JsonManager.DeserializeObject<FontResource>(jString); });
        }

        public static void RegisterResourceFromFile(string filepath, ResourceManager registry)
        {
            JsonDocument jDoc = JsonManager.GetJsonDocumentFromFile(FileFinder.ResourcePath(filepath));

            string type = jDoc.RootElement.GetProperty("Type").ToString();

            resourceDeserializerLookup.TryGetValue(type, out Func<string, Resource> typeDeserializer);

            Resource res = typeDeserializer(jDoc.RootElement.GetRawText());

            registry.RegisterResource(res);

            jDoc.Dispose();
        }
    }
}
