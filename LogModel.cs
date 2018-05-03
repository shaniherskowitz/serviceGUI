using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI
{
    class LogModel
    {
        public IList<CommandInfo> ListCommands { get; set; }
        public LogModel()
        {
            ListCommands = new List<CommandInfo>();
            //ListCommands.Add(new CommandInfo(MessageTypeEnum.FAIL.ToString(), "does this work?"));
            ConnectToServer();
        }

        public void ConnectToServer()
        {
            Connect c = Connect.Instance;
            string info = c.WriteConnection("2");
            IList<string> eachPath = info.Split(';').Reverse().ToList<string>();
            ListCommands.Add(new CommandInfo(MessageTypeEnum.FAIL.ToString(), eachPath[0]));
        }



    }
}
