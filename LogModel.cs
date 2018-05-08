using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using Newtonsoft.Json;

namespace ServiceGUI
{
    class LogModel : IReceiver
    {
        private Connect c = Connect.Instance;
        private object lockObj = new object();
        public IList<CommandInfo> ListCommands { get; set; }
        public LogModel()
        {
            ListCommands = new List<CommandInfo>();
            ConnectToServer();
            BindingOperations.EnableCollectionSynchronization(ListCommands, lockObj);
            c.SubscribeToMessage(this);
        }

        public void ConnectToServer()
        {
            
            string info = c.WriteConnection("2");
        

            IList<string> eachPath = info.Split('*').Reverse().ToList<string>();

            for (int i = 0; i < eachPath.Count; i++)
            {
                IList<string> each = eachPath[i].Split(',').Reverse().ToList<string>();
                if (each.Count == 2)
                {
                    lock (lockObj)
                    {
                        Int32.TryParse(each[1], out int x);
                        if (x == (int)MessageTypeEnum.INFO)
                            ListCommands.Add(new CommandInfo(MessageTypeEnum.INFO.ToString(), each[0]));
                        if (x == (int)MessageTypeEnum.FAIL)
                            ListCommands.Add(new CommandInfo(MessageTypeEnum.FAIL.ToString(), each[0]));
                        if (x == (int)MessageTypeEnum.WARNING)
                            ListCommands.Add(new CommandInfo(MessageTypeEnum.WARNING.ToString(), each[0]));
                    }
                }
            }
            Thread t = new Thread(() => c.ReadConnection());
            t.Start();
            //Thread t = new Thread(() => ReadLogs(ListCommands));
            // t.Start();

        }

       /* public void ReadLogs(IList<CommandInfo> commands)
        {
            string log = "";
            while (log != "error")
            {
                log = c.ReadConnection();
                if (log == "No Log") continue;
                IList<string> each = log.Split(',').Reverse().ToList<string>();
                if (each.Count == 2)
                {
                    lock (lockObj)
                    {
                        commands.Add(new CommandInfo(each[1], each[0]));
                    }
                    
                }
            }
        }*/

        public void Subscribe(object sender, MessageEventArgs args)
        {
    
            if (args.receiver == "Settings") return;
            if (args.receiver == "Log")
            {
                string msg = args.message.Substring(1);
                IList<string> each = msg.Split(',').Reverse().ToList<string>();
                if (each.Count == 2)
                {

                    ListCommands.Add(new CommandInfo(each[1], each[0]));


                }
            }
           
        }
    }
}

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
