using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class ToolLibrarySystem : iToolLibrarySystem
    {
        private static Dictionary<string, Dictionary<string, iToolCollection>> toolData;
        private static iMemberCollection memebrData;
        private static iTool selectedTool;
        public ToolLibrarySystem(iMemberCollection memebrData)
        {
            if(toolData==null)
            {
                toolData = new Dictionary<string, Dictionary<string, iToolCollection>>();
                // populate categories and tool types
                string[] categories = Data.GetCategories();
                Array.ForEach(categories, category =>
            {
                toolData.Add(category, new Dictionary<string, iToolCollection>());
                // add tool types to the category
                string[] toolTypes = Data.GetToolTypesByCategory(category);
                Array.ForEach(toolTypes, toolType =>
                {
                    toolData[category].Add(toolType, new ToolCollection());
                });
            });
            }

            ToolLibrarySystem.memebrData = memebrData;
        }

        public void add(iTool aTool)
        {
            State state = Store.GetState();
            toolData[state.ToolCategory][state.ToolType].add(aTool);
        }

        public void add(iTool aTool, int quantity)
        {
            // use the tool from "displayTools" method
            if (aTool == null) aTool = selectedTool;

            State state = Store.GetState();
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
            throw new NotImplementedException();
        }

        public void delete(iTool aTool)
        {
            // use the tool from "displayTools" method
            if (aTool == null) aTool = selectedTool;
            State state = Store.GetState();
            iToolCollection tools = toolData[state.ToolCategory][state.ToolType];
            tools.delete(aTool);
        }

        public void delete(iTool aTool, int quantity)
        {
            // use the tool from "displayTools" method
            if (aTool == null) aTool = selectedTool;
            State state = Store.GetState();
            iToolCollection tools = toolData[state.ToolCategory][state.ToolType];
            iTool tool = Array.Find(tools.toArray(), tool => tool.Name == aTool.Name);
            try
            {
                tool.Quantity -= quantity;
                Console.WriteLine($"Updated the quantity of the tool in the library to {Math.Max(tool.Quantity, 0)}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void delete(iMember aMember)
        {
            throw new NotImplementedException();
        }

        public void displayBorrowingTools(iMember aMember)
        {
            throw new NotImplementedException();
        }

        public void displayTools(string aToolType)
        {
            State state = Store.GetState();
            iTool[] tools = toolData[state.ToolCategory][aToolType].toArray();
            int num = 1;
            string title = "Tool Type List of Tools";
            string heading = $"{"ToolName",-30} {"Available",10} {"Total",10}";

            // UI
            Console.WriteLine(title);
            Console.WriteLine(Menu.Line(70));
            Console.WriteLine(heading);
            Console.WriteLine(Menu.Line(70));

            // display tools
            Array.ForEach(tools, tool =>
            {
                string toolName = $"{num}. {tool.Name}";
                string line = $"{toolName,-30} {tool.AvailableQuantity,10} {tool.Quantity,10}";
                Console.WriteLine(line);
                num++;
            });

            int option = Menu.GetUserOption("Select a tool - ",1,tools.Length);
            selectedTool = tools[option - 1];
        }

        public void displayTopTHree()
        {
            throw new NotImplementedException();
        }

        public string[] listTools(iMember aMember)
        {
            throw new NotImplementedException();
        }

        public void returnTool(iMember aMember, iTool aTool)
        {
            throw new NotImplementedException();
        }
    }
}

