using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _0426_ImageWCFService
{
    [ServiceContract] // 서비스계약
    interface IImage
    {
        [OperationContract] // 메시지 계약
        byte[] GetPicture(string strFileName);
        [OperationContract] // 메시지 계약
        string[] GetPictureList();
        [OperationContract] // 메시지 계약
        bool UploadPicture(string strFileName, byte[] bytePic);
    }
}
