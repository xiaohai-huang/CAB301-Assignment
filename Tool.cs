using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class Tool : iTool
    {
        public string Name { get; set; }
        public int Quantity { get;  set; }
        public int AvailableQuantity { get;  set; }
        public int NoBorrowings
        {
            get
            {
                return borrowers.Number;
            }
            set
            {
                throw new NotImplementedException("Setter for NoBorrowings is not supported!");
            }
        }

        public iMemberCollection GetBorrowers
        {
            get
            {
                return borrowers;
            }
        }
        private iMemberCollection borrowers;
        public Tool(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
            borrowers = new MemberCollection();
        }

        public void addBorrower(iMember aMember)
        {
            borrowers.add(aMember);
        }

        public void deleteBorrower(iMember aMember)
        {
            borrowers.delete(aMember);
        }
    }
}
