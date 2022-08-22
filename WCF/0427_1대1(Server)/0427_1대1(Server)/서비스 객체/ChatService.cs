using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _0427_1대1_Server_
{
    internal class ChatService : IChat
    {
        public delegate void Chat(int idx, string msg, string type);
        private Chat MyChat;

        IChatCallback callback = null;

        public bool Join(int idx)
        {
            MyChat = new Chat(UserHandler);
            //2. 사용자에게 보내 줄 채널을 설정한다.
            callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();

            //현재 접속자 정보를 모두에게 전달
            BroadcastMessage(idx, "", "UserEnter");
            return true;

        }
        public void Say(int idx, string msg)
        {
            BroadcastMessage(idx, msg, "Receive");
        }

        public void Leave(int idx)
        {
            Chat d = null;
            MyChat -= d;
        }
        private void BroadcastMessage(int idx, string msg, string msgType)
        {
            MyChat.BeginInvoke(idx, msg, msgType, new AsyncCallback(EndAsync), null);
        }


        private void UserHandler(int idx, string msg, string msgType)
        {
            try
            {
                //클라이언트에게 보내기
                switch (msgType)
                {
                    case "Receive":
                        callback.Receive(idx, msg);
                        break;
                    case "UserEnter":
                        callback.UserEnter(idx);
                        break;
                }
            }
            catch//에러가 발생했을 경우
            {
                Console.WriteLine("{0} 에러", idx);
            }
        }

        private void EndAsync(IAsyncResult ar)
        {
            Chat d = null;
            try
            {
                System.Runtime.Remoting.Messaging.AsyncResult asres = (System.Runtime.Remoting.Messaging.AsyncResult)ar;
                d = ((Chat)asres.AsyncDelegate);
                d.EndInvoke(ar);
            }
            catch
            {
            }
        }
    }
}

