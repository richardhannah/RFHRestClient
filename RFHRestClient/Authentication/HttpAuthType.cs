using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RFHRestClient
{
    public abstract class HttpAuthType
    {
        protected AuthenticationHeaderValue authorization;
        public AuthenticationHeaderValue Authorization { get { return authorization; } }

        public virtual void Authenticate()
        {

        }

    }
}
