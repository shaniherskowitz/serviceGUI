using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace ServiceGUI
{
    class SettingsViewModel 
    {
       
        //public ICommand RemoveCommand { get; private set; }
        private SettingsModel sm;
        public ObservableCollection<string> ListPaths { get; set; }
        public string Output { get; set; }
        public string Source { get; set; }
        public string LogName { get; set; }
        public string ThumbName { get; set; }
        public string selected { get; set; }

        public SettingsViewModel()
        {
            //this.RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove);

            this.sm = new SettingsModel();


            this.Output = "Output Directory:";
            this.Source = "Sourc Name: ";
            LogName = "Log Name:";
            ThumbName = "Thumbnail Name:";
            ListPaths = new ObservableCollection<string>();
            ListPaths.Add("shani");
        }

        public SettingsModel Sm
        {
            get { return this.Sm; }
            set { this.Sm = value; }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            
        }

        /*public void Remove(string path)
        {
            if (path != null) ListPaths.Remove(path);
        }*/

        private void OnRemove(object obj)
        {

        }

        private bool CanRemove(object obj)
        {
            return true;
        }


    }
}
