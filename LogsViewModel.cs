using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI
{
    public enum MessageTypeEnum : int
    {
        INFO,
        WARNING,
        FAIL
    }

    public class CommandInfo
    {
        public string ID { get; set; }
        public string Details { get; set; }

        public CommandInfo(string iD, string details)
        {
            ID = iD;
            Details = details;
        }
    }


    class LogsViewModel 
    {
        private LogModel log;
        

        public LogsViewModel()
        {
            log = new LogModel();
        }
        public LogModel Log
        {
            get { return this.log; }
            set { this.log = value; }

        }
        
        
    }
}
