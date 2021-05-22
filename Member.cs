using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Assignment
{
    class Member : iMember
    {
        private string firstName;
        private string lastName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = CaptalizeFirst(value);
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = CaptalizeFirst(value);
            }
        }
        public string ContactNumber { get; set; }
        public string PIN { get; set; }

        public string[] Tools
        {
            get
            {
                string[] toolNames = new string[toolObjects.Count];
                for(int i =0;i<toolObjects.Count;i++)
                {
                    toolNames[i] = toolObjects[i].Name;
                }
                return toolNames;
            }
        }
        
        private const int MAX_TOOLS = 3;
        private List<iTool> toolObjects;
        public Member() { }
        public Member(string firstName, string lastName, string contactNumber, string pin)
        {
            FirstName = firstName;
            LastName = lastName;
            ContactNumber = contactNumber;
            PIN = pin;
            toolObjects = new List<iTool>(MAX_TOOLS);
        }
        public Member(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            toolObjects = new List<iTool>(MAX_TOOLS);
        }
        public void addTool(iTool aTool)
        {
            if (toolObjects.Count< MAX_TOOLS)
            {
                toolObjects.Add(aTool);
            }
            else
            {
                throw new IndexOutOfRangeException("A memebr cannot borrow more than three tools at the same time.");
            }
        }

        public void deleteTool(iTool aTool)
        {
            if (toolObjects.Count == 0) throw new Exception("This member is not holding any tools.");
            int toolId = aTool.GetHashCode();

            // find the tool by using id (index) 
            iTool tool =  toolObjects[toolId];
            if (tool.Name != aTool.Name) throw new Exception("Tool id and tool name does not match!");

            // delete the tool
            if (tool != null)
            {
                tool.deleteBorrower(this);
                toolObjects.RemoveAt(toolId);
            }
            else
            {
                throw new ArgumentException($"Failed to return the tool. This memebr did not borrow the tool with Name: {aTool.Name}");
            }
        }

        private static string GetFullName(iMember member)
        {
            return $"{member.FirstName} {member.LastName}";
        }
        /// <summary>
        /// Captalize the first letter and make the rest of the letters lower case.
        /// </summary>
        /// <param name="str">The string to be converted</param>
        /// <returns>A captalized string</returns>
        private static string CaptalizeFirst(string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }

        public int CompareTo(iMember other)
        {
            return string.Compare(GetFullName(this), GetFullName(other));
        }
        /// <summary>
        /// A string containing the first name, lastname, and contact phone number of this memeber
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetFullName(this) +" "+ ContactNumber;
        }
    }
}

