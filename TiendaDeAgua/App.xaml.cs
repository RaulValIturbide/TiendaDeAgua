using System.Windows;

namespace TiendaDeAgua
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var splash = new SplashScreen();
            splash.Show();

            // Simula carga o espera
            System.Threading.Thread.Sleep(2000);

            var main = new MainWindow();
            main.Show();

            splash.Close();
        }
    }

}
