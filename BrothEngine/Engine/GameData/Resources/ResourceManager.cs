using System;
using System.Collections.Generic;

using Broth.Engine.IO;
using Broth.Engine.Util;

namespace Broth.Engine.GameData.Resources
{
    public class ResourceManager
    {
        private readonly Dictionary<string, Resource> resourceRegistry = new Dictionary<string, Resource>();

        public readonly JsonPolymorphicDeserializer<Resource> ResourceDeserializer = new JsonPolymorphicDeserializer<Resource>();

        public ResourceManager(bool registerDefaultDeserializers = true)
        {
            if (registerDefaultDeserializers)
            {
                ResourceDeserializer.RegisterType("image", JsonManager.DeserializeObject<GraphicsResources.ImageResource>);
                ResourceDeserializer.RegisterType("font", JsonManager.DeserializeObject<FontResource>);
            }
        }

        public TResource TryGetResource<TResource>(string id) where TResource : Resource
        {
            if (resourceRegistry.TryGetValue(id, out Resource resource) == false)
            {
                Debug.Warning("Failed to retrieve resource " + id + " from registry.");
                return null;
            }
            try
            {
                return (TResource)resource;
            }
            catch (InvalidCastException)
            {
                Debug.Warning("Resource " + id + " is registered as an unexpected type.\n" +
                    "Expected: " + typeof(TResource).FullName + " Received: " + resource.GetType().FullName);
            }
            return null;
        }

        public void RegisterResource(Resource resource)
        {
            if (resourceRegistry.TryAdd(resource.ID, resource) == false)
                Debug.Warning("Resource ID conflict.\n" +
                    "Attempted to register " + resource.ID + " but that ID is already in use.");
        }
    }
}
