using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class MainMenu
    {
        private Menu menu;
        public MainMenu()
        {
            menu = new Menu("Main Menu", Menu.MAIN_MENU_OPTIONS);
        }

        public void Display()
        {
            menu.Display();
            switch (menu.UserOption)
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

        private const string USER_NAME = "staff";
        private const string PASSWORD = "today123";
        private void HandleStaffLogin()
        {
            string username=null;
            string passowrd=null;
            bool valid = username == USER_NAME && passowrd == PASSWORD;
            
            Console.WriteLine("Staff Login\n\n");


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
