using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class Tool : iTool
    {
        public string Name { get; set; }
        private int quantity;
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (value >= GetBorrowers.Number)
                {
                    quantity = value;
                }
                else
                {
                    throw new ArgumentException("Failed to decrease the tool's quantity.");
                }
            }
        }
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
        private int ID;
        public Tool(string name, int quantity)
        {
            Name = name;
            borrowers = new MemberCollection();
            Quantity = quantity;
        }
        /// <summary>
        /// Used for returning tool
        /// </summary>
        /// <param name="name"></param>
        /// <param name="quantity"></param>
        /// <param name="id">ID of the tool, starts at 0</param>
        public Tool(string name, int quantity, int id)
        {
            Name = name;
            borrowers = new MemberCollection();
            Quantity = quantity;
            ID = id;

        }
        public void addBorrower(iMember aMember)
        {
            if (AvailableQuantity > 0)
            {
                borrowers.add(aMember);
                NoBorrowings++;
            }
            else
            {
                throw new Exception("This tool is out of stock. It is currently not available.");
            }
        }

        public void deleteBorrower(iMember aMember)
        {
            bool exist = borrowers.search(aMember);
            if (exist)
            {
                borrowers.delete(aMember);
            }
            else
            {
                throw new Exception($"{aMember.FirstName} {aMember.LastName} did not borrow this tool - {this.Name}.");
            }
        }

        public override string ToString()
        {
            return $"{Name} - Available Quantity: {AvailableQuantity} - Quantity: {Quantity}";
        }

        public override bool Equals(object obj)
        {
            iTool other = (iTool)obj;
            return this.GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public int CompareTo(iTool other)
        {
            return other.NoBorrowings - NoBorrowings;
        }
    }
}
