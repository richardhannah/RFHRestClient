using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RFHRestClient.Config
{
    public abstract class Configuration
    {
        public String BaseUrl { get; set; }
        public HttpAuthType AuthType { get; set; }
        public int Timeout { get; set; }
        public int Retries { get; set; }
        public MediaTypeWithQualityHeaderValue MediaType { get; set; }

    }
}
