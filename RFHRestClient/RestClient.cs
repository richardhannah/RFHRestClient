using log4net;
using RFHRestClient.Auth;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RFHRestClient
{
    class RestClient
    {

        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(RestClient));
        public String BaseUrl { get; set; }
        public HttpAuthType AuthType { get; set; }

        private int retries;

        public RestClient(string baseUrl, HttpAuthType authType)
        {
            BaseUrl = baseUrl;
            AuthType = authType;
        }

        public HttpResponseMessage get(string resource)
        {
            LOGGER.Debug("getting resource : " + resource);

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (AuthType != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", AuthType.Authorization);
                }
                client.Timeout = new TimeSpan(0, 0, 30);

                try
                {
                    httpResponseMessage = client.GetAsync(resource).Result;
                }
                catch (AggregateException ex)
                {

                    throw new HttpRequestException("Request timed out", ex);
                }
            }


            if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                LOGGER.Debug("Authorization failed");
                if (retries < 3)
                {
                    AuthType.Authenticate();
                    retries++;
                    LOGGER.Debug($"Retrying - attempt {retries}");
                    get(resource);
                }
            }
            retries = 0;

            return httpResponseMessage;
        }

        public HttpResponseMessage post(string resource, string body)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            StringContent queryString = new StringContent(body);
            queryString.Headers.Clear();
            queryString.Headers.Add("Content-Type", "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (AuthType != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", AuthType.Authorization);
                }
                client.Timeout = new TimeSpan(0, 0, 30);

                try
                {
                    httpResponseMessage = client.PostAsync(resource, queryString).Result;
                }
                catch (AggregateException ex)
                {

                    throw new HttpRequestException("Request timed out", ex);
                }
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                LOGGER.Debug("Authorization failed");
                if (retries < 3)
                {
                    AuthType.Authenticate();
                    retries++;
                    LOGGER.Debug($"Retrying - attempt {retries}");
                    post(resource, body);
                }
            }
            retries = 0;

            return httpResponseMessage;
        }


        public HttpResponseMessage put(string resource, string body)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            StringContent queryString = new StringContent(body);
            queryString.Headers.Clear();
            queryString.Headers.Add("Content-Type", "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (AuthType != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", AuthType.Authorization);
                }
                client.Timeout = new TimeSpan(0, 0, 30);
                try
                {
                    httpResponseMessage = client.PutAsync(resource, queryString).Result;
                }
                catch (AggregateException ex)
                {
                    throw new HttpRequestException("Request timed out", ex);
                }
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                LOGGER.Debug("Authorization failed");
                if (retries < 3)
                {
                    AuthType.Authenticate();
                    retries++;
                    LOGGER.Debug($"Retrying - attempt {retries}");
                    put(resource, body);
                }
            }
            retries = 0;

            return httpResponseMessage;
        }

        public HttpResponseMessage delete(string resource)
        {
            LOGGER.Debug("deleting resource : " + resource);

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (AuthType != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", AuthType.Authorization);
                }
                client.Timeout = new TimeSpan(0, 0, 30);

                try
                {
                    httpResponseMessage = client.DeleteAsync(resource).Result;
                }
                catch (AggregateException ex)
                {

                    throw new HttpRequestException("Request timed out", ex);
                }
            }


            if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                LOGGER.Debug("Authorization failed");
                if (retries < 3)
                {
                    AuthType.Authenticate();
                    retries++;
                    LOGGER.Debug($"Retrying - attempt {retries}");
                    get(resource);
                }
            }
            retries = 0;

            return httpResponseMessage;
        }






    }
}
