using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class MemberCollection : iMemberCollection
    {
        public int Number => members.Size;
        private BinaryTree<iMember> members;
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
            return members.Search(aMember);
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
