using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class ToolLibrarySystem : iToolLibrarySystem
    {
        private static Dictionary<string, Dictionary<string, iToolCollection>> toolData;
        private static iMemberCollection memebrData;
        private static State state;
        public ToolLibrarySystem(Dictionary<string, Dictionary<string, iToolCollection>> toolData, iMemberCollection memebrData, State state)
        {

            ToolLibrarySystem.toolData = toolData;
            ToolLibrarySystem.memebrData = memebrData;
            ToolLibrarySystem.state = state;
        }

        public void add(iTool aTool)
        {
            iToolCollection tools = toolData[state.ToolCategory][state.ToolType];
            bool exist = tools.search(aTool);
            if (exist) throw new ArgumentException("Warning: Failed to add a new tool. Because this tool already exists in this tool type!");

            toolData[state.ToolCategory][state.ToolType].add(aTool);
        }

        public void add(iTool aTool, int quantity)
        {
            iToolCollection oldTools = toolData[state.ToolCategory][state.ToolType];
            iTool oldTool = Array.Find(oldTools.toArray(), tool => tool.Name == aTool.Name);
            oldTool.Quantity += quantity;
        }

        public void add(iMember aMember)
        {
            bool exist = memebrData.search(aMember);
            if (exist) throw new ArgumentException($"The member already exists!");
            memebrData.add(aMember);
        }

        public void borrowTool(iMember aMember, iTool aTool)
        {
            aMember.addTool(aTool);
            aTool.addBorrower(aMember);
        }

        public void delete(iTool aTool)
        {
            iToolCollection tools = toolData[state.ToolCategory][state.ToolType];
            tools.delete(aTool);
        }

        public void delete(iTool aTool, int quantity)
        {
            iToolCollection tools = toolData[state.ToolCategory][state.ToolType];
            iTool tool = Array.Find(tools.toArray(), tool => tool.Name == aTool.Name);
            tool.Quantity -= quantity;
        }

        public void delete(iMember aMember)
        {
            bool exist = memebrData.search(aMember);
            if (!exist) throw new ArgumentException($"The member does not exist!");
            memebrData.delete(aMember);
        }

        public void displayBorrowingTools(iMember aMember)
        {
            string[] tools = aMember.Tools;
            int num = 1;
            Array.ForEach(tools, tool =>
            {
                if (tool == null) return;
                Console.WriteLine($"{num}. {tool}");
                num++;
            });
        }

        public void displayTools(string aToolType)
        {
            iTool[] tools = toolData[state.ToolCategory][aToolType].toArray();
            int num = 1;
            string title = "Tool Type List of Tools";
            string heading = $"{"ToolName",-50} {"Available",10} {"Total",10}";

            // UI
            Console.WriteLine(title);
            Console.WriteLine(Menu.Line(80));
            Console.WriteLine(heading);
            Console.WriteLine(Menu.Line(80));

            // display tools
            Array.ForEach(tools, tool =>
            {
                string toolName = $"{num}. {tool.Name}";
                string line = $"{toolName,-50} {tool.AvailableQuantity,10} {tool.Quantity,10}";
                Console.WriteLine(line);
                num++;
            });
            Console.WriteLine(Menu.Line(80));


        }

        public void displayTopTHree()
        {
            List<iTool> allTools = new List<iTool>();
            foreach(string category in toolData.Keys)
            {
                // tools under a tool type
                foreach(string toolType in toolData[category].Keys)
                {
                    Array.ForEach(toolData[category][toolType].toArray(), tool =>
                    {
                        if(tool!=null) allTools.Add(tool);
                    });
                }
            }
            
            
            
            iTool[] tools = allTools.ToArray();
            // sort in desc order
            // slice first 3
            Array.Sort(tools);
            int num = 1;
            Array.ForEach(tools[..3], tool => {
                Console.WriteLine($"{num}. {tool.Name} num borrowed: {tool.NoBorrowings}");
                num++;
            });
        }

        public string[] listTools(iMember aMember)
        {
            List<string> tools = new List<string>();
            Array.ForEach(aMember.Tools, tool =>
            {
                if (tool != null) tools.Add(tool);
            });
            return tools.ToArray();
        }

        public void returnTool(iMember aMember, iTool aTool)
        {
            aMember.deleteTool(aTool); // test multiple users that are borrowing the same tool
        }
    }
}

