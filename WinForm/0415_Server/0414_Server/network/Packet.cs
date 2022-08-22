using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0414_Server
{ 
    class Packet
    {
        //--[Client --> Server]
        private const string GETMEMBERALLLIST     = "GetMemberAllList";        //@검색할정보
        //
        private const string LOGIN = "Login";
        private const string NEWMEMBER = "NewMember";
        private const string IDTOMEMBER = "IdtoMember";
        private const string SELECTMEMBER = "SelectMember";
        private const string DELETEMEMBER = "DeleteMember";
        private const string UPDATEMEMBER = "UpdateMember";
        private const string LOGOUT = "Logout";
        //public static string SelectAllBookName(bool b, List<string> booknames)
        //{
        //    string packet = string.Format("{0}@{1}#",SELECTALLBOOKNAME, b);
        //    foreach(string s in booknames)
        //    {
        //        packet += s;
        //    }
        //    return packet;
        //}

        #region [Server --> Client(관리자)]
        public static string GetMemberAllList_ACK(bool b, List<Member> members)
        {
            //맴버#맴버#맴버$
            string msg = "";
            members.ForEach((elem) =>
            {
                msg += elem.IsLogin + "#" + elem.Id + "#" + elem.Pw + "#" + elem.Name + "#" + elem.Phone +
                "#" + elem.Age + "#" + elem.DateTime.ToShortDateString() + "$";
            });
            string packet = string.Format("{0}@{1}", GETMEMBERALLLIST, msg);
            return packet;
        }

        #endregion

        #region [Server --> Client(사용자)]
        public static string Login_ACK(bool b, string name)
        {  
            string packet = string.Format("{0}@{1}#{2}", LOGIN, b,name);
            return packet;
        }

        public static string NewMember_ASK(bool b)
        {
            string packet = string.Format("{0}@{1}", NEWMEMBER, b);
            return packet;
        }

        public static string IdtoMember_ASK(bool b)
        {           
           
             string packet = string.Format("{0}@{1}", IDTOMEMBER, b);
             return packet;               
        }
        public static string SelectMember_ASK(bool b)
        {

            string packet = string.Format("{0}@{1}", SELECTMEMBER, b);
            return packet;
        }

        public static string SelectMember_ASK(bool b, Member mem)
        {
            string msg = string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}",mem.IsLogin, mem.Id, mem.Pw, mem.Name, mem.Phone, mem.Age,mem.DateTime);
            string packet = string.Format("{0}@{1}${2}", SELECTMEMBER, b,msg);
            return packet;
        }

        public static string DeleteMember_ASK(bool b)
        {

            string packet = string.Format("{0}@{1}", DELETEMEMBER, b);
            return packet;
        }

        public static string UpdateMember_ASK(bool b)
        {

            string packet = string.Format("{0}@{1}", UPDATEMEMBER, b);
            return packet;
        }

        public static string Logout_ASK(bool b)
        {

            string packet = string.Format("{0}@{1}", LOGOUT, b);
            return packet;
        }
        #endregion

    }
}
