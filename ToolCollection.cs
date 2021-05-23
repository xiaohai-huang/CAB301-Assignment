using System;

namespace Assignment
{
    class ToolCollection : iToolCollection
    {
        public int Number { get; private set; }
        private const int MAX_LENGTH = 30;
        private iTool[] tools;

        public ToolCollection()
        {
            Number = 0;
            tools = new Tool[MAX_LENGTH];
        }

        public void add(iTool aTool)
        {
            if(Number < MAX_LENGTH)
            {
                // Find an empty slot to insert
                int index = Array.FindIndex(tools, tool => tool==null);
                tools[index] = aTool;
                Number++;
            }
            else
            {
                throw new IndexOutOfRangeException("Failed to insert a new tool. The collection is full.");
            }
        }

        public void delete(iTool aTool)
        {
            int index = Array.FindIndex(tools, tool => tool.Name == aTool.Name);
            if (index != -1)
            {
                tools[index] = null;
                Number--;
            }
            throw new ArgumentException($"The tool with name: {aTool.Name} does not exist in this collection.");
        }

        public bool search(iTool aTool)
        {
            // this assumes that Name is the unique identifier of Tool.
            return Array.FindIndex(tools, tool =>  tool?.Name == aTool.Name) != -1;
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
