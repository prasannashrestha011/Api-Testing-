using FileSharing.ModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharing.NavigatorStores
{
    public class MainNavigator
    {
        public event Action CurrentViewModelChanged;
        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                currentViewModel = value;
               OnCurrentViewModelChange();
            }
        }
        public void OnCurrentViewModelChange()
        {
            CurrentViewModelChanged?.Invoke();
        }
       
    }
}
