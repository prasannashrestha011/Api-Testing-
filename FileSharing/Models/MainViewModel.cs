using CommunityToolkit.Mvvm.ComponentModel;
using FileSharing.ModelBase;
using FileSharing.NavigatorStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FileSharing.Models
{
    public class MainViewModel:ViewModelBase
    {
       
        public MainNavigator _mainNavigator;
        public ViewModelBase CurrentViewModel => _mainNavigator.CurrentViewModel;

        public MainViewModel(MainNavigator _mainNavigator )
        {
            this._mainNavigator = _mainNavigator;
            _mainNavigator.CurrentViewModelChanged += OnViewModelChanged;
        }
        protected void OnViewModelChanged()
        {
            OnPropertyChange(nameof(CurrentViewModel));
        }
    }
}
