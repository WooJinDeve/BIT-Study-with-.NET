using System;

namespace _0427_WCFChat
{
    internal class Chatter
    {
        public string Name  { get; set; }
        public Chat MyChat  { get; set; }

        public Chatter() { }
        public Chatter(string name, Chat mychat)
        {
            Name = name;
            MyChat = mychat;
        }
    }
}
