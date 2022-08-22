using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace _0427_Service
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드, svc 및 config 파일에서 클래스 이름 "Service1"을 변경할 수 있습니다.
    // 참고: 이 서비스를 테스트하기 위해 WCF 테스트 클라이언트를 시작하려면 솔루션 탐색기에서Service1.svc나 Service1.svc.cs를 선택하고 디버깅을 시작하십시오.
    public class CCal : ICal
    {
        public float Result { get; set; }
        private ICallback callback = null;
        public CCal()
        {
            callback = OperationContext.Current.GetCallbackChannel<ICallback>();
        }


        public void Add(int num1, int num2)
        {
            Result = num1 + num2;
            callback.Result(Result);
        }

        public void Sub(int num1, int num2)
        {
            Result = num1 - num2;
            callback.Result(Result);
        }

        public void Mul(int num1, int num2)
        {
            Result = num1 * num2;
            callback.Result(Result);
        }

        public void Div(int num1, int num2)
        {
            Result = num1 / num2;
            callback.Result(Result);
        }
    }
}
