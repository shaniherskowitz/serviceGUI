using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;

namespace ServiceGUI
{

    class ViewModel
    {
         /// <summary>
        /// Gets and sets the color
        /// </summary>
   
        public SolidColorBrush Color1 { get; set; }

         /// <summary>
        /// Creates the viewmodel
        /// </summary>
     
        public ViewModel()
        {
             Connect c = Connect.Instance;
             if(c.connected)
             Color1 = new SolidColorBrush(Color.FromRgb(249, 249, 209));
             else Color1 = new SolidColorBrush(Color.FromRgb(228, 224, 229));

            
        }
      
    }
}
