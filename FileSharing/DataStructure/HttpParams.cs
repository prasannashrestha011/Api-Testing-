using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharing.DataStructure
{
    public class HttpParams
    {
        public string key { get; set; }
        public string value {  get; set; }
        public HttpParams(string key,string value) { 
            this.key = key;
            this.value = value;
        }
    }
}
