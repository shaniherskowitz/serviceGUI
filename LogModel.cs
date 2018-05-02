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
            ListCommands.Add(new CommandInfo(MessageTypeEnum.FAIL.ToString(), "does this work?"));

        }



    }
}
