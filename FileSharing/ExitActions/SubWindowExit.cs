using FileSharing.ModelBase;
using FileSharing.SubWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileSharing.ExitActions
{
    public class SubWindowExit:ViewModelBase
    {
        private static SettingWindow settingWindow;
        public static SettingWindow SettingWindow
        {
            get { return settingWindow; }
            set { settingWindow = value;
                   OnPropertyChangeStatic(nameof(SettingWindow));
              
                }
        }  
        public static void SettingWindowExit()
        {
            if (settingWindow != null)
            {
                settingWindow.Close();
            }
        }
    }
}
