using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFHRestClient
{

    namespace Authentication
    {

        public class NoAuth : HttpAuthType
        {
            public NoAuth()
            {
                authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("none");
            }
        }
    }
}
