using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharing.DataStructure
{
    public class HttpStructure
    {
        public string? HttpDomain { get; set; }
        public string? HttpBody { get; set; }
        public string? HttpResponse { get; set; }
        public HttpHeader HttpHeader { get; set; }
        public ObservableCollection<HttpParams> HttpParams { get; set; }
        public HttpStructure(string httpdomain,string httpbody,string httpresponse,HttpHeader httpHeader, ObservableCollection<HttpParams> httpParams)
        {
            HttpDomain = httpdomain;
            HttpBody = httpbody;
            HttpResponse = httpresponse;
            HttpHeader = httpHeader;
            HttpParams = httpParams;
        }
        
    }
}
