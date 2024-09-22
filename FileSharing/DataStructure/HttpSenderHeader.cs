using System.Windows;
using System.Windows.Controls;

namespace FileSharing.DataStructure
{
    public class HttpSenderHeader
    {
        public TextBox HeaderKey { get; set; }

        public TextBox HeaderValue { get;set; }
       
       
        public HttpSenderHeader(TextBox headerKey,TextBox headerValue) 
        { 
            HeaderKey = headerKey;
            HeaderValue = headerValue;
           
         
        }
    }
}
