using FileSharing.DataStructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileSharing.Apis
{
    public class SendRequest
    {
        private string domainAddress;
        private ObservableCollection<HttpSenderHeader> requestHeaders;
        private int httpStatusCode;
        
        public SendRequest(string domainAddress,ObservableCollection<HttpSenderHeader> requestHeaders) {
            this.domainAddress = domainAddress;
            this.requestHeaders = requestHeaders;
        }
        public async Task<(string, int,HttpHeader)> GetRequest()
        {
            using(var client= new HttpClient())
            {
                try
                {
                    if (requestHeaders != null)
                    {
                        foreach (var header in requestHeaders)
                        {
                            client.DefaultRequestHeaders.Add(header.HeaderKey.Text, header.HeaderValue.Text);
                        }
                    }
                    var response = await client.GetAsync(domainAddress);
                    var responseString = await response.Content.ReadAsStringAsync();
                    httpStatusCode = (int)response.StatusCode;
                  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpHeader receivedHeader = new HttpHeader(
                                 response.Headers.Date?.ToString(),  // Safe null check
                                 response.Content?.Headers.ContentType?.ToString(),  // Safe null check for content
                                 response.Headers.TransferEncoding?.ToString() ?? string.Empty,  // Provide default value in case of null
                                 response.Headers.Connection?.ToString() ?? string.Empty,  // Safe null check with default
                                 response.Headers.TryGetValues("Report-To", out var reportTo) ? string.Join(", ", reportTo) : null,
                                 response.Headers.TryGetValues("Reporting-Endpoints", out var reportingEndpoints) ? string.Join(", ", reportingEndpoints) : null,
                                 response.Headers.TryGetValues("Nel", out var nel) ? string.Join(", ", nel) : null,
                                 response.Headers.TryGetValues("Age", out var age) ? string.Join(", ", age) : null,
                                 response.Headers.TryGetValues("Authorization", out var authorization) ? authorization.FirstOrDefault() : null // Safe handling for missing Authorization
                             );


                    if (response.IsSuccessStatusCode)
                    {
                        return (responseString,httpStatusCode,receivedHeader);
                    }
                    else
                    {
                        return ("error",500,null);
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message.ToString());
                    return ("URL not found (404)",500,null) ;
                }
            }
        }
        public async Task<(string,int,HttpHeader)> PostRequest(string responseBody)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var jsonContent = JsonConvert.SerializeObject(responseBody);

                    var content = new StringContent(responseBody, Encoding.UTF8, "application/json");
                    if (requestHeaders != null)
                    {
                        foreach (var header in requestHeaders)
                        {
                            client.DefaultRequestHeaders.Add(header.HeaderKey.Text, header.HeaderValue.Text);
                        }
                    }
                    var response = await client.PostAsync(domainAddress, content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    HttpHeader receivedHeader = new HttpHeader(
                             response.Headers.Date?.ToString(),  // Safe null check
                             response.Content?.Headers.ContentType?.ToString(),  // Safe null check for content
                             response.Headers.TransferEncoding?.ToString() ?? string.Empty,  // Provide default value in case of null
                             response.Headers.Connection?.ToString() ?? string.Empty,  // Safe null check with default
                             response.Headers.TryGetValues("Report-To", out var reportTo) ? string.Join(", ", reportTo) : null,
                             response.Headers.TryGetValues("Reporting-Endpoints", out var reportingEndpoints) ? string.Join(", ", reportingEndpoints) : null,
                             response.Headers.TryGetValues("Nel", out var nel) ? string.Join(", ", nel) : null,
                             response.Headers.TryGetValues("Age", out var age) ? string.Join(", ", age) : null,
                             response.Headers.TryGetValues("Authorization", out var authorization) ? authorization.FirstOrDefault() : null // Safe handling for missing Authorization
                         );
                    httpStatusCode = (int)response.StatusCode;
                    return (responseString, httpStatusCode, receivedHeader);
                }
                catch (Exception ex) {
                    return ("URL not found (404)",500,null);
                }
               
            }
        } 
        public async Task<(string,int,HttpHeader)> PatchRequest(string responseBody)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var jsonContent = JsonConvert.SerializeObject(responseBody);
                    if (requestHeaders != null)
                    {
                        foreach (var header in requestHeaders)
                        {
                            client.DefaultRequestHeaders.Add(header.HeaderKey.Text, header.HeaderValue.Text);
                        }
                    }
                    var content = new StringContent(responseBody, Encoding.UTF8, "application/json");
                    var response = await client.PatchAsync(domainAddress, content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    HttpHeader receivedHeader = new HttpHeader(
                             response.Headers.Date?.ToString(),  // Safe null check
                             response.Content?.Headers.ContentType?.ToString(),  // Safe null check for content
                             response.Headers.TransferEncoding?.ToString() ?? string.Empty,  // Provide default value in case of null
                             response.Headers.Connection?.ToString() ?? string.Empty,  // Safe null check with default
                             response.Headers.TryGetValues("Report-To", out var reportTo) ? string.Join(", ", reportTo) : null,
                             response.Headers.TryGetValues("Reporting-Endpoints", out var reportingEndpoints) ? string.Join(", ", reportingEndpoints) : null,
                             response.Headers.TryGetValues("Nel", out var nel) ? string.Join(", ", nel) : null,
                             response.Headers.TryGetValues("Age", out var age) ? string.Join(", ", age) : null,
                             response.Headers.TryGetValues("Authorization", out var authorization) ? authorization.FirstOrDefault() : null // Safe handling for missing Authorization
                         );
                    httpStatusCode = (int)response.StatusCode;
                    return (responseString, httpStatusCode, receivedHeader);
                }
                catch (Exception ex) {
                    return ("URL not found (404)",500,null);
                }
               
            }
        }
        public async Task<(string,int,HttpHeader)> DeleteRequest(string responseBody)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var jsonContent = JsonConvert.SerializeObject(responseBody);
                    if (requestHeaders != null)
                    {
                        foreach (var header in requestHeaders)
                        {
                            client.DefaultRequestHeaders.Add(header.HeaderKey.Text, header.HeaderValue.Text);
                        }
                    }

                    var response = await client.DeleteAsync(domainAddress);
                    var responseString = await response.Content.ReadAsStringAsync();
                    HttpHeader receivedHeader = new HttpHeader(
                             response.Headers.Date?.ToString(),  // Safe null check
                             response.Content?.Headers.ContentType?.ToString(),  // Safe null check for content
                             response.Headers.TransferEncoding?.ToString() ?? string.Empty,  // Provide default value in case of null
                             response.Headers.Connection?.ToString() ?? string.Empty,  // Safe null check with default
                             response.Headers.TryGetValues("Report-To", out var reportTo) ? string.Join(", ", reportTo) : null,
                             response.Headers.TryGetValues("Reporting-Endpoints", out var reportingEndpoints) ? string.Join(", ", reportingEndpoints) : null,
                             response.Headers.TryGetValues("Nel", out var nel) ? string.Join(", ", nel) : null,
                             response.Headers.TryGetValues("Age", out var age) ? string.Join(", ", age) : null,
                             response.Headers.TryGetValues("Authorization", out var authorization) ? authorization.FirstOrDefault() : null // Safe handling for missing Authorization
                         );
                    httpStatusCode = (int)response.StatusCode;
                    return (responseString, httpStatusCode, receivedHeader);
                }
                catch (Exception ex) {
                    return ("URL not found (404)",500,null);
                }
               
            }
        }    
        public async Task<(string,int,HttpHeader)> PutRequest(string responseBody)
        {

            using (var client = new HttpClient())
            {
                try
                {
                    var jsonContent = JsonConvert.SerializeObject(responseBody);
                    if (requestHeaders != null)
                    {
                        foreach (var header in requestHeaders)
                        {
                            client.DefaultRequestHeaders.Add(header.HeaderKey.Text, header.HeaderValue.Text);
                        }
                    }
                    var content = new StringContent(responseBody, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(domainAddress, content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    HttpHeader receivedHeader = new HttpHeader(
                             response.Headers.Date?.ToString(),  // Safe null check
                             response.Content?.Headers.ContentType?.ToString(),  // Safe null check for content
                             response.Headers.TransferEncoding?.ToString() ?? string.Empty,  // Provide default value in case of null
                             response.Headers.Connection?.ToString() ?? string.Empty,  // Safe null check with default
                             response.Headers.TryGetValues("Report-To", out var reportTo) ? string.Join(", ", reportTo) : null,
                             response.Headers.TryGetValues("Reporting-Endpoints", out var reportingEndpoints) ? string.Join(", ", reportingEndpoints) : null,
                             response.Headers.TryGetValues("Nel", out var nel) ? string.Join(", ", nel) : null,
                             response.Headers.TryGetValues("Age", out var age) ? string.Join(", ", age) : null,
                             response.Headers.TryGetValues("Authorization", out var authorization) ? authorization.FirstOrDefault() : null // Safe handling for missing Authorization
                         );
                    httpStatusCode = (int)response.StatusCode;
                    return (responseString, httpStatusCode, receivedHeader);
                }
                catch (Exception ex) {
                    return ("URL not found (404)",500,null);
                }
               
            }
        }
    }
}
