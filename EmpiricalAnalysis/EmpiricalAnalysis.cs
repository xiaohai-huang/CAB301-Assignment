using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment;

namespace ToolLibrary.EmpiricalAnalysis
{
    class EmpiricalAnalysis
    {
        private ToolLibrarySystem TLS;
        private Random rng;
        private int numCategories = 50;
        private int numToolTypes = 50;
       

        public EmpiricalAnalysis()
        {
            rng = new Random();
            numCategories = rng.Next(1, 20);
            numToolTypes = rng.Next(1, 20);
        }
        public int PopulateRandomData(int numToolsPerType = 30)
        {
            Dictionary<string, Dictionary<string, iToolCollection>> toolData = new Dictionary<string, Dictionary<string, iToolCollection>>();
            for (int i = 0; i < numCategories; i++)
            {
                string category = i.ToString();
                // tool type - tool collection
                var toolType_tools = new Dictionary<string, iToolCollection>();
                for (int j = 0; j < numToolTypes; j++)
                {
                    string toolType = j.ToString();
                    iToolCollection tools = new ToolCollection(numToolsPerType);
                    for(int k =0;k<numToolsPerType;k++)
                    {
                        string toolName = k.ToString();
                        iTool tool = new Tool(toolName, rng.Next())
                        {
                            NoBorrowings = rng.Next()
                        };
                        tools.add(tool);
                    }

                    toolType_tools.Add(toolType,tools);
                }


                toolData.Add(category, toolType_tools);
            }
            TLS = new ToolLibrarySystem(toolData, null, null);

            // iterate over the dictionary and store all the tools in a List
            List<iTool> toolsList = new List<iTool>();
            foreach (string category in toolData.Keys)
            {
                // tools under a tool type
                foreach (string toolType in toolData[category].Keys)
                {
                    Array.ForEach(toolData[category][toolType].toArray(), tool =>
                    {
                        if (tool != null) toolsList.Add(tool);
                    });
                }
            }

            return toolsList.Count;

        }

        public void GetTopThree()
        {
            TLS.displayTopTHree();
        }
        
        
       
    }
}
