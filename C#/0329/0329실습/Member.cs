using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329실습
{
    class Member 
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime Date { get; set; }

        #region 생성자 
        public Member()
        {
            Name    = string.Empty;
            Phone   = string.Empty;
            Gender  = string.Empty;  
            Date    = DateTime.Now;
        }
        public Member(string _name, string _phone, string _gender)
        {
            Name = _name;
            Phone = _phone;
            Gender = _gender;
            Date = DateTime.Now;
        }

        public Member(string _name, string _phone, string _gender, DateTime _date)
        {
            Name    = _name;
            Phone   = _phone;
            Gender  = _gender;
            Date    = _date;
        }

        #endregion 
        
        public override string ToString()
        {
            return "이름 : "+Name + " 전화번호 :"+ Phone +" 성별 : " +Gender +"날짜" +Date;
        }
    }

    //Member 정렬관련클래스

}
