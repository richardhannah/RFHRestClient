using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RFHRestClient
{
    public interface IRestClient
    {
        HttpResponseMessage Get(Uri uri);
        T GetObject<T>();
    }
}
