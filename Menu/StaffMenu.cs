﻿using System;
using System.Collections.Generic;
using System.Text;

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

            Menu categoryMenu = new Menu("Select the category", categories, false);
            categoryMenu.Display();
            string category = categories[categoryMenu.UserOption - 1];
            string[] toolTypes = Data.GetToolTypesByCategory(category);
            Console.WriteLine($"Tool Types under {category} category");
            Menu toolTypesMenu = new Menu("Select the tool type", toolTypes, false);
            toolTypesMenu.Display();

            // a category might not have any tool types
            if (toolTypesMenu.UserOption != -1)
            {
                string toolType = toolTypes[toolTypesMenu.UserOption - 1];
                // save the state
                state.ToolCategory = category;
                state.ToolType = toolType;

                iTool tool = new Tool(toolName, 0);
                toolLibrarySystem.add(tool);
            }
            else
            {
                Console.WriteLine("Unable to add this tool to library. No tool type is selected!");
            }
            Console.WriteLine($"Successfully added a new tool with name - {toolName} to library!");
            Console.ReadLine();
            // go back to staff menu
            GoBack();
        }
        private void HandleAddToolQty()
        {
            string title = "Update Existing Tool Stock Level";
            Console.WriteLine(title + "\n");
            Console.WriteLine(Line(title) + "\n");

            string[] categories = Data.GetCategories();
            Menu categoryMenu = new Menu("Select a tool category", categories, false);
            categoryMenu.Display();
            string category = categories[categoryMenu.UserOption - 1];
            string[] toolTypes = Data.GetToolTypesByCategory(category);
            Console.WriteLine($"Tool Types under {category} category");
            Menu toolTypesMenu = new Menu("Select a tool type", toolTypes, false);
            toolTypesMenu.Display();



            // a category might not have any tool types
            if (toolTypesMenu.UserOption != -1)
            {
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
                    toolLibrarySystem.displayTools(toolType);

                    int toolNumber = GetUserOption("Select a tool from the table - ", 1, tools.Length);
                    int quantity = GetUserOption("Enter the quantity to add into the library - ", 1, int.MaxValue);
                    Console.WriteLine();
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

            string[] categories = Data.GetCategories();
            Menu categoryMenu = new Menu("Select a tool category", categories, false);
            categoryMenu.Display();
            string category = categories[categoryMenu.UserOption - 1];
            string[] toolTypes = Data.GetToolTypesByCategory(category);
            Console.WriteLine($"Tool Types under {category} category");
            Menu toolTypesMenu = new Menu("Select a tool type", toolTypes, false);
            toolTypesMenu.Display();



            // a category might not have any tool types
            if (toolTypesMenu.UserOption != -1)
            {
                string toolType = toolTypes[toolTypesMenu.UserOption - 1];
                // save user input to state
                state.ToolCategory = category;
                state.ToolType = toolType;

                iTool[] tools = toolData[state.ToolCategory][state.ToolType].toArray();
                toolLibrarySystem.displayTools(toolType);

                int toolNumber = GetUserOption("Select a tool from the table - ", 1, tools.Length);

                int quantity = GetUserOption("Enter the quantity to remove from the library - ", 1, int.MaxValue);
                Console.WriteLine();
                iTool tool = tools[toolNumber - 1];
                try
                {
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
            string title = "Register New Member with Library";
            Console.WriteLine(title + "\n");
            Console.WriteLine(Line(title) + "\n");
            string firstName = GetStringInput("Enter the first name of the new user - ");
            string lastName = GetStringInput("Enter the last name of the new user - ");
            string phoneNumber = GetNumberInput("Enter the mobile number of the new user - ").ToString();
            string PIN = GetStringInput("Enter PIN - ");
            string result = $"Added {firstName} {lastName} successfully as a new memebr\n";
            // add to the database
            iMember member = new Member(firstName, lastName, phoneNumber, PIN);
            try
            {
                toolLibrarySystem.add(member);
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
            
            string result = $"Successfully removed the member {firstName} {lastName} from library!\n";
            // remove from the database
            iMember member = new Member(firstName, lastName);
            try
            {
                toolLibrarySystem.delete(member);
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
    }
}
