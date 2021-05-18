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
                // save user input to state
                state.ToolCategory = category;
                state.ToolType = toolType;

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

                toolLibrarySystem.displayTools(toolType);

                int quantity = GetUserOption("Enter the quantity to add into the library - ", 1, int.MaxValue);
                Console.WriteLine();
                toolLibrarySystem.add(null, quantity);
                Console.WriteLine($"Updated the quantity of the tool in the library to {quantity}");
            }
            else
            {
                Console.WriteLine("There is no tool under this category!");
            }
            Console.ReadLine();
            // go back to staff menu
            GoBack();
        }

        private void HandleReturnTool()
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

                toolLibrarySystem.displayTools(toolType);

                int quantity = GetUserOption("Enter the quantity to remove from the library - ", 1, int.MaxValue);
                Console.WriteLine();
                toolLibrarySystem.delete(null, quantity);

            }
            else
            {
                Console.WriteLine("There is no tool under this category!");
            }
            Console.ReadLine();
            // go back to staff menu
            GoBack();
        }

        private void HandleListToolsHolding()
        {
            string title = "Register New Member with Library";
            Console.WriteLine(title + "\n");
            Console.WriteLine(Line(title) + "\n");
            string firstName = GetStringInput("Enter the first name of the new user - ");
            string lastName = GetStringInput("Enter the last name of the new user - ");
            string phoneNumber = GetStringInput("Enter the mobile number of the new user - ");
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

        private void HandleDisplayToThree()
        {
            throw new NotImplementedException();
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
