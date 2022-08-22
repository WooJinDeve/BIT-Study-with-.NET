using javax.management.openmbean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace _0428_RESTAPI_TEST
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드, svc 및 config 파일에서 클래스 이름 "Service1"을 변경할 수 있습니다.
    // 참고: 이 서비스를 테스트하기 위해 WCF 테스트 클라이언트를 시작하려면 솔루션 탐색기에서Service1.svc나 Service1.svc.cs를 선택하고 디버깅을 시작하십시오.
    public class Service1 : IService1
    {
        public Student GetData(string value)
        {
            Student student = new Student();
            student.Name = value;
            student.Age = 20;
            return student;
        }

        public string PostData(string name, int age)
        {
            return string.Format("{0}님의 정보가 저장되었습니다.", name);
        }
    }
}
