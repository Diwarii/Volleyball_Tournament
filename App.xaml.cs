using System.Configuration;
using System.Data;
using System.Windows;
using Work.View;

namespace Work
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loginWindow = new LoginWindow();
            bool? result = loginWindow.ShowDialog();

            if (result == true && loginWindow.AuthenticatedUser != null)
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                Shutdown();
            }
        }
    }

}
