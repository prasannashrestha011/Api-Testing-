using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileSharing.ModelBase
{
    public class ViewModelBase : INotifyPropertyChanged
    {
      
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChange([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }
        static public void OnPropertyChangeStatic(string propertyName)
        {
            // Implement static property changed logic here (or notify manually if necessary)
        }
    }
}
