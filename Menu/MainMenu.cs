using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class MainMenu:Menu
    {
        public MainMenu():base("Main Menu", Menu.MAIN_MENU_OPTIONS) {}

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
        //private const string USER_NAME = "staff";
        //private const string PASSWORD = "today123";
        private const string USER_NAME = "s";
        private const string PASSWORD = "123";
        private void HandleStaffLogin()
        {
            string username=null;
            string passowrd=null;
            bool valid = username == USER_NAME && passowrd == PASSWORD;

            Console.Clear();
            Console.WriteLine("Staff Login\n");


            while (!valid)
            {
                Console.Write("username: ");
                username = Console.ReadLine();

                Console.Write("password: ");
                passowrd = Console.ReadLine();
                valid = username == USER_NAME && passowrd == PASSWORD;
                if (!( username==null && passowrd == null) && !valid)
                {
                    Console.WriteLine("Username or password is invalid!\n");
                }
            }
            Console.Clear();
            new StaffMenu().Display();
        }

        private void HandleMemebrLogin()
        {
            
        }
    }
}
