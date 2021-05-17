using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class Tool : iTool
    {
        public string Name { get; set; }
        private int quantity;
        public int Quantity { get {
                return quantity;
            } set { 
                if(value >= GetBorrowers.Number)
                {
                    quantity = value;
                }
                else
                {
                    throw new ArgumentException("Failed to decrease the tool's quantity.");
                }
            } }
        private int availableQuantity;
        public int AvailableQuantity
        {
            get
            {
                availableQuantity = Quantity - GetBorrowers.Number;
                return availableQuantity;
            }
            set { availableQuantity = value; }
        }
        public int NoBorrowings
        {
            get; set;
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
            borrowers = new MemberCollection();
            Quantity = quantity;
        }

        public void addBorrower(iMember aMember)
        {
            borrowers.add(aMember);
            NoBorrowings++;
        }

        public void deleteBorrower(iMember aMember)
        {
            borrowers.delete(aMember);
        }

        public override string ToString()
        {
            return $"{Name} - Available Quantity: {AvailableQuantity} - Quantity: {Quantity}";
        }
    }
}
