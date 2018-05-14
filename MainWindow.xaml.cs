using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServiceGUI
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        /// <summary>
        /// Creates the mainwindow
        /// </summary>
        public MainWindow()
        {

            InitializeComponent();
            this.DataContext = new ViewModel();
            
        }

        /// <summary>
        /// Closes the connection
        /// </summary>
        /// <param object="sende"></param>
        /// <param EventArgs="args"></param>
        public void Close(object sende, EventArgs args)
        {
            Connect c = Connect.Instance;
            c.Write("3");
            c.CloseConnction();
            Console.WriteLine("Disconnected");
        }


    }
}
