using System.IO;
using System.Collections.Generic;
using System.Text.Json;

namespace Assignment
{
    /// <summary>
    /// For fetching pre-exist data
    /// </summary>
    static class Data
    {
        private static string jsonText;
        /// <summary>
        /// Loads an array of categories
        /// </summary>
        /// <returns>An array of categories</returns>
        public static string[] GetCategories()
        {
            ReadToolText();
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
        private static void ReadToolText()
        {
            if(jsonText==null)
            {
                jsonText = File.ReadAllText("ToolTypes.json");
            }
        }
    
        /// <summary>
        /// Get a default member
        /// </summary>
        /// <returns>A memeber</returns>
        public static iMember GetMember()
        {
            string memberText = File.ReadAllText("UserData.json");

            return JsonSerializer.Deserialize<Member>(memberText);
        }
    
        public static void PopulateTools(Dictionary<string, Dictionary<string, iToolCollection>> toolData)
        {
            // ["Gardening Tools"]["Line Trimmers"]
            toolData["Gardening Tools"]["Line Trimmers"].add(new Tool("Ozito 290mm Electric Line Trimmer", 4));

            toolData["Gardening Tools"]["Line Trimmers"].add(new Tool("Ryobi 1200W Line Trimmer", 1));

            toolData["Gardening Tools"]["Line Trimmers"].add(new Tool("Ryobi Easy Stat Curved Shaft Line Trimmer", 2));

            toolData["Gardening Tools"]["Line Trimmers"].add(new Tool("Ozito 550W Electric Hedge Trimmer", 6));

            toolData["Gardening Tools"]["Line Trimmers"].add(new Tool("Ozito Lithium-Ion Cordless Hedge Trimmer", 2));

            toolData["Gardening Tools"]["Line Trimmers"].add(new Tool("Ryobi 18V Telescropic Hedge Trimmer", 3));

            toolData["Gardening Tools"]["Line Trimmers"].add(new Tool("Ozito 500W Electric Pole Hedge Trimmer", 5));

            toolData["Gardening Tools"]["Line Trimmers"].add(new Tool("same", 2));

            toolData["Gardening Tools"]["Lawn Mowers"].add(new Tool("same", 10));
        }

        public static void PopulateMemebrs(iMemberCollection memberData)
        {
            iMember member = GetMember();
            memberData.add(member);
        }
    }
}
