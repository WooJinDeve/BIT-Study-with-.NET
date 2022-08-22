using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_selection
{
    class Control1
    {
   
        private LinkedList<int> list = new LinkedList<int>();

        public void SelectAll()
        {
            Console.WriteLine("저장 갯수 : " + list.Count);
            Console.WriteLine("head : " + (list.First != null ? list.First.Value : 0));
            Console.WriteLine("tail : " + (list.Last != null ? list.Last.Value : 0));

            foreach (int value in list)
            {
                Console.Write("{0,5}", value);
            }
            Console.WriteLine();
        }

        public void Insert(int value)
        {
            list.AddFirst(value); //list.AddLast(value);
        }

        public void Insertidx(int idx, int value)
        {
            // 인덱스 만큼 노드 이동
            LinkedListNode<int> head = list.First;
            for(int i = 0; i<idx; i++)
            {
                head = head.Next;
            }

            list.AddAfter(head, value); //list.Before(value);
        }

        private LinkedListNode<int> ValueToIdx(int value)
        {
            LinkedListNode<int> head = list.First;
            LinkedListNode<int> tail = list.Last;

            if (tail.Value == value)
                return tail;
            while(head != tail)
            {
                if (head.Value == value)
                {
                    return head;
                }
                head = head.Next;
            }
           
            return null;
        }

        public void Select(int value)
        {
            LinkedListNode<int> node = ValueToIdx(value);
            if (node == null)
            {
                Console.WriteLine("없다");
            }
            else
            {
                Console.WriteLine("찾음 : " + node.Value);
            }
        }

        public void Update(int value, int updatevalue)
        {
            LinkedListNode<int> node = ValueToIdx(value);
            if (node == null)
            {
                Console.WriteLine("없다");
            }
            else
            {
                node.Value = updatevalue;
            }
        }

        public void Delete(int value)
        {
            list.Remove(value);
        }

        public void DeleteAt(int idx)
        {
            Console.WriteLine("해당 기능은 지원되지 않습니다.");
        }

        public void Clear()
        {
            list.Clear();
        }
    }
}

