using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _0427_File_Server_
{
    [MessageContract]
    public class StudentData
    {
        [MessageBodyMember]
        public bool Flag { get; set; }  

        [MessageBodyMember]
        public string Name { get; set; }

        [MessageBodyMember]
        public int SeatNum { get; set; }

        public StudentData() { }
        public StudentData(bool f, string n, int sn)
        {
            Flag = f;
            Name = n;
            SeatNum = sn;
        }
    }
}
