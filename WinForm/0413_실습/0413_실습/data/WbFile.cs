using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace _0413_실습
{
    internal static class WbFile
    {
        private const string FILENAME = "members.wbit";
        public static void Load(List<Member> members)
        {
            try
            {
                Stream rs = new FileStream(FILENAME, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                int size = (int)formatter.Deserialize(rs);
                for (int i = 0; i < size; i++)
                {
                    Member mem = (Member)formatter.Deserialize(rs);
                    members.Add(mem);
                }
                rs.Close();
            }
            catch(Exception)
            { }
        }
        public static void Save(List<Member> members)
        {
            Stream ws = new FileStream(FILENAME, FileMode.Create);
            BinaryFormatter serializer = new BinaryFormatter();

            //개수 
            int count = members.Count;
            serializer.Serialize(ws, count);
            foreach (Member mem in members)
            {
                serializer.Serialize(ws, mem);
            }
            ws.Close();
        }
    }
}
