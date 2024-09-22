using FileSharing.ExitActions;
using FileSharing.Models;
using FileSharing.NavigatorStores;
using FileSharing.SubWindows;
using FileSharing.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace FileSharing
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var MainWindow= new MainWindow();
            var mainNavStore = new MainNavigator();
            mainNavStore.CurrentViewModel = new HomeViewModel();

            var MainViewModel= new MainViewModel(mainNavStore);
            MainWindow.DataContext = MainViewModel;
            MainWindow.Show();
            MainWindow.Closed += OnApplicationClose;
            
        }
        private void OnApplicationClose(object sender, EventArgs  e)
        {
         
            SubWindowExit.SettingWindowExit();
        }
    }

}
