using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI
{
    interface IReceiver
    {
        /// <summary>
        /// Subscribes to the connect event
        /// </summary>
        /// <param object="sender"></param>
        /// <param MessageEventArgs="args"></param>
        void Subscribe(object sender, MessageEventArgs args);
    }
}
