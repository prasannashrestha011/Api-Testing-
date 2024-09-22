using FileSharing.DataStructure;
using FileSharing.LocalStorage;
using FileSharing.ModelBase;
using FileSharing.NavigatorStores;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharing.Models
{
    public class SettingMainViewModel:ViewModelBase
    {
        public SettingNavigationStore settingNavigationStore;
        public ViewModelBase CurrentViewModel => settingNavigationStore.CurrentViewModel;

        public ViewModelBase SettingSideBarViewModel => new SettingSideBarViewModel(settingNavigationStore);
        private ThemeStructure theme;
        public ThemeStructure Theme
        {
            get { return theme; }
            set
            {
                theme = value;

                OnPropertyChange(nameof(Theme));

            }
        }
        private string fontName;
        public string FontName
        {
            get { return fontName; }
            set
            {
                fontName = value;
                OnPropertyChange();
            }
        }
        public SettingMainViewModel(SettingNavigationStore settingNavigationStore)
        {
            this.settingNavigationStore = settingNavigationStore;
            settingNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            LoadTheme();
            LoadFont();
      
            AppearanceLocalStorage.ThemeChanged += LoadTheme;
            AppearanceLocalStorage.FontChanged += LoadFont;
        }
        private void OnCurrentViewModelChanged()
        {
          OnPropertyChange(nameof(CurrentViewModel));
        }
        private void LoadTheme()
        {
            var ThemeCode = AppearanceLocalStorage.LoadThemeFromLocalStorage("applicationTheme.txt") ?? "Yellow";

            switch (ThemeCode)
            {
                case "Default":
                    Theme = new ThemeStructure("White", "Black", "White");
                    break;
                case "Dark Olive":
                    Theme = new ThemeStructure("#181C14", "#FCFAEE", "#1E1E1E");
                    break;
                case "Charcoal":
                    Theme = new ThemeStructure("#3C3D37", "#F5F5F5", "#1E1E1E");
                    break;
                case "Midnight Purple":
                    Theme = new ThemeStructure("#2E073F", "#F5F5F5", "#1E1E1E");
                    break;
                case "Dark Teal":
                    Theme = new ThemeStructure("#1A3636", "#F5F5F5", "#1E1E1E");
                    break;
                case "Dark 100":
                    Theme = new ThemeStructure("#121212", "#F5F5F5", "#282828");
                    break;
            }

        }
        private void LoadFont()
        {
            FontName = AppearanceLocalStorage.LoadFontFromLocalStorage("appFont.txt");
        }
    }
}
