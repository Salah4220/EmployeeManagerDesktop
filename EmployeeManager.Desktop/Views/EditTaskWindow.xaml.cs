using EmployeeManager.Desktop.Services;
using EmployeeManager.Desktop.ViewModels;
using EmployeeManager.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmployeeManager.Desktop.Views
{
    /// <summary>
    /// Logique d'interaction pour EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {

        public EditTaskWindow(TaskDto task)
        {
            InitializeComponent();
            DataContext = new EditTaskViewModel(task, new MessageService());
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
     }
}
