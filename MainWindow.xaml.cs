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
        
        

        public MainWindow()
        {

            InitializeComponent();
            this.DataContext = new ViewModel();
            

            /*UpdateFrame(TB1, "shani");
            urls.ItemsSource = vm.ListPaths;
            logInfo.ItemsSource = vm.ListCommands;
            Model m = new Model(vm);
            m.ConnectToServer();*/


        }

        /*private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            vm.Remove(urls.SelectedItem.ToString());
        }

        private void UpdateFrame(TextBlock tb, string path)
        {
            tb.Text = "Output Directory: " + path;
        }*/

       
    }
}
