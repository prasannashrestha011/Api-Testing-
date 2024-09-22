using FileSharing.DataStructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSharing.LocalStorage
{
    public  class UserLocalStorage
    {
        public static event Action? LocalStorageChanged;
     

        public static void SaveDataToLocalStorage(string key, ObservableCollection<HttpStructure> data)
        {
            if (string.IsNullOrEmpty(key))
                return;
            ObservableCollection<HttpStructure> existingData=new ObservableCollection<HttpStructure>();
       
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isoStore.FileExists(key))
                {
                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(key, FileMode.Open, isoStore))
                    {
                        using (StreamReader reader = new StreamReader(isoStream))
                        {
                            var existingContent=reader.ReadToEnd();
                            if (!string.IsNullOrEmpty(existingContent))
                            {
                                existingData = JsonConvert.DeserializeObject<ObservableCollection<HttpStructure>>(existingContent);
                            }

                        }
                    }
                }
                foreach (var item in data)
                {
                    existingData.Add(item);
                }
                var parsedData=JsonConvert.SerializeObject(existingData);

                using(IsolatedStorageFileStream isoStream=new IsolatedStorageFileStream(key,FileMode.Create,isoStore))
                {
                    using(StreamWriter writer=new StreamWriter(isoStream))
                    {
                        writer.Write(parsedData);
                        OnLocalStorageChanged();
                      
                    }
                }
              


            }
        }

        public static ObservableCollection<HttpStructure> LoadHttpDataFromLocalStorage(string key)
        {
            using(IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isoStore.FileExists(key))
                {
                    using(IsolatedStorageFileStream isoStream=new IsolatedStorageFileStream(key,FileMode.Open, isoStore))
                    {
                        using(StreamReader reader=new StreamReader(isoStream))
                        {
                            var parseData=reader.ReadToEnd();
                          
                            var rawdata=JsonConvert.DeserializeObject<ObservableCollection<HttpStructure>>(parseData);
                           
                            return rawdata;
                        }
                    }
                }
                else
                {
                    return null;
                }

            }

        }
        public static bool DeleteHttpDataFromLocalStorage(string key)
        {
            ObservableCollection<HttpStructure> rawData;
            using(IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isoStore.FileExists(key)) {
                    isoStore.DeleteFile(key);
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }
        private static void OnLocalStorageChanged()
        {
            LocalStorageChanged?.Invoke();
        }
    }
}
