using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assignment
{
    /// <summary>
    /// Represents the current of the App.
    /// </summary>
    class State
    {
        /// <summary>
        /// Current user of the app (only support Member)
        /// </summary>
        public iMember User { get; set; }
        public string ToolCategory { get; set; }
        public string ToolType { get; set; }
        public State() { }
        public State(string toolCategory, string toolType)
        {
            ToolCategory = toolCategory;
            ToolType = toolType;
        }
        public State(iMember user, string toolCategory, string toolType)
        {
            User = user;
            ToolCategory = toolCategory;
            ToolType = toolType;
        }
    }
}
