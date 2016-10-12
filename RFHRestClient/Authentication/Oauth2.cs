using System;
using System.Net.Http.Headers;
using System.Net.Http;
using log4net;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace RFHRestClient
{
    namespace Authentication
    {
        public class Oauth2 : HttpAuthType
        {
            private static readonly ILog LOGGER = LogManager.GetLogger(typeof(Oauth2));
            public string AccessTokenUri { get; set; }
            public string ClientId { get; set; }
            public string ClientSecret { get; set;}
            public string Scope { get; set; }
            public string GrantType { get; set; }
            

            public Oauth2(string accessTokenUri, string clientId,string clientSecret, string scope, string grantType) {

                AccessTokenUri = accessTokenUri;
                ClientId = clientId;
                ClientSecret = clientSecret;
                Scope = scope;
                GrantType = grantType;
                
                try
                {
                    Authenticate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
            }

            public override void Authenticate()
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(AccessTokenUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", encodeCredentials(ClientId, ClientSecret));          

                    try
                    {
                        httpResponseMessage = client.PostAsync("", HttpContent()).Result;
                        
                    }
                    catch (AggregateException ex)
                    {

                        throw new HttpRequestException("Request timed out", ex);
                    }
                }

                Oauth2Token token = null;
                token = JsonConvert.DeserializeObject<Oauth2Token>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                authorization = new AuthenticationHeaderValue(token.token_type, token.access_token);                              

            }

            private string encodeCredentials(string clientId, string clientSecret)
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(clientId+":"+clientSecret);
                return System.Convert.ToBase64String(plainTextBytes);                
            }

            private HttpContent HttpContent()
            {
                KeyValuePair<string, string> grant_type = new KeyValuePair<string, string>("grant_type", GrantType);
                KeyValuePair<string, string> client_credentials = new KeyValuePair<string, string>("scope", Scope);

                List<KeyValuePair<string, string>> nameValueCollection = new List<KeyValuePair<string, string>>();
                nameValueCollection.Add(grant_type);
                nameValueCollection.Add(client_credentials);

                return new FormUrlEncodedContent(nameValueCollection);
            }

        }
    }
}


/*

               client.BaseAddress = new Uri("https://identity.m2mcloud.com");
               client.DefaultRequestHeaders.Accept.Clear();
               client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));             
               client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodeCredentials("0fbe475209b9451bafe3bc668af7b9a9", "527e45a9-35ee-472b-9d4a-80204083dd7e"));

               KeyValuePair<string, string> grant_type = new KeyValuePair<string, string>("grant_type", "client_credentials");
               KeyValuePair<string, string> client_credentials = new KeyValuePair<string, string>("scope", "lighthouse sms");

               List<KeyValuePair<string, string>> nameValueCollection = new List<KeyValuePair<string, string>>();
               nameValueCollection.Add(grant_type);
               nameValueCollection.Add(client_credentials);

               HttpContent content = new FormUrlEncodedContent(nameValueCollection);





               byte[] bytedata = Encoding.UTF8.GetBytes(String.Format($"grant_type={GrantType}&scope={Scope}"));
               HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AccessTokenUri);
               request.Headers.Add("Authorization", encodeCredentials(ClientId,ClientSecret) );
               request.ContentType = "application/x-www-form-urlencoded";
               request.ContentLength = bytedata.Length;
               request.Method = "POST";
               Stream dataStream = request.GetRequestStream();
               dataStream.Write(bytedata, 0, bytedata.Length);
               dataStream.Close();

               HttpWebResponse response = (HttpWebResponse)request.GetResponse();
               JObject jObject = null;
               using (StreamReader reader = new StreamReader(response.GetResponseStream()))
               {
                   jObject = JObject.Parse(reader.ReadToEnd());
               }
               authorization = jObject["token_type"] + " " + jObject["access_token"];
               authorization = new AuthenticationHeaderValue("Bearer", jObject["access_token"].to);
               */

