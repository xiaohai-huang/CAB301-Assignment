using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class StaffMenu:Menu
    {
        public StaffMenu(): base("Staff Menu", Menu.STAFF_MENU_OPTIONS) {}

        public override void Display()
        {
            Console.Clear();
            Console.WriteLine(greeting);
            base.Display();
            Console.Clear();
            switch (UserOption){
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


                case 0:
                    new MainMenu().Display();
                    break;
            }
        }

    

        private void HandleAddNewTool()
        {
            string[] categories = Data.GetCategories();

            string title = "Add a New Tool to Library";
            Console.WriteLine(title+"\n");
            Console.WriteLine(Line(title));
            // TODO: input validation
            Console.Write("Enter the name of the new Tool: ");
            string toolName = Console.ReadLine();
            Console.WriteLine();

            Menu categoryMenu =  new Menu("Select the category", categories, false);
            categoryMenu.Display();
            string category = categories[categoryMenu.UserOption - 1];
            string[] toolTypes = Data.GetToolTypesByCategory(category);
            Console.WriteLine($"Tool Types under {category} category");
            Menu toolTypesMenu = new Menu("Select the tool type",toolTypes, false);
            toolTypesMenu.Display();

            // a category might not have any tool types
            if (toolTypesMenu.UserOption != -1)
            {
                string toolType = toolTypes[toolTypesMenu.UserOption - 1];
                // save the state
                State state = new State(category, toolType);
                Store.SaveState(state);

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
            Console.WriteLine(title+"\n");
            Console.WriteLine(Line(title)+"\n");

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
                State state = new State(category, toolType);
                Store.SaveState(state);

                toolLibrarySystem.displayTools(toolType);

                int quantity = GetUserOption("Enter the quantity to add into the library - ", 1, int.MaxValue);
                Console.WriteLine();
                toolLibrarySystem.add(null,quantity);
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
                State state = new State(category, toolType);
                Store.SaveState(state);

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

        private void HandleMemeberRegister()
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

        /// <summary>
        /// Go back to the menu.
        /// </summary>
        private void GoBack()
        {
            Display();
        }


    }
}
