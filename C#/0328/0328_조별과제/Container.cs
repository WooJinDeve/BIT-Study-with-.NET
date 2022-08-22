using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _0328_조별과제
{
    class Container
    {
        Account[] arr;
        int length = 0;
        int aIndex = 0;

        public Container(int len)
        {
            if (len <= 0)
                len = 50;
            length = len;

            arr = new Account[length];
        }  

        public void insert(Account data)
        {
            if(aIndex == length)
            {
                Console.WriteLine("저장할 공간이 없습니다");
            }
            arr[aIndex++] = data;
        }

        public Account Remove(int index)
        {
            if (index < 0 || index >= aIndex)
            {
                Console.WriteLine("존재하지 않는 요소입니다.");
            }

            Account del = arr[index];

            for(int i = index; i < aIndex - 1; i++)
            {
                arr[i] = arr[i + 1];
            }
            aIndex--;

            return del;
        }
        public Account GetItem(int idx)
        {
            if(idx < 0 || idx >= aIndex)
            {
                Console.WriteLine("존재하지 않는 요소입니다");
                return null;
            }
            return arr[idx];
        }

        public Account this[int idx]
        {
            get { return arr[idx]; }
            set { arr[idx] = value; }
        }

        public int GetElemSum()         // 저장된 객체의 수
        {
            return aIndex;
        }

    }
}
