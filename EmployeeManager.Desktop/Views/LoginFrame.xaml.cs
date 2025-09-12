using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeManager.Desktop.Services;
using EmployeeManager.Desktop.ViewModels;
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

namespace EmployeeManager.Desktop
{
    /// <summary>
    /// Logique d'interaction pour LoginFrame.xaml
    /// </summary>
    public partial class LoginFrame : Window
    {


        public LoginFrame()
        {
            InitializeComponent();
            //DataContext = new SignInViewModel();
            DataContext = new SignInViewModel(new MessageService());
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
                vm.Password = PasswordBox.Password;
        }

    }
}
