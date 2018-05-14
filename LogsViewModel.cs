using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI
{
        /// <summary>
        /// Creates the messageTypeEnum
        /// </summary>
    public enum MessageTypeEnum : int
    {
        INFO,
        WARNING,
        FAIL
    }

         /// <summary>
        /// Creates the commadnInfo
        /// </summary>
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
        

        /// <summary>
        /// Creates the logViewModl
        /// </summary>
        public LogsViewModel()
        {
            log = new LogModel();
        }

        /// <summary>
        ///Gets the logmodel
        /// </summary>
        /// <return the log></return>
        public LogModel Log
        {
            get { return this.log; }
            set { this.log = value; }

        }
        
        
    }
}
