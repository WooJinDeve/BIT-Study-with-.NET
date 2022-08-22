using System;

namespace _0427_WCFChat
{
    //델리게이트 선언
    public delegate void Chat(string nickname, string msg, string type);

    
    internal class WbChat
    {
        //동기화 작업을 위해서 가상의 객체 생성
        private static object syncObj = new object();

        //클라이언트 연결 인터페이스
        IChatCallback callback = null;
                
        public bool Join(string nickname, IChatCallback call)
        {
            callback = call;
            ChatterList chatters = ChatterList.Instance;
            lock (syncObj)
            {
                if (!chatters.Contains(nickname))
                {
                    //델리게이터 추가(향후 데이터 수신이 가능하도록 구성)
                    Chatter newchatter = new Chatter(nickname, new Chat(UserHandler));
                    chatters.Add(newchatter);

                    //현재 접속자 정보를 모두에게 전달
                    BroadcastMessage(nickname, "", "UserEnter");
                    return true;
                }
                return false;
            }
        }

        public void Leave(string nickname)
        {
            try
            {
                ChatterList chatters = ChatterList.Instance;
                Chatter chatter = chatters.GetChatter(nickname);

                //컬렉션에서 제거
                chatters.Remove(chatter);

                //모든 사람에게 전송
                string msg = string.Format(nickname + "이(가) 나갔습니다");
                BroadcastMessage(nickname, msg, "UserLeave");
            }
            catch(Exception)
            {
            }

        }

        public void BroadcastMessage(string nickname, string msg, string msgType)
        {
            ChatterList chatters = ChatterList.Instance;
            foreach (Chatter chatter in chatters)
            {
                chatter.MyChat.BeginInvoke(nickname, msg, msgType, new AsyncCallback(EndAsync), null);
            }
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
                    case "UserLeave":
                        callback.UserLeave(nickname);
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
