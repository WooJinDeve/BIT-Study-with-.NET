using System;
using System.ServiceModel;

namespace _0427_WCFChat
{
    internal class ChatService : IChat
    {
        //클라이언트 연결 인터페이스
        IChatCallback callback = null;

        private WbChat wbchat = new WbChat();

        public ChatService()
        { 
            Console.WriteLine("서비스 객체 생성");
        }

        #region IChat

        public bool Join(string nickname)
        {
            //사용자에게 보내 줄 채널을 설정한다.
            callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();

            return wbchat.Join(nickname, callback);
        }

        public void Leave(string nickname)
        {
            wbchat.Leave(nickname);
        }

        public void Say(string nickname, string msg)
        {
            wbchat.BroadcastMessage(nickname, msg, "Receive");
        }
        #endregion 
    }
}
