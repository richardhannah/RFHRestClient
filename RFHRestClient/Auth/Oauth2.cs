using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace RFHRestClient.Auth
{
    public class Oauth2 : HttpAuthType
    {
        public string AccessTokenUri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string GrantType { get; set; }


        public Oauth2(string accessTokenUri, string clientId, string clientSecret, string scope, string grantType)
        {

            AccessTokenUri = accessTokenUri;
            ClientId = clientId;
            ClientSecret = clientSecret;
            Scope = scope;
            GrantType = grantType;

            try
            {
                Authenticate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public override void Authenticate()
        {
            byte[] bytedata = Encoding.UTF8.GetBytes(String.Format($"grant_type={GrantType}&scope={Scope}"));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AccessTokenUri);
            request.Headers.Add("Authorization", encodeCredentials(ClientId, ClientSecret));
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
        }

        private string encodeCredentials(string clientId, string clientSecret)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(clientId + ":" + ClientSecret);
            return "Basic " + System.Convert.ToBase64String(plainTextBytes);
        }


    }
}
