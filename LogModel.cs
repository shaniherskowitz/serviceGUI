using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            //var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            //info = JsonConvert.DeserializeObject<String>(info);


            /* EventRecord eventRecord;

             while ((eventRecord = reader.ReadEvent()) != null)
             {

                 int x = eventRecord.Id;

                 if (x == (int)MessageTypeEnum.INFO)
                     ListCommands.Add(new CommandInfo(MessageTypeEnum.INFO.ToString(), eventRecord.FormatDescription()));
                 if (x == (int)MessageTypeEnum.FAIL)
                     ListCommands.Add(new CommandInfo(MessageTypeEnum.FAIL.ToString(), eventRecord.FormatDescription()));
                 if (x == (int)MessageTypeEnum.WARNING)
                     ListCommands.Add(new CommandInfo(MessageTypeEnum.WARNING.ToString(), eventRecord.FormatDescription()));
             }*/

            IList<string> eachPath = info.Split('*').Reverse().ToList<string>();

            for (int i = 0; i < eachPath.Count; i++)
            {
                IList<string> each = eachPath[i].Split(',').Reverse().ToList<string>();
                if (each.Count == 2)
                {
                    int x = 0;

                    Int32.TryParse(each[1], out x);
                    if (x == (int)MessageTypeEnum.INFO)
                        ListCommands.Add(new CommandInfo(MessageTypeEnum.INFO.ToString(), each[0]));
                    if (x == (int)MessageTypeEnum.FAIL)
                        ListCommands.Add(new CommandInfo(MessageTypeEnum.FAIL.ToString(), each[0]));
                    if (x == (int)MessageTypeEnum.WARNING)
                        ListCommands.Add(new CommandInfo(MessageTypeEnum.WARNING.ToString(), each[0]));
                }
            }
        }
    }
}

