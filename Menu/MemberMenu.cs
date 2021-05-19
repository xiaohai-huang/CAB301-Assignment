using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class MemberMenu : Menu
    {
        public MemberMenu() : base("Member Menu", MEMBER_MENU_OPTIONS) { }

        public override void Display()
        {
            Console.Clear();
            Console.WriteLine(greeting);
            base.Display();
            Console.Clear();
            switch (UserOption)
            {
                case 1:
                    HandleDisplayToolsByType();
                    break;
                case 2:
                    HandleBorrowTool();
                    break;
                case 3:
                    HandleReturnTool();
                    break;
                case 4:
                    HandleListToolsHolding();
                    break;
                case 5:
                    HandleDisplayToThree();
                    break;

                case 0:
                    new MainMenu().Display();
                    break;
            }
        }



        private void HandleDisplayToolsByType()
        {
            string[] categories = Data.GetCategories();

            string title = "Display tools by tool type";
            Console.WriteLine(title + "\n");
            Console.WriteLine(Line(title));

            // Display all the nine (9) tool categories
            Menu categoryMenu = new Menu("Select the category", categories, false);
            categoryMenu.Display();

            // Select a category
            string category = categories[categoryMenu.UserOption - 1];

            // Display all the tool types of the selected category
            string[] toolTypes = Data.GetToolTypesByCategory(category);
            Console.WriteLine($"Tool Types under {category} category");
            Menu toolTypesMenu = new Menu("Select the tool type", toolTypes, false);
            toolTypesMenu.Display();


            // a category might not have any tool types
            if (toolTypesMenu.UserOption != -1)
            {
                // Select a tool type
                string toolType = toolTypes[toolTypesMenu.UserOption - 1];
                // save user input to state
                state.ToolCategory = category;
                state.ToolType = toolType;

                // Display the information about all the tools of the selected tool type
                toolLibrarySystem.displayTools(toolType);
            }
            else
            {
                Console.WriteLine("There is no tool under this category!");
            }

            Console.ReadLine();
            // go back to staff menu
            GoBack();
        }

        private void HandleBorrowTool()
        {
            string title = "Borrow Tool from Tool Library";
            Console.WriteLine(title + "\n");
            Console.WriteLine(Line(title) + "\n");

            // Display all the tool categories
            string[] categories = Data.GetCategories();
            Menu categoryMenu = new Menu("Select a tool category", categories, false);
            categoryMenu.Display();
            // Select a category
            string category = categories[categoryMenu.UserOption - 1];
            // Display all the tool types of the selected category
            string[] toolTypes = Data.GetToolTypesByCategory(category);
            Console.WriteLine($"Tool Types under {category} category");
            Menu toolTypesMenu = new Menu("Select a tool type", toolTypes, false);
            toolTypesMenu.Display();



            // a category might not have any tool types
            if (toolTypesMenu.UserOption != -1)
            {
                // Select a tool type
                string toolType = toolTypes[toolTypesMenu.UserOption - 1];
                // save user input to state
                state.ToolCategory = category;
                state.ToolType = toolType;
                iTool[] tools = toolData[state.ToolCategory][state.ToolType].toArray();
                if (tools.Length == 0)
                {
                    Console.WriteLine($"There is no tool under {toolType} tool type.");
                }
                else
                {
                    // Display all the tools of the selected tool type
                    toolLibrarySystem.displayTools(toolType);
                    // Select a tool from the tool list
                    int toolNumber = GetUserOption("Select a tool from the table - ", 1, tools.Length);

                    Console.WriteLine();
                    // borrow the tool
                    iTool tool = tools[toolNumber - 1];
                    try
                    {
                        iMember member = state.User;
                        toolLibrarySystem.borrowTool(member, tool);
                        Console.WriteLine($"{member.FirstName} {member.LastName} borrowed {tool.Name} from the library.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("There is no tool type under this category!");
            }
            Console.ReadLine();
            // go back to staff menu
            GoBack();
        }

        private void HandleReturnTool()
        {
            string[] tools = toolLibrarySystem.listTools(state.User);
            string title = "Return Tool to Tool Library";
            Console.WriteLine(title + "\n");
            Console.WriteLine(Line(title) + "\n");
            if (tools.Length != 0)
            {

                toolLibrarySystem.displayBorrowingTools(state.User);
                Console.WriteLine();
                // zero based
                int toolID = GetUserOption("Select a tool to return - ", 1, tools.Length) - 1;
                iTool toolToReturn = new Tool(tools[toolID], 0, toolID);
                try
                {
                    toolLibrarySystem.returnTool(state.User, toolToReturn);
                    Console.WriteLine($"{state.User.FirstName} {state.User.LastName} returned {toolToReturn.Name} to the library.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("This user is not holding any tools!");
            }
            Console.ReadLine();
            // go back to staff menu
            GoBack();
        }

        private void HandleListToolsHolding()
        {
            iMember member = state.User;
            string title = $"Tools on loan to {member.FirstName} {member.LastName}";
            Console.WriteLine(title + "\n");
            Console.WriteLine(Line(title) + "\n");

            toolLibrarySystem.displayBorrowingTools(state.User);

            Console.WriteLine();
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            // go back to staff menu
            GoBack();
        }

        private void HandleDisplayToThree()
        {
            toolLibrarySystem.displayTopTHree();
            Console.ReadLine();
            GoBack();
        }

        /// <summary>
        /// Go back to the menu.
        /// </summary>
        private void GoBack()
        {
            Display();
        }


    }
}
