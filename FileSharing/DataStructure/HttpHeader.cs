using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace FileSharing.DataStructure
{
    public class HttpHeader
    {
        [JsonProperty("Date")]
        public string Date { get; set; }
        [JsonProperty("Content-Type")]
        public string? Content_Type { get; set; }
        [JsonProperty("Transfer-Encoding")]
        public string Transfer_Encoding {  get; set; }

        [JsonProperty("Connection")]
        public string Connection { get; set; }

        [JsonProperty("Report-To")]
        public dynamic Report_To { get; set; }

        [JsonProperty("Reporting-Endpoints")]
        public dynamic Reporting_Endpoints { get; set; }

        [JsonProperty("Nel")]
        public dynamic Nel { get; set; }

        [JsonProperty("Age")]
        public string Age { get; set; }

        [JsonProperty("Authorization")]
        public string Authorization { get; set; }
        public HttpHeader(string date, string? content_Type, string transfer_Encoding, string connection, dynamic report_To, dynamic reporting_Endpoints, dynamic nel, string age, string authorization)
        {
            Date = date;
            Content_Type = content_Type;
            Transfer_Encoding = transfer_Encoding;
            Connection = connection;
            Report_To = report_To;
            Reporting_Endpoints = reporting_Endpoints;
            Nel = nel;
            Age = age;
            Authorization = authorization;

           
        }
    }
}
