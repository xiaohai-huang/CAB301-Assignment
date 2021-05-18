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

        protected iToolLibrarySystem toolLibrarySystem;

        protected string LINE;

        protected string greeting = "Welcome to the Tool Library";

        private bool back;


        public static readonly string[] MAIN_MENU_OPTIONS = new string[] { "Staff Login", "Member Login", "Exit" };
        public static readonly string[] STAFF_MENU_OPTIONS = new string[]
        {
            "Add a new tool",
            "Add new pieces of an existing tool",
            "Remove some pieces of a tool",
            "Register a new member",
            "Remove a memeber",
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

        protected static State state;
        protected static iMemberCollection memberData;
        protected static Dictionary<string, Dictionary<string, iToolCollection>> toolData;
        static Menu()
        {
            state = new State();

            memberData = new MemberCollection();

            // populate (9) categories and tool types
            toolData = new Dictionary<string, Dictionary<string, iToolCollection>>();
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

            // populate data
            Data.PopulateTools(toolData);
            Data.PopulateMemebrs(memberData);
        }
        public Menu(string title, string[] options, bool back = true)
        {
            Title = title;
            Options = options;
            this.back = back;
            LINE = Line(10);
            toolLibrarySystem = new ToolLibrarySystem(toolData, memberData, state);
        }
        /// <summary>
        /// Display the menu.
        /// </summary>
        public virtual void Display()
        {
            string top = $"{LINE}{Title}{LINE}";
            string bottom = $"{LINE}{Line(Title.Length)}{LINE}";
            string query = $"Please make a selection (1-{Options.Length - 1}, or 0): ";
            if (!back) query = $"Please make a selection (1-{Options.Length}): ";
            int optionNumber = 1;

            Console.WriteLine(top);
            Array.ForEach(Options, (option) =>
            {
                // the last option is exit or go back
                if (optionNumber == Options.Length && back)
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
            if (Options.Length == 0)
            {
                Console.WriteLine("Empty");
                UserOption = -1;
            }
            else
            {
                GetUserInput(query);
            }
            Console.WriteLine();

        }

        private void GetUserInput(string query)
        {
            int lowerBound = back ? 0 : 1;
            int upperBound = back ? Options.Length - 1 : Options.Length;
            UserOption = GetUserOption(query, lowerBound, upperBound);
        }

        public static int GetUserOption(string query, int start, int end)
        {
            bool valid = false;
            int lowerBound = start;
            int upperBound = end;
            int option = -1;
            while (!valid)
            {

                Console.Write(query);
                string userInput = Console.ReadLine();

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

                if (lowerBound <= option && option <= upperBound)
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                    Console.WriteLine($"Input must be within range ({lowerBound}-{upperBound})\n");
                }

            }

            return option;
        }

        public static string GetStringInput(string query)
        {
            Console.Write(query);
            return Console.ReadLine();
        }

        public static long GetNumberInput(string query)
        {
            bool valid = false;
            long userInput = -1;
            while(!valid)
            {
                try
                {
                    userInput = long.Parse(GetStringInput(query));
                    valid = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Phone Number must be a number\n");
                    valid = false;
                }
            }

            return userInput;
        }

        public static string Line(int length)
        {
            return new string('=', length);
        }

        /// <summary>
        /// A line (=====) which has the same length of the input string.
        /// </summary>
        /// <param name="str">The input string.</param>
        /// <returns>A line.</returns>
        protected string Line(string str)
        {
            return Line(str.Length);
        }
    }
}
