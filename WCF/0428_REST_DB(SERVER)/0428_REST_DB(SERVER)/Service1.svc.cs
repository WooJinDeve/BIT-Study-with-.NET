using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace _0428_REST_DB_SERVER_
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드, svc 및 config 파일에서 클래스 이름 "Service1"을 변경할 수 있습니다.
    // 참고: 이 서비스를 테스트하기 위해 WCF 테스트 클라이언트를 시작하려면 솔루션 탐색기에서Service1.svc나 Service1.svc.cs를 선택하고 디버깅을 시작하십시오.
    public class Service1 : IService1
    {
        public List<Member> GetData()
        {
            try
            {
                WbDB.Instance.Open();
                string sql = WbQuery.GetData();
                string msg = WbDB.Instance.CommandQuery(sql);

                List<Member> members = new List<Member>();
                string[] sp = msg.Split('$');
                foreach(string sp1 in sp)
                {
                    if (sp1 == "")
                        break;
                    string[] sp2 = sp1.Split('#');
                    members.Add(new Member(sp2[0], int.Parse(sp2[1]), sp2[2]));
                }
                return members;

            }
            catch 
            {
                return null;
            }
            finally
            {
                WbDB.Instance.Close();
            }
            
        }
        public bool PostData(string name, int age, string phone)
        {
            try
            {
                WbDB.Instance.Open();
                string sql = WbQuery.PostData(name, age, phone);
                WbDB.Instance.CommandNonQuery(sql);
                return true;
            }
            catch 
            {
                return false;
            }
            finally
            {
                WbDB.Instance.Close();
            }
        }

        public bool PutData(string name, int age, string phone)
        {
            try
            {
                WbDB.Instance.Open();
                string sql = WbQuery.PutData(name, age, phone);
                WbDB.Instance.CommandNonQuery(sql);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                WbDB.Instance.Close();
            }
        }
        public bool DeleteData(string name)
        {
            try
            {
                WbDB.Instance.Open();
                string sql = WbQuery.DeleteData(name);
                WbDB.Instance.CommandNonQuery(sql);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                WbDB.Instance.Close();
            }
        }
    }
}
