using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI
{
    class MessageEventArgs : EventArgs
    {
        public string message;

        public string receiver;

        public MessageEventArgs(string message, string receiver)
        {
            this.message = message;
            this.receiver = receiver;
        }
    }
}
