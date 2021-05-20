using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class MainMenu : Menu
    {
        public MainMenu() : base("Main Menu", MAIN_MENU_OPTIONS) { }

        public override void Display()
        {
            Console.WriteLine(greeting);
            base.Display();
            switch (UserOption)
            {
                case 1:
                    HandleStaffLogin();
                    break;
                case 2:
                    HandleMemebrLogin();
                    break;
                case 0:
                    Console.WriteLine("Bye~");
                    Environment.Exit(0);
                    break;
            }
        }

        // TODO: change password to "today123"
        private const int MAX_ATTEMPTS = 3;
        //private const string USER_NAME = "staff";
        //private const string PASSWORD = "today123";
        private const string USER_NAME = "s";
        private const string PASSWORD = "123";
        private void HandleStaffLogin()
        {
            string username;
            string passowrd;
            bool valid = false;

            Console.Clear();
            Console.WriteLine("Staff Login\n");

            int attempts = 0;
            while (!valid)
            {
                if (attempts >= MAX_ATTEMPTS) break;
                attempts++;

                username = GetStringInput("username: ");

                passowrd = GetStringInput("passowrd: ");

                // validation
                valid = username == USER_NAME && passowrd == PASSWORD;
                if (!(username == null && passowrd == null) && !valid)
                {
                    Console.WriteLine("Username or password is incorrect!\n");
                }
            }
            // login failed too many times
            if (attempts >= MAX_ATTEMPTS && !valid)
            {
                Console.WriteLine("You have tried too many times!");
                Console.ReadLine();
                Console.Clear();
                Display();
            }
            else
            {
                Console.Clear();
                new StaffMenu().Display();
            }
        }
        /// <summary>
        /// The number of digits of PIN
        /// </summary>
        private const int NUM_DIGITS = 4;
        private void HandleMemebrLogin()
        {
            iMember member = null;
            bool valid = false;

            Console.Clear();
            Console.WriteLine("Member Login\n");

            int attempts = 0;
            while (!valid)
            {
                if (attempts >= MAX_ATTEMPTS) break;
                attempts++;

                // obtain user input
                string username = GetStringInput("username (FirstNameLastName): ");

                string passowrd = GetPIN("password: ", NUM_DIGITS);

                // retrieve the user from database
                try
                {

                    string[] names = SplitName(username);

                    member = new Member(names[0], names[1]);
                    bool exist = memberData.search(member);


                    valid = exist && passowrd == member.PIN;
                    if (!(username == null && passowrd == null) && !valid)
                    {
                        throw new Exception("Authentication Failed!");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Username or password is incorrect!\n");
                }
            }
            // login failed too many times
            if (attempts >= MAX_ATTEMPTS && !valid)
            {
                Console.WriteLine("You have tried too many times!");
                Console.ReadLine();
                Console.Clear();
                Display();
            }
            else
            {
                // save the user info in state
                state.User = member;
                Console.Clear();
                new MemberMenu().Display();
            }
        }

        /// <summary>
        /// Split the name into first name and last name - SmithJoe -> [Smith, Joe]
        /// </summary>
        /// <param name="name">The name to split</param>
        /// <returns>[firstName, lastName]</returns>
        private static string[] SplitName(string name)
        {
            string[] names = new string[2];

            int indexOfLastName = -1;
            int index = 0;
            foreach (char ch in name)
            {
                if (char.IsUpper(ch))
                {
                    indexOfLastName = index;
                }
                index++;
            }

            names[0] = name.Substring(0, indexOfLastName);
            names[1] = name.Substring(indexOfLastName);
            return names;
        }
    }
}
