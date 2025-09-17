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

namespace EmployeeManager.Desktop.Views
{
    /// <summary>
    /// Logique d'interaction pour SignInFrame.xaml
    /// </summary>
    public partial class SignInFrame : Window
    {
        public SignInFrame()
        {
            InitializeComponent();
            //DataContext = new SignInViewModel();
            DataContext = new SignInViewModel(new MessageService());

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is SignInViewModel vm)
                vm.Password = PasswordBox.Password;
        }
        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is SignInViewModel vm)
                vm.ConfirmPassword = ConfirmPasswordBox.Password;
        }
    }
}
