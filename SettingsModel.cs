using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ServiceGUI
{
    class SettingsModel : INotifyPropertyChanged, IReceiver
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Output { get; set; }
        private ObservableCollection<Object> ListP;
        private object lockObj = new object();


         /// <summary>
        /// Test value and modify it.
        /// </summary>
        /// <param object="sender"></param>
        /// <param propertychangedeventargs="e"></param>
        public ObservableCollection<Object> ListPaths { 
           get { return ListP; } 
           set {
                NotifyPropertyChanged("ListPaths"); 
            } 
        }
        
        /// <summary>
        /// Gets and sets the string source
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets and sets the string logname
        /// </summary>
        public string LogName { get; set; }

         /// <summary>
        /// Gets and sets the string thumbname
        /// </summary>
        public string ThumbName { get; set; }

         /// <summary>
        /// Creates the setting model
        /// </summary>
        public SettingsModel()
        {
            Output = "Output Directory: ";
            Source = "Sourc Name: ";
            LogName = "Log Name: ";
            ThumbName = "Thumbnail Name: ";
            ListP = new ObservableCollection<Object>();
            BindingOperations.EnableCollectionSynchronization(ListP, lockObj);
            ConnectToServer(); 
            
        }

         /// <summary>
        /// Connects to the server
        /// </summary>
        public void ConnectToServer()
        {
            Connect c = Connect.Instance;
            c.SubscribeToMessage(this);
            SetConfig(c);

        }
        
        /// <summary>
        /// Sets the app config - writes and cplits the returned string
        /// </summary>
        /// <param connect="c"></param>
        public void SetConfig(Connect c)
        {
            string set = c.WriteConnection("1");
            IList<string> eachPath = set.Split('*').Reverse().ToList<string>();
            if (eachPath.Count == 5)
            {
                Output = Output + eachPath[4];
                Source = Source + eachPath[3];
                LogName = LogName + eachPath[2];
                ThumbName = ThumbName + eachPath[1];
                string lPaths = eachPath[0];

                IList<string> each = lPaths.Split(';').Reverse().ToList<string>();
                each.ToList().ForEach(ListPaths.Add);
            }
        }
        
        /// <summary>
        /// Notifies that the property has changed
        /// </summary>
        /// <param string="name"></param>
         protected void NotifyPropertyChanged(string name)
         {
             if(PropertyChanged != null)
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
         }

        /// <summary>
        /// Subscribes to the event handler
        /// Removes from the list
        /// </summary>
        /// <param object="sender"></param>
        /// <param MessageEventArgs="args"></param>
        public void Subscribe(object sender, MessageEventArgs args)
        {
            string msg = args.message.Substring(1);
            if (args.receiver == "Settings")
            {

                ListPaths.Remove(msg);
            }
        }
    }
    }

