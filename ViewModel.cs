using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    class ViewModel
    {
        public ObservableCollection<string> ListPaths = new ObservableCollection<string>();

        public IList<CommandInfo> ListCommands = new List<CommandInfo>();

        public void Remove(string path)
        {
            if (path != null) ListPaths.Remove(path);
        }

        public ViewModel()
        {
            ListCommands.Add(new CommandInfo(MessageTypeEnum.INFO.ToString(), "what is the problem"));
            ListPaths.Add("hi my name is");
            ListPaths.Add("this is great");
        }
    }
}
