using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Assignment
{
    class StaffMenu : Menu
    {
        public StaffMenu() : base("Staff Menu", STAFF_MENU_OPTIONS) { }
        public override void Display()
        {
            Console.Clear();
            Console.WriteLine(greeting);
            base.Display();
            Console.Clear();
            switch (UserOption)
            {
                case 1:
                    HandleAddNewTool();
                    break;
                case 2:
                    HandleAddToolQty();
                    break;
                case 3:
                    HandleRemoveToolQty();
                    break;
                case 4:
                    HandleMemeberRegister();
                    break;
                case 5:
                    HandleMemeberRemove();
                    break;
                case 6:
                    HandleFindContactNumber();
                    break;

                case 0:
                    new MainMenu().Display();
                    break;
            }
        }

        

        private void HandleAddNewTool()
        {
            string[] categories = Data.GetCategories();

            string title = "Add a New Tool to Library";
            Console.WriteLine(title + "\n");
            Console.WriteLine(Line(title));
            // TODO: input validation
            Console.Write("Enter the name of the new Tool: ");
            string toolName = Console.ReadLine();
            Console.WriteLine();

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
                // save the state
                state.ToolCategory = category;
                state.ToolType = toolType;

                try
                {
                    // Add a new tool to the tool type
                    iTool tool = new Tool(toolName, 0);
                    toolLibrarySystem.add(tool);

                    // Display all the tools in the selected tool type again
                    toolLibrarySystem.displayTools(toolType);
                    Console.WriteLine($"Successfully added a new tool with name - {toolName} to library!");
                }
                catch(ArgumentException e)
                {
                    // Display all the tools in the selected tool type again
                    toolLibrarySystem.displayTools(toolType);
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Unable to add this tool to library. No tool type is selected!");
            }
            Console.ReadLine();
            // go back to staff menu
            GoBack();
        }
        private void HandleAddToolQty()
        {
            string title = "Update Existing Tool Stock Level";
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
                if(tools.Length==0)
                {
                    Console.WriteLine($"There is no tool under {toolType} tool type.");
                }
                else
                {
                    // Display all the tools of the selected tool type
                    toolLibrarySystem.displayTools(toolType);
                    // Select a tool from the tool list
                    int toolNumber = GetUserOption("Select a tool from the table - ", 1, tools.Length);
                    int quantity = GetUserOption("Enter the quantity to add into the library - ", 1, int.MaxValue);
                    Console.WriteLine();
                    // Add the quantity of the tool
                    iTool tool = tools[toolNumber - 1];
                    toolLibrarySystem.add(tool, quantity);
                    Console.WriteLine($"Updated the quantity of the tool in the library to {tool.Quantity}");
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
        private void HandleRemoveToolQty()
        {
            string title = "Delete Existing Tool from Library";
            Console.WriteLine(title + "\n");
            Console.WriteLine(Line(title) + "\n");
            // Display all the nine (9) tool categories
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
                // Display all the tools of the selected tool type
                toolLibrarySystem.displayTools(toolType);

                // Select a tool from the tool list
                int toolNumber = GetUserOption("Select a tool from the table - ", 1, tools.Length);

                // Input the number of pieces of the tool to be removed
                int quantity = GetUserOption("Enter the quantity to remove from the library - ", 1, int.MaxValue);
                Console.WriteLine();
                iTool tool = tools[toolNumber - 1];
                try
                {
                    /*
                        If the number of pieces of the tool is not more than the number of 
                        pieces that are currently in the library, reduce the total quantity and 
                        the available quantity of the tool
                     */
                    toolLibrarySystem.delete(tool, quantity);
                    Console.WriteLine($"Decreased the quantity of the tool in the library to {tool.Quantity}");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            else
            {
                Console.WriteLine("There is no tool under this category!");
            }
            Console.ReadLine();
            // go back to staff menu
            GoBack();
        }
        private void HandleMemeberRegister()
        {
            const int NUM_DIGITS = 4;
            string title = "Register New Member with Library";
            Console.WriteLine(title + "\n");
            Console.WriteLine(Line(title) + "\n");
            string firstName = GetStringInput("Enter the first name of the new user - ");
            string lastName = GetStringInput("Enter the last name of the new user - ");
            string phoneNumber = GetNumberInput("Enter the mobile number of the new user - ").ToString();
            string PIN = GetPIN("Enter PIN - ", NUM_DIGITS); //  4 digits PIN
            // add to the database
            iMember member = new Member(firstName, lastName, phoneNumber, PIN);
            try
            {
                toolLibrarySystem.add(member);
                string result = $"Added {member.FirstName} {member.LastName} successfully as a new memebr\n";

                Console.WriteLine(result);

            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
            // go back to staff menu
            GoBack();
        }
        private void HandleMemeberRemove()
        {
            string title = "Remove a Member from Library";
            Console.WriteLine(title + "\n");
            Console.WriteLine(Line(title) + "\n");
            string firstName = GetStringInput("Enter the first name of the user - ");
            string lastName = GetStringInput("Enter the last name of the user - ");
            
            
            // remove from the database
            iMember member = new Member(firstName, lastName);
            try
            {
                toolLibrarySystem.delete(member);
                string result = $"Successfully removed the member {member.FirstName} {member.LastName} from library!\n";
                Console.WriteLine(result);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
            // go back to staff menu
            GoBack();
        }
        private void HandleFindContactNumber()
        {
            string title = "Find the Contact Number of a Member";
            Console.WriteLine(title + "\n");
            Console.WriteLine(Line(title) + "\n");
            string firstName = GetStringInput("Enter the first name of the user - ");
            string lastName = GetStringInput("Enter the last name of the user - ");

            
            iMember member = new Member(firstName, lastName);
            bool exist = memberData.search(member);
            if(exist)
            {
                Console.WriteLine($"{member}'s Contact Number is {member.ContactNumber}");
            }
            else
            {
                Console.WriteLine($"{member} does not exist!");
            }
            Console.ReadLine();
            // go back to staff menu
            GoBack();
        }
        /// <summary>
        /// Go back to the menu.
        /// </summary>
        private void GoBack()
        {
            Display();
        }
   
        private static string GetPIN(string query, int numDigits)
        {
            Regex rx = new Regex(@"[0-9]"+"{"+numDigits+"}", RegexOptions.Compiled);
            bool valid = false;
            string pin = null;
            while(!valid)
            {
                Console.Write(query);
                pin = Console.ReadLine();
                valid = rx.IsMatch(pin);
                if(!valid)
                {
                    Console.WriteLine($"PIN must be a {numDigits}-digit integer.\n");
                }
            }
            return pin;
        }
    }
}
