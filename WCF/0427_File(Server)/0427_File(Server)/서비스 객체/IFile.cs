using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _0427_File_Server_
{

    #region 1. (클라이언트->서버)
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IFileCallback))]
    internal interface IFile
    {
        [OperationContract(IsOneWay = false, IsInitiating = true, IsTerminating = false)]
        bool UpLoadFile(string name,int idx, string filename, byte[] data);

        [OperationContract(IsOneWay = false, IsInitiating = true, IsTerminating = false)]
        StudentData[] Join(string name, int idx);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void Leave(string name, int idx);
    }
    #endregion

    #region 2. (서버 -> 클라이언트)
    public interface IFileCallback
    {
        [OperationContract(IsOneWay = true)]
        void FileRecive(string name, int idx, string msg, byte[] filedata);
        [OperationContract(IsOneWay = true)]
        void UserEnter(string name, int idx);
        [OperationContract(IsOneWay = true)]
        void UserLeave(string name, int idx);
    }

    #endregion 
}
