using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Broth.Util;

namespace Broth.IO
{
    /// <summary>
    /// This class contains all of the filepath variables, excluding those defined in scripts.
    /// It also provides functions for locating files that aren't explicitly defined,
    /// and some filepath string manipulation functions.
    /// </summary>
    public static class FileFinder
    {
        // CONSTANTS
        /// <summary> Config is the only constant relative to the entry point (game.exe), all others are relative to the resources folder </summary>
        public static string CONFIG { get { return "config.json"; } }
        public static string ENGINE { get { return "BrothEngine"; } }
        public static string CONTENT_PACKS { get { return "ContentPacks"; } }
        public static string RESOURCE_PACKS { get { return "ResourcePacks"; } }
        public static string LANGUAGE_PACKS { get { return "LanguagePacks"; } }
        public static string SAVES { get { return "Saves"; } }
        public static string MODS { get { return "Mods"; } }

        private static string _resourcesFolder = "";
        /// <summary> The absolute path to the resources folder with no trailing directory separator. </summary>
        public static string ResourcesFolder
        {
            get
            {
                if (_resourcesFolder.Length < 1)
                {
                    Debug.Warning("Attempted to access resource folder before it was located.");
                    throw new IOException("Resources folder not located");
                }
                return _resourcesFolder;
            }
        }

        /// <returns> The absolute path to the resources folder. </returns>
        public static string ResourcePath(bool appendTrailingSeparator = false)
        {
            string path = ResourcesFolder;
            if (appendTrailingSeparator)
                path += Path.DirectorySeparatorChar;
            return path;
        }
        /// <returns> Combine a path with the path to the resources folder. </returns>
        public static string ResourcePath(string filepathRelativeToResourcesFolder, bool appendTrailingSeparator = false)
        {
            string path = Path.Combine(ResourcesFolder, filepathRelativeToResourcesFolder);
            if (appendTrailingSeparator)
                path += Path.DirectorySeparatorChar;
            return path;
        }
        /// <returns> Combine an array of directories with the path to the resource folder. </returns>
        public static string ResourcePath(string[] folderArrayRelativeToResourcesFolder, bool appendTrailingSeparator = false)
        {
            string path = Path.Combine(ResourcesFolder, Path.Join(folderArrayRelativeToResourcesFolder));
            if (appendTrailingSeparator)
                path += Path.DirectorySeparatorChar;
            return path;
        }

        /// <summary>
        /// While forward slashes almost always work regardless of OS,
        /// this allows us to use them as a standard in our json files without worry.
        /// </summary>
        public static string ReplaceSlashesWithSeperators(string filepath)
        {
            return filepath.Replace('/', Path.DirectorySeparatorChar);
        }

        internal static void LocateResources(string resourcesFolder)
        {
            _resourcesFolder = Path.GetFullPath(resourcesFolder);
            if (!Directory.Exists(_resourcesFolder))
            {
                // TODO: Throw an error and crash! We can't continue until we have resources!
                Debug.Warning("Could not find resources folder:\n" + _resourcesFolder + "\nDouble check that the config file is pointing to the correct folder.");
            }
        }
    }
}
