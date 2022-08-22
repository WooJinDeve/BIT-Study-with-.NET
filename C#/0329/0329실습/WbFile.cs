using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace _0329실습
{
     static class WbFile
     {
        //static string path = @"E:\비트고급\실습\0329실습\0329실습\전화번호.txt";
        static string path = "전화번호.txt";

        static public void WriteFile(List<Member> members)
        {
            //비관리 리소스(파일)
            //관리 --> heap 메모리가 올라가면
            StreamWriter sw = File.CreateText(path);

            sw.WriteLine(members.Count);
                
            for (int i = 0; i < members.Count; i++)
            {
                Member boo = members[i];
                sw.WriteLine(boo.Name + " "
                                 + boo.Phone + " "
                                 + boo.Gender + " "
                                 + boo.Date.Year + " " + boo.Date.Month + " " + boo.Date.Day);
            }
            //sw.Close();  //StringWriter객체가 내부적으로 갖고 있는
                        // 파일을 Close
            sw.Dispose(); //StreadmWriter객체가 사용하는 모든 리소스를 소멸!!!!
        }

        static public void ReadFile(List<Member> members)
        {
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    int size = int.Parse(sr.ReadLine());

                    for (int i = 0; i < size; i++)
                    {
                        string memberstr = sr.ReadLine();
                        string[] sp = memberstr.Split(' ');
                        string name = sp[0];
                        string phone = sp[1];
                        string gender = sp[2];
                        DateTime publishtime = new DateTime(
                            int.Parse(sp[3]),
                            int.Parse(sp[4]),
                            int.Parse(sp[5])
                            );

                        Member temp = new Member(name, phone, gender, publishtime);
                        members.Add(temp);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //using (StreamReader sr = File.OpenText(path))
            //{
            //    string s = null; //크기(3) + 객체 + 객체 + 객체.
            //    string k = null;
            //    while ((k = sr.ReadLine()) != null)
            //    {
            //        s += k;
            //    }

                //    string[] readfile = s.Split(' ');
                //    int count = int.Parse(readfile[0]);
                //    int push = 1;
                //    for (int i = 0; i < count; i++)
                //    {
                //        string name = readfile[push++];
                //        string phone = readfile[push++];
                //        string gender = readfile[push++];
                //        DateTime publishtime = new DateTime(int.Parse(readfile[push++]),
                //            int.Parse(readfile[push++]),
                //            int.Parse(readfile[push++]));

                //        Member temp = new Member(name, phone, gender, publishtime);
                //        members.Add(temp);
                //    }

                //} //자동으로 sr.Dispose();

        }
    }
}
