﻿using System;
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



        public ObservableCollection<Object> ListPaths { 
           get { return ListP; } 
           set {
                NotifyPropertyChanged("ListPaths"); 
            } 
        }
        
        public string Source { get; set; }
        public string LogName { get; set; }
        public string ThumbName { get; set; }


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

        public void ConnectToServer()
        {
            Connect c = Connect.Instance;
            c.SubscribeToMessage(this);
            SetConfig(c);

        }
        
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
        
         protected void NotifyPropertyChanged(string name)
         {
             if(PropertyChanged != null)
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
         }

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

