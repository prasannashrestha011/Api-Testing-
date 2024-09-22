using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharing.tools
{
    public class JsonFormatter
    {
        public string plainJson;
        public JsonFormatter(string plainJson)
        {
            this.plainJson = plainJson;
        }
        public  string FormatJson()
        {
            try
            {
                var parsedJson = JsonConvert.DeserializeObject<dynamic>(plainJson);
                var formattedJson = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                return formattedJson;
            }catch(Exception ex)
            {
                return "url not found";
            }
        }
    }
}
