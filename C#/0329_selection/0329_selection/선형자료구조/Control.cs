using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_selection
{
    internal class Control
    {
        //raw클래스 : Object
        //private ArrayList list = new ArrayList();

        //templet(배열)
        private List<int> list = new List<int>();

        //private LinkedList<int> list = new LinkedList<int>();

        public void SelectAll()
        {
            Console.WriteLine("최대 저장 공간 : " + list.Capacity);
            Console.WriteLine("저장 갯수 : " + list.Count);
            foreach(int value in list)
            {
                Console.Write("{0,5}", value);
            }
            Console.WriteLine();
        }

        public void Insert(int value)
        {
            list.Add(value);
        }

        public void Insertidx(int idx, int value)
        {
            if(list.Count >= idx && idx >=0)
                list.Insert(idx, value);
        }

        private int ValueToIdx(int value)
        {
            for(int i = 0; i<list.Count; i++)
            {
                if ((int)list[i] == value)
                    return i;
            }
            return -1;
        }
        public void Select(int value)
        {
            int idx = ValueToIdx(value);
            if (idx == -1)
            {
                Console.WriteLine("없다");
            }
            else
            {
                Console.WriteLine("찾음 : " + list[idx]);
            }
        }

        public void Update(int value, int updatevalue)
        {
            int idx = ValueToIdx(value);
            if (idx == -1)
            {
                Console.WriteLine("없다");
            }
            else
            {
                list[idx] = updatevalue;
            }
        }

        public void Delete(int value)
        {
            list.Remove(value);
        }

        public void DeleteAt(int idx)
        {
            list.RemoveAt(idx);
        }

        public void Clear()
        {
            list.Clear();
        }
    }
}
