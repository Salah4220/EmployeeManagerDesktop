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
    /// Logique d'interaction pour DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Window
    {
        public DashboardView()
        {
            InitializeComponent();
            DataContext = new DashboardViewModel(new MessageService());
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid grid && grid.SelectedItem is TaskDto selectedTask)
            {
                var editWindow = new EditTaskWindow(selectedTask);
                editWindow.ShowDialog();
            }
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            var newTask = new NewTaskView(new TaskDto());
            newTask.ShowDialog();
        }
    }
}
