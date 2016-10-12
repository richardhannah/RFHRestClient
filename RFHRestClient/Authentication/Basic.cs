using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFHRestClient.Authentication
{
    public class Basic : HttpAuthType
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }


        public Basic(string clientId,string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }


        private string encodeCredentials(string clientId, string clientSecret)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(clientId + ":" + ClientSecret);
            return "Basic " + System.Convert.ToBase64String(plainTextBytes);
        }


    }
}
