using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class StaffMenu:Menu
    {
        public StaffMenu(): base("Staff Menu", Menu.STAFF_MENU_OPTIONS)
        {
        }

        public override void Display()
        {
            base.Display();
            switch (UserOption){
                case 1:
                    HandleAddNewTool();
                    break;
                case 2:
                    HandleAddToolQty();
                    break;

                case 0:
                    new MainMenu().Display();
                    break;
            }
        }

        private void HandleAddNewTool()
        {
            Console.WriteLine("Add a new tool!");
        }

        private void HandleAddToolQty()
        {

        }


    }
}
