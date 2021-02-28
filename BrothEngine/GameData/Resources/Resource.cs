using System;
using System.Text.Json.Serialization;

using Broth.Util;

namespace Broth.GameData.Resources
{
    public enum ResourceContext
    {
        ALWAYS,
        LAZY,
    }

    public abstract class Resource : IDisposable
    {
        public string ID { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResourceContext Context { get; set; } = ResourceContext.LAZY;

        public abstract bool IsLoaded();

        public abstract void Load();

        public abstract void Dispose();

        /// <summary>
        /// Should be called before the resource is used.
        /// Sends a debug warning if the resource is out of context.
        /// Can be overwritten to not allow out of context lazy loading.
        /// </summary>
        public virtual void LazyLoad()
        {
            if (IsLoaded())
                return;

            if (Context != ResourceContext.LAZY)
                Debug.Warning("Resource " + ID + " is being loaded unexpectedly.");

            Load();
        }
    }
}
