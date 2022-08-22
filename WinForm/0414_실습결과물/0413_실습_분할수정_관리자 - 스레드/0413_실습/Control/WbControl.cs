using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0413_실습
{
    internal class WbControl : IDisposable
    {
        #region 싱글톤 패턴
        private WbControl() { }
        public static WbControl Instance { get; private set; }
        static WbControl()
        {
            Instance = new WbControl();
        }
        #endregion 
    
        private WbDocument doc = WbDocument.Instance;

        public List<Member> GetMemberAllList()
        {
            List<Member> members = doc.GetMemberAllList();
            return members;
        }   
        public void Dispose()
        {
            doc.Dispose();
        }
    }
}