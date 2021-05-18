using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class MemberCollection : iMemberCollection
    {
        public int Number => members.Size;
        private BinaryTree<iMember> members;
        public MemberCollection()
        {
            members = new BinaryTree<iMember>();
        }
        public void add(iMember aMember)
        {
            members.Add(aMember);
        }

        public void delete(iMember aMember)
        {
            members.Delete(aMember);
        }

        public bool search(iMember aMember)
        {
            iMember member =  members.Search(aMember);
            if (member == null) return false;
            // populate memeber data
            aMember.ContactNumber = member.ContactNumber;
            aMember.PIN = member.PIN;
            return true;
        }

        public iMember[] toArray()
        {
            iMember[] array = new iMember[members.Size];
            int index = 0;
            BinaryTree<iMember>.PostOrderTraversal(members.root, member =>
            {
                array[index] = member;
                index++;
            });
            return array;
        }
    }
}
