using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI
{
    interface IReceiver
    {
        void Subscribe(object sender, MessageEventArgs args);
    }
}
