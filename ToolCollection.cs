using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class ToolCollection : iToolCollection
    {
        public int Number
        {
            get
            {
                return number;
            }
        }
        private const int MAX_LENGTH = 30;
        private iTool[] tools;
        private int number;

        public ToolCollection()
        {
            number = 0;
            tools = new Tool[MAX_LENGTH];
        }

        public void add(iTool aTool)
        {
            if(Number < MAX_LENGTH)
            {
                // Find an empty slot to insert
                int index = Array.FindIndex(tools, tool => tool==null);
                tools[index] = aTool;
                number++;
            }
            throw new IndexOutOfRangeException("Failed to insert a new tool. The collection is full.");
        }

        public void delete(iTool aTool)
        {
            int index = Array.FindIndex(tools, tool => tool.Name == aTool.Name);
            if (index != -1)
            {
                tools[index] = null;
                number--;
            }
            throw new ArgumentException($"The tool with name: {aTool.Name} does not exist in this collection.");
        }

        public bool search(iTool aTool)
        {
            return Array.FindIndex(tools, tool => tool.Name == aTool.Name) != -1;
        }

        public iTool[] toArray()
        {
            // Remove nulls
            iTool[] result = new iTool[Number];
            int index = 0;
            Array.ForEach(tools, tool =>
            {
                if (tool != null)
                {
                    result[index] = tool;
                    index++;
                }
            });
            return result;
        }
    }
}
