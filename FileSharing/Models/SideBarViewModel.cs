using CommunityToolkit.Mvvm.ComponentModel;
using FileSharing.Commands;
using FileSharing.Components;
using FileSharing.DataStructure;
using FileSharing.LocalStorage;
using FileSharing.ModelBase;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FileSharing.Models
{
    public class SideBarViewModel:ViewModelBase
    {
        private ObservableCollection<HttpStructure> _saveList;
        public ObservableCollection<HttpStructure> SaveList
        {
            get {  return _saveList; }
            set
            {
                _saveList = value;
             
                OnPropertyChange(nameof(SaveList));
            }
        }
        private HttpStructure selectedHttpItem;
        public HttpStructure SelectedHttpItem
        {
            get { return selectedHttpItem; }
            set
            {
                selectedHttpItem = value;
                OnPropertyChange(nameof(SelectedHttpItem));
                OnItemSelect();
            }
        }

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

        public ICommand ClearList => new relayCommand(execute => ClearListFunc(),canExecute=>true);
        public ICommand RefreshList => new relayCommand(execute => RefresthListFunc(), canExecute => true);
        public SideBarViewModel()
        {
            LoadSaveListData();
            LoadTheme();
            LoadFont();
          
            AppearanceLocalStorage.ThemeChanged += LoadTheme;
            AppearanceLocalStorage.FontChanged += LoadFont;
        }
        private void LoadSaveListData()
        {
            SaveList = UserLocalStorage.LoadHttpDataFromLocalStorage("httpList.txt");
          
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
        private void OnItemSelect() 
        {
            if (SelectedHttpItem != null) 
            {
                HttpSelectedState.AssignSelectedHttpItem(SelectedHttpItem.HttpDomain, SelectedHttpItem.HttpBody, SelectedHttpItem.HttpResponse);
               
            }        
    
        }
        public void  ClearListFunc()
        {
            bool isListClear = UserLocalStorage.DeleteHttpDataFromLocalStorage("httpList.txt");
            if (!isListClear)
                MessageBox.Show("error clearing list", "Error");
            LoadSaveListData();
            OnPropertyChange(nameof(SaveList));
        }
        private void RefresthListFunc()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                LoadSaveListData();
                OnPropertyChange(nameof(SaveList));
            });

        }
    }
}
