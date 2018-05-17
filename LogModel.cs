using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ServiceGUI
{
    class LogModel : INotifyPropertyChanged, IReceiver
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Connect c = Connect.Instance;
        private object lockObj = new object();
        public ObservableCollection<CommandInfo> ListCommand; 

        /// <summary>
        /// Creates the log model
        /// </summary>
        public LogModel()
        {
            ListCommands = new ObservableCollection<CommandInfo>();
            BindingOperations.EnableCollectionSynchronization(ListCommands, lockObj);
            c.SubscribeToMessage(this);
            ConnectToServer();
            
        }

        /// <summary>
        ///Gets and sets the list of logs
        /// </summary>
        /// <return the listCommand></return>
          public ObservableCollection<CommandInfo> ListCommands
        {
            get { return ListCommand; }
            set
            {
                this.ListCommand = value;
                NotifyPropertyChanged("ListCommands");
            }
        }

        /// <summary>
        ///Invokesthe property changed
        /// </summary>
        /// <Param string: name></param>
         protected void NotifyPropertyChanged(string name)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
      }

        /// <summary>
        /// Connects to the server and gets the logs 
        /// Adds the command according to the message enum
        /// </summary>
        public void ConnectToServer()
        {
            Console.WriteLine("got to log");
            string info = c.WriteConnection("2");

            Console.WriteLine("read " + info);
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
            //creates a new thread to always be listening for connection
            Thread t = new Thread(() => c.ReadConnection());
            t.Start();
            

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

        /// <summary>
        /// Subscribes to the event as the log 
        /// splits to string and adds to the commandinfo
        /// </summary>
        /// <param object="sender"></param>
        /// <param MessageEventArgs="args"></param>
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
