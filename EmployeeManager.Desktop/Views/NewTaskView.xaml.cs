using EmployeeManager.Desktop.Services;
using EmployeeManager.Desktop.ViewModels;
using EmployeeManager.Shared;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace EmployeeManager.Desktop.Views
{
    /// <summary>
    /// Logique d'interaction pour NewTaskView.xaml
    /// </summary>
    public partial class NewTaskView : Window
    {
        public NewTaskView(TaskDto task)
        {
            InitializeComponent();
           
            DataContext = new NewTaskViewModel(task, new MessageService());
        }
                private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
