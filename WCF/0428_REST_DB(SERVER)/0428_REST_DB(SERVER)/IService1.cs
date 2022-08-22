using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace _0428_REST_DB_SERVER_
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드 및 config 파일에서 인터페이스 이름 "IService1"을 변경할 수 있습니다.
    [ServiceContract]
    public interface IService1
    {
        // 전체 리스트 출력
        [OperationContract]
        [WebGet(UriTemplate = "GetData")]
        List<Member> GetData();

        //Insert
        [OperationContract]
        [WebInvoke(UriTemplate = "PostData",
           Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        bool PostData(string name, int age, string phone);

        //Updata
        [OperationContract]
        [WebInvoke(UriTemplate = "PutData",
           Method = "PUT",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        bool PutData(string name, int age, string phone);

        //Delete
        [OperationContract]
        [WebInvoke(UriTemplate = "DeleteData",
           Method = "DELETE",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        bool DeleteData(string name);
    }
}
