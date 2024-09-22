using FileSharing.ModelBase;
using FileSharing.NavigatorStores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using FileSharing.LocalStorage;
namespace FileSharing.Models
{
    public class ThemeViewModel:ViewModelBase
    {
        public SettingNavigationStore settingNavigationstore;

        public ObservableCollection<string> Themes { get; set; }
        private string selectedThemeItem;
        public string SelectedThemeItem
        {
            get { return selectedThemeItem; }
            set
            {
                selectedThemeItem = value;
                OnPropertyChange(nameof(SelectedThemeItem));
                OnThemeSelect();

            }
        }
        public ThemeViewModel(SettingNavigationStore settingNavigationStore) 
        {
            Themes = new ObservableCollection<string> { "Default", "Dark Olive", "Midnight Purple", "Charcoal", "Dark Teal", "Dark 100" };

            SelectedThemeItem = AppearanceLocalStorage.LoadThemeFromLocalStorage("applicationTheme.txt") ?? "Default"; ;
             this.settingNavigationstore = settingNavigationStore;
        }
        private void OnThemeSelect()
        {
            
          
            AppearanceLocalStorage.SaveThemeToLocalStorage("applicationTheme.txt", SelectedThemeItem);


        }

    }
}
