using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;

using System.Windows.Input;
namespace FileSharing.Models
{
    public class TabViewModel:ObservableObject
    {
        public ObservableCollection<TabItem> Tabs { get; set; }
        private TabItem selectedTab;
        public TabItem SelectedTab
        {
            get => selectedTab;
            set=>SetProperty(ref selectedTab, value);
        }
        public ICommand SwitchTab => new RelayCommand(SwitchToTab2);
        public TabViewModel()
        {
            
        }
        
        public void SwitchToTab1()
        {
            SelectedTab = Tabs[0];
        }
        public void SwitchToTab2()
        {
            SelectedTab = Tabs[1];
        }
    }
}
