using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _0427_File_Server_
{
    internal class CFile : IFile
    {
        public delegate void Data(string name, int idx, string msg, byte[] filedata, string type);
        private static Object syncObj = new Object();
        private static ArrayList Chatter = new ArrayList();
        private Data MyFile;
        private static Data List;
        IFileCallback callback = null;


        public bool UpLoadFile(string name, int idx, string filename, byte[] data)
        {
            try
            {
                string path = @"C:\Users\User\Desktop\정우진\비트고급\WCF\실습\0427_File(WPF_Client)\0427_File(WPF_Client)\bin\Debug\File\";
                FileStream writeFileStream = new FileStream(path + filename, FileMode.Create, FileAccess.Write);
                BinaryWriter dataWriter = new BinaryWriter(writeFileStream);

                dataWriter.Write(data);
                writeFileStream.Close();
                BroadcastMessage(name, idx, filename, data, "FileSender");

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public StudentData[] Join(string name, int idx)
        {
            MyFile = new Data(UserHandler);
            lock (syncObj)
            {
                if(!Chatter.Contains(name))
                {
                    StudentData data = new StudentData(true, name, idx);
                    Chatter.Add(data);

                    callback = OperationContext.Current.GetCallbackChannel<IFileCallback>();

                    BroadcastMessage(name, idx, "", null, "UserEnter");

                    List += MyFile;

                    StudentData[] list = new StudentData[Chatter.Count];
                    lock(syncObj)
                    {
                        Chatter.CopyTo(list);
                    }
                    return list;
                }
                else
                    return null;
            }

        }

        public void Leave(string name, int idx)
        {

            if (name == null) return;

            lock(syncObj)
            {
                foreach(StudentData data in Chatter)
                {
                    if(data.Name == name)
                    {
                        Chatter.Remove(data);
                        break;
                    }
                }
            }
        }


        private void BroadcastMessage(string name, int idx, string msg, byte[] filedata, string msgType)
        {
           if(List != null)
            {
                foreach(Data handler in List.GetInvocationList())
                {
                    if (handler == MyFile && msgType == "FileSender")
                        continue;

                    handler.BeginInvoke(name, idx, msg, filedata, msgType, new AsyncCallback(EndAsync), null);
                }
            }
        }

        private void EndAsync(IAsyncResult ar)
        {
            Data d = null;
            try
            {
                AsyncResult asres = (AsyncResult)ar;
                d = (Data)asres.AsyncDelegate;
                d.EndInvoke(ar);
            }
            catch
            {
                List -= d;
            }
        }

        private void UserHandler(string name, int idx, string msg, byte[] filedata, string msgType)
        {
            try
            {
                switch(msgType)
                {
                    case "UserEnter": callback.UserEnter(name, idx); break;

                    case "UserLeave": callback.UserLeave(name, idx); break;

                    case "FileSender": callback.FileRecive(name, idx,msg, filedata); break;
                }
            }
            catch
            {
                Leave(name, idx);
            }
        }
    }
}
