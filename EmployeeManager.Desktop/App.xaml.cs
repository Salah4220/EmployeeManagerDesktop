using EmployeeManager.Desktop.ViewModels;
using EmployeeManager.Desktop.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace EmployeeManager.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
         /*   var loginView = new LoginFrame();
            loginView.Show();*/
        var signInView = new SignInFrame();
            signInView.Show();
        }

    }
}
