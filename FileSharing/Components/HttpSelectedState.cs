using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharing.Components
{
    public class HttpSelectedState
    {
        public static event Action HttpDataChanged;
        private static string httpDomain;
        private static string httpBody;
        private static string httpResponse;
        public static string HttpDomain
        {
            get { return httpDomain; }
            set
            {
                httpDomain = value;
                OnHttpDataChanged();
            }
        }
        public static string HttpBody
        {
            get { return httpBody; }
            set
            {
                httpBody = value;
                OnHttpDataChanged();
            }
        }
        public static dynamic HttpResponse{
            get { return httpResponse; }
            set
            {
                httpResponse = value;
                OnHttpDataChanged();
            }
        }
       public  static void AssignSelectedHttpItem(string httpDomain,string httpBody,dynamic httpResponse)
        {
            HttpDomain = httpDomain;
            HttpResponse = httpBody;
            HttpResponse = httpResponse;

        }
        private static  void OnHttpDataChanged()
        {
            HttpDataChanged?.Invoke();
        }
    }
}
