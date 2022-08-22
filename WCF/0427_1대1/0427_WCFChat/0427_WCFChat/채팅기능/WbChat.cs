using System;

namespace _0427_WCFChat
{
    //델리게이트 선언
    public delegate void Chat(string nickname, string msg, string type);

    internal class WbChat
    {
        // 개인용 델리게이트
        private Chat MyChat = null;
        IChatCallback callback = null;

        public bool Join(IChatCallback call, string nickname)
        {
            MyChat = new Chat(UserHandler);
            callback = call;

            return true;
        }

        public void Leave(string nickname)
        {
            Chat d = null;
            MyChat -= d;
        }
  
        public void BroadcastMessage(string nickname, string msg, string msgType)
        {
            MyChat.BeginInvoke(nickname, msg, msgType, new AsyncCallback(EndAsync), null);
        }

        //BroadcastMessage()에서 호출 : Client 메서드 호출
        private void UserHandler(string nickname, string msg, string msgType)
        {
            try
            {
                //클라이언트에게 보내기
                switch (msgType)
                {
                    case "Receive":
                        callback.Receive(nickname, msg);
                        break;
                    case "UserEnter":
                        callback.UserEnter(nickname);
                        break;
                }
            }
            catch//에러가 발생했을 경우
            {
                Console.WriteLine("{0} 에러", nickname);
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
