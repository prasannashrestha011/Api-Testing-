using FileSharing.CommandBaseSource;
using FileSharing.DataStructure;
using FileSharing.LocalStorage;
using FileSharing.ModelBase;
using FileSharing.NavigatorStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileSharing.Models
{
    public class SettingSideBarViewModel:ViewModelBase
    {
        public SettingNavigationStore settingNavigationStore;
        public ICommand themeNavCommand => new RelayCommand(execute => NavToThemeView(), canExecute => true);
        public ICommand fontsNavCommand=>new RelayCommand(execute=>NavToFontsView(), canExecute => true);
      
        public SettingSideBarViewModel(SettingNavigationStore settingNavigationStore)
        {
            this.settingNavigationStore = settingNavigationStore;
     
        }
        public void NavToThemeView()
        {
            settingNavigationStore.CurrentViewModel = new ThemeViewModel(settingNavigationStore);
        }
        public void NavToFontsView()
        {
            settingNavigationStore.CurrentViewModel=new FontsViewModel(settingNavigationStore);
        }
    
    }
}
