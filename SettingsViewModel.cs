using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace ServiceGUI
{
    class SettingsViewModel
    {
        private SettingsModel sm;
        public event PropertyChangedEventHandler PropertyChanges;
        private object SelectedItems;

        public SettingsViewModel()
        {
            this.sm = new SettingsModel();
            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
            PropertyChanges += RemoveCommandPropertyChanged;
            sm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        public Object SelectedItem
        {
            get { return SelectedItems; }
            set
            {
                this.SelectedItems = value;
                NotifyPropertyChanged("SelectedItem");
            }
        }

        public ObservableCollection<Object> ListPaths
        {
            get { return sm.ListPaths; }
            set { sm.ListPaths = value; }
        }

        private void RemoveCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var command = this.RemoveCommand as DelegateCommand<object>;
            command.RaiseCanExecuteChanged();
        }

        public SettingsModel Sm
        {
            get { return this.sm; }
            set { this.sm = value; }
        }

        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanges?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public SettingsViewModel settingsViewModel { get; set; }

        private void OnRemove(Object s)
        {
            Connect c = Connect.Instance;
            c.Write("5," + SelectedItem.ToString());
            
            
        }

        public ICommand RemoveCommand { get; private set; }

        private bool CanRemove(Object s)
        {
            if (SelectedItem != null) return true;
            else return false;
        }


    }
}

