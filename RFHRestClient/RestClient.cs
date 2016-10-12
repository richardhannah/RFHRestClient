using log4net;
using System;
using System.Net.Http;
using System.Net;
using RFHRestClient.Config;

namespace RFHRestClient
{
    public class RestClient : IRestClient
    {
        #region Private member vars
        private HttpClient httpClient;
        #endregion

        #region Properties
        public Configuration Configuration { get; set; }
        #endregion

        #region Constructors
        public RestClient() {}

        public RestClient (Configuration config)
        {
            this.Configuration = config;
            ConfigureClient();
        }
        #endregion

        #region Public Methods
        public HttpResponseMessage Get(Uri uri)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            
            using (httpClient)
            {
                try {
                    httpResponseMessage = httpClient.GetAsync(uri).Result;
                }
                catch(AggregateException ex)
                {                    
                    throw new HttpRequestException("Request timed out",ex);
                }
            }

            if(httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {                
                if(Configuration.Retries < 3)
                {                    
                    Configuration.AuthType.Authenticate();
                    Configuration.Retries++;
                    Get(uri);
                }
            }
            Configuration.Retries = 0;            

            return httpResponseMessage;
        }

        public T GetObject<T>()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private methods
        private void ConfigureClient()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Configuration.BaseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(Configuration.MediaType);
            httpClient.Timeout = new TimeSpan(0, 0, Configuration.Timeout);
            httpClient.DefaultRequestHeaders.Authorization = Configuration.AuthType != null ? Configuration.AuthType.Authorization : null;
        }
        #endregion
    }
}
