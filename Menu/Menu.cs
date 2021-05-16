using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class Menu
    {
        public string Title
        {
            get; private set;
        }

        public string[] Options
        {
            get;
            private set;
        }

        public int UserOption
        {
            get;
            private set;
        }

        public static readonly string[] MAIN_MENU_OPTIONS = new string[] { "Staff Login", "Member Login", "Exit" };
        public static readonly string[] STAFF_MENU_OPTIONS = new string[]
        {
            "Add a new tool",
            "Add new pieces of an existing tool",
            "Remove some pieces of a tool",
            "Register a new member",
            "Find the contact number of a member",
            "Return to main menu"
        };

        public static readonly string[] MEMBER_MENU_OPTIONS = new string[] 
        { 
            "Display all the tools of a tool type",
            "Borrow a tool",
            "Return a tool",
            "List All the tools that I am renting",
            "Display top three (3) most frequently rented tools",
            "Return to main menu"
        };

        public Menu(string title, string[] options)
        {
            Title = title;
            Options = options;
        }
        /// <summary>
        /// Display the menu
        /// </summary>
        public virtual void Display()
        {
            string LINE = new string('=', 10);
            string top = $"{LINE}{Title}{LINE}";
            string bottom = $"{LINE}{new string('=', Title.Length)}{LINE}";
            string query = $"Please make a selection (1-{Options.Length - 1}, or 0 to return to main menu): ";
            int optionNumber = 1;

            Console.WriteLine(top);
            Array.ForEach(Options, (option) =>
            {
                // the last option is exit or go back
                if (optionNumber == Options.Length)
                {
                    Console.WriteLine($"0. {option}");
                }
                else
                {
                    Console.WriteLine($"{optionNumber}. {option}");
                }

                optionNumber++;
            });
            Console.WriteLine(bottom);
            Console.WriteLine();

            GetUserInput(query);
        }

        private void GetUserInput(string query)
        {
            bool valid = false;
            while (!valid)
            {

                Console.Write(query);
                string userInput = Console.ReadLine();
                int option;
                try
                {
                    option = int.Parse(userInput);
                }
                catch (Exception)
                {
                    Console.WriteLine("Input must be a number!\n");
                    valid = false;
                    continue;
                }

                if (0 <= option && option < Options.Length)
                {
                    valid = true;
                    UserOption = option;
                }
                else
                {
                    valid = false;
                    Console.WriteLine($"Input must be within range (1-{Options.Length - 1}, or 0)\n");
                }

            }

            Console.Clear();
        }


    }
}
