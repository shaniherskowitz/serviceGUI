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

        /// <summary>
        /// Creates the settingViewModel
        /// </summary>
        
        public SettingsViewModel()
        {
            this.sm = new SettingsModel();
            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
            PropertyChanges += RemoveCommandPropertyChanged;
            sm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        /// <summary>
        /// Sets and gets the selected item property
        /// </summary>
        /// <returns>SelectedItems.</returns>
        public Object SelectedItem
        {
            get { return SelectedItems; }
            set
            {
                this.SelectedItems = value;
                NotifyPropertyChanged("SelectedItem");
            }
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns>ListPaths</returns>
        public ObservableCollection<Object> ListPaths
        {
            get { return sm.ListPaths; }
            set { sm.ListPaths = value; }
        }

        /// <summary>
        /// Test value and modify it.
        /// </summary>
        /// <param object="sender"></param>
        /// <param propertychangedeventargs="e"></param>
        private void RemoveCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var command = this.RemoveCommand as DelegateCommand<object>;
            command.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Sets and gets the setting model
        /// </summary>
        /// <returns>setting model</returns>
        public SettingsModel Sm
        {
            get { return this.sm; }
            set { this.sm = value; }
        }

         /// <summary>
        /// Notifies when a property has changed- invokes new event 
        /// </summary>
        /// <param string="name"></param>
        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanges?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// sets and gets the settingviewmodel
        /// </summary>
        public SettingsViewModel settingsViewModel { get; set; }

        /// <summary>
        /// Will remove selected item from the list - writes to the server
        /// </summary>
        /// <param object="s"></param>
        private void OnRemove(Object s)
        {
            Connect c = Connect.Instance;
            c.Write("5," + SelectedItem.ToString());
            
            
        }

         /// <summary>
        /// Gets and send the remove command
        /// </summary>
        public ICommand RemoveCommand { get; private set; }

         /// <summary>
        /// Sees if can remove the selected item or not
        /// </summary>
        /// <param object="s"></param>
        /// <return true or false<return>
        private bool CanRemove(Object s)
        {
            if (SelectedItem != null) return true;
            else return false;
        }


    }
}

