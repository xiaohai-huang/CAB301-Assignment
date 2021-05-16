﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Assignment
{
    class Member : iMember
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string PIN { get; set; }

        public string[] Tools
        {
            get;
        }
        private const int MAX_TOOLS = 3;
        private int numTools;

        public Member(string firstName, string lastName, string contactNumber, string pin)
        {
            FirstName = firstName;
            LastName = lastName;
            ContactNumber = contactNumber;
            PIN = pin;
            Tools = new string[MAX_TOOLS];
            numTools = 0;
        }

        public void addTool(iTool aTool)
        {
            if (numTools < 3)
            {
                // find an empty slot to insert the tool
                int index = Array.FindIndex(Tools, tool => tool == null);
                Tools[index] = aTool.Name;
                numTools++;
            }
            throw new IndexOutOfRangeException("A memebr cannot borrow more than three tools at the same time.");
        }

        public void deleteTool(iTool aTool)
        {
            if (numTools == 0) throw new Exception("This member is not holding any tools.");
            // find the tool in the Tools array
            int toolIndex = Array.FindIndex(Tools, t => t == aTool.Name);

            // delete the tool
            if (toolIndex != -1)
            {
                Tools[toolIndex] = null;
                numTools--;
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

        public int CompareTo(iMember other)
        {
            return string.Compare(GetFullName(this), GetFullName(other));
        }
    }
}

