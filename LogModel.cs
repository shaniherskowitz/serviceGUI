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
            IList<string> eachPath = info.Split('*').Reverse().ToList<string>();
            
            for (int i = 0; i < eachPath.Count; i++)
            {
                IList<string> each = eachPath[i].Split(',').Reverse().ToList<string>();
                if (each.Count == 2)
                {
                    if (each[1] == "Information") each[1] = "INFO";
                    ListCommands.Add(new CommandInfo(each[1], each[0]));
                }
            }
        }



    }
}
