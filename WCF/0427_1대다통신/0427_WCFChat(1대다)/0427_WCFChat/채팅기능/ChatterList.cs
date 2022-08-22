using System;
using System.Collections.Generic;

namespace _0427_WCFChat
{
    internal class ChatterList : List<Chatter>
    {
        #region 싱글톤

        private ChatterList() { }

        public static ChatterList Instance { get; private set; }

        static ChatterList()
        {
            Instance = new ChatterList();
        }

        #endregion

        public bool Contains(string name)
        {
            foreach(Chatter chatter in this)
            {
                if(chatter.Name == name)
                    return true;
            }
            return false;
        }
    
        public Chatter GetChatter(string name)
        {
            foreach (Chatter chatter in this)
            {
                if (chatter.Name == name)
                    return chatter;
            }
            return null;
        }
    }
}
