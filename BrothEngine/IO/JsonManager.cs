using Broth.Util;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Broth.IO
{
    /// <summary>
    /// Methods for saving and reading json
    /// </summary>
    public static class JsonManager
    {
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
        };

        private static JsonDocumentOptions documentOptions = new JsonDocumentOptions
        {
            AllowTrailingCommas = true,
            CommentHandling = JsonCommentHandling.Skip,
        };

        /// <summary>
        /// Converts an object into a json formatted string.
        /// </summary>
        /// <param name="obj"> The object to convert </param>
        /// <returns> The object represented as a json formatted string </returns>
        public static string SerializeObject(object obj)
        {
            return JsonSerializer.Serialize(obj, options);
        }

        /// <summary>
        /// Creates an instance of an object based on a json string.
        /// </summary>
        /// <typeparam name="TObject"> The object Type that the string represents </typeparam>
        /// <param name="jsonString"> The json formatted string </param>
        /// <returns> A TObject representation of the json values </returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="JsonException"></exception>
        public static TObject DeserializeObject<TObject>(string jsonString)
        {
            return JsonSerializer.Deserialize<TObject>(jsonString, options);
        }


        public static JsonDocument GetJsonDocumentFromFile(string absoulteFilepath)
        {
            using (StreamReader reader = new StreamReader(absoulteFilepath))
            {
                string jString = reader.ReadToEnd();
                return JsonDocument.Parse(jString, documentOptions);
            }
        }

        /// <summary>
        /// Creates an instance of an object based on a json file.
        /// </summary>
        /// <typeparam name="TObject"> The object Type that the file represents. </typeparam>
        /// <param name="absoluteFilepath"> The location of the json file. </param>
        /// <returns> A TObject representation of the json values. </returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        public static TObject GetObjectFromFile<TObject>(string absoluteFilepath)
        {
            using (StreamReader reader = new StreamReader(absoluteFilepath))
            {
                string jString = reader.ReadToEnd();
                return DeserializeObject<TObject>(jString);
            };
        }

        /// <summary>
        /// Safely attempts to create an instance of an object based on a json file.
        /// Logs any errors to the debug log.
        /// </summary>
        /// <typeparam name="TObject"> The object Type that the file represents. </typeparam>
        /// <param name="absoluteFilepath"> The location of the json file. </param>
        /// <returns> A TObject representation of the json values, or null if unsuccessful. </returns>
        public static TObject TryGetObjectFromFile<TObject>(string absoluteFilepath) where TObject : class
        {
            try
            {
                return GetObjectFromFile<TObject>(absoluteFilepath);
            }
            catch (FileNotFoundException)
            {
                Debug.Warning("No file found at " + absoluteFilepath);
            }
            catch (DirectoryNotFoundException)
            {
                Debug.Warning("Could not find filepath " + absoluteFilepath);
            }
            catch (IOException)
            {
                Debug.Warning("IOException, could not load " + absoluteFilepath);
            }
            catch (OutOfMemoryException)
            {
                Debug.Warning("Ran out of memory while reading file " + absoluteFilepath);
            }
            catch (JsonException)
            {
                Debug.Warning("Failed to interpret JSON data " + absoluteFilepath);
            }
            catch (Exception)
            {
                Debug.Warning("JSON file failed for unknown reason " + absoluteFilepath);
            }

            return null;
        }

        /// <summary>
        /// Attempts to save the object to a json file.
        /// </summary>
        /// <param name="obj"> The object to save to json. </param>
        /// <param name="absoluteFilepath"> Where to save the file. </param>
        /// <returns> True if successful. </returns>
        public static bool SaveObjectToFile(object obj, string absoluteFilepath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(absoluteFilepath))
                {
                    writer.Write(SerializeObject(obj));
                };
                return true;
            }
            catch (Exception)
            {
                Debug.Warning(obj.GetType().FullName + " not saved to file " + absoluteFilepath);
            }

            return false;
        }

    }
}
