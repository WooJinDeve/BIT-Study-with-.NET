using System;
using System.ServiceModel;

namespace _0427_WCFChat
{
    internal class ChatService : IChat
    {
         private IChatCallback callback = null;
         private WbChat wbchat = new WbChat();

        #region IChat

        public bool Join(string nickname)
        {
            //사용자에게 보내 줄 채널을 설정한다.
            callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();

            if (wbchat.Join(callback, nickname) == false)
                return false;            
            
            //현재 접속자 클라이언트에게 전달
            wbchat.BroadcastMessage(nickname, "", "UserEnter");
            return true;
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
