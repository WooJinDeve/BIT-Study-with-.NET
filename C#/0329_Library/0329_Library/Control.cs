using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_Library
{
    internal class Control
    {
        private List<Library> Lib = new List<Library>();
        public void FileLoad()
        {
            WbFile.ReadFile(Lib);
        }
        public void FileSave()
        {
           
            WbFile.WriteFile(Lib);
        }

        public void SelectAll ()
        {
            Console.WriteLine("저장 갯수 : " + Lib.Count);
            foreach (Library lib in Lib)
            {
                Console.WriteLine (lib);
            }
            Console.WriteLine();
        }

        public void Insert(Library lib)
        {
            Lib.Add(lib);
        }


        private int NameToIdx(string name)
        {
            for (int i = 0; i < Lib.Count; i++)
            {
                if (Lib[i].Name == name)
                    return i;
            }
            return -1;
        }

        public void Select(string name)
        {
            int idx = NameToIdx(name);
            if (idx == -1)
            {
                Console.WriteLine("없다");
            }
            else
            {
                Console.WriteLine("select : " + Lib[idx]);
            }
        }

        private Library NameToLibrary(string name)
        {
            for (int i = 0; i < Lib.Count; i++)
            {
                Library mem = Lib[i];
                if (Lib[i].Name == name)
                    return mem;
            }
            return null;
        }

        public void Delete(string name)
        {
            Library del_lib = NameToLibrary(name);
            Lib.Remove(del_lib);
        }

        public void Update(string name, string updatename)
        {
            //이름만 수정하는 코드임.
            int idx = NameToIdx(name);
            if (idx == -1)
            {
                Console.WriteLine("없다");
            }
            else
            {
                Lib[idx].Name = updatename;
            }
        }

        public void DeleteAll()
        {
            Lib.Clear();
        }

        public void Sort()
        {
            Lib.Sort();
        }             
    }
}
