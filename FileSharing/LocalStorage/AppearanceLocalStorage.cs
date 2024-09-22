using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileSharing.LocalStorage
{
    public class AppearanceLocalStorage
    {
        public static event Action? ThemeChanged;
        public static event Action? FontChanged;
        public static void SaveThemeToLocalStorage(string key,string value)
        {
            using(IsolatedStorageFile isoStore = IsolatedStorageFile.GetMachineStoreForApplication())
            {
                using(IsolatedStorageFileStream isoStream=new IsolatedStorageFileStream(key, FileMode.Create, isoStore))
                {
                    using(StreamWriter writer=new StreamWriter(isoStream))
                    {
                        writer.Write(value);
                       
                    }
                }
            }
            OnThemeChanged();
        }
        public static string LoadThemeFromLocalStorage(string key)
        {
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetMachineStoreForApplication())
            {
                if (isoStore.FileExists(key))
                {
                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(key, FileMode.Open, isoStore))
                    {
                        using (StreamReader reader = new StreamReader(isoStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
                else
                {
                    return null;
                }

            }
        }
       
        public static void SaveFontToLocalStorage(string key,string value)
        {
            using(IsolatedStorageFile isoStore = IsolatedStorageFile.GetMachineStoreForApplication())
            {
                using(IsolatedStorageFileStream isoStream=new IsolatedStorageFileStream(key,FileMode.Create, isoStore))
                {
                    using(StreamWriter writer=new StreamWriter(isoStream))
                    {
                        writer.Write(value);
                       
                    }
                }
                OnFontChanged();
            }
        }
        public static string LoadFontFromLocalStorage(string key)
        {
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetMachineStoreForApplication())
            {
                if (isoStore.FileExists(key))
                {
                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(key, FileMode.Open, isoStore))
                    {
                        using(StreamReader reader = new StreamReader(isoStream))
                        {
                            var data = reader.ReadToEnd();
                            return data;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        public static void OnThemeChanged()
        {
            ThemeChanged?.Invoke();
        }
        public static void OnFontChanged()
        {
            FontChanged?.Invoke();  
        }
    }
}
