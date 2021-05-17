using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assignment
{
    static class Store
    {
        /// <summary>
        /// Saves the state to the disk
        /// </summary>
        /// <param name="newState">The state to save</param>
        public static void SaveState(State newState)
        {
            const string PATH = "state.json";
            string jsonString = JsonSerializer.Serialize(newState);
            File.WriteAllText(PATH, jsonString);
        }

        /// <summary>
        /// Loads the state from disk.
        /// </summary>
        /// <returns>State {"UserType", "ToolCategory","ToolType"}</returns>
        public static State GetState()
        {
            const string PATH = "state.json";
            string jsonString = File.ReadAllText(PATH);
            State state= JsonSerializer.Deserialize<State>(jsonString);

            return state;
        }
    }
    /// <summary>
    /// Represents the current of the App.
    /// </summary>
    class State
    {
        public string UserType { get; set; }
        public string ToolCategory { get; set; }
        public string ToolType { get; set; }
        public State() { }
        public State(string toolCategory, string toolType)
        {
            ToolCategory = toolCategory;
            ToolType = toolType;
        }
        public State(string userType, string toolCategory, string toolType)
        {
            UserType = userType;
            ToolCategory = toolCategory;
            ToolType = toolType;
        }
    }
}
