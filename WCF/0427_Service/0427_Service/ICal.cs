using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace _0427_Service
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드 및 config 파일에서 인터페이스 이름 "IService1"을 변경할 수 있습니다.

    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface ICal
    {

        [OperationContract(IsOneWay = true)]
        void Add(int num1, int num2);

        [OperationContract(IsOneWay = true)]
        void Sub(int num1, int num2);

        [OperationContract(IsOneWay = true)]
        void Mul(int num1, int num2);

        [OperationContract(IsOneWay = true)]
        void Div(int num1, int num2);

    }

    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void Result(float result);
    }



}
