using System;
using System.Collections.Generic;

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
            // tool might be out of stock, so try to add borrower first
            if(aTool.AvailableQuantity > 0 && aMember.Tools.Length<3)
            {
                aTool.addBorrower(aMember);
                aMember.addTool(aTool);
            }
            else
            {
                throw new Exception("Unable to borrow the tool!");
            }
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
            // iterate over the dictionary and store all the tools in a List
            List<iTool> tools = new List<iTool>();
            foreach (string category in toolData.Keys)
            {
                // tools under a tool type
                foreach (string toolType in toolData[category].Keys)
                {
                    Array.ForEach(toolData[category][toolType].toArray(), tool =>
                    {
                        if (tool != null) tools.Add(tool);
                    });
                }
            }



            const int TOP_THREE = 3;
            Queue<iTool> topThree = new Queue<iTool>();
            // find three largest numbers
            for (int i = 0; i < TOP_THREE; i++)
            {
                int max = -1;
                int largestIndex = -1;
                for (int index = 0; index < tools.Count; index++)
                {
                    iTool tool = tools[index];
                    if (tool != null && tool.NoBorrowings >= max)
                    {
                        max = tool.NoBorrowings;
                        largestIndex = index;
                    }
                }
                if (largestIndex == -1) break;
                topThree.Enqueue(tools[largestIndex]);
                tools[largestIndex] = null;
            }

            int num = 1;
            while (topThree.Count != 0)
            {
                iTool tool = topThree.Dequeue();
                Console.WriteLine($"{num}. {tool.Name,-50} --- Borrowed {tool.NoBorrowings} times");
                num++;
            }
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

