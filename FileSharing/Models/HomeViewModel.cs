using FileSharing.Apis;
using FileSharing.CommandBaseSource;
using FileSharing.Components;
using FileSharing.DataStructure;
using FileSharing.ExitActions;
using FileSharing.LocalStorage;
using FileSharing.ModelBase;
using FileSharing.SubWindows;
using FileSharing.tools;
using ICSharpCode.AvalonEdit.Document;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
namespace FileSharing.Models
{
    public class HomeViewModel:ViewModelBase
    {
        private string httpData;
       
        public string HttpData
        {
            get { return httpData; }
            set { httpData = value;
                OnPropertyChange(nameof(HttpData));
                CheckParams(httpData);
            }
        }
        private dynamic httpResponse;
        public dynamic HttpResponse
        {
            get { return httpResponse; }
            set
            {
                httpResponse = value;
                OnPropertyChange(nameof(HttpResponse));
            }
        }
       
        private string httpBody;
        public string HttpBody
        {
            get { return httpBody; }
            set
            {
                httpBody = value;          
                OnPropertyChange(nameof(HttpBody));
               
            }
        }
   
        private string requestType;
        public string RequestType
        {
            get { return requestType; }
            set
            {
                requestType = value;
                OnPropertyChange(nameof(RequestType));
               
            }
        }
        private int httpStatusCode;
        public int HttpStatusCode
        {
            get { return httpStatusCode; }
            set
            {
                httpStatusCode = value;
                OnPropertyChange(nameof(HttpStatusCode));
            }
        }
        private int httpStatus=202;

        private HttpHeader httpHeader;
        public HttpHeader HttpHeader
        {
            get
            {
                return httpHeader;
            }
            set
            {
                httpHeader = value;
                OnPropertyChange(nameof(HttpHeader));
            }
        }
        public int HttpStatus
        {
            get { return httpStatus; }
            set
            {
                httpStatus = value;
                OnPropertyChange(nameof(HttpStatus));
              
            }
        }

        private ObservableCollection<HttpParams> httpParams;
        public ObservableCollection<HttpParams> HttpParams
        {
            get { return httpParams; }
            set
            {
                httpParams = value;
                OnPropertyChange();
            }
        }
        private TextDocument _textDocument;
        public TextDocument TextDocument
        {
            get { return _textDocument; }
            set
            {
                _textDocument = value;
                OnPropertyChange();
            }
        }
        public TabViewModel TabControl => new TabViewModel();
       
        static private string _flowDoc=null;
       static public string FlowDoc
        {
            get { return _flowDoc; }
            set
            {
                _flowDoc = value;

                OnPropertyChangeStatic(nameof(FlowDoc));
               
            }
        }
        public ObservableCollection<HttpStructure> _httpListData;
        
        public ObservableCollection<string> RequestTypes { get; set; }
        public ICommand SubmitCommand => new RelayCommand(execute => RequestMethod(requestType), canExecute => { return !string.IsNullOrEmpty(httpData); });

        public ICommand OpenSettingWindowCommand => new RelayCommand(execute =>OpenSettingWindow(), canExecute => true);

        private ThemeStructure theme;
        public ThemeStructure Theme
        {
            get { return theme; }
            set
            {
                theme = value;
                
                OnPropertyChange(nameof(Theme));
               
            }
        }
        private string fontName;
        public string FontName
        {
            get { return fontName; }
            set
            {
                fontName = value;
                OnPropertyChange();
            }
        }

        string paramKey, paramValue;
        private string userValue="test";
        public string UserValue
        {
            get { return userValue; }
            set
            {
                userValue = value;
                OnPropertyChange();
                MessageBox.Show("propertyChanged");
            }
        }
        public ObservableCollection<HttpSenderHeader> HttpSenderHeaderList { get; set; }
        public ICommand AddNewHttpHeader => new RelayCommand(execute => AddSenderHeaderInputField(), canExecute => true);
    
        public HomeViewModel()
        {
           
          
            TextDocument = new TextDocument();
             LoadTheme();
            LoadFont();
            AppearanceLocalStorage.ThemeChanged += LoadTheme;
            AppearanceLocalStorage.FontChanged += LoadFont;
            _httpListData = new ObservableCollection<HttpStructure>();
            RequestTypes = new ObservableCollection<string> { "GET", "POST","PATCH","PUT","DELETE" }; 
            RequestType = RequestTypes[0];
            HttpSelectedState.HttpDataChanged += GetSelectedHttpData;
            HttpParams = new ObservableCollection<HttpParams>();
            HttpSenderHeaderList= new ObservableCollection<HttpSenderHeader>();
        }
        private void LoadTheme()
        {
           var ThemeCode = AppearanceLocalStorage.LoadThemeFromLocalStorage("applicationTheme.txt") ?? "Yellow";
            
            switch (ThemeCode)
            {
                case "Default":
                    
                    Theme = new ThemeStructure("White", "Black","White");
                    Application.Current.Resources["FontColor"] = "Black";
                    break;
                case "Dark Olive":
                    Theme = new ThemeStructure("#181C14", "#FCFAEE", "#1E1E1E");
                    Application.Current.Resources["FontColor"] = "FCFAEE";
                    break;
                case "Charcoal":
                    Theme = new ThemeStructure("#3C3D37", "#F5F5F5", "#1E1E1E");
                    Application.Current.Resources["FontColor"] = "FCFAEE";
                    break;
                case "Midnight Purple":
                    Theme = new ThemeStructure("#2E073F", "#F5F5F5", "#1E1E1E");
                    Application.Current.Resources["FontColor"] = "FCFAEE";
                    break;
                case "Dark Teal":
                    Theme = new ThemeStructure("#1A3636", "#F5F5F5", "#1E1E1E");
                    Application.Current.Resources["FontColor"] = "FCFAEE";
                    break;
                case "Dark 100":
                    Theme = new ThemeStructure("#121212", "#F5F5F5", "#282828");
                    Application.Current.Resources["FontColor"] = "FCFAEE";
                    break;
            }
         
        }
        private void LoadFont()
        {
            FontName = AppearanceLocalStorage.LoadFontFromLocalStorage("appFont.txt");
        }
       
        private void SaveHttpListDataToLocalStoreage()
        {
           
            HttpStructure _httpStoredList=new HttpStructure(httpData,httpBody,httpResponse, httpHeader, httpParams);
            _httpListData.Add(_httpStoredList);
            UserLocalStorage.SaveDataToLocalStorage("httpList.txt", _httpListData);
             

        }
        private async void RequestMethod(string method)
        {
            try
            {
                HttpStatus = 199;
                SendRequest sendRequest = new SendRequest(httpData, HttpSenderHeaderList);
                var httpResponseString="";
                switch (method)
                {
                    case "GET":
                        ( httpResponseString, HttpStatusCode, HttpHeader) = await sendRequest.GetRequest();
                        break;
                    case "POST":
                        ( httpResponseString, HttpStatusCode, HttpHeader) = await sendRequest.PostRequest(_flowDoc);
                        break;
                    case "PUT":
                        ( httpResponseString, HttpStatusCode, HttpHeader) = await sendRequest.PatchRequest(_flowDoc);
                        break;
                    case "PATCH":
                        (httpResponseString, HttpStatusCode, HttpHeader) = await sendRequest.PutRequest(_flowDoc);
                        break;
                    case "DELETE":
                        ( httpResponseString, HttpStatusCode, HttpHeader) = await sendRequest.DeleteRequest(_flowDoc);
                        break;

                }
                var JsonFormatter = new JsonFormatter(httpResponseString);
                var formattedJson = JsonFormatter.FormatJson();

                if (formattedJson == "url not found")
                    HttpStatus = 404;
                else
                    HttpStatus = 200;

                HttpResponse = formattedJson;
                TextDocument.Text = formattedJson;
                SaveHttpListDataToLocalStoreage();
            }
            catch (Exception e) {
                HttpResponse = " url not found";
            }
        }
        private void GetSelectedHttpData()
        {
            HttpData = HttpSelectedState.HttpDomain;
            HttpBody = HttpSelectedState.HttpBody;
            HttpResponse = HttpSelectedState.HttpResponse;
        }
     
        
        private async void CheckParams(string httpDomainPath)
        {
           
            if (httpDomainPath.Contains("?"))
            {
                string queryString = httpDomainPath.Split("?")[1];
                if (queryString.Length > 0)
                {
                    string[] parameters = queryString.Split("&");
                    var currentKeys = new HashSet<string>();

                    foreach(var parameter in parameters)
                    {
                        string[] parameterParts=parameter.Split("=");
                        string paramKey= parameterParts[0];
                        string paramValue= parameterParts.Length>1?parameterParts[1].Trim():string.Empty;
                        if (!string.IsNullOrEmpty(paramKey))
                        {
                            currentKeys.Add(paramKey);
                            var existingParams = HttpParams.FirstOrDefault(p => p.key == paramKey);
                             if (existingParams!=null)
                            {
                                int index=HttpParams.IndexOf(existingParams);
                                HttpParams[index]=new HttpParams(paramKey, paramValue);
                            }
                            else
                            {
                                HttpParams.Add(new HttpParams(paramKey, paramValue));
                            }
                        }
                    }
                    for (int i= HttpParams.Count - 1;i >= 0; i--)
                    {

                        if (!currentKeys.Contains(HttpParams[i].key))
                        {
                            HttpParams.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    HttpParams.Clear();
                }


            }
            }

        private void AddSenderHeaderInputField()
        {
       
            HttpSenderHeaderList.Add(new HttpSenderHeader(new TextBox(), new TextBox()));
        } 
      
     
        private  void OpenSettingWindow()
        {
            SettingWindow settingWindow=new SettingWindow();
            settingWindow.Show();
            SubWindowExit.SettingWindow = settingWindow;
        }

        }
}
