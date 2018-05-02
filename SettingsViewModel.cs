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
        

        public SettingsViewModel()
        {
            //this.RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove);

            this.sm = new SettingsModel();
            
        }

        public SettingsModel Sm
        {
            get { return this.sm; }
            set { this.sm = value; }
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
