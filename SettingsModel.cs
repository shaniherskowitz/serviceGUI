using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI
{
    class SettingsModel 
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        public string Output { get; set; }
        public ObservableCollection<string> ListPaths { get; set; }
        public string Source { get; set; }
        public string LogName { get; set; }
        public string ThumbName { get; set; }

        public SettingsModel()
        {
            this.Output = "Output Directory: ";
            this.Source = "Sourc Name: ";
            LogName = "Log Name: ";
            ThumbName = "Thumbnail Name: ";
            ListPaths = new ObservableCollection<string>();
            ConnectToServer();
        }


        public void ConnectToServer()
        {
            Connect c = Connect.Instance;
            int result;
            Output = Output + c.WriteConnection("1");
            //Output = Output + c.ReadConnection(out result);
            Source = Source + c.WriteConnection("1");
            LogName = LogName + c.WriteConnection("1");
            ThumbName = ThumbName + c.WriteConnection("1");
            string h = c.WriteConnection("1");
         
            IList<string> eachPath = h.Split(';').Reverse().ToList<string>();
            eachPath.ToList().ForEach(ListPaths.Add);

            
        }

        /*protected void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }*/


    }
}
