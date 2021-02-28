using System;
using System.Collections.Generic;
using Broth.Util;

namespace Broth.GameData.Resources
{
    public class ResourceManager
    {
        private readonly Dictionary<string, Resource> resourceRegistry = new Dictionary<string, Resource>();

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
