using FileSharing.LocalStorage;
using FileSharing.ModelBase;
using FileSharing.NavigatorStores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharing.Models
{
    public class FontsViewModel:ViewModelBase
    {
        public SettingNavigationStore settingNavigationStore;
        public ObservableCollection<string> FontList { get; set; }

        private string selectedFont;
        public string SelectedFont
        {
            get { return selectedFont; }
            set
            {
                selectedFont = value;
                OnPropertyChange(nameof(SelectedFont));
                OnFontSelect();
            }
        }
        public FontsViewModel(SettingNavigationStore settingNavigationStore)
        {
            this.settingNavigationStore = settingNavigationStore;
            FontList = new ObservableCollection<string>{"Arial","Calibri","Cambria",
                                                        "Comic Sans MS", "Consolas","Courier New",
                                                        "Georgia", "Helvetica",  "Impact",
                                                         "Lucida Console","Lucida Sans Unicode",    "Palatino Linotype",
                                                         "Segoe UI", "Tahoma",     "Times New Roman",
                                                         "Trebuchet MS",   "Verdana","Trebuchet MS" };
            SelectedFont = AppearanceLocalStorage.LoadFontFromLocalStorage("appFont.txt") ?? "Arial";
        }
        private void OnFontSelect()
        {
            AppearanceLocalStorage.SaveFontToLocalStorage("appFont.txt",SelectedFont);
        }
    }
}
