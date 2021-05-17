using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

namespace Assignment
{
    static class Data
    {
        private static string jsonText;
        /// <summary>
        /// Loads an array of categories
        /// </summary>
        /// <returns>An array of categories</returns>
        public static string[] GetCategories()
        {
            ReadText();
            JsonDocument json = JsonDocument.Parse(jsonText);
            JsonElement root = json.RootElement;
            int length = root.GetArrayLength();
            string[] result = new string[length];
            int index = 0;
            foreach (var node in root.EnumerateArray())
            {
                string category = node.GetRawText().Split(":")[0].Split("\"")[1];
                result[index] = category;
                index++;
            }


            
            return result;

        }

        /// <summary>
        /// Gets all the tool types under the specified category
        /// </summary>
        /// <param name="category">The category to look for</param>
        /// <returns>An array of tool types</returns>
        public static string[] GetToolTypesByCategory(string category)
        {
            
            JsonDocument json = JsonDocument.Parse(jsonText);
            JsonElement root = json.RootElement;

            var result = new List<string>();
            int index = 0;
            foreach (var node in root.EnumerateArray())
            {
                string currentCategory = node.GetRawText().Split(":")[0].Split("\"")[1];
                if (category == currentCategory)
                {
                    foreach(var toolType in  node.GetProperty(category).EnumerateArray())
                    {
                        result.Add(toolType.GetString());
                    }
                }
                index++;
            }
            return result.ToArray();
        }

        /// <summary>
        /// Loads ToolTypes.json file
        /// </summary>
        private static void ReadText()
        {
            if(jsonText==null)
            {
                jsonText = File.ReadAllText("ToolTypes.json");
            }
        }
    
    
    }
}
